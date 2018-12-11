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
    /// <summary>
    /// imsValueNode : imsBaseNode
    /// </summary>
    public class imsValueNode : imsBaseNode
    {
        /// <summary>
        /// Data Type - 
        /// </summary>
        protected Type DataType = typeof(byte);
        [Category("Value Node"), Description("The .NET datatype of this instance")]
        public Type getDataType { get { return DataType; } }
        [Category("Value Node"), Description("The .NET datatype of this instance")]
        public Type setDataType { get { return DataType; } set { DataType = value; } }

        /// <summary>
        /// ArrayLength
        /// </summary>
        protected int ArrayLength = 0;
        [Category("Value Node"), Description("The number elements of this data type in the array")]
        public int getArrayLength { get { return ArrayLength; } }
        [Category("Value Node"), Description("The number elements of this data type in the array")]
        public int setArrayLength { get { return ArrayLength; } set { ArrayLength = value; } }

        /// <summary>
        /// DataSize
        /// </summary>
        protected int DataSize = 0;
        [Category("Value Node"), Description("The size, in bytes, of the data type * number of elements in array")]
        public int getDataSize { get { return DataSize; } }
        [Category("Value Node"), Description("The size, in bytes, of the data type * number of elements in array")]
        public int setDataSize { get { return DataSize; } set { DataSize = value; } }

        /// <summary>
        /// charValue
        /// </summary>
        protected char charValue;
        protected List<char> charValues;
        [Category("datatype: char"), Description("The .NET char datatype representation")]
        public char getcharValue { get { return charValue; } }
        [Category("datatype: char"), Description("The .NET char datatype representation")]
        public char setcharValue { get { return charValue; } set { charValue = value; } }
        [Category("datatype: char"), Description("The .NET char datatype representation")]
        public List<char> getcharValues { get { return charValues; } }
        [Category("datatype: char"), Description("The .NET char datatype representation")]
        public List<char> setcharValues { get { return charValues; } set { charValues = value; } }

        /// <summary>
        /// byteValue
        /// </summary>
        protected byte byteValue;
        protected List<byte> byteValues;
        [Category("datatype: byte"), Description("The .NET byte datatype representation")]
        public byte getbyteValue { get { return byteValue; } }
        [Category("datatype: byte"), Description("The .NET byte datatype representation")]
        public byte setbyteValue { get { return byteValue; } set { byteValue = value; } }
        [Category("datatype: byte"), Description("The .NET byte datatype representation")]
        public List<byte> getbyteValues { get { return byteValues; } }
        [Category("datatype: byte"), Description("The .NET byte datatype representation")]
        public List<byte> setbyteValues { get { return byteValues; } set { byteValues = value; } }

        /// <summary>
        /// ushortValue
        /// </summary>
        protected ushort ushortValue;
        protected List<ushort> ushortValues;
        [Category("datatype: ushort"), Description("The .NET char ushort representation")]
        public ushort getushortValue { get { return ushortValue; } }
        [Category("datatype: ushort"), Description("The .NET char ushort representation")]
        public ushort setushortValue { get { return ushortValue; } set { ushortValue = value; } }
        [Category("datatype: ushort"), Description("The .NET char ushort representation")]
        public List<ushort> getushortValues { get { return ushortValues; } }
        [Category("datatype: ushort"), Description("The .NET char ushort representation")]
        public List<ushort> setushortValues { get { return ushortValues; } set { ushortValues = value; } }

        /// <summary>
        /// shortValue
        /// </summary>
        protected short shortValue;
        protected List<short> shortValues;
        [Category("datatype: short"), Description("The .NET char short representation")]
        public short getshortValue { get { return shortValue; } }
        [Category("datatype: short"), Description("The .NET char short representation")]
        public short setshortValue { get { return shortValue; } set { shortValue = value; } }
        [Category("datatype: short"), Description("The .NET char short representation")]
        public List<short> getshortValues { get { return shortValues; } }
        [Category("datatype: short"), Description("The .NET char short representation")]
        public List<short> setshortValues { get { return shortValues; } set { shortValues = value; } }

        /// <summary>
        /// uintValue
        /// </summary>
        protected uint uintValue;
        protected List<uint> uintValues;
        [Category("datatype: uint"), Description("The .NET uint representation")]
        public uint getuintValue { get { return uintValue; } }
        [Category("datatype: uint"), Description("The .NET uint representation")]
        public uint setuintValue { get { return uintValue; } set { uintValue = value; } }
        [Category("datatype: uint"), Description("The .NET uint representation")]
        public List<uint> getuintValues { get { return uintValues; } }
        [Category("datatype: uint"), Description("The .NET uint representation")]
        public List<uint> setuintValues { get { return uintValues; } set { uintValues = value; } }

        /// <summary>
        /// intValue
        /// </summary>
        protected int intValue;
        protected List<int> intValues;
        [Category("datatype: int"), Description("The .NET int representation")]
        public int getintValue { get { return intValue; } }
        [Category("datatype: int"), Description("The .NET int representation")]
        public int setintValue { get { return intValue; } set { intValue = value; } }
        [Category("datatype: int"), Description("The .NET int representation")]
        public List<int> getintValues { get { return intValues; } }
        [Category("datatype: int"), Description("The .NET int representation")]
        public List<int> setintValues { get { return intValues; } set { intValues = value; } }

        /// <summary>
        /// floatValue
        /// </summary>
        protected float floatValue;
        protected List<float> floatValues;
        [Category("datatype: float"), Description("The .NET float representation")]
        public float getfloatValue { get { return floatValue; } }
        [Category("datatype: float"), Description("The .NET float representation")]
        public float setfloatValue { get { return floatValue; } set { floatValue = value; } }
        [Category("datatype: float"), Description("The .NET float representation")]
        public List<float> getfloatValues { get { return floatValues; } }
        [Category("datatype: float"), Description("The .NET float representation")]
        public List<float> setfloatValues { get { return floatValues; } set { floatValues = value; } }

        /// <summary>
        /// doubleValue
        /// </summary>
        protected double doubleValue;
        protected List<double> doubleValues;
        [Category("datatype: double"), Description("The .NET double representation")]
        public double getdoubleValue { get { return doubleValue; } }
        [Category("datatype: double"), Description("The .NET double representation")]
        public double setdoubleValue { get { return doubleValue; } set { doubleValue = value; } }
        [Category("datatype: double"), Description("The .NET double representation")]
        public List<double> getdoubleValues { get { return doubleValues; } }
        [Category("datatype: double"), Description("The .NET double representation")]
        public List<double> setdoubleValues { get { return doubleValues; } set { doubleValues = value; } }

        /// <summary>
        /// latchTimes
        /// </summary>
        protected List<double> latchTimes;
        public List<double> LatchTimes { set { latchTimes = value; } get { return latchTimes; } }

        protected bool newData = false;
        public bool setnewData { get { return newData; }set { newData = value; } }

        public bool HoldRefresh {set{holdRefresh = value; } get {return holdRefresh;} }
        protected bool holdRefresh = false;

        /// <summary>
        /// clearValues()
        /// </summary>
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

        protected List<GUIValueLinks> StaticGUILinks = new List<GUIValueLinks>();
        public List<GUIValueLinks> getStaticGUILinks { get { return StaticGUILinks; } }
        public void addStaticGUILink2SPD(Control GUIFieldIn, string unitsstringin, float scalarin, string formatstringin, bool readOnlyIn)
        {
            StaticGUILinks.Add(new GUIValueLinks(this, GUIFieldIn, unitsstringin, scalarin, formatstringin, readOnlyIn));
        }
        
        public void clearStaticGUILinks()
        {
            StaticGUILinks.Clear();
        }
        public void updateGUILINKS()
        {
            if(newData && !holdRefresh)
            {
                foreach (GUIValueLinks GUILink in StaticGUILinks)
                {
                    if (typeof(TextBox).IsInstanceOfType(GUILink.StaticLinkedGUIField))
                    {
                        ((TextBox)(GUILink.StaticLinkedGUIField)).Text = ToValueString(GUILink);
                    }
                    else if (typeof(System.Windows.Forms.Label).IsInstanceOfType(GUILink.StaticLinkedGUIField))
                    {
                        ((System.Windows.Forms.Label)(GUILink.StaticLinkedGUIField)).Text = ToValueString(GUILink);
                    }
                    else if (typeof(ComboBox).IsInstanceOfType(GUILink.StaticLinkedGUIField))
                    {
                        int tempIndex = ToSelectedIndex(GUILink);
                        if (tempIndex <= ((ComboBox)(GUILink.StaticLinkedGUIField)).MaxDropDownItems &&
                            tempIndex >= 0)
                            ((ComboBox)(GUILink.StaticLinkedGUIField)).SelectedIndex = tempIndex;
                        else
                            ((ComboBox)(GUILink.StaticLinkedGUIField)).Text = ToValueString(GUILink);

                    }
                }
                newData = false;
            }
            
            
        }
        public string toNameString(GUIValueLinks LinkIn)
        {
            return NodeName + " " + LinkIn.UnitsString;
        }

        public string ToValueString(GUIValueLinks LinkIn)
        {
            int i;
            if (DataType == typeof(byte))
            {
                if (LinkIn.formatstring.Contains("x") || LinkIn.formatstring.Contains("X"))
                    return "0x" + ((byte)(LinkIn.scalar*byteValue)).ToString("X2");
                else if (LinkIn.formatstring.Contains("b") || LinkIn.formatstring.Contains("B"))
                    return string.Concat("0b", (Convert.ToString((byte)(LinkIn.scalar * byteValue), 2)).PadLeft(8, '0'));
                else
                    return ((byte)(byteValue * LinkIn.scalar)).ToString(LinkIn.formatstring);
            }
                
            else if (DataType == typeof(char))
            {
                if(ArrayLength>1)
                {
                    List<char> tempStringChars = new List<char>();
                    tempStringChars.AddRange(charValues);
                    string outString = "";
                    for(i=0; i< tempStringChars.Count; i++)
                        outString = string.Concat(outString, tempStringChars[i]);
                        
                    return outString;
                }
                else
                {
                    if (LinkIn.formatstring.Contains("x") || LinkIn.formatstring.Contains("X"))
                        return "0x" + ((byte)(LinkIn.scalar * charValue)).ToString("X2");
                    else if (LinkIn.formatstring.Contains("b") || LinkIn.formatstring.Contains("B"))
                        return string.Concat("0b", (Convert.ToString(((char)(charValue * LinkIn.scalar)), 2)).PadLeft(8, '0'));
                    else
                        return ((char)(charValue * LinkIn.scalar)).ToString();
                }
                
            }
                
            else if (DataType == typeof(ushort))
            {
                if (LinkIn.formatstring.Contains("x") || LinkIn.formatstring.Contains("X"))
                    return "0x" + ((ushort)(LinkIn.scalar * ushortValue)).ToString("X2");
                else if (LinkIn.formatstring.Contains("b") || LinkIn.formatstring.Contains("B"))
                    return string.Concat("0b", (Convert.ToString(((ushort)(ushortValue*LinkIn.scalar)), 2)).PadLeft(8, '0'));
                else
                    return ((ushort)(ushortValue * LinkIn.scalar)).ToString(LinkIn.formatstring);
            }
                
            else if (DataType == typeof(short))
            {
                if (LinkIn.formatstring.Contains("x") || LinkIn.formatstring.Contains("X"))
                    return "0x" + ((short)(LinkIn.scalar * shortValue)).ToString("X2");
                else if (LinkIn.formatstring.Contains("b") || LinkIn.formatstring.Contains("B"))
                    return string.Concat("0b", (Convert.ToString(((short)(shortValue * LinkIn.scalar)), 2)).PadLeft(8, '0'));
                else
                    return (shortValue * LinkIn.scalar).ToString(LinkIn.formatstring);
            }
                
            else if (DataType == typeof(uint))
            {
                if (LinkIn.formatstring.Contains("x") || LinkIn.formatstring.Contains("X"))
                    return "0x" + ((uint)(LinkIn.scalar * uintValue)).ToString("X2");
                else if (LinkIn.formatstring.Contains("b") || LinkIn.formatstring.Contains("B"))
                    return string.Concat("0b", (Convert.ToString(((uint)(uintValue * LinkIn.scalar)), 2)).PadLeft(8, '0'));
                else
                    return(uintValue * LinkIn.scalar).ToString(LinkIn.formatstring);
            }
                
            else if (DataType == typeof(int))
            {
                if (LinkIn.formatstring.Contains("x") || LinkIn.formatstring.Contains("X"))
                    return "0x" + ((int)(LinkIn.scalar * intValue)).ToString("X2");
                else if (LinkIn.formatstring.Contains("b") || LinkIn.formatstring.Contains("B"))
                    return string.Concat("0b", (Convert.ToString(((int)(intValue * LinkIn.scalar)), 2)).PadLeft(8, '0'));
                else
                    return (intValue * LinkIn.scalar).ToString(LinkIn.formatstring);
            }
                
            else if (DataType == typeof(float))
            {
                if (LinkIn.formatstring.Contains("x") || LinkIn.formatstring.Contains("X"))
                    return (floatValue * LinkIn.scalar).ToString();// "0x" + (LinkIn.scalar * floatValue).ToString("X2");
                else if (LinkIn.formatstring.Contains("b") || LinkIn.formatstring.Contains("B"))
                    return (floatValue * LinkIn.scalar).ToString();//string.Concat("0b", (Convert.ToString(((floatValue * LinkIn.scalar)), 2)).PadLeft(8, '0'));
                else
                    return (floatValue * LinkIn.scalar).ToString();
            }
                
            else if (DataType == typeof(double))
            {
                if (LinkIn.formatstring.Contains("x") || LinkIn.formatstring.Contains("X"))
                    return (setdoubleValue * LinkIn.scalar).ToString();//"0x" + (LinkIn.scalar * doubleValue).ToString("X2");
                else if (LinkIn.formatstring.Contains("b") || LinkIn.formatstring.Contains("B"))
                    return (setdoubleValue * LinkIn.scalar).ToString();//string.Concat("0b", (Convert.ToString(((doubleValue * LinkIn.scalar)), 2)).PadLeft(8, '0'));
                else
                    return (setdoubleValue * LinkIn.scalar).ToString();
            }
                

            return "";
        }
        public int ToSelectedIndex(GUIValueLinks LinkIn)
        {
            if (DataType == typeof(byte))
            {
                return ((byte)(byteValue * LinkIn.scalar));
            }

            else if (DataType == typeof(char))
            {
                return ((char)(charValue * LinkIn.scalar));
            }

            else if (DataType == typeof(ushort))
            {
                return ((ushort)(ushortValue * LinkIn.scalar));
            }

            else if (DataType == typeof(short))
            {
                return ((short)(shortValue * LinkIn.scalar));
            }

            else if (DataType == typeof(uint))
            {
                return (int)((uint)(uintValue * LinkIn.scalar));
            }

            else if (DataType == typeof(int))
            {
                return ((int)(intValue * LinkIn.scalar));
            }

            else if (DataType == typeof(float))
            {
                return ((int)(floatValue * LinkIn.scalar));
            }

            else if (DataType == typeof(double))
            {
                return ((int)(setdoubleValue * LinkIn.scalar));
            }
            return 0;
        }
        public double ToPlotDouble(GUIValueLinks LinkIn)
        {
            if (DataType == typeof(byte))
            {
                return (((double)byteValue * LinkIn.scalar));
            }

            else if (DataType == typeof(char))
            {
                return (((double)charValue * LinkIn.scalar));
            }

            else if (DataType == typeof(ushort))
            {
                return (((double)ushortValue * LinkIn.scalar));
            }

            else if (DataType == typeof(short))
            {
                return (((double)shortValue * LinkIn.scalar));
            }

            else if (DataType == typeof(uint))
            {
                return (((double)uintValue * LinkIn.scalar));
            }

            else if (DataType == typeof(int))
            {
                return (((double)intValue * LinkIn.scalar));
            }

            else if (DataType == typeof(float))
            {
                return (((double)floatValue * LinkIn.scalar));
            }

            else if (DataType == typeof(double))
            {
                return ((setdoubleValue * LinkIn.scalar));
            }
            return 0;
        }
        public int getLength()
        {
            return latchTimes.Count;
        }

        /// <summary>
        /// imsValueNode()
        /// </summary>
        /// <param name="globalNodeListIn"></param>
        /// <param name="nameString"></param>
        /// <param name="DataTypeIn"></param>
        /// <param name="ArrayLengthIn"></param>
        public imsValueNode(List<imsBaseNode> globalNodeListIn, string nameString, Type DataTypeIn, int ArrayLengthIn) :base(globalNodeListIn)
        {
            NodeType = typeof(imsValueNode);
            NodeName = nameString;
            DataType = DataTypeIn;
            ArrayLength = ArrayLengthIn;

            //FileStream deSerializeFs = new FileStream();
            //BinaryFormatter DeSerializeFormatter = new BinaryFormatter();

            if (ArrayLength < 2)
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

            latchTimes = new List<double>();
        }
        /// <summary>
        /// imsValueNode()
        /// </summary>
        /// <param name="DeSerializeFormatter"></param>
        /// <param name="deSerializeFs"></param>
        public imsValueNode(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs) :base(DeSerializeFormatter, deSerializeFs)
        {

        }


    }
}
