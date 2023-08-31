using SPDTCore.Core.Protocol;
using SPDTCore.Core.Readers;
using SPDTCore.Core.Writers;
using SPDTImpl.Readers;
using SPDTImpl.Writers;
using SPDTSdk;

namespace SPDTImpl.Protocol;

/// <summary>
/// Implementação default da interface <see cref="ISPDTProtocol"/>.
/// </summary>
public sealed class SPDTProtocol : ISPDTProtocol
{
    #region READRS & WRITERS
    private ISPDTProtocolHeaderReader _HeaderReader;
    private ISPDTProtocolFrameReader _FrameReader;
    //WRITERS
    private ISPDTProtocolHeaderWriter _HeaderWriter;
    private ISPDTProtocolFrameWriter _FrameWriter;
    #endregion


    #region CONSTRUCTOR
    public SPDTProtocol()
    {
        _HeaderReader = new SPDTProtocolHeaderReader();
        _FrameReader = new SPDTProtocolFrameReader();
        //Writers
        _HeaderWriter = new SPDTProtocolHeaderWriter();
        _FrameWriter = new SPDTProtocolFrameWriter();
    }
    #endregion

    #region READERS
    public uint ReadFragmentID
        (ReadOnlySpan<byte> pBufferHeader)
    {
        return _HeaderReader.ReadFragmentID(pBufferHeader);
    }

    public UInt24 ReadFrameLenght
        (ReadOnlySpan<byte> pFrameBuffer)
    {
       return _FrameReader.ReadFrameLenght(pFrameBuffer);
    }

    public ReadOnlyMemory<byte> ReadFramePayload
        (ReadOnlyMemory<byte> pFrameBuffer)
    {
        return _FrameReader.ReadFramePayload(pFrameBuffer);
    }

    public SPDTFrameType ReadFrameType
        (ReadOnlySpan<byte> pFrameBuffer)
    {
        return _FrameReader.ReadFrameType(pFrameBuffer);
    }

    public SPDTPacketType ReadPacketType
        (ReadOnlySpan<byte> pBufferHeader)
    {
        return _HeaderReader.ReadPacketType(pBufferHeader);
    }

    public SPDTProtocolVersion ReadProtocolVersion
        (ReadOnlySpan<byte> pBufferHeader)
    {
        return _HeaderReader.ReadProtocolVersion(pBufferHeader);
    }

    public uint ReadStreamID
        (ReadOnlySpan<byte> pBufferHeader)
    {
        return _HeaderReader.ReadStreamID(pBufferHeader);
    }
    #endregion

    #region WRITERS
    public void WriteFragmentID
        (uint pFragmentID, Memory<byte> pBufferHeader)
    {
        _HeaderWriter.WriteFragmentID(pFragmentID, pBufferHeader);
    }

    public void WritePacketType
        (SPDTPacketType pPacketType, Memory<byte> pBufferHeader)
    {
        _HeaderWriter.WritePacketType(pPacketType, pBufferHeader);
    }

    public void WriteProtocolVersion
        (SPDTProtocolVersion pProtocolVersion, Memory<byte> pBufferHeader)
    {
        _HeaderWriter.WriteProtocolVersion(pProtocolVersion, pBufferHeader);
    }

    public void WriterFrameLenght
        (UInt24 pFrameLenght, Memory<byte> pBufferFrame)
    {
       _FrameWriter.WriterFrameLenght(pFrameLenght, pBufferFrame);
    }

    public void WriterFramePayload
        (Memory<byte> pFramePayload, Memory<byte> pBufferFrame)
    {
        _FrameWriter.WriterFramePayload(pFramePayload, pBufferFrame);   
    }

    public void WriterFramePayload
        (Span<byte> pFramePayload, Memory<byte> pBufferFrame)
    {
        _FrameWriter.WriterFramePayload(pFramePayload, pBufferFrame);
    }

    public void WriterFrameType
        (SPDTFrameType pSPDTFrameType, Memory<byte> pBufferFrame)
    {
        _FrameWriter.WriterFrameType(pSPDTFrameType, pBufferFrame);
    }

    public void WriteStreamID
        (uint pStreamID, Memory<byte> pBufferHeader)
    {
        _HeaderWriter.WriteStreamID(pStreamID, pBufferHeader);
    }
    #endregion
}
