
namespace SPDTSdk;

public struct UInt24 : IEquatable<UInt24>
{
    #region Propriedades
    public static readonly UInt24 MaxValue = (UInt24)0x00FFFFFF;
    public static readonly UInt24 MinValue = 0;
    public Endianness Endian
    {
        get
        {
            return _EndianActually;
        }
    }
    #endregion

    #region Variaveis privadas
    internal byte[] _NArrayData;
    private Endianness _EndianActually = Endianness.LittleEndian;
    #endregion

    #region Construtores
    private UInt24(int pValue)
    {
        Span<byte> Value = GetSpan(pValue);
        _NArrayData = ConstructValue(Endianness.LittleEndian, Value);
    }
    private UInt24(ushort pValue)
    {
        Span<byte> Value = GetSpan(pValue);
        _NArrayData = ConstructValue(Endianness.LittleEndian, Value);
    }
    private UInt24(uint pValue)
    {
        Span<byte> Value = GetSpan(pValue);
        _NArrayData = ConstructValue(Endianness.LittleEndian, Value);
    }
    private UInt24(ReadOnlySpan<byte> pValue)
    {
        _NArrayData = ConstructValue(Endianness.LittleEndian, pValue);
    }
    #endregion

    #region Operadores de Conversão
    public static implicit operator UInt24(ushort v)
    {
        return new UInt24(v);
    }

    public static implicit operator UInt24(uint v)
    {
        return new UInt24(v);
    }

    public static implicit operator UInt24(int v)
    {
        return new UInt24(v);
    }

    public static implicit operator int(UInt24 Value)
    {
        return ToInt(Value);
    }

    public static explicit operator UInt24(Span<byte> value)
    {
        return new UInt24(value);
    }

    public static explicit operator UInt24(ReadOnlySpan<byte> value)
    {
        return new UInt24(value);
    }
    #endregion

    #region Operadores de Verificação
    public static bool operator ==(UInt24 value1, UInt24 value2)
    {
        return value1.Equals(value2);
    }

    public static bool operator !=(UInt24 value1, UInt24 value2)
    {
        return !(value1 == value2);
    }

    public static bool operator >(UInt24 value1, UInt24 value2)
    {
        return BitComparator_Large(value1, value2);
    }

    public static bool operator >=(UInt24 value1, UInt24 value2)
    {
        return BitComparator_BiggerOrEqual(value1, value2);
    }

    public static bool operator <(UInt24 value1, UInt24 value2)
    {
        return BitComparator_Smaller(value1, value2);
    }

    public static bool operator <=(UInt24 value1, UInt24 value2)
    {
        return BitComparator_LessOrEqual(value1, value2);
    }
    #endregion

    #region IEquatable
    public bool Equals(UInt24 other)
    {
        return (
            _NArrayData[0] == other._NArrayData[0] &
            _NArrayData[1] == other._NArrayData[1] &
            _NArrayData[2] == other._NArrayData[2]);
    }

    public override bool Equals(object obj)
    {
        return obj is UInt24 @uint24 && Equals(@uint24);
    }
    #endregion

    #region Métodos publicos
    public Span<byte> ToSpanArray()
    {
        return GetSpan(this);
    }

    public void SwapEndian()
    {
        //Swap Endian Prop
        _EndianActually = _EndianActually == Endianness.LittleEndian ? Endianness.BigEndian : Endianness.LittleEndian;

        //Swap Endian Data.
        SwapEndianness(
            ref _NArrayData,
            _EndianActually);
    }

    public void SetEndian(Endianness pEndian)
    {
        _EndianActually = pEndian;
    }

    public override int GetHashCode()
    {
        return this;
    }

    public override string ToString()
    {
        return ToUInt(this).ToString();
    }
    #endregion

    #region Métodos privados
    private static Span<byte> GetSpan(ushort pValue)
    {
        return new Span<byte>(BitConverter.GetBytes(pValue));
    }
    private static Span<byte> GetSpan(uint pValue)
    {
        return new Span<byte>(BitConverter.GetBytes(pValue));
    }
    private static Span<byte> GetSpan(int pValue)
    {
        return new Span<byte>(BitConverter.GetBytes(pValue));
    }
    private static Span<byte> GetSpan(UInt24 pValue)
    {
        return pValue._NArrayData;
    }

    private static byte[] ConstructValue(Endianness pEndDestination, ReadOnlySpan<byte> pValue)
    {
        int Count = pValue.Length;
        byte[] Result = new byte[3];
        switch (pEndDestination)
        {
            case Endianness.LittleEndian:
                for (int i = 0; i < pValue.Length; i++)
                {
                    if (i < 3)
                        Result[i] = pValue[i];
                }
                break;
            case Endianness.BigEndian:
                for (int i = 2; i < pValue.Length; i--)
                {
                    if (i >= 0)
                        Result[i] = pValue[i];
                }
                break;
        }
        return Result;
    }

    private static int ToInt(UInt24 Value)
    {
        int Result = 0;
        byte[] BuffValue = Value._NArrayData;
        Endianness EndianValue = Value.Endian;
        byte[] bInt = new byte[4];
        if (EndianValue == Endianness.BigEndian)
        {
            bInt[0] = 0x0;
            bInt[1] = BuffValue[0];
            bInt[2] = BuffValue[1];
            bInt[3] = BuffValue[2];
            Result = (int)BitConverter.ToUInt32(bInt);
        }
        else
        {
            bInt[0] = BuffValue[0];
            bInt[1] = BuffValue[1];
            bInt[2] = BuffValue[2];
            bInt[3] = 0x0;
            Result = (int)BitConverter.ToUInt32(bInt);
        }
        return Result;
    }

    private static uint ToUInt(UInt24 Value)
    {
        uint Result = 0;
        byte[] BuffValue = Value._NArrayData;
        byte[] bInt = new byte[4];
        bool PlatformIsLittleEndian = OsLittleEndian();

        if (PlatformIsLittleEndian)
        {
            //LITTLE ENDIAN PLATFORM
            Endianness ValueEndian = Value.Endian;
            switch (ValueEndian)
            {
                case Endianness.LittleEndian:
                    bInt[0] = BuffValue[0];
                    bInt[1] = BuffValue[1];
                    bInt[2] = BuffValue[2];
                    bInt[3] = 0x0;
                    break;

                case Endianness.BigEndian:
                    bInt[0] = BuffValue[2];
                    bInt[1] = BuffValue[1];
                    bInt[2] = BuffValue[0];
                    bInt[3] = 0x0;
                    break;
                default:
                    break;
            }
        }
        else
        {
            //BIG ENDIAN PLATFORM
            throw new Exception("BIG ENDIAN PLATFORM NOT IMPLEMENTED CORRECT");

            /*
            Endianness ValueEndian = Value.Endian;
            switch (ValueEndian)
            {
                case Endianness.LittleEndian:
                    bInt[0] = 0x0;
                    bInt[1] = BuffValue[2];
                    bInt[2] = BuffValue[1];
                    bInt[3] = BuffValue[0];
                    break;
                case Endianness.BigEndian:
                    bInt[0] = 0x0;
                    bInt[1] = BuffValue[0];
                    bInt[2] = BuffValue[1];
                    bInt[3] = BuffValue[2];
                    break;
                default:
                    break;
            }
            */
        }

        Result = BitConverter.ToUInt32(bInt, 0);
        return Result;
    }

    private static bool OsLittleEndian()
    {
        int n = 1;
        // little endian if true
        return (n & (1 << 0)) != 0;
    }

    private static void SwapEndianness(ref byte[] pByteSwap, Endianness pDest)
    {
        pByteSwap = pByteSwap.Reverse().ToArray();
    }

    private static bool GetBit(UInt24 value, int bitnumber)
    {
        bool Resultado;

        switch (value.Endian)
        {
            case Endianness.LittleEndian:
                Resultado = (value._NArrayData[0] & (1 << bitnumber)) != 0;
                break;
            case Endianness.BigEndian:
                Resultado = (value._NArrayData[2] & (1 << bitnumber)) != 0;
                break;
            default:
                Resultado = false;
                break;
        }

        return Resultado;
    }

    #region Bit Value Comparator
    //0x00 = maior
    //0x01 = menor
    //0x02 = igual


    private static bool BitComparator_Large(UInt24 value1, UInt24 value2)
    {
        byte ResultComparator = CallComparator(value1, value2);
        bool Resultado = false;

        if (ResultComparator == 0x00)
            Resultado = true;

        return Resultado;
    }

    private static bool BitComparator_BiggerOrEqual(UInt24 value1, UInt24 value2)
    {
        byte ResultComparator = CallComparator(value1, value2);
        bool Resultado = false;

        if (ResultComparator == 0x00 || ResultComparator == 0x02)
            Resultado = true;

        return Resultado;
    }

    private static bool BitComparator_Smaller(UInt24 value1, UInt24 value2)
    {
        byte ResultComparator = CallComparator(value1, value2);
        bool Resultado = false;

        if (ResultComparator == 0x01)
            Resultado = true;

        return Resultado;
    }

    private static bool BitComparator_LessOrEqual(UInt24 value1, UInt24 value2)
    {
        byte ResultComparator = CallComparator(value1, value2);
        bool Resultado = false;

        if (ResultComparator == 0x01 || ResultComparator == 0x02)
            Resultado = true;

        return Resultado;
    }

    private static byte CallComparator(UInt24 value1, UInt24 value2)
    {
        //TIPOS DE RETORNO
        //0x00 = maior
        //0x01 = menor
        //0x02 = maior_ou_igual
        //0x03 = menor_ou_igual
        byte Result = 0x0;

        //Converte para Boolean.
        bool[] Val1 = new bool[24];
        bool[] Val2 = new bool[24];
        int pos = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int b = 0; b < 8; b++)
            {
                Val1[pos] = GetBit(value1._NArrayData[i], b);
                Val2[pos] = GetBit(value2._NArrayData[i], b);
                pos++;
            }
        }

        //Chama o comparador
        Result = BitComparator(Val1, Val2);

        //Retorna o resultado.
        return Result;
    }

    private static byte BitComparator(bool[] value1, bool[] value2)
    {
        //0x00 = maior
        //0x01 = menor
        //0x02 = igual
        byte Result = 0x0;
        Result = BitMagnitudeComparator.ComparatorBits(value1, value2, 24);
        return Result;
    }
    #endregion

    #endregion
}
