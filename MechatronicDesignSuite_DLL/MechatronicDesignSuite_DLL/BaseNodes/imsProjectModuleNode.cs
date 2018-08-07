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
        public PCExeSysMetaData MetaDataStructure
        {
            set { pcExecutionSystemMetaData = value; }
            get { return pcExecutionSystemMetaData; }
        }
        PCExeSysMetaData pcExecutionSystemMetaData;        
        public string ActiveProjectPath { get; set; } = "";
        
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
            pcExecutionSystemMetaData = (PCExeSysMetaData)DeSerializeFormatter.Deserialize(deSerializeFs);
        }
        public override void writeNode2file(BinaryFormatter SerializeFormatter, FileStream SerializeFs)
        {
            base.writeNode2file(SerializeFormatter, SerializeFs);
            SerializeFormatter.Serialize(SerializeFs, pcExecutionSystemMetaData);
        }
        public override void MainInit()
        {
            pcExecutionSystemMetaData = new PCExeSysMetaData();
            pcExecutionSystemMetaData = PCExeSysMetaData.generateMetaData(pcExecutionSystemMetaData);
            pcExecutionSystemMetaData.TryLoadLastPrj = true;
        }
        public override void MainLoop()
        {
            if(ActiveProjectPath != "")
            {
                // manage active project

                
            }
            else if(pcExecutionSystemMetaData != null)
            {
                if (pcExecutionSystemMetaData.SysModuleProjectPathStrings != null)
                {
                    if (pcExecutionSystemMetaData.SysModuleProjectPathStrings.Count > 0)
                    {
                        if (pcExecutionSystemMetaData.TryLoadLastPrj)
                        {
                            PCExeSysLink.DeSerializingSystem = true;
                            PCExeSysLink.DeSerializationRequested = true;
                            PCExeSysLink.ProjModNodeProperty.ProjectPathRequestedforOpen = Path.GetFullPath(pcExecutionSystemMetaData.SysModuleProjectPathStrings[0]);
                            pcExecutionSystemMetaData.TryLoadLastPrj = false;
                        }
                    }
                }
                
            }

            
        }
        public override void ExtAppBGThread()
        {
            // save active project when requested
            if (SaveToProjectPath != "")
            {
                if (File.Exists(SaveToProjectPath))
                {// open, truncate, serialize
                    PCExeSysLink.SerializePCSystem(SaveToProjectPath);

                    ActiveProjectPath = SaveToProjectPath;
                    pcExecutionSystemMetaData.AddProjectPathString(ActiveProjectPath);
                    SaveToProjectPath = "";
                }
                else
                    SaveToProjectPath = "";

            }

            // open project from file when requested
            if (RequestedProjectPath != "")
            {
                if (File.Exists(RequestedProjectPath))
                {
                    // deserialize
                    PCExeSysLink.DeserializePCSystem(RequestedProjectPath);

                    ActiveProjectPath = RequestedProjectPath;
                    pcExecutionSystemMetaData.AddProjectPathString(ActiveProjectPath);
                    RequestedProjectPath = "";
                }
                else
                {
                    RequestedProjectPath = "";
                    PCExeSysLink.DeSerializingSystem = false;
                    PCExeSysLink.DeSerializationRequested = false;
                }

            }
        }


    }
}
