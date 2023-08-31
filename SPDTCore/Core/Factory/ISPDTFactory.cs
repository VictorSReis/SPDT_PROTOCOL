﻿using SPDTCore.Core.Protocol;
using SPDTCore.Core.Stream;
using System;

namespace SPDTCore.Core.Factory;

public interface ISPDTFactory
{
    public ISPDTHeader CreateHeaderObject();

    public ISPDTFrame CreateFrameObject();

    public ISPDTStreamMessages CreateStreamMessages();

    public ISPDTStreamManager CreateStreamManager();
}