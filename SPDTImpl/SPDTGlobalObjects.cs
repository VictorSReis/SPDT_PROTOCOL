using SPDTCore.Core;
using SPDTCore.Core.Factory;
using SPDTCore.Core.Protocol;
using SPDTCore.Core.SPDT;

namespace SPDTImpl;

public sealed class SPDTGlobalObjects : ISPDTGlobalObjects
{
    #region PROPERITES
    public ISPDTProtocol SpdtProtocol { get; private set; }

    public ISPDTFactory SpdtFactory { get; private set; }

    public ISPDTCoreValidator SpdtValidator { get; private set; }
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

    public void RegisterSPDTValidator
        (ISPDTCoreValidator pValidator)
    {
        SpdtValidator = pValidator;
    }
    #endregion
}
