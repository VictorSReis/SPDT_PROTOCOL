using SPDTCore.Core.Protocol;
using SPDTCore.Core.Readers;
using SPDTCore.Core.SPDT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPDTProtocolTeste;

public partial class UserSocket : Form
{
    public UserSocket()
    {
        InitializeComponent();
    }


    ISPDTCore Protocol = new SPDTImpl.SPDT.SPDTCore();
    ISPDTCoreForwardEndpoint ForwardEndpoint;
    ISPDTCoreMessageCreator MessageCreator;
    ISPDTReaderStream ReaderStream;
    SocClient SocClient;
    UInt32 StreamID1;
    UInt32 StreamID2;
    ISPDTStream Stream1, Stream2;
    private void UserSocket_Load(object sender, EventArgs e)
    {
        CreateProtocol();
    }

    private void Btn_Connect_Click(object sender, EventArgs e)
    {
        SocClient = new SocClient(ReaderStream);
        SocClient.OnNewAvailebleData += SocClient_OnNewAvailebleData;
        SocClient.CreateConnection("127.0.0.1", 50564);
        MessageBox.Show("Conexão estabelecida");
    }

    private void SocClient_OnNewAvailebleData(object sender, EventArgs e)
    {
        bool Result = SocClient.Receiver(out var BufferReceiver);
        if(Result)
        {
            Protocol.ProcessInput(BufferReceiver);
        }
    }

    private void Btn_CreateStream1_Click(object sender, EventArgs e)
    {
        var data = MessageCreator.CreateMessage_CreateNewStreamEndpoint(out StreamID1);
        SocClient.Send(data);
    }

    private void Btn_CreateStream2_Click(object sender, EventArgs e)
    {
        var data = MessageCreator.CreateMessage_CreateNewStreamEndpoint(out StreamID2);
        SocClient.Send(data);
    }

    #region PRIVATE
    private void CreateProtocol()
    {
        Protocol.CreateResources();
        Protocol.Initialize();
        Protocol.OnNewOpenStream += Protocol_OnNewOpenStream;

        MessageCreator = Protocol.GetMessageCreator();
        ReaderStream = Protocol.GetReaderStream();
        ForwardEndpoint = Protocol.GetForwardEndpoint();
        ForwardEndpoint.OnNewMessageForward += ForwardEndpoint_OnNewMessageForward;
    }

    private void ForwardEndpoint_OnNewMessageForward(object sender, Memory<byte> e)
    {
        SocClient.Send(e);
    }

    private void Protocol_OnNewOpenStream(object sender, SPDTCore.Core.Protocol.ISPDTStream e)
    {
        if (e.StreamID == StreamID1)
        { 
            Stream1 = e;
            UpdateLabelStream(true);
        }
        else
        { 
            Stream2 = e;
            UpdateLabelStream(false);
        }
    }

    private void UpdateLabelStream(bool Stm1)
    {
        if(Stm1)
        {
            InvokeRequeredInLabel
                (LabelStream1, new Action(() => { LabelStream1.BackColor = Color.Green; }));
        }
        else
        {
            InvokeRequeredInLabel
                (LabelStream2, new Action(() => { LabelStream1.BackColor = Color.Gold; }));
        }
    }

    private void InvokeRequeredInLabel(Control pControl, Action pAction)
    {
        if(pControl.InvokeRequired)
        {
            pControl.Invoke(pAction);
        }
        else
        {
            pAction.Invoke();
        }
    }
    #endregion
}
