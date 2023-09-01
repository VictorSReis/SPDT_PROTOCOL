using SPDTCore.Core.Protocol;
using SPDTCore.Core.Stream;
using System;

namespace SPDTCore.Core.Factory;

public interface ISPDTFactory
{
    public ISPDTHeader CreateHeaderObject();

    public ISPDTFrame CreateFrameObject();

    public ISPDTMessage CreateMessageObject();

    public ISPDTStreamMessages CreateStreamMessages();

    public ISPDTStreamManager CreateStreamManager();

    public ISPDTStream CreateSPDTStream
        (ISPDTStreamManager pStreamManager, ISPDTStreamMessages pStreamMensages);
}
