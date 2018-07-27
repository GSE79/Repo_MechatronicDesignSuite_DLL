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
            this.components = new System.ComponentModel.Container();
            this.PCExeSysStatusStrip = new System.Windows.Forms.StatusStrip();
            this.PCExeSysMenuStrip = new System.Windows.Forms.MenuStrip();
            this.PCExeSysProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PCExeSysStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PCExeSysGUITimer = new System.Windows.Forms.Timer(this.components);
            this.PCExeSysProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.PCExeSysStatusStrip.SuspendLayout();
            this.PCExeSysMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // PCExeSysStatusStrip
            // 
            this.PCExeSysStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PCExeSysStatusLabel,
            this.PCExeSysProgressBar});
            this.PCExeSysStatusStrip.Location = new System.Drawing.Point(0, 428);
            this.PCExeSysStatusStrip.Name = "PCExeSysStatusStrip";
            this.PCExeSysStatusStrip.Size = new System.Drawing.Size(800, 22);
            this.PCExeSysStatusStrip.TabIndex = 0;
            this.PCExeSysStatusStrip.Text = "statusStrip1";
            // 
            // PCExeSysMenuStrip
            // 
            this.PCExeSysMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PCExeSysProjectMenuItem});
            this.PCExeSysMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.PCExeSysMenuStrip.Name = "PCExeSysMenuStrip";
            this.PCExeSysMenuStrip.Size = new System.Drawing.Size(800, 24);
            this.PCExeSysMenuStrip.TabIndex = 1;
            this.PCExeSysMenuStrip.Text = "menuStrip1";
            // 
            // PCExeSysProjectMenuItem
            // 
            this.PCExeSysProjectMenuItem.Name = "PCExeSysProjectMenuItem";
            this.PCExeSysProjectMenuItem.Size = new System.Drawing.Size(56, 20);
            this.PCExeSysProjectMenuItem.Text = "Project";
            // 
            // PCExeSysStatusLabel
            // 
            this.PCExeSysStatusLabel.Name = "PCExeSysStatusLabel";
            this.PCExeSysStatusLabel.Size = new System.Drawing.Size(122, 17);
            this.PCExeSysStatusLabel.Text = "PC ExeSys StatusLabel";
            // 
            // PCExeSysGUITimer
            // 
            this.PCExeSysGUITimer.Interval = 10;
            // 
            // PCExeSysProgressBar
            // 
            this.PCExeSysProgressBar.Name = "PCExeSysProgressBar";
            this.PCExeSysProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // MechatronicDesignSuiteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PCExeSysStatusStrip);
            this.Controls.Add(this.PCExeSysMenuStrip);
            this.MainMenuStrip = this.PCExeSysMenuStrip;
            this.Name = "MechatronicDesignSuiteForm";
            this.Text = "Mechatronic Design Suite - Test GUI";
            this.PCExeSysStatusStrip.ResumeLayout(false);
            this.PCExeSysStatusStrip.PerformLayout();
            this.PCExeSysMenuStrip.ResumeLayout(false);
            this.PCExeSysMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip PCExeSysStatusStrip;
        private System.Windows.Forms.MenuStrip PCExeSysMenuStrip;
        private System.Windows.Forms.ToolStripStatusLabel PCExeSysStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem PCExeSysProjectMenuItem;
        private System.Windows.Forms.ToolStripProgressBar PCExeSysProgressBar;
        private System.Windows.Forms.Timer PCExeSysGUITimer;
    }
}