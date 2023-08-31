using SPDTCore.Core.SPDT;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace SPDTImpl.SPDT;

public sealed class SPDTCoreStreamGenerateID : ISPDTCoreStreamGenerateID
{
    #region COLLECTIONS STREAM ID`S GENERATED
    private Collection<UInt32> _GeneratedIDs = new();
    #endregion


    #region ISPDTCoreStreamGenerateID
    public UInt32 GenerateStreamID()
    {
        UInt32 Result;
        while (true)
        {
            //GENERATE DATA NUMBER ID
            byte[] RandomDataID = RandomNumberGenerator.GetBytes(4);

            //GARANTE QUE SEJA UM VALOR POSITIVO.
            SetBit(RandomDataID, 0, false);

            //CONSTRUCTOR
            Result = BitConverter.ToUInt32(RandomDataID);

            //VERIFICA SE O VALOR É VALIDO
            if (Result == 0)
                continue;

            //VERIFICA SE NÃO FOI GERADO ANTERIORMENTE UM ID IGUAL
            bool SafeGerantedId = IdSafe(Result);
            if (!SafeGerantedId)
                continue;

            //ID VALIDO
            _GeneratedIDs.Add(Result);

            break;
        }
        return Result;
    }
    #endregion

    #region PRIVATE
    private bool IdSafe(UInt32 pIdCheck)
        => !_GeneratedIDs.Contains(pIdCheck);


    private static byte[] SetBit(byte[] self, int index, bool value)
    {
        int byteIndex = index / 8;
        int bitIndex = index % 8;
        byte mask = (byte)(1 << bitIndex);

        self[byteIndex] = (byte)(value ? (self[byteIndex] | mask) : (self[byteIndex] & ~mask));
        return self;
    }
    #endregion
}
