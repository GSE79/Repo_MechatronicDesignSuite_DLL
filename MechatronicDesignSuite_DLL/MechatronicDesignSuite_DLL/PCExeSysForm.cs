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
            sysModuleExeSys.ProjModNodeProperty.ProjectPathRequestedforSave = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(),"SerializeTest.imsPrj")); 
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sysModuleExeSys.DeSerializingSystem = true;
            sysModuleExeSys.DeSerializationRequested = true;
            sysModuleExeSys.ProjModNodeProperty.ProjectPathRequestedforOpen = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "SerializeTest.imsPrj"));
            ;
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sysModuleExeSys.Dispose();
            sysModuleExeSys = null;
            if (sysModuleExeSys == null)
                sysModuleExeSys = new PCExeSys(this);
        }

        private void viewExceptionLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imsExceptionViewer thisExcpViewer = new imsExceptionViewer();
            thisExcpViewer.pCExeSysLink = sysModuleExeSys;
        }

    }
}
