using SPDTCore.Core.SPDT;
using SPDTSdk;
using System.Collections.Immutable;

namespace SPDTImpl.SPDT;

internal sealed class SPDTCoreValidator : ISPDTCoreValidator
{
    private ImmutableList<SPDTPacketType> LOOKUP_TABLE_PACKET_TYPES;

    #region CONSTUCTOR
    internal SPDTCoreValidator()
    {
        LOOKUP_TABLE_PACKET_TYPES = Enum.GetValues<SPDTPacketType>().ToImmutableList();
    }
    #endregion

    #region MÉTODOS
    public bool ValidatePacketType(byte pPacketType)
    {
        return LOOKUP_TABLE_PACKET_TYPES.Exists(x => x == (SPDTPacketType)pPacketType);
    }
    #endregion
}
