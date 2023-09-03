using SPDTCore.Core.Protocol;
using SPDTSdk;

namespace SPDTCore.Core.SPDT;

/// <summary>
/// Interface responsável por criar mensagens, em bytes, para serem transmitidas ao endpoint.
/// </summary>
public interface ISPDTCoreMessageCreator
{
    /// <summary>
    /// Mensagem utilizada para solicitar do endpoint que ele crie um novo fluxo para envio de dados.
    /// </summary>
    /// <param name="pOutStreamIDRequestCreate">Retorna o ID do fluxo a ser criado.</param>
    /// <returns></returns>
    public Memory<byte> CreateMessageRequestNewStreamEndpoint
        (out UInt32 pOutStreamIDRequestCreate);

    /// <summary>
    /// Mensagem de dados comum do protocolo. É essa mensagem que trafega os dados do aplicativo.
    /// </summary>
    /// <param name="pStreamID">O ID do fluxo que a mensagem vai ser enviada.</param>
    /// <param name="PayloadFrameData">Os dados da mensagem.</param>
    /// <returns></returns>
    public Memory<byte> CreateMessageData
        (UInt32 pStreamID, Memory<byte> PayloadFrameData);

    /// <summary>
    /// Mensagem utilizada para notificar o endpoint sobre erros.
    /// </summary>
    /// <param name="pErrorType">O tipo do error a ser notificado.</param>
    /// <param name="pStreamID">O ID do stream que gerou erro ou 0 para informar que foi ao nível da conexão.</param>
    /// <returns></returns>
    public Memory<byte> CreateMessageError
        (SPDTError pErrorType, UInt32 pStreamID);

    /// <summary>
    /// Cria uma mensagem de acordo com os parametros informado.
    /// </summary>
    /// <param name="pProtocolVersion">A versão do protocolo utilizado.</param>
    /// <param name="pPacketType">O tipo da mensagem.</param>
    /// <param name="pStreamId">Um Id do stream a ser trafegado essa mensagem, se necessário.</param>
    /// <param name="pFramentId">Um Id de identificação do fragmento de dados atual.</param>
    /// <param name="pFrameType">O tipo do frame atual.</param>
    /// <param name="pFramePayloadLenght">O tamanho, em bytes, do payload do frame.</param>
    /// <param name="pFramePayload">Um buffer com os dados do payload a ser vinculado.</param>
    /// <returns></returns>
    public Memory<byte> CreateMessage(
        SPDTProtocolVersion pProtocolVersion,
        SPDTPacketType pPacketType,
        UInt32 pStreamId,
        UInt32 pFramentId,
        SPDTFrameType pFrameType,
        UInt32 pFramePayloadLenght,
        Memory<byte> pFramePayload);

    /// <summary>
    /// Cria uma mensagem com os parametros dados mais sem o frame de dados (<see cref="ISPDTFrame"/>).
    /// </summary>
    /// <param name="pProtocolVersion">A versão do protocolo utilizado.</param>
    /// <param name="pPacketType">O tipo da mensagem.</param>
    /// <param name="pStreamId">Um Id do stream a ser trafegado essa mensagem, se necessário.</param>
    /// <param name="pFramentId">Um Id de identificação do fragmento de dados atual.</param>
    /// <returns></returns>
    public Memory<byte> CreateMessage(
        SPDTProtocolVersion pProtocolVersion,
        SPDTPacketType pPacketType,
        uint pStreamId,
        uint pFramentId);
}
