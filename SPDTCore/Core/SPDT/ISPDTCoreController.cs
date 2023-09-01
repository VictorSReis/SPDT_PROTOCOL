using SPDTCore.Core.Protocol;

namespace SPDTCore.Core.SPDT;

public interface ISPDTCoreController
{
    //NOTIFY
    public event EventHandler OnNotifyProcessInputNewData;
    public event EventHandler<ISPDTMessage> OnNotifyProcessInputNewMessage;
    public event EventHandler OnNotifyProcessInputMalformedData;
    public event EventHandler<UInt32> OnNotifyProcessInputCreateNewStream;
    public event EventHandler<UInt32> OnNotifyProcessInputResetStream;
    public event EventHandler<UInt32> OnNotifyProcessInputCloseStream;
    public event EventHandler<UInt32> OnNotifyProcessInputPingStream;


    //NOTIFY
    public void Invoke_ProcessInputNewData();
    public void Invoke_ProcessInputNewMessage(ISPDTMessage pMessage);
    public void Invoke_ProcessInputMalformedData();
    public void Invoke_ProcessInputCreateNewStream(UInt32 pStreamID);
    public void Invoke_ProcessInputResetStream(UInt32 pStreamID);
    public void Invoke_ProcessInputCloseStream(UInt32 pStreamID);
    public void Invoke_ProcessInputPingStream(UInt32 pStreamID);
}
