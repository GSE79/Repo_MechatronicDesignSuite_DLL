using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Reflection.Emit;

namespace MechatronicDesignSuite_DLL
{
    [Serializable]
    public class imsValueNode : imsBaseNode
    {
        public bool TestProp {set; get;}
        public imsValueNode(List<imsBaseNode> globalNodeListIn) :base(globalNodeListIn)
        {
            nodeType = typeof(imsValueNode);
            nodeName = "Value Node";
        }
        public imsValueNode(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs) :base(DeSerializeFormatter, deSerializeFs)
        {

        }

    }
}
