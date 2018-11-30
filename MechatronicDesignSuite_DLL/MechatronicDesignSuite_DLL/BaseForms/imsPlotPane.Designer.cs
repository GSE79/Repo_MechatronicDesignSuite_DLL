using ZedGraph;

namespace MechatronicDesignSuite_DLL
{
    partial class imsPlotPane
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Y Axis 1 (Left)");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Y Axis 2 (Right)");
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ZedgraphControl1 = new ZedGraph.ZedGraphControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.Color.Silver;
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ZedgraphControl1);
            this.splitContainer2.Size = new System.Drawing.Size(1128, 434);
            this.splitContainer2.SplitterDistance = 231;
            this.splitContainer2.SplitterWidth = 10;
            this.splitContainer2.TabIndex = 1;
            // 
            // ZedgraphControl1
            // 
            this.ZedgraphControl1.AllowDrop = true;
            this.ZedgraphControl1.BackColor = System.Drawing.Color.Silver;
            this.ZedgraphControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ZedgraphControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ZedgraphControl1.IsShowHScrollBar = true;
            this.ZedgraphControl1.Location = new System.Drawing.Point(0, 0);
            this.ZedgraphControl1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ZedgraphControl1.Name = "ZedgraphControl1";
            this.ZedgraphControl1.ScrollGrace = 0D;
            this.ZedgraphControl1.ScrollMaxX = 0D;
            this.ZedgraphControl1.ScrollMaxY = 0D;
            this.ZedgraphControl1.ScrollMaxY2 = 0D;
            this.ZedgraphControl1.ScrollMinX = 0D;
            this.ZedgraphControl1.ScrollMinY = 0D;
            this.ZedgraphControl1.ScrollMinY2 = 0D;
            this.ZedgraphControl1.Size = new System.Drawing.Size(885, 432);
            this.ZedgraphControl1.TabIndex = 3;
            this.ZedgraphControl1.DragDrop += new System.Windows.Forms.DragEventHandler(this.ZedgraphControl1_DragDrop);
            this.ZedgraphControl1.DragEnter += new System.Windows.Forms.DragEventHandler(this.ZedgraphControl1_DragEnter);
            this.ZedgraphControl1.MouseHover += new System.EventHandler(this.ZedgraphControl1_MouseHover);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(229, 432);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.treeView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(202, 424);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Plot Signals";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(202, 424);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Available Signals";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Y Axis 1 (Left)";
            treeNode2.Name = "Node1";
            treeNode2.Text = "Y Axis 2 (Right)";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(196, 418);
            this.treeView1.TabIndex = 2;
            // 
            // imsPlotPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 434);
            this.Controls.Add(this.splitContainer2);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "imsPlotPane";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Form2";
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        public ZedGraphControl ZedgraphControl1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}