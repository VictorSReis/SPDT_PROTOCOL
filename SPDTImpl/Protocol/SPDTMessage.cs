﻿using SPDTCore.Core;
using SPDTCore.Core.Protocol;

namespace SPDTImpl.Protocol;

/// <summary>
/// Implementação default da interface <see cref="ISPDTMessage"/>.
/// </summary>
public sealed class SPDTMessage : ISPDTMessage
{
    #region PROPERTIES
    public ISPDTHeader Header { get; private set; }

    public ISPDTFrame Frame { get; private set; }
    #endregion

    #region MEMORY OBJECT MESSAGE
    private Memory<byte> _MemoryMessageObject;
    #endregion

    #region GLOBAL OBJECTS
    private ISPDTGlobalObjects _SPDTGlobalObjects;
    #endregion


    #region CONSTRUCTOR
    internal SPDTMessage
        (ISPDTGlobalObjects pSPDTGlobalObjects)
    {
        _SPDTGlobalObjects = pSPDTGlobalObjects;
    }
    #endregion

    #region ISPDTMessage
    public void CreateMessage()
    {
        Header = _SPDTGlobalObjects.SpdtFactory.CreateHeaderObject();
        Frame = _SPDTGlobalObjects.SpdtFactory.CreateFrameObject();
    }

    public void AssembleMessage
        (Memory<byte> pBufferMessage)
    {
        //SET MEMORY OBJECT THIS MESSAGE
        _MemoryMessageObject = pBufferMessage;

        //HEADER - 11 BYTES
        //FRAME  - 4 + PAYLOAD.
        var MemHeader = _MemoryMessageObject[..11];
        var MemFrame = _MemoryMessageObject[11..];

        //LOAD
        Header.LoadProtocolHeader(MemHeader);
        Frame.LoadFrameProtocol(MemFrame);
    }

    public void ResetMessage()
    {
        Header?.ResetHeader();
        Frame?.ResetFrame();
    }

    public void ReleaseMessageMemory()
    {
       if(!_MemoryMessageObject.IsEmpty)
        {
            _MemoryMessageObject.Span.Fill(0);
            _MemoryMessageObject.Span.Clear();
            _MemoryMessageObject = new byte[1];
            _MemoryMessageObject = null;
        }
    }
    #endregion
}