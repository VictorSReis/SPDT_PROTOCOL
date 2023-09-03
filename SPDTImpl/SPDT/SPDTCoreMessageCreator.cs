using SPDTCore.Core;
using SPDTCore.Core.SPDT;
using SPDTImpl.Protocol;
using SPDTSdk;

namespace SPDTImpl.SPDT;

internal sealed class SPDTCoreMessageCreator : ISPDTCoreMessageCreator
{
    #region PRIVATE VALUES
    private ISPDTGlobalObjects _SpdtGlobalObjects;
    private ISPDTCoreStreamGenerateID _SpdtGenerateStreamID;
    #endregion

    #region CONSTRUCTOR
    internal SPDTCoreMessageCreator
        (ISPDTGlobalObjects pSpdtGlobalObjects, ISPDTCoreStreamGenerateID pSpdtStreamIdGenerator)
    {
        _SpdtGlobalObjects = pSpdtGlobalObjects;
        _SpdtGenerateStreamID = pSpdtStreamIdGenerator;
    }
    #endregion


    #region ISPDTCoreMessageCreator
    public Memory<byte> CreateMessageRequestNewStreamEndpoint
        (out uint pOutStreamIDRequestCreate)
    {
        //MESSAGE STRUCTURE:
        //HEADER
        //FRAME - NÃO É NECESSÁRIO.
        var MemoryData = CreateBufferMemory(SPDTConstants.SIZE_HEADER_OBJECT);

        //CREATE ID
        pOutStreamIDRequestCreate = _SpdtGenerateStreamID.GenerateStreamID();

        //ASSEMBLE FRAME
        AssembleHeaderWithoutFrame
            (MemoryData, SPDTPacketType.PACKET_TP_CREATE_STREAM, pOutStreamIDRequestCreate);

        return MemoryData;
    }

    public Memory<byte> CreateMessageData
        (UInt32 pStreamID, Memory<byte> PayloadFrameData)
    {
        //MESSAGE STRUCTURE:
        //HEADER
        //FRAME
        int TotalSizeMessage = SPDTConstants.SIZE_HEADER_OBJECT + SPDTConstants.SIZE_HEADER_FRAME_OBJECT + PayloadFrameData.Length;
        UInt24 SizePayload = SPDTConstants.SIZE_HEADER_FRAME_OBJECT + PayloadFrameData.Length;
        var MemoryData = CreateBufferMemory(TotalSizeMessage);
        var MemoryDataSliceFramePart = MemoryData[SPDTConstants.SIZE_HEADER_OBJECT..];

        //WRITE HEADER
        AssembleHeader(MemoryData, SPDTPacketType.PACKET_TP_DATA, pStreamID, 0, SizePayload);
        //WRITE HEADER FRAME
        AssembleFrameHeader(MemoryDataSliceFramePart, SPDTFrameType.FRAME_TP_SINGLE, PayloadFrameData.Length);
        //WRITE FRAME PAYLOAD
        AssembleFramePayload(MemoryDataSliceFramePart, PayloadFrameData);

        return MemoryData;
    }

    public Memory<byte> CreateMessageError
        (SPDTError pErrorType, UInt32 pStreamID)
    {
        //MESSAGE STRUCTURE:
        //HEADER
        //FRAME
        int FramePayloadDataSize = 2; //+2 -> SPDTError é um enum de 2 bytes.
        int TotalSize = SPDTConstants.SIZE_HEADER_OBJECT + SPDTConstants.SIZE_HEADER_FRAME_OBJECT + FramePayloadDataSize;
        UInt24 SizePayload = SPDTConstants.SIZE_HEADER_FRAME_OBJECT + FramePayloadDataSize;
        var MemoryData = CreateBufferMemory(TotalSize);
        var MemoryDataSliceFramePart = MemoryData[SPDTConstants.SIZE_HEADER_OBJECT..];

        //WRITE HEADER
        AssembleHeader(MemoryData, SPDTPacketType.PACKET_TP_ERROR, pStreamID, 0, SizePayload);
        //WRITE FRAME HEADER
        AssembleFrameHeader(MemoryDataSliceFramePart, SPDTFrameType.FRAME_TP_SINGLE, FramePayloadDataSize);
        //WRITE FRAME PAYLOAD
        BitWriter.WriterUInt16BigEndian(
            MemoryDataSliceFramePart[SPDTConstants.SIZE_HEADER_FRAME_OBJECT..].Span, //GET SLICE FRAME PAYLOAD
            (UInt16)pErrorType); //ENUM ERROR TYPE TO UINT16 FOR WRITER IN PAYLOAD

        return MemoryData;
    }

    public Memory<byte> CreateMessage
        (SPDTProtocolVersion pProtocolVersion,
        SPDTPacketType pPacketType,
        uint pStreamId,
        uint pFramentId,
        SPDTFrameType pFrameType,
        uint pFramePayloadLenght,
        Memory<byte> pFramePayload)
    {
        int FramePayloadDataSize = pFramePayload.IsEmpty ? -1 : pFramePayload.Length;
        int TotalSize = SPDTConstants.SIZE_HEADER_OBJECT + (FramePayloadDataSize == -1 ? 0 : (SPDTConstants.SIZE_HEADER_FRAME_OBJECT + FramePayloadDataSize));
        UInt24 SizePayload = SPDTConstants.SIZE_HEADER_FRAME_OBJECT + FramePayloadDataSize;
        var MemoryData = CreateBufferMemory(TotalSize);

        //WRITE HEADER
        AssembleHeader(MemoryData, pProtocolVersion, pPacketType, pStreamId, pFramentId, SizePayload);

        //CHECK CONTAIN FRAME PAYLOAD
        if (FramePayloadDataSize > 0)
        {
            var MemoryDataSliceFramePart = MemoryData[SPDTConstants.SIZE_HEADER_OBJECT..];
            //WRITE FRAME HEADER
            AssembleFrameHeader(MemoryDataSliceFramePart, pFrameType, pFramePayloadLenght);
            //WRITE FRAME PAYLOAD
            AssembleFramePayload(MemoryDataSliceFramePart, pFramePayload);
        }

        return MemoryData;
    }

    public Memory<byte> CreateMessage(
        SPDTProtocolVersion pProtocolVersion,
        SPDTPacketType pPacketType,
        uint pStreamId,
        uint pFramentId)
    {
        int TotalSize = SPDTConstants.SIZE_HEADER_OBJECT;
        UInt24 SizePayload = 0;
        var MemoryData = CreateBufferMemory(TotalSize);

        //WRITE HEADER
        AssembleHeader(MemoryData, pProtocolVersion, pPacketType, pStreamId, pFramentId, SizePayload);

        return MemoryData;
    }
    #endregion

    #region PRIVATE
    private static Memory<byte> CreateBufferMemory(int pSize)
    {
        Memory<byte> BufferCreated = default;
        BufferCreated = new byte[pSize];
        return BufferCreated;
    }

    private void AssembleHeaderWithoutFrame(
        Memory<byte> pBuffer, 
        SPDTPacketType pPacketType,
        UInt32 pStreamID,
        UInt32 pFragmentID = 0)
    {
        var Protocol = _SpdtGlobalObjects.SpdtProtocol;

        Protocol.WriteProtocolVersion(SPDTProtocolVersion.ProtocolVersion10, pBuffer);
        Protocol.WritePacketType(pPacketType, pBuffer);
        Protocol.WriteStreamID(pStreamID, pBuffer);
        Protocol.WriteFragmentID(pFragmentID, pBuffer);
        Protocol.WritePayloadLenght(0, pBuffer);
    }

    private void AssembleHeader(
        Memory<byte> pBuffer,
        SPDTPacketType pPacketType,
        UInt32 pStreamID,
        UInt32 pFragmentID,
        UInt24 pSizePayload)
    {
        var Protocol = _SpdtGlobalObjects.SpdtProtocol;

        Protocol.WriteProtocolVersion(SPDTProtocolVersion.ProtocolVersion10, pBuffer);
        Protocol.WritePacketType(pPacketType, pBuffer);
        Protocol.WriteStreamID(pStreamID, pBuffer);
        Protocol.WriteFragmentID(pFragmentID, pBuffer);
        Protocol.WritePayloadLenght(pSizePayload, pBuffer);
    }

    private void AssembleHeader(
        Memory<byte> pBuffer,
        SPDTProtocolVersion pSpdtProtocolVersion,
        SPDTPacketType pPacketType,
        UInt32 pStreamID,
        UInt32 pFragmentID,
        UInt24 pSizePayload)
    {
        var Protocol = _SpdtGlobalObjects.SpdtProtocol;

        Protocol.WriteProtocolVersion(pSpdtProtocolVersion, pBuffer);
        Protocol.WritePacketType(pPacketType, pBuffer);
        Protocol.WriteStreamID(pStreamID, pBuffer);
        Protocol.WriteFragmentID(pFragmentID, pBuffer);
        Protocol.WritePayloadLenght(pSizePayload, pBuffer);
    }

    private void AssembleFrameHeader(
        Memory<byte> pBufferFrame,
        SPDTFrameType pFrameType,
        UInt24 pFramePayloadLenght)
    {
        var Protocol = _SpdtGlobalObjects.SpdtProtocol;

        Protocol.WriteFrameType(pFrameType, pBufferFrame);
        Protocol.WriteFrameLenght(pFramePayloadLenght, pBufferFrame);
    }

    private void AssembleFramePayload
        (Memory<byte> pBufferFrame, Memory<byte> pFramePayloadData)
    {
        var Protocol = _SpdtGlobalObjects.SpdtProtocol;
        Protocol.WriteFramePayload(pFramePayloadData, pBufferFrame);
    }
    #endregion
}
