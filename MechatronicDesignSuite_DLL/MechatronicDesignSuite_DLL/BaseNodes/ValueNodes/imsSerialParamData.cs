using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace MechatronicDesignSuite_DLL.BaseNodes
{
    public class imsSerialParamData : imsValueNode
    {
        public List<byte> SerialDataIn { get; } = new List<byte>();
        public List<byte> SerialDataOut { get; } = new List<byte>();
        public int PacketDataOffset { set;  get; } = -1;

        int ByteIdx;

        public imsSerialParamData(List<imsBaseNode> globalNodeListIn, string nameString, Type DataTypeIn, int arrayLength) : base(globalNodeListIn, nameString, DataTypeIn, arrayLength)
        {
            nodeType = typeof(imsSerialParamData);
        }
        public imsSerialParamData(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs) : base(DeSerializeFormatter, deSerializeFs)
        {

        }
        public void UpdateValue(ref byte[] PacketSerialDataIn, bool LogDataFlag, DateTime RxTime)
        {
            SerialDataIn.Clear();
            for (ByteIdx = PacketDataOffset; ByteIdx < PacketDataOffset + DataSize; ByteIdx++)
                SerialDataIn.Add(PacketSerialDataIn[ByteIdx]);

            updateFromSerialData(LogDataFlag, RxTime);

        }
        public void AccumulatePackDataOffset(ref int pDoff)
        {
            PacketDataOffset = pDoff;
            pDoff += DataSize;
        }
        public void updateFromSerialData(bool logDataFlag, DateTime RxTime)
        {
            if (DataType == typeof(char)) {
                if (charValues.Count > 0 && !logDataFlag)
                    charValue = BitConverter.ToChar(SerialDataIn.ToArray(), 0);
                else
                    charValues.Add(BitConverter.ToChar(SerialDataIn.ToArray(), 0)); }
            else if (DataType == typeof(byte))
            {
                if (byteValues.Count > 0 && !logDataFlag)
                    byteValue = SerialDataIn[0];
                else
                    byteValues.Add(SerialDataIn[0]);
            }
            else if (DataType == typeof(ushort))
            {
                if (ushortValues.Count > 0 && !logDataFlag)
                    ushortValue = BitConverter.ToUInt16(SerialDataIn.ToArray(), 0);
                else
                    ushortValues.Add(BitConverter.ToUInt16(SerialDataIn.ToArray(), 0));
            }
            else if (DataType == typeof(short))
            {
                if (shortValues.Count > 0 && !logDataFlag)
                    shortValue = BitConverter.ToInt16(SerialDataIn.ToArray(), 0);
                else
                    shortValues.Add(BitConverter.ToInt16(SerialDataIn.ToArray(), 0));
            }
            else if (DataType == typeof(uint))
            {
                if (uintValues.Count > 0 && !logDataFlag)
                    uintValue = BitConverter.ToUInt32(SerialDataIn.ToArray(), 0);
                else
                    uintValues.Add(BitConverter.ToUInt32(SerialDataIn.ToArray(), 0));
            }
            else if (DataType == typeof(int))
            {
                if (intValues.Count > 0 && !logDataFlag)
                    intValue = BitConverter.ToInt32(SerialDataIn.ToArray(), 0);
                else
                    intValues.Add(BitConverter.ToInt32(SerialDataIn.ToArray(), 0));
            }
            else if (DataType == typeof(float))
            {
                if (floatValues.Count > 0 && !logDataFlag)
                    floatValue = BitConverter.ToSingle(SerialDataIn.ToArray(), 0);
                else
                    floatValues.Add(BitConverter.ToSingle(SerialDataIn.ToArray(), 0));
            }
            else if (DataType == typeof(double))
            {
                if (doubleValues.Count > 0 && !logDataFlag)
                    doubleValue = BitConverter.ToUInt32(SerialDataIn.ToArray(), 0);
                else
                    doubleValues.Add(BitConverter.ToUInt32(SerialDataIn.ToArray(), 0));
            }
            else
                throw new Exception("Attempted to update (from serial data) an Un-Supported Value Node Data Type");

        }
    }
}
