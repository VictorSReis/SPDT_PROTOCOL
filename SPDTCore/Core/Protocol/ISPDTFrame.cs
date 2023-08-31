using SPDTSdk;

namespace SPDTCore.Core.Protocol;

public interface ISPDTFrame
{
    public SPDTFrameType FrameType { get; }

    public UInt24 FrameLenght { get; }

    public Memory<byte> FramePayload { get; }


    public void LoadFrameProtocol(Memory<byte> pBufferFrame);

    public void ResetFrame();
}
