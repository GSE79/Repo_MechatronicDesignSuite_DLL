using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using MechatronicDesignSuite_DLL.BaseNodes;
using MechatronicDesignSuite_DLL.BaseTypes;

namespace MechatronicDesignSuite_DLL
{
    public partial class imsExtAppConsole : Form, ImsBaseForm
    {
        ExtAppWrapper extAppWrapper;
        public bool runInvisible { set; get; } = false;
        public PCExeSys pCExeSysLink
        {
            set
            {
                if (value != null)
                {
                    pcexesys = value;
                    this.Show();
                }
                else
                    throw (new Exception("Attempted Null Link of pcexesys"));
            }
            get { return pcexesys; }
        }
        PCExeSys pcexesys;

        List<byte> stdOutByteList = new List<byte>();

        public imsExtAppConsole()
        {
            InitializeComponent();
        }
        public imsExtAppConsole(string ExeAppPath)
        {
            InitializeComponent();
            extAppWrapper = new ExtAppWrapper(new string[] { ExeAppPath });
        }
        public void UpdateUI()
        {
            int tempLen = stdOutByteList.Count;
            extAppWrapper.readStdOutput(ref stdOutByteList);
            if (stdOutByteList.Count > tempLen)
            {
                List<byte> newBytes = stdOutByteList.GetRange(tempLen, stdOutByteList.Count - tempLen);
                string newString = "";

                foreach (byte b in newBytes)
                    newString += (char)b;

                richTextBox1.AppendText(newString);
            }
        }

        private void imsExtAppConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            extAppWrapper.writeStdInput(new byte[] { Convert.ToByte('x') }, 1);
            extAppWrapper.shutdownAndExit();
        }
    }
}
