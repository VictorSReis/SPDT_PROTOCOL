using SPDTCore.Core;
using SPDTCore.Core.SPDT;
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
    public Memory<byte> CreateMessage_RequestNewStreamEndpoint
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

    public Memory<byte> CreateMessage_CreatedStreamSuccessfully
        (UInt32 pStreamID)
    {
        //MESSAGE STRUCTURE:
        //HEADER
        //FRAME - NÃO É NECESSÁRIO.
        var MemoryData = CreateBufferMemory(SPDTConstants.SIZE_HEADER_OBJECT);

        //ASSEMBLE FRAME
        AssembleHeaderWithoutFrame
            (MemoryData, SPDTPacketType.PACKET_TP_STREAM_CREATED_SUCESSFULLY, pStreamID);

        return MemoryData;
    }

    public Memory<byte> CreateMessage_Data
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
        //WRIT FRAME PAYLOAD
        AssembleFramePayload(MemoryDataSliceFramePart, PayloadFrameData);

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
