using SPDTSdk;

namespace SPDTCore.Core.Writers;

public interface ISPDTProtocolHeaderWriter
{
    public void WriteProtocolVersion
        (SPDTProtocolVersion pProtocolVersion, Memory<Byte> pBufferHeader);

    public void WritePacketType
        (SPDTPacketType pPacketType, Memory<Byte> pBufferHeader);

    public void WriteStreamID
        (UInt32 pStreamID, Memory<Byte> pBufferHeader);

    public void WriteFragmentID
        (UInt32 pFragmentID, Memory<Byte> pBufferHeader);
}
