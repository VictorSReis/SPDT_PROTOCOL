using SPDTSdk;


namespace SPDTCore.Core.Protocol;

/// <summary>
/// Representa o Header de um pacote do protocolo.
/// </summary>
public interface ISPDTHeader
{
    public SPDTProtocolVersion ProtocolVersion { get; }

    public SPDTPacketType PacketType { get; }

    public UInt32 StreamID { get; }

    public UInt32 FragmentID { get; }

    public UInt24 PayloadLenght { get; }


    public void LoadProtocolHeader
        (Memory<byte> pBufferHeader);

    public void ResetHeader();
}
