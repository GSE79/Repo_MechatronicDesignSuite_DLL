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

namespace MechatronicDesignSuite_DLL
{
    [Serializable]
    public class imsBaseNode: IDisposable
    {
        [Category("Base Node"),
        Description("Name of Node")]
        public string NodeName
        {
            set {nodeName = value; }
            get {return nodeName; }
        }

        [Category("Base Node"),
        Description("Type of Node")]
        public Type NodeType
        {
            //set {; }
            get {return nodeType; }
        }

        [Category("Base Node"), Description("List Index of node in global node list")]
        public int GlobalNodeID
        {
            //set {; }
            get { return nodeGlobalID; }
        }

        protected string nodeName = null; 
        protected Type nodeType = null;
        protected int nodeGlobalID;

        public imsBaseNode(List<imsBaseNode> globalNodeListIn)
        {
            nodeType = typeof(imsBaseNode);
            nodeName = "Base Node";
            nodeGlobalID = globalNodeListIn.Count;
            globalNodeListIn.Add(this);
        }
        public imsBaseNode(BinaryFormatter DeSerializeFormatter, FileStream deSerializeFs)
        {
            nodeType = (Type)DeSerializeFormatter.Deserialize(deSerializeFs);
            nodeGlobalID = (int)DeSerializeFormatter.Deserialize(deSerializeFs);
            nodeName = (string)DeSerializeFormatter.Deserialize(deSerializeFs);
            ;
        }
        public virtual void writeNode2file(BinaryFormatter SerializeFormatter, FileStream SerializeFs)
        {
            SerializeFormatter.Serialize(SerializeFs, nodeType);
            SerializeFormatter.Serialize(SerializeFs, nodeGlobalID);
            SerializeFormatter.Serialize(SerializeFs, nodeName);
        }
        
        public virtual TreeNode toNewTreeNode()
        {
            TreeNode outNode = new TreeNode(nodeName);
            outNode.Name = nodeName;
            outNode.ToolTipText = nodeName + "\n" + nodeGlobalID.ToString() + "\n\n" + nodeType.ToString();
            outNode.Tag = this;
            return outNode;
        }


        #region iDisposable Interface Requirements
        bool disposed = false;
        protected virtual void ReleaseManagedResources()
        {
            
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
