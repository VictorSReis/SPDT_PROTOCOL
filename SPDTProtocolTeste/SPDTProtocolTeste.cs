using SPDTCore.Core.Protocol;
using SPDTCore.Core.Readers;
using SPDTCore.Core.SPDT;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace SPDTProtocolTeste;

public partial class SPDTProtocolTesteForm : Form
{
    public SPDTProtocolTesteForm()
    {
        InitializeComponent();
    }

    ISPDTCore Protocol = new SPDTImpl.SPDT.SPDTCore();
    ISPDTCoreForwardEndpoint ForwardEndpoint;
    ISPDTCoreMessageCreator MessageCreator;
    ISPDTReaderStream ReaderStream;
    SocServer Servidor = new();
    List<ISPDTStream> StreamAbertos = new List<ISPDTStream>(2);
    private void SPDTProtocolTeste_Load(object sender, EventArgs e)
    {
        CreateProtocol();
    }

    private void Btn_CreateServer_Click(object sender, EventArgs e)
    {
        Servidor.CreateServidor();
        Servidor.ClientConnected += Servidor_ClientConnected;
        Servidor.OnNewData += Servidor_OnNewData;
        MessageBox.Show("Servidor criado e iniciado com sucesso!");
    }

    private void Servidor_OnNewData(object sender, Socket e)
    {
        bool Result = ReaderStream.ReadFromSocket(e, out var BufferReaded);
        if (Result)
            Protocol.ProcessInput(BufferReaded);
    }

    private void Servidor_ClientConnected(object sender, EventArgs e)
    {
        Debug.WriteLine("DEBUG SERVER: New client connected!");
    }

    private void Btn_CreateClient_Click(object sender, EventArgs e)
    {
        UserSocketForm FrmSoc = new ();
        FrmSoc.Show();
    }

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

    private void Protocol_OnNewOpenStream(object sender, ISPDTStream e)
    {
        StreamAbertos.Add(e);
        e.OnNewMessageAvaileble += E_OnNewMessageAvaileble;
    }

    private void E_OnNewMessageAvaileble(object sender, EventArgs e)
    {
        var SpdtStream = sender as ISPDTStream;
        var Msg = SpdtStream.GetSPDTMessage();
        var StringMensagem = Encoding.UTF8.GetString(Msg.Frame.FramePayload.Span);
        Debug.WriteLine($"Spdt Stream {SpdtStream.StreamID}: {StringMensagem}");

        Memory<byte> bt = Encoding.UTF8.GetBytes($"Olá Fluxo '{SpdtStream.StreamID}'!!");
        var Pacote = MessageCreator.CreateMessage_Data(SpdtStream.StreamID, bt);

        Servidor.SendToClient(Pacote);
    }

    private void ForwardEndpoint_OnNewMessageForward(object sender, Memory<byte> e)
    {
        Servidor.SendToClient(e);
    }

}