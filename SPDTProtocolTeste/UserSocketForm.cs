using SPDTCore.Core.Protocol;
using SPDTCore.Core.Readers;
using SPDTCore.Core.SPDT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPDTProtocolTeste;

public partial class UserSocketForm : Form
{
    public UserSocketForm()
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
        CheckAvailebleMessageInStream();
        MessageBox.Show("Conexão estabelecida");
    }

    private void SocClient_OnNewAvailebleData(object sender, EventArgs e)
    {
        bool Result = SocClient.Receiver(out var BufferReceiver);
        if (Result)
        {
            Protocol.ProcessInput(BufferReceiver);
        }
    }

    private void Btn_CreateStream1_Click(object sender, EventArgs e)
    {
        var data = MessageCreator.CreateMessage_RequestNewStreamEndpoint(out StreamID1);
        SocClient.Send(data);
    }

    private void Btn_CreateStream2_Click(object sender, EventArgs e)
    {
        var data = MessageCreator.CreateMessage_RequestNewStreamEndpoint(out StreamID2);
        SocClient.Send(data);
    }

    private void Btn_SendStreamData1_Click(object sender, EventArgs e)
    {
        Memory<byte> BuffHello = UTF8Encoding.UTF8.GetBytes("Olá Servidor! Stm 1");
        var NewData = MessageCreator.CreateMessage_Data(StreamID1, BuffHello);
        SocClient.Send(NewData);    
    }

    private void Btn_SendStreamData2_Click(object sender, EventArgs e)
    {
        Memory<byte> BuffHello = UTF8Encoding.UTF8.GetBytes("Olá Servidor! Stm 2");
        var NewData = MessageCreator.CreateMessage_Data(StreamID2, BuffHello);
        SocClient.Send(NewData);
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

    private void CheckAvailebleMessageInStream()
    {
        Task.Factory.StartNew(() =>
        {
            bool StmContainMessage = false;
            while (true)
            {
                if(Stream1 is not null)
                {
                    StmContainMessage = Stream1.AvailebleData();
                    if (StmContainMessage)
                        ProcessMessageStream(Stream1.GetSPDTMessage());
                }

                if (Stream2 is not null)
                {
                    StmContainMessage = Stream2.AvailebleData();
                    if (StmContainMessage)
                        ProcessMessageStream(Stream2.GetSPDTMessage());
                }

                Thread.Sleep(10);
            }
        });
    }

    private void ForwardEndpoint_OnNewMessageForward(object sender, Memory<byte> e)
    {
        SocClient.Send(e);
    }

    private void Protocol_OnNewOpenStream(object sender, ISPDTStream e)
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

    private void ProcessMessageStream(ISPDTMessage pNewMessage)
    {
        UInt32 StreamId = pNewMessage.Header.StreamID;
        string Msg = $"{StreamId}: {Encoding.UTF8.GetString(pNewMessage.Frame.FramePayload.Span)}";

        InvokeRequeredControl(Lst_ResponseServer, () => 
        {
            Lst_ResponseServer.Items.Add(Msg);
        });
    }

    private void UpdateLabelStream(bool Stm1)
    {
        if (Stm1)
        {
            InvokeRequeredControl
                (LabelStream1, new Action(() => { LabelStream1.ForeColor = Color.Green; }));
        }
        else
        {
            InvokeRequeredControl
                (LabelStream2, new Action(() => { LabelStream2.ForeColor = Color.Green; }));
        }
    }

    private void InvokeRequeredControl(Control pControl, Action pAction)
    {
        if (pControl.InvokeRequired)
        {
            pControl.Invoke(new MethodInvoker(() => { pAction.Invoke(); }));
        }
        else
        {
            pAction.Invoke();
        }
    }
    #endregion

}
