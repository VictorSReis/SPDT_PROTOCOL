using SPDTCore.Core.Protocol;
using SPDTSdk;

namespace SPDTCore.Core.SPDT;

/// <summary>
/// Interface utilizada internamente para receber notificações sobre processos internos.
/// </summary>
public interface ISPDTCoreController
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

    #region METHODS FOR INVOKE EVENTS
    public void InvokeNotifyNewMessageDataReceived
        (int pCountBytesReceived);
    public void InvokeNotifyNewPublicMessage
        (ISPDTMessage pMessage);
    public void InvokeNotifyError
        (NotifyErrorObject pSpdtError);
    public void InvokeNotifyCreateNewStream
        (UInt32 pStreamID);
    public void InvokeNotifyResetStream
        (UInt32 pStreamID);
    public void InvokeNotifyCloseStream
        (UInt32 pStreamID);
    public void InvokeNotifyPingStream
        (UInt32 pStreamID);
    #endregion
}
