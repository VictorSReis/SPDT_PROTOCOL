using SPDTCore.Core.Protocol;
using SPDTCore.Core.Readers;
using System;

namespace SPDTCore.Core.SPDT;

/// <summary>
/// Interface que implementa o protocolo SPDT e todos os seus recursos necessários para execução.
/// </summary>
public interface ISPDTCore
{
    public event EventHandler<ISPDTStream> OnNewOpenStream;


    public void CreateResources();

    public void Initialize();

    public void ProcessInput(Memory<byte> pSpdtPacket);

    public ISPDTCoreMessageCreator GetMessageCreator();

    public ISPDTCoreForwardEndpoint GetForwardEndpoint();

    public ISPDTReaderStream GetReaderStream();
}
