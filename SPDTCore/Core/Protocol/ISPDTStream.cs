using SPDTCore.Core.Stream;
using SPDTSdk;
using System;

namespace SPDTCore.Core.Protocol;

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



}
