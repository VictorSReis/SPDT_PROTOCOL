using SPDTCore.Core.Stream;
using SPDTSdk;

namespace SPDTCore.Core.Protocol;

/// <summary>
/// Representa uma linha de fluxo dentro de uma conexão.
/// </summary>
public interface ISPDTStream
{
    /// <summary>
    /// Contém o ID do fluxo.
    /// </summary>
    public UInt32 StreamID { get; }

    /// <summary>
    /// Um identificador do objeto atual.
    /// </summary>
    public Guid StreamGuid { get; }

    /// <summary>
    /// Informa o esatado atual do fluxo.
    /// </summary>
    public SPDTStreamState StreamState { get; }

    /// <summary>
    /// Contém informações de estatisticas sobre o fluxo atual.
    /// </summary>
    public ISPDTStreamStatistics StreamStatistics { get; }


    /// <summary>
    /// Verifica se há dados disponiveis no fluxo.
    /// </summary>
    /// <returns></returns>
    public bool AvailebleData();

    /// <summary>
    /// Obtém a proxima mensagem disponivel no fluxo.
    /// </summary>
    /// <returns></returns>
    public ISPDTMessage GetSPDTMessage();
}
