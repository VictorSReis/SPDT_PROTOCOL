using System.Collections.Concurrent;
using SPDTCore.Core.Protocol;
using SPDTCore.Core.Stream;

namespace SPDTImpl.Stream;

/// <summary>
/// Implementação default da interface <see cref="ISPDTStreamMessages"/>.
/// </summary>
public sealed class SPDTStreamMessages : ISPDTStreamMessages
{
    #region PROPERTIES
    public Guid StreamGuid { get; private set; }
    #endregion

    #region PRIVATE VALUES
    private ConcurrentQueue<ISPDTMessage> _ConcurrentQueueSPDTMessage { get; set; }
    #endregion


    #region ISPDTStreamMessages
    public void CreateStreamMessages
        (Guid pStreamGuid)
    {
        //SET STREAM GUID.
        StreamGuid = pStreamGuid;

        //CREATE QUEUE RECEIVER MESSAGES
        _ConcurrentQueueSPDTMessage = new ConcurrentQueue<ISPDTMessage>();
    }

    public void EnqueueMessage
        (ISPDTMessage pNewMessage)
    {
        _ConcurrentQueueSPDTMessage.Enqueue(pNewMessage);
    }

    public ISPDTMessage GetNextMessage()
    {
        _ConcurrentQueueSPDTMessage.TryDequeue(out ISPDTMessage DequeueNextMessage);
        return DequeueNextMessage;
    }

    public bool MessageAvaileble()
    {
        return !_ConcurrentQueueSPDTMessage.IsEmpty;
    }
    #endregion
}
