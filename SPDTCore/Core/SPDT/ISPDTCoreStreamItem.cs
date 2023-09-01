

using SPDTCore.Core.Protocol;
using SPDTCore.Core.Stream;

namespace SPDTCore.Core.SPDT;

/// <summary>
/// Representa um objeto que contém outros objetos importantes para um determinado fluxo.
/// </summary>
public interface ISPDTCoreStreamItem
{
    public UInt32 StreamID { get; }

    public ISPDTStream SpdtStream { get; }

    public ISPDTStreamManager SpdtStreamManager { get; }

    public ISPDTStreamMessages SpdtStreamMessages { get; }


    public void CreateItem(
        UInt32 pStreamID, 
        ISPDTStream pSpdtStream,
        ISPDTStreamManager pSpdtStmManager, 
        ISPDTStreamMessages pSpdtStmMessages);

    public void Reset();
}
