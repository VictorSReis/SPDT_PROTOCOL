using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SPDTProtocolTeste;

internal class SocServer
{
    private TcpListener Servidor;
    private Task _TaskMonitorClients;
    internal List<TcpClient> ListaClientes;

    public event EventHandler<EventArgs> ClientConnected;
    public event EventHandler<Socket> OnNewData;
    private readonly object SyncList = new ();

    public void CreateServidor()
    {
        IPEndPoint NewIpEndpoint = new (IPAddress.Parse("127.0.0.1"), 50564);
        Servidor = new TcpListener(NewIpEndpoint);
        Servidor.Start(10);
        ListaClientes = new List<TcpClient>(10);

        _TaskMonitorClients = new Task(MonitoringClients);
        _TaskMonitorClients.Start();
    }

    public IEnumerable<TcpClient> ForEarchList()
    {
        if (ListaClientes.Count == 0)
            yield break;

        lock (SyncList)
        {
            foreach (var item in ListaClientes)
            {
                yield return item;
            }
        }
    }

    public void SendToClient(Memory<byte> pBuffer)
    {
        lock (SyncList)
        {
            ListaClientes[0].Client.Send(pBuffer.Span);
        }
    }

    private void MonitoringClients()
    {
        while (true)
        {
            if(Servidor.Pending())
            {
                var NewClient = Servidor.AcceptTcpClient();
                AddCliente(NewClient);
            }

            foreach (var item in ForEarchList())
            {
                if(item.Available > 0)
                {
                    OnNewData?.Invoke(this, item.Client);
                }
            }
        }
    }

    private void AddCliente(TcpClient pnewclient)
    {
        lock (SyncList)
        {
            ListaClientes.Add(pnewclient);
        }
        ClientConnected?.Invoke(this, null);
    }

}
