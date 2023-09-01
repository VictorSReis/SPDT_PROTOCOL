using SPDTCore.Core.Protocol;
using SPDTCore.Core.SPDT;
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
            case SPDTSdk.SPDTPacketType.PACKET_TP_DATA:
                {
                    //Notify new message assembled
                    _SpdtCoreController.
                        Invoke_ProcessInputNewMessage(pMessage);
                }
                break;

            case SPDTSdk.SPDTPacketType.PACKET_TP_CREATE_STREAM:
                _SpdtCoreController.Invoke_ProcessInputCreateNewStream(StreamID);
                break;

            case SPDTSdk.SPDTPacketType.PACKET_TP_STREAM_CREATED_SUCESSFULLY:
                _SpdtCoreController.Invoke_ProcessInputStreamCreatedSuccessfully(StreamID);
                break;

            case SPDTSdk.SPDTPacketType.PACKET_TP_CLOSE_STREAM:
                _SpdtCoreController.Invoke_ProcessInputCloseStream(StreamID);
                break;

            case SPDTSdk.SPDTPacketType.PACKET_TP_RESET_STREAM:
                _SpdtCoreController.Invoke_ProcessInputResetStream(StreamID);
                break;

            case SPDTSdk.SPDTPacketType.PACKET_TP_PING_STREAM:
                _SpdtCoreController.Invoke_ProcessInputPingStream(StreamID);
                break;

            case SPDTSdk.SPDTPacketType.PACKET_TP_PING:
                //DEVE SER TRATADA IGUAL = SPDTSdk.SPDTPacketType.PACKET_TP_DATA
                //O USUÁRIO DEVE DECIDIR O QUE FAZER.
                _SpdtCoreController.Invoke_ProcessInputNewMessage(pMessage);
                break;

            case SPDTSdk.SPDTPacketType.Unknown:
                Debug.WriteLine($"DEBUG: SPDTPacketType desconhecido: {SPDTSdk.SPDTPacketType.Unknown} - Value({(byte)pMessage.Header.PacketType})");
                _SpdtCoreController.Invoke_ProcessInputMalformedData();
                break;

            default:
                Debug.WriteLine($"DEBUG: SPDTPacketType não foi reconhecido: {pMessage.Header.PacketType} - Value({(byte)pMessage.Header.PacketType})");
                _SpdtCoreController.Invoke_ProcessInputMalformedData();
                break;
        }
    }
    #endregion
}
