using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Reflection.Emit;
using System.Globalization;
using System.Windows.Forms;
using System.ComponentModel;
using MechatronicDesignSuite_DLL.BaseNodes;

namespace MechatronicDesignSuite_DLL.BaseTypes
{
    public class SerialParameterPacketHeader
    {
        public uint PacketID;
        public uint PacketLength;
        public uint PacketType;
        public uint DataOffset;

        public Type DefaultType = typeof(byte);

        public byte[] HDR2ByteArray()
        {
            return HDR2ByteArray(DefaultType);
        }
        public uint HDRLength()
        {
            return HDRLength(DefaultType);
        }

        private byte [] getBytesTrimmed(byte[] BytesIn, int trimSize)
        {
            List<byte> tempBytes = new List<byte>();
            tempBytes.AddRange(BytesIn);
            if(tempBytes.Count>trimSize)
            {
                tempBytes.RemoveRange(trimSize, tempBytes.Count-trimSize);
            }
            return tempBytes.ToArray();
        }
        public byte[] HDR2ByteArray(Type HDRDTypeIn)
        {
            List<byte[]> ByteArrayBuffer = new List<byte[]>();            
            if(HDRDTypeIn==typeof(byte))
            {
                ByteArrayBuffer.Add(getBytesTrimmed(BitConverter.GetBytes(((byte)(PacketID))),1));
                ByteArrayBuffer.Add(getBytesTrimmed(BitConverter.GetBytes(((byte)(PacketLength))), 1));
                ByteArrayBuffer.Add(getBytesTrimmed(BitConverter.GetBytes(((byte)(PacketType))), 1));
                ByteArrayBuffer.Add(getBytesTrimmed(BitConverter.GetBytes(((byte)(DataOffset))), 1));
            }
            else if (HDRDTypeIn == typeof(ushort))
            {
                ByteArrayBuffer.Add(getBytesTrimmed(BitConverter.GetBytes(((ushort)(PacketID))), 2));
                ByteArrayBuffer.Add(getBytesTrimmed(BitConverter.GetBytes(((ushort)(PacketLength))), 2));
                ByteArrayBuffer.Add(getBytesTrimmed(BitConverter.GetBytes(((ushort)(PacketType))), 2));
                ByteArrayBuffer.Add(getBytesTrimmed(BitConverter.GetBytes(((ushort)(DataOffset))), 2));
            }
            else if (HDRDTypeIn == typeof(uint))
            {
                ByteArrayBuffer.Add(getBytesTrimmed(BitConverter.GetBytes(((uint)(PacketID))), 4));
                ByteArrayBuffer.Add(getBytesTrimmed(BitConverter.GetBytes(((uint)(PacketLength))), 4));
                ByteArrayBuffer.Add(getBytesTrimmed(BitConverter.GetBytes(((uint)(PacketType))), 4));
                ByteArrayBuffer.Add(getBytesTrimmed(BitConverter.GetBytes(((uint)(DataOffset))), 4));
            }

            List<byte> ByteBuffer = new List<byte>();
            foreach (byte[] bArray in ByteArrayBuffer)
            {
                foreach (byte b in bArray)
                {
                    ByteBuffer.Add(b);
                }
            }

            return ByteBuffer.ToArray();
        }
        public uint HDRLength(Type HDRDTypeIn)
        {
            return (uint)(HDR2ByteArray(HDRDTypeIn)).Length;
        }
    }

}
