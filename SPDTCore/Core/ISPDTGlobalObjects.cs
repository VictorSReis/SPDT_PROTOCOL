using SPDTCore.Core.Factory;
using SPDTCore.Core.Protocol;

namespace SPDTCore.Core;

public interface ISPDTGlobalObjects
{
    public ISPDTProtocol SpdtProtocol { get; }

    public ISPDTFactory SpdtFactory { get; }


    public void RegisterSPDTProtocol(ISPDTProtocol pProtocol);

    public void RegisterSPDTFactory(ISPDTFactory pFactory);
}
