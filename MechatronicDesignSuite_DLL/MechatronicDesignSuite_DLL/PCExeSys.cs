using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

namespace MechatronicDesignSuite_DLL
{
    [Serializable]
    public class PCExeSysMetaData
    {
        [NonSerialized]
        FileStream MDFileStream;
        [NonSerialized]
        BinaryFormatter MDFileFormatter;
        string executableDirectoryString;
        string MetaDataPathString;
        List<string> SysModuleLibraryPathStrings;
        List<string> SysModuleProjectPathStrings;

        [NonSerialized]
        PCExeSysMetaData TempSerializationContainer;
        public PCExeSysMetaData()
        {
            executableDirectoryString = Path.GetFullPath(Directory.GetCurrentDirectory());
            if(Directory.Exists(executableDirectoryString))
            {
                MetaDataPathString = Path.Combine(executableDirectoryString, "MDSMetaData.md");
                if (File.Exists(MetaDataPathString))
                {
                    MDFileStream = File.OpenRead(MetaDataPathString);
                    MDFileFormatter = new BinaryFormatter();
                    TempSerializationContainer = (PCExeSysMetaData)MDFileFormatter.Deserialize(MDFileStream);
                    MDFileStream.Close();
                }
                else
                {
                    MDFileStream = File.OpenWrite(MetaDataPathString);
                    MDFileFormatter = new BinaryFormatter();
                    MDFileFormatter.Serialize(MDFileStream, this);
                    MDFileStream.Close();
                }

            }
        }
        public void AddLibraryPathString(string LibPathStringIn)
        {
            if (SysModuleLibraryPathStrings == null)
                SysModuleLibraryPathStrings = new List<string>();

            if (SysModuleLibraryPathStrings.Count == 0)
                SysModuleLibraryPathStrings.Add(Path.GetFullPath(LibPathStringIn));
            else
                SysModuleLibraryPathStrings.Insert(0, Path.GetFullPath(LibPathStringIn));

            if (SysModuleLibraryPathStrings.Count > 10)
                SysModuleLibraryPathStrings.RemoveAt(10);

            MDFileStream = File.Open(MetaDataPathString, FileMode.Truncate);
            MDFileFormatter.Serialize(MDFileStream, this);
            MDFileStream.Close();
        }
        public void AddProjectPathString(string ProjPathStringIn)
        {
            if (SysModuleProjectPathStrings == null)
                SysModuleProjectPathStrings = new List<string>();

            if (SysModuleProjectPathStrings.Count == 0)
                SysModuleProjectPathStrings.Add(Path.GetFullPath(ProjPathStringIn));
            else
                SysModuleProjectPathStrings.Insert(0, Path.GetFullPath(ProjPathStringIn));

            if (SysModuleProjectPathStrings.Count > 10)
                SysModuleProjectPathStrings.RemoveAt(10);

            MDFileStream = File.Open(MetaDataPathString, FileMode.Truncate);
            MDFileFormatter.Serialize(MDFileStream, this);
            MDFileStream.Close();
        }

    }
    public class imsException
    {
        public string Source
        {
            set {; }
            get { return thisException.Source; }
        }
        public string Message
        {
            set {; }
            get { return thisException.Message; }
        }
        public string StackTrace
        {
            set {; }
            get { return thisException.StackTrace; }
        }
        public string ThreadID
        {
            set {; }
            get { return ThreadIDString; }
        }

        public Exception thisException;
        public string ThreadIDString = "";
        public UInt32 guiBGWorkerIndex = 0xffffffff, guiTimerIndex = 0xffffffff;
        public override string ToString()
        {
            return string.Concat("Caught Exception Thread ID: ", ThreadIDString, "\nCaught Exception BGWorkerIndex: ", guiBGWorkerIndex.ToString(), "\n\n", thisException.ToString());
        }
        public TreeNode toNewTreeNode()
        {
            TreeNode outNode = new TreeNode();
            outNode.Text = ThreadIDString;// + " " + thisException.Message;
            outNode.Tag = this;
            return outNode;
        }
    }
    public class PCExeSys: IDisposable
    {
        public BackgroundWorker ExtAppBGWorkerLink
        {
            set {; }
            get { return guiBGWorkers[1]; }
        }
        public Timer GUITimerLink
        {
            set {; }
            get { return guiTimers[0]; }
        }
        public List<BackgroundWorker> BGWorkersList
        {
            set {; }
            get { return guiBGWorkers; }
        }
        public imsProjectModuleNode ProjModNodeProperty
        {
            set {; }
            get { return (imsProjectModuleNode)APISysModules[2]; }
        }
        public imsBGThreadManager BGManagerNode
        {
            get { return (imsBGThreadManager)APISysModules[1]; }
        }
        MechatronicDesignSuiteForm LinkedMDSForm;

        List<Timer> guiTimers = new List<Timer>();
        List<BackgroundWorker> guiBGWorkers = new List<BackgroundWorker>();
        List<imsException> ExceptionLog = new List<imsException>();
        public List<imsAPISysModule> APISysModules = new List<imsAPISysModule>();
        List<imsBaseNode> globalNodeList = new List<imsBaseNode>();
        public bool DeSerializingSystem = false;
        public bool DeSerializationRequested = false;
        public PCExeSys(MechatronicDesignSuiteForm MDSFormIn)
        {
            if (MDSFormIn != null)
                LinkedMDSForm = MDSFormIn;
            CallEntryPointFunction(MainInit);
        }
        
        private void AnalyzeMainForm(MechatronicDesignSuiteForm MDSFormIn)
        {
            // loop through all windows form controls           
            foreach (Control guiCtrl in MDSFormIn.Controls)
            {
                ;
            }
        }
        private void InitializeExecutionSystem()
        {
            if (guiTimers != null)
                if (guiTimers.Count != 0)
                    guiTimers.Clear();

            if (guiBGWorkers != null)
                if (guiBGWorkers.Count != 0)
                    guiBGWorkers.Clear();

            // create timer and background tasks
            guiTimers.Add(new Timer());
            GUITimerLink.Interval = 100;                             // 10 Hz primary guiupdate loop
            GUITimerLink.Tick += new EventHandler(GUITimer_Tick);    // Link to Main Entry Point

            guiBGWorkers.Add(new BackgroundWorker());
            guiBGWorkers[0].DoWork += new DoWorkEventHandler(CommBGThread_DoWork);

            guiBGWorkers.Add(new BackgroundWorker());
            ExtAppBGWorkerLink.DoWork += new DoWorkEventHandler(ExternalAppThread_DoWork);

            guiBGWorkers.Add(new BackgroundWorker());
            guiBGWorkers[2].DoWork += new DoWorkEventHandler(SimThread_DoWork);

            foreach (BackgroundWorker bgWorker in guiBGWorkers)
            {
                bgWorker.WorkerSupportsCancellation = true;
                bgWorker.WorkerReportsProgress = true;
            }

        }

        private void GUITimer_Tick(object sender, EventArgs e)
        {
            CallEntryPointFunction(MainLoop);
        }
        private void CommBGThread_DoWork(object sender, DoWorkEventArgs e)
        {
            CallEntryPointFunction(CommsBGThread);
        }
        private void ExternalAppThread_DoWork(object sender, DoWorkEventArgs e)
        {
            CallEntryPointFunction(ExtAppBGThread);
        }
        private void SimThread_DoWork(object sender, DoWorkEventArgs e)
        {
            CallEntryPointFunction(SimBGThread);
        }
        public void SerializePCSystem(string pathString)
        {
            BinaryFormatter SerializeFormatter = new BinaryFormatter();
            FileStream SerializeFileStream = new FileStream(pathString, FileMode.Truncate);
            foreach (imsBaseNode node2Serialize in globalNodeList)
            {
                node2Serialize.writeNode2file(SerializeFormatter, SerializeFileStream);
                //SerializeFormatter.Serialize(SerializeFileStream, node2Serialize);
            }
            SerializeFileStream.Close();
        }
        public void DeserializePCSystem(string pathString)
        {
            int nodeIndex;
            BinaryFormatter DeSerializeFormatter = new BinaryFormatter();
            FileStream DeSerializeFileStream = new FileStream(pathString, FileMode.Open);
            object[] cTorParams = new object[] { DeSerializeFormatter, DeSerializeFileStream };
            long startingFP; imsBaseNode tempBaseNode;
            Assembly tempAss = typeof(imsBaseNode).Assembly;
            Type[] tempTypes = tempAss.GetTypes();
            List<imsBaseNode> tempGlobalNodeList = new List<imsBaseNode>();
            while (DeSerializeFileStream.Position < DeSerializeFileStream.Length)
            {
                startingFP = DeSerializeFileStream.Position;
                tempBaseNode = new imsBaseNode(DeSerializeFormatter, DeSerializeFileStream);
                if (tempTypes.Contains(tempBaseNode.NodeType))
                {
                    DeSerializeFileStream.Position = startingFP;
                    tempGlobalNodeList.Add((imsBaseNode)(Activator.CreateInstance(tempBaseNode.NodeType, cTorParams)));
                }   
                // if the node ids don't line up...
                if(tempGlobalNodeList[tempGlobalNodeList.Count-1].GlobalNodeID != tempGlobalNodeList.FindIndex(x=>x.GlobalNodeID== tempBaseNode.GlobalNodeID))
                {
                    DeSerializeFileStream.Close();
                    throw (new Exception("DeSerialized Node ID's dont Line Up"));
                }
            }
            DeSerializeFileStream.Close();


            for (nodeIndex = 0; nodeIndex < globalNodeList.Count; nodeIndex++)
                globalNodeList[nodeIndex].Dispose();

            globalNodeList.Clear();

            foreach (imsBaseNode deSerialNode in tempGlobalNodeList)
                globalNodeList.Add(deSerialNode);

            
            DeSerializingSystem = false;
        }
        public void PopulateTreeView(TreeView TreeViewIn)
        {
            if(TreeViewIn.Nodes.Count == 0)
            {
                // Populate Node 0 with ExeSys API Modules
                TreeViewIn.Nodes.Add(new TreeNode("ExeSys API"));
                foreach(imsAPISysModule apiMod in APISysModules)
                {
                    TreeViewIn.Nodes[0].Nodes.Add(apiMod.toNewTreeNode());
                    if(apiMod.NodeType == typeof(imsProjectModuleNode))
                    {
                        TreeViewIn.Nodes[0].Nodes[TreeViewIn.Nodes[0].Nodes.Count - 1].Text = "Project Settings";
                    }
                    else if (apiMod.NodeType == typeof(imsPCClocksModule))
                    {
                        TreeViewIn.Nodes[0].Nodes[TreeViewIn.Nodes[0].Nodes.Count - 1].Text = "Application Timing";
                    }
                    else if (apiMod.NodeType == typeof(imsBGThreadManager))
                    {
                        TreeViewIn.Nodes[0].Nodes[TreeViewIn.Nodes[0].Nodes.Count - 1].Text = "BG Thread Manager";
                    }
                }

                // Populate Node 1 with Active Project Modules
                TreeViewIn.Nodes.Add(ProjModNodeProperty.toNewTreeNode());
                TreeViewIn.Nodes[1].Text = "New Project";
                //TreeViewIn.Refresh();
            }
            else if(ProjModNodeProperty.ActiveProjectPath != "")
            {

                TreeViewIn.Nodes.Clear();
                // Populate Node 0 with ExeSys API Modules
                TreeViewIn.Nodes.Add(new TreeNode("ExeSys API"));
                foreach (imsAPISysModule apiMod in APISysModules)
                {
                    TreeViewIn.Nodes[0].Nodes.Add(apiMod.toNewTreeNode());
                    if (apiMod.NodeType == typeof(imsProjectModuleNode))
                    {
                        TreeViewIn.Nodes[0].Nodes[TreeViewIn.Nodes[0].Nodes.Count - 1].Text = "Project Settings";
                    }
                    else if (apiMod.NodeType == typeof(imsPCClocksModule))
                    {
                        TreeViewIn.Nodes[0].Nodes[TreeViewIn.Nodes[0].Nodes.Count - 1].Text = "Application Timing";
                    }
                    else if (apiMod.NodeType == typeof(imsBGThreadManager))
                    {
                        TreeViewIn.Nodes[0].Nodes[TreeViewIn.Nodes[0].Nodes.Count - 1].Text = "BG Thread Manager";
                    }
                }

                // Populate Node 1 with Active Project Modules
                TreeViewIn.Nodes.Add(ProjModNodeProperty.toNewTreeNode());
            }
        }
        public void PopulateExceptionTreeView(TreeView TreeViewIn)
        {
            if (TreeViewIn.Nodes.Count == 0)
            {
                foreach(imsException excp in ExceptionLog)
                {
                    TreeViewIn.Nodes.Add(excp.toNewTreeNode());
                    TreeViewIn.Nodes[TreeViewIn.Nodes.Count - 1].ToolTipText = excp.StackTrace;
                }
            }
        }

        private int MainInit()
        {
            InitializeExecutionSystem();

            APISysModules.Add(new imsPCClocksModule(this, globalNodeList));
            APISysModules.Add(new imsBGThreadManager(this, globalNodeList));
            APISysModules.Add(new imsProjectModuleNode(this, globalNodeList));



            foreach (imsSysModuleNode sysModNode in APISysModules)
                sysModNode.MainInit();

            return 0;
        }
        private int MainLoop()
        {
            if (!DeSerializingSystem)
            {
                foreach (imsSysModuleNode sysModNode in APISysModules)
                    sysModNode.MainLoop();
            }
            else if(DeSerializationRequested)
            {
                foreach (imsSysModuleNode sysModNode in APISysModules)
                    sysModNode.MainLoop();
            }

            return 0;
        }
        private int CommsBGThread()
        {
            
            return 0;
        }
        private int ExtAppBGThread()
        {
            if(!DeSerializingSystem)
            {
                foreach (imsSysModuleNode sysModNode in APISysModules)
                    sysModNode.ExtAppBGThread();
            }
            else if (DeSerializationRequested)
            {
                DeSerializationRequested = false;
                foreach (imsSysModuleNode sysModNode in APISysModules)
                    sysModNode.ExtAppBGThread();
                APISysModules.Clear();
                foreach (imsBaseNode deSerialNode in globalNodeList)
                {
                    if (typeof(imsAPISysModule).IsInstanceOfType(deSerialNode))
                        ((imsAPISysModule)deSerialNode).deSerializeSetAPILinks(this);
                    if (typeof(imsSysModuleNode).IsInstanceOfType(deSerialNode))
                        ((imsSysModuleNode)deSerialNode).deSerializeLinkNodes(globalNodeList);

                }
                DeSerializingSystem = false;
            }


            
            return 0;
        }
        private int SimBGThread()
        {
            return 0;
        }

        private void CallEntryPointFunction(Func<int> EntryPoint)
        {
            try
            {
                EntryPoint();
            }
            catch (Exception caughtExcpIn)
            {
                ExceptionLog.Add(new imsException());
                ExceptionLog[ExceptionLog.Count - 1].thisException = caughtExcpIn;
                if (EntryPoint == MainLoop)
                    ExceptionLog[ExceptionLog.Count - 1].ThreadIDString = "MainLoop";
                else if (EntryPoint == MainInit)
                    ExceptionLog[ExceptionLog.Count - 1].ThreadIDString = "MainInit";
                else if (EntryPoint == CommsBGThread)
                    ExceptionLog[ExceptionLog.Count - 1].ThreadIDString = "CommsBGThread";
                else if (EntryPoint == ExtAppBGThread)
                {
                    ExceptionLog[ExceptionLog.Count - 1].ThreadIDString = "ExtAppBGThread";
                    BGManagerNode.StopBGWorker(ExceptionLog[ExceptionLog.Count - 1].ThreadIDString);
                }
                else if (EntryPoint == SimBGThread)
                    ExceptionLog[ExceptionLog.Count - 1].ThreadIDString = "SimBGThread";

                LinkedMDSForm.BeginInvoke(new Action(() => {
                    MessageBox.Show(LinkedMDSForm, ExceptionLog[ExceptionLog.Count - 1].ToString(), "Caught an Exception");
                }));
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PCExeSys() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
