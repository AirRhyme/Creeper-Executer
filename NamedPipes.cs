using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Windows.Forms;



namespace Creeper_Executer
{
    internal class NamedPipes
    {
        public static string cmdpipe = "StoXzCmd";

        public static string scriptpipe = "StoXzC";



        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WaitNamedPipe(string name, int timeout);

        public static bool NamedPipeExist(string pipeName)
        {
            bool flag;
            try
            {
                int timeout = 0;
                if (!NamedPipes.WaitNamedPipe(Path.GetFullPath(string.Format("\\\\\\\\.\\\\pipe\\\\{0}", pipeName)), timeout))
                {
                    int lastWin32Error = Marshal.GetLastWin32Error();
                    if (lastWin32Error == 0)
                    {
                        flag = false;
                        bool result = flag;
                        return result;
                    }
                    if (lastWin32Error == 2)
                    {
                        flag = false;
                        bool result = flag;
                        return result;
                    }
                }
                flag = true;
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }

        public static void CommandPipe(string command)
        {
            if (NamedPipes.NamedPipeExist(NamedPipes.cmdpipe))
            {
                try
                {
                    using (NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", NamedPipes.cmdpipe, PipeDirection.Out))
                    {
                        namedPipeClientStream.Connect();
                        using (StreamWriter streamWriter = new StreamWriter(namedPipeClientStream))
                        {
                            streamWriter.Write(command);
                            streamWriter.Dispose();
                        }
                        namedPipeClientStream.Dispose();
                    }
                    return;
                }
                catch (IOException)
                {
                    MessageBox.Show("Error occured connecting to the pipe.", "Connection Failed!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                catch (Exception arg_6D_0)
                {
                    MessageBox.Show(arg_6D_0.Message.ToString());
                    return;
                }
            }
            MessageBox.Show("Inject " + Functions.dll + " before Using this!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void LuaCPipe(string script)
        {
            if (NamedPipes.NamedPipeExist(NamedPipes.scriptpipe))
            {
                try
                {
                    using (NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", NamedPipes.scriptpipe, PipeDirection.Out))
                    {
                        namedPipeClientStream.Connect();
                        using (StreamWriter streamWriter = new StreamWriter(namedPipeClientStream))
                        {
                            streamWriter.Write(script);
                            streamWriter.Dispose();
                        }
                        namedPipeClientStream.Dispose();
                    }
                    return;
                }
                catch (IOException)
                {
                    MessageBox.Show("Error occured connecting to the pipe.", "Connection Failed!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                catch (Exception arg_6D_0)
                {
                    MessageBox.Show(arg_6D_0.Message.ToString());
                    return;
                }
            }
            MessageBox.Show("Inject " + Functions.dll + " before Using this!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
