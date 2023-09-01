
namespace SPDTCore.Core.SPDT;

public interface ISPDTCoreMessageCreator
{
    public Memory<byte> CreateMessage_CreateNewStreamEndpoint();
}
