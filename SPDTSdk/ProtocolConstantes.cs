using System;

namespace SPDTSdk;

public readonly struct SPDTConstants
{
    /// <summary>
    /// DEFINE O TAMANHO, EM BYTES, DO OBJETO (HEADER).
    /// </summary>
    public const int SIZE_HEADER_OBJECT = 14;

    /// <summary>
    /// DEFINE O TAMANHO, EM BYTES, DO HEADER PERTENCENTE AO OBJETO (FRAME).
    /// </summary>
    public const int SIZE_HEADER_FRAME_OBJECT = 4;
}
