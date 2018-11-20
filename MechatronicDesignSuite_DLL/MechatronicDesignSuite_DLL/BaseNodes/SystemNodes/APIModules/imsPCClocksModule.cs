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
    /// <summary>
    /// imsPCClocksModule : imsAPIModule
    /// </summary>
    class imsPCClocksModule : imsAPISysModule
    {
        protected DateTime InitializationSystemTime = DateTime.Now;
        [Category("PC Clocks Module"), Description("Time Stamp from .NET when MainInit() is called")]
        public DateTime getInitializationSystemTime { get { return InitializationSystemTime; } }

        DateTime MainLoopSystemTime, LastMainLoopTime;
        TimeSpan MainLoopDuration;
        [Category("PC Clocks Module"), Description("Duration from .NET of MainLoop() round trip time")]
        public TimeSpan getMainLoopDuration { get { return MainLoopDuration; } }

        DateTime ExtAppStartTime, LastExtAppStartTime;
        TimeSpan ExtAppDuration;
        [Category("PC Clocks Module"), Description("Duration from .NET of ExtBGAppThread() round trip time")]
        public TimeSpan getExtAppDuration { get { return ExtAppDuration; } }

        [Category("PC Clocks Module"), Description("Duration from .NET Timer object callback execution time")]
        public int MainLoopCycleTime
        {
            set
            { PCExeSysLink.GUITimerLink.Interval = value; }
            get
            { return PCExeSysLink.GUITimerLink.Interval; }
        }

        public imsPCClocksModule(List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            NodeType = typeof(imsPCClocksModule);
            NodeName = "PC Clock Module Node";
        }
        public imsPCClocksModule(PCExeSys PCExeSysIn, List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            NodeType = typeof(imsPCClocksModule);
            NodeName = "PC Clock Module Node";
            if (PCExeSysIn != null)
                PCExeSysLink = PCExeSysIn;
        }
        public imsPCClocksModule(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs) : base(DeSerializeFormatter, deSerializeFs)
        {

        }
        public override void MainInit()
        {
            LastMainLoopTime = InitializationSystemTime;
            if (PCExeSysLink != null)
                PCExeSysLink.GUITimerLink.Start();
            isInitialized = true;
        }
        public override void MainLoop()
        {
            MainLoopSystemTime = DateTime.Now;
            MainLoopDuration = MainLoopSystemTime - LastMainLoopTime;
            LastMainLoopTime = MainLoopSystemTime;
        }
        public override void ExtAppBGThread()
        {
            ExtAppStartTime = DateTime.Now;
            ExtAppDuration = ExtAppStartTime - LastExtAppStartTime;
            LastExtAppStartTime = ExtAppStartTime;
        }
    }
}
