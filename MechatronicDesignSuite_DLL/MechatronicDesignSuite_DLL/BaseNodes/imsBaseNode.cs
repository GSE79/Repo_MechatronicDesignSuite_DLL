using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Reflection.Emit;
using System.Globalization;

namespace MechatronicDesignSuite_DLL.BaseNodes
{
    /// <summary>
    /// imsBaseNode : IDisposable
    /// </summary>
    [TypeConverterAttribute(typeof(BaseModelClassConverter)), DescriptionAttribute("Expand to see...")]
    public class imsBaseNode: IDisposable
    {
        /// <summary>
        /// NodeName
        /// </summary>
        protected string NodeName = "base node";
        [Category("Base Node"), Description("Name of Node")]
        public string getNodeName {get {return NodeName; }  }
        [Category("Base Node"), Description("Name of Node")]
        public string setNodeName { set { NodeName = value; } get { return NodeName; } }

        /// <summary>
        /// NodeType
        /// </summary>
        protected Type NodeType = typeof(imsBaseNode);
        [Category("Base Node"), Description("Type of Node")]
        public Type getNodeType { get { return NodeType; } }
        [Category("Base Node"), Description("Type of Node")]
        public Type setNodeType { get { return NodeType; } set { NodeType = value; } }

        /// <summary>
        /// GlobalNodeID
        /// </summary>
        protected int GlobalNodeID;
        [Category("Base Node"), Description("List Index of node in global node list")]
        public int getGlobalNodeID { get { return GlobalNodeID; } }
        [Category("Base Node"), Description("List Index of node in global node list")]
        public int setGlobalNodeID { get { return GlobalNodeID; } set { GlobalNodeID = value; } }

        /// <summary>
        /// globalNodeListLink
        /// </summary>
        protected List<imsBaseNode> globalNodeListLink;

        /// <summary>
        /// imsBaseNode()
        /// </summary>
        /// <param name="globalNodeListIn"></param>
        public imsBaseNode(List<imsBaseNode> globalNodeListIn)
        {
            NodeType = typeof(imsBaseNode);
            NodeName = "Base Node";
            GlobalNodeID = globalNodeListIn.Count;
            globalNodeListIn.Add(this);
            globalNodeListLink = globalNodeListIn;
        }
        /// <summary>
        /// imsBaseNode()
        /// </summary>
        /// <param name="DeSerializeFormatter"></param>
        /// <param name="deSerializeFs"></param>
        public imsBaseNode(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs)
        {
            NodeType = (Type)DeSerializeFormatter.Deserialize(deSerializeFs);
            GlobalNodeID = (int)DeSerializeFormatter.Deserialize(deSerializeFs);
            NodeName = (string)DeSerializeFormatter.Deserialize(deSerializeFs);    
        }
        /// <summary>
        /// writeNode2File()
        /// </summary>
        /// <param name="SerializeFormatter"></param>
        /// <param name="SerializeFs"></param>
        public virtual void writeNode2file(BinaryFormatter SerializeFormatter, FileStream SerializeFs)
        {
            SerializeFormatter.Serialize(SerializeFs, NodeType);
            SerializeFormatter.Serialize(SerializeFs, GlobalNodeID);
            SerializeFormatter.Serialize(SerializeFs, NodeName);
        }
        /// <summary>
        /// restoreFromFile()
        /// </summary>
        /// <param name="DeSerializeFormatter"></param>
        /// <param name="deSerializeFs"></param>
        public virtual void restoreFromFile(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs)
        {
            Type deSerialType = (Type)DeSerializeFormatter.Deserialize(deSerializeFs);

            if (deSerialType == NodeType)
                NodeType = deSerialType;
            else
                throw (new Exception("Attempeted to restore from wrong node type"));

            GlobalNodeID = (int)DeSerializeFormatter.Deserialize(deSerializeFs);
            NodeName = (string)DeSerializeFormatter.Deserialize(deSerializeFs);
        }

        /// <summary>
        /// toNewTreeNode()
        /// </summary>
        /// <returns></returns>
        public virtual TreeNode toNewTreeNode()
        {
            TreeNode outNode = new TreeNode(NodeName);
            outNode.Name = NodeName;
            outNode.ToolTipText = NodeName + "\n" + GlobalNodeID.ToString() + "\n\n" + NodeType.ToString();
            outNode.Tag = this;
            return outNode;
        }
        

        #region iDisposable Interface Requirements
        bool disposed = false;
        protected virtual void ReleaseManagedResources()
        {
            globalNodeListLink.Remove(this);
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
        ~imsBaseNode()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }
    }
    #endregion

    /// <summary>
    /// BaseModelClassConverter
    /// </summary>
    public class BaseModelClassConverter : ExpandableObjectConverter
    {
        // functions for property grid expandability
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return base.GetPropertiesSupported(context);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
        {
            if (destinationType == typeof(imsBaseNode))
                return true;

            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
        {
            if (destinationType == typeof(System.String) && value is imsBaseNode)
            {

                imsBaseNode so = (imsBaseNode)value;
                return so.getNodeName;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    /// <summary>
    /// DynamicNodeCreator
    /// </summary>
    public class DynamicNodeCreator
    {
        delegate imsBaseNode CtorInvoker();
        CtorInvoker CtorHandler = null;

        public DynamicNodeCreator(Type type, Type[] ctorArgs)
        {
            CreateConstructor(type.GetConstructor(ctorArgs));
        }

        void CreateConstructor(ConstructorInfo targetNode)
        {
            DynamicMethod dynamic = new DynamicMethod(string.Empty,
                                                    typeof(imsBaseNode),
                                                    new Type[0],
                                                    targetNode.DeclaringType);
            ILGenerator il = dynamic.GetILGenerator();
            il.DeclareLocal(targetNode.DeclaringType);
            il.Emit(OpCodes.Newobj, targetNode);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);
        }
    }
    

    
}
