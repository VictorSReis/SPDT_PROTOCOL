using SPDTCore.Core.Protocol;
using SPDTCore.Core.SPDT;
using SPDTCore.Core.Stream;

namespace SPDTImpl.SPDT;

internal sealed class SPDTCoreStreamItem : ISPDTCoreStreamItem
{
    #region PROPERTIES
    public UInt32 StreamID { get; private set; }

    public ISPDTStream SpdtStream { get; private set; }

    public ISPDTStreamManager SpdtStreamManager { get; private set; }

    public ISPDTStreamMessages SpdtStreamMessages { get; private set; }
    #endregion

    #region ISPDTCoreStreamItem
    public void CreateItem(
        UInt32 pStreamID,
        ISPDTStream pSpdtStream,
        ISPDTStreamManager pSpdtStmManager,
        ISPDTStreamMessages pSpdtStmMessages)
    {
        StreamID = pStreamID;
        SpdtStream = pSpdtStream;
        SpdtStreamManager = pSpdtStmManager;
        SpdtStreamMessages = pSpdtStmMessages;
    }

    public void Reset()
    {
        StreamID = 0;
        if (SpdtStream is not null)
            SpdtStream = null;
        if (SpdtStreamManager is not null)
            SpdtStreamManager = null;
        if (SpdtStreamMessages is not null)
            SpdtStreamMessages = null;

    }
    #endregion
}
