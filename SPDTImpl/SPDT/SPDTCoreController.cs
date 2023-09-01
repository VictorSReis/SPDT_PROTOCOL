using SPDTCore.Core.Protocol;
using SPDTCore.Core.SPDT;

namespace SPDTImpl.SPDT;

internal struct SPDTCoreController : ISPDTCoreController
{
    public event EventHandler OnNotifyProcessInputNewData;
    public event EventHandler<ISPDTMessage> OnNotifyProcessInputNewMessage;
    public event EventHandler OnNotifyProcessInputMalformedData;
    public event EventHandler<uint> OnNotifyProcessInputCreateNewStream;
    public event EventHandler<uint> OnNotifyProcessInputResetStream;
    public event EventHandler<uint> OnNotifyProcessInputCloseStream;
    public event EventHandler<uint> OnNotifyProcessInputPingStream;


    public void Invoke_ProcessInputMalformedData()
    {
        OnNotifyProcessInputMalformedData?.Invoke(this, null);
    }

    public void Invoke_ProcessInputNewData()
    {
        OnNotifyProcessInputNewData?.Invoke(this, null);
    }

    public void Invoke_ProcessInputNewMessage
        (ISPDTMessage pMessage)
    {
        OnNotifyProcessInputNewMessage?.Invoke(this, pMessage);
    }

    public void Invoke_ProcessInputCreateNewStream
        (uint pStreamID)
    {
        OnNotifyProcessInputCreateNewStream?.Invoke(this, pStreamID);
    }

    public void Invoke_ProcessInputCloseStream
        (uint pStreamID)
    {
        OnNotifyProcessInputCloseStream?.Invoke(this, pStreamID);
    }

    public void Invoke_ProcessInputPingStream
        (uint pStreamID)
    {
        OnNotifyProcessInputPingStream?.Invoke(this, pStreamID);
    }

    public void Invoke_ProcessInputResetStream
        (uint pStreamID)
    {
        OnNotifyProcessInputResetStream?.Invoke(this, pStreamID);
    }
}
