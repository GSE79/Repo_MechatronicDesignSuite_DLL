namespace MechatronicDesignSuite_DLL
{
    partial class MechatronicDesignSuiteForm
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
            this.PCExeSysMenuStrip = new System.Windows.Forms.MenuStrip();
            this.PCExeSysProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewProjectExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewExceptionLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PCExeSysStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PCExeSysProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.PCExeSysStatusStrip = new System.Windows.Forms.StatusStrip();
            this.PCExeSysMenuStrip.SuspendLayout();
            this.PCExeSysStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // PCExeSysMenuStrip
            // 
            this.PCExeSysMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.PCExeSysMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PCExeSysProjectMenuItem});
            this.PCExeSysMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.PCExeSysMenuStrip.Name = "PCExeSysMenuStrip";
            this.PCExeSysMenuStrip.Padding = new System.Windows.Forms.Padding(12, 4, 0, 4);
            this.PCExeSysMenuStrip.Size = new System.Drawing.Size(1600, 46);
            this.PCExeSysMenuStrip.TabIndex = 1;
            this.PCExeSysMenuStrip.Text = "menuStrip1";
            // 
            // PCExeSysProjectMenuItem
            // 
            this.PCExeSysProjectMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewProjectExplorerToolStripMenuItem,
            this.viewExceptionLogToolStripMenuItem});
            this.PCExeSysProjectMenuItem.Name = "PCExeSysProjectMenuItem";
            this.PCExeSysProjectMenuItem.Size = new System.Drawing.Size(100, 38);
            this.PCExeSysProjectMenuItem.Text = "Project";
            // 
            // viewProjectExplorerToolStripMenuItem
            // 
            this.viewProjectExplorerToolStripMenuItem.Name = "viewProjectExplorerToolStripMenuItem";
            this.viewProjectExplorerToolStripMenuItem.Size = new System.Drawing.Size(337, 38);
            this.viewProjectExplorerToolStripMenuItem.Text = "view project explorer";
            this.viewProjectExplorerToolStripMenuItem.Click += new System.EventHandler(this.viewProjectExplorerToolStripMenuItem_Click);
            // 
            // viewExceptionLogToolStripMenuItem
            // 
            this.viewExceptionLogToolStripMenuItem.Name = "viewExceptionLogToolStripMenuItem";
            this.viewExceptionLogToolStripMenuItem.Size = new System.Drawing.Size(337, 38);
            this.viewExceptionLogToolStripMenuItem.Text = "view exception log";
            this.viewExceptionLogToolStripMenuItem.Click += new System.EventHandler(this.viewExceptionLogToolStripMenuItem_Click);
            // 
            // PCExeSysStatusLabel
            // 
            this.PCExeSysStatusLabel.Name = "PCExeSysStatusLabel";
            this.PCExeSysStatusLabel.Size = new System.Drawing.Size(247, 33);
            this.PCExeSysStatusLabel.Text = "PC ExeSys StatusLabel";
            // 
            // PCExeSysProgressBar
            // 
            this.PCExeSysProgressBar.Name = "PCExeSysProgressBar";
            this.PCExeSysProgressBar.Size = new System.Drawing.Size(200, 32);
            // 
            // PCExeSysStatusStrip
            // 
            this.PCExeSysStatusStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.PCExeSysStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PCExeSysStatusLabel,
            this.PCExeSysProgressBar});
            this.PCExeSysStatusStrip.Location = new System.Drawing.Point(0, 827);
            this.PCExeSysStatusStrip.Name = "PCExeSysStatusStrip";
            this.PCExeSysStatusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 28, 0);
            this.PCExeSysStatusStrip.Size = new System.Drawing.Size(1600, 38);
            this.PCExeSysStatusStrip.TabIndex = 0;
            this.PCExeSysStatusStrip.Text = "statusStrip1";
            // 
            // MechatronicDesignSuiteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 865);
            this.Controls.Add(this.PCExeSysStatusStrip);
            this.Controls.Add(this.PCExeSysMenuStrip);
            this.MainMenuStrip = this.PCExeSysMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MechatronicDesignSuiteForm";
            this.Text = "PCExeSysForm";
            this.PCExeSysMenuStrip.ResumeLayout(false);
            this.PCExeSysMenuStrip.PerformLayout();
            this.PCExeSysStatusStrip.ResumeLayout(false);
            this.PCExeSysStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected System.Windows.Forms.MenuStrip PCExeSysMenuStrip;
        protected System.Windows.Forms.StatusStrip PCExeSysStatusStrip;
        private System.Windows.Forms.ToolStripMenuItem viewProjectExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewExceptionLogToolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel PCExeSysStatusLabel;
        public System.Windows.Forms.ToolStripProgressBar PCExeSysProgressBar;
        public System.Windows.Forms.ToolStripMenuItem PCExeSysProjectMenuItem;
    }
}