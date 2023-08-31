using System;

namespace SPDTSdk;

public static class BitWriter
{
    public static void WriterUInt16BigEndian(Span<byte> pBufferDest, UInt16 pValue)
    {
        WriteBigEndian(pBufferDest, pValue);
    }

    public static void WriterUInt24BigEndian(Span<byte> pBufferDest, UInt24 pValue)
    {
        WriteBigEndian(pBufferDest, pValue);
    }

    public static void WriterUInt32BigEndian(Span<byte> pBufferDest, UInt32 pValue)
    {
        WriteBigEndian(pBufferDest, pValue);
    }

    #region PRIVATE
    private static void WriteBigEndian(Span<byte> pBufferDest, UInt16 pUint16)
    {
        pBufferDest[0] = (byte)((pUint16 >> 8) & 0xff);
        pBufferDest[1] = (byte)((pUint16) & 0xff);
    }

    private static void WriteBigEndian(Span<byte> pBufferDest, UInt24 pUint24)
    {
        pBufferDest[0] = (byte)((pUint24 >> 16) & 0xff);
        pBufferDest[1] = (byte)((pUint24 >> 8) & 0xff);
        pBufferDest[2] = (byte)((pUint24) & 0xff);
    }

    private static void WriteBigEndian(Span<byte> pBufferDest, UInt32 pUint32)
    {
        pBufferDest[0] = (byte)((pUint32 >> 24) & 0xff);
        pBufferDest[1] = (byte)((pUint32 >> 16) & 0xff);
        pBufferDest[2] = (byte)((pUint32 >> 8) & 0xff);
        pBufferDest[3] = (byte)((pUint32) & 0xff);
    }
    #endregion
}
