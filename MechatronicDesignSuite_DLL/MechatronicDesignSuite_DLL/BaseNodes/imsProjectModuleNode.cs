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

    public class imsProjectModuleNode : imsAPISysModule
    {
        public imsProjectModuleNode(List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            nodeType = typeof(imsProjectModuleNode);
            nodeName = "ProjectModule Node";
        }
        public imsProjectModuleNode(PCExeSys PCExeSysIn, List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            nodeType = typeof(imsProjectModuleNode);
            nodeName = "ProjectModule Node";
            if (PCExeSysIn != null)
                PCExeSysLink = PCExeSysIn;
        }
        public imsProjectModuleNode(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs) : base(DeSerializeFormatter, deSerializeFs)
        {

        }
        public override void MainInit()
        {
            
        }
        public override void MainLoop()
        {
            

        }
        public override void ExtAppBGThread()
        {
            
        }

    }
}
