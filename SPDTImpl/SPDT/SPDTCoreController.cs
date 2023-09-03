using SPDTCore.Core.Protocol;
using SPDTCore.Core.SPDT;
using SPDTSdk;

namespace SPDTImpl.SPDT;


internal sealed class SPDTCoreController : ISPDTCoreController
{
    #region EVENTS
    public event EventHandler<int> OnNotifyNewMessageDataReceived;
    public event EventHandler<ISPDTMessage> OnNotifyNewPublicMessage;
    public event EventHandler<NotifyErrorObject> OnNotifyError;
    public event EventHandler<UInt32> OnNotifyCreateNewStream;
    public event EventHandler<UInt32> OnNotifyResetStream;
    public event EventHandler<UInt32> OnNotifyCloseStream;
    public event EventHandler<UInt32> OnNotifyPingStream;
    #endregion

    #region ISPDTCoreController
    public void InvokeNotifyError
        (NotifyErrorObject pSpdtError)
    {
        OnNotifyError?.Invoke(this, pSpdtError);
    }

    public void InvokeNotifyNewMessageDataReceived
        (int pCountBytesReceived)
    {
        OnNotifyNewMessageDataReceived?.Invoke(this, pCountBytesReceived);
    }

    public void InvokeNotifyNewPublicMessage
        (ISPDTMessage pMessage)
    {
        OnNotifyNewPublicMessage?.Invoke(this, pMessage);
    }

    public void InvokeNotifyCreateNewStream
        (uint pStreamID)
    {
        OnNotifyCreateNewStream?.Invoke(this, pStreamID);
    }

    public void InvokeNotifyCloseStream
        (uint pStreamID)
    {
        OnNotifyCloseStream?.Invoke(this, pStreamID);
    }

    public void InvokeNotifyPingStream
        (uint pStreamID)
    {
        OnNotifyPingStream?.Invoke(this, pStreamID);
    }

    public void InvokeNotifyResetStream
        (uint pStreamID)
    {
        OnNotifyResetStream?.Invoke(this, pStreamID);
    }
    #endregion
}
