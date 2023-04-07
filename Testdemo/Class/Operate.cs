using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Drawing;
using System.IO;

namespace ICD.Class
{
    public class Operate
    {
        public void OperateOSK(bool Open = false)
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName == "osk")
                {
                    if (!Open)
                    {
                        process.Kill();
                    }
                    return;
                }
            }
            if (Open)
            {
                if (File.Exists(Application.StartupPath + "\\osk.exe"))
                {
                    Process.Start(Application.StartupPath + "\\osk.exe");
                }
            }
        }
    }
}
