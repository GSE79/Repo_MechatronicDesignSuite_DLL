namespace MechatronicDesignSuite_DLL
{
    partial class imsExceptionViewer
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
            this.ExceptionTreeview = new System.Windows.Forms.TreeView();
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
            this.splitContainer1.Panel1.Controls.Add(this.ExceptionTreeview);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.NodePropertyGrid);
            this.splitContainer1.Size = new System.Drawing.Size(1689, 1341);
            this.splitContainer1.SplitterDistance = 562;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 3;
            // 
            // NodePropertyGrid
            // 
            this.NodePropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NodePropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.NodePropertyGrid.Name = "NodePropertyGrid";
            this.NodePropertyGrid.Size = new System.Drawing.Size(1117, 1341);
            this.NodePropertyGrid.TabIndex = 0;
            // 
            // ExceptionTreeview
            // 
            this.ExceptionTreeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExceptionTreeview.Location = new System.Drawing.Point(0, 0);
            this.ExceptionTreeview.Name = "ExceptionTreeview";
            this.ExceptionTreeview.Size = new System.Drawing.Size(562, 1341);
            this.ExceptionTreeview.TabIndex = 3;
            this.ExceptionTreeview.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ExceptionTreeview_MouseDoubleClick);
            // 
            // imsExceptionViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1689, 1341);
            this.Controls.Add(this.splitContainer1);
            this.Name = "imsExceptionViewer";
            this.Text = "imsExceptionViewer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView ExceptionTreeview;
        private System.Windows.Forms.PropertyGrid NodePropertyGrid;
    }
}