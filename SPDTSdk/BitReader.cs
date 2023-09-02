using System;

namespace SPDTSdk;

public static class BitReader
{
    public static UInt16 ReadUInt16BigEndian(ReadOnlySpan<byte> pMemory, int StartIndex)
    {
        UInt16 Valor;

        ReadOnlySpan<byte> Temp = pMemory.Slice(StartIndex, 2);// 2 BYTES BIG-ENDIAN
        Valor = (UInt16)((Temp[0] << 8) | (Temp[1] & 0xFF));

        return Valor;
    }

    public static UInt24 ReadUInt24BigEndian(ReadOnlySpan<byte> pMemory, int StartIndex)
    {
        UInt24 Valor;

        ReadOnlySpan<byte> Temp = pMemory.Slice(StartIndex, 3);// 3 BYTES BIG-ENDIAN
        Valor = (UInt24)((Temp[0] << 16) | (Temp[1] << 8) | (Temp[2] & 0xFF));

        return Valor;
    }

    public static UInt32 ReadUInt32BigEndian(ReadOnlySpan<byte> pMemory, int StartIndex)
    {
        UInt32 Valor;

        ReadOnlySpan<byte> Temp = pMemory.Slice(StartIndex, 4);// 4 BYTES BIG-ENDIAN
        Valor = (UInt32)((Temp[0] << 24) | (Temp[1] << 16) | (Temp[2] << 8) | (Temp[3] & 0xFF));

        return Valor;
    }
}
