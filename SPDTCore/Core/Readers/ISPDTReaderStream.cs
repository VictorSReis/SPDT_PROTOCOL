using System.Net.Sockets;

namespace SPDTCore.Core.Readers;

/// <summary>
/// Interface responsável por ler um pacote de bytes completo do protocolo a parti de um fluxo 
/// de bytes ou aparti do socket.
/// </summary>
public interface ISPDTReaderStream
{
    public bool Read(System.IO.Stream pStreamData, out Memory<byte> pOutBufferSDPTPaket);

    public bool ReadFromSocket(Socket pSocket, out Memory<byte> pOutBufferSPDTPacket);
}
