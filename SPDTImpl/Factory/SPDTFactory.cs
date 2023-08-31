using SPDTCore.Core;
using SPDTCore.Core.Factory;
using SPDTCore.Core.Protocol;
using SPDTImpl.Protocol;

namespace SPDTImpl.Factory;

/// <summary>
/// Implementação default da interface <see cref="ISPDTFactory"/>.
/// </summary>
public sealed class SPDTFactory : ISPDTFactory
{
    #region SPDT GLOBAL OBJECTS
    private ISPDTGlobalObjects _SPDTGlobalObjects;
    #endregion


    #region CONSTRUCTOR
    internal SPDTFactory
        (ISPDTGlobalObjects pSPDTGlobalObjects)
    {
        _SPDTGlobalObjects = pSPDTGlobalObjects;
    }
    #endregion

    #region ISPDTFactory
    public ISPDTFrame CreateFrameObject()
    {
        var SpdtFrame = new SPDTFrame(_SPDTGlobalObjects);
        return SpdtFrame;
    }

    public ISPDTHeader CreateHeaderObject()
    {
        var SpdtHeader = new SPDTHeader(_SPDTGlobalObjects);
        return SpdtHeader;
    }
    #endregion
}
