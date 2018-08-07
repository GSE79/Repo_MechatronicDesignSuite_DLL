using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        void updateTreeView()
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
    }
}
