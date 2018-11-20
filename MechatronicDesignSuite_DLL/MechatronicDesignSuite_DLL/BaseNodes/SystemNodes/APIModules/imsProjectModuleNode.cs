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
using MechatronicDesignSuite_DLL;

namespace MechatronicDesignSuite_DLL.BaseNodes
{
    public class imsProjectModuleNode : imsAPISysModule
    {
        /// <summary>
        /// ExecuteProjectModules
        /// </summary>
        protected bool ExecuteProjectModules = false;
        [Category("Project Module Node"), Description("Indication for Execution System to Execute Project Modules")]
        public bool getExecuteProjectModules { get { return ExecuteProjectModules; } }
        [Category("Project Module Node"), Description("Indication for Execution System to Execute Project Modules")]
        public bool setExecuteProjectModules { get { return ExecuteProjectModules; } set { ExecuteProjectModules = value; } }

        /// <summary>
        /// imsProjectModuleNode()
        /// </summary>
        /// <param name="globalNodeListIn"></param>
        public imsProjectModuleNode(List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            NodeType = typeof(imsProjectModuleNode);
            NodeName = "ProjectModule Node";
        }
        /// <summary>
        /// imsProjectModuleNode()
        /// </summary>
        /// <param name="PCExeSysIn"></param>
        /// <param name="globalNodeListIn"></param>
        public imsProjectModuleNode(PCExeSys PCExeSysIn, List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            NodeType = typeof(imsProjectModuleNode);
            NodeName = "ProjectModule Node";
            if (PCExeSysIn != null)
                PCExeSysLink = PCExeSysIn;
        }
        /// <summary>
        /// imsProjectModuleNode()
        /// </summary>
        /// <param name="DeSerializeFormatter"></param>
        /// <param name="deSerializeFs"></param>
        public imsProjectModuleNode(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs) : base(DeSerializeFormatter, deSerializeFs)
        {

        }
        /// <summary>
        /// AddSysModuletoProject()
        /// </summary>
        /// <param name="SysModule2Add"></param>
        public void AddSysModuletoProject(imsSysModuleNode SysModule2Add)
        {
            ExecuteProjectModules = false;

            if (this.subSystems == null)
                subSystems = new List<imsSysModuleNode>();
            
            subSystems.Add(SysModule2Add);

            SysModule2Add.MainInit();

            ExecuteProjectModules = true;
            
        }

        /// <summary>
        /// MainInit()
        /// </summary>
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

        /// <summary>
        /// MainLoop()
        /// </summary>
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

        /// <summary>
        /// ExtBGAppThread()
        /// </summary>
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
