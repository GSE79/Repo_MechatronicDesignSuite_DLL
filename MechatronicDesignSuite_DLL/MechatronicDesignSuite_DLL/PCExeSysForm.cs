using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
/// <summary>
/// The MechatronicDesignSuite Library is compiled to a DLL and ..
/// </summary>
namespace MechatronicDesignSuite_DLL
{
    /// <summary>
    /// This is the main execution system form of the Mechatronic Design Suite and ...
    /// </summary>
    public partial class MechatronicDesignSuiteForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private PCExeSys sysModuleExeSys;
        /// <summary>
        /// 
        /// </summary>
        public MechatronicDesignSuiteForm()
        {
            InitializeComponent();
            if(sysModuleExeSys == null)
                sysModuleExeSys = new PCExeSys(this);
        }

        private void viewProjectExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imsProjectExplorer thisPrjViewer = new imsProjectExplorer();
            thisPrjViewer.pCExeSysLink = sysModuleExeSys;
            
        }
        
        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveProjectDialog = new SaveFileDialog();
            SaveProjectDialog.Title = "Save Project File as (*.imsprj)";
            SaveProjectDialog.Filter = @"project files|*.imsPrj;";
            //SaveProjectDialog.CheckPathExists = true;
            //SaveProjectDialog.CheckFileExists = true;
            if (sysModuleExeSys.ProjModNodeProperty.MetaDataStructure.SysModuleProjectPathStrings != null)
            {
                if (sysModuleExeSys.ProjModNodeProperty.MetaDataStructure.SysModuleProjectPathStrings.Count > 0)
                    SaveProjectDialog.InitialDirectory = Path.GetDirectoryName(sysModuleExeSys.ProjModNodeProperty.MetaDataStructure.SysModuleProjectPathStrings[0]);
            }
            DialogResult theseResults = SaveProjectDialog.ShowDialog();
            if (theseResults == DialogResult.OK)
            {
                sysModuleExeSys.ProjModNodeProperty.ProjectPathRequestedforSave = SaveProjectDialog.FileName;
                if (!File.Exists(sysModuleExeSys.ProjModNodeProperty.ProjectPathRequestedforSave))
                {
                    FileStream tfs = File.Create(sysModuleExeSys.ProjModNodeProperty.ProjectPathRequestedforSave);
                    if (tfs != null)
                        tfs.Close();
                }
            }
            else
                sysModuleExeSys.ProjModNodeProperty.ProjectPathRequestedforSave = "";

        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog OpenProjectDialog = new OpenFileDialog();
            OpenProjectDialog.Title = "Select a Project File to Open (*.imsprj)";
            OpenProjectDialog.Filter = @"project files|*.imsPrj;";
            OpenProjectDialog.CheckPathExists = true;
            OpenProjectDialog.CheckFileExists = true;
            if (sysModuleExeSys.ProjModNodeProperty.MetaDataStructure.SysModuleProjectPathStrings != null)
            {
                if (sysModuleExeSys.ProjModNodeProperty.MetaDataStructure.SysModuleProjectPathStrings.Count > 0)
                    OpenProjectDialog.InitialDirectory = Path.GetDirectoryName(sysModuleExeSys.ProjModNodeProperty.MetaDataStructure.SysModuleProjectPathStrings[0]);
            }
            DialogResult theseResults = OpenProjectDialog.ShowDialog();
            if (theseResults == DialogResult.OK)
            {
                sysModuleExeSys.DeSerializingSystem = true;
                sysModuleExeSys.DeSerializationRequested = true;
                sysModuleExeSys.ProjModNodeProperty.ProjectPathRequestedforOpen = OpenProjectDialog.FileName;
            }

        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sysModuleExeSys.Dispose();
            sysModuleExeSys = null;
            if (sysModuleExeSys == null)
                sysModuleExeSys = new PCExeSys(this);
            sysModuleExeSys.ProjModNodeProperty.MetaDataStructure.TryLoadLastPrj = false;
        }

        private void viewExceptionLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imsExceptionViewer thisExcpViewer = new imsExceptionViewer();
            thisExcpViewer.pCExeSysLink = sysModuleExeSys;
        }

    }
}
