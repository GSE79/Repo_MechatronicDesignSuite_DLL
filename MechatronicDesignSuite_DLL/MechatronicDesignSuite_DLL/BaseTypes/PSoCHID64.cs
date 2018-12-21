using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using MechatronicDesignSuite_DLL;
using MechatronicDesignSuite_DLL.BaseTypes;
using MechatronicDesignSuite_DLL.BaseNodes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using CyUSB;

namespace MechatronicDesignSuite_DLL.BaseTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class PSoCHID64
    {
        string[] CYACDLines;
        uint JTAGIDfile, JTAGID, BLVER;
        byte DEVREV;
        public string fullPathCyacd;

        CyUSB.USBDeviceList usbDevices = null;        // Pointer to list of USB devices
        public CyUSB.CyHidDevice myHidDevice = null;         // Handle of USB device
        public CyUSB.CyHidDevice myHidDevice_FW = null;         // Handle of USB device
        public CyUSB.CyHidDevice myHidDevice_BL = null;         // Handle of USB device
        public static int Vendor_ID = 0x04B4;                       // Cypress Vendor ID 
        public static int Product_ID_FW = 0xE177;                       // Example Project Product ID
        public static int Product_ID_BL = 0xB71D;                       // Example Project Product ID
        public bool useCRC_ChkSum = false;
        public bool blRespMaster = false;
        public bool newData = false;
        // Bytes Rx'd from USB
        public List<byte> USBRxBytes = new List<byte>();
        // Bytes Tx'ing to USB
        public List<byte> USBTxBytes = new List<byte>();

        public void setPathString(string cyacdFullPath)
        {
            fullPathCyacd = cyacdFullPath;
        }
        public void setupUSBComms()
        {
            // Create a list of CYUSB devices for this application
            usbDevices = new CyUSB.USBDeviceList(CyUSB.CyConst.DEVICES_HID);

            //Add event handlers for device attachment and device removal
            usbDevices.DeviceAttached += new EventHandler(usbDevices_DeviceAttached);
            usbDevices.DeviceRemoved += new EventHandler(usbDevices_DeviceRemoved);

            //Connect to the USB device
            GetDevice();
        }

        public void CyclicHIDRead()
        {
            int i = 0;

            // "Clear" Input Buffer   
            for (i = 1; i < myHidDevice_FW.Inputs.DataBuf.Length - 1; i++)
                myHidDevice_FW.Inputs.DataBuf[i] = 0xff;

            // Always Read USB HID Data
            myHidDevice_FW.ReadInput();

            USBRxBytes.Clear();
            if (myHidDevice_FW.Inputs.DataBuf[1] != 0xff)
                for (i = 1; i < myHidDevice_FW.Inputs.DataBuf.Length - 1; i++)
                    USBRxBytes.Add(myHidDevice_FW.Inputs.DataBuf[i]);

            if (USBRxBytes.Count > 3)
                newData = true;
        }

        public void CyclicHIDWrite()
        {
            if (USBTxBytes.Count > 0)
            {
                int i = 1;
                // "Clear" Input Buffer   
                for (i = 1; i < myHidDevice_FW.Outputs.DataBuf.Length - 1; i++)
                    myHidDevice_FW.Outputs.DataBuf[i] = 0xff;

                i = 1;
                foreach (byte txByte in USBTxBytes)
                    myHidDevice_FW.Outputs.DataBuf[i++] = txByte;
            }

            switch (myHidDevice_FW.Outputs.DataBuf[1])
            {
                default:
                    break;
            }

            // Always Write USB HID Data
            myHidDevice_FW.WriteOutput();



        }

        public static void BlockingBLUSBManager(string fullPathCyacd, BackgroundWorker BLCommThread)
        {
            CyHidDevice myHidDevice_BLMgr = null;         // Handle of USB device
            USBDeviceList usbDevices_BLMgr = new USBDeviceList(CyConst.DEVICES_HID);        // Pointer to list of USB devices
            uint JTAGIDMgr, JTAGIDfileMgr, BLVERMgr;
            byte DEVREVMgr;
            string[] CYACDLinesMgr;
            BLCommThread.WorkerReportsProgress = true;

            myHidDevice_BLMgr = usbDevices_BLMgr[Vendor_ID, Product_ID_BL] as CyHidDevice;
            if (myHidDevice_BLMgr != null)
            {
                myHidDevice_BLMgr = usbDevices_BLMgr[Vendor_ID, Product_ID_BL] as CyHidDevice;
            }
            else
                return;

            ///////////////////////////////////////////////////////////////////////////
            // Prepare and Send
            #region // --Enter Bootloader Command
            List<byte> cmdBuffer = new List<byte>();
            List<byte> rspBuffer = new List<byte>();

            // Build Enter Bootloader Packet
            CreateEnterBootLoaderCmd(ref cmdBuffer);

            // ____----____----Send the packet
            //First Byte in buffer holds the Report ID
            myHidDevice_BLMgr.Outputs.DataBuf[0] = myHidDevice_BLMgr.Outputs.ID;

            //Load 64 bytes of data
            for (int i = 1; i <= cmdBuffer.Count; i++)
                myHidDevice_BLMgr.Outputs.DataBuf[i] = cmdBuffer[i - 1];

            // Write to HID output buffer
            myHidDevice_BLMgr.WriteOutput();

            // Read from HID input buffer
            myHidDevice_BLMgr.ReadInput();

            // Parse the response
            rspBuffer.Clear();
            foreach (byte b in myHidDevice_BLMgr.Inputs.DataBuf)
                rspBuffer.Add(b);

            if (rspBuffer.Count != 0 && rspBuffer[2] == 0x00)
            {
                BLCommThread.ReportProgress(0);

                JTAGIDMgr = rspBuffer[5];
                JTAGIDMgr |= (uint)rspBuffer[6] << 8;
                JTAGIDMgr |= (uint)rspBuffer[7] << 16;
                JTAGIDMgr |= (uint)rspBuffer[8] << 24;
                DEVREVMgr = rspBuffer[9];
                BLVERMgr = rspBuffer[10];
                BLVERMgr |= (uint)rspBuffer[11] << 8;
                BLVERMgr |= (uint)rspBuffer[12] << 16;

                CYACDLinesMgr = File.ReadAllLines(fullPathCyacd);
                JTAGIDfileMgr = uint.Parse(CYACDLinesMgr[0].Substring(0, 8), System.Globalization.NumberStyles.HexNumber);

                ///////////////////////////////////////////////////////////////////////////
                // Prepare and Send
                // --Programming Actions
                if (JTAGIDMgr == JTAGIDfileMgr)
                {
                    byte ArrayID = 0;
                    ushort RowNum = 0;
                    ushort DataLen = 0;
                    byte rowChkSum = 0;
                    byte rowChkSumVerify = 0;
                    int rowCnt = 0;

                    // Row Data - Data Length bytes - 2*datalength ascii chars
                    List<byte[]> RowDataBytes = new List<byte[]>();
                    List<string> CyacdLines = CYACDLinesMgr.ToList();
                    CyacdLines.RemoveAt(0);

                    foreach (string line in CyacdLines)
                    {
                        #region // parse
                        // Array ID - 1 byte - 2 ascii chars
                        ArrayID = byte.Parse(line.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);

                        // Row Number - 2 bytes - 4 ascii chars
                        RowNum = ushort.Parse(line.Substring(3, 4), System.Globalization.NumberStyles.HexNumber);

                        // Data Length - 2 bytes- 4 ascii chars
                        DataLen = ushort.Parse(line.Substring(7, 4), System.Globalization.NumberStyles.HexNumber);

                        // Row Data - Data Length bytes - 2*datalength ascii chars
                        RowDataBytes.Clear();
                        RowDataBytes = ASCIIHexByteArray2Bytes(line.Substring(11, 2 * DataLen));

                        // Row Checksum
                        rowChkSum = byte.Parse(line.Substring(11 + 2 * DataLen, 2), System.Globalization.NumberStyles.HexNumber);

                        #endregion

                        #region  // erase
                        // Build Erase Row Packet
                        CreateEraseRowCmd(ref cmdBuffer, ArrayID, RowNum);

                        // ____----____----Send the packet
                        //First Byte in buffer holds the Report ID
                        myHidDevice_BLMgr.Outputs.DataBuf[0] = myHidDevice_BLMgr.Outputs.ID;

                        //Load 64 bytes of data
                        for (int i = 1; i <= cmdBuffer.Count; i++)
                            myHidDevice_BLMgr.Outputs.DataBuf[i] = cmdBuffer[i - 1];

                        // Write to HID output buffer
                        myHidDevice_BLMgr.WriteOutput();

                        // Read from HID input buffer
                        myHidDevice_BLMgr.ReadInput();

                        // Parse the response
                        rspBuffer.Clear();
                        foreach (byte b in myHidDevice_BLMgr.Inputs.DataBuf)
                            rspBuffer.Add(b);

                        if (rspBuffer.Count == 0)
                        { MessageBox.Show("Failure of Erase Row Command - no response bytes"); break; }
                        if (rspBuffer[2] != 0x00)
                        { MessageBox.Show("Failure of Erase Row Command - " + rspBuffer[2].ToString("X2")); break; }

                        #endregion

                        #region  // send data

                        // Buffer Row on Master PSoC
                        foreach (byte[] rowDat in RowDataBytes)
                        {
                            // Build Send Data Packet
                            CreateSendDataCmd(ref cmdBuffer, rowDat);

                            // ____----____----Send the packet
                            //First Byte in buffer holds the Report ID
                            myHidDevice_BLMgr.Outputs.DataBuf[0] = myHidDevice_BLMgr.Outputs.ID;

                            //Load data
                            for (int i = 1; i <= cmdBuffer.Count; i++)
                                myHidDevice_BLMgr.Outputs.DataBuf[i] = cmdBuffer[i - 1];

                            // Write to HID output buffer
                            myHidDevice_BLMgr.WriteOutput();

                            // Read from HID input buffer
                            myHidDevice_BLMgr.ReadInput();

                            // Parse the response
                            rspBuffer.Clear();
                            foreach (byte b in myHidDevice_BLMgr.Inputs.DataBuf)
                                rspBuffer.Add(b);

                            if (rspBuffer.Count == 0)
                            { MessageBox.Show("Failure of Send Data Commands - no response bytes"); break; }
                            if (rspBuffer[2] != 0x00)
                            { MessageBox.Show("Failure of Send Data Commands - " + rspBuffer[2].ToString("X2")); break; }

                        }
                        if (rspBuffer.Count == 0)
                        { MessageBox.Show("Failure of Send Data Commands Detected - no response bytes"); break; }
                        if (rspBuffer[2] != 0x00)
                        { MessageBox.Show("Failure of Send Data Commands Detected- " + rspBuffer[2].ToString("X2")); break; }

                        #endregion+		new System.Collections.Generic.Mscorlib_CollectionDebugView<byte[]>(RowDataBytes).Items[8]	{byte[0x00000020]}	byte[]

                        #region  // program
                        // Build Program Row Packet
                        CreateProgramRowCmd(ref cmdBuffer, ArrayID, RowNum);

                        // ____----____----Send the packet
                        //First Byte in buffer holds the Report ID
                        myHidDevice_BLMgr.Outputs.DataBuf[0] = myHidDevice_BLMgr.Outputs.ID;

                        //Load 64 bytes of data
                        for (int i = 1; i <= cmdBuffer.Count; i++)
                            myHidDevice_BLMgr.Outputs.DataBuf[i] = cmdBuffer[i - 1];

                        // Write to HID output buffer
                        myHidDevice_BLMgr.WriteOutput();

                        // Read from HID input buffer
                        myHidDevice_BLMgr.ReadInput();

                        // Parse the response
                        rspBuffer.Clear();
                        foreach (byte b in myHidDevice_BLMgr.Inputs.DataBuf)
                            rspBuffer.Add(b);

                        if (rspBuffer.Count == 0)
                        { MessageBox.Show("Failure of Program Row Command - no response bytes"); break; }
                        if (rspBuffer[2] != 0x00)
                        { MessageBox.Show("Failure of Program Row Command - " + rspBuffer[2].ToString("X2")); break; }

                        #endregion

                        #region  // verify
                        rowChkSumVerify = (byte)(rowChkSum + ArrayID + RowNum + (RowNum >> 8) + DataLen + (DataLen >> 8));

                        // Build Verify Row Packet
                        CreateVerifyRowCmd(ref cmdBuffer, ArrayID, RowNum, rowChkSumVerify);

                        // ____----____----Send the packet
                        //First Byte in buffer holds the Report ID
                        myHidDevice_BLMgr.Outputs.DataBuf[0] = myHidDevice_BLMgr.Outputs.ID;

                        //Load 64 bytes of data
                        for (int i = 1; i <= cmdBuffer.Count; i++)
                            myHidDevice_BLMgr.Outputs.DataBuf[i] = cmdBuffer[i - 1];

                        // Write to HID output buffer
                        myHidDevice_BLMgr.WriteOutput();

                        // Read from HID input buffer
                        myHidDevice_BLMgr.ReadInput();

                        // Parse the response
                        rspBuffer.Clear();
                        foreach (byte b in myHidDevice_BLMgr.Inputs.DataBuf)
                            rspBuffer.Add(b);

                        if (rspBuffer[2] != 0x00)
                        { MessageBox.Show("Failure of Verify Row CheckSum Command - " + rspBuffer[2].ToString("X2")); break; }
                        if (rspBuffer[5] != rowChkSumVerify)
                        { MessageBox.Show("Failure of Verify Row CheckSum Command - Failed CheckSum\nExpecting " + rowChkSumVerify.ToString("X") + " Received " + rspBuffer[5].ToString("X") + "\nArrayID: " + ArrayID.ToString("X") + " RowNum: " + RowNum.ToString("X")); break; }
                        //else
                        //{ MessageBox.Show("Verify Row CheckSum - Passed CheckSum\nExpecting " + rowChkSumVerify.ToString("X") + " Received " + rspBuffer[5].ToString("X") + "\nArrayID: " + ArrayID.ToString("X") + " RowNum: " + RowNum.ToString("X")); }
                        #endregion

                        BLCommThread.ReportProgress((int)(((rowCnt * 1.0f) / CyacdLines.Count) * 100));
                        rowCnt++;
                    }

                    ////////////////////////////////////////////////////////////////////////////
                    // Prepare and Send
                    #region  // -- Verify Application Checksum
                    // Build Verify Application Packet
                    CreateVerifyApplicationCmd(ref cmdBuffer);

                    // ____----____----Send the packet
                    //First Byte in buffer holds the Report ID
                    myHidDevice_BLMgr.Outputs.DataBuf[0] = myHidDevice_BLMgr.Outputs.ID;

                    //Load 64 bytes of data
                    for (int i = 1; i <= cmdBuffer.Count; i++)
                        myHidDevice_BLMgr.Outputs.DataBuf[i] = cmdBuffer[i - 1];

                    // Write to HID output buffer
                    myHidDevice_BLMgr.WriteOutput();

                    // Read from HID input buffer
                    myHidDevice_BLMgr.ReadInput();

                    // Parse the response
                    rspBuffer.Clear();
                    foreach (byte b in myHidDevice_BLMgr.Inputs.DataBuf)
                        rspBuffer.Add(b);

                    if (rspBuffer[2] != 0x00)
                    { MessageBox.Show("Failure of Verify Application CheckSum Command - " + rspBuffer[2].ToString("X2")); }
                    if (rspBuffer[5] == 0x00)
                    { MessageBox.Show("Failure of Verify Application CheckSum Command - Failed CheckSum"); }

                    #endregion

                    ///////////////////////////////////////////////////////////////////////////
                    // Prepare and Send
                    #region  // --Exit the bootloader
                    // Build Exit Bootloader Packet
                    CreateExitBootLoaderCmd(ref cmdBuffer);

                    // ____----____----Send the packet
                    //First Byte in buffer holds the Report ID
                    myHidDevice_BLMgr.Outputs.DataBuf[0] = myHidDevice_BLMgr.Outputs.ID;

                    //Load 64 bytes of data
                    for (int i = 1; i <= cmdBuffer.Count; i++)
                        myHidDevice_BLMgr.Outputs.DataBuf[i] = cmdBuffer[i - 1];

                    // Write to HID output buffer
                    myHidDevice_BLMgr.WriteOutput();
                    MessageBox.Show("  - Remember to Disconnect the USB cable from the POC; then,\n\n  --  Please disconnect and reconnect power to the FS Comfort to complete the programming process.\n\n", "Boot Load Operation Exited!");
                    #endregion

                }
            }
            #endregion


        }


        #region  // Bootloader HID USB FUnctions




        // Populate cmdBuf with bytes for bootloader interface to "Verify Row"
        public static void CreateVerifyRowCmd(ref List<byte> cmdBuf, byte ArrayId, ushort RowNumber, byte CkSum)
        {
            // clear the buffer first

            ushort cmuf;
            cmdBuf.Clear();

            // prepare/open packet buffer
            cmdBuf.Add(0x01);       // Start of Packet

            cmdBuf.Add(0x3A);       // --Get Row Checksum

            cmdBuf.Add(0x03);       // --Data Length[0] - 3

            cmdBuf.Add(0x00);       // --Data Length[1] - 3

            cmdBuf.Add(ArrayId);    // --Data - Flash Array ID

            cmdBuf.Add((byte)(RowNumber & 0x00ff));           // --Data - RowNumber[0]

            cmdBuf.Add((byte)((RowNumber & 0xff00) >> 8));    // --Data - RowNumber[1]

            // Calculate the CRC checksum
            cmuf = CyBtldr_ComputeChecksum(cmdBuf.ToArray(), (ulong)cmdBuf.Count);

            cmdBuf.Add((byte)(cmuf & 0x00ff));              // --Ck Sum[0]

            cmdBuf.Add((byte)((cmuf & 0xff00) >> 8));       // --Ck Sum[1]

            // close packet buffer
            cmdBuf.Add(0x17);       // End of Packet
        }

        // Populate cmdBuf with bytes for bootloader interface to "Send block data"
        public static void CreateSendDataCmd(ref List<byte> cmdBuf, byte[] BlockData)
        {
            // clear the buffer first

            ushort cmuf;
            cmdBuf.Clear();

            // prepare/open packet buffer
            cmdBuf.Add(0x01);       // Start of Packet

            cmdBuf.Add(0x37);       // --Send block Data

            cmdBuf.Add((byte)((BlockData.Length) & 0x00ff));                // --Data Length[0] - DataLen bytes data

            cmdBuf.Add((byte)(((BlockData.Length) & 0xff00) >> 8));       // --Data Length[1] - DataLen bytes data

            // Add 32 byte block of Row Data Bytes
            foreach (byte b in BlockData)
                cmdBuf.Add(b);

            // Calculate the CRC checksum
            cmuf = CyBtldr_ComputeChecksum(cmdBuf.ToArray(), (ulong)cmdBuf.Count);

            cmdBuf.Add((byte)(cmuf & 0x00ff));              // --Ck Sum[0]

            cmdBuf.Add((byte)((cmuf & 0xff00) >> 8));       // --Ck Sum[1]

            // close packet buffer
            cmdBuf.Add(0x17);       // End of Packet
        }

        // Populate cmdBuf with bytes for bootloader interface to "program flash row"
        public static void CreateProgramRowCmd(ref List<byte> cmdBuf, byte ArrayId, ushort RowNumber)
        {
            // clear the buffer first
            ushort cmuf;
            cmdBuf.Clear();

            // prepare/open packet buffer
            cmdBuf.Add(0x01);       // Start of Packet

            cmdBuf.Add(0x39);       // --Erase Flash Row CMD

            cmdBuf.Add(0x03);                // --Data Length[0] - 3

            cmdBuf.Add(0x00);       // --Data Length[1] - 3

            cmdBuf.Add(ArrayId);    // --Data - Flash Array ID

            cmdBuf.Add((byte)(RowNumber & 0x00ff));           // --Data - RowNumber[0]

            cmdBuf.Add((byte)((RowNumber & 0xff00) >> 8));    // --Data - RowNumber[1]

            // Calculate the CRC checksum
            cmuf = CyBtldr_ComputeChecksum(cmdBuf.ToArray(), (ulong)cmdBuf.Count);

            cmdBuf.Add((byte)(cmuf & 0x00ff));              // --Ck Sum[0]

            cmdBuf.Add((byte)((cmuf & 0xff00) >> 8));       // --Ck Sum[1]

            // close packet buffer
            cmdBuf.Add(0x17);       // End of Packet
        }

        // Populate cmdBuf with bytes for bootloader interface to "erase flash row"
        public static void CreateEraseRowCmd(ref List<byte> cmdBuf, byte ArrayId, ushort RowNumber)
        {
            // clear the buffer first
            ushort cmuf;
            cmdBuf.Clear();

            // prepare/open packet buffer
            cmdBuf.Add(0x01);       // Start of Packet

            cmdBuf.Add(0x34);       // --Erase Flash Row CMD

            cmdBuf.Add(0x03);       // --Data Length[0] - 3 bytes data

            cmdBuf.Add(0x00);       // --Data Length[1] - 3 bytes data

            cmdBuf.Add(ArrayId);    // --Data - Flash Array ID

            cmdBuf.Add((byte)(RowNumber & 0x00ff));           // --Data - RowNumber[0]

            cmdBuf.Add((byte)((RowNumber & 0xff00) >> 8));    // --Data - RowNumber[1]

            // Calculate the CRC checksum
            cmuf = CyBtldr_ComputeChecksum(cmdBuf.ToArray(), (ulong)cmdBuf.Count);

            cmdBuf.Add((byte)(cmuf & 0x00ff));              // --Ck Sum[0]

            cmdBuf.Add((byte)((cmuf & 0xff00) >> 8));       // --Ck Sum[1]

            // close packet buffer
            cmdBuf.Add(0x17);       // End of Packet
        }

        // Populate cmdBuf with bytes for bootloader interface to "exit bootloader"
        public static void CreateExitBootLoaderCmd(ref List<byte> cmdBuf)
        {
            // clear the buffer first
            ushort cmuf;
            cmdBuf.Clear();

            // prepare/open packet buffer
            cmdBuf.Add(0x01);       // Start of Packet

            cmdBuf.Add(0x3b);       // --Exit Bootloader CMD

            cmdBuf.Add(0x00);       // --Data Length[0] - no data

            cmdBuf.Add(0x00);       // --Data Length[1] - no data

            // Calculate the CRC checksum
            cmuf = CyBtldr_ComputeChecksum(cmdBuf.ToArray(), (ulong)cmdBuf.Count);

            cmdBuf.Add((byte)(cmuf & 0x00ff));       // --Ck Sum[0] - no data

            cmdBuf.Add((byte)((cmuf & 0xff00) >> 8));       // --Ck Sum[1] - no data

            // close packet buffer
            cmdBuf.Add(0x17);       // End of Packet
        }

        // Populate cmdBuf with bytes for bootloader interface to "enter bootloader"
        public static void CreateVerifyApplicationCmd(ref List<byte> cmdBuf)
        {
            // clear the buffer first
            ushort cmuf;
            cmdBuf.Clear();

            // prepare/open packet buffer
            cmdBuf.Add(0x01);       // Start of Packet

            cmdBuf.Add(0x31);       // --Verify Application CheckSum

            cmdBuf.Add(0x00);       // --Data Length[0] - no data

            cmdBuf.Add(0x00);       // --Data Length[1] - no data

            // Calculate the CRC checksum
            cmuf = CyBtldr_ComputeChecksum(cmdBuf.ToArray(), (ulong)cmdBuf.Count);

            cmdBuf.Add((byte)(cmuf & 0x00ff));       // --Ck Sum[0] - no data

            cmdBuf.Add((byte)((cmuf & 0xff00) >> 8));       // --Ck Sum[1] - no data

            // close packet buffer
            cmdBuf.Add(0x17);       // End of Packet
        }
        // Populate cmdBuf with bytes for bootloader interface to "enter bootloader"
        public static void CreateEnterBootLoaderCmd(ref List<byte> cmdBuf)
        {
            // clear the buffer first
            ushort cmuf;
            cmdBuf.Clear();

            // prepare/open packet buffer
            cmdBuf.Add(0x01);       // Start of Packet

            cmdBuf.Add(0x38);       // --Enter Bootloader CMD

            cmdBuf.Add(0x00);       // --Data Length[0] - no data

            cmdBuf.Add(0x00);       // --Data Length[1] - no data

            // Calculate the CRC checksum
            cmuf = CyBtldr_ComputeChecksum(cmdBuf.ToArray(), (ulong)cmdBuf.Count);

            cmdBuf.Add((byte)(cmuf & 0x00ff));       // --Ck Sum[0] - no data

            cmdBuf.Add((byte)((cmuf & 0xff00) >> 8));       // --Ck Sum[1] - no data

            // close packet buffer
            cmdBuf.Add(0x17);       // End of Packet
        }
        public static ushort CyBtldr_ComputeChecksum(byte[] buf, ulong size)
        {
            //if (useCRC_ChkSum)
            //{
            //    ushort crc = 0xffff;
            //    ushort tmp;
            //    int i;

            //    if (size == 0)
            //        return ((ushort)~crc);


            //    foreach (byte b in buf)
            //    {
            //        for (i = 0, tmp = (ushort)(0x00ff & b); i < 8; i++, tmp >>= 1)
            //        {
            //            if (((crc & 0x0001) ^ (tmp & 0x0001)) != 0)
            //                crc = (ushort)((crc >> 1) ^ 0x8408);
            //            else
            //                crc >>= 1;
            //        }
            //    }
            //    crc = (ushort)~crc;
            //    tmp = crc;
            //    crc = (ushort)((crc << 8) | (tmp >> 8 & 0xFF));

            //    return crc;
            //}
            //else
            {
                ushort sum = 0;
                foreach (byte b in buf)
                    sum += b;

                return (ushort)(1 + ~sum);
            }

        }
        public static List<byte[]> ASCIIHexByteArray2Bytes(string ASCIIHEXSTRING)
        {
            List<byte> tempBytes = new List<byte>();
            List<byte[]> outBytes = new List<byte[]>();
            int i;
            for (i = 0; i < ASCIIHEXSTRING.Length; i = i + 2)
            {
                tempBytes.Add(byte.Parse(ASCIIHEXSTRING.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));

                if (tempBytes.Count == 32)
                {
                    outBytes.Add(tempBytes.ToArray());
                    tempBytes.Clear();
                }
            }
            //outBytes.Add(tempBytes.ToArray());
            return outBytes;
        }
        #endregion


        //USB COMMS Event Callback FUNCTIONS
        #region //USB COMMS Event Callback FUNCTIONS
        /**********************************************************************
        * NAME: usbDevices_DeviceRemoved
        * 
        * DESCRIPTION: Event handler for the removal of a USB device. When the removal
        * of a USB device is detected, this function will be called which will check to
        * see if the device removed was the device we were using. If so, then reset
        * device handler (myHidDevice), disable the timer, and update the GUI.
        * 
        ***********************************************************************/

        public void usbDevices_DeviceRemoved(object sender, EventArgs e)
        {

            USBEventArgs usbEvent = e as USBEventArgs;
            // Handle FW ID
            if ((usbEvent.ProductID == Product_ID_FW) && (usbEvent.VendorID == Vendor_ID))
            {
                myHidDevice_FW = null;
                GetDevice();                        // Process device status
            }
            // Handle BL ID
            if ((usbEvent.ProductID == Product_ID_BL) && (usbEvent.VendorID == Vendor_ID))
            {
                myHidDevice_BL = null;
                GetDevice();                        // Process device status
            }
        }

        /**********************************************************************
        * NAME: usbDevices_DeviceAttached
        * 
        * DESCRIPTION: Event handler for the attachment of a USB device. The function+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        * first checks to see if a matching device is already connected by seeing
        * if the handler (myHidDevice) is null. If no device is previously attached,
        * the function will call GetDevice to check and see if a matching device was 
        * attached.
        * 
         ***********************************************************************/

        public void usbDevices_DeviceAttached(object sender, EventArgs e)
        {
            if (myHidDevice_FW == null && myHidDevice_BL == null)
            {
                GetDevice();                        // Process device status
            }
        }

        /**********************************************************************
        * NAME: GetDevice
        * 
        * DESCRIPTION: Function checks to see if a matching USB device is attached
        * based on the VID and PID provided in the application. When a device is
        * found, it is assigned a handler (myHidDevice) and the GUI is updated to 
        * reflect the connection. Additionally, if the device is not connected,
        * the function will update the GUI to reflect the disconnection.
        * 
        ***********************************************************************/
        public void GetDevice()
        {
            //Look for device matching VID/PID
            myHidDevice = usbDevices[Vendor_ID, Product_ID_FW] as CyHidDevice;

            if (myHidDevice != null)                //Check to see if device is already connected
            {
                myHidDevice = null;
                myHidDevice_FW = usbDevices[Vendor_ID, Product_ID_FW] as CyHidDevice;
            }
            else
            {
                // check if it is the bootloader
                myHidDevice = usbDevices[Vendor_ID, Product_ID_BL] as CyHidDevice;
                if (myHidDevice != null)
                {
                    myHidDevice = null;
                    myHidDevice_BL = usbDevices[Vendor_ID, Product_ID_BL] as CyHidDevice;
                }
                else
                {
                    ;
                }
            }
        }
        #endregion

    }
}
