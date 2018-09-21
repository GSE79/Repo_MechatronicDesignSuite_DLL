using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MechatronicDesignSuite_DLL
{
    public class imsCyclicPacketCommSystem : imsAPISysModule
    {
        #region System Properties
        public List<SerialParameterPacket> StaticSPDPackets { set; get; } = new List<SerialParameterPacket>();
        public bool WriteFirst { set; get; } = false;
        public bool DeviceConnected { set; get; } = false;
        public List<byte[]> RxPacketBuffer { set; get; } = new List<byte[]>();
        public List<DateTime> RxPacketTimes { set; get; } = new List<DateTime>();
        public int CommLoopCounter { set; get; } = 0;
        public int SleepTime { set; get; } = 5;
        public bool LogData { set; get; } = false;
        public bool ClearLoggedData { set; get; } = false;

        #region System Feilds (not exposed to property grid/explorer)
        int RxPckIndx, ParsePckIndx, ClearPckIdx;
        bool devConnectedHistory = false;
        #endregion
        #endregion

        #region System Constructors
        public imsCyclicPacketCommSystem(List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            nodeType = typeof(imsCyclicPacketCommSystem);
            nodeName = "Cyclic Packet Comm System";
        }
        public imsCyclicPacketCommSystem(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs) : base(DeSerializeFormatter, deSerializeFs)
        {
            
        }
        public imsCyclicPacketCommSystem(PCExeSys PCExeSysIn, List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            nodeType = typeof(imsCyclicPacketCommSystem);
            nodeName = "Cyclic Packet Comm System";
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
            if(RxPacketBuffer.Count > 0)
            {
                for(RxPckIndx=0; RxPckIndx<RxPacketBuffer.Count; RxPckIndx++)
                {
                    ParsePckIndx = StaticSPDPackets.FindIndex(x => x.PacketID == RxPacketBuffer[RxPckIndx][0]);
                    if (ParsePckIndx > -1 && ParsePckIndx < RxPacketBuffer[RxPckIndx].Length)
                    {
                        StaticSPDPackets[ParsePckIndx].ParsePacket(RxPacketBuffer[RxPckIndx], LogData, RxPacketTimes[RxPckIndx]);
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
        public void AddPacket2Buffer(byte[] PacketIn)
        {
            RxPacketBuffer.Add(PacketIn);
            RxPacketTimes.Add(DateTime.Now);
        }
    }


}
