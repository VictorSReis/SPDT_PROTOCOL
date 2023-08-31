using SPDTCore.Core.Readers;
using SPDTCore.Core.Writers;

namespace SPDTCore.Core.Protocol;

public interface ISPDTProtocol: 
    ISPDTProtocolHeaderReader,
    ISPDTProtocolFrameReader,
    ISPDTProtocolHeaderWriter, 
    ISPDTProtocolFrameWriter
{

}

