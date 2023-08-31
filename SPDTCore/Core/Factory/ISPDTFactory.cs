using SPDTCore.Core.Protocol;
using System;

namespace SPDTCore.Core.Factory;

public interface ISPDTFactory
{
    public ISPDTHeader CreateHeaderObject();

    public ISPDTFrame CreateFrameObject();
}
