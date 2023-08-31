using SPDTCore.Core.Protocol;
using SPDTCore.Core.Stream;
using SPDTSdk;

namespace SPDTImpl.Stream;

internal class SPDTStreamManager : ISPDTStreamManager
{
    #region EVENTOS
    public event EventHandler<uint> OnSetIDStream;
    public event EventHandler<Guid> OnSetGuidStream;
    public event EventHandler<SPDTStreamState> OnUpdateStreamState;
    public event EventHandler<ISPDTMessage> OnNewMessage;
    #endregion

    #region MÉTODOS DE GERÊNCIA
    public void InvokeSetIDStream(uint pIDStream)
    {
        OnSetIDStream?.Invoke(this, pIDStream);
    }

    public void InvokeSetGuidStream(Guid pGuidStream)
    {
        OnSetGuidStream?.Invoke(this, pGuidStream);
    }

    public void InvokeUpdateStreamState(SPDTStreamState pStreamState)
    {
        OnUpdateStreamState?.Invoke(this, pStreamState);
    }

    public void InvokeNewMessage(ISPDTMessage pNewMessage)
    {
        OnNewMessage?.Invoke(this, pNewMessage);
    }
    #endregion
}
