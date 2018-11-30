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
using System.Globalization;
using MechatronicDesignSuite_DLL.BaseNodes;

namespace MechatronicDesignSuite_DLL
{
    public class MetaDataClassConverter : ExpandableObjectConverter
    {
        // functions for property grid expandability
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return base.GetPropertiesSupported(context);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
        {
            if (destinationType == typeof(PCExeSysMetaData))
                return true;

            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
        {
            if (destinationType == typeof(System.String) && value is PCExeSysMetaData)
            {

                PCExeSysMetaData so = (PCExeSysMetaData)value;
                return "MetaData";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
    [Serializable]
    [TypeConverterAttribute(typeof(MetaDataClassConverter)), DescriptionAttribute("Expand to see...")]
    public class PCExeSysMetaData
    {
        public static string MetaDataPathString;
        public List<string> SysModuleLibraryPathStrings { set; get; }
        public List<string> SysModuleProjectPathStrings { set; get; }
        public bool TryLoadLastPrj { set; get; } = false;
        //public static string MetaDataPathString = "";// 
        public static void saveMetaData(PCExeSysMetaData toBeSaved)
        {
            if (toBeSaved != null)
            {
                FileStream MDFileStream = File.Open(MetaDataPathString, FileMode.Truncate);
                BinaryFormatter MDFileFormatter = new BinaryFormatter();
                MDFileFormatter.Serialize(MDFileStream, toBeSaved);
                MDFileStream.Close();

                //FileStream MDFileStream = File.OpenWrite(MetaDataPathString);
                //BinaryFormatter MDFileFormatter = new BinaryFormatter();
                //MDFileFormatter.Serialize(MDFileStream, toBeSaved);
                //MDFileStream.Close();
            }
        }
        public static PCExeSysMetaData generateMetaData()
        {
            string executableDirectoryString = Path.GetFullPath(Directory.GetCurrentDirectory());
            if (Directory.Exists(executableDirectoryString))
            {
                MetaDataPathString = Path.Combine(executableDirectoryString, "MDSMetaData.md");
                if (File.Exists(MetaDataPathString))
                {
                    FileStream MDFileStream = File.OpenRead(MetaDataPathString);
                    BinaryFormatter MDFileFormatter = new BinaryFormatter();
                    PCExeSysMetaData TempDeserializationContainer = (PCExeSysMetaData)MDFileFormatter.Deserialize(MDFileStream);
                    MDFileStream.Close();
                    return TempDeserializationContainer;
                }
                else
                {
                    return new PCExeSysMetaData();
                }

                
                

            }
            return null;
        }

        public PCExeSysMetaData()
        {

            

        }
        public void AddLibraryPathString(string LibPathStringIn)
        {
            if (SysModuleLibraryPathStrings == null)
                SysModuleLibraryPathStrings = new List<string>();

            if (SysModuleLibraryPathStrings.Count == 0)
                SysModuleLibraryPathStrings.Add(Path.GetFullPath(LibPathStringIn));
            else if (!SysModuleLibraryPathStrings.Contains(Path.GetFullPath(LibPathStringIn)))
            {
                int IndeX = -1;
                IndeX = SysModuleLibraryPathStrings.FindIndex(x => x == Path.GetFullPath(LibPathStringIn));
                if (IndeX >= 0 && IndeX < SysModuleLibraryPathStrings.Count)
                    SysModuleLibraryPathStrings.RemoveAt(IndeX);
                SysModuleLibraryPathStrings.Insert(0, Path.GetFullPath(LibPathStringIn));
            }

            if (SysModuleLibraryPathStrings.Count > 10)
                SysModuleLibraryPathStrings.RemoveAt(10);

            saveMetaData(this);
        }
        public void AddProjectPathString(string ProjPathStringIn)
        {
            if (SysModuleProjectPathStrings == null)
                SysModuleProjectPathStrings = new List<string>();

            if (SysModuleProjectPathStrings.Count == 0)
                SysModuleProjectPathStrings.Add(Path.GetFullPath(ProjPathStringIn));
            else if (SysModuleProjectPathStrings.Contains(Path.GetFullPath(ProjPathStringIn)))
            {
                int IndeX = -1;
                IndeX = SysModuleProjectPathStrings.FindIndex(x=>x== Path.GetFullPath(ProjPathStringIn));
                if (IndeX >= 0 && IndeX < SysModuleProjectPathStrings.Count)
                    SysModuleProjectPathStrings.RemoveAt(IndeX);
                SysModuleProjectPathStrings.Insert(0, Path.GetFullPath(ProjPathStringIn));
            }
            else
            {
                SysModuleProjectPathStrings.Insert(0, Path.GetFullPath(ProjPathStringIn));
            }

            if (SysModuleProjectPathStrings.Count > 10)
                SysModuleProjectPathStrings.RemoveAt(10);

            saveMetaData(this);
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
    public class PCExeSys : IDisposable
    {

        #region PC Execution System Properties
        public BackgroundWorker CommThreadBGWorkerLink
        {
            get { return guiBGWorkers[0]; }
        }
        public BackgroundWorker ExtAppBGWorkerLink
        {
            get { return guiBGWorkers[1]; }
        }
        public Timer GUITimerLink
        {
            get { return guiTimers[0]; }
        }

        public List<BackgroundWorker> BGWorkersList
        {
            get { return guiBGWorkers; }
        }
        public imsProjectModuleNode ProjModNodeProperty
        {
            get
            {
                if (APISysModules != null)
                    if (APISysModules.Count > 2)
                        return (imsProjectModuleNode)APISysModules[2];
                return null;
            }
        }
        public imsPlotModule PlotModuleProperty
        {
            get
            {
                if (APISysModules != null)
                    if (APISysModules.Count > 3)
                        return (imsPlotModule)APISysModules[3];
                return null;
            }
        }
        public imsBGThreadManager BGManagerNode
        {
            get { return (imsBGThreadManager)APISysModules[1]; }
        }
        public bool EnableMainLoop { set; get; } = true;
        public bool EnableExtAppThread { set; get; } = true;
        public bool EnableCommsThread { set; get; } = true;
        public bool EnableSimThread { set; get; } = true;
        #endregion

        #region PC Execution System Fields
        public MechatronicDesignSuiteForm LinkedMDSForm;

        List<Timer> guiTimers = new List<Timer>();
        List<BackgroundWorker> guiBGWorkers = new List<BackgroundWorker>();
        List<imsException> ExceptionLog = new List<imsException>();

        public imsProjectExplorer thisPrjViewer;
        public imsExceptionViewer thisExcpViewer;

        public PCExeSysMetaData pcExecutionSystemMetaData;
        public string ActiveProjectPath { get { return activeProjectpath; } set { if (value != "") activeProjectpath = value; } }
        string activeProjectpath = "";

        string RequestedProjectPath = "";
        string SaveToProjectPath = "";
        
        public string ProjectPathRequestedforOpen
        {
            set
            {
                if (File.Exists(value))
                    RequestedProjectPath = value;
                else if (Directory.Exists(value))
                    RequestedProjectPath = value;
            }
            get { return RequestedProjectPath; }
        }
        public string ProjectPathRequestedforSave
        {
            set
            {
                if (File.Exists(value))
                    SaveToProjectPath = value;
                else
                {
                    FileStream FS = File.Create(value);
                    if (FS != null)
                        FS.Close();
                    if (File.Exists(value))
                        SaveToProjectPath = value;
                }
            }
            get { return SaveToProjectPath; }
        }

        /// <summary>
        /// Executional Modules of the PC Exe Sys (API Modules)
        /// </summary>
        public List<imsAPISysModule> APISysModules = new List<imsAPISysModule>();
        /// <summary>
        /// Global List of all Nodes that can be serialized and/or deserialized
        /// </summary>
        public List<imsBaseNode> globalNodeList = new List<imsBaseNode>();
        /// <summary>
        /// List of imported dll assemblies from which modules can be instantied/deserialized
        /// </summary>
        protected List<Assembly> importedDLLs = new List<Assembly>();
        #endregion

        
        /// <summary>
        /// The PCExeSys Constructor
        /// - Intended use: to be called by PCExeSysForm during its construction/instantiation
        /// - Establishes link between execution system and main PC form
        /// - Calls maininit function of the execution system
        /// </summary>
        /// <param name="MDSFormIn"></param>
        public PCExeSys(MechatronicDesignSuiteForm MDSFormIn)
        {
            if (MDSFormIn != null)
                LinkedMDSForm = MDSFormIn;
            pcExecutionSystemMetaData = PCExeSysMetaData.generateMetaData();
            InitializeExecutionSystem();
            ToolStripDropDownButton ExeSysMenuItems = null;
            foreach (Control ctr in LinkedMDSForm.Controls)
            {
                if(ctr.GetType()==typeof(MenuStrip))
                {
                    LinkedMDSForm.MainMenuStrip = (MenuStrip)ctr;
                    ExeSysMenuItems = new ToolStripDropDownButton("PCExeSys");
                    LinkedMDSForm.MainMenuStrip.Items.Add(ExeSysMenuItems);
                    ExeSysMenuItems.DropDownItems.Add("Project Explorer", null, viewProjectExplorerToolStripMenuItem_Click);
                    ExeSysMenuItems.DropDownItems.Add("Exception Log", null, viewExceptionLogToolStripMenuItem_Click);
                    ExeSysMenuItems.DropDownItems.Add("Open New Plot Pane", null, viewPlotPaneMenuItem_Click);
                    LinkedMDSForm.MainMenuStrip.Parent = LinkedMDSForm;
                    LinkedMDSForm.MainMenuStrip.Visible = true;
                    LinkedMDSForm.MainMenuStrip.Show();
                }
                else if(ctr.GetType()==typeof(ToolStrip))
                {
                    ExeSysMenuItems = new ToolStripDropDownButton("PCExeSys");
                    ((ToolStrip)ctr).Items.Add(ExeSysMenuItems);
                    ExeSysMenuItems.DropDownItems.Add("Project Explorer", null, viewProjectExplorerToolStripMenuItem_Click);
                    ExeSysMenuItems.DropDownItems.Add("Exception Log", null, viewExceptionLogToolStripMenuItem_Click);
                    ExeSysMenuItems.DropDownItems.Add("Open New Plot Pane", null, viewPlotPaneMenuItem_Click);
                    ((ToolStrip)ctr).Parent = LinkedMDSForm;
                    ((ToolStrip)ctr).Visible = true;
                    ((ToolStrip)ctr).Show();
                }
            }
            if(ExeSysMenuItems==null)
            {
                LinkedMDSForm.MainMenuStrip = new MenuStrip();
                LinkedMDSForm.MainMenuStrip.Dock = DockStyle.Top;
                ExeSysMenuItems = new ToolStripDropDownButton("PCExeSys");
                LinkedMDSForm.MainMenuStrip.Items.Add(ExeSysMenuItems);
                ExeSysMenuItems.DropDownItems.Add("Project Explorer", null, viewProjectExplorerToolStripMenuItem_Click);
                ExeSysMenuItems.DropDownItems.Add("Exception Log", null, viewExceptionLogToolStripMenuItem_Click);
                ExeSysMenuItems.DropDownItems.Add("Open New Plot Pane", null, viewPlotPaneMenuItem_Click);
                LinkedMDSForm.MainMenuStrip.Parent = LinkedMDSForm;
                LinkedMDSForm.MainMenuStrip.Visible = true;
                LinkedMDSForm.MainMenuStrip.Show();
            }
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
            guiBGWorkers[1].DoWork += new DoWorkEventHandler(ExternalAppThread_DoWork);
  

            guiBGWorkers.Add(new BackgroundWorker());
            guiBGWorkers[2].DoWork += new DoWorkEventHandler(SimThread_DoWork);

            foreach (BackgroundWorker bgWorker in guiBGWorkers)
            {
                bgWorker.WorkerSupportsCancellation = true;
                bgWorker.WorkerReportsProgress = true;
            }

            importedDLLs.Add(typeof(imsBaseNode).Assembly);
        }
        public void InstantiateNewMainFormStaticGUIModule(Assembly AssIn, imsSysModuleNode FSFormModIn, int MainLoopInterval)
        {
            InstantiateNewAPIModules();
            AddDlltoProject(AssIn);
            ProjModNodeProperty.AddSysModuletoProject(FSFormModIn);
            GUITimerLink.Interval = MainLoopInterval;
        }
        


        #region PC Execution System Main Entry Point Callbacks
        /// <summary>
        /// MainInit() is a primary entry point function for the PC exe sys
        /// - Here the Execution System is Initialized and all modules MainInit() functions are called
        /// - This code is executed only during initialization of the execution system, initialization of the main form
        /// </summary>
        /// <returns></returns>
        private int MainInit()
        {
            foreach (imsSysModuleNode sysModNode in APISysModules)
                if(!sysModNode.getisInitialized)
                    sysModNode.MainInit();

            return 0;
        }
        private int MainLoop()
        {
            MainLoopProjectCheck();

            if (EnableMainLoop)
            {
                foreach (imsSysModuleNode sysModNode in APISysModules)
                    if(sysModNode.getisInitialized)
                        sysModNode.MainLoop();
            }

            return 0;
        }
        private int CommsBGThread()
        {
            if (EnableCommsThread)
            {
                foreach (imsSysModuleNode sysModNode in APISysModules)
                {
                    if (sysModNode.getisInitialized)
                    {
                        if (typeof(imsCyclicPacketCommSystem).IsAssignableFrom(sysModNode.GetType()))
                        {
                            ((imsCyclicPacketCommSystem)sysModNode).CommThreadExe();
                        }
                    }
                        
                }
            }
            return 0;
        }
        private int ExtAppBGThread()
        {
            ExtAppOPenCloseProj();

            if (EnableExtAppThread)
            {
                foreach (imsSysModuleNode sysModNode in APISysModules)
                    if(sysModNode.getisInitialized)
                        sysModNode.ExtAppBGThread();
            }
            return 0;
        }
        private int SimBGThread()
        {
            if (EnableSimThread)
            {
                //foreach ()
                //      sysModNode.S();
            }
            return 0;
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
        public void CallEntryPointFunction(Func<int> EntryPoint)
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
                {
                    ExceptionLog[ExceptionLog.Count - 1].ThreadIDString = "MainLoop";
                    EnableMainLoop = false;
                }
                else if (EntryPoint == MainInit)
                    ExceptionLog[ExceptionLog.Count - 1].ThreadIDString = "MainInit";
                else if (EntryPoint == CommsBGThread)
                {
                    ExceptionLog[ExceptionLog.Count - 1].ThreadIDString = "CommsBGThread";
                    BGManagerNode.StopBGWorker(ExceptionLog[ExceptionLog.Count - 1].ThreadIDString);
                }
                else if (EntryPoint == ExtAppBGThread)
                {
                    ExceptionLog[ExceptionLog.Count - 1].ThreadIDString = "ExtAppBGThread";
                    BGManagerNode.StopBGWorker(ExceptionLog[ExceptionLog.Count - 1].ThreadIDString);
                }
                else if (EntryPoint == SimBGThread)
                    ExceptionLog[ExceptionLog.Count - 1].ThreadIDString = "SimBGThread";
                else
                    ExceptionLog[ExceptionLog.Count - 1].ThreadIDString = "CallbackFunction";

                LinkedMDSForm.BeginInvoke(new Action(() => {
                    DialogResult dsults =  MessageBox.Show(LinkedMDSForm, ExceptionLog[ExceptionLog.Count - 1].ToString(), "Caught an Exception");
                    if(dsults == DialogResult.OK)
                    {
                        if (EntryPoint == MainLoop && !EnableMainLoop)
                            EnableMainLoop = true;

                    }
                }));
            }
        }
        #endregion

        public void AddDlltoProject(Assembly ModuleAssembly)
        {
            importedDLLs.Add(ModuleAssembly);
            
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
            List<Type> AllTypes = new List<Type>();
            foreach (Assembly tempAss in importedDLLs)
            {
                foreach(Type tempType in tempAss.GetTypes())
                {
                    AllTypes.Add(tempType);
                }
            }
            List<imsBaseNode> tempGlobalNodeList = new List<imsBaseNode>();

            while (DeSerializeFileStream.Position < DeSerializeFileStream.Length)
            {
                startingFP = DeSerializeFileStream.Position;
                tempBaseNode = new imsBaseNode(DeSerializeFormatter, DeSerializeFileStream);

                if (AllTypes.Contains(tempBaseNode.getNodeType))
                {
                    DeSerializeFileStream.Position = startingFP;
                    tempGlobalNodeList.Add((imsBaseNode)(Activator.CreateInstance(tempBaseNode.getNodeType, cTorParams)));
                }
                else
                {
                    DeSerializeFileStream.Close();
                    throw (new Exception("Deserialized unknown node type from file"));
                }


                // if the node ids don't line up...
                if (tempGlobalNodeList[tempGlobalNodeList.Count - 1].getGlobalNodeID != tempGlobalNodeList.FindIndex(x => x.getGlobalNodeID == tempBaseNode.getGlobalNodeID))
                {
                    DeSerializeFileStream.Close();
                    throw (new Exception("DeSerialized Node ID's dont Line Up"));
                }
            }
            DeSerializeFileStream.Close();

            APISysModules.Clear();
            globalNodeList.Clear();

            foreach (imsBaseNode deSerialNode in tempGlobalNodeList)
            {
                globalNodeList.Add(deSerialNode);

                if (typeof(imsAPISysModule).IsInstanceOfType(deSerialNode))
                {
                    ((imsAPISysModule)deSerialNode).deSerializeSetAPILinks(this);
                }
            }
            foreach (imsBaseNode deSerialNode in tempGlobalNodeList)
            {
                if (typeof(imsSysModuleNode).IsInstanceOfType(deSerialNode))
                {
                    ((imsSysModuleNode)deSerialNode).deSerializeLinkNodes(globalNodeList);
                }
            }


        }
        public void PopulateTreeView(TreeView TreeViewIn)
        {
            TreeViewIn.Nodes.Clear();
            // Populate Node 0 with ExeSys API Modules
            if(APISysModules.Count>0)
            {
                if(ActiveProjectPath!="")
                    TreeViewIn.Nodes.Add(new TreeNode(Path.GetFileNameWithoutExtension(ActiveProjectPath)));
                else
                    TreeViewIn.Nodes.Add(new TreeNode(Path.GetFileNameWithoutExtension("New Project File Name")));

                foreach (imsAPISysModule apiMod in APISysModules)
                {
                    TreeViewIn.Nodes[0].Nodes.Add(apiMod.toNewTreeNode());
                    if (apiMod.getNodeType == typeof(imsProjectModuleNode))
                    {
                        TreeViewIn.Nodes[0].Nodes[TreeViewIn.Nodes[0].Nodes.Count - 1].Text = "Project Settings";
                        TreeViewIn.Nodes[0].Nodes[TreeViewIn.Nodes[0].Nodes.Count - 1].Nodes.Clear();
                    }
                    else if (apiMod.getNodeType == typeof(imsPCClocksModule))
                    {
                        TreeViewIn.Nodes[0].Nodes[TreeViewIn.Nodes[0].Nodes.Count - 1].Text = "Application Timing";
                    }
                    else if (apiMod.getNodeType == typeof(imsBGThreadManager))
                    {
                        TreeViewIn.Nodes[0].Nodes[TreeViewIn.Nodes[0].Nodes.Count - 1].Text = "BG Thread Manager";
                    }
                }
                // Populate Node 1 with Active Project Modules
                TreeViewIn.Nodes.Add(ProjModNodeProperty.toNewTreeNode());
            }
            TreeViewIn.Refresh();



        }
        public void PopulateExceptionTreeView(TreeView TreeViewIn)
        {
            if (TreeViewIn.Nodes.Count == 0)
            {
                foreach (imsException excp in ExceptionLog)
                {
                    TreeViewIn.Nodes.Add(excp.toNewTreeNode());
                    TreeViewIn.Nodes[TreeViewIn.Nodes.Count - 1].ToolTipText = excp.StackTrace;
                }
            }
        }
        public void PopulateLibraryTreeView(TreeView TreeViewIn)
        {
            TreeViewIn.Nodes.Clear();
            foreach(Assembly AssLooper in importedDLLs)
            {
                TreeViewIn.Nodes.Add(string.Concat(AssLooper.GetName().Name, " ", AssLooper.GetName().Version.ToString()));
                TreeViewIn.Nodes[TreeViewIn.Nodes.Count - 1].Tag = AssLooper;
                foreach (Type AssType in AssLooper.ExportedTypes)
                {
                    if(typeof(imsSysModuleNode).IsAssignableFrom(AssType))
                    {
                        TreeViewIn.Nodes[TreeViewIn.Nodes.Count - 1].Nodes.Add(AssType.Name);
                        TreeViewIn.Nodes[TreeViewIn.Nodes.Count - 1].Nodes[TreeViewIn.Nodes[TreeViewIn.Nodes.Count - 1].Nodes.Count - 1].Tag = AssType;
                    }
                    
                }
            }
        }

        private void viewProjectExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CallEntryPointFunction(LaunchNewProjectExplorer);
        }
        private void viewExceptionLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CallEntryPointFunction(LaunchNewExceptionLog);
        }
        private void viewPlotPaneMenuItem_Click(object sender, EventArgs e)
        {
            CallEntryPointFunction(LaunchNewPlotPane);
        }
        public int PromptSaveProject2File()
        {
            SaveFileDialog SaveProjectDialog = new SaveFileDialog();
            SaveProjectDialog.Title = "Save Project File as (*.imsprj)";
            SaveProjectDialog.Filter = @"project files|*.imsPrj;";
            //SaveProjectDialog.CheckPathExists = true;
            //SaveProjectDialog.CheckFileExists = true;
            if (pcExecutionSystemMetaData.SysModuleProjectPathStrings != null)
            {
                if (pcExecutionSystemMetaData.SysModuleProjectPathStrings.Count > 0)
                    SaveProjectDialog.InitialDirectory = Path.GetDirectoryName(pcExecutionSystemMetaData.SysModuleProjectPathStrings[0]);
            }
            DialogResult theseResults = SaveProjectDialog.ShowDialog();
            if (theseResults == DialogResult.OK)
            {
                if (!File.Exists(SaveProjectDialog.FileName) && SaveProjectDialog.FileName != "")
                {
                    FileStream tfs = File.Create(SaveProjectDialog.FileName);
                    if (tfs != null)
                    {
                        ProjectPathRequestedforSave = SaveProjectDialog.FileName;
                        tfs.Close();
                    }
                }
                else if (File.Exists(SaveProjectDialog.FileName))
                {
                    ProjectPathRequestedforSave = SaveProjectDialog.FileName;
                }
                ExtAppBGWorkerLink.RunWorkerAsync();
            }
            return 0;
        }
        public int PromptOpenProjectFile()
        {
            OpenFileDialog OpenProjectDialog = new OpenFileDialog();
            OpenProjectDialog.Title = "Select a Project File to Open (*.imsprj)";
            OpenProjectDialog.Filter = @"project files|*.imsPrj;";
            OpenProjectDialog.CheckPathExists = true;
            OpenProjectDialog.CheckFileExists = true;
            if (pcExecutionSystemMetaData.SysModuleProjectPathStrings != null)
            {
                if (pcExecutionSystemMetaData.SysModuleProjectPathStrings.Count > 0)
                    OpenProjectDialog.InitialDirectory = Path.GetDirectoryName(pcExecutionSystemMetaData.SysModuleProjectPathStrings[0]);
            }
            DialogResult theseResults = OpenProjectDialog.ShowDialog();
            if (theseResults == DialogResult.OK)
            {
                ProjectPathRequestedforOpen = OpenProjectDialog.FileName;
                CloseProject();
            }
            ExtAppBGWorkerLink.RunWorkerAsync();
            return 0;
        }
        /// <summary>
        /// PromptCloseProjectFile()
        /// - This Function is called to close the API and User Modules attached to the execution system
        /// - First the user is prompted to confirm close operation
        /// - if Confirmed, all modules are disposed of, all lists are cleared
        /// - if Canceled, no modules are disposed of, no lists are cleared, no action is performed
        /// </summary>
        /// <returns></returns>
        public int PromptCloseProjectFile()
        {
            // Prompt user to confirm close operation
            DialogResult theseResults = MessageBox.Show("Are you sure you want to close the active project?","Close the active project now?",MessageBoxButtons.OKCancel);
            if(theseResults == DialogResult.OK )
            {
                CloseProject();

            }

            return 0;
        }
        public int LaunchNewProjectExplorer()
        {
            if (thisPrjViewer == null)
            {
                thisPrjViewer = new imsProjectExplorer();
                thisPrjViewer.pCExeSysLink = this;
                MainLoopProjectCheck();
            }
            return 0;
        }
        public int LaunchNewExceptionLog()
        {
            if(thisExcpViewer==null)
            {
                thisExcpViewer = new imsExceptionViewer();
                thisExcpViewer.pCExeSysLink = this;
            }
            
            return 0;
        }
        public int LaunchNewPlotPane()
        {
            PlotModuleProperty.sysModFormList.Add(new imsPlotPane());
            (PlotModuleProperty.sysModFormList[PlotModuleProperty.sysModFormList.Count-1]).pCExeSysLink = this;
            return 0;
        }
        /// <summary>
        /// InstantiateNewAPIModules()
        /// - This function is called to create a new project in the execution system
        /// </summary>
        public int InstantiateNewAPIModules()
        {
            CloseProject();

            APISysModules.Add(new imsPCClocksModule(this, globalNodeList));
            APISysModules.Add(new imsBGThreadManager(this, globalNodeList));
            APISysModules.Add(new imsProjectModuleNode(this, globalNodeList));
            APISysModules.Add(new imsPlotModule(this, globalNodeList));

            activeProjectpath = Path.Combine(Directory.GetCurrentDirectory(),"NewProjectFile.imsprj");

            CallEntryPointFunction(MainInit);

            return 0;
        }
        public void AddAPIMod2ExeSys(imsAPISysModule Sys2Add)
        {
            APISysModules.Add(Sys2Add);
            CallEntryPointFunction(MainInit);
        }
        private void CloseProject()
        {
            if (APISysModules.Count != 0)
            {
                int i;
                for (i = APISysModules.Count - 1; i > -1; i--)
                    APISysModules[i].Dispose();
                APISysModules.Clear();
            }
            activeProjectpath = "";
        }
        private void MainLoopProjectCheck()
        {
            if (thisPrjViewer != null)
            {
                thisPrjViewer.UpatePrjIndicators(activeProjectpath);
            }

            if (activeProjectpath != "")
            {
                // manage active project
                
                
            }
            else
            {
                

                if (pcExecutionSystemMetaData != null)
                {
                    if (pcExecutionSystemMetaData.SysModuleProjectPathStrings != null)
                    {
                        if (pcExecutionSystemMetaData.SysModuleProjectPathStrings.Count > 0)
                        {
                            if (pcExecutionSystemMetaData.TryLoadLastPrj)
                            {
                                ProjectPathRequestedforOpen = Path.GetFullPath(pcExecutionSystemMetaData.SysModuleProjectPathStrings[0]);
                                pcExecutionSystemMetaData.TryLoadLastPrj = false;
                            }
                        }
                    }
                }


            }
        }
        private void ExtAppOPenCloseProj()
        {
            // save active project when requested
            if (SaveToProjectPath != "")
            {
                if (File.Exists(SaveToProjectPath))
                {// open, truncate, serialize
                    SerializePCSystem(SaveToProjectPath);

                    activeProjectpath = SaveToProjectPath;
                    pcExecutionSystemMetaData.AddProjectPathString(activeProjectpath);
                    SaveToProjectPath = "";
                    if (thisPrjViewer != null)
                        LinkedMDSForm.BeginInvoke(new Action(() => {
                            thisPrjViewer.updateTreeView();
                        }));
                }
                else
                    SaveToProjectPath = "";

            }

            // open project from file when requested
            if (RequestedProjectPath != "")
            {
                if (File.Exists(RequestedProjectPath))
                {
                    // deserialize
                    DeserializePCSystem(RequestedProjectPath);

                    activeProjectpath = RequestedProjectPath;
                    pcExecutionSystemMetaData.AddProjectPathString(activeProjectpath);
                    RequestedProjectPath = "";

                    if (thisPrjViewer != null)
                      LinkedMDSForm.BeginInvoke(new Action(() => {
                          thisPrjViewer.updateTreeView();
                          MainInit();
                      }));
                }
                else
                {
                    RequestedProjectPath = "";
                }

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
                    int i;
                    for (i = 0; i < globalNodeList.Count; i++)
                        globalNodeList[i].Dispose();
                    for (i = 0; i < APISysModules.Count; i++)
                        APISysModules[i].Dispose();
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

    public class Crc16
    {
        const ushort polynomial = 0x8408;
        ushort[] table = new ushort[256];

        public ushort ComputeChecksum(byte[] bytes)
        {
            ushort crc = 0;
            for (int i = 0; i < bytes.Length; ++i)
            {
                byte index = (byte)(crc ^ bytes[i]);
                crc = (ushort)((crc >> 8) ^ table[index]);
            }
            return crc;
        }

        public byte[] ComputeChecksumBytes(byte[] bytes)
        {
            ushort crc = ComputeChecksum(bytes);
            return BitConverter.GetBytes(crc);
        }

        public Crc16()
        {
            ushort value;
            ushort temp;
            for (ushort i = 0; i < table.Length; ++i)
            {
                value = 0;
                temp = i;
                for (byte j = 0; j < 8; ++j)
                {
                    if (((value ^ temp) & 0x0001) != 0)
                    {
                        value = (ushort)((value >> 1) ^ polynomial);
                    }
                    else
                    {
                        value >>= 1;
                    }
                    temp >>= 1;
                }
                table[i] = value;
            }
        }
    }

    #region // CSV Read / Write Classes
    /// <summary>
    /// Class to store one CSV row
    /// </summary>
    public class CsvRow : List<string>
    {
        public string LineText { get; set; }
    }

    /// <summary>
    /// Class to write data to a CSV file
    /// </summary>
    public class CsvFileWriter : StreamWriter
    {
        public CsvFileWriter(Stream stream)
            : base(stream)
        {
        }

        public CsvFileWriter(string filename)
            : base(filename)
        {
        }

        /// <summary>
        /// Writes a single row to a CSV file.
        /// </summary>
        /// <param name="row">The row to be written</param>
        public void WriteRow(CsvRow row)
        {
            StringBuilder builder = new StringBuilder();
            bool firstColumn = true;
            foreach (string value in row)
            {
                // Add separator if this isn't the first value
                if (!firstColumn)
                    builder.Append(',');
                // Implement special handling for values that contain comma or quote
                // Enclose in quotes and double up any double quotes
                if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
                    builder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
                else
                    builder.Append(value);
                firstColumn = false;
            }
            row.LineText = builder.ToString();
            WriteLine(row.LineText);
        }
    }

    /// <summary>
    /// Class to read data from a CSV file
    /// </summary>
    public class CsvFileReader : StreamReader
    {
        public CsvFileReader(Stream stream)
            : base(stream)
        {
        }

        public CsvFileReader(string filename)
            : base(filename)
        {
        }

        /// <summary>
        /// Reads a row of data from a CSV file
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool ReadRow(CsvRow row)
        {
            row.LineText = ReadLine();
            if (String.IsNullOrEmpty(row.LineText))
                return false;

            int pos = 0;
            int rows = 0;

            while (pos < row.LineText.Length)
            {
                string value;

                // Special handling for quoted field
                if (row.LineText[pos] == '"')
                {
                    // Skip initial quote
                    pos++;

                    // Parse quoted value
                    int start = pos;
                    while (pos < row.LineText.Length)
                    {
                        // Test for quote character
                        if (row.LineText[pos] == '"')
                        {
                            // Found one
                            pos++;

                            // If two quotes together, keep one
                            // Otherwise, indicates end of value
                            if (pos >= row.LineText.Length || row.LineText[pos] != '"')
                            {
                                pos--;
                                break;
                            }
                        }
                        pos++;
                    }
                    value = row.LineText.Substring(start, pos - start);
                    value = value.Replace("\"\"", "\"");
                }
                else
                {
                    // Parse unquoted value
                    int start = pos;
                    while (pos < row.LineText.Length && row.LineText[pos] != ',')
                        pos++;
                    value = row.LineText.Substring(start, pos - start);
                }

                // Add field to list
                if (rows < row.Count)
                    row[rows] = value;
                else
                    row.Add(value);
                rows++;

                // Eat up to and including next comma
                while (pos < row.LineText.Length && row.LineText[pos] != ',')
                    pos++;
                if (pos < row.LineText.Length)
                    pos++;
            }
            // Delete any unused items
            while (row.Count > rows)
                row.RemoveAt(rows);

            // Return true if any columns read
            return (row.Count > 0);
        }
    }
    #endregion
}
