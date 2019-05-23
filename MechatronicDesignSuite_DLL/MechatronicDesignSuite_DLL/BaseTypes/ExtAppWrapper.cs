using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace MechatronicDesignSuite_DLL.BaseTypes
{
    public class ExtAppWrapper
    {
        public class InStruct
        {
            public Process p;
            public MemoryStream s;
        }

        Process process;

        Thread outputThread;
        Thread errorThread;
        Thread inputThread;

        MemoryStream inputStream = new MemoryStream();
        MemoryStream errorStream = new MemoryStream();
        MemoryStream outputStream = new MemoryStream();

        InStruct InputStructure = new InStruct();

        public ExtAppWrapper(string[] args)
        {
            string fileName = args[0];

            InputStructure.p = process;
            InputStructure.s = outputStream;

            // Fires up a new process to run inside this one
            process = Process.Start(new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,

                FileName = fileName
            });
            // Depending on your application you may either prioritize the IO or the exact opposite
            const ThreadPriority ioPriority = ThreadPriority.Highest;
            outputThread = new Thread(outputReader) { Name = "ChildIO Output", Priority = ioPriority };
            errorThread = new Thread(errorReader) { Name = "ChildIO Error", Priority = ioPriority };
            inputThread = new Thread(inputReader) { Name = "ChildIO Input", Priority = ioPriority };

            // Set as background threads (will automatically stop when application ends)
            outputThread.IsBackground = errorThread.IsBackground
                = inputThread.IsBackground = true;

            // Start the IO threads
            outputThread.Start(process);
            errorThread.Start(process);
            inputThread.Start(process);
            
        }
        public void shutdownAndExit()
        {
            // Signal to end the application
            ManualResetEvent stopApp = new ManualResetEvent(false);

            // Enables the exited event and set the stopApp signal on exited
            process.EnableRaisingEvents = true;
            process.Exited += (e, sender) => { stopApp.Set(); };

            // Wait for the child app to stop
            stopApp.WaitOne();

            // Write some nice output for now?
            Console.WriteLine();
            Console.Write("Process ended... shutting down host");
            Thread.Sleep(1000);
        }
        public void writeStdInput(byte[] buff2write, int numBytes2write )
        {
            // Demonstrate that the host app can be written to by the application
            //process.StandardInput.WriteLine("Message from host");
            inputStream.Write(buff2write, 0, numBytes2write);
        }
        public void readStdOutput(ref List<byte> outList)
        {            
            int tempByte;
            outputStream.Position = 0;
            while ((outputStream.Length>0)&&(outList.Count < outputStream.Length))
            {
                tempByte = outputStream.ReadByte();
                if(tempByte!=-1)
                    outList.Add((byte)tempByte);
            }       
        }
        /// <summary>
        /// Continuously copies data from one stream to the other.
        /// </summary>
        /// <param name="instream">The input stream.</param>
        /// <param name="outstream">The output stream.</param>
        private static void passThrough(Stream instream, Stream outstream)
        {
            while (true)
            {
                instream.CopyTo(outstream);
                outstream.Flush();
                Thread.Sleep(0);
            }
        }

        private void outputReader(object p)
        {
            var process = (Process)p;
            // Pass the standard output of the child to our output
            passThrough(process.StandardOutput.BaseStream, outputStream);
        }

        private void errorReader(object p)
        {
            var process = (Process)p;
            // Pass the standard error of the child to our error
            passThrough(process.StandardError.BaseStream, errorStream);
        }

        private void inputReader(object p)
        {
            var process = (Process)p;
            // Pass our input into the standard input of the child
            passThrough(inputStream, inputStream);
        }
    }
}
