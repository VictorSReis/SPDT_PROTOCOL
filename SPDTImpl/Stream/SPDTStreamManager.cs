using SPDTCore.Core.Protocol;
using SPDTCore.Core.Stream;
using SPDTSdk;

namespace SPDTImpl.Stream;

internal sealed class SPDTStreamManager : ISPDTStreamManager
{
    #region EVENTOS
    public event EventHandler<uint> OnSetIDStream;
    public event EventHandler<Guid> OnSetGuidStream;
    public event EventHandler<SPDTStreamState> OnUpdateStreamState;
    public event EventHandler<ISPDTMessage> OnNewMessage;
    public event EventHandler OnResetStream;
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

    public void InvokeResetStream()
    {
        OnResetStream?.Invoke(this, null);
    }
    #endregion
}
