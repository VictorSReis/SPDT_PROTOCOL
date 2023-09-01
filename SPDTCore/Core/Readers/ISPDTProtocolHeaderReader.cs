using SPDTSdk;

namespace SPDTCore.Core.Readers;

public interface ISPDTProtocolHeaderReader
{
    public SPDTProtocolVersion ReadProtocolVersion
        (ReadOnlySpan<byte> pBufferHeader);

    public SPDTPacketType ReadPacketType
        (ReadOnlySpan<byte> pBufferHeader);

    public UInt32 ReadStreamID
        (ReadOnlySpan<byte> pBufferHeader);

    public UInt32 ReadFragmentID
        (ReadOnlySpan<byte> pBufferHeader);

    public UInt24 ReadPayloadLenght
       (ReadOnlySpan<byte> pBufferHeader);
}
