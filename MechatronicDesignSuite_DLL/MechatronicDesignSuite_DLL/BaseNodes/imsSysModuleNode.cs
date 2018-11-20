using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MechatronicDesignSuite_DLL.BaseNodes
{
    /// <summary>
    /// imsSysModuleNode : imsBaseNode
    /// </summary>
    public class imsSysModuleNode : imsBaseNode
    {
        /// <summary>
        /// sysValues
        /// </summary>
        protected List<imsValueNode> sysValues;
        [Category("System Module"), Description("An ordered list of all value nodes of the system module.")]
        public List<imsValueNode> getsysValues { get { return sysValues; } }
        [Category("System Module"), Description("An ordered list of all value nodes of the system module.")]
        public List<imsValueNode> setsysValues { get { return sysValues; } set { sysValues = value; } }

        /// <summary>
        /// subSystems
        /// </summary>
        protected List<imsSysModuleNode> subSystems;
        [Category("System Module"), Description("An ordered list of all sub system nodes of the system module.")]
        public List<imsSysModuleNode> getsubSystems { get { return subSystems;} }
        [Category("System Module"), Description("An ordered list of all sub system nodes of the system module.")]
        public List<imsSysModuleNode> setsubSystems { get { return subSystems; } set { subSystems = value; } }

        List<int> ValueIndexList;
        List<int> SubSysIndexList;

        /// <summary>
        /// sysModForm
        /// </summary>
        [Category("System Module"), Description("An optional graphical interface form for this system module.")]
        public ImsBaseForm sysModForm { set; get; } = null;

        /// <summary>
        /// isInitialized
        /// </summary>
        protected bool isInitialized = false;
        [Category("System Module"), Description("Indication that MainInit() Code should (re)Execute")]
        public bool getisInitialized { get { return isInitialized; } }
        [Category("System Module"), Description("Indication that MainInit() Code should (re)Execute")]
        public bool setisInitialized { get { return isInitialized; } set { isInitialized = value; } }

        private void SetValuesOnSerializing(StreamingContext context)
        {
            SetValuesOnSerializing();
        }
        private void SetValuesOnSerializing()
        {
            if (sysValues != null)
            {
                if (ValueIndexList == null)
                    ValueIndexList = new List<int>();
                ValueIndexList.Clear();
                foreach (imsValueNode vNode in sysValues)
                    ValueIndexList.Add(vNode.getGlobalNodeID);
            }
            else
            {
                if (ValueIndexList == null)
                    ValueIndexList = new List<int>();
            }
            if (subSystems != null)
            {
                if (SubSysIndexList == null)
                    SubSysIndexList = new List<int>();
                SubSysIndexList.Clear();
                foreach (imsSysModuleNode sNode in subSystems)
                    SubSysIndexList.Add(sNode.GlobalNodeID);
            }
            else
            {
                if (SubSysIndexList == null)
                    SubSysIndexList = new List<int>();
            }
        }

        /// <summary>
        /// imsSysModuleNode()
        /// </summary>
        /// <param name="globalNodeListIn"></param>
        public imsSysModuleNode(List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            NodeType = typeof(imsSysModuleNode);
            NodeName = "SysModule Node";
        }
        /// <summary>
        /// imsSysModuleNode()
        /// </summary>
        /// <param name="DeSerializeFormatter"></param>
        /// <param name="deSerializeFs"></param>
        public imsSysModuleNode(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs) : base(DeSerializeFormatter, deSerializeFs)
        {
            ValueIndexList = (List<int>)DeSerializeFormatter.Deserialize(deSerializeFs);
            SubSysIndexList = (List<int>)DeSerializeFormatter.Deserialize(deSerializeFs);
        }
        /// <summary>
        /// writeNode2file()
        /// </summary>
        /// <param name="SerializeFormatter"></param>
        /// <param name="SerializeFs"></param>
        public override void writeNode2file(BinaryFormatter SerializeFormatter, FileStream SerializeFs)
        {
            base.writeNode2file(SerializeFormatter, SerializeFs);
            SetValuesOnSerializing();
            SerializeFormatter.Serialize(SerializeFs, ValueIndexList);
            SerializeFormatter.Serialize(SerializeFs, SubSysIndexList);
        }
        /// <summary>
        /// restoreFromFile()
        /// </summary>
        /// <param name="DeSerializeFormatter"></param>
        /// <param name="deSerializeFs"></param>
        public override void restoreFromFile(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs)
        {
            base.restoreFromFile(DeSerializeFormatter, deSerializeFs);
            ValueIndexList = (List<int>)DeSerializeFormatter.Deserialize(deSerializeFs);
            SubSysIndexList = (List<int>)DeSerializeFormatter.Deserialize(deSerializeFs);
        }
        /// <summary>
        /// deSerializeLinkNodes()
        /// </summary>
        /// <param name="gNodeList"></param>
        public virtual void deSerializeLinkNodes(List<imsBaseNode> gNodeList)
        {
            if(ValueIndexList.Count > 0)
            {
                if (sysValues == null)
                    sysValues = new List<imsValueNode>();
                sysValues.Clear();
                foreach (int vindex in ValueIndexList)
                {
                    if (vindex < gNodeList.Count)
                        sysValues.Add((imsValueNode)gNodeList[vindex]);
                    else
                        throw (new Exception("ValueNode Index exceeds global array bounds"));
                }
            }
            if (SubSysIndexList.Count > 0)
            {
                if (subSystems == null)
                    subSystems = new List<imsSysModuleNode>();
                subSystems.Clear();
                foreach (int sindex in SubSysIndexList)
                {
                    if (sindex < gNodeList.Count)
                        subSystems.Add((imsSysModuleNode)gNodeList[sindex]);
                    else
                        throw (new Exception("SubSystem Index exceeds global array bounds"));
                }

            }

        }

        /// <summary>
        /// MainInit()
        /// </summary>
        public virtual void MainInit()
        {
            isInitialized = true;
        }
        /// <summary>
        /// MainLoop()
        /// </summary>
        public virtual void MainLoop()
        {

        }
        /// <summary>
        /// ExtAppBGThread()
        /// </summary>
        public virtual void ExtAppBGThread()
        {

        }
        /// <summary>
        /// toNewTreeNode()
        /// </summary>
        /// <returns></returns>
        public override TreeNode toNewTreeNode()
        {
            TreeNode outNode = base.toNewTreeNode();
            if(sysValues!=null)
                foreach (imsValueNode valNode in sysValues)
                    outNode.Nodes.Add(valNode.toNewTreeNode());
            if(subSystems!=null)
                foreach (imsSysModuleNode sysNode in subSystems)
                    outNode.Nodes.Add(sysNode.toNewTreeNode());
            return outNode;
        }
    }
}
