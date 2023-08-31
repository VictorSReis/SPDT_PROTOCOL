using SPDTCore.Core;
using SPDTCore.Core.Factory;
using SPDTCore.Core.Protocol;

namespace SPDTImpl;

public sealed class SPDTGlobalObjects : ISPDTGlobalObjects
{
    #region PROPERITES
    public ISPDTProtocol SpdtProtocol { get; private set; }

    public ISPDTFactory SpdtFactory { get; private set; }
    #endregion

    #region ISPDTGlobalObjects
    public void RegisterSPDTFactory
        (ISPDTFactory pFactory)
    {
        SpdtFactory = pFactory;
    }

    public void RegisterSPDTProtocol
        (ISPDTProtocol pProtocol)
    {
        SpdtProtocol = pProtocol;
    }
    #endregion
}
