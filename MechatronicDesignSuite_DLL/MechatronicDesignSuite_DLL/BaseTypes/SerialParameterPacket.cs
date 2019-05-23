using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Reflection.Emit;
using System.Globalization;
using System.Windows.Forms;
using System.ComponentModel;
using MechatronicDesignSuite_DLL.BaseNodes;
using MechatronicDesignSuite_DLL.BaseTypes;

namespace MechatronicDesignSuite_DLL
{
    [TypeConverter(typeof(SPpacketConverter))]
    public class SerialParameterPacket : IDisposable
    {
        //Access    //Data Type                 // Variable Name                    // Default Value
        protected   int                         MaxPacketPayLoadSize =              60;
        protected   imsCyclicPacketCommSystem   LinkedCommSystem;
        public      string                      PackDescription { set; get; } =     "New Serial Packet";
        public      int                         PackID { set; get; }
        public      bool                        HoldParsing =                       false;
        public      List<imsSerialParamData>    PacketSPDs { get; } =               new List<imsSerialParamData>();
        

        public SerialParameterPacket(imsCyclicPacketCommSystem CommSys, string PacketDescription, int PacketIDin, List<imsBaseNode> globalNodeListIn)
        {
            PackID = PacketIDin;
            PackDescription = PacketDescription;
            LinkedCommSystem = CommSys;
        }
        public void ParsePacket(byte [] PacketSerialDataIn, bool LogDataFlag, DateTime RxTime)
        {
            if(!HoldParsing)
            {
                foreach (imsSerialParamData SPD in PacketSPDs)
                {
                    SPD.UpdateValue(ref PacketSerialDataIn, LogDataFlag, RxTime);
                }
            }
            
        }
        public void AddSPD2Packet(imsSerialParamData spdIn)
        {
            int PackDOff = 0;
            foreach (imsSerialParamData SPD in PacketSPDs)
                SPD.AccumulatePackDataOffset(ref PackDOff);

            if (PackDOff + spdIn.getDataSize > MaxPacketPayLoadSize)
                throw new Exception("Add Parameter to Packet Failed: Parameter would cause packet to exceed maximum size.");
            else
            {
                PacketSPDs.Add(spdIn);
                spdIn.setcyclicCommSysLink = LinkedCommSystem;
                PackDOff = 0;
                foreach (imsSerialParamData SPD in PacketSPDs)
                    SPD.AccumulatePackDataOffset(ref PackDOff);
            }
        }
        public void ClearLoggedValues()
        {
            foreach (imsSerialParamData SPD in PacketSPDs)
                SPD.clearValues();
        }

        public void LinkSPDs2CommSystem(imsCyclicPacketCommSystem Sys2Link)
        {
            foreach (imsSerialParamData SPD in PacketSPDs)
                SPD.setcyclicCommSysLink = Sys2Link;
        }

        public string toCPkgFunctionDefinitionString()
        {
            string tempText = toCPkgFunctionPrototypeString();
            tempText = tempText.Replace("XPLAT_DLL_API", "");
            string startText = tempText.Substring(0, tempText.IndexOf("packStructPtr);\n") +("packStructPtr)").Length);
            string endText = tempText.Substring(tempText.IndexOf("packStructPtr);\n") + ("packStructPtr);\n").Length);

            endText = endText.Substring(0, endText.Length - 2);

            // Package Function Definition
            startText += "\n{\n";
            startText += "\t// Initialize pointer to start of packet area in output buffer\n\txplatAPI_Data->outPackBuffPtr = xplatAPI_Data->outputPacket;\n\t// Package bytes and increment pointer\n";
            int PacketOffset = 0;
            XPlatAutoGEN packXPlatAutoGEN = new XPlatAutoGEN(4);
            XPlatAutoGEN unpackXPlatAutoGEN = new XPlatAutoGEN(4);
            foreach (imsSerialParamData SPD in PacketSPDs)
            {
                if(LinkedCommSystem.SystemIsBigEndian)
                    packXPlatAutoGEN.AddLineTokens(SPD.toCPackFuncDefString(ref PacketOffset, "BIG"));
                else
                    packXPlatAutoGEN.AddLineTokens(SPD.toCPackFuncDefString(ref PacketOffset, "little"));
            }
            packXPlatAutoGEN.AlignColumnsInputTokens();
            startText += packXPlatAutoGEN.ReturnOutputLines() + "}\n";

            // Unpack Function Definition
            endText += "\n{\n";
            endText += "\t// Initialize pointer to start of packet area in input buffer\n\txplatAPI_Data->inPackBuffPtr = xplatAPI_Data->inputPacket;\n\t// UnPack bytes and increment pointer\n";
            endText += "\tswitch(xplatAPI_Data->inputBuffer[HDRPCKOFFSETINDEX])\n\t{\n";
            PacketOffset = 0;
            foreach (imsSerialParamData SPD in PacketSPDs)
            {
                if (LinkedCommSystem.SystemIsBigEndian)
                    unpackXPlatAutoGEN.AddLineTokens(SPD.toCUnPackFuncDefString(ref PacketOffset, "BIG"));
                else
                    unpackXPlatAutoGEN.AddLineTokens(SPD.toCUnPackFuncDefString(ref PacketOffset, "little"));
            }
            unpackXPlatAutoGEN.AlignColumnsInputTokens();
            endText += unpackXPlatAutoGEN.ReturnOutputLines() + "\n\t\tdefault:break;\n\t}\n}\n";

            return startText + endText;
        }
        public string toCPacketStructPointersString()
        {
            // Add Closing Struct Text
            string tempString = "";
            foreach (char thisChar in PackDescription)
                if (Char.IsLetterOrDigit(thisChar))
                    tempString += thisChar;
            if (!Char.IsLetter(tempString[0]))
                tempString = string.Concat("pck", tempString);

            return tempString + "struct*\t\t"+tempString+"Ptr;\n";

        }
        public string toCCommDefinitionString(string packUnpackString)
        {
            string nameString = "";
            foreach (char thisChar in PackDescription)
                if (Char.IsLetterOrDigit(thisChar))
                    nameString += thisChar;
            if (!Char.IsLetter(nameString[0]))
                nameString = string.Concat("pck", nameString);

            string startString = "\t\tcase PckID_" + nameString + ":\t// "+ PackDescription+"\n\t\t{\n\t\t\t";
            startString += packUnpackString + nameString + "(xplatAPI_Data, xplatAPI_Data->" + nameString +"Ptr);\n";
            startString += "\t\t\tbreak;\n\t\t}\n";
            return startString;
        }
        public string toCPkgFunctionPrototypeString()
        {
            string tempString = "";
            foreach (char thisChar in PackDescription)
                if (Char.IsLetterOrDigit(thisChar))
                    tempString += thisChar;
            if (!Char.IsLetter(tempString[0]))
                tempString = string.Concat("pck",tempString);

            string tempText = "//\n// - " + PackDescription + " - //\n// \n";
            tempText += "XPLAT_DLL_API void package" + tempString + "(xplatAPI_DATAstruct* xplatAPI_Data, " + tempString+"struct* packStructPtr);\n";
            tempText += "XPLAT_DLL_API void unpack" + tempString + "(xplatAPI_DATAstruct* xplatAPI_Data, " + tempString + "struct* unpackStructPtr);\n";
            return tempText;
        }
        public string toCTypeString()
        {
            // Loop SPDs in Packet
            // Add Opening Struct Text
            string tempText = "//\n// - " + PackDescription + " - //\n// \ntypedef struct\n{\n";
            int tempInt = 0;
            XPlatAutoGEN myXPlatAutoGEN = new XPlatAutoGEN(5);
            foreach (imsSerialParamData SPD in PacketSPDs)
                myXPlatAutoGEN.AddLineTokens(SPD.toCTypeString(ref tempInt));
            myXPlatAutoGEN.AlignColumnsInputTokens();
            tempText += myXPlatAutoGEN.ReturnOutputLines();
                

            // Add Closing Struct Text
            string tempString = "";
            foreach (char thisChar in PackDescription)
                if (Char.IsLetterOrDigit(thisChar))
                    tempString += thisChar;
            if (!Char.IsLetter(tempString[0]))
                tempString = string.Concat("pck", tempString);
            tempText += "}XPLAT_DLL_API " + tempString + "struct;\t// Export - " + PackDescription + " - //\n";
            tempText += "#define\tPckSize_" + tempString + "\t\t" + tempInt.ToString() + "\t\t// ( 0x" + tempInt.ToString("X2") + " )\n";
            tempText += "#define\tPckID_" + tempString + "\t\t" + PackID.ToString() + "\t\t// ( 0x" + PackID.ToString("X2") + " )\n";

            return tempText;
        }

        #region iDisposable Interface Requirements
        bool disposed = false;
        protected virtual void ReleaseManagedResources()
        {
            int PackIdx;
            for (PackIdx = PacketSPDs.Count - 1; PackIdx >= 0; PackIdx--)
                PacketSPDs[PackIdx].Dispose();
        }
        protected virtual void ReleaseUnManagedResources()
        {

        }
        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    ReleaseManagedResources();
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                ReleaseUnManagedResources();

                // Note disposing has been done.
                disposed = true;

            }
        }

        // Use interop to call the method necessary
        // to clean up the unmanaged resource.
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~SerialParameterPacket()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }
    
    #endregion
    }

    public class SPpacketConverter : ExpandableObjectConverter
    {
        // functions for property grid expandability
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return base.GetPropertiesSupported(context);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
        {
            if (destinationType == typeof(SerialParameterPacket))
                return true;

            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
        {
            if (destinationType == typeof(System.String) && value is imsBaseNode)
            {

                SerialParameterPacket so = (SerialParameterPacket)value;
                return so.PackDescription;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}


