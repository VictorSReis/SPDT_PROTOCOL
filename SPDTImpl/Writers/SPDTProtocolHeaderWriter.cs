using SPDTCore.Core.Writers;
using SPDTSdk;

namespace SPDTImpl.Writers;

public sealed class SPDTProtocolHeaderWriter : ISPDTProtocolHeaderWriter
{
    /*
     * ---------------------HEADER---------------------
     * SPDT VERSION	   (16) BITS	BIG-ENDIAN   [0-1]
     * PACKET TYPE	   (8) BITS	                 [2]
     * STREAM ID	   (32) BITS	BIG-ENDIAN   [3-6]
     * FRAGMENT ID	   (32) BITS	BIG-ENDIAN   [7-10]
     */


    public void WriteProtocolVersion
        (SPDTProtocolVersion pProtocolVersion, Memory<byte> pBufferHeader)
    {
        BitWriter.WriterUInt16BigEndian(pBufferHeader.Span, (UInt16)pProtocolVersion);
    }

    public void WritePacketType
        (SPDTPacketType pPacketType, Memory<byte> pBufferHeader)
    {
        pBufferHeader.Span[2] = (byte)pPacketType;
    }

    public void WriteStreamID
        (uint pStreamID, Memory<byte> pBufferHeader)
    {
        BitWriter.WriterUInt32BigEndian(pBufferHeader.Span[3..], pStreamID);
    }

    public void WriteFragmentID
        (uint pFragmentID, Memory<byte> pBufferHeader)
    {
        BitWriter.WriterUInt32BigEndian(pBufferHeader.Span[7..], pFragmentID);
    }
}
