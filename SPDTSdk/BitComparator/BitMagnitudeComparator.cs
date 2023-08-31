namespace SPDTSdk;

internal static class BitMagnitudeComparator
{
    private static readonly int[] _PesoPerIndexBit = new int[24]
    {
        //Multi * 2
       8,
        16,
        32,
        64,
        128,
        256,
        512,
        1024,
        2048,
        4096,
        8192,
        16384,
        32768,
        65536,
        131072,
        262144,
        524288,
        1048576,
        2097152,
        4194304,
        8388608,
        16777216,
        33554432,
        67108864
    };

    /// <summary>
    /// 0x0 - Maior | 0x01 - Menor | 0x02 - Igual
    /// </summary>
    /// <param name="pValue1"></param>
    /// <param name="pValue2"></param>
    /// <param name="pCountBits"></param>
    /// <returns></returns>
    public static byte ComparatorBits(bool[] pValue1, bool[] pValue2, int pCountBits)
    {
        byte bResult = 0;
        //0x00 = maior
        //0x01 = menor
        //0x02 = igual

        int PesoValue1 = 0, PesoValue2 = 0;
        int Count8BitsArray = (pCountBits / 8) > 0 ? pCountBits / 8 : 1;
        int Pos = 0;

        for (int i = 0; i < Count8BitsArray; i++)
        {
            for (int b = 0; b < 8; b++)
            {
                PesoValue1 += (pValue1[Pos] ? 1 : 0) * _PesoPerIndexBit[Pos];
                PesoValue2 += (pValue2[Pos] ? 1 : 0) * _PesoPerIndexBit[Pos];
                Pos++;
            }
        }

        if (PesoValue1 > PesoValue2)
            bResult = 0x00;
        if (PesoValue1 < PesoValue2)
            bResult = 0x01;
        if (PesoValue1 == PesoValue2)
            bResult = 0x02;

        return bResult;
    }
}
