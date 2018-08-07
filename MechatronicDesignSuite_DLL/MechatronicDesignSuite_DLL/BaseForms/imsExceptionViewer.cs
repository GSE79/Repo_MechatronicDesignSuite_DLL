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
    public partial class imsExceptionViewer : Form, ImsBaseForm
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
        public imsExceptionViewer()
        {
            InitializeComponent();
        }
        void updateTreeView()
        {
            if (pCExeSysLink == null)
                throw (new Exception("Attempted Update of Null pcexesys"));
            else
            {
                ExceptionTreeview.ShowNodeToolTips = true;
                ExceptionTreeview.Nodes.Clear();
                pCExeSysLink.PopulateExceptionTreeView(ExceptionTreeview);
            }
        }

        private void ExceptionTreeview_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode SelectedNode = ExceptionTreeview.GetNodeAt(e.Location);
            imsException tempExcp = ((imsException)(SelectedNode.Tag));
            if (tempExcp != null)
                NodePropertyGrid.SelectedObject = ((imsException)(SelectedNode.Tag));
        }
    }
}
