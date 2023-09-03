using SPDTCore.Core;
using SPDTCore.Core.Factory;
using SPDTCore.Core.Handles;
using SPDTCore.Core.Protocol;
using SPDTCore.Core.Readers;
using SPDTCore.Core.SPDT;
using SPDTImpl.Factory;
using SPDTImpl.Protocol;
using SPDTImpl.Readers;
using SPDTSdk;
using System.Diagnostics;

namespace SPDTImpl.SPDT;

/// <summary>
/// Implementação default do protocolo SPDT.
/// </summary>
public sealed class SPDTCore: ISPDTCore
{
    #region EVENT
    public event EventHandler<ISPDTStream> OnNewOpenStream;
    public event EventHandler<ISPDTMessage> OnNewGlobalMessage;
    #endregion

    #region PRIVATE RESOURCES
    private ISPDTGlobalObjects _SpdtGlobalObjects;
	private ISPDTFactory _SpdtFactory;
    private ISPDTCoreValidator _SpdtCoreValidator;
    private ISPDTProtocol _SpdtProtocol;
	private ISPDTCoreStreams _SpdtCoreStreams;
	private ISPDTCoreStreamGenerateID _SpdtStreamGenerateID;
	private ISPDTCoreController _SpdtCoreController;
    private ISPDTCoreProcessPacketType _SpdtProcessPacketType;
    private ISPDTCoreMessageCreator _SpdtCoreMessageCreator;
    private ISPDTReaderStream _SpdtReaderStream;
    #endregion

    #region PROCESS
    private ISPDTCoreProcessInput _SpdtCoreProcessInput;
    private ISPDTCoreForwarderMessage _SpdtCoreForwarderMessage;
    private ISPDTCoreForwardEndpoint _SpdtCoreForwardEndpoint;
    private SPDTCoreHandles.NotifyUserForwardMessageHandle _SpdtNotifyForwardMessageHandle;
    #endregion

    List<UInt32> Lista_Stream_Requested_Created_Endpoint = new ();

    #region CONSTRUCTOR
    public SPDTCore()
	{

    }
    #endregion

    #region ISPDTCore
    public void CreateResources()
	{
        //CREATE OBJECTS RESOURCE

        _SpdtGlobalObjects = new SPDTGlobalObjects();
        _SpdtFactory = new SPDTFactory(_SpdtGlobalObjects);
        _SpdtCoreValidator = new SPDTCoreValidator();
        _SpdtProtocol = new SPDTProtocol();
        _SpdtGlobalObjects.RegisterSPDTFactory(_SpdtFactory);
        _SpdtGlobalObjects.RegisterSPDTProtocol(_SpdtProtocol);
        _SpdtGlobalObjects.RegisterSPDTValidator(_SpdtCoreValidator);
        _SpdtCoreStreams = new SPDTCoreStreams(_SpdtGlobalObjects);
        _SpdtStreamGenerateID = new SPDTCoreStreamGenerateID();
        _SpdtCoreController = new SPDTCoreController();
        _SpdtProcessPacketType = new SPDTCoreProcessPacketType(_SpdtCoreController);
        _SpdtCoreMessageCreator = new SPDTCoreMessageCreator(_SpdtGlobalObjects, _SpdtStreamGenerateID);
        _SpdtCoreForwardEndpoint = new SPDTCoreForwardEndpoint(ref _SpdtNotifyForwardMessageHandle);
        _SpdtReaderStream = new SPDTReaderStream(_SpdtProtocol);

        //CREATE PROCESS INPUT
        _SpdtCoreForwarderMessage = new SPDTCoreForwarderMessage(_SpdtCoreStreams);
        _SpdtCoreProcessInput = new SPDTCoreProcessInput(_SpdtCoreController, _SpdtGlobalObjects, _SpdtProcessPacketType);
    }

    public void Initialize()
	{
        //Initialize Core Streams
        _SpdtCoreStreams.CreateSPDTCoreStream(100);

        //CONFIGURE EVENTS CONTROLLER
        PrivateAssociateEventsCoreController();

        //INITIALIZE PROCESS INPUT
        _SpdtCoreProcessInput.Start();
    }

    public void ProcessInput(Memory<byte> pSpdtPacket)
    {
        _SpdtCoreProcessInput.ProcessInput(pSpdtPacket);
    }

    public UInt32 RequestStreamCreation()
    {
        var MessageRequestCreateStream = _SpdtCoreMessageCreator.CreateMessageRequestNewStreamEndpoint
            (out var OutStreamIDRequest);
        _SpdtNotifyForwardMessageHandle.Invoke(MessageRequestCreateStream);
        return OutStreamIDRequest;
    }

    public void RegisterStreamIDRequested(UInt32 pStreamID)
    {
        Lista_Stream_Requested_Created_Endpoint.Add(pStreamID);
    }

    public ISPDTCoreMessageCreator GetMessageCreator()
    {
        return _SpdtCoreMessageCreator;
    }

    public ISPDTCoreForwardEndpoint GetForwardEndpoint()
    {
        return _SpdtCoreForwardEndpoint;
    }

    public ISPDTReaderStream GetReaderStream()
        => _SpdtReaderStream;
    #endregion

    #region EVENTS CORE CONTROLLER
    private void _SpdtCoreController_OnNotifyProcessInputNewMessage
        (object sender, ISPDTMessage e)
    {
        Debug.WriteLine($"DEBUG: New Message Stream ID: {e.Header.StreamID}");
        if(e.Header.PacketType == SPDTPacketType.PACKET_TP_ERROR)
        {
            OnNewGlobalMessage?.Invoke(this, e);
            return;
        }    

        _SpdtCoreForwarderMessage.Forwarder(e);
    }

    private void _SpdtCoreController_OnNotifyNewMessageDataReceived
        (object sender, int e)
    {
        Debug.WriteLine($"DEBUG: New '{e} bytes' on input data");
    }

    private void _SpdtCoreController_OnNotifyError
        (object sender, NotifyErrorObject e)
    {
        var MessageForward = _SpdtCoreMessageCreator.
            CreateMessageError(e.Error, e.StreamIDGeneratedError);
        _SpdtNotifyForwardMessageHandle?.Invoke(MessageForward);
    }

    private void _SpdtCoreController_OnNotifyInputPingStream
        (object sender, uint e)
    {
        throw new NotImplementedException();
    }

    private void _SpdtCoreController_OnNotifyCloseStream
        (object sender, uint e)
    {
        throw new NotImplementedException();
    }

    private void _SpdtCoreController_OnNotifyResetStream
        (object sender, uint e)
    {
        var StreamManager = _SpdtCoreStreams.GetStreamManagerByID(e);
        if (StreamManager is null)
            return;

        StreamManager.InvokeResetStream();
    }

    private void _SpdtCoreController_OnNotifyRequestCreateNewStream
        (object sender, uint e)
    {
        Debug.WriteLine($"DEBUG: Request create new stream id: {e}");

        try
        {
            //VERIFICA SE JÁ NÃO EXISTE UM STREAM COM ESSE ID.
            if (_SpdtCoreStreams.StreamIsRegistred(e))
                throw new InvalidOperationException($"Ocorreu uma falha grave. O fluxo '{e}' já se encontrava registrado!");

            //CREATE STREAM
            var NewStreamItem = _SpdtCoreStreams.CreateStream
                (e, SPDTStreamState.STREAM_CREATED);

            //REGISTER
            _SpdtCoreStreams.RegisterStream(NewStreamItem);

            //UPDATE STATE
            NewStreamItem.SpdtStreamManager.InvokeUpdateStreamState
                (SPDTStreamState.STREAM_OPEN);

            //VERIFICA SE FOI O USUÁRIO QUE SOLICITOU A CRIAÇÃO DO FLUXO E O ENDPOINT
            //ESTA APENAS NOTIFICANDO QUE FOI CRIADO, SE SIM, NÃO CHAMA O MÉTODO QUE SOLICITA A PARTE CONTRATIA
            //A CRIAÇÃO DE UM NOVO FLUXO.
            if (PrivateCheckStreamIDRequestedInList(e))
            {
                Lista_Stream_Requested_Created_Endpoint.Remove(e);
            }
            else
            {
                //Notify Endpoint for created stream.
                PrivateSendMessageEndpointCreatedStream(e);
            }

            //Notify user for open stream.
            OnNewOpenStream?.Invoke(this, NewStreamItem.SpdtStream);
        }
        catch (Exception Er)
        {
            Debug.WriteLine($"DEBUG: Exception in create new stream id({e}): Message > {Er.GetType().Name}:{Er.Message}");
        }
    }
    #endregion

    #region PRIVATE
    private void PrivateAssociateEventsCoreController()
    {
        //GET EVENTS CORE CONROLLER
        _SpdtCoreController.OnNotifyError += _SpdtCoreController_OnNotifyError;
        _SpdtCoreController.OnNotifyNewMessageDataReceived += _SpdtCoreController_OnNotifyNewMessageDataReceived;
        _SpdtCoreController.OnNotifyNewPublicMessage += _SpdtCoreController_OnNotifyProcessInputNewMessage;
        _SpdtCoreController.OnNotifyCreateNewStream += _SpdtCoreController_OnNotifyRequestCreateNewStream;
        _SpdtCoreController.OnNotifyResetStream += _SpdtCoreController_OnNotifyResetStream;
        _SpdtCoreController.OnNotifyCloseStream += _SpdtCoreController_OnNotifyCloseStream;
        _SpdtCoreController.OnNotifyPingStream += _SpdtCoreController_OnNotifyInputPingStream;
    }

    private void PrivateSendMessageEndpointCreatedStream
        (UInt32 pStreamIDCreated)
    {
        var MessageForward = _SpdtCoreMessageCreator.
            CreateMessage(SPDTProtocolVersion.ProtocolVersion10, SPDTPacketType.PACKET_TP_CREATE_STREAM, pStreamIDCreated, 0);
        _SpdtNotifyForwardMessageHandle?.Invoke(MessageForward);
    }

    private bool PrivateCheckStreamIDRequestedInList(UInt32 pStreamID)
    {
        if(Lista_Stream_Requested_Created_Endpoint.Exists(x=> x == pStreamID))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
