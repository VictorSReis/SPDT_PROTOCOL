using SPDTCore.Core;
using SPDTCore.Core.Factory;
using SPDTCore.Core.Protocol;
using SPDTCore.Core.SPDT;
using SPDTImpl.Factory;
using SPDTImpl.Protocol;
using System.Diagnostics;

namespace SPDTImpl.SPDT;

public sealed class SPDTCore
{
	#region PRIVATE RESOURCES
	private ISPDTGlobalObjects _SpdtGlobalObjects;
	private ISPDTFactory _SpdtFactory;
	private ISPDTProtocol _SpdtProtocol;
	private ISPDTCoreStreams _SpdtCoreStreams;
	private ISPDTCoreStreamGenerateID _SpdtStreamGenerateID;
	private ISPDTCoreController _SpdtCoreController;
	#endregion

	#region PROCESS
	private ISPDTCoreProcessInput _SpdtCoreProcessInput;
    private ISPDTCoreForwarderMessage _SpdtCoreForwarderMessage;
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

        //CREATE PROCESS INPUT
        _SpdtCoreProcessInput = new SPDTCoreProcessInput(_SpdtCoreController, _SpdtGlobalObjects);
        _SpdtCoreForwarderMessage = new SPDTCoreForwarderMessage(_SpdtCoreStreams);
    }

    public void Initialize()
	{
        //Initialize Core Streams
        _SpdtCoreStreams.CreateSPDTCoreStream(100);

        //GET EVENTS CORE CONROLLER
        _SpdtCoreController.OnNotifyProcessInputMalformedData += _SpdtCoreController_OnNotifyProcessInputMalformedData;
        _SpdtCoreController.OnNotifyProcessInputNewData += _SpdtCoreController_OnNotifyProcessInputNewData;
        _SpdtCoreController.OnNotifyProcessInputNewMessage += _SpdtCoreController_OnNotifyProcessInputNewMessage;

        //INITIALIZE PROCESS INPUT
        _SpdtCoreProcessInput.Start();
    }

    public void ProcessInput(Memory<byte> pSpdtPacket)
    {
        _SpdtCoreProcessInput.ProcessInput(pSpdtPacket);
    }
    #endregion

    #region EVENTS CORE CONTROLLER
    private void _SpdtCoreController_OnNotifyProcessInputNewMessage(object sender, ISPDTMessage e)
    {
        Debug.WriteLine($"DEBUG: New Message Stream ID: {e.Header.StreamID}");
        _SpdtCoreForwarderMessage.Forwarder(e);
    }

    private void _SpdtCoreController_OnNotifyProcessInputNewData(object sender, EventArgs e)
    {
        Debug.WriteLine($"DEBUG: New input data");
    }

    private void _SpdtCoreController_OnNotifyProcessInputMalformedData(object sender, EventArgs e)
    {
        Debug.WriteLine($"DEBUG: Malformed Packet");
    }
    #endregion

    #region PRIVATE

    #endregion
}
