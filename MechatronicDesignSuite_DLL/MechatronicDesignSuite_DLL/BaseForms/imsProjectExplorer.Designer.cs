namespace MechatronicDesignSuite_DLL
{
    partial class imsProjectExplorer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ProjectTreeView = new System.Windows.Forms.TreeView();
            this.NodePropertyGrid = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ProjectTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.NodePropertyGrid);
            this.splitContainer1.Size = new System.Drawing.Size(1671, 1209);
            this.splitContainer1.SplitterDistance = 556;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 4;
            // 
            // ProjectTreeView
            // 
            this.ProjectTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProjectTreeView.Location = new System.Drawing.Point(0, 0);
            this.ProjectTreeView.Name = "ProjectTreeView";
            this.ProjectTreeView.Size = new System.Drawing.Size(556, 1209);
            this.ProjectTreeView.TabIndex = 3;
            this.ProjectTreeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ProjectTreeView_MouseDoubleClick);
            // 
            // NodePropertyGrid
            // 
            this.NodePropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NodePropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.NodePropertyGrid.Name = "NodePropertyGrid";
            this.NodePropertyGrid.Size = new System.Drawing.Size(1105, 1209);
            this.NodePropertyGrid.TabIndex = 0;
            // 
            // imsProjectExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1671, 1209);
            this.Controls.Add(this.splitContainer1);
            this.Name = "imsProjectExplorer";
            this.Text = "ProjectExplorer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView ProjectTreeView;
        private System.Windows.Forms.PropertyGrid NodePropertyGrid;
    }
}