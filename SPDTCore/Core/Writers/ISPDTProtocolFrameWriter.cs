using SPDTSdk;

namespace SPDTCore.Core.Writers;

public interface ISPDTProtocolFrameWriter
{
    public void WriterFrameType
        (SPDTFrameType pSPDTFrameType, Memory<Byte> pBufferFrame);

    public void WriterFrameLenght
        (UInt24 pFrameLenght, Memory<Byte> pBufferFrame);

    public void WriterFramePayload
        (Memory<byte> pFramePayload, Memory<Byte> pBufferFrame);

    public void WriterFramePayload
        (Span<byte> pFramePayload, Memory<Byte> pBufferFrame);
}
