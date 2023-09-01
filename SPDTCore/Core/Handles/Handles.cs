namespace SPDTCore.Core.Handles;

public readonly struct SPDTCoreHandles
{
    public delegate void NotifyUserForwardMessageHandle(Memory<byte> pBufferMessage);
}
