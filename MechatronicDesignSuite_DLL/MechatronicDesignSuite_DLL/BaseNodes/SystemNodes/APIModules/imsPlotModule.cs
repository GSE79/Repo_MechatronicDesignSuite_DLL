using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using MechatronicDesignSuite_DLL.BaseTypes;

namespace MechatronicDesignSuite_DLL.BaseNodes
{
    public class imsPlotModule : imsAPISysModule
    {
        [Category("Plot Module System"), Description("Modulo value of MainLoop() cycles on which to trigger plot updates")]
        public uint PlotUpdateModulo {get{ return plotUpdateModulo;} set { plotUpdateModulo = value; } }
        uint plotUpdateModulo = 10;
        
        uint mainLoopModuloCounter = 0;
        public override void MainLoop()
        {
            if(mainLoopModuloCounter++ % plotUpdateModulo == 0)
            {
                foreach (ImsBaseForm imsBF in sysModForms)
                {
                    ((imsPlotPane)(imsBF)).plotValues();
                }
            }

            
        }


        #region System Constructors
        public imsPlotModule(List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            NodeType = typeof(imsPlotModule);
            NodeName = "Plot Module System";
        }
        public imsPlotModule(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs) : base(DeSerializeFormatter, deSerializeFs)
        {

        }
        public imsPlotModule(PCExeSys PCExeSysIn, List<imsBaseNode> globalNodeListIn) : base(globalNodeListIn)
        {
            NodeType = typeof(imsPlotModule);
            NodeName = "Plot Module System";
            if (PCExeSysIn != null)
                PCExeSysLink = PCExeSysIn;
        }
        #endregion
    }
}
