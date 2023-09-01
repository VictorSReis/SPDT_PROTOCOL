using SPDTCore.Core.Handles;
using SPDTCore.Core.SPDT;

namespace SPDTImpl.SPDT;

internal sealed class SPDTCoreForwardEndpoint : ISPDTCoreForwardEndpoint
{
    #region EVENTS
    public event EventHandler<Memory<byte>> OnNewMessageForward;
    #endregion


    #region CONSTRUCTOR
    public SPDTCoreForwardEndpoint
        (ref SPDTCoreHandles.NotifyUserForwardMessageHandle pRefHandleNotify)
    {
        pRefHandleNotify = NotifyForwardMessageHandle;
    }
    #endregion

    #region HANDLE NOTIFY BY SPDTCore
    private void NotifyForwardMessageHandle(Memory<byte> pBufferMemory)
    {
        OnNewMessageForward?.Invoke(this, pBufferMemory);
    }
    #endregion
}
