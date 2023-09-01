using SPDTCore.Core.Protocol;

namespace SPDTCore.Core.SPDT;

public interface ISPDTCoreController
{
    //NOTIFY
    public event EventHandler OnNotifyProcessInputNewData;
    public event EventHandler<ISPDTMessage> OnNotifyProcessInputNewMessage;
    public event EventHandler OnNotifyProcessInputMalformedData;



    //NOTIFY
    public void Invoke_ProcessInput_NewData();
    public void Invoke_ProcessInput_NewMessage(ISPDTMessage pMessage);
    public void Invoke_ProcessInput_Malformed_Data();
}
