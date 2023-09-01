using SPDTCore.Core.Protocol;
using SPDTCore.Core.SPDT;

namespace SPDTImpl.SPDT;

internal struct SPDTCoreController : ISPDTCoreController
{
    public event EventHandler OnNotifyProcessInputNewData;
    public event EventHandler<ISPDTMessage> OnNotifyProcessInputNewMessage;
    public event EventHandler OnNotifyProcessInputMalformedData;

    public void Invoke_ProcessInput_Malformed_Data()
    {
        OnNotifyProcessInputMalformedData?.Invoke(this, null);
    }

    public void Invoke_ProcessInput_NewData()
    {
        OnNotifyProcessInputNewData?.Invoke(this, null);
    }

    public void Invoke_ProcessInput_NewMessage(ISPDTMessage pMessage)
    {
        OnNotifyProcessInputNewMessage?.Invoke(this, pMessage);
    }
}
