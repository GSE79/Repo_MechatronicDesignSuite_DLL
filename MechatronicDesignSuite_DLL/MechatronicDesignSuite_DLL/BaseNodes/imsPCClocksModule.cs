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
    [Serializable]
    class imsPCClocksModule: imsAPISysModule
    {
        [NonSerialized]
        DateTime InitializationSystemTime = DateTime.Now;
        [NonSerialized]
        DateTime MainLoopSystemTime, LastMainLoopTime;
        [NonSerialized]
        TimeSpan MainLoopDuration;
        [NonSerialized]
        DateTime ExtAppStartTime, LastExtAppStartTime;
        [NonSerialized]
        TimeSpan ExtAppDuration;

        public imsPCClocksModule(List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            nodeType = typeof(imsPCClocksModule);
            nodeName = "PC Clock Module Node";
        }
        public imsPCClocksModule(PCExeSys PCExeSysIn, List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            nodeType = typeof(imsPCClocksModule);
            nodeName = "PC Clock Module Node";
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
