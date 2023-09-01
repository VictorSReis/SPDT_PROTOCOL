using SPDTCore.Core.Protocol;
using SPDTSdk;

namespace SPDTCore.Core.SPDT;

/// <summary>
/// Interface responsável por processar todas as mensagens (<see cref="SPDTPacketType"/>) 
/// que não sejam do tipo (<see cref="SPDTPacketType.PACKET_TP_DATA"/>).
/// </summary>
public interface ISPDTCoreProcessPacketType
{
    public void ProcessMessagePacketType(ISPDTMessage pMessage);
}
