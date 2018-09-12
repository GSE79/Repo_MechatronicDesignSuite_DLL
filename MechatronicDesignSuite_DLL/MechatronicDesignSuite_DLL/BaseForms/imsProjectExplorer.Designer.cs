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
            this.NodePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ProjectStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProjectProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ProjectTabPage = new System.Windows.Forms.TabPage();
            this.LibraryTabPage = new System.Windows.Forms.TabPage();
            this.ProjectTreeView = new System.Windows.Forms.TreeView();
            this.LibraryTreeView = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.ProjectTabPage.SuspendLayout();
            this.LibraryTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.NodePropertyGrid);
            this.splitContainer1.Size = new System.Drawing.Size(836, 251);
            this.splitContainer1.SplitterDistance = 278;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 4;
            // 
            // NodePropertyGrid
            // 
            this.NodePropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NodePropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.NodePropertyGrid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.NodePropertyGrid.Name = "NodePropertyGrid";
            this.NodePropertyGrid.Size = new System.Drawing.Size(553, 251);
            this.NodePropertyGrid.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(836, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 22);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeProjectToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProjectStatusLabel,
            this.ProjectProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 252);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this.statusStrip1.Size = new System.Drawing.Size(836, 23);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ProjectStatusLabel
            // 
            this.ProjectStatusLabel.Name = "ProjectStatusLabel";
            this.ProjectStatusLabel.Size = new System.Drawing.Size(37, 18);
            this.ProjectStatusLabel.Text = "??????";
            // 
            // ProjectProgressBar
            // 
            this.ProjectProgressBar.Name = "ProjectProgressBar";
            this.ProjectProgressBar.Size = new System.Drawing.Size(150, 17);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControl1.Controls.Add(this.ProjectTabPage);
            this.tabControl1.Controls.Add(this.LibraryTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(278, 251);
            this.tabControl1.TabIndex = 0;
            // 
            // ProjectTabPage
            // 
            this.ProjectTabPage.Controls.Add(this.ProjectTreeView);
            this.ProjectTabPage.Location = new System.Drawing.Point(4, 4);
            this.ProjectTabPage.Name = "ProjectTabPage";
            this.ProjectTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ProjectTabPage.Size = new System.Drawing.Size(251, 243);
            this.ProjectTabPage.TabIndex = 0;
            this.ProjectTabPage.Text = "Project";
            this.ProjectTabPage.UseVisualStyleBackColor = true;
            // 
            // LibraryTabPage
            // 
            this.LibraryTabPage.Controls.Add(this.LibraryTreeView);
            this.LibraryTabPage.Location = new System.Drawing.Point(4, 4);
            this.LibraryTabPage.Name = "LibraryTabPage";
            this.LibraryTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.LibraryTabPage.Size = new System.Drawing.Size(251, 243);
            this.LibraryTabPage.TabIndex = 1;
            this.LibraryTabPage.Text = "Libraries";
            this.LibraryTabPage.UseVisualStyleBackColor = true;
            // 
            // ProjectTreeView
            // 
            this.ProjectTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProjectTreeView.Location = new System.Drawing.Point(3, 3);
            this.ProjectTreeView.Margin = new System.Windows.Forms.Padding(2);
            this.ProjectTreeView.Name = "ProjectTreeView";
            this.ProjectTreeView.Size = new System.Drawing.Size(245, 237);
            this.ProjectTreeView.TabIndex = 4;
            this.ProjectTreeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ProjectTreeView_MouseDoubleClick);
            // 
            // LibraryTreeView
            // 
            this.LibraryTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LibraryTreeView.Location = new System.Drawing.Point(3, 3);
            this.LibraryTreeView.Margin = new System.Windows.Forms.Padding(2);
            this.LibraryTreeView.Name = "LibraryTreeView";
            this.LibraryTreeView.Size = new System.Drawing.Size(245, 237);
            this.LibraryTreeView.TabIndex = 5;
            this.LibraryTreeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LibraryTreeView_MouseDoubleClick);
            // 
            // imsProjectExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 275);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "imsProjectExplorer";
            this.Text = "ProjectExplorer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.imsProjectExplorer_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ProjectTabPage.ResumeLayout(false);
            this.LibraryTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid NodePropertyGrid;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ProjectStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar ProjectProgressBar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ProjectTabPage;
        private System.Windows.Forms.TreeView ProjectTreeView;
        private System.Windows.Forms.TabPage LibraryTabPage;
        private System.Windows.Forms.TreeView LibraryTreeView;
    }
}