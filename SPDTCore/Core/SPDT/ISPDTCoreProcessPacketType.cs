using SPDTCore.Core.Protocol;
using SPDTSdk;

namespace SPDTCore.Core.SPDT;

/// <summary>
/// Interface responsável por processar todas as mensagens (<see cref="SPDTPacketType"/>).
/// </summary>
public interface ISPDTCoreProcessPacketType
{
    public void ProcessMessagePacketType(ISPDTMessage pMessage);
}
