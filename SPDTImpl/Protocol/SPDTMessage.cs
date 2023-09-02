using SPDTCore.Core;
using SPDTCore.Core.Protocol;
using SPDTSdk;

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

        //WRITE HEADER
        var MemHeader = _MemoryMessageObject[..(SPDTConstants.SIZE_HEADER_OBJECT)];
        Header.LoadProtocolHeader(MemHeader);

        //WRITE FRAME IF PRESENT
        if (Header.PacketType == SPDTPacketType.PACKET_TP_DATA)
        {
            var MemFrame = _MemoryMessageObject[SPDTConstants.SIZE_HEADER_OBJECT..];
            Frame.LoadFrameProtocol(MemFrame);
        }
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
