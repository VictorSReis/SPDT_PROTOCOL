using System;

namespace SPDTCore.Core.Protocol;

public interface ISPDTMessage
{
    public ISPDTHeader Header { get; }

    public ISPDTFrame Frame { get; }


    public void CreateMessage();

    public void AssembleMessage
        (Memory<byte> pBufferMessage, bool pFramePayloadPresent);

    public void ResetMessage();

    public void ReleaseMessageMemory();
}
