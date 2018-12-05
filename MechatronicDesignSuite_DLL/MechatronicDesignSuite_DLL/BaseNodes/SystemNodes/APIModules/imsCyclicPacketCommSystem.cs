using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using MechatronicDesignSuite_DLL.BaseTypes;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace MechatronicDesignSuite_DLL.BaseNodes
{
    public class imsCyclicPacketCommSystem : imsAPISysModule
    {
        #region System Properties
        protected int GUILinkUPdateModulo = 10;
        [Category("Cyclic Packet Comms System "), Description("Trigger to update GUI links in this module cycle of the mainloop")]
        public int getGUILinkUPdateModulo { get { return GUILinkUPdateModulo; } }
        [Category("Cyclic Packet Comms System "), Description("Trigger to update GUI links in this module cycle of the mainloop")]
        public int setGUILinkUPdateModulo { get { return GUILinkUPdateModulo; } set { GUILinkUPdateModulo = value; } }

        protected List<SerialParameterPacket> StaticSPDPackets = new List<SerialParameterPacket>();
        [Category("Cyclic Packet Comms System "), Description("Static Defined Packet Structures of Serial Parameter Data Nodes")]
        public List<SerialParameterPacket> getStaticSPDPackets { get { return StaticSPDPackets; } }
        [Category("Cyclic Packet Comms System "), Description("Static Defined Packet Structures of Serial Parameter Data Nodes")]
        public List<SerialParameterPacket> setStaticSPDPackets { get { return StaticSPDPackets; } set { StaticSPDPackets = value; } }

        protected bool WriteFirst = false;
        [Category("Cyclic Packet Comms System "), Description("Indication to Communication Controller to initiate communication channel with a write() before attempting to read()")]
        public bool getWriteFirst { get { return WriteFirst; } }
        [Category("Cyclic Packet Comms System "), Description("Indication to Communication Controller to initiate communication channel with a write() before attempting to read()")]
        public bool setWriteFirst { get { return WriteFirst; } set { WriteFirst = value; } }


        protected bool DeviceConnected = false;
        [Category("Cyclic Packet Comms System "), Description("Indication to Communication Controller that a device is connect and it is clear to call read()/write() functions")]
        public bool getDeviceConnected { get { return DeviceConnected; } }
        [Category("Cyclic Packet Comms System "), Description("Indication to Communication Controller that a device is connect and it is clear to call read()/write() functions")]
        public bool setDeviceConnected { get { return DeviceConnected; } set { DeviceConnected = value; } }

        protected List<byte[]> RxPacketBuffer = new List<byte[]>();
        [Category("Cyclic Packet Comms System "), Description("The Dynamic Data Buffer populated by the read() function")]
        public List<byte[]> getRxPacketBuffer { get { return RxPacketBuffer; } }
        [Category("Cyclic Packet Comms System "), Description("The Dynamic Data Buffer populated by the read() function")]
        public List<byte[]> setRxPacketBuffer { get { return RxPacketBuffer; } set { RxPacketBuffer = value; } }

        protected List<DateTime> RxPacketTimes = new List<DateTime>();
        [Category("Cyclic Packet Comms System "), Description("The Dynamic Data Buffer Capture Times")]
        public List<DateTime> getRxPacketTimes { get { return RxPacketTimes; } }
        [Category("Cyclic Packet Comms System "), Description("The Dynamic Data Buffer Capture Times")]
        public List<DateTime> setRxPacketTimes { get { return RxPacketTimes; } set { RxPacketTimes = value; } }

        protected List<byte[]> TxPacketQueue = new List<byte[]>();
        [Category("Cyclic Packet Comms System "), Description("The Dynamic Data Buffer consumed by the write() function")]
        public List<byte[]> getTxPacketQueue { get { return TxPacketQueue; } }
        [Category("Cyclic Packet Comms System "), Description("The Dynamic Data Buffer consumed by the write() function")]
        public List<byte[]> setTxPacketQueue { get { return TxPacketQueue; } set { TxPacketQueue = value; } }

        protected int CommLoopCounter = 0;
        [Category("Cyclic Packet Comms System "), Description("Running Counter of Communication Loop Iterations while Device Connected")]
        public int getCommLoopCounter { get { return CommLoopCounter; }  }
        [Category("Cyclic Packet Comms System "), Description("Running Counter of Communication Loop Iterations while Device Connected")]
        public int setCommLoopCounter { get { return CommLoopCounter; } set { CommLoopCounter = value; } }



        protected int SleepTime = 1000;
        [Category("Cyclic Packet Comms System "), Description(".NET time (ms) for infinite loop of comms system to sleep ending each iteration")]
        public int getSleepTime { get { return SleepTime; } }
        [Category("Cyclic Packet Comms System "), Description(".NET time (ms) for infinite loop of comms system to sleep ending each iteration")]
        public int setSleepTime { set { SleepTime = value; } get { return SleepTime; } }
        
        protected bool LogData = false;
        [Category("Cyclic Packet Comms System "), Description(".NET time (ms) for infinite loop of comms system to sleep ending each iteration")]
        public bool getLogData { get { return LogData; } }
        [Category("Cyclic Packet Comms System "), Description(".NET time (ms) for infinite loop of comms system to sleep ending each iteration")]
        public bool setLogData { set { LogData = value; } get { return LogData; } }

        protected bool ClearLoggedData = false;
        [Category("Cyclic Packet Comms System "), Description("trigger to clear all logged data from RAM buffers")]
        public bool getClearLoggedData { get { return ClearLoggedData; } }
        [Category("Cyclic Packet Comms System "), Description("trigger to clear all logged data from RAM buffers")]
        public bool setClearLoggedData { set { ClearLoggedData = value; } get { return ClearLoggedData; } }

        protected float SimulatedInput4Bytes = 0x0;
        [Category("Cyclic Packet Comms System "), Description("4 Byte input (simulated data) to the read() function")]
        public float getSimulatedInput4Bytes { get { return SimulatedInput4Bytes; } }
        [Category("Cyclic Packet Comms System "), Description("4 Byte input (simulated data) to the read() function")]
        public float setSimulatedInput4Bytes { set { SimulatedInput4Bytes = value; } get { return SimulatedInput4Bytes; } }

        protected int mainLoopCounter = 0;



        public DateTime TimeAtStartLogging { get {return timeAtStartLogging; } }
        DateTime timeAtStartLogging;

        #region System Feilds (not exposed to property grid/explorer)
        int RxPckIndx, ParsePckIndx, ClearPckIdx;
        bool devConnectedHistory = false;
        bool RxPackBufferLocked = false;
        #endregion
        #endregion

        #region System Constructors
        public imsCyclicPacketCommSystem(List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            NodeType = typeof(imsCyclicPacketCommSystem);
            NodeName = "Cyclic Packet Comm System";
        }
        public imsCyclicPacketCommSystem(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs) : base(DeSerializeFormatter, deSerializeFs)
        {
            
        }
        public imsCyclicPacketCommSystem(PCExeSys PCExeSysIn, List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            NodeType = typeof(imsCyclicPacketCommSystem);
            NodeName = "Cyclic Packet Comm System";
            if (PCExeSysIn != null)
                PCExeSysLink = PCExeSysIn;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public override void MainLoop()
        {
            if(ClearLoggedData)
            {
                for(ClearPckIdx = 0; ClearPckIdx<StaticSPDPackets.Count; ClearPckIdx++)
                {
                    StaticSPDPackets[ClearPckIdx].ClearLoggedValues();
                }
                ClearLoggedData = false;
            }


            //if(RxPacketBuffer.Count > 0 && RxPacketTimes.Count > 0)
            //{
            //    RxPackBufferLocked = true;
            //    for (RxPckIndx = 0; RxPckIndx < RxPacketBuffer.Count; RxPckIndx++)
            //    {
            //        // 1) mark rxbuffered bytes as "for removal/disposal"
            //        // 2) transfer bytes stripped of header from rxbufferred to new shorter byte []

            //        if (RxPacketBuffer[RxPckIndx].Length > 3)
            //        {
            //            // ***needs to work with variable header data sizes*** //
            //            if (RxPacketBuffer[RxPckIndx][0] != 0xff || RxPacketBuffer[RxPckIndx][1] != 0xff ||
            //                RxPacketBuffer[RxPckIndx][2] != 0xff || RxPacketBuffer[RxPckIndx][3] != 0xff)
            //            {
            //                byte tempIDIndex = RxPacketBuffer[RxPckIndx][0]; 
                            

            //                // ***needs to work with variable header data sizes*** //
            //                RxPacketBuffer[RxPckIndx][0] = 0xff;
            //                RxPacketBuffer[RxPckIndx][1] = 0xff;
            //                RxPacketBuffer[RxPckIndx][2] = 0xff;
            //                RxPacketBuffer[RxPckIndx][3] = 0xff;

            //                ParsePckIndx = StaticSPDPackets.FindIndex(x => x.PackID == tempIDIndex);

            //                if (ParsePckIndx > -1 && ParsePckIndx < RxPacketBuffer[RxPckIndx].Length)
            //                {
            //                    List<byte> tempBytes = new List<byte>(RxPacketBuffer[RxPckIndx]);

            //                    // ***needs to work with variable header data sizes*** //
            //                    tempBytes.RemoveRange(0, 4);

            //                    // header should be stripped, spd data only, no header bytes
            //                    StaticSPDPackets[ParsePckIndx].ParsePacket(tempBytes.ToArray(), LogData, RxPacketTimes[RxPckIndx]);

            //                }
            //            }
            //        }
            //    }
            //    RxPackBufferLocked = false;
            //}


            if(DeviceConnected && !devConnectedHistory)
            {
                // Build Static Packets on Connection?

                // Capture Connection Time
                timeAtStartLogging = DateTime.Now;
            }
            else if(!DeviceConnected && devConnectedHistory)
            {
                // Destroy Static Packets?
            }
            devConnectedHistory = DeviceConnected;
            UpdateStaticGUILinks();
        }
        protected virtual void ParsePacketData()
        {
            for (RxPckIndx = 0; RxPckIndx < RxPacketBuffer.Count; RxPckIndx++)
            {
                if (RxPacketBuffer[RxPckIndx].Length > 3)
                {
                    ParsePckIndx = StaticSPDPackets.FindIndex(x => x.PackID == RxPacketBuffer[RxPckIndx][0]);

                    if (ParsePckIndx > -1 && ParsePckIndx < RxPacketBuffer[RxPckIndx].Length)
                    {
                        List<byte> tempBytes = new List<byte>(RxPacketBuffer[RxPckIndx]);

                        // ***needs to work with variable header data sizes*** //
                        tempBytes.RemoveRange(0, 4);

                        // header should be stripped, spd data only, no header bytes
                        StaticSPDPackets[ParsePckIndx].ParsePacket(tempBytes.ToArray(), LogData, RxPacketTimes[RxPckIndx]);

                    }
                }
            }
            RxPacketBuffer.Clear();
            setRxPacketTimes.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void CommThreadExe()
        {
            while (DeviceConnected)
            {
                //if (RxPacketBuffer.Count > 0 && (CommLoopCounter % 10 == 0) && !RxPackBufferLocked)
                //{
                //    bool finishedRemovals = false;
                //    int firstindextoRemove = -1;
                //    int secondindextoRemove = -1;
                //    while (RxPacketBuffer.Count > 0 && !finishedRemovals)
                //    {
                //        firstindextoRemove = RxPacketBuffer.FindIndex(x => x.Length < 4);
                //        if(firstindextoRemove > -1)
                //        {
                //            RxPacketBuffer.RemoveAt(firstindextoRemove);
                //            RxPacketTimes.RemoveAt(firstindextoRemove);
                //        }

                //        secondindextoRemove = RxPacketBuffer.FindIndex(x => (x[0] == 0xff && x[1] == 0xff && x[2] == 0xff && x[3] == 0xff));
                //        if (secondindextoRemove > -1)
                //        {
                //            RxPacketBuffer.RemoveAt(secondindextoRemove);
                //            RxPacketTimes.RemoveAt(secondindextoRemove);
                //        }

                //        if (firstindextoRemove == -1 && secondindextoRemove == -1)
                //            finishedRemovals = true;
                //    }
                //}

                if (WriteFirst)
                {
                    // Write then Read, Queue as needed

                    // Tx Packet Selection
                    TxPacketSelection();

                    // Write
                    WriteSerialPacket();

                    // Read
                    ReadSerialData();

                    // Parse New Data
                    ParsePacketData();

                }
                else
                {
                    // Read, Queue as needed, then Write

                    // Read
                    ReadSerialData();

                    // Parse New Data
                    ParsePacketData();

                    // Tx Packet Selection
                    TxPacketSelection();

                    // Write
                    WriteSerialPacket();
                }
                CommLoopCounter++;
                System.Threading.Thread.Sleep(SleepTime);
            }


            if (!DeviceConnected)
                CommLoopCounter = 0;
            
        }
        public virtual void TxPacketSelection()
        {

        }
        public virtual void ReadSerialData()
        {
            
        }
        public virtual void WriteSerialPacket()
        {

        }
        public virtual void BuildStaticPackets()
        {

        }
        public virtual void UpdateStaticGUILinks()
        {            
            if(mainLoopCounter++ % GUILinkUPdateModulo == 0)
            {
                foreach(SerialParameterPacket SPDPacket in StaticSPDPackets)
                {
                    foreach(imsSerialParamData SPD in SPDPacket.PacketSPDs)
                    {
                        SPD.updateGUILINKS();
                    }
                }
            }
        }
        public void AddPacket2Buffer(byte[] PacketIn)
        {
            RxPacketBuffer.Add(PacketIn);
            RxPacketTimes.Add(DateTime.Now);
        }
        public imsSerialParamData getMatchingSPD(int packIDin, string SPDName, Type DataTypeIn, int arrayLenIn)
        {

            if (StaticSPDPackets.Count > 0)
            {
                SerialParameterPacket tempPacket = StaticSPDPackets.Find(x => x.PackID == packIDin);
                if (tempPacket != null)
                {
                    imsSerialParamData tempSPD = tempPacket.PacketSPDs.Find(x => ((x.getNodeName == SPDName) && (x.getDataType == DataTypeIn) && (x.getArrayLength == arrayLenIn)));
                    if (tempSPD != null)
                    {
                        tempSPD.setcyclicCommSysLink = this;
                        tempSPD.setTxPacket(packIDin);
                    }
                    return tempSPD;
                }
                else
                    return null;              
            }
            else
                return null;
        }
        public void AddTxPack2TXQueue(SerialParameterPacketHeader txHeaderIn, List<byte> SerialDataOut_in)
        {
            if (StaticSPDPackets.Count > 0)
            {
                SerialParameterPacket tempPacket = StaticSPDPackets.Find(x => x.PackID == txHeaderIn.PacketID);
                if (tempPacket != null)
                {
                    List<byte> tempBuffer = new List<byte>();

                    tempBuffer.AddRange(txHeaderIn.HDR2ByteArray());
                    if(SerialDataOut_in!=null)
                        if(SerialDataOut_in.Count>0)
                            tempBuffer.AddRange(SerialDataOut_in);

                    TxPacketQueue.Add(tempBuffer.ToArray());
                }
            }
        }

    }


}
