using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using MechatronicDesignSuite_DLL.BaseTypes;

namespace MechatronicDesignSuite_DLL.BaseNodes
{
    public class imsSerialParamData : imsValueNode
    {
        protected List<byte> SerialDataIn = new List<byte>();
        [Category("Serial Data"), Description("The serial data in - convert to to data type")]
        public List<byte> getSerialDataIn { get { return SerialDataIn; } }
        [Category("Serial Data"), Description("The serial data in - convert to to data type")]
        public List<byte> setSerialDataIn { get { return SerialDataIn; } set { SerialDataIn = value; } }

        protected List<byte> SerialDataOut = new List<byte>();
        [Category("Serial Data"), Description("The serial data out - converted from data type")]
        public List<byte> getSerialDataOut { get { return SerialDataOut; } }
        [Category("Serial Data"), Description("The serial data out - converted from data type")]
        public List<byte> setSerialDataOut { get { return SerialDataOut; } set { SerialDataOut = value; } }

        protected int PacketDataOffset = -1;
        [Category("Serial Data"), Description("The serial data offset - within its serial packet")]
        public int getPacketDataOffset { get { return PacketDataOffset; } }
        [Category("Serial Data"), Description("The serial data offset - within its serial packet")]
        public int setPacketDataOffset { get { return PacketDataOffset; } set { PacketDataOffset = value; } }

        int ByteIdx;

        protected imsCyclicPacketCommSystem cyclicCommsSysLink;
        public imsCyclicPacketCommSystem setcyclicCommSysLink {set{ cyclicCommsSysLink = value; } get { return cyclicCommsSysLink; } }
        protected SerialParameterPacketHeader txPackHeader = new SerialParameterPacketHeader();

        public imsSerialParamData(List<imsBaseNode> globalNodeListIn, string nameString, Type DataTypeIn, int arrayLength) : base(globalNodeListIn, nameString, DataTypeIn, arrayLength)
        {
            NodeType = typeof(imsSerialParamData);
        }
        public imsSerialParamData(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs) : base(DeSerializeFormatter, deSerializeFs)
        {

        }
        public void setTxPacket(int packIDin)
        {
            txPackHeader.PacketID = (uint)packIDin;
            txPackHeader.PacketType = 1u;
            txPackHeader.DataOffset = (uint)PacketDataOffset;
        }
        public void UpdateValue(ref byte[] PacketSerialDataIn, bool LogDataFlag, DateTime RxTime)
        {
            SerialDataIn.Clear();
            for (ByteIdx = PacketDataOffset; ByteIdx < PacketDataOffset + DataSize; ByteIdx++)
                SerialDataIn.Add(PacketSerialDataIn[ByteIdx]);

            updateFromSerialData(LogDataFlag, RxTime);
            newData = true;
        }
        public void AccumulatePackDataOffset(ref int pDoff)
        {
            PacketDataOffset = pDoff;
            pDoff += DataSize;
            txPackHeader.DataOffset = (uint)PacketDataOffset;
        }
        public void updateFromSerialData(bool logDataFlag, DateTime RxTime)
        {
            if (DataType == typeof(char))
            {

                if (ArrayLength > 1)
                {
                    charValues.Clear();
                    foreach (byte b in SerialDataIn)
                        charValues.Add((char)b);
                    charValue = charValues[charValues.Count-1];
                }
                else
                {
                    charValue = (char)SerialDataIn[0];// BitConverter.ToChar(SerialDataIn.ToArray(), 0);

                    if (logDataFlag)
                        charValues.Add(charValue);
                }                   
            }
            else if (DataType == typeof(byte))
            {
                byteValue = SerialDataIn[0];

                if (logDataFlag)
                    byteValues.Add(byteValue);
            }
            else if (DataType == typeof(ushort))
            {
                ushortValue = BitConverter.ToUInt16(SerialDataIn.ToArray(), 0);

                if (logDataFlag)
                    ushortValues.Add(ushortValue);
            }
            else if (DataType == typeof(short))
            {
                shortValue = BitConverter.ToInt16(SerialDataIn.ToArray(), 0);
                if (logDataFlag)
                   shortValues.Add(shortValue);
            }
            else if (DataType == typeof(uint))
            {
                uintValue = BitConverter.ToUInt32(SerialDataIn.ToArray(), 0);
                if (logDataFlag)
                   uintValues.Add(uintValue);
            }
            else if (DataType == typeof(int))
            {
                intValue = BitConverter.ToInt32(SerialDataIn.ToArray(), 0);
                if (logDataFlag)
                    intValues.Add(intValue);
            }
            else if (DataType == typeof(float))
            {
                floatValue = BitConverter.ToSingle(SerialDataIn.ToArray(), 0);
                if (logDataFlag)
                    floatValues.Add(floatValue);
            }
            else if (DataType == typeof(double))
            {
                setdoubleValue = BitConverter.ToDouble(SerialDataIn.ToArray(), 0);
                if (logDataFlag)
                    doubleValues.Add(setdoubleValue);
            }
            else
                throw new Exception("Attempted to update (from serial data) an Un-Supported Value Node Data Type");

            if (logDataFlag)
                latchTimes.Add((RxTime-cyclicCommsSysLink.TimeAtStartLogging).TotalSeconds);

        }

        public void packageToSerialData()
        {
            SerialDataOut.Clear();
            SerialDataOut.Capacity = DataSize;

            if(DataType == typeof(byte))
            {
                SerialDataOut = (BitConverter.GetBytes(byteValue)).ToList();
                while (SerialDataOut.Count > 1)
                    SerialDataOut.RemoveAt(SerialDataOut.Count - 1);
            }
            else if (DataType == typeof(char))
            {
                if(ArrayLength>1)
                {
                    foreach (char c in charValues)
                        SerialDataOut.Add((byte)c);
                }
                else
                {
                    SerialDataOut = (BitConverter.GetBytes(charValue)).ToList();
                    while (SerialDataOut.Count > 1)
                        SerialDataOut.RemoveAt(SerialDataOut.Count - 1);
                }
                
            }
            else if (DataType == typeof(ushort))
            {
                SerialDataOut = (BitConverter.GetBytes(ushortValue)).ToList();
                while (SerialDataOut.Count > 2)
                    SerialDataOut.RemoveAt(SerialDataOut.Count - 1);
            }
            else if (DataType == typeof(short))
            {
                SerialDataOut = (BitConverter.GetBytes(shortValue)).ToList();
                while (SerialDataOut.Count > 2)
                    SerialDataOut.RemoveAt(SerialDataOut.Count - 1);
            }
            else if (DataType == typeof(uint))
            {
                SerialDataOut = (BitConverter.GetBytes(uintValue)).ToList();
                while (SerialDataOut.Count > 4)
                    SerialDataOut.RemoveAt(SerialDataOut.Count - 1);
            }
            else if (DataType == typeof(int))
            {
                SerialDataOut = (BitConverter.GetBytes(intValue)).ToList();
                while (SerialDataOut.Count > 4)
                    SerialDataOut.RemoveAt(SerialDataOut.Count - 1);
            }
            else if (DataType == typeof(float))
            {
                SerialDataOut = (BitConverter.GetBytes(floatValue)).ToList();
                while (SerialDataOut.Count > 4)
                    SerialDataOut.RemoveAt(SerialDataOut.Count - 1);
            }
            else if (DataType == typeof(double))
            {
                SerialDataOut = (BitConverter.GetBytes(setdoubleValue)).ToList();
                while (SerialDataOut.Count > 8)
                    SerialDataOut.RemoveAt(SerialDataOut.Count - 1);
            }

            // Flag, Call, or otherwise Initiate Queing of Write Packet for this SPD
            cyclicCommsSysLink.AddTxPack2TXQueue(txPackHeader, SerialDataOut);

        }
        
    }
}
