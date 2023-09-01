using System;

namespace SPDTCore.Core.SPDT;

/// <summary>
/// Interface que implementa o protocolo SPDT e todos os seus recursos necessários para execução.
/// </summary>
public interface ISPDTCore
{
    public void CreateResources();

    public void Initialize();

    public void ProcessInput(Memory<byte> pSpdtPacket);
}
