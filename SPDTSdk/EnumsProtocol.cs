namespace SPDTSdk;

/// <summary>
/// Enumera as possiveis versões do protocolo.
/// </summary>
public enum SPDTProtocolVersion : UInt16
{
    /// <summary>
    /// SDPT Versão 1.0
    /// </summary>
    ProtocolVersion10 = 0x0001,

    /// <summary>
    /// Versão desconhecida
    /// </summary>
    Unknown = UInt16.MaxValue
}

/// <summary>
/// Enumera o tipo do pacote enviado/recebido.
/// </summary>
public enum SPDTPacketType : byte
{
    /// <summary>
    /// Pacote de dados.
    /// </summary>
    PACKET_TP_DATA = 0x0,

    /// <summary>
    /// Pacote informando a criação de um fluxo.
    /// </summary>
    PACKET_TP_CREATE_STREAM,

    /// <summary>
    /// Pacote solicitando o fechamento do fluxo.
    /// </summary>
    PACKET_TP_CLOSE_STREAM,

    /// <summary>
    /// Pacote solicitando que o fluxo seja resetado.
    /// </summary>
    PACKET_TP_RESET_STREAM,

    /// <summary>
    /// Pacote utilizado para manter vivo um fluxo.
    /// </summary>
    PACKET_TP_PING_STREAM,

    /// <summary>
    /// Pacote utilizado para informar o endpoint sobre erros. 
    /// Este tipo deve ser enviado com dados de payload.
    /// </summary>
    PACKET_TP_ERROR,

    /// <summary>
    /// Pacote de ping para avaliar a conexão com o endpoint final.
    /// </summary>
    PACKET_TP_PING = 0xFE,

    /// <summary>
    /// Pacote desconhecido.
    /// </summary>
    Unknown = byte.MaxValue
}

/// <summary>
/// Enumera os tipos de frame possiveis.
/// </summary>
public enum SPDTFrameType: byte
{
    /// <summary>
    /// Define que a mensagem é de frame único, todos os dados já estão completos.
    /// </summary>
    FRAME_TP_SINGLE,

    /// <summary>
    /// Informa que é multiplos frames, os dados do pacote ainda não estão completos.
    /// </summary>
    FRAME_TP_MULTIPLES,

    /// <summary>
    /// Informa que o frame atual é o ultimo.
    /// </summary>
    FRAME_TP_MULTIPLES_END,

    /// <summary>
    /// O tipo do frame é desconhecido.
    /// </summary>
    Unknown = byte.MaxValue
}

/// <summary>
/// Enumera os possiveis erros durante a transmissão e recebimento de dados.
/// </summary>
public enum SPDTError : UInt16
{
    /// <summary>
    /// O pacote informado tem uma estrutura inválida.
    /// </summary>
    MALFORMED_PACKET,

    /// <summary>
    /// O fluxo informado no pacote não existe.
    /// </summary>
    STREAM_DOES_NOT_EXIST,





    /// <summary>
    /// Ocorreu um erro interno ao processar alguma solicitação.
    /// </summary>
    INTERNAL_ERROR = UInt16.MaxValue
}