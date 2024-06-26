﻿using SPDTCore.Core.Writers;
using SPDTSdk;
using System;

namespace SPDTImpl.Writers;

public sealed class SPDTProtocolFrameWriter : ISPDTProtocolFrameWriter
{
    /*
     * ---------------------FRAME HEADER---------------------
     * FRAME TYPE	            (8)  BITS	            [0]
     * FRAME LENGHT	            (24) BITS	BIG-ENDIAN  [1-3]
     * PAYLOAD	                BYTES DE DADOS	        [4...]
     */


    public void WriteFrameType
        (SPDTFrameType pSPDTFrameType, Memory<byte> pBufferFrame)
    {
        pBufferFrame.Span[0] = (byte)pSPDTFrameType;
    }

    public void WriteFrameLenght
        (UInt24 pFrameLenght, Memory<byte> pBufferFrame)
    {
        BitWriter.WriterUInt24BigEndian(pBufferFrame.Span[1..], pFrameLenght);
    }

    public void WriteFramePayload
        (Memory<byte> pFramePayload, Memory<byte> pBufferFrame)
    {
        var Destination = pBufferFrame.Span[4..];
        pFramePayload.Span.CopyTo(Destination);
    }

    public void WriteFramePayload
        (Span<byte> pFramePayload, Memory<byte> pBufferFrame)
    {
        var Destination = pBufferFrame.Span[4..];
        pFramePayload.CopyTo(Destination);
    }
}
