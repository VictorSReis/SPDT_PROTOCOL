using SPDTCore.Core.Protocol;
using SPDTCore.Core.SPDT;
using SPDTCore.Core.Stream;

namespace SPDTImpl.SPDT;

internal sealed class SPDTCoreForwarderMessage : ISPDTCoreForwarderMessage
{
    #region PRIVATE
    private ISPDTCoreStreams _CoreStreams;
    #endregion

    #region CONSTRUCTOR
    internal SPDTCoreForwarderMessage
        (ISPDTCoreStreams pCoreStreams)
    {
        _CoreStreams = pCoreStreams;
    }
    #endregion


    #region ISPDTCoreForwarderMessage
    public bool Forwarder(ISPDTMessage pMessage)
    {
        bool Result = false;

        var StreamManager = PrivateGetStreamManagerById(pMessage.Header.StreamID);
        if (StreamManager is null)
            goto Done;

        StreamManager.InvokeNewMessage(pMessage);
        Result = true;

        Done:;
        return Result;
    }
    #endregion

    #region PRIVATE
    private ISPDTStreamManager PrivateGetStreamManagerById(UInt32 pStreamID)
    {
        return _CoreStreams.GetStreamManagerByID(pStreamID);
    }
    #endregion
}
