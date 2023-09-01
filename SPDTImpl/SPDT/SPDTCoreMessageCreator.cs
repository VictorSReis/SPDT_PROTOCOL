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
    public Memory<byte> CreateMessage_CreateNewStreamEndpoint
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
    #endregion

    #region PRIVATE
    private static Memory<byte> CreateBufferMemory(int pSize)
    {
        Memory<byte> BufferCreated = default;
        BufferCreated = new byte[pSize];
        return BufferCreated;
    }

    private void AssembleHeaderWithoutFrame
        (Memory<byte> pBuffer, 
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
    #endregion
}
