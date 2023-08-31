using SPDTCore.Core.Protocol;
using SPDTSdk;

namespace SPDTCore.Core.SPDT;

/// <summary>
/// Interface responsável por conter todos os fluxos para a conexão atual.
/// </summary>
public interface ISPDTCoreStreams
{
    public UInt16 MaximumStreams { get; }


    /// <summary>
    /// Cria todos os recursos necessários.
    /// </summary>
    /// <param name="pMaximumStreams">A quantiade maximas de streams.</param>
    public void CreateSPDTCoreStream
        (UInt16 pMaximumStreams);

    /// <summary>
    /// Cria um novo fluxo, mais não regista.
    /// </summary>
    /// <param name="pStreamID">O ID para este novo fluxo.</param>
    /// <param name="pStreamState">O estado para o fluxo.</param>
    /// <returns></returns>
    public ISPDTStream CreateStream
        (UInt16 pStreamID, SPDTStreamState pStreamState);

    /// <summary>
    /// Registra um novo fluxo no sistema.
    /// </summary>
    /// <param name="pStream">O fluxo a ser registrado.</param>
    public void RegisterStream(ISPDTStream pStream);

    /// <summary>
    /// Retorna a quantidade de stream atualmente em uso.
    /// </summary>
    /// <returns></returns>
    public UInt16 GetCountStreams();
    
}
