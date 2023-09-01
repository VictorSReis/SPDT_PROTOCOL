using SPDTCore.Core.Protocol;

namespace SPDTCore.Core.SPDT;

/// <summary>
/// Interface responsável por encaminhar uma mensagem <see cref="ISPDTMessage"/> para o fluxo de destino.
/// </summary>
public interface ISPDTCoreForwarderMessage
{
    public bool Forwarder(ISPDTMessage pMessage);
}
