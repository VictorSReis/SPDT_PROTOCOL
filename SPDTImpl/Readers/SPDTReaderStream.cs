﻿using SPDTCore.Core.Protocol;
using SPDTCore.Core.Readers;
using SPDTSdk;
using System.Net.Sockets;

namespace SPDTImpl.Readers;

internal sealed class SPDTReaderStream : ISPDTReaderStream
{
    #region PRIVATE VALUES
    private ISPDTProtocol _SpdtProtocol;
    #endregion


    #region CONSTUCTOR
    internal SPDTReaderStream
        (ISPDTProtocol pSpdtProtocol)
    {
        _SpdtProtocol = pSpdtProtocol;
    }
    #endregion

    #region ISPDTReaderStream
    public bool Read(System.IO.Stream pStream, out Memory<byte> pOutBufferSDPTPaket)
    {
        bool Result = false;
        pOutBufferSDPTPaket = default;

        if (pStream.Length < SPDTConstants.SIZE_HEADER_OBJECT)
            goto Done;

        Memory<byte> Header = new byte[SPDTConstants.SIZE_HEADER_OBJECT];
        pStream.Read(Header.Span);

        UInt24 SizePayload = _SpdtProtocol.ReadPayloadLenght(Header.Span);
        if (SizePayload == 0)
        {
            pOutBufferSDPTPaket = new byte[SPDTConstants.SIZE_HEADER_OBJECT];
            Header.CopyTo(pOutBufferSDPTPaket);
            Result = true;

            goto Done;
        }

        while (pStream.Length < SizePayload)
        {
            Thread.Sleep(10);
        }

        Memory<byte> Payload = new byte[SizePayload];
        pStream.Read(Payload.Span);

        pOutBufferSDPTPaket = new byte[SPDTConstants.SIZE_HEADER_OBJECT + Payload.Length];
        Header.CopyTo(pOutBufferSDPTPaket);
        Payload.CopyTo(pOutBufferSDPTPaket[SPDTConstants.SIZE_HEADER_OBJECT..]);
        Result = true;

        Header.Span.Fill(0);
        Payload.Span.Fill(0);
        Header = new byte[1];
        Payload = new byte[1];
        Header = null;
        Payload = null;

    Done:;
        return Result;
    }

    public bool ReadFromSocket(Socket pSocket, out Memory<byte> pOutBufferSDPTPaket)
    {
        bool Result = false;
        pOutBufferSDPTPaket = default;

        if (pSocket.Available < SPDTConstants.SIZE_HEADER_OBJECT)
            goto Done;

        Memory<byte> Header = new byte[SPDTConstants.SIZE_HEADER_OBJECT];
        PrivateReadSocket(pSocket, Header);

        UInt24 SizePayload = _SpdtProtocol.ReadPayloadLenght(Header.Span);
        if (SizePayload == 0)
        {
            pOutBufferSDPTPaket = new byte[SPDTConstants.SIZE_HEADER_OBJECT];
            Header.CopyTo(pOutBufferSDPTPaket);
            Result = true;

            goto Done;
        }

        while (pSocket.Available < SizePayload)
        {
            Thread.Sleep(10);
        }

        Memory<byte> Payload = new byte[SizePayload];
        PrivateReadSocket(pSocket, Payload);

        pOutBufferSDPTPaket = new byte[SPDTConstants.SIZE_HEADER_OBJECT + Payload.Length];
        Header.CopyTo(pOutBufferSDPTPaket);
        Payload.CopyTo(pOutBufferSDPTPaket[SPDTConstants.SIZE_HEADER_OBJECT..]);
        Result = true;

        Header.Span.Fill(0);
        Payload.Span.Fill(0);
        Header = new byte[1];
        Payload = new byte[1];
        Header = null;
        Payload = null;

    Done:;
        return Result;
    }
    #endregion


    #region PRIVATE
    private void PrivateReadSocket(Socket pSocket, Memory<byte> pBufferReceiver)
    {
        pSocket.Receive(pBufferReceiver.Span);
    }
    #endregion
}
