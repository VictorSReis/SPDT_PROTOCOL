using SPDTCore.Core;
using SPDTCore.Core.Factory;
using SPDTCore.Core.Handles;
using SPDTCore.Core.Protocol;
using SPDTCore.Core.SPDT;
using SPDTImpl.Factory;
using SPDTImpl.Protocol;
using System.Diagnostics;

namespace SPDTImpl.SPDT;

/// <summary>
/// Implementação default do protocolo SPDT.
/// </summary>
public sealed class SPDTCore: ISPDTCore
{
	#region PRIVATE RESOURCES
	private ISPDTGlobalObjects _SpdtGlobalObjects;
	private ISPDTFactory _SpdtFactory;
	private ISPDTProtocol _SpdtProtocol;
	private ISPDTCoreStreams _SpdtCoreStreams;
	private ISPDTCoreStreamGenerateID _SpdtStreamGenerateID;
	private ISPDTCoreController _SpdtCoreController;
    private ISPDTCoreProcessPacketType _SpdtProcessPacketType;
    private ISPDTCoreMessageCreator _SpdtCoreMessageCreator;
    #endregion

    #region PROCESS
    private ISPDTCoreProcessInput _SpdtCoreProcessInput;
    private ISPDTCoreForwarderMessage _SpdtCoreForwarderMessage;
    private ISPDTCoreForwardEndpoint _SpdtCoreForwardEndpoint;
    private SPDTCoreHandles.NotifyUserForwardMessageHandle _SpdtNotifyForwardMessageHandle;
    #endregion


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
        _SpdtProtocol = new SPDTProtocol();
        _SpdtGlobalObjects.RegisterSPDTFactory(_SpdtFactory);
        _SpdtGlobalObjects.RegisterSPDTProtocol(_SpdtProtocol);
        _SpdtCoreStreams = new SPDTCoreStreams(_SpdtGlobalObjects);
        _SpdtStreamGenerateID = new SPDTCoreStreamGenerateID();
        _SpdtCoreController = new SPDTCoreController();
        _SpdtProcessPacketType = new SPDTCoreProcessPacketType(_SpdtCoreController);
        _SpdtCoreMessageCreator = new SPDTCoreMessageCreator(_SpdtGlobalObjects, _SpdtStreamGenerateID);
        _SpdtCoreForwardEndpoint = new SPDTCoreForwardEndpoint(ref _SpdtNotifyForwardMessageHandle);

        //CREATE PROCESS INPUT
        _SpdtCoreForwarderMessage = new SPDTCoreForwarderMessage(_SpdtCoreStreams);
        _SpdtCoreProcessInput = new SPDTCoreProcessInput(_SpdtCoreController, _SpdtGlobalObjects, _SpdtProcessPacketType);
    }

    public void Initialize()
	{
        //Initialize Core Streams
        _SpdtCoreStreams.CreateSPDTCoreStream(100);

        //GET EVENTS CORE CONROLLER
        _SpdtCoreController.OnNotifyProcessInputMalformedData += _SpdtCoreController_OnNotifyProcessInputMalformedData;
        _SpdtCoreController.OnNotifyProcessInputNewData += _SpdtCoreController_OnNotifyProcessInputNewData;
        _SpdtCoreController.OnNotifyProcessInputNewMessage += _SpdtCoreController_OnNotifyProcessInputNewMessage;
        _SpdtCoreController.OnNotifyProcessInputCreateNewStream += _SpdtCoreController_OnNotifyProcessInputCreateNewStream;
        _SpdtCoreController.OnNotifyProcessInputResetStream += _SpdtCoreController_OnNotifyProcessInputResetStream;
        _SpdtCoreController.OnNotifyProcessInputCloseStream += _SpdtCoreController_OnNotifyProcessInputCloseStream;
        _SpdtCoreController.OnNotifyProcessInputPingStream += _SpdtCoreController_OnNotifyProcessInputPingStream;
        _SpdtCoreController.OnNotifyProcessInputStreamCreatedSuccessfully += _SpdtCoreController_OnNotifyProcessInputStreamCreatedSuccessfully;

        //INITIALIZE PROCESS INPUT
        _SpdtCoreProcessInput.Start();
    }

    public void ProcessInput(Memory<byte> pSpdtPacket)
    {
        _SpdtCoreProcessInput.ProcessInput(pSpdtPacket);
    }

    public ISPDTCoreMessageCreator GetMessageCreator()
    {
        return _SpdtCoreMessageCreator;
    }
    #endregion

    #region EVENTS CORE CONTROLLER
    private void _SpdtCoreController_OnNotifyProcessInputNewMessage
        (object sender, ISPDTMessage e)
    {
        Debug.WriteLine($"DEBUG: New Message Stream ID: {e.Header.StreamID}");
        _SpdtCoreForwarderMessage.Forwarder(e);
    }

    private void _SpdtCoreController_OnNotifyProcessInputNewData
        (object sender, EventArgs e)
    {
        Debug.WriteLine($"DEBUG: New input data");
    }

    private void _SpdtCoreController_OnNotifyProcessInputMalformedData
        (object sender, EventArgs e)
    {
        Debug.WriteLine($"DEBUG: Malformed Packet");
    }

    private void _SpdtCoreController_OnNotifyProcessInputPingStream
        (object sender, uint e)
    {
        throw new NotImplementedException();
    }

    private void _SpdtCoreController_OnNotifyProcessInputCloseStream
        (object sender, uint e)
    {
        throw new NotImplementedException();
    }

    private void _SpdtCoreController_OnNotifyProcessInputResetStream
        (object sender, uint e)
    {
        var StreamManager = _SpdtCoreStreams.GetStreamManagerByID(e);
        if (StreamManager is null)
            return;

        StreamManager.InvokeResetStream();
    }


    private void _SpdtCoreController_OnNotifyProcessInputCreateNewStream
        (object sender, uint e)
    {
        try
        {
            //CREATE STREAM
            var NewStreamItem = _SpdtCoreStreams.CreateStream
                (e, SPDTSdk.SPDTStreamState.STREAM_CREATED);

            //REGISTER
            _SpdtCoreStreams.RegisterStream(NewStreamItem);

            //UPDATE STATE
            NewStreamItem.SpdtStreamManager.InvokeUpdateStreamState
                (SPDTSdk.SPDTStreamState.STREAM_OPEN);
        }
        catch (Exception Er)
        {
            Debug.WriteLine($"DEBUG: Exception in create new stream id({e}): Message > {Er.GetType().Name}:{Er.Message}");
        }
    }

    private void _SpdtCoreController_OnNotifyProcessInputStreamCreatedSuccessfully
        (object sender, uint e)
    {
        try
        {
            //CREATE STREAM
            var NewStreamItem = _SpdtCoreStreams.CreateStream
                (e, SPDTSdk.SPDTStreamState.STREAM_CREATED);

            //REGISTER
            _SpdtCoreStreams.RegisterStream(NewStreamItem);

            //UPDATE STATE
            NewStreamItem.SpdtStreamManager.InvokeUpdateStreamState
                (SPDTSdk.SPDTStreamState.STREAM_OPEN);
        }
        catch (Exception Er)
        {
            Debug.WriteLine($"DEBUG: Exception in create new stream id({e}): Message > {Er.GetType().Name}:{Er.Message}");
        }
    }
    #endregion

    #region PRIVATE

    #endregion
}
