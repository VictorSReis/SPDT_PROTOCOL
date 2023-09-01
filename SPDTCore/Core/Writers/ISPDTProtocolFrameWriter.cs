using SPDTSdk;

namespace SPDTCore.Core.Writers;

public interface ISPDTProtocolFrameWriter
{
    public void WriteFrameType
        (SPDTFrameType pSPDTFrameType, Memory<Byte> pBufferFrame);

    public void WriteFrameLenght
        (UInt24 pFrameLenght, Memory<Byte> pBufferFrame);

    public void WriteFramePayload
        (Memory<byte> pFramePayload, Memory<Byte> pBufferFrame);

    public void WriteFramePayload
        (Span<byte> pFramePayload, Memory<Byte> pBufferFrame);
}
