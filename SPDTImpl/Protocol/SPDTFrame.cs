using SPDTCore.Core;
using SPDTCore.Core.Protocol;
using SPDTSdk;

namespace SPDTImpl.Protocol;

/// <summary>
/// Implementação default da interface <see cref="ISPDTFrame"/>.
/// </summary>
public sealed class SPDTFrame : ISPDTFrame
{
    #region PROPERTIES
    public SPDTFrameType FrameType { get; private set; }

    public UInt24 FrameLenght { get; private set; }

    public ReadOnlyMemory<byte> FramePayload { get; private set; }
    #endregion

    #region FRAME MEMORY
    private Memory<byte> _FrameMemoryData;
    #endregion

    #region GLOBAL OBJECTS
    private ISPDTGlobalObjects _SPDTGlobalObjects;
    #endregion


    #region CONSTRUCTOR
    internal SPDTFrame
        (ISPDTGlobalObjects pGlobalObjects)
    {
        _SPDTGlobalObjects = pGlobalObjects;
    }
    #endregion

    #region ISPDTFrame
    public void LoadFrameProtocol
        (Memory<byte> pBufferFrame)
    {
        //SET FRAME.
        _FrameMemoryData = pBufferFrame;

        //LOAD PROPERTIES.
        FrameType = _SPDTGlobalObjects.SpdtProtocol.
            ReadFrameType(_FrameMemoryData.Span);
        FrameLenght = _SPDTGlobalObjects.SpdtProtocol.
            ReadFrameLenght(_FrameMemoryData.Span);
        FramePayload = _SPDTGlobalObjects.SpdtProtocol.
            ReadFramePayload(_FrameMemoryData);
    }

    public void ResetFrame()
    {
        //RESET DATA
        FrameType = SPDTFrameType.Unknown;
        FrameLenght = 0;
        FramePayload = null;
        _FrameMemoryData = null;
    }
    #endregion
}
