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



        #region System Feilds (not exposed to property grid/explorer)
        int RxPckIndx, ParsePckIndx, ClearPckIdx;
        bool devConnectedHistory = false;
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
            if(RxPacketBuffer.Count > 0 && RxPacketTimes.Count > 0)
            {
                RxPckIndx = 0;

                while (RxPacketBuffer.Count > 0)
                {
                    if (RxPacketBuffer[RxPckIndx].Length > 0)
                    {
                        ParsePckIndx = StaticSPDPackets.FindIndex(x => x.PacketID == RxPacketBuffer[RxPckIndx][0]);

                        if (ParsePckIndx > -1 && ParsePckIndx < RxPacketBuffer[RxPckIndx].Length)
                        {
                            StaticSPDPackets[ParsePckIndx].ParsePacket(RxPacketBuffer[RxPckIndx], LogData, RxPacketTimes[RxPckIndx]);
                            RxPacketBuffer.RemoveAt(RxPckIndx);
                            RxPacketTimes.RemoveAt(RxPckIndx);
                        }
                    }
                }

            }
            if(DeviceConnected && !devConnectedHistory)
            {
                // Build Static Packets on Connection?
            }
            else if(!DeviceConnected && devConnectedHistory)
            {
                // Destroy Static Packets?
            }
            devConnectedHistory = DeviceConnected;
            UpdateStaticGUILinks();
        }
        /// <summary>
        /// 
        /// </summary>
        public void CommThreadExe()
        {            
            while(DeviceConnected)
            {
                if (WriteFirst)
                {
                    // Write then Read, Queue as needed

                    // Tx Packet Selection
                    TxPacketSelection();

                    // Write
                    WriteSerialPacket();

                    // Read
                    ReadSerialData();

                }
                else
                {
                    // Read, Queue as needed, then Write

                    // Read
                    ReadSerialData();

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
            if(CommLoopCounter % GUILinkUPdateModulo == 0)
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
                return StaticSPDPackets.Find(x => x.PacketID == packIDin).PacketSPDs.Find(x=> ((x.getNodeName == SPDName) && (x.getDataType == DataTypeIn) && (x.getArrayLength == arrayLenIn)));
            }
            else
                return null;
        }
    }


}
