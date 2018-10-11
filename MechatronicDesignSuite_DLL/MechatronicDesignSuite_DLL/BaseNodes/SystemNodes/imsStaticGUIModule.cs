using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MechatronicDesignSuite_DLL.BaseNodes
{
    public class imsStaticGUIModule : imsSysModuleNode
    {
        public MechatronicDesignSuiteForm guiLink;
        /// <summary>
        /// 
        /// </summary>
        public imsStaticGUIModule(List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            nodeType = typeof(MechatronicDesignSuiteForm);
            nodeName = "Static GUI Module";
        }
        /// <summary>
        /// 
        /// </summary>
        public imsStaticGUIModule(List<imsBaseNode> globalNodeListIn, MechatronicDesignSuiteForm GuiForm) : base(globalNodeListIn)
        {
            nodeType = typeof(MechatronicDesignSuiteForm);
            nodeName = "Static GUI Module";
            guiLink = GuiForm;
            guiLink.StaticExeModule = this;
        }
        /// <summary>
        /// 
        /// </summary>
        public imsStaticGUIModule(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs) : base(DeSerializeFormatter, deSerializeFs)
        {

        }
    }
}
