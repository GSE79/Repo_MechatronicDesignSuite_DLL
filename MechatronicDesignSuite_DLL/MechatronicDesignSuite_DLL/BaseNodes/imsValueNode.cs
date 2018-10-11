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

    public class imsValueNode : imsBaseNode
    {
        public Type DataType { get; } = typeof(byte);
        public int ArrayLength { get; } = 0;
        public int DataSize {get;} = 0;
        public char charValue { get { return charValues[charValues.Count - 1]; } set { charValues[charValues.Count - 1] = value; } }
        public byte byteValue { get { if (byteValues.Count > 0) return byteValues[byteValues.Count - 1]; else return 0x00; } set { if (byteValues.Count > 0) byteValues[byteValues.Count - 1] = value; } }
        public ushort ushortValue { get { return ushortValues[ushortValues.Count - 1]; } set { ushortValues[ushortValues.Count - 1] = value; } }
        public short shortValue { get { return shortValues[shortValues.Count - 1]; } set { shortValues[shortValues.Count - 1] = value; } }
        public uint uintValue { get { return uintValues[uintValues.Count - 1]; } set {uintValues [uintValues.Count - 1] = value; } }
        public int intValue { get { return intValues[intValues.Count - 1]; } set { intValues[intValues.Count - 1] = value; } }
        public float floatValue { get { return floatValues[floatValues.Count - 1]; } set { floatValues[floatValues.Count - 1] = value; } }
        public double doubleValue { get { return doubleValues[doubleValues.Count - 1]; } set { doubleValues[doubleValues.Count - 1] = value; } }

        protected List<char> charValues;
        protected List<byte> byteValues;

        protected List<ushort> ushortValues;
        protected List<short> shortValues;

        protected List<uint> uintValues;
        protected List<int> intValues;

        protected List<float> floatValues;
        protected List<double> doubleValues;

        protected List<DateTime> latchTimes;

        public void clearValues()
        {
            if (DataType == typeof(char))
                charValues.Clear();
            else if (DataType == typeof(byte))
                byteValues.Clear();
            else if (DataType == typeof(ushort))
                ushortValues.Clear();
            else if (DataType == typeof(short))
                shortValues.Clear();
            else if (DataType == typeof(uint))
                uintValues.Clear();
            else if (DataType == typeof(int))
                intValues.Clear();
            else if (DataType == typeof(float))
                floatValues.Clear();
            else if (DataType == typeof(double))
                doubleValues.Clear();
            else
                throw new Exception("Attempted to clear values of an Un-Supported Value Node Data Type");

            latchTimes.Clear();
        }

        public imsValueNode(List<imsBaseNode> globalNodeListIn, string nameString, Type DataTypeIn, int ArrayLengthIn) :base(globalNodeListIn)
        {
            nodeType = typeof(imsValueNode);
            nodeName = nameString;
            DataType = DataTypeIn;
            ArrayLength = ArrayLengthIn;

            //FileStream deSerializeFs = new FileStream();
            //BinaryFormatter DeSerializeFormatter = new BinaryFormatter();

            if (ArrayLength == 0)
                DataSize = (Marshal.SizeOf(DataTypeIn));
            else
                DataSize = ArrayLength *  (Marshal.SizeOf(DataTypeIn));

            if (DataType == typeof(char))
                charValues = new List<char>();
            else if (DataType == typeof(byte))
                byteValues = new List<byte>();
            else if (DataType == typeof(ushort))
                ushortValues = new List<ushort>();
            else if (DataType == typeof(short))
                shortValues = new List<short>();
            else if (DataType == typeof(uint))
                uintValues = new List<uint>();
            else if (DataType == typeof(int))
                intValues = new List<int>();
            else if (DataType == typeof(float))
                floatValues = new List<float>();
            else if (DataType == typeof(double))
                doubleValues = new List<double>();
            else
                throw new Exception("Attempted to instantiate an Un-Supported Value Node Data Type");

            latchTimes = new List<DateTime>();
        }

        public imsValueNode(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs) :base(DeSerializeFormatter, deSerializeFs)
        {

        }

        

    }
}
