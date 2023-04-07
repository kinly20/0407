using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace IntelligentScrewing
{
    public partial class Frm_welcome : Form
    {
        public Frm_welcome()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 
        }

        public static ushort ver = 20;
        private void Frm_welcome_Load(object sender, EventArgs e)
        {
            Process[] pProcess = Process.GetProcesses();
            labInfor.Text = "";
            if ((Process.GetProcessesByName(Application.ProductName).Length - 1) > 0)
            {
                MessageBox.Show("软件已启动，请勿重复运行！");
                for (int i = 0; i <= pProcess.Length - 1; i++)
                {
                    if (pProcess[i].ProcessName == "IntelligentScrewing")
                    {
                        pProcess[i].Kill();
                    }
                }
                this.Dispose();
            }
            for (int i = 0; i <= pProcess.Length - 1; i++)
            {
                if (pProcess[i].ProcessName == "osk")
                {
                    pProcess[i].Kill();
                }
            }
            if (System.IO.Directory.Exists("D:\\"))
            {
                Parameter.ParaDisk = "D:\\";
            }
            else
            {
                Parameter.ParaDisk = "C:\\";
            }
            Parameter.ParaPath = Parameter.ParaDisk + "NewAutoScrew\\Parameter\\";
            Parameter.SysParaFile = Parameter.ParaDisk + "NewAutoScrew\\Parameter\\ParaSave.ini";
            Parameter.PlcAlarmFile = Parameter.ParaDisk + "NewAutoScrew\\Parameter\\PlcAlarm.txt";
            Parameter.ReportPath = Parameter.ParaDisk + "NewAutoScrew\\ReportData\\";
            Parameter.RptDataFile = Parameter.ParaDisk + "NewAutoScrew\\ReportData\\rptdb.accdb";
            Parameter.ImagePath = Parameter.ParaDisk + "NewAutoScrew\\DeviceImage\\";
            Parameter.ProgFilePath = Parameter.ParaDisk + "NewAutoScrew\\工艺文件\\";
            Parameter.CameraPath = Parameter.ParaDisk + "NewAutoScrew\\Parameter\\Camera\\";
            Parameter.TableFile = Parameter.ParaDisk + "\\NewAutoScrew\\TableConfig.ini";
            Parameter.LogoPath = Parameter.ParaDisk + "\\NewAutoScrew\\logo.jpg";
            //
            tools.FileSomething fst = new tools.FileSomething();
            fst.DirectoryCheckAndCreate(Parameter.ParaPath);
            fst.FileCheckAndCreate(Parameter.SysParaFile);
            fst.DirectoryCheckAndCreate(Parameter.ReportPath);
            fst.DirectoryCheckAndCreate(Parameter.ImagePath);
            fst.DirectoryCheckAndCreate(Parameter.ProgFilePath);
            fst.DirectoryCheckAndCreate(Parameter.CameraPath);
            
            //copy file from app.path to D:\
            if (!System.IO.File.Exists(Parameter.PlcAlarmFile))
            {
                System.IO.File.Copy(Application.StartupPath + "\\PlcAlarm.txt", Parameter.PlcAlarmFile, true);
            }
            if (System.IO.File.Exists(Parameter.LogoPath))
            {
                picLogo.Load(Parameter.LogoPath);
            }
            
            //         
            labVer.Text ="V6.1.0."+ver;
            Parameter.SupUserMac[0] = "C8-60-00-77-C8-72";
            Parameter.SupUserMac[1] = "14-2D-27-46-4C-79";
            Parameter.SupUserMac[2] = "28-D2-44-70-A6-35";
            Parameter.SupUserMac[3] = "54-EE-75-9E-F3-D3"; //LZH
            Timer1.Enabled = true;
        }
        

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Timer1.Enabled = false;
            MainForm mf = new MainForm();
            mf.Show();
            this.Hide();
            //Timer1.Stop();
        }
    }
}
