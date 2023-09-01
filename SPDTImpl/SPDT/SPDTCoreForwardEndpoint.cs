using SPDTCore.Core.SPDT;

namespace SPDTImpl.SPDT;

internal sealed class SPDTCoreForwardEndpoint : ISPDTCoreForwardEndpoint
{
    public event EventHandler<Memory<byte>> OnNewMessageForward;


}
