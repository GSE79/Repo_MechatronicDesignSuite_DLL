﻿using System;
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

namespace MechatronicDesignSuite_DLL
{
    [Serializable]
    public class imsSysModuleNode : imsBaseNode
    {
        [NonSerialized,Category("System Module"),Description("An ordered list of all value nodes of the system module.")]
        List<imsValueNode> sysValues;
        [NonSerialized, Category("System Module"), Description("An ordered list of all sub system nodes of the system module.")]
        List<imsSysModuleNode> subSystems;

        List<int> ValueIndexList;
        List<int> SubSysIndexList;

        [OnSerializing]
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
                    ValueIndexList.Add(vNode.GlobalNodeID);
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
        //[OnDeserialized]
        //private void SetValuesOnDeSerializing()
        //{
        //    if(ValueIndexList!=null)
        //    {
        //        if (sysValues == null)
        //            sysValues = new List<imsValueNode>();
        //        sysValues.Clear();
        //        foreach (uint globalIndex in ValueIndexList)
        //            sysValues.Add();
        //    }
        //}

        public imsSysModuleNode(List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            nodeType = typeof(imsSysModuleNode);
            nodeName = "SysModule Node";
        }
        public imsSysModuleNode(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs) : base(DeSerializeFormatter, deSerializeFs)
        {
            ValueIndexList = (List<int>)DeSerializeFormatter.Deserialize(deSerializeFs);
            SubSysIndexList = (List<int>)DeSerializeFormatter.Deserialize(deSerializeFs);
        }
        public override void writeNode2file(BinaryFormatter SerializeFormatter, FileStream SerializeFs)
        {
            base.writeNode2file(SerializeFormatter, SerializeFs);
            SetValuesOnSerializing();
            SerializeFormatter.Serialize(SerializeFs, ValueIndexList);
            SerializeFormatter.Serialize(SerializeFs, SubSysIndexList);
        }
        public virtual void deSerializeLinkNodes(List<imsBaseNode> gNodeList)
        {
            if(ValueIndexList.Count > 0)
            {
                if (sysValues == null)
                    sysValues = new List<imsValueNode>();
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
                foreach (int sindex in SubSysIndexList)
                {
                    if (sindex < gNodeList.Count)
                        subSystems.Add((imsSysModuleNode)gNodeList[sindex]);
                    else
                        throw (new Exception("SubSystem Index exceeds global array bounds"));
                }

            }

        }
        public virtual void MainInit()
        {

        }
        public virtual void MainLoop()
        {

        }
        public virtual void ExtAppBGThread()
        {

        }
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