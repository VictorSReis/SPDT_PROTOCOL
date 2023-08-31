using SPDTCore.Core.Protocol;

namespace SPDTCore.Core.SPDT;

/// <summary>
/// Interface responsável por conter todos os fluxos para a conexão atual.
/// </summary>
public interface ISPDTCoreStreams
{
    public UInt16 MaximumStreams { get; }

    public UInt16 SizeStream { get; }



    public void CreateSPDTCoreStream
        (UInt16 pMaximumStreams, UInt16 pMaximumSizeStream);

    public ISPDTStream CreateStream()
    
}
