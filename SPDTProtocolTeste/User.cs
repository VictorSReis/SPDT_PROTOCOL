using SPDTCore.Core.SPDT;
using System;

namespace SPDTProtocolTeste;

internal class User
{
    ISPDTCore Protocol = new SPDTImpl.SPDT.SPDTCore();
    ISPDTCoreForwardEndpoint ForwardEndpoint;
    ISPDTCoreMessageCreator MessageCreator;
    User UserDestination = null;
    UInt32 StreamPending = 0;

    public void CreateProtocol(User pUserDestination)
    {
        UserDestination = pUserDestination;
        Protocol.CreateResources();
        Protocol.Initialize();

        MessageCreator = Protocol.GetMessageCreator();
        ForwardEndpoint = Protocol.GetForwardEndpoint();
        ForwardEndpoint.OnNewMessageForward += ForwardEndpoint_OnNewMessageForward;
    }

    public Memory<byte> CreateStreamMessageTesteStream()
    {
        var Data = MessageCreator.CreateMessageRequestNewStreamEndpoint(out UInt32 StreamPending);
        return Data;
    }

    public void SimulateSendMessage(Memory<byte> pBuffer)
    {
        Protocol.ProcessInput(pBuffer);
    }

    private void ForwardEndpoint_OnNewMessageForward(object sender, Memory<byte> e)
    {
        UserDestination.SimulateSendMessage(e);
    }
}
