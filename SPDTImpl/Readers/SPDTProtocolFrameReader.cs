using SPDTCore.Core.Readers;
using SPDTSdk;
using System.Collections;

namespace SPDTImpl.Readers;

public sealed class SPDTProtocolFrameReader : ISPDTProtocolFrameReader
{
    /*
     * ---------------------FRAME HEADER---------------------
     * FRAME TYPE	            (8)  BITS	            [0]
     * FRAME LENGHT	            (24) BITS	BIG-ENDIAN  [1-3]
     * PAYLOAD	                BYTES DE DADOS	[4...]
     */


    public SPDTFrameType ReadFrameType
        (ReadOnlySpan<byte> pFrameBuffer)
    {
        var Value = (SPDTFrameType)pFrameBuffer[0];
        return Value;
    }

    public UInt24 ReadFrameLenght
        (ReadOnlySpan<byte> pFrameBuffer)
    {
        var Value = BitReader.ReadUInt24BigEndian(pFrameBuffer, 1);
        return Value;
    }

    public ReadOnlyMemory<byte> ReadFramePayload
        (ReadOnlyMemory<byte> pFrameBuffer)
    {
        var FrameLenghtPayload = ReadFrameLenght(pFrameBuffer.Span);
        var Value = pFrameBuffer.Slice(4, FrameLenghtPayload);
        return Value;
    }
}
