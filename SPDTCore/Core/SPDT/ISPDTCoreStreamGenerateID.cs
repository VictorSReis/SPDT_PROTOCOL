namespace SPDTCore.Core.SPDT;

/// <summary>
/// Interface responsável por gerar ID`s de forma segura para os fluxos.
/// </summary>
public interface ISPDTCoreStreamGenerateID
{
    public UInt32 GenerateStreamID();
}
