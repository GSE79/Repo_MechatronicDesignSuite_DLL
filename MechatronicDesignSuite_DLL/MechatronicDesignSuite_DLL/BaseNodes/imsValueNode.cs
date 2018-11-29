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
        public int  getArrayLength {get{return ArrayLength;} }
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
        public ushort setushortValue{ get { return ushortValue; } set { ushortValue = value; } }
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
        public double setfdoubleValue { get { return doubleValue; } set { doubleValue = value; } }
        [Category("datatype: double"), Description("The .NET double representation")]
        public List<double> getdoubleValues { get { return doubleValues; } }
        [Category("datatype: double"), Description("The .NET double representation")]
        public List<double> setdoubleValues { get { return doubleValues; } set { doubleValues = value; } }

        /// <summary>
        /// latchTimes
        /// </summary>
        protected List<DateTime> latchTimes;

        protected bool newData = false;
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

        public class GUIValueLinks
        {
            public Control StaticLinkedGUIField;
            public string UnitsString;
            public float scalar;
            public string formatstring;
            public bool readOnly;
            public imsValueNode vNodeLink;
            public ToolTip GroupBoxToolTip = new ToolTip();

            public GUIValueLinks(imsValueNode vNodeLinkIn, Control GUIFieldIn, string unitsstringin, float scalarin, string formatstringin, bool readOnlyIn)
            {
                StaticLinkedGUIField = GUIFieldIn;
                vNodeLink = vNodeLinkIn;                
                UnitsString = unitsstringin;
                scalar = scalarin;
                formatstring = formatstringin;
                readOnly = readOnlyIn;

                if (typeof(TextBox).IsInstanceOfType(GUIFieldIn))
                {
                    ((TextBox)(GUIFieldIn)).Text = vNodeLink.NodeName + " " + unitsstringin;
                    //StaticLinkedGUIField.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox_MouseDown);
                    StaticLinkedGUIField.Enter += new EventHandler(textBox_Enter);
                    StaticLinkedGUIField.Leave += new EventHandler(textbox_Leave);
                    ((TextBox)(GUIFieldIn)).KeyPress += new KeyPressEventHandler(textbox_Keypress);
                    ((TextBox)(GUIFieldIn)).ReadOnly = readOnlyIn;

                    if (typeof(GroupBox).IsInstanceOfType(GUIFieldIn.Parent) && GUIFieldIn.Dock == DockStyle.Fill)
                    {
                        ((GroupBox)(GUIFieldIn.Parent)).Text = vNodeLink.NodeName + " " + unitsstringin;
                        ((GroupBox)(GUIFieldIn.Parent)).MouseDown += new System.Windows.Forms.MouseEventHandler(textBox_MouseDown);
                        ((GroupBox)(GUIFieldIn.Parent)).MouseHover += new System.EventHandler(this.GroupBox_MouseHover);

                    }

                }
                else if (typeof(System.Windows.Forms.Label).IsInstanceOfType(GUIFieldIn))
                {
                    ((System.Windows.Forms.Label)(GUIFieldIn)).Text = vNodeLink.NodeName + " " + unitsstringin;

                    if (typeof(GroupBox).IsInstanceOfType(GUIFieldIn.Parent) && GUIFieldIn.Dock == DockStyle.Fill)
                    {
                        ((GroupBox)(GUIFieldIn.Parent)).Text = vNodeLink.NodeName + " " + unitsstringin;
                        ((GroupBox)(GUIFieldIn.Parent)).MouseDown += new System.Windows.Forms.MouseEventHandler(textBox_MouseDown);
                        ((GroupBox)(GUIFieldIn.Parent)).MouseHover += new System.EventHandler(this.GroupBox_MouseHover);

                    }

                }
                else if (typeof(ComboBox).IsInstanceOfType(GUIFieldIn))
                {
                    ((ComboBox)(GUIFieldIn)).Text = vNodeLink.NodeName + " " + unitsstringin;
                    //StaticLinkedGUIField.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox_MouseDown);
                    StaticLinkedGUIField.Enter += new EventHandler(textBox_Enter);
                    StaticLinkedGUIField.Leave += new EventHandler(textbox_Leave);
                    ((ComboBox)(GUIFieldIn)).KeyPress += new KeyPressEventHandler(comboBox_KeyPress);
                    ((ComboBox)(GUIFieldIn)).DropDown += new EventHandler(comboBox_DropDown);

                    if (typeof(GroupBox).IsInstanceOfType(GUIFieldIn.Parent) && GUIFieldIn.Dock == DockStyle.Fill)
                    {
                        ((GroupBox)(GUIFieldIn.Parent)).Text = vNodeLink.NodeName + " " + unitsstringin;
                        ((GroupBox)(GUIFieldIn.Parent)).MouseDown += new System.Windows.Forms.MouseEventHandler(textBox_MouseDown);
                        ((GroupBox)(GUIFieldIn.Parent)).MouseHover += new System.EventHandler(this.GroupBox_MouseHover);

                    }

                }
            }
            public void textbox_Keypress(object sender, KeyPressEventArgs e)
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true;
                    if(!readOnly)
                        getValuefromTextbox((TextBox)sender);

                    //// Make sure its been read once prior to writing
                    //if (this.ByteOffset > 3)
                    //{
                    //    // Initialize Common Variables
                    //    TxSerialPacketHeader WriteHeader;

                    //    // Get bytes from text string
                    //    getDataBytesFromText(((TextBox)sender).Text);

                    //    // Package Headers
                    //    WriteHeader = new TxSerialPacketHeader();
                    //    WriteHeader.MsgID = this.MessageID;
                    //    WriteHeader.MsgType = this.WriteDType;
                    //    WriteHeader.SMOffset = (byte)(this.ByteOffset - 4);
                    //    WriteHeader.ArrayData = new List<byte>();
                    //    if (this.SPDParamInputs.DataType == typeof(byte[]))
                    //    {
                    //        WriteHeader.Byte0 = 0x00;
                    //        WriteHeader.Byte1 = 0x00;
                    //        WriteHeader.Byte2 = 0x00;
                    //        WriteHeader.Byte3 = 0x00;
                    //        if (SerialDataOut.Count > 0)
                    //            WriteHeader.Byte0 = SerialDataOut[0];
                    //        if (SerialDataOut.Count > 1)
                    //            WriteHeader.Byte1 = SerialDataOut[1];
                    //        if (SerialDataOut.Count > 2)
                    //            WriteHeader.Byte2 = SerialDataOut[2];
                    //        if (SerialDataOut.Count > 3)
                    //            WriteHeader.Byte3 = SerialDataOut[3];
                    //        if (SerialDataOut.Count > 4)
                    //            for (int i = 0; i < SerialDataOut.Count - 4; i++)
                    //                WriteHeader.ArrayData.Add(SerialDataOut[4 + i]);
                    //    }
                    //    else
                    //    {
                    //        WriteHeader.Byte0 = SerialDataOut[0];
                    //        if (WriteHeader.MsgType == NGPOCconstants.DTYPE_WRITE16 || WriteHeader.MsgType == NGPOCconstants.DTYPE_WRITE32)
                    //            WriteHeader.Byte1 = SerialDataOut[1];
                    //        if (WriteHeader.MsgType == NGPOCconstants.DTYPE_WRITE32)
                    //        {
                    //            WriteHeader.Byte2 = SerialDataOut[2];
                    //            WriteHeader.Byte3 = SerialDataOut[3];
                    //        }
                    //    }



                    //    this.AddtoCMDQue(WriteHeader);

                    //}
                    textbox_Leave(null, null);
                }
                else
                    vNodeLink.holdRefresh = true;

            }
            public void comboBox_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true;
                    if (!readOnly)
                        getValuefromComboBox((ComboBox)sender);
                    // Make sure its been read once prior to writing
                    //if (this.ByteOffset > 3)
                    //{
                    //    // Initialize Common Variables
                    //    TxSerialPacketHeader WriteHeader;

                        //    // Get bytes from text string

                        //    if (this.SPDParamInputs.name == "Alarm Details")
                        //    {
                        //        getDataBytesFromText(((ushort)((AlarmCodes)(Enum.Parse(typeof(AlarmCodes), ((ComboBox)this.GUILinks.guiField).SelectedValue.ToString())))).ToString());
                        //    }
                        //    else
                        //        getDataBytesFromText(((ComboBox)this.GUILinks.guiField).SelectedIndex.ToString());

                        //    // Package Headers
                        //    WriteHeader = new TxSerialPacketHeader();
                        //    WriteHeader.MsgID = this.MessageID;
                        //    WriteHeader.MsgType = this.WriteDType;
                        //    WriteHeader.SMOffset = (byte)(this.ByteOffset - 4);

                        //    WriteHeader.Byte0 = SerialDataOut[0];
                        //    if (WriteHeader.MsgType == NGPOCconstants.DTYPE_WRITE16 || WriteHeader.MsgType == NGPOCconstants.DTYPE_WRITE32)
                        //        WriteHeader.Byte1 = SerialDataOut[1];
                        //    if (WriteHeader.MsgType == NGPOCconstants.DTYPE_WRITE32)
                        //    {
                        //        WriteHeader.Byte2 = SerialDataOut[2];
                        //        WriteHeader.Byte3 = SerialDataOut[3];
                        //    }

                        //    this.AddtoCMDQue(WriteHeader);

                        //}
                    textbox_Leave(null, null);
                }
                else
                    vNodeLink.holdRefresh = true;


            }
            private void comboBox_DropDown(object sender, EventArgs e)
            {
                vNodeLink.holdRefresh = true;
            }
            private void textBox_MouseDown(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    DragDropEffects RetEffects = StaticLinkedGUIField.DoDragDrop(this, DragDropEffects.Link);
                }


            }
            public void GroupBox_MouseHover(object sender, EventArgs e)
            {
                string toolTipText = string.Concat(
                            vNodeLink.NodeName,
                            " ", UnitsString,
                            "\n  GuiFeild: ", StaticLinkedGUIField.Name,
                            "\n  DataType: ", vNodeLink.DataType.ToString(),
                            "\n  Size: ", vNodeLink.getDataSize.ToString(), " (bytes)",
                            "\n  Scalar: ", scalar.ToString(),
                            "\n  Format String: ", formatstring );

                if (typeof(imsSerialParamData).IsInstanceOfType(vNodeLink))
                {
                    imsSerialParamData thisSPD = (imsSerialParamData)vNodeLink;
                    string Sin = "", Sout = "";
                    int i = 0;

                    if (thisSPD.getSerialDataIn != null)
                        for (i = thisSPD.getSerialDataIn.Count - 1; i >= 0; i--)
                            Sin += thisSPD.getSerialDataIn[i].ToString("X2");
                    if (thisSPD.getSerialDataOut != null)
                        for (i = thisSPD.getSerialDataOut.Count - 1; i >= 0; i--)
                            Sout += thisSPD.getSerialDataOut[i].ToString("X2");
                    toolTipText = string.Concat(toolTipText,
                                "\n\nSerial Data In: 0x", Sin,
                                "\nSerial Data Out: 0x", Sout);
                }




                GroupBoxToolTip.AutoPopDelay = 32000;
                GroupBoxToolTip.SetToolTip(((GroupBox)(sender)), toolTipText);
                            

            }
            private void textBox_Enter(object sender, EventArgs e)
            {
                vNodeLink.holdRefresh = true;
            }
            public void textbox_Leave(object sender, EventArgs e)
            {
                vNodeLink.holdRefresh = false;
            }
            public void getValuefromTextbox(TextBox tbIn)
            {
                float tempFloat;

                if (vNodeLink.DataType == typeof(byte))
                {
                    if (float.TryParse(tbIn.Text, out tempFloat))
                        vNodeLink.byteValue = (byte)(tempFloat / scalar);                                            
                }
                else if (vNodeLink.DataType == typeof(char))
                {
                    char tempChar;
                    if (char.TryParse(tbIn.Text, out tempChar))
                        vNodeLink.charValue = (char)(tempChar / scalar);
                    else if(float.TryParse(tbIn.Text, out tempFloat))
                        vNodeLink.charValue = (char)(tempFloat / scalar);
                }
                else if (vNodeLink.DataType == typeof(ushort))
                {
                    if (float.TryParse(tbIn.Text, out tempFloat))
                        vNodeLink.ushortValue = (ushort)(tempFloat / scalar);
                }
                else if (vNodeLink.DataType == typeof(short))
                {
                    if (float.TryParse(tbIn.Text, out tempFloat))
                        vNodeLink.shortValue = (short)(tempFloat / scalar);
                }
                else if (vNodeLink.DataType == typeof(uint))
                {
                    if (float.TryParse(tbIn.Text, out tempFloat))
                        vNodeLink.uintValue = (uint)(tempFloat / scalar);
                }
                else if (vNodeLink.DataType == typeof(int))
                {
                    if (float.TryParse(tbIn.Text, out tempFloat))
                        vNodeLink.intValue = (int)(tempFloat / scalar);
                }
                else if (vNodeLink.DataType == typeof(float))
                {
                    if (float.TryParse(tbIn.Text, out tempFloat))
                        vNodeLink.floatValue = (tempFloat / scalar);
                }
                else if (vNodeLink.DataType == typeof(double))
                {
                    double tempDouble;
                    if (double.TryParse(tbIn.Text, out tempDouble))
                        vNodeLink.doubleValue = (tempDouble / scalar);
                }

                if(typeof(imsSerialParamData).IsInstanceOfType(vNodeLink))
                {
                    ((imsSerialParamData)(vNodeLink)).packageToSerialData();
                }

            }
            public void getValuefromComboBox(ComboBox cbIn)
            {
                float tempFloat;

                if (vNodeLink.DataType == typeof(byte))
                {
                    vNodeLink.byteValue = (byte)(cbIn.SelectedIndex / scalar);
                }
                else if (vNodeLink.DataType == typeof(char))
                {
                    vNodeLink.charValue = (char)(cbIn.SelectedIndex / scalar);
                }
                else if (vNodeLink.DataType == typeof(ushort))
                {
                    vNodeLink.ushortValue = (ushort)(cbIn.SelectedIndex / scalar);
                }
                else if (vNodeLink.DataType == typeof(short))
                {
                    vNodeLink.shortValue = (short)(cbIn.SelectedIndex / scalar);
                }
                else if (vNodeLink.DataType == typeof(uint))
                {
                    vNodeLink.uintValue = (uint)(cbIn.SelectedIndex / scalar);
                }
                else if (vNodeLink.DataType == typeof(int))
                {
                    vNodeLink.intValue = (int)(cbIn.SelectedIndex / scalar);
                }
                else if (vNodeLink.DataType == typeof(float))
                {
                    vNodeLink.floatValue = (cbIn.SelectedIndex / scalar);
                }
                else if (vNodeLink.DataType == typeof(double))
                {
                    vNodeLink.doubleValue = (cbIn.SelectedIndex / scalar);
                }

                if (typeof(imsSerialParamData).IsInstanceOfType(vNodeLink))
                {
                    ((imsSerialParamData)(vNodeLink)).packageToSerialData();
                }
            }
        }

        protected List<GUIValueLinks> StaticGUILinks = new List<GUIValueLinks>();
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
                if (LinkIn.formatstring.Contains("x") || LinkIn.formatstring.Contains("X"))
                    return "0x" + ((byte)(LinkIn.scalar*charValue)).ToString("X2");
                else if (LinkIn.formatstring.Contains("b") || LinkIn.formatstring.Contains("B"))
                    return string.Concat("0b", (Convert.ToString(((char)(charValue * LinkIn.scalar)), 2)).PadLeft(8, '0'));
                else
                    return ((char)(charValue * LinkIn.scalar)).ToString();
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
                    return (doubleValue * LinkIn.scalar).ToString();//"0x" + (LinkIn.scalar * doubleValue).ToString("X2");
                else if (LinkIn.formatstring.Contains("b") || LinkIn.formatstring.Contains("B"))
                    return (doubleValue * LinkIn.scalar).ToString();//string.Concat("0b", (Convert.ToString(((doubleValue * LinkIn.scalar)), 2)).PadLeft(8, '0'));
                else
                    return (doubleValue * LinkIn.scalar).ToString();
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
                return ((int)(doubleValue * LinkIn.scalar));
            }
            return 0;
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

            if (ArrayLength <= 0)
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
