namespace SPDTCore.Core.SPDT;

/// <summary>
/// Interface utilizada pelo servidor para comunicar o cliente que uma mensagem deve ser encaminhada para o endpoint.
/// </summary>
public interface ISPDTCoreForwardEndpoint
{
    /// <summary>
    /// Evento que notifica que o usuário deve enviar uma determinada mensagem para o seu endpoint.
    /// </summary>
    public event EventHandler<Memory<byte>> OnNewMessageForward;
}
