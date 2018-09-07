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

namespace MechatronicDesignSuite_DLL
{
    public partial class imsProjectExplorer : Form, ImsBaseForm
    {
        public PCExeSys pCExeSysLink
        {
            set
            {
                if (value != null)
                {
                    pcexesys = value;
                    this.Show();
                    updateTreeView();
                }
                else
                    throw (new Exception("Attempted Null Link of pcexesys"));
            }
            get { return pcexesys; }
        }
        PCExeSys pcexesys;

        public imsProjectExplorer()
        {
            InitializeComponent();
        }

        private void ProjectTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode SelectedNode = ProjectTreeView.GetNodeAt(e.Location);
            imsBaseNode tempNode = ((imsBaseNode)(SelectedNode.Tag));
            if (tempNode != null)
                NodePropertyGrid.SelectedObject = ((imsBaseNode)(SelectedNode.Tag));
        }
        public void updateTreeView()
        {
            if (pCExeSysLink == null)
                throw (new Exception("Attempted Update of Null pcexesys"));
            else
            {
                ProjectTreeView.ShowNodeToolTips = true;
                ProjectTreeView.Nodes.Clear();
                pCExeSysLink.PopulateTreeView(ProjectTreeView);
            }
        }
        public void UpatePrjIndicators(string pathString)
        {
            if(pathString!="")
            {
                ProjectStatusLabel.BackColor = Color.Green;
                ProjectStatusLabel.Text = Path.GetFileName(pathString);
                saveToolStripMenuItem.Enabled = true;
                closeToolStripMenuItem.Enabled = true;
                openToolStripMenuItem.Enabled = false;
                newToolStripMenuItem.Enabled = false;
            }
            else
            {
                ProjectStatusLabel.BackColor = Color.Yellow;
                ProjectStatusLabel.Text = "Closed";
                saveToolStripMenuItem.Enabled = false;
                closeToolStripMenuItem.Enabled = false;
                openToolStripMenuItem.Enabled = true;
                newToolStripMenuItem.Enabled = true;
            }
        }
        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pCExeSysLink.CallEntryPointFunction(pCExeSysLink.PromptSaveProject2File);
            
        }
        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pCExeSysLink.CallEntryPointFunction(pCExeSysLink.PromptOpenProjectFile);
            
        }
        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pCExeSysLink.CallEntryPointFunction(pCExeSysLink.PromptCloseProjectFile);
            updateTreeView();
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pCExeSysLink.CallEntryPointFunction(pCExeSysLink.InstantiateNewAPIModules);
            updateTreeView();
        }

        private void imsProjectExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            pCExeSysLink.thisPrjViewer = null;
        }
    }
}
