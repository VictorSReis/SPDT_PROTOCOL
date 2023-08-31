using SPDTCore.Core.Readers;
using SPDTSdk;

namespace SPDTImpl.Readers;

public sealed class SPDTProtocolHeaderReader : ISPDTProtocolHeaderReader
{
    /*
     * ---------------------HEADER---------------------
     * SPDT VERSION	   (16) BITS	BIG-ENDIAN   [0-1]
     * PACKET TYPE	   (8) BITS	                 [2]
     * STREAM ID	   (32) BITS	BIG-ENDIAN   [3-6]
     * FRAGMENT ID	   (32) BITS	BIG-ENDIAN   [7-10]
     */


    public SPDTProtocolVersion ReadProtocolVersion
        (ReadOnlySpan<byte> pBufferHeader)
    {
        var Value = (SPDTProtocolVersion)BitReader.ReadUInt16BigEndian(pBufferHeader, 0);
        return Value;
    }

    public SPDTPacketType ReadPacketType
        (ReadOnlySpan<byte> pBufferHeader)
    {
        var Value = (SPDTPacketType)pBufferHeader[2];
        return Value;
    }

    public uint ReadStreamID
        (ReadOnlySpan<byte> pBufferHeader)
    {
        var Value = BitReader.ReadUInt32BigEndian(pBufferHeader, 3);
        return Value;
    }

    public uint ReadFragmentID
        (ReadOnlySpan<byte> pBufferHeader)
    {
        var Value = BitReader.ReadUInt32BigEndian(pBufferHeader, 7);
        return Value;
    }
}
