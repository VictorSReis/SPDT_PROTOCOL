namespace SPDTSdk;

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

public enum SPDTPacketType : byte
{
    /// <summary>
    /// Pacote de dados.
    /// </summary>
    PACKET_TP_DATA = 0x0,

    /// <summary>
    /// Pacote informando a criação de um fluxo.
    /// </summary>
    PACKET_TP_CREATE_STREAM = 0x1,

    /// <summary>
    /// Pacote solicitando o fechamento do fluxo.
    /// </summary>
    PACKET_TP_CLOSE_STREAM = 0x2,

    /// <summary>
    /// Pacote solicitando que o fluxo seja resetado.
    /// </summary>
    PACKET_TP_RESET_STREAM = 0x3,

    /// <summary>
    /// Pacote utilizado para manter vivo um fluxo.
    /// </summary>
    PACKET_TP_PING_STREAM = 0x4,


    /// <summary>
    /// Pacote de ping para avaliar a conexão com o endpoint final.
    /// </summary>
    PACKET_TP_PING = 0xFE,

    /// <summary>
    /// Pacote desconhecido.
    /// </summary>
    Unknown = byte.MaxValue
}

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
