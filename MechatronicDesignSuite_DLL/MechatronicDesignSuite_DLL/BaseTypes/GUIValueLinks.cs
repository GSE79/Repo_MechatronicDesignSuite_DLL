using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MechatronicDesignSuite_DLL.BaseNodes;

namespace MechatronicDesignSuite_DLL.BaseTypes
{
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

            if(GUIFieldIn==null)
            {

            }
            else if (typeof(TextBox).IsInstanceOfType(GUIFieldIn))
            {
                ((TextBox)(GUIFieldIn)).Text = vNodeLink.toNameString(this);
                //StaticLinkedGUIField.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox_MouseDown);
                StaticLinkedGUIField.Enter += new EventHandler(textBox_Enter);
                StaticLinkedGUIField.Leave += new EventHandler(textbox_Leave);
                ((TextBox)(GUIFieldIn)).KeyPress += new KeyPressEventHandler(textbox_Keypress);
                ((TextBox)(GUIFieldIn)).ReadOnly = readOnlyIn;

                if (typeof(GroupBox).IsInstanceOfType(GUIFieldIn.Parent) && (GUIFieldIn.Dock == DockStyle.Fill || GUIFieldIn.Dock == DockStyle.Bottom))
                {
                    ((GroupBox)(GUIFieldIn.Parent)).Text = vNodeLink.getNodeName + " " + unitsstringin;
                    ((GroupBox)(GUIFieldIn.Parent)).MouseDown += new System.Windows.Forms.MouseEventHandler(textBox_MouseDown);
                    ((GroupBox)(GUIFieldIn.Parent)).MouseHover += new System.EventHandler(this.GroupBox_MouseHover);

                }
            }
            else if (typeof(System.Windows.Forms.Label).IsInstanceOfType(GUIFieldIn))
            {
                ((System.Windows.Forms.Label)(GUIFieldIn)).Text = vNodeLink.getNodeName + " " + unitsstringin;

                if (typeof(GroupBox).IsInstanceOfType(GUIFieldIn.Parent) && GUIFieldIn.Dock == DockStyle.Fill)
                {
                    ((GroupBox)(GUIFieldIn.Parent)).Text = vNodeLink.getNodeName + " " + unitsstringin;
                    ((GroupBox)(GUIFieldIn.Parent)).MouseDown += new System.Windows.Forms.MouseEventHandler(textBox_MouseDown);
                    ((GroupBox)(GUIFieldIn.Parent)).MouseHover += new System.EventHandler(this.GroupBox_MouseHover);

                }

            }
            else if (typeof(ComboBox).IsInstanceOfType(GUIFieldIn))
            {
                ((ComboBox)(GUIFieldIn)).Text = vNodeLink.getNodeName + " " + unitsstringin;
                //StaticLinkedGUIField.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox_MouseDown);
                StaticLinkedGUIField.Enter += new EventHandler(textBox_Enter);
                StaticLinkedGUIField.Leave += new EventHandler(textbox_Leave);
                ((ComboBox)(GUIFieldIn)).KeyPress += new KeyPressEventHandler(comboBox_KeyPress);
                ((ComboBox)(GUIFieldIn)).DropDown += new EventHandler(comboBox_DropDown);

                if (typeof(GroupBox).IsInstanceOfType(GUIFieldIn.Parent) && GUIFieldIn.Dock == DockStyle.Fill)
                {
                    ((GroupBox)(GUIFieldIn.Parent)).Text = vNodeLink.getNodeName + " " + unitsstringin;
                    ((GroupBox)(GUIFieldIn.Parent)).MouseDown += new System.Windows.Forms.MouseEventHandler(textBox_MouseDown);
                    ((GroupBox)(GUIFieldIn.Parent)).MouseHover += new System.EventHandler(this.GroupBox_MouseHover);

                }

            }
            else if (typeof(FlowLayoutPanel).IsInstanceOfType(GUIFieldIn))
            {
                StaticLinkedGUIField = new TextBox();
                StaticLinkedGUIField.Name = "imsTextBox" + vNodeLink.getGlobalNodeID.ToString();
                StaticLinkedGUIField.Parent = new GroupBox();

                StaticLinkedGUIField.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                StaticLinkedGUIField.Parent.Height = (int)(StaticLinkedGUIField.Size.Height * 2.0);


                StaticLinkedGUIField.Parent.Margin = new Padding(5, 2, 5, 2);
                StaticLinkedGUIField.Parent.Padding = new Padding(5, 2, 5, 2);
                StaticLinkedGUIField.Text = vNodeLink.toNameString(this);// + this.units;
                StaticLinkedGUIField.Parent.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

                StaticLinkedGUIField.Dock = DockStyle.Bottom;

                StaticLinkedGUIField.Parent.Visible = true;
                StaticLinkedGUIField.KeyPress += new KeyPressEventHandler(textbox_Keypress);

                ((TextBox)StaticLinkedGUIField).ReadOnly = readOnlyIn;
                ((FlowLayoutPanel)(GUIFieldIn)).Controls.Add(StaticLinkedGUIField.Parent);

                StaticLinkedGUIField.Enter += new EventHandler(textBox_Enter);
                StaticLinkedGUIField.Leave += new EventHandler(textbox_Leave);

                ((GroupBox)(StaticLinkedGUIField.Parent)).Text = vNodeLink.toNameString(this);
                ((GroupBox)(StaticLinkedGUIField.Parent)).MouseDown += new System.Windows.Forms.MouseEventHandler(textBox_MouseDown);
                ((GroupBox)(StaticLinkedGUIField.Parent)).MouseHover += new System.EventHandler(this.GroupBox_MouseHover);
            }
            else
                throw new Exception("Attempted to link "+vNodeLink.getNodeName+" to the unsupported control "+GUIFieldIn.ToString());
        }
        public GUIValueLinks(imsValueNode vNodeLinkIn, Control GUIFieldIn)
        {
            StaticLinkedGUIField = GUIFieldIn;
            vNodeLink = vNodeLinkIn;
            UnitsString = "";
            scalar = 1.0f;
            formatstring = "";
            readOnly = true;

            if (GUIFieldIn == null)
            {

            }
            else if (typeof(TextBox).IsInstanceOfType(GUIFieldIn))
            {
                ((TextBox)(GUIFieldIn)).Text = vNodeLink.toNameString(this);
                //StaticLinkedGUIField.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox_MouseDown);
                StaticLinkedGUIField.Enter += new EventHandler(textBox_Enter);
                StaticLinkedGUIField.Leave += new EventHandler(textbox_Leave);
                ((TextBox)(GUIFieldIn)).KeyPress += new KeyPressEventHandler(textbox_Keypress);
                ((TextBox)(GUIFieldIn)).ReadOnly = readOnly;

                if (typeof(GroupBox).IsInstanceOfType(GUIFieldIn.Parent) && (GUIFieldIn.Dock == DockStyle.Fill || GUIFieldIn.Dock == DockStyle.Bottom))
                {
                    ((GroupBox)(GUIFieldIn.Parent)).Text = vNodeLink.getNodeName + " " + UnitsString;
                    ((GroupBox)(GUIFieldIn.Parent)).MouseDown += new System.Windows.Forms.MouseEventHandler(textBox_MouseDown);
                    ((GroupBox)(GUIFieldIn.Parent)).MouseHover += new System.EventHandler(this.GroupBox_MouseHover);

                }
            }
            else if (typeof(System.Windows.Forms.Label).IsInstanceOfType(GUIFieldIn))
            {
                ((System.Windows.Forms.Label)(GUIFieldIn)).Text = vNodeLink.getNodeName + " " + UnitsString;

                if (typeof(GroupBox).IsInstanceOfType(GUIFieldIn.Parent) && GUIFieldIn.Dock == DockStyle.Fill)
                {
                    ((GroupBox)(GUIFieldIn.Parent)).Text = vNodeLink.getNodeName + " " + UnitsString;
                    ((GroupBox)(GUIFieldIn.Parent)).MouseDown += new System.Windows.Forms.MouseEventHandler(textBox_MouseDown);
                    ((GroupBox)(GUIFieldIn.Parent)).MouseHover += new System.EventHandler(this.GroupBox_MouseHover);

                }

            }
            else if (typeof(ComboBox).IsInstanceOfType(GUIFieldIn))
            {
                ((ComboBox)(GUIFieldIn)).Text = vNodeLink.getNodeName + " " + UnitsString;
                //StaticLinkedGUIField.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox_MouseDown);
                StaticLinkedGUIField.Enter += new EventHandler(textBox_Enter);
                StaticLinkedGUIField.Leave += new EventHandler(textbox_Leave);
                ((ComboBox)(GUIFieldIn)).KeyPress += new KeyPressEventHandler(comboBox_KeyPress);
                ((ComboBox)(GUIFieldIn)).DropDown += new EventHandler(comboBox_DropDown);

                if (typeof(GroupBox).IsInstanceOfType(GUIFieldIn.Parent) && GUIFieldIn.Dock == DockStyle.Fill)
                {
                    ((GroupBox)(GUIFieldIn.Parent)).Text = vNodeLink.getNodeName + " " + UnitsString;
                    ((GroupBox)(GUIFieldIn.Parent)).MouseDown += new System.Windows.Forms.MouseEventHandler(textBox_MouseDown);
                    ((GroupBox)(GUIFieldIn.Parent)).MouseHover += new System.EventHandler(this.GroupBox_MouseHover);

                }

            }
            else if (typeof(FlowLayoutPanel).IsInstanceOfType(GUIFieldIn))
            {
                StaticLinkedGUIField = new Label();
                StaticLinkedGUIField.Name = "imsTextBox" + vNodeLink.getGlobalNodeID.ToString();
                StaticLinkedGUIField.Parent = new GroupBox();

                //StaticLinkedGUIField.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                StaticLinkedGUIField.Parent.Height = (int)(StaticLinkedGUIField.Size.Height * 2.0);


                //StaticLinkedGUIField.Parent.Margin = new Padding(5, 2, 5, 2);
               // StaticLinkedGUIField.Parent.Padding = new Padding(5, 2, 5, 2);
                StaticLinkedGUIField.Text = vNodeLink.toNameString(this);// + this.units;
                //StaticLinkedGUIField.Parent.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                

                //StaticLinkedGUIField.Parent.Dock = DockStyle.Fill;

                StaticLinkedGUIField.Dock = DockStyle.Fill;
                ((Label)StaticLinkedGUIField).TextAlign = ContentAlignment.MiddleLeft;
                StaticLinkedGUIField.Parent.Visible = true;
                StaticLinkedGUIField.KeyPress += new KeyPressEventHandler(textbox_Keypress);

                ((FlowLayoutPanel)(GUIFieldIn)).Controls.Add(StaticLinkedGUIField.Parent);


                StaticLinkedGUIField.Enter += new EventHandler(textBox_Enter);
                StaticLinkedGUIField.Leave += new EventHandler(textbox_Leave);

                ((GroupBox)(StaticLinkedGUIField.Parent)).Text = vNodeLink.toNameString(this);
                ((GroupBox)(StaticLinkedGUIField.Parent)).MouseDown += new System.Windows.Forms.MouseEventHandler(textBox_MouseDown);
                ((GroupBox)(StaticLinkedGUIField.Parent)).MouseHover += new System.EventHandler(this.GroupBox_MouseHover);
            }
            else if(typeof(TableLayoutPanel).IsInstanceOfType(GUIFieldIn))
            {
                //((TableLayoutPanel)(GUIFieldIn)).ColumnCount = 1;
                //((TableLayoutPanel)(GUIFieldIn)).RowCount = ((TableLayoutPanel)(GUIFieldIn)).Controls.Count+1;

                StaticLinkedGUIField = new Label();
                StaticLinkedGUIField.Name = "imsTextBox" + vNodeLink.getGlobalNodeID.ToString();
                StaticLinkedGUIField.Parent = new GroupBox();
                StaticLinkedGUIField.Parent.Height = (int)(StaticLinkedGUIField.Size.Height * 2.0);
                StaticLinkedGUIField.Text = vNodeLink.toNameString(this);// + this.units;
                StaticLinkedGUIField.Dock = DockStyle.Fill;
                StaticLinkedGUIField.Parent.Dock = DockStyle.Fill;
                ((Label)StaticLinkedGUIField).TextAlign = ContentAlignment.MiddleLeft;
                StaticLinkedGUIField.Parent.Visible = true;

                ((TableLayoutPanel)(GUIFieldIn)).Controls.Add(StaticLinkedGUIField.Parent);

                StaticLinkedGUIField.Enter += new EventHandler(textBox_Enter);
                StaticLinkedGUIField.Leave += new EventHandler(textbox_Leave);

                ((GroupBox)(StaticLinkedGUIField.Parent)).Text = vNodeLink.toNameString(this);
                ((GroupBox)(StaticLinkedGUIField.Parent)).MouseDown += new System.Windows.Forms.MouseEventHandler(textBox_MouseDown);
                ((GroupBox)(StaticLinkedGUIField.Parent)).MouseHover += new System.EventHandler(this.GroupBox_MouseHover);
            }
            else
                throw new Exception("Attempted to link " + vNodeLink.getNodeName + " to the unsupported control " + GUIFieldIn.ToString());
        }

        public void textbox_Keypress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (!readOnly)
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
                vNodeLink.HoldRefresh = true;

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
                vNodeLink.HoldRefresh = true;
        }
        private void comboBox_DropDown(object sender, EventArgs e)
        {
            vNodeLink.HoldRefresh = true;
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
                        vNodeLink.getNodeName,
                        " ", UnitsString,
                        "\n  GuiFeild: ", StaticLinkedGUIField.Name,
                        "\n  DataType: ", vNodeLink.getDataType.ToString(),
                        "\n  ArrayLength: ", vNodeLink.getArrayLength.ToString(),
                        "\n  Size: ", vNodeLink.getDataSize.ToString(), " (bytes)",
                        "\n  Scalar: ", scalar.ToString(),
                        "\n  Format String: ", formatstring);

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
            vNodeLink.HoldRefresh = true;
        }
        public void textbox_Leave(object sender, EventArgs e)
        {
            vNodeLink.HoldRefresh = false;
        }
        public void getValuefromTextbox(TextBox tbIn)
        {
            float tempFloat;
            float tempScalar = 1.0f/scalar;
            

            if (vNodeLink.getDataType == typeof(byte))
            {
                if (float.TryParse(tbIn.Text, out tempFloat))
                    vNodeLink.setbyteValue = (byte)(tempFloat * tempScalar);
            }
            else if (vNodeLink.getDataType == typeof(char))
            {
                if(vNodeLink.getArrayLength>1)
                {
                    vNodeLink.getcharValues.Clear();
                    foreach (char c in tbIn.Text)
                        vNodeLink.getcharValues.Add(c);
                }
                else
                {
                    char tempChar;
                    if (char.TryParse(tbIn.Text, out tempChar))
                        vNodeLink.setcharValue = (char)(tempChar * tempScalar);
                    else if (float.TryParse(tbIn.Text, out tempFloat))
                        vNodeLink.setcharValue = (char)(tempFloat * tempScalar);
                }
            }
            else if (vNodeLink.getDataType == typeof(ushort))
            {
                if (float.TryParse(tbIn.Text, out tempFloat))
                    vNodeLink.setushortValue = (ushort)(tempFloat * tempScalar);
            }
            else if (vNodeLink.getDataType == typeof(short))
            {
                if (float.TryParse(tbIn.Text, out tempFloat))
                    vNodeLink.setshortValue = (short)(tempFloat * tempScalar);
            }
            else if (vNodeLink.getDataType == typeof(uint))
            {
                if (float.TryParse(tbIn.Text, out tempFloat))
                    vNodeLink.setuintValue = (uint)(tempFloat * tempScalar);
            }
            else if (vNodeLink.getDataType == typeof(int))
            {
                if (float.TryParse(tbIn.Text, out tempFloat))
                    vNodeLink.setintValue = (int)(tempFloat * tempScalar);
            }
            else if (vNodeLink.getDataType == typeof(float))
            {
                if (float.TryParse(tbIn.Text, out tempFloat))
                    vNodeLink.setfloatValue = (tempFloat * tempScalar);
            }
            else if (vNodeLink.getDataType == typeof(double))
            {
                double tempDouble;
                if (double.TryParse(tbIn.Text, out tempDouble))
                    vNodeLink.setdoubleValue = (tempDouble * tempScalar);
            }

            if (typeof(imsSerialParamData).IsInstanceOfType(vNodeLink))
            {
                ((imsSerialParamData)(vNodeLink)).packageToSerialData();
            }

        }
        public void getValuefromComboBox(ComboBox cbIn)
        {
            float tempFloat;
            float tempScalar = 1.0f / scalar;

            if (vNodeLink.getDataType == typeof(byte))
            {
                vNodeLink.setbyteValue = (byte)(cbIn.SelectedIndex * tempScalar);
            }
            else if (vNodeLink.getDataType == typeof(char))
            {
                vNodeLink.setcharValue = (char)(cbIn.SelectedIndex * tempScalar);
            }
            else if (vNodeLink.getDataType == typeof(ushort))
            {
                vNodeLink.setushortValue = (ushort)(cbIn.SelectedIndex * tempScalar);
            }
            else if (vNodeLink.getDataType == typeof(short))
            {
                vNodeLink.setshortValue = (short)(cbIn.SelectedIndex * tempScalar);
            }
            else if (vNodeLink.getDataType == typeof(uint))
            {
                vNodeLink.setuintValue = (uint)(cbIn.SelectedIndex * tempScalar);
            }
            else if (vNodeLink.getDataType == typeof(int))
            {
                vNodeLink.setintValue = (int)(cbIn.SelectedIndex * tempScalar);
            }
            else if (vNodeLink.getDataType == typeof(float))
            {
                vNodeLink.setfloatValue = (cbIn.SelectedIndex * tempScalar);
            }
            else if (vNodeLink.getDataType == typeof(double))
            {
                vNodeLink.setdoubleValue = (cbIn.SelectedIndex * tempScalar);
            }

            if (typeof(imsSerialParamData).IsInstanceOfType(vNodeLink))
            {
                ((imsSerialParamData)(vNodeLink)).packageToSerialData();
            }
        }
    }

}
