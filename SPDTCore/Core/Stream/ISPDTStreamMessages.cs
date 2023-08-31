using System.Collections.Concurrent;
using SPDTCore.Core.Protocol;

namespace SPDTCore.Core.Stream;

/// <summary>
/// Interface que armazena as menagens (<see cref="ISPDTMessage"/>) e objetos internos 
/// necessário para cada fluxo.
/// </summary>
public interface ISPDTStreamMessages
{
    public Guid StreamGuid { get; }


    public void CreateStreamMessages(Guid pStreamGuid);

    public void EnqueueMessage(ISPDTMessage pNewMessage);

    public bool MessageAvaileble();

    public ISPDTMessage GetNextMessage();
}
