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
using System.Windows.Forms;
using MechatronicDesignSuite_DLL;

namespace MechatronicDesignSuite_DLL
{
    public class imsProjectModuleNode : imsAPISysModule
    {
        public bool ExecuteProjectModules { set; get; } = false;
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
        public void AddSysModuletoProject(imsSysModuleNode SysModule2Add)
        {
            ExecuteProjectModules = false;

            if (this.subSystems == null)
                subSystems = new List<imsSysModuleNode>();
            
            subSystems.Add(SysModule2Add);

            SysModule2Add.MainInit();

            ExecuteProjectModules = true;
            
        }
        public override void MainInit()
        {
            int sysIndex;
            // if there is an active project...
            // if there sysModule of the MainGUIApplication Type...
            // Call submodule enrty point
            if(this.subSystems!=null&& ExecuteProjectModules)
            {
                if(subSystems.Count>0)
                {
                    for(sysIndex=0;sysIndex< subSystems.Count;sysIndex++)
                    {
                        subSystems[sysIndex].MainInit();
                    }
                }
            }
            isInitialized = true;
        }
        public override void MainLoop()
        {
            int sysIndex;
            // if there is an active project...
            // if there sysModule of the MainGUIApplication Type...
            // Call submodule enrty point
            if (this.subSystems != null && ExecuteProjectModules)
            {
                if (subSystems.Count > 0)
                {
                    for (sysIndex = 0; sysIndex < subSystems.Count; sysIndex++)
                    {
                        subSystems[sysIndex].MainLoop();
                    }
                }
            }

        }
        public override void ExtAppBGThread()
        {
            int sysIndex;
            // if there is an active project...
            // if there sysModule of the MainGUIApplication Type...
            // Call submodule enrty point
            if (this.subSystems != null && ExecuteProjectModules)
            {
                if (subSystems.Count > 0)
                {
                    for (sysIndex = 0; sysIndex < subSystems.Count; sysIndex++)
                    {
                        subSystems[sysIndex].ExtAppBGThread();
                    }
                }
            }
        }

         

        //public override TreeNode toNewTreeNode()
        //{
        //    TreeNode outNode = base.toNewTreeNode();
        //    if (sysValues != null)
        //        foreach (imsValueNode valNode in sysValues)
        //            outNode.Nodes.Add(valNode.toNewTreeNode());
        //    if (subSystems != null)
        //        foreach (imsSysModuleNode sysNode in subSystems)
        //            outNode.Nodes.Add(sysNode.toNewTreeNode());
        //    return outNode;
        //}
    }
}
