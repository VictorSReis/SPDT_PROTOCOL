using SPDTCore.Core.Protocol;
using SPDTCore.Core.SPDT;
using SPDTSdk;
using System.Diagnostics;

namespace SPDTImpl.SPDT;

internal sealed class SPDTCoreProcessPacketType : ISPDTCoreProcessPacketType
{
    #region PRIVATE VALUES
    private ISPDTCoreController _SpdtCoreController;
    #endregion


    #region CONSTRUCTOR
    internal SPDTCoreProcessPacketType
        (ISPDTCoreController pSpdtCoreController)
    {
        _SpdtCoreController = pSpdtCoreController;
    }
    #endregion

    #region ISPDTCoreProcessPacketType
    public void ProcessMessagePacketType
        (ISPDTMessage pMessage)
    {
        Private_ProcessPacketType(pMessage);
    }
    #endregion

    #region PRIVATE
    private void Private_ProcessPacketType(ISPDTMessage pMessage) 
    {
        UInt32 StreamID = pMessage.Header.StreamID;

        switch (pMessage.Header.PacketType)
        {
            case SPDTPacketType.PACKET_TP_DATA:
                {
                    //Notify new message assembled
                    _SpdtCoreController.
                        InvokeNotifyNewPublicMessage(pMessage);
                }
                break;

            case SPDTPacketType.PACKET_TP_CREATE_STREAM:
                _SpdtCoreController.InvokeNotifyCreateNewStream(StreamID);
                break;

            case SPDTPacketType.PACKET_TP_CLOSE_STREAM:
                _SpdtCoreController.InvokeNotifyCloseStream(StreamID);
                break;

            case SPDTPacketType.PACKET_TP_RESET_STREAM:
                _SpdtCoreController.InvokeNotifyResetStream(StreamID);
                break;

            case SPDTPacketType.PACKET_TP_PING_STREAM:
                _SpdtCoreController.InvokeNotifyPingStream(StreamID);
                break;

            case SPDTPacketType.PACKET_TP_ERROR:
                //DEVE SER TRATADA IGUAL = SPDTSdk.SPDTPacketType.PACKET_TP_DATA
                //O USUÁRIO DEVE SER INFORMADO.
                _SpdtCoreController.InvokeNotifyNewPublicMessage(pMessage);
                break;

            case SPDTPacketType.PACKET_TP_PING:
                //DEVE SER TRATADA IGUAL = SPDTSdk.SPDTPacketType.PACKET_TP_DATA
                //O USUÁRIO DEVE DECIDIR O QUE FAZER.
                _SpdtCoreController.InvokeNotifyNewPublicMessage(pMessage);
                break;

            case SPDTPacketType.Unknown:
                Debug.WriteLine($"DEBUG: SPDTPacketType desconhecido: {SPDTPacketType.Unknown} - Value({(byte)pMessage.Header.PacketType})");
                _SpdtCoreController.InvokeNotifyError(
                    new NotifyErrorObject(
                        SPDTError.INTERNAL_ERROR,
                        pMessage.Header.StreamID));
                break;

            default:
                Debug.WriteLine($"DEBUG: SPDTPacketType não foi reconhecido: {pMessage.Header.PacketType} - Value({(byte)pMessage.Header.PacketType})");
                _SpdtCoreController.InvokeNotifyError(
                    new NotifyErrorObject(
                        SPDTError.INTERNAL_ERROR,
                        pMessage.Header.StreamID));
                break;
        }
    }
    #endregion
}
