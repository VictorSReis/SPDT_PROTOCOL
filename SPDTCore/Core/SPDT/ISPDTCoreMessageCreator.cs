
namespace SPDTCore.Core.SPDT;

/// <summary>
/// Interface responsável por criar mensagens, em bytes, para serem transmitidas ao endpoint.
/// </summary>
public interface ISPDTCoreMessageCreator
{
    public Memory<byte> CreateMessage_CreateNewStreamEndpoint
        (out UInt32 pOutStreamIDRequestCreate);

    public Memory<byte> CreateMessage_CreatedStreamSuccessfully
        (UInt32 pStreamID);
}
