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

namespace MechatronicDesignSuite_DLL
{
    [Serializable]
    public class imsBGThreadManager: imsAPISysModule
    {
        public bool EnableExtAppBGWorker
        {
            set { if (value == true) DisableExtAppBGWorker = false; }
            get { return !DisableExtAppBGWorker; }
        }
        bool DisableExtAppBGWorker = false;
        public imsBGThreadManager(List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            nodeType = typeof(imsBGThreadManager);
            nodeName = "BG Thread Manager Module Node";
        }
        public imsBGThreadManager(PCExeSys PCExeSysIn, List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            nodeType = typeof(imsBGThreadManager);
            nodeName = "BG Thread Manager Module Node";
            if (PCExeSysIn != null)
                PCExeSysLink = PCExeSysIn;

        }
        public imsBGThreadManager(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs) : base(DeSerializeFormatter, deSerializeFs)
        {

        }
        public void StopBGWorker(string ThreadIDString)
        {
            if(ThreadIDString == "ExtAppBGThread")
            {
                DisableExtAppBGWorker = true;
            }
        }
        public override void MainInit()
        {
            
        }
        
        public override void MainLoop()
        {
            foreach(BackgroundWorker BGWorker in PCExeSysLink.BGWorkersList)
            {
                if(BGWorker == PCExeSysLink.ExtAppBGWorkerLink)
                {
                    if(BGWorker.IsBusy)
                    {

                    }
                    else
                    {
                        if(!DisableExtAppBGWorker)
                            BGWorker.RunWorkerAsync();
                    }
                }
            }
        }
        public override void ExtAppBGThread()
        {

        }
    }
}
