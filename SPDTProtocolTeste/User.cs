using Microsoft.Win32;
using SPDTCore.Core.SPDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPDTProtocolTeste
{
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
            var Data = MessageCreator.CreateMessage_CreateNewStreamEndpoint(out UInt32 StreamPending);
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
}
