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

        [Category("Cyclic Packet Comms System - Auto-GEN "),            Description("Trigger to generate cross platform embedded code for this cyclic packet communication system")]
        public bool         TriggerXPlatAutoGEN { set; get; } =         false;
        [Category("Cyclic Packet Comms System - Auto-GEN "),            Description("Indication that all required template files are present")]
        public bool         AutoGen_HasAllFiles { get; set; } =         false;
        [Category("Cyclic Packet Comms System - Auto-GEN "),            Description("Number of bytes in the buffers consumed by header bytes")]
        public int          PacketHeaderSize { get; set; } =            4;
        [Category("Cyclic Packet Comms System - Auto-GEN "),            Description("Indication of Endian-ness (true for Big endian)")]
        public bool         SystemIsBigEndian { get; set; } =           true;
        [Category("Cyclic Packet Comms System - Auto-GEN "),            Description("XPlatSource template directory string")]
        public string       TemplateDirectory { set; get; } =           Path.Combine(Directory.GetCurrentDirectory(), "XPlatSource\\");
        [Category("Cyclic Packet Comms System - Auto-GEN "),            Description("xplat_class.cpp source template file name")]
        public string       TemplateFileName_ClassCpp { set; get; } =   Path.Combine(Directory.GetCurrentDirectory(), "XPlatSource\\XPlat_LIB\\xplat_class.cpp");
        [Category("Cyclic Packet Comms System - Auto-GEN "),            Description("xplat_class.hpp header template file name")]
        public string       TemplateFileName_ClassHpp { set; get; } =   Path.Combine(Directory.GetCurrentDirectory(), "XPlatSource\\XPlat_LIB\\xplat_class.hpp");
        [Category("Cyclic Packet Comms System - Auto-GEN "),            Description("xplat_base.c source template file name")]
        public string       TemplateFileName_BaseC { set; get; } =      Path.Combine(Directory.GetCurrentDirectory(), "XPlatSource\\XPlat_LIB\\xplat_base.c");
        [Category("Cyclic Packet Comms System - Auto-GEN "),            Description("xplat_base.h header template file name")]
        public string       TemplateFileName_BaseH { set; get; } =      Path.Combine(Directory.GetCurrentDirectory(), "XPlatSource\\XPlat_LIB\\xplat_base.h");

        [Category("Cyclic Packet Comms System - Auto-GEN "),            Description("Short name of output library")]
        public string       OutputModuleName { set; get; } =            "XPlat";
        [Category("Cyclic Packet Comms System - Auto-GEN "),            Description("Directory of output library")]
        public string       OutputModuleDir { set; get; } =             Path.Combine(Directory.GetCurrentDirectory(), "XPlatSourceDir");
        [Category("Cyclic Packet Comms System - Auto-GEN "),            Description("xplat_class.cpp auto-gen output file name")]
        public string       OutputFileName_ClassCpp { set; get; } =     Path.Combine(Directory.GetCurrentDirectory(), "XPlatSourceDir\\XPlat_LIB\\xplat_class.cpp");
        [Category("Cyclic Packet Comms System - Auto-GEN "),            Description("xplat_class.hpp auto-gen file name")]
        public string       OutputFileName_ClassHpp { set; get; } =     Path.Combine(Directory.GetCurrentDirectory(), "XPlatSourceDir\\XPlat_LIB\\xplat_class.hpp");
        [Category("Cyclic Packet Comms System - Auto-GEN "),            Description("xplat_base.c auto-gen file name")]
        public string       OutputFileName_BaseC { set; get; } =        Path.Combine(Directory.GetCurrentDirectory(), "XPlatSourceDir\\XPlat_LIB\\xplat_base.c");
        [Category("Cyclic Packet Comms System - Auto-GEN "),            Description("xplat_base.h auto-gen file name")]
        public string       OutputFileName_BaseH { set; get; } =        Path.Combine(Directory.GetCurrentDirectory(), "XPlatSourceDir\\XPlat_LIB\\xplat_base.h");


        protected int GUILinkUPdateModulo = 10;
        [Category("Cyclic Packet Comms System "), Description("Trigger to update GUI links in this module cycle of the mainloop")]
        public int setGUILinkUPdateModulo { get { return GUILinkUPdateModulo; } set { GUILinkUPdateModulo = value; } }

        protected List<SerialParameterPacket> StaticSPDPackets = new List<SerialParameterPacket>();
        [Category("Cyclic Packet Comms System "), Description("Static Defined Packet Structures of Serial Parameter Data Nodes")]
        public List<SerialParameterPacket> setStaticSPDPackets { get { return StaticSPDPackets; } set { StaticSPDPackets = value; } }

        protected List<imsValueNode> LinkedValueNodes = new List<imsValueNode>();
        [Category("Cyclic Packet Comms System "), Description("Linked Value Nodes not automatically updated by Comm. System")]
        public List<imsValueNode> getLinkedValueNodes { get{return LinkedValueNodes; } }


        protected bool WriteFirst = false;
        [Category("Cyclic Packet Comms System "), Description("Indication to Communication Controller to initiate communication channel with a write() before attempting to read()")]
        public bool setWriteFirst { get { return WriteFirst; } set { WriteFirst = value; } }


        protected bool DeviceConnected = false;
        [Category("Cyclic Packet Comms System "), Description("Indication to Communication Controller that a device is connect and it is clear to call read()/write() functions")]
        public bool setDeviceConnected { get { return DeviceConnected; } set { DeviceConnected = value; } }

        protected List<byte[]> RxPacketBuffer = new List<byte[]>();
        [Category("Cyclic Packet Comms System "), Description("The Dynamic Data Buffer populated by the read() function")]
        public List<byte[]> setRxPacketBuffer { get { return RxPacketBuffer; } set { RxPacketBuffer = value; } }

        protected List<DateTime> RxPacketTimes = new List<DateTime>();
        [Category("Cyclic Packet Comms System "), Description("The Dynamic Data Buffer Capture Times")]
        public List<DateTime> setRxPacketTimes { get { return RxPacketTimes; } set { RxPacketTimes = value; } }

        protected List<byte[]> TxPacketQueue = new List<byte[]>();
        [Category("Cyclic Packet Comms System "), Description("The Dynamic Data Buffer consumed by the write() function")]
        public List<byte[]> setTxPacketQueue { get { return TxPacketQueue; } set { TxPacketQueue = value; } }

        public List<Double> RxLatchTimeHistory = new List<Double>();

        protected int CommLoopCounter = 0;
        [Category("Cyclic Packet Comms System "), Description("Running Counter of Communication Loop Iterations while Device Connected")]
        public int setCommLoopCounter { get { return CommLoopCounter; } set { CommLoopCounter = value; } }

        protected int SleepTime = 1000;
        [Category("Cyclic Packet Comms System "), Description(".NET time (ms) for infinite loop of comms system to sleep ending each iteration")]
        public int setSleepTime { set { SleepTime = value; } get { return SleepTime; } }
        
        protected bool LogData = false;
        [Category("Cyclic Packet Comms System "), Description(".NET time (ms) for infinite loop of comms system to sleep ending each iteration")]
        public bool setLogData {
            set {

                if(value && !LogData)
                {
                    if (resetLoggingClock)
                        timeAtStartLogging = DateTime.Now;
                    resetLoggingClock = false;
                }

                LogData = value; }
            get { return LogData; } }
        protected bool resetLoggingClock = true;
        protected bool ClearLoggedData = false;
        [Category("Cyclic Packet Comms System "), Description("trigger to clear all logged data from RAM buffers")]
        public bool setClearLoggedData { set { resetLoggingClock = true; ClearLoggedData = value; } get { return ClearLoggedData; } }

        protected float SimulatedInput4Bytes = 0x0;
        [Category("Cyclic Packet Comms System "), Description("4 Byte input (simulated data) to the read() function")]
        public float setSimulatedInput4Bytes { set { SimulatedInput4Bytes = value; } get { return SimulatedInput4Bytes; } }





        protected int mainLoopCounter = 0;
        protected string BaseHText = "";
        protected string BaseCText = "";
        protected string ClassHppText = "";
        protected string ClassCppText = "";






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
                for(ClearPckIdx = 0; ClearPckIdx<LinkedValueNodes.Count; ClearPckIdx++)
                {
                    LinkedValueNodes[ClearPckIdx].clearValues();
                }
                ClearLoggedData = false;
            }
            

            if(DeviceConnected && !devConnectedHistory)
            {
                // Build Static Packets on Connection?

                // Capture Connection Time
                
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

                        if(LogData)
                            RxLatchTimeHistory.Add((RxPacketTimes[RxPckIndx] - TimeAtStartLogging).TotalSeconds);

                        // header should be stripped, spd data only, no header bytes
                        StaticSPDPackets[ParsePckIndx].ParsePacket(tempBytes.ToArray(), LogData, RxPacketTimes[RxPckIndx]);

                        // custom post parse actions
                        CustomPostParse(StaticSPDPackets[ParsePckIndx].PackID, LogData, RxPacketTimes[RxPckIndx]);
                    }
                }
            }
            RxPacketBuffer.Clear();
            setRxPacketTimes.Clear();
        }
        protected virtual void CustomPostParse(int PackIDin, bool LogData, DateTime RxPacketTime)
        {
            ;
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
            {
                CommLoopCounter = 0;
                GenerateXPlatSource();
            }
            
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
        public SerialParameterPacket AddNewStaticPacket(string PacketNameString, byte PacketIDbyte, List<imsBaseNode> globalNodeListLinkIn)
        {
            StaticSPDPackets.Add(new SerialParameterPacket(this, PacketNameString, PacketIDbyte, globalNodeListLinkIn));

            return StaticSPDPackets[StaticSPDPackets.Count - 1];
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
                foreach (imsValueNode vNOde in LinkedValueNodes)
                    vNOde.updateGUILINKS();
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
                        return tempSPD;
                    }
                    else
                        throw new Exception("Failed to find matching SPD: "+SPDName);
                }
                else
                    throw new Exception("Failed to find matching SPD: No Matching Packet ID");              
            }
            else
                throw new Exception("Failed to find matching SPD: No Packets Defined");
        }
        public imsValueNode getLinkedvNode(string vNodeName, Type DataTypeIn, int arrayLenIn)
        {
            imsValueNode tempvNode = LinkedValueNodes.Find(x => ((x.getNodeName == vNodeName) && (x.getDataType == DataTypeIn) && (x.getArrayLength == arrayLenIn)));
            if (tempvNode != null)
            {
                ;
            }
            return tempvNode;
        }
        public void AddTxPack2TXQueue(SerialParameterPacketHeader txHeaderIn, List<byte> SerialDataOut_in)
        {
            if (StaticSPDPackets.Count > 0)
            {
                SerialParameterPacket tempPacket = StaticSPDPackets.Find(x => x.PackID == txHeaderIn.PacketID);
                if (tempPacket != null)
                {
                    AddTxPack2TXQueueNoStatic(txHeaderIn, SerialDataOut_in);
                }
            }
        }
        public void AddTxPack2TXQueueNoStatic(SerialParameterPacketHeader txHeaderIn, List<byte> SerialDataOut_in)
        {
            List<byte> tempBuffer = new List<byte>();

            tempBuffer.AddRange(txHeaderIn.HDR2ByteArray());
            if (SerialDataOut_in != null)
                if (SerialDataOut_in.Count > 0)
                    tempBuffer.AddRange(SerialDataOut_in);

            TxPacketQueue.Add(tempBuffer.ToArray());
        }
        public virtual void GenerateXPlatSource()
        {
            if(!AutoGen_HasAllFiles && TriggerXPlatAutoGEN)
            {                
                do
                {
                    if (!Directory.Exists(TemplateDirectory))
                    {
                        // Prompt for Template Directory
                        FolderBrowserDialog TemplateFolderDialog = new FolderBrowserDialog();
                        TemplateFolderDialog.Description = "Select the Directory for XPlat Template Files";
                        DialogResult thisResult = TemplateFolderDialog.ShowDialog();
                        if (thisResult == DialogResult.OK || thisResult == DialogResult.Yes)
                        {
                            if (Directory.Exists(TemplateFolderDialog.SelectedPath))
                            {
                                AutoGen_HasAllFiles = Directory.Exists(TemplateDirectory);
                                AutoGen_HasAllFiles &= File.Exists(TemplateFileName_ClassCpp);
                                AutoGen_HasAllFiles &= File.Exists(TemplateFileName_ClassHpp);
                                AutoGen_HasAllFiles &= File.Exists(TemplateFileName_BaseC);
                                AutoGen_HasAllFiles &= File.Exists(TemplateFileName_BaseH);
                                if (AutoGen_HasAllFiles)
                                {
                                    TemplateDirectory = TemplateFolderDialog.SelectedPath;
                                }
                                else
                                {
                                    MessageBox.Show("This is not a valid directory of template files!");
                                    TriggerXPlatAutoGEN = false;
                                }
                            }

                        }
                    }
                    else
                    {
                        AutoGen_HasAllFiles = Directory.Exists(TemplateDirectory);
                        AutoGen_HasAllFiles &= File.Exists(TemplateFileName_ClassCpp);
                        AutoGen_HasAllFiles &= File.Exists(TemplateFileName_ClassHpp);
                        AutoGen_HasAllFiles &= File.Exists(TemplateFileName_BaseC);
                        AutoGen_HasAllFiles &= File.Exists(TemplateFileName_BaseH);
                    }

                } while (!AutoGen_HasAllFiles && TriggerXPlatAutoGEN);
            }
            else if(TriggerXPlatAutoGEN)
            {
                TriggerXPlatAutoGEN = false;
                if (Directory.Exists(OutputModuleDir))
                {
                    // Delete all contents
                    FileNDirTools.RecurrsiveDelete(OutputModuleDir);
                    System.Threading.Thread.Sleep(500);
                    Directory.CreateDirectory(OutputModuleDir);

                    // Copy Template Files to Output Directory
                    foreach (string DirString in Directory.EnumerateDirectories(TemplateDirectory))
                    {
                        string OutDirString = OutputModuleDir + DirString.Substring(DirString.LastIndexOf("\\"));
                        Directory.CreateDirectory(OutDirString);
                        foreach (string FileString in Directory.EnumerateFiles(DirString))
                            File.Copy(FileString, Path.Combine(OutDirString, Path.GetFileName(FileString)));                                              
                    }

                    // Open Template Files and Read into RAM
                    BaseHText = File.ReadAllText(OutputFileName_BaseH);
                    BaseCText = File.ReadAllText(OutputFileName_BaseC);
                    ClassHppText = File.ReadAllText(OutputFileName_ClassHpp);
                    ClassCppText = File.ReadAllText(OutputFileName_ClassCpp);

                    // Find Areas and Auto-Gen each Area
                    AutoGen_FindNProcess();

                    // Write Auto-Gen Files to Output Directory
                    File.WriteAllText(OutputFileName_BaseH, BaseHText);
                    File.WriteAllText(OutputFileName_BaseC, BaseCText);
                    File.WriteAllText(OutputFileName_ClassHpp, ClassHppText);
                    File.WriteAllText(OutputFileName_ClassCpp, ClassCppText);
                }
                else
                {
                    // Try to create the Directory
                    Directory.CreateDirectory(OutputModuleDir);

                    // Check again and maybe set the trigger again
                    if (Directory.Exists(OutputModuleDir))
                        TriggerXPlatAutoGEN = true;
                    else
                    {
                        // Prompt for Output Directory
                        FolderBrowserDialog OutputFolderDialog = new FolderBrowserDialog();
                        OutputFolderDialog.Description = "Select the Directory for XPlat Output Files";
                        DialogResult thatResult = OutputFolderDialog.ShowDialog();
                        if (thatResult == DialogResult.OK || thatResult == DialogResult.Yes)
                            if (!Directory.Exists(OutputFolderDialog.SelectedPath))
                                MessageBox.Show("This is not a valid directory for output files!");
                            else
                                TriggerXPlatAutoGEN = true;
                    }                    
                }
            }
        }
        protected virtual void AutoGen_FindNProcess()
        {
            List<string> AutoGen_Keys = new List<string>();                          
            AutoGen_Keys.Add("XPLAT_DLL_API_Packets Structures");                   // 0
            AutoGen_Keys.Add("XPLAT_DLL_API_PackagePacket Function Prototypes");    // 1
            AutoGen_Keys.Add("XPLAT_DLL_API_Comm Function Prototypes");             // 2
            AutoGen_Keys.Add("XPLAT_DLL_API_PackagePacket Function Definitions");   // 3
            AutoGen_Keys.Add("XPLAT_DLL_API_Comm Function Definitions");            // 4
            AutoGen_Keys.Add("XPLAT_CLASS_PackagePacket Method Definitions");       // 5
            AutoGen_Keys.Add("XPLAT_CLASS_Comm Method Definitions");                // 6
            AutoGen_Keys.Add("XPLAT_CLASS_PackagePacket Method Prototypes");        // 7
            AutoGen_Keys.Add("XPLAT_CLASS_Comm Method Prototypes");                 // 8
            AutoGen_Keys.Add("XPLAT_DLL_API_Comm Packet Structure Pointers");       // 9

            // Loop all files, process lines, insert new lines according to Auto-Gen::Key
            List<string> FileTexts = new List<string>(4) {BaseHText, BaseCText, ClassHppText, ClassCppText };
            for(int i = 0; i < FileTexts.Count; i++)
            {
                foreach(string keyString in AutoGen_Keys)
                {
                    if(FileTexts[i].Contains(keyString))
                    {
                        string tempText = "";
                        string tempString = "";
                        switch (AutoGen_Keys.IndexOf(keyString))
                        {
                        case 0:     // Packet Structures
                            {
                            tempText += "//////////////////////////////////////////////////////////\n//\n// Auto-Gen Packet Structures \n//\n//////////////////////////////////////////////////////////\n";
                            tempText += "#ifdef PckHDRSize\n\t#undef PckHDRSize\n#endif\n#define PckHDRSize " + PacketHeaderSize.ToString() + "\n";
                            // Loop all Static Packets in Comm System
                            foreach (SerialParameterPacket SPDPacket in StaticSPDPackets)
                                tempText += SPDPacket.toCTypeString();
                            

                            break;
                            }
                        case 1:     // Package Function Prototypes
                                {
                                    tempText += "//////////////////////////////////////////////////////////\n//\n// Auto-Gen Packet Package Function Prototypes \n//\n//////////////////////////////////////////////////////////\n";
                                    // Loop all Static Packets in Comm System
                                    foreach (SerialParameterPacket SPDPacket in StaticSPDPackets)
                                        tempText += SPDPacket.toCPkgFunctionPrototypeString();


                                    break;
                                }
                            case 2:     // Comm Function Prototypes
                            {
                                    tempText += "//////////////////////////////////////////////////////////\n//\n// Auto-Gen Packet Comm Function Prototypes \n//\n//////////////////////////////////////////////////////////\n";
                                    tempText += "XPLAT_DLL_API void processRxPacket(xplatAPIstruct* xplatAPI);\nXPLAT_DLL_API void prepareTxPacket(xplatAPIstruct* xplatAPI);\n";

                                    
                                    break;
                            }
                        case 3:     // Package Function Definitions
                                {
                                    tempText += "//////////////////////////////////////////////////////////\n//\n// Auto-Gen Packet Package Function Definitions \n//\n//////////////////////////////////////////////////////////\n";
                                    // Loop all Static Packets in Comm System
                                    foreach (SerialParameterPacket SPDPacket in StaticSPDPackets)
                                        tempText += SPDPacket.toCPkgFunctionDefinitionString();
                                    break;
                                }
                        case 4:     // Comm Function Definitions
                            {
                                    tempText += "//////////////////////////////////////////////////////////\n//\n// Auto-Gen Packet Comm Function Definitions \n//\n//////////////////////////////////////////////////////////\n";
                                    tempText += "void processRxPacket(xplatAPIstruct* xplatAPI)\n{\n\t// Based on bit 0 of the input buffer, switch on packetID\n\tswitch (xplatAPI->Data->inputBuffer[0])\n\t{\n\t\t// Auto-Gen cases from packets\n";
                                    // Loop all Static Packets in Comm System
                                    foreach (SerialParameterPacket SPDPacket in StaticSPDPackets)
                                        tempText += SPDPacket.toCPkgCommDefinitionString("unpack");
                                    tempText += "\t\tdefault:break;\n\t}\n}\n";

                                    tempText += "void prepareTxPacket(xplatAPIstruct* xplatAPI)\n{\n\t// Based on bit 0 of the output buffer, switch on packetID\n\tswitch (xplatAPI->Data->outputBuffer[0])\n\t{\n\t\t// Auto-Gen cases from packets";
                                    // Loop all Static Packets in Comm System
                                    foreach (SerialParameterPacket SPDPacket in StaticSPDPackets)
                                        tempText += SPDPacket.toCPkgCommDefinitionString("package");
                                    tempText += "\t\tdefault:break;\n\t}\n}\n";

                                    break;
                            }
                        case 5:     // Package Method Definitions
                            {
                                break;
                            }
                        case 6:     // Comm Method Definitions
                            {
                                break;
                            }
                        case 7:     // Package Method Prototypes
                            {
                                break;
                            }
                        case 8:     // Comm Method Prototypes
                            {
                                break;
                            }
                            case 9:// Packet Structure Pointers
                                {
                                    tempText += "//////////////////////////////////////////////////////////\n//\n// Auto-Gen Packet Structure Pointers \n//\n//////////////////////////////////////////////////////////\n";
                                    // Loop all Static Packets in Comm System
                                    foreach (SerialParameterPacket SPDPacket in StaticSPDPackets)
                                        tempText += SPDPacket.toCPacketStructPointersString();
                                    break;
                                }
                        default:
                            {
                                break;
                            }                          
                        }
                        if(tempText!="")
                        {
                            int tempIndex = FileTexts[i].IndexOf(keyString) + keyString.Length;
                            FileTexts[i] = FileTexts[i].Insert(tempIndex, string.Concat("\n", tempText, "\n"));
                        }
                        
                    }
                }
            }

            // (re)Capture
            BaseHText =     FileTexts[0];
            BaseCText =     FileTexts[1];
            ClassHppText =  FileTexts[2];
            ClassCppText =  FileTexts[3];
        }
    }
}
