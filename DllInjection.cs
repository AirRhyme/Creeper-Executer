using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Creeper_Executer
{
    internal class DllInjection
    {
        public enum DllInjectionResult
        {
            DllNotFound,
            GameProcessNotFound,
            InjectionFailed,
            Success
        }

        public sealed class DllInjector
        {
            private static readonly IntPtr INTPTR_ZERO;

            private static DllInjection.DllInjector _instance;

            public static DllInjection.DllInjector GetInstance
            {
                get
                {
                    bool flag = DllInjection.DllInjector._instance == null;
                    if (flag)
                    {
                        DllInjection.DllInjector._instance = new DllInjection.DllInjector();
                    }
                    return DllInjection.DllInjector._instance;
                }
            }

            static DllInjector()
            {
                DllInjection.DllInjector.INTPTR_ZERO = (IntPtr)0;
            }

            private DllInjector()
            {
            }

            private bool bInject(uint pToBeInjected, string sDllPath)
            {
                IntPtr intPtr = DllInjection.DllInjector.OpenProcess(1082u, 1, pToBeInjected);
                bool flag = intPtr == DllInjection.DllInjector.INTPTR_ZERO;
                bool result;
                if (flag)
                {
                    result = false;
                }
                else
                {
                    IntPtr procAddress = DllInjection.DllInjector.GetProcAddress(DllInjection.DllInjector.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                    bool flag2 = procAddress == DllInjection.DllInjector.INTPTR_ZERO;
                    if (flag2)
                    {
                        result = false;
                    }
                    else
                    {
                        IntPtr intPtr2 = DllInjection.DllInjector.VirtualAllocEx(intPtr, (IntPtr)0, (IntPtr)sDllPath.Length, 12288u, 64u);
                        bool flag3 = intPtr2 == DllInjection.DllInjector.INTPTR_ZERO;
                        if (flag3)
                        {
                            result = false;
                        }
                        else
                        {
                            byte[] bytes = Encoding.ASCII.GetBytes(sDllPath);
                            bool flag4 = DllInjection.DllInjector.WriteProcessMemory(intPtr, intPtr2, bytes, (uint)bytes.Length, 0) == 0;
                            if (flag4)
                            {
                                result = false;
                            }
                            else
                            {
                                bool flag5 = DllInjection.DllInjector.CreateRemoteThread(intPtr, (IntPtr)0, DllInjection.DllInjector.INTPTR_ZERO, procAddress, intPtr2, 0u, (IntPtr)0) == DllInjection.DllInjector.INTPTR_ZERO;
                                if (flag5)
                                {
                                    result = false;
                                }
                                else
                                {
                                    DllInjection.DllInjector.CloseHandle(intPtr);
                                    result = true;
                                }
                            }
                        }
                    }
                }
                return result;
            }

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern int CloseHandle(IntPtr hObject);

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern IntPtr GetModuleHandle(string lpModuleName);

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

            public DllInjection.DllInjectionResult Inject(string sProcName, string sDllPath)
            {
                bool flag = !File.Exists(sDllPath);
                DllInjection.DllInjectionResult result;
                if (flag)
                {
                    result = DllInjection.DllInjectionResult.DllNotFound;
                }
                else
                {
                    uint num = 0u;
                    Process[] processes = Process.GetProcesses();
                    for (int i = 0; i < processes.Length; i++)
                    {
                        bool flag2 = processes[i].ProcessName != sProcName;
                        if (!flag2)
                        {
                            num = (uint)processes[i].Id;
                            break;
                        }
                    }
                    bool flag3 = num == 0u;
                    if (flag3)
                    {
                        result = DllInjection.DllInjectionResult.GameProcessNotFound;
                    }
                    else
                    {
                        bool flag4 = !this.bInject(num, sDllPath);
                        if (flag4)
                        {
                            result = DllInjection.DllInjectionResult.InjectionFailed;
                        }
                        else
                        {
                            result = DllInjection.DllInjectionResult.Success;
                        }
                    }
                }
                return result;
            }

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, uint flAllocationType, uint flProtect);

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, uint size, int lpNumberOfBytesWritten);
        }
    }
}
