using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MechatronicDesignSuite_DLL
{
    [Serializable]
    public class imsProjectModuleNode : imsAPISysModule
    {
        [NonSerialized]
        PCExeSysMetaData pcExecutionSystemMetaData;        
        public string ActiveProjectPath { get; set; } = "";
        [NonSerialized]
        string RequestedProjectPath = "";
        string SaveToProjectPath = "";

        
        public string ProjectPathRequestedforOpen
        {
            set
            {
                if (File.Exists(value))
                    RequestedProjectPath = value;
                else if (Directory.Exists(value))
                    RequestedProjectPath = value;
                else
                {
                    PCExeSysLink.DeSerializingSystem = false;
                    PCExeSysLink.DeSerializationRequested = false;
                }
            }
            get { return RequestedProjectPath; }
        }
        public string ProjectPathRequestedforSave
        {
            set
            {
                if (File.Exists(value))
                    SaveToProjectPath = value;
                else
                {
                    FileStream FS = File.Create(value);
                    if (FS != null)
                        FS.Close();
                    if (File.Exists(value))
                        SaveToProjectPath = value;
                }
            }
            get { return SaveToProjectPath; }
        }

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
            //SaveToProjectPath = (string)DeSerializeFormatter.Deserialize(deSerializeFs);
        }
        public override void writeNode2file(BinaryFormatter SerializeFormatter, FileStream SerializeFs)
        {
            base.writeNode2file(SerializeFormatter, SerializeFs);
            //SerializeFormatter.Serialize(SerializeFs, SaveToProjectPath);
        }
        public override void MainInit()
        {
            pcExecutionSystemMetaData = new PCExeSysMetaData();
        }
        public override void MainLoop()
        {
            if(ActiveProjectPath != "")
            {
                // manage active project

                
            }

            
        }
        public override void ExtAppBGThread()
        {
            // save active project when requested
            if (SaveToProjectPath != "")
            {
                if (File.Exists(SaveToProjectPath))
                {
                    // open, truncate, serialize
                    PCExeSysLink.SerializePCSystem(SaveToProjectPath);

                    ActiveProjectPath = SaveToProjectPath;
                    SaveToProjectPath = "";
                }
            }

            // open project from file when requested
            if (RequestedProjectPath != "")
            {
                if (File.Exists(RequestedProjectPath))
                {
                    // deserialize
                    PCExeSysLink.DeserializePCSystem(RequestedProjectPath);

                    ActiveProjectPath = RequestedProjectPath;
                    RequestedProjectPath = "";
                }

            }
        }


    }
}
