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
    public class imsBGThreadManager: imsAPISysModule
    {
        #region System Properties
        public bool EnableExtAppBGWorker
        {
            set { if (value == true) DisableExtAppBGWorker = false; }
            get { return !DisableExtAppBGWorker; }
        }
        bool DisableExtAppBGWorker = false;
        public bool EnableCommsBGWorker
        {
            set { if (value == true) DisableCommsBGWorker = false; }
            get { return !DisableCommsBGWorker; }
        }
        bool DisableCommsBGWorker = false;
        #endregion

        #region System Constructors
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
        #endregion

        #region System Entry Points

        public override void MainLoop()
        {
            foreach (BackgroundWorker BGWorker in PCExeSysLink.BGWorkersList)
            {
                if (BGWorker == PCExeSysLink.ExtAppBGWorkerLink)
                {
                    if (BGWorker.IsBusy)
                    {

                    }
                    else
                    {
                        if (!DisableExtAppBGWorker)
                            BGWorker.RunWorkerAsync();
                    }
                }
                else if (BGWorker == PCExeSysLink.CommThreadBGWorkerLink)
                {
                    if (BGWorker.IsBusy)
                    {

                    }
                    else
                    {
                        if (!DisableCommsBGWorker)
                            BGWorker.RunWorkerAsync();
                    }
                }
            }
        }
        #endregion

        #region System Methods
        public void StopBGWorker(string ThreadIDString)
        {
            if (ThreadIDString == "ExtAppBGThread")
            {
                DisableExtAppBGWorker = true;
            }
            else if(ThreadIDString == "CommsBGThread")
            {
                DisableCommsBGWorker = true;
            }
        }


        #endregion

        
    }
}
