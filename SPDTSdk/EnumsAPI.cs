using System;


namespace SPDTSdk;

/// <summary>
/// Informa o estado de um fluxo.
/// </summary>
public enum SPDTStreamState: byte
{
    /// <summary>
    /// O fluxo está aberto.
    /// </summary>
    STREAM_OPEN,

    /// <summary>
    /// O fluxo está fechado.
    /// </summary>
    STREAM_CLOSED,

    /// <summary>
    /// O fluxo está em estado de erro.
    /// </summary>
    STREAM_ERROR,

    /// <summary>
    /// O fluxo foi resetado.
    /// </summary>
    STREAM_RESET,

    /// <summary>
    /// O estado do fluxo é desconhecido.
    /// </summary>
    Unknown = byte.MaxValue
}

/// <summary>
/// Informa o tipo da endianness
/// </summary>
public enum Endianness : byte
{
    LittleEndian = 0,
    BigEndian
}
