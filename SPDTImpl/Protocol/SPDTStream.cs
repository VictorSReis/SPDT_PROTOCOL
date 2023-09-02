using SPDTCore.Core.Protocol;
using SPDTCore.Core.Stream;
using SPDTSdk;

namespace SPDTImpl.Protocol;

public sealed class SPDTStream : ISPDTStream
{
    #region PROPERTIES
    public uint StreamID { get; private set; }

    public Guid StreamGuid { get; private set; }

    public SPDTStreamState StreamState { get; private set; }

    public ISPDTStreamStatistics StreamStatistics { get; private set; }
    #endregion

    #region STREAM MANAGER
    private ISPDTStreamManager _StreamManager;
    #endregion

    #region PRIVATE OBJECTS STREAM
    private ISPDTStreamMessages _StreamMessages;
    #endregion


    #region CONSTRUCTOR
    internal SPDTStream
        (ISPDTStreamManager pStreamManager, ISPDTStreamMessages pStreamMessages)
    {
        _StreamManager = pStreamManager;
        _StreamMessages = pStreamMessages;

        //LOAD EVENT STREAM MANAGER
        _StreamManager.OnSetIDStream += StreamManager_OnSetIDStream;
        _StreamManager.OnSetGuidStream += StreamManager_OnSetGuidStream;
        _StreamManager.OnUpdateStreamState += StreamManager_OnUpdateStreamState;
        _StreamManager.OnNewMessage += StreamManager_OnNewMessage;
        _StreamManager.OnResetStream += _StreamManager_OnResetStream;
    }
    #endregion

    #region ISPDTStream
    public bool AvailebleData()
    {
        return _StreamMessages.MessageAvaileble();
    }

    public ISPDTMessage GetSPDTMessage()
    {
        return _StreamMessages.GetNextMessage();
    }
    #endregion

    #region ISPDTStreamManager
    private void StreamManager_OnUpdateStreamState
        (object sender, SPDTStreamState e)
    {
        StreamState = e;
    }

    private void StreamManager_OnSetGuidStream
        (object sender, Guid e)
    {
        StreamGuid = e;
    }

    private void StreamManager_OnSetIDStream
        (object sender, uint e)
    {
        StreamID = e;
    }

    private void StreamManager_OnNewMessage
        (object sender, ISPDTMessage e)
    {
        _StreamMessages.EnqueueMessage(e);
    }

    private void _StreamManager_OnResetStream(object sender, EventArgs e)
    {
        StreamID = 0;
        StreamGuid = Guid.Empty;
        StreamState = SPDTStreamState.STREAM_RESET;
    }
    #endregion
}
