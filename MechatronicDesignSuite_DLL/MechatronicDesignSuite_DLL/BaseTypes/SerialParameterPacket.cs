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
        public string PackDescription { set; get; } = "New Serial Packet";
        public int PackID { set; get; }
        public List<imsSerialParamData> PacketSPDs { get; } = new List<imsSerialParamData>();
        public SerialParameterPacket(string PacketDescription, int PacketIDin, List<imsBaseNode> globalNodeListIn)
        {
            PackID = PacketIDin;
            PackDescription = PacketDescription;
        }
        public void ParsePacket(byte [] PacketSerialDataIn, bool LogDataFlag, DateTime RxTime)
        {
            foreach(imsSerialParamData SPD in PacketSPDs)
            {
                SPD.UpdateValue(ref PacketSerialDataIn, LogDataFlag, RxTime);
            }
        }
        public void AddSPD2Packet(imsSerialParamData spdIn)
        {
            PacketSPDs.Add(spdIn);
            int PackDOff = 0;
            foreach (imsSerialParamData SPD in PacketSPDs)
                SPD.AccumulatePackDataOffset(ref PackDOff);
        }
        public void ClearLoggedValues()
        {
            foreach (imsSerialParamData SPD in PacketSPDs)
                SPD.clearValues();
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


