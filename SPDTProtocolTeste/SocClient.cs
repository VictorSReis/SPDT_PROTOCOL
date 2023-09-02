using SPDTCore.Core.Readers;
using System.Diagnostics;
using System.Net.Sockets;

namespace SPDTProtocolTeste;

internal class SocClient
{
    private TcpClient Client;
    private ISPDTReaderStream SpdtReadStream;
    private Task _TaskCheckAvailebleData;

    public event EventHandler OnNewAvailebleData;

    public SocClient
        (ISPDTReaderStream pSpdtReadStream)
    {
        SpdtReadStream = pSpdtReadStream;
    }

    public void CreateConnection(string ip, int port)
    {
        try
        {
            Client = new TcpClient(ip, port);
            _TaskCheckAvailebleData = new Task(TaskCheckAvailebleData);
            _TaskCheckAvailebleData.Start();
        }
        catch (Exception Er)
        {
            MessageBox.Show("Failed to connect server: " + Er.Message);
            goto Done;
        }

    Done:;
    }

    public void Send(Memory<byte> pBuffer)
    {
        if (Client is null)
            return;

        Client.Client.Send(pBuffer.Span);
    }

    public bool Receiver(out Memory<byte> pBuffer)
    {
        return SpdtReadStream.ReadFromSocket(Client.Client, out pBuffer);
    }

    private void TaskCheckAvailebleData()
    {
        try
        {
            while (Client is not null)
            {
                if (Client.Available > 0)
                    OnNewAvailebleData?.Invoke(this, null);

                Thread.Sleep(10);
            }
        }
        catch (Exception Er)
        {
            Debug.WriteLine($"DEBUG CLIENT SOCKET: Falha na task de recebimento de dados: {Er.Message}");
        }
    }
}
