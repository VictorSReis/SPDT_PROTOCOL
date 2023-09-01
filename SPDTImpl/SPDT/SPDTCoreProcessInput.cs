﻿using SPDTCore.Core;
using SPDTCore.Core.Protocol;
using SPDTCore.Core.SPDT;

namespace SPDTImpl.SPDT;

public sealed class SPDTCoreProcessInput : ISPDTCoreProcessInput
{
    #region PRIVATE VALUES
    private ISPDTCoreController _SpdtCoreController;
    private ISPDTGlobalObjects _pSpdtGlobalObjects;
    private Queue<Memory<byte>> _QueueMemorySpdt;
    private Task _TaskOperationProcessInput;
    private bool _PermissionWhile;
    #endregion


    #region CONSTRUCTOR
    internal SPDTCoreProcessInput
        (ISPDTCoreController pSpdtCoreController, ISPDTGlobalObjects pSpdtGlobalObjects)
    {
        _SpdtCoreController = pSpdtCoreController;
        _pSpdtGlobalObjects = pSpdtGlobalObjects;
        _QueueMemorySpdt = new Queue<Memory<byte>>(10);
        _PermissionWhile = true;
    }
    #endregion

    #region ISPDTCoreProcessInput
    public void ProcessInput
        (Memory<byte> pSPDTPacket)
    {
        _QueueMemorySpdt.Enqueue(pSPDTPacket);
        _SpdtCoreController.Invoke_ProcessInput_NewData();
    }

    public void Start()
    {
        _TaskOperationProcessInput = new Task(StartProcessInput);
        _TaskOperationProcessInput.Start();
    }

    public void Stop()
    {
        _PermissionWhile = false;
        _TaskOperationProcessInput.Wait();
    }
    #endregion

    #region TASK METHOD PROCESS INPUT
    private void StartProcessInput()
    {
        while (_PermissionWhile)
        {
            if (_QueueMemorySpdt.Count <= 0)
                goto Done;

            //GET PACKET FOR PROCESS
            var PacketForProcess = PrivateGetNextMemoryPacketProcess();

            //CRATE MESSAGE OBJECT
            var NewSpdtMessageObject = PrivateCreateNewMessageObject();

            //ASSEMBLE MESSAGE
            bool ResultAssembleMsg = PrivateAssembleMessage
                (ref NewSpdtMessageObject, PacketForProcess);
            if (!ResultAssembleMsg)
                _SpdtCoreController.Invoke_ProcessInput_Malformed_Data();

            //Notify new message assembled
            _SpdtCoreController.Invoke_ProcessInput_NewMessage
                (NewSpdtMessageObject);


            if (_QueueMemorySpdt.Count > 0)
                goto NextProcess;

            Done:;
            Thread.Sleep(10);
        NextProcess:;
        }
    }
    #endregion

    #region PRIVATE
    private Memory<byte> PrivateGetNextMemoryPacketProcess()
    {
        _QueueMemorySpdt.TryDequeue(out Memory<byte> OutPacket);
        return OutPacket;
    }

    private ISPDTMessage PrivateCreateNewMessageObject()
    {
        var NewMessage = _pSpdtGlobalObjects.SpdtFactory.CreateMessageObject();
        NewMessage.CreateMessage();
        return NewMessage;
    }

    private bool PrivateAssembleMessage
        (ref ISPDTMessage pMessage, Memory<byte> pObjectDataAssemble)
    {
        bool Result = false;

        try
        {
            pMessage.AssembleMessage(pObjectDataAssemble);
            Result = true;
        }
        catch (Exception)
        {
            //MALFORMED PACKET
        }

        return Result;
    }
    #endregion
}