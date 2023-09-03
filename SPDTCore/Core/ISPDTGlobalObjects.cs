using SPDTCore.Core.Factory;
using SPDTCore.Core.Protocol;
using SPDTCore.Core.SPDT;

namespace SPDTCore.Core;

public interface ISPDTGlobalObjects
{
    public ISPDTProtocol SpdtProtocol { get; }

    public ISPDTFactory SpdtFactory { get; }

    public ISPDTCoreValidator SpdtValidator { get; }


    public void RegisterSPDTProtocol(ISPDTProtocol pProtocol);

    public void RegisterSPDTFactory(ISPDTFactory pFactory);

    public void RegisterSPDTValidator(ISPDTCoreValidator pValidator);
}
