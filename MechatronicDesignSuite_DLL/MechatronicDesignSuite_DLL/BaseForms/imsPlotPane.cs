using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MechatronicDesignSuite_DLL.BaseNodes;
using MechatronicDesignSuite_DLL.BaseTypes;
using ZedGraph;

namespace MechatronicDesignSuite_DLL
{
    public partial class imsPlotPane : Form, ImsBaseForm
    {
        public PCExeSys pCExeSysLink
        {
            set
            {
                if (value != null)
                {
                    pcexesys = value;
                    this.Show();
                    //updateTreeView();
                }
                else
                    throw (new Exception("Attempted Null Link of pcexesys"));
            }
            get { return pcexesys; }
        }
        PCExeSys pcexesys;

        List<imsValueNode> plotParamReference = new List<imsValueNode>();
        public double plotWindow = 60;
        double xMax;
        ToolTip PlotToolTip = new ToolTip();

        List<string> Y1Units = new List<string>();
        List<string> Y2Units = new List<string>();

        public imsPlotPane()
        {
            InitializeComponent();

            ZedgraphControl1.GraphPane.Title.IsVisible = false;

            ZedgraphControl1.GraphPane.XAxis.Title.Text = "Time (sec)";
            ZedgraphControl1.GraphPane.YAxis.Title.Text = "Y1 Axis";
            ZedgraphControl1.GraphPane.YAxis.MinorGrid.IsVisible = false;
            ZedgraphControl1.GraphPane.YAxis.MajorGrid.IsVisible = false;

            ZedgraphControl1.GraphPane.Legend.IsVisible = true;

            ZedgraphControl1.GraphPane.Y2Axis.IsVisible = true;
            ZedgraphControl1.GraphPane.Y2Axis.Title.Text = "Y2 Axis";
            ZedgraphControl1.GraphPane.Y2Axis.MajorGrid.IsVisible = false;
            ZedgraphControl1.GraphPane.Y2Axis.MinorGrid.IsVisible = false;

            Text = "Plot Pane";
          
        }

        public imsPlotPane(TabPage plotTabPage)
        {
            InitializeComponent();

            this.splitContainer2.Parent = plotTabPage;
            this.splitContainer2.Visible = true;
            Text = "Plot Pane";
            ZedgraphControl1.GraphPane.Title.IsVisible = false;

            ZedgraphControl1.GraphPane.XAxis.Title.Text = "Time (sec)";
            ZedgraphControl1.GraphPane.YAxis.Title.Text = "Y1 Axis";
            ZedgraphControl1.GraphPane.YAxis.MinorGrid.IsVisible = false;
            ZedgraphControl1.GraphPane.YAxis.MajorGrid.IsVisible = false;

            ZedgraphControl1.GraphPane.Legend.IsVisible = true;

            ZedgraphControl1.GraphPane.Y2Axis.IsVisible = true;
            ZedgraphControl1.GraphPane.Y2Axis.Title.Text = "Y2 Axis";
            ZedgraphControl1.GraphPane.Y2Axis.MajorGrid.IsVisible = false;
            ZedgraphControl1.GraphPane.Y2Axis.MinorGrid.IsVisible = false;
            this.Visible = false;
        }

        private void ZedgraphControl1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }
        public void DropParam(GUIValueLinks GUIVlinkIN, int axisSelect)
        {
            // show initial setting for plot window
            if (treeView1.Nodes[0].Nodes.Count > 0)
            {
                treeView1.Nodes[0].Nodes[0].Text = plotWindow.ToString();
            }
            else
            {
                treeView1.Nodes[0].Nodes.Add(plotWindow.ToString());
            }
            


            // Initialize some temporary variables
            string NodeName = "";
            int totalCount = 0;
            Color[] plotColors = { Color.Red, Color.Blue, Color.Black, Color.Brown, Color.DarkCyan, Color.LimeGreen, Color.Purple, Color.Green, Color.HotPink, Color.ForestGreen };

            NodeName = GUIVlinkIN.vNodeLink.getNodeName;

            // Capture total count of nodes in tree
            totalCount = treeView1.Nodes[1].Nodes.Count + treeView1.Nodes[2].Nodes.Count;

            // Limit the total count to 0-8, Limit one of each name key, to prevent duplicates, no exact name matches!!
            // Otherwise, process the drop...
            if (!treeView1.Nodes[axisSelect].Nodes.ContainsKey(NodeName) && totalCount < 9)
            {
                // Add a node to the treeview with our temp nodename
                treeView1.Nodes[axisSelect].Nodes.Add(new TreeNode(NodeName));
                treeView1.Nodes[axisSelect].Nodes[treeView1.Nodes[axisSelect].Nodes.Count - 1].Name = NodeName;
                // Add the tooltip text to the node
                treeView1.Nodes[axisSelect].Nodes[treeView1.Nodes[axisSelect].Nodes.Count - 1].ToolTipText = "Right Click to Remove from Plot";

                // Select a color for the new dropped node
                bool duplicateColor = true;
                // Loop through all possible colors from our color list
                foreach (Color c in plotColors)
                {
                    duplicateColor = false;
                    // For each color, loop through each curve on the plot
                    foreach (LineItem curve in ZedgraphControl1.GraphPane.CurveList)
                    {
                        // If the colors match, indicate duplicate and move on
                        if (curve.Color == c)
                        {
                            duplicateColor = true;
                            break;
                        }

                    }

                    // Then, IF its not a duplicate color, set the curve color to this color and exit the loop
                    if (!duplicateColor)
                    {
                        treeView1.Nodes[axisSelect].Nodes[treeView1.Nodes[axisSelect].Nodes.Count - 1].ForeColor = c;
                        ZedgraphControl1.GraphPane.AddCurve(NodeName, new PointPairList(), c, SymbolType.None);
                        if (axisSelect > 1)
                            ZedgraphControl1.GraphPane.CurveList[this.ZedgraphControl1.GraphPane.CurveList.Count - 1].IsY2Axis = true;
                        break;
                    }
                }

                // Add a link to the serialparameterdata object for use in GUI callbacks
                plotParamReference.Add(GUIVlinkIN.vNodeLink);
                // Establish links between curve and treenode
                treeView1.Nodes[axisSelect].Nodes[treeView1.Nodes[axisSelect].Nodes.Count - 1].Tag = ZedgraphControl1.GraphPane.CurveList[this.ZedgraphControl1.GraphPane.CurveList.Count - 1];
                // Establish links between curve and serialparameterdata object
                ZedgraphControl1.GraphPane.CurveList[this.ZedgraphControl1.GraphPane.CurveList.Count - 1].Tag = GUIVlinkIN;// plotParamReference[plotParamReference.Count - 1];
            }

            updateAxisUnitsText();

            // Set Legend visible and expand the treeview
            ZedgraphControl1.GraphPane.Legend.IsVisible = true;
            treeView1.ExpandAll();
        }
        public void ZedgraphControl1_DragDrop(object sender, DragEventArgs e)
        {
            GUIValueLinks GUIVLinkDropped = null;
            int axisSelect = 2;

            // Analyze drop location according to window geometry, and select Y1 vs Y2
            if (e == null && sender == null)
                axisSelect = 1;
            else if (e == null && sender != null)
                axisSelect = 1;
            else if (e.X == 0 && e.Y == 0)
            {
                axisSelect = 1;
            }
            else if (e.X == 1 && e.Y == 1)
                axisSelect = 2;
            else if (e.X <= ZedgraphControl1.ParentForm.Location.X + ZedgraphControl1.Parent.Location.X + ZedgraphControl1.Size.Width / 2)
                axisSelect = 1;
            else
                axisSelect = 2;

            if(((GUIValueLinks)e.Data.GetData(typeof(GUIValueLinks))) != null)
            {
                GUIVLinkDropped = ((GUIValueLinks)e.Data.GetData(typeof(GUIValueLinks)));
                DropParam(GUIVLinkDropped, axisSelect);
            }

            ZedgraphControl1.Refresh();
        }

        void updateAxisUnitsText()
        {
            // Update Y axis titles with units from serial parameters
            ZedgraphControl1.GraphPane.YAxis.Title.Text = "Y1 ";
            ZedgraphControl1.GraphPane.Y2Axis.Title.Text = "Y2 ";
            foreach (CurveItem CI in ZedgraphControl1.GraphPane.CurveList)
            {
                if (!CI.IsY2Axis)// Y1 Axis
                {
                    if (typeof(GUIValueLinks).IsInstanceOfType(CI.Tag))
                    {
                        if (!Y1Units.Contains(((GUIValueLinks)CI.Tag).UnitsString))
                            Y1Units.Add(((GUIValueLinks)CI.Tag).UnitsString);
                    }
                }
                else // Y2 Axis
                {
                    if (typeof(GUIValueLinks).IsInstanceOfType(CI.Tag))
                    {
                        if (!Y2Units.Contains(((GUIValueLinks)CI.Tag).UnitsString))
                            Y2Units.Add(((GUIValueLinks)CI.Tag).UnitsString);
                    }
                }
            }
            foreach (string str in Y1Units)
                ZedgraphControl1.GraphPane.YAxis.Title.Text += (str + " ");
            foreach (string str in Y2Units)
                ZedgraphControl1.GraphPane.Y2Axis.Title.Text += (str + " ");
        }

        public void plotValues()
        {

            if (ZedgraphControl1.GraphPane.CurveList.Count > 0)
            {

                bool onePointAdded = false;
                int curveIndex = 0;
                int firstIndex = 0;
                foreach (CurveItem curve in ZedgraphControl1.GraphPane.CurveList)
                {
                    if (curve.Points.Count > 0)
                        firstIndex = plotParamReference[curveIndex].LatchTimes.FindIndex(x => x > curve.Points[curve.Points.Count - 1].X);
                    else
                        firstIndex = 0;

                    if (firstIndex > -1)
                        for (int i = firstIndex; i < plotParamReference[curveIndex].getLength(); i++)
                        {
                            curve.AddPoint(plotParamReference[curveIndex].LatchTimes[i], plotParamReference[curveIndex].ToPlotDouble((GUIValueLinks)curve.Tag));
                            onePointAdded = true;
                        }


                    if (curve.NPts > 0 && curveIndex == 0)
                        xMax = curve.Points[curve.Points.Count - 1].X;
                    else if (curve.NPts > 0)
                        xMax = Math.Max(xMax, curve.Points[curve.Points.Count - 1].X);
                    curveIndex++;
                }

                if (onePointAdded)
                {
                    if (xMax - plotWindow >= 0)
                        ZedgraphControl1.GraphPane.XAxis.Scale.Min = xMax - plotWindow;
                    else
                        ZedgraphControl1.GraphPane.XAxis.Scale.Min = 0;
                    ZedgraphControl1.GraphPane.XAxis.Scale.Max = xMax;


                    ZedgraphControl1.ScrollMinX = 0;
                    ZedgraphControl1.ScrollMaxX = xMax;
                    ZedgraphControl1.AxisChange();
                    ZedgraphControl1.Refresh();
                }

            }

        }

        public void clearPlotValues()
        {
            
            foreach (CurveItem curve in ZedgraphControl1.GraphPane.CurveList)
                curve.Clear();

            ZedgraphControl1.AxisChange();
            ZedgraphControl1.Refresh();

        }

        public void ClearPlotSymbols()
        {
            clearPlotValues();
            Y1Units.Clear();
            Y2Units.Clear();

            plotParamReference.Clear();//.Remove(((SerialParameterData)(((CurveItem)treeView1.SelectedNode.Tag).Tag)));
            ZedgraphControl1.GraphPane.CurveList.Clear();//.Remove(((CurveItem)treeView1.SelectedNode.Tag));
            treeView1.Nodes.Clear(); // Remove(treeView1.SelectedNode);

            treeView1.ExpandAll();

            updateAxisUnitsText();

            ZedgraphControl1.Refresh();

        }



      

        private void PlotPropsTreeView_MouseClick(object sender, MouseEventArgs e)
        {
            // the curve is tagged with the guivalue link
            // the node is tagged with the curve
            // the plotparameterreference it the value node linked by the guivalue link

            treeView1.SelectedNode = treeView1.GetNodeAt(e.X, e.Y);

            if (e.Button == MouseButtons.Right)
            {
                if(treeView1.SelectedNode.Parent != null && treeView1.SelectedNode != treeView1.Nodes[0].Nodes[0])
                {
                    if (!((CurveItem)treeView1.SelectedNode.Tag).IsY2Axis)// Y1 Axis
                        Y1Units.Remove(((GUIValueLinks)(((CurveItem)treeView1.SelectedNode.Tag).Tag)).UnitsString);
                    else// Y2 Axis
                        Y2Units.Remove(((GUIValueLinks)(((CurveItem)treeView1.SelectedNode.Tag).Tag)).UnitsString);

                    plotParamReference.Remove((((GUIValueLinks)(((CurveItem)treeView1.SelectedNode.Tag).Tag)).vNodeLink));
                    ZedgraphControl1.GraphPane.CurveList.Remove(((CurveItem)treeView1.SelectedNode.Tag));
                    treeView1.Nodes.Remove(treeView1.SelectedNode);

                    treeView1.ExpandAll();

                    // Update Y axis titles with units from serial parameters
                    updateAxisUnitsText();

                    ZedgraphControl1.Refresh();
                }
                //else if(treeView1.SelectedNode == treeView1.Nodes[0].Nodes[0])
                //{
                //    treeView1.SelectedNode.BeginEdit();
                    
                //}
            }
            if (e.Button == MouseButtons.Left)
            {
                if (treeView1.SelectedNode.Parent != null && treeView1.SelectedNode != treeView1.Nodes[0].Nodes[0])
                {
                    foreach (LineItem curve in this.ZedgraphControl1.GraphPane.CurveList)
                    {
                        curve.Line.Width = 1.0F;
                    }
                    if (((LineItem)(treeView1.SelectedNode.Tag)) != null)
                        ((LineItem)(treeView1.SelectedNode.Tag)).Line.Width = 3.0F;
                }
            }
        }

        private void ZedgraphControl1_MouseHover(object sender, EventArgs e)
        {
            PlotToolTip.SetToolTip(ZedgraphControl1, "Drag N Drop to add signal to Plot.\nFrom: Parameter Labels \nTo: Leftside Plot Y1 or Rightside Plot Y2");

        }

        public ulong getBytes()
        {
            ulong theseBytes = 0;
            foreach (CurveItem curve in ZedgraphControl1.GraphPane.CurveList)
                theseBytes += ((ulong)curve.Points.Count * 16);

            return theseBytes;

        }

        private void imsPlotPane_FormClosing(object sender, FormClosingEventArgs e)
        {
            pCExeSysLink.PlotModuleProperty.sysModFormList.Remove(this);
        }

        private void ZedgraphControl1_Load(object sender, EventArgs e)
        {

        }

        
        private void treeView1_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node != treeView1.Nodes[0].Nodes[0])
                e.CancelEdit = true;
            
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            double tempD;
            if (double.TryParse(e.Label, out tempD))
            {
                if (tempD > 0)
                {
                    plotWindow = tempD;
                    // show initial setting for plot window
                    if (treeView1.Nodes[0].Nodes.Count > 0)
                    {
                        treeView1.Nodes[0].Nodes[0].Text = plotWindow.ToString();
                    }
                    else
                    {
                        treeView1.Nodes[0].Nodes.Add(plotWindow.ToString());
                    }
                    //updateAxisUnitsText();
                    return;
                }
                else
                    e.CancelEdit = true;
            }
            else
                e.CancelEdit = true;


            



        }
    }
}
