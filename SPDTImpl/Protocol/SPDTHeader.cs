using SPDTCore.Core;
using SPDTCore.Core.Protocol;
using SPDTSdk;

namespace SPDTImpl.Protocol;

/// <summary>
/// Implementação default da interface <see cref="ISPDTHeader"/>.
/// </summary>
public sealed class SPDTHeader : ISPDTHeader
{
    #region PROPERTIES
    public SPDTProtocolVersion ProtocolVersion { get; private set; }

    public SPDTPacketType PacketType { get; private set; }

    public uint StreamID { get; private set; }

    public uint FragmentID { get; private set; }

    public UInt24 PayloadLenght { get; private set; }
    #endregion

    #region FRAME MEMORY
    private Memory<byte> _HeaderMemoryData;
    #endregion

    #region GLOBAL OBJECTS
    private ISPDTGlobalObjects _SPDTGlobalObjects;
    #endregion


    #region CONSTRUCTOR
    internal SPDTHeader
        (ISPDTGlobalObjects pGlobalObjects)
    {
        _SPDTGlobalObjects = pGlobalObjects;
    }
    #endregion

    #region ISPDTHeader
    public void LoadProtocolHeader
        (Memory<byte> pBufferHeader)
    {
        //SET HEADER DATA.
        _HeaderMemoryData = pBufferHeader;

        //LOAD PROPERTIES.
        ProtocolVersion = _SPDTGlobalObjects.SpdtProtocol.
            ReadProtocolVersion(_HeaderMemoryData.Span);

        PacketType = _SPDTGlobalObjects.SpdtProtocol.
            ReadPacketType(_HeaderMemoryData.Span);
        bool ValidatePacketType = _SPDTGlobalObjects.SpdtValidator.ValidatePacketType((byte)PacketType);
        if (!ValidatePacketType)
            throw new Exception("MALFORMED PACKET");

        StreamID = _SPDTGlobalObjects.SpdtProtocol.
            ReadStreamID(_HeaderMemoryData.Span);

        FragmentID = _SPDTGlobalObjects.SpdtProtocol.
            ReadFragmentID(_HeaderMemoryData.Span);

        PayloadLenght = _SPDTGlobalObjects.SpdtProtocol.
            ReadPayloadLenght(_HeaderMemoryData.Span);
    }

    public void ResetHeader()
    {
        throw new NotImplementedException();
    }
    #endregion
}
