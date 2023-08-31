using SPDTCore.Core.Protocol;
using SPDTSdk;

namespace SPDTCore.Core.Stream;

/// <summary>
/// Essa interface representa o gerente(proxy) responsável por comunicar o fluxo sobre alterações, mensagens
/// e qualquer outra ação necessária.
/// O Owner dessa interface é a SPDTCore.
/// </summary>
public interface ISPDTStreamManager
{
    public event EventHandler<UInt32> OnSetIDStream;
    public event EventHandler<Guid> OnSetGuidStream;
    public event EventHandler<SPDTStreamState> OnUpdateStreamState;
    public event EventHandler<ISPDTMessage> OnNewMessage;


    public void InvokeSetIDStream(UInt32 pIDStream);

    public void InvokeSetGuidStream(Guid pGuidStream);

    public void InvokeUpdateStreamState(SPDTStreamState pStreamState);

    public void InvokeNewMessage(ISPDTMessage pNewMessage);
}
