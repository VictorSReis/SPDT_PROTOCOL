﻿namespace SPDTCore.Core.SPDT;


/// <summary>
/// Interface responsável por processar os dados de entrada no protocolo.
/// </summary>
public interface ISPDTCoreProcessInput
{
    public void Start();

    public void ProcessInput(Memory<byte> pSPDTPacket);

    public void Stop();
}
