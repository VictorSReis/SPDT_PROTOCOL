﻿using SPDTCore.Core;
using SPDTCore.Core.Factory;
using SPDTCore.Core.Protocol;
using SPDTCore.Core.Stream;
using SPDTImpl.Protocol;
using SPDTImpl.Stream;

namespace SPDTImpl.Factory;

/// <summary>
/// Implementação default da interface <see cref="ISPDTFactory"/>.
/// </summary>
public sealed class SPDTFactory : ISPDTFactory
{
    #region SPDT GLOBAL OBJECTS
    private ISPDTGlobalObjects _SPDTGlobalObjects;
    #endregion


    #region CONSTRUCTOR
    internal SPDTFactory
        (ISPDTGlobalObjects pSPDTGlobalObjects)
    {
        _SPDTGlobalObjects = pSPDTGlobalObjects;
    }
    #endregion

    #region ISPDTFactory
    public ISPDTFrame CreateFrameObject()
    {
        var SpdtFrame = new SPDTFrame(_SPDTGlobalObjects);
        return SpdtFrame;
    }

    public ISPDTHeader CreateHeaderObject()
    {
        var SpdtHeader = new SPDTHeader(_SPDTGlobalObjects);
        return SpdtHeader;
    }

    public ISPDTMessage CreateMessageObject()
    {
        var NewMessage = new SPDTMessage(_SPDTGlobalObjects);
        return NewMessage;
    }

    public ISPDTStreamManager CreateStreamManager()
    {
        var SpdtStreamManager = new SPDTStreamManager();
        return SpdtStreamManager;
    }

    public ISPDTStreamMessages CreateStreamMessages()
    {
        var SpdtStreamMessages = new SPDTStreamMessages();
        return SpdtStreamMessages;
    }

    public ISPDTStream CreateSPDTStream
        (ISPDTStreamManager pStreamManager, ISPDTStreamMessages pStreamMensages)
    {
        var NewStream = new SPDTStream(pStreamManager, pStreamMensages);
        return NewStream;
    }
    #endregion
}
