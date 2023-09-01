using SPDTCore.Core;
using SPDTCore.Core.Protocol;
using SPDTCore.Core.SPDT;
using SPDTSdk;

namespace SPDTImpl.SPDT;

public sealed class SPDTCoreStreams : ISPDTCoreStreams
{
    #region PROPERTIES
    public ushort MaximumStreams { get; private set; }
    #endregion

    #region COLLECTIONS STREAMS
    private List<ISPDTCoreStreamItem> _CollectionStreams;
    #endregion

    #region PRIVATE VALUES
    private ISPDTGlobalObjects _SPDTGlobalObjets;
    #endregion


    #region CONSTRUCTOR
    internal SPDTCoreStreams
        (ISPDTGlobalObjects pSpdtGlobalObjects)
    {
        _SPDTGlobalObjets = pSpdtGlobalObjects;
    }
    #endregion

    #region ISPDTCoreStreams
    public void CreateSPDTCoreStream
        (ushort pMaximumStreams)
    {
        _CollectionStreams = new List<ISPDTCoreStreamItem>(pMaximumStreams);
        MaximumStreams = pMaximumStreams;
    }

    public ISPDTCoreStreamItem CreateStream
        (UInt32 pStreamID, SPDTStreamState pStreamState)
    {
        var NewItem = new SPDTCoreStreamItem();
        Guid GuidForStream = Guid.NewGuid();

        //CRIA OS OBJETOS PARA O STREAM.
        var StreamMsgs = _SPDTGlobalObjets.SpdtFactory.CreateStreamMessages();
        StreamMsgs.CreateStreamMessages(GuidForStream);
        var StreamManager = _SPDTGlobalObjets.SpdtFactory.CreateStreamManager();
        var NewStream = _SPDTGlobalObjets.SpdtFactory.CreateSPDTStream(StreamManager, StreamMsgs);

        //CONFIGURA O STREAM.
        StreamManager.InvokeSetIDStream(pStreamID);
        StreamManager.InvokeUpdateStreamState(SPDTStreamState.STREAM_CREATED);
        StreamManager.InvokeSetGuidStream(GuidForStream);

        //DEFINE OS OBJETOS NO ITEM
        NewItem.CreateItem(pStreamID, NewStream, StreamManager, StreamMsgs);

        return NewItem;
    }

    public ushort GetCountStreams()
    {
        return _CollectionStreams is null ? (ushort)0 : (ushort)_CollectionStreams.Count;
    }

    public ISPDTStream GetStreamID(UInt32 pStreamID)
    {
        PrivateGetStreamByID(pStreamID, out var OutStream);
        return OutStream;
    }

    public void RegisterStream
        (ISPDTCoreStreamItem pStreamItem)
    {
        PrivateRegisterNewStreamItem(pStreamItem);
    }
    #endregion

    #region PRIVATE
    private void PrivateRegisterNewStreamItem
        (ISPDTCoreStreamItem pStreamItem)
    {
        _CollectionStreams.Add(pStreamItem);
    }

    private bool PrivateGetStreamByID
        (UInt32 pStreamID, out ISPDTStream pOutStream)
    {
        pOutStream = default;

        var StreamItemGet = _CollectionStreams.Find(x=> x.StreamID == pStreamID);
        if (StreamItemGet is null)
            return false;

        pOutStream = StreamItemGet.SpdtStream;

        return true;
    }
    #endregion
}
