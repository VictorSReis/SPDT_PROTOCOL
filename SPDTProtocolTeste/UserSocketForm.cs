using SPDTCore.Core.Protocol;
using SPDTCore.Core.Readers;
using SPDTCore.Core.SPDT;
using SPDTSdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

    public struct TextColored
    {
        public string text;
        public Color color;
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
        Lst_ResponseServer.DrawMode = DrawMode.OwnerDrawFixed;
        Lst_ResponseServer.DrawItem += Lst_ResponseServer_DrawItem;
        CreateProtocol();
    }

    private void Lst_ResponseServer_DrawItem(object sender, DrawItemEventArgs e)
    {
        var item = (TextColored)Lst_ResponseServer.Items[e.Index];

        e.Graphics.DrawString(
            item.text,
            e.Font,
            new SolidBrush(item.color),
            e.Bounds);
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
        if (Result)
        {
            Protocol.ProcessInput(BufferReceiver);
        }
    }

    private void Btn_CreateStream1_Click(object sender, EventArgs e)
    {
        StreamID1 = Protocol.RequestStreamCreation();
        Protocol.RegisterStreamIDRequested(StreamID1);
    }

    private void Btn_CreateStream2_Click(object sender, EventArgs e)
    {
        StreamID2 = Protocol.RequestStreamCreation();
        Protocol.RegisterStreamIDRequested(StreamID2);
    }

    private void Btn_SendStreamData1_Click(object sender, EventArgs e)
    {
        Memory<byte> BuffHello = UTF8Encoding.UTF8.GetBytes("Olá Servidor! Stm 1");
        var NewData = MessageCreator.CreateMessageData(StreamID1, BuffHello);
        SocClient.Send(NewData);
    }

    private void Btn_SendStreamData2_Click(object sender, EventArgs e)
    {
        Memory<byte> BuffHello = UTF8Encoding.UTF8.GetBytes("Olá Servidor! Stm 2");
        var NewData = MessageCreator.CreateMessageData(StreamID2, BuffHello);
        SocClient.Send(NewData);
    }

    private void Btn_SendMalformedPkt_Click(object sender, EventArgs e)
    {
        var Msg = MessageCreator.CreateMessage(SPDTProtocolVersion.ProtocolVersion10, SPDTPacketType.PACKET_TP_DATA, StreamID1, 0);
        Msg.Span[2] = 185;
        SocClient.Send(Msg);
    }


    #region PRIVATE
    private void CreateProtocol()
    {
        Protocol.CreateResources();
        Protocol.Initialize();
        Protocol.OnNewOpenStream += Protocol_OnNewOpenStream;
        Protocol.OnNewGlobalMessage += Protocol_OnNewGlobalMessage;

        MessageCreator = Protocol.GetMessageCreator();
        ReaderStream = Protocol.GetReaderStream();
        ForwardEndpoint = Protocol.GetForwardEndpoint();
        ForwardEndpoint.OnNewMessageForward += ForwardEndpoint_OnNewMessageForward;

    }

    private void Protocol_OnNewGlobalMessage(object sender, ISPDTMessage e)
    {
        UInt32 StreamId = e.Header.StreamID;
        SPDTError ErrorOcurred = (SPDTError)BitReader.ReadUInt16BigEndian(e.Frame.FramePayload.Span, 0);
        string msg = $"{StreamId}: Ocorreu uma falha > {ErrorOcurred}";


        InvokeRequeredControl(Lst_ResponseServer, () =>
        {
            TextColored color = new()
            {
                text = msg,
                color = Color.DarkRed
            };
            Lst_ResponseServer.Items.Add(color);
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
            e.SetNameStream("NOTIFICATIONS_STM");
            Stream1 = e;
            Stream1.OnNewMessageAvaileble += SpdtStream_OnNewMessageAvaileble;
            UpdateLabelStream(true);
        }
        else
        {
            e.SetNameStream("FOLDER_MANAGER");
            Stream2 = e;
            Stream2.OnNewMessageAvaileble += SpdtStream_OnNewMessageAvaileble;
            UpdateLabelStream(false);
        }
    }

    private void SpdtStream_OnNewMessageAvaileble
        (object sender, EventArgs e)
    {
        ISPDTStream Stm = sender as ISPDTStream;
        Debug.WriteLine($"DEBUG: Stream '{Stm.StreamName}' availeble message!");
        ProcessMessageStream(Stm.GetSPDTMessage());
    }

    private void ProcessMessageStream(ISPDTMessage pNewMessage)
    {
        if (pNewMessage.Header.PacketType == SPDTPacketType.PACKET_TP_DATA)
        {
            UInt32 StreamId = pNewMessage.Header.StreamID;
            string Msg = $"{StreamId}: {Encoding.UTF8.GetString(pNewMessage.Frame.FramePayload.Span)}";

            InvokeRequeredControl(Lst_ResponseServer, () =>
            {
                TextColored color = new()
                {
                    text = Msg,
                    color = Color.Black
                };
                Lst_ResponseServer.Items.Add(color);
            });
        }
        else
        {
            throw new NotImplementedException();
        }
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
