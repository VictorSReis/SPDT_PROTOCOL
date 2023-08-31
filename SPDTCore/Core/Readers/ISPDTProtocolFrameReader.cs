using SPDTSdk;

namespace SPDTCore.Core.Readers;

public interface ISPDTProtocolFrameReader
{
    public SPDTFrameType ReadFrameType
        (ReadOnlySpan<byte> pFrameBuffer);

    public UInt24 ReadFrameLenght
        (ReadOnlySpan<byte> pFrameBuffer);

    public ReadOnlyMemory<byte> ReadFramePayload
        (ReadOnlyMemory<byte> pFrameBuffer);
}
