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
    public event EventHandler<ISPDTMessage> OnNewGlobalMessage;

    public void CreateResources();

    public void Initialize();

    public void ProcessInput(Memory<byte> pSpdtPacket);

    /// <summary>
    /// Método responsável por requisitar a criação de um novo fluxo para o endpoint.
    /// O chamado deve registrar o ID retornado por esse método.
    /// </summary>
    /// <returns>Retorna o StreamID para o fluxo que será aberto.</returns>
    public UInt32 RequestStreamCreation();

    public void RegisterStreamIDRequested(UInt32 pStreamID);

    public ISPDTCoreMessageCreator GetMessageCreator();

    public ISPDTCoreForwardEndpoint GetForwardEndpoint();

    public ISPDTReaderStream GetReaderStream();
}
