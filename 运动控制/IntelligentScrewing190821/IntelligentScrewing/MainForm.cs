using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.IO.Ports;
using System.Net.NetworkInformation;
using ThreeAxisTable;
using System.Runtime.InteropServices;
using IntelligentScrewing.tools;
using DxfParser;


namespace IntelligentScrewing
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        [DllImport("winmm.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int timeGetTime();
        private Daffodil.UI.VisionView visionView = new Daffodil.UI.VisionView();
        private ComboBox[] sysPara_Cbb,pdtPara_Cbb;
        private TextBox[] sysPara_txt, pdtPara_txt;
        public DeviceInfor.DeviceServer deviceServer = new DeviceInfor.DeviceServer();
        public System.Timers.Timer deviceServerMonitor = new System.Timers.Timer(100);
        AlarmControl_DKG.AlarmList alarmList = new AlarmControl_DKG.AlarmList();
        DBHelper.DBWR dbwr = new DBHelper.DBWR();
        public static string[] AxisType = new string[2] { "A轴", "B轴" };
        public static string[] ScrewType = new string[3] { "1#送钉机", "2#送钉机","相机"};
        public static string[] WorkMode = new string[2] { "普通模式", "避障模式" };
        public DataBase.DataBase dataBase = new DataBase.DataBase();
        public Table tab = new ThreeAxisTable.Table();
        DxfParser.DxfShow dxfparser = new DxfParser.DxfShow();
        TransportSystem.Transport transport = new TransportSystem.Transport();
        string lastFile = "", productCode="-1";
        public static int selectionindex = 0;
        public int startNum = 0,startNum2=0;
        private int deviceStatus, allCount, allErrorCount, productCount;          
        public bool spindleRun = false,safetyLight=false;
        private int feed1Ready = 0;
        public bool getScrew1 = false, getScrew2 = false,CanTurn=false,turnGo=false,turnBack=false;
        private int testMode = 0;
        bool auto = false, pause = false, stop = false, reset = false, productReady = false, codeRequest = false, run = false, DBlink=false,showPic=false;
        bool table1Run = false, table2Run = false;
        bool Unscrew = false;
        int[] axisPara = new int[15];
        //Alm ALM = new Alm();
        int productStartTime;
        float PointTime;
        short[] LineControlToPLC = new short[50], LineStatusFromPLC = new short[50];
        public string Mstatus = "系统初始化...";
        string[] databaseList = new string[18], DBhelperData=new string[14];
        int workPointNum,workErrorNum,workLooseNum,workFloatNum,productDone;
        Stopwatch almtime = new Stopwatch();
        ModbusTCP.ModbusTCP modbusPlc = new ModbusTCP.ModbusTCP();
        ModbusTCP.ModbusTCP siemensPLC = new ModbusTCP.ModbusTCP();
        short PLClink = 0, siemensLink = 0;
        System.Timers.Timer PLCmonitor = new System.Timers.Timer(60);
        short[] mw0 = new short[50], mw100 = new short[50], ReadFromSiemens = new short[100], WriteToSiemens = new short[100];

        int[] lifetimes = new int[4];
        List<float[]> floatCheckData = new List<float[]>(); 
        private CoordCorrectionPoint[] startPoint=new CoordCorrectionPoint[2], endPoint=new CoordCorrectionPoint[2];
        public LicencePro.Licence lic = new LicencePro.Licence();
        public ushort patchnum;
        int commboxSectionIndex = -1;
        private XHwritetoSQL.WriteToSQL WR_ToSQL;
        private bool lxScannerOK = false;
        private AFurl afurl = new AFurl();

          
        public List<DxfParser.DxfShow.PointDraw> ElementListOrg = new List<DxfParser.DxfShow.PointDraw>();
       
        public List<DxfParser.DxfShow.PointDraw> ElementListOrg2 = new List<DxfParser.DxfShow.PointDraw>();
        public struct CoordCorrectionPoint
        {
            public float X;
            public float Y;
            public int index;
        }      
        private void GetIP()
        {
            string hostName = Dns.GetHostName();//本机名   
            System.Net.IPAddress[] addressList = Dns.GetHostByName(hostName).AddressList;//会警告GetHostByName()已过期，我运行时且只返回了一个IPv4的地址   
            //System.Net.IPAddress[] addressList = Dns.GetHostAddresses(hostName);//会返回所有地址，包括IPv4和IPv6   
            foreach (IPAddress ip in addressList)
            {
                combbPara6.Items.Add(ip.ToString());
                combbPara11.Items.Add(ip.ToString());
            }
        }
        private static bool Delay(int DeTime)
        {
            Stopwatch sw0 = new Stopwatch();
            sw0.Start();
            while (sw0.ElapsedMilliseconds < DeTime)
            {
                Application.DoEvents();
            }
            sw0.Stop();
            return true;
        }
        private void GetCommon()
        {
            string[] common = SerialPort.GetPortNames();
            if(common!=null)
            combbPara7.Items.AddRange(common);
        }
        private void AddDGV_ComboBoxColumn(DataGridView DGV,int index,string[] List,string ColumnName )
        {
           
            DataGridViewComboBoxColumn DGVC = new DataGridViewComboBoxColumn();
            DGVC.Items.AddRange(List);
            DGVC.HeaderText = ColumnName;
            DGVC.Width = 150;
            DGVC.Resizable = 0;
            DGVC.FillWeight = 150;
            DGVC.MinimumWidth = 90;
            DGVC.SortMode = DataGridViewColumnSortMode.NotSortable;
            DGVC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV.Columns.Insert(index,DGVC);
        }
        private void TabControlShow(TabControl tab)
        {
            foreach (TabPage page in tab.TabPages)
            {
                tab.SelectTab(page);
                foreach (Control ctl in page.Controls)
                {
                    if (ctl is TabControl)
                    {
                        TabControlShow((TabControl)ctl);
                    }
                }
            }
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
           
            GBpara1.Enabled = false;
            GBpara2.Enabled = false;
            GBpara3.Enabled = false;
            GBpara4.Enabled = false;
            GBpara5.Enabled = false;
            GBpara6.Enabled = false;
            GBpara7.Enabled = false;
            GBpara8.Enabled = false;
            GBpara9.Enabled = false;
            GBpara10.Enabled = false;
            GBpara11.Enabled = false;
            AddDGV_ComboBoxColumn(DataGrid,3, AxisType, "主 轴");
            AddDGV_ComboBoxColumn(DataGrid, 4, ScrewType, "工 艺");
            AddDGV_ComboBoxColumn(DataGrid, 5, WorkMode, "避 障");
            AddDGV_ComboBoxColumn(DataGrid2, 3, AxisType, "主 轴");
            AddDGV_ComboBoxColumn(DataGrid2, 4, ScrewType, "工 艺");
            AddDGV_ComboBoxColumn(DataGrid2, 5, WorkMode, "避 障");
            cmbManuFeedSel.SelectedIndex = 0;
            cmbManuToolSel.SelectedIndex = 0;
            TabPage4.Controls.Add(transport);
            transport.Dock = DockStyle.Fill;
            transport.BackgroundThreadExitFlag = true;
            tabPage9.Controls.Add(alarmList);
            alarmList.Dock = DockStyle.Fill;
            alarmList.NewAlarm += alarmList_NewAlarm;
            alarmList.GetPlcAlarmData += alarmList_GetPlcAlarmData;
            //ALM.AlmShow.Visible = true;
            //ALM.LoadAlm(Parameter.PlcAlarmFile);
            tabPage8.Controls.Add(dataBase);
            dataBase.Dock = DockStyle.Fill;
            DBlink= dataBase.LinkDb(Parameter.RptDataFile);
            GetIP();
            GetCommon();
            Parameter.LoadPara();
            cbb_TableSelect.SelectedIndex = 0;
            if (Parameter.Para[0]=="单面线")
            {
                transport.EnableFlipFunction = false;
            }
            else if (Parameter.Para[0] == "双面线" | Parameter.Para[0] == "新线体")
            {
                if (Parameter.Para[0] == "双面线")
                {
                  transport.EnableFlipFunction = true;
                }
                PLClink = modbusPlc.Link(Parameter.Para[6], 0, Parameter.Para[54], 502, "LFL2014-MODBUSTCP-V1");
                PLCmonitor.Elapsed += PLCmonitor_Elapsed;
                PLCmonitor.Start();
                if(PLClink!=0)
                {
                    MessageBox.Show("PLC连接失败!");
                }
            }else
            {
                TabPage4.Parent = null;
                butPage6.Visible = false;
                butPage8.Location = butPage6.Location;
            }
            if(Parameter.Para[5]=="爱立信")
            {
                siemensLink=siemensPLC.Link(Parameter.Para[6], 0, "192.168.0.20", 502, "LFL2014-MODBUSTCP-V1");
                transport.EnableFlipFunction = false;
                btnSysReset.Visible = true;
                btnSysReset.Enabled = true;
                btnSysReset.BringToFront();
                btnSysReset.Location = btnScrewMode.Location;
                btnScrewMode.Enabled = false;
                btnScrewMode.Visible = false;

                label17.Visible = false;
                combbPara1.Visible = false;
                label41.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                combbPara11.Visible = false;
                txtPara9.Visible = false;
                txtPara10.Visible = false;
                txtPara11.Visible = false;
                cbb_TableSelect.Visible = false;
                labItemTotal.Visible = false;
                ckb_xh.Visible = false;
                butGoHome.Visible = false;
                button17.Visible = false;
            }
            if (combbPara3.SelectedIndex==1)
            {
                label89.Visible = false;
                label99.Visible = false;
                txtPara37.Visible = false;
                txtPara38.Visible = false;
                txtPara41.Visible = false;
                txtPara42.Visible = false;
                button15.Visible = false;
                button16.Visible = false;
            }
                
           if(Parameter.Para[22]=="手动扫码")
           {
               txtManuCode.Visible = true;
           }
            patchnum = ushort.Parse(Parameter.Para[52]);
            if(patchnum>=100)
            {
                patchnum = (ushort)(patchnum % 100);
            }
            if(patchnum==0)
            {
                patchnum = 1;
            }
            if (Parameter.Para[19] == "滚筒上料")
            {
                //tab.tableMode = 1;
                //tab.SongdingjiMode = "滚筒";
                butFeedOpera3.Text = "滚筒正转";
                butFeedOpera4.Visible = true;
                button18.Visible = true;
                btnJiaju.Visible = true;
                cmbManuToolSel.SelectedIndex = 0;
                cmbManuToolSel.Enabled = false;

            }
            if(Parameter.Para[0]=="桌面双平台")
            {
                tab.tableMode = 1;
                //tab.SongdingjiMode = "滚筒";
            }
            if (System.IO.File.Exists(Parameter.LogoPath))
            {
                PictureBox1.Load(Parameter.LogoPath);
            }
            if(Parameter.Para[1]=="启用")
            {
                if (Parameter.Para[11].Length > 5)
                {
                    deviceServer.IniSender(Parameter.Para[11], 8500);
                    deviceServerMonitor.Elapsed += deviceServerMonitor_Elapsed;
                    deviceServerMonitor.Start();
                }
            }
            if(Parameter.Para[4]=="启用")
            {
                tabPage7.Controls.Add(visionView);
                visionView.Visible = true;
                visionView.Dock = DockStyle.Fill;
               // visionView.CloseCamera();
              if(!visionView.OpenCamera())
               {
                   MessageBox.Show("打开相机失败,请检查连接!");
               }
            }else
            {
                tabPage7.Parent = null;
                butPage8.Visible = false;
            }
            grpButtonArea.BringToFront();
            sysPara_Cbb = new ComboBox[] { combbPara0, combbPara1, combbPara2, combbPara3, combbPara4, combbPara5, combbPara6, combbPara7, combbPara8, combbPara9, combbPara10,
                combbPara11, combbPara12, combbPara13, combbPara14, combbPara15, combbPara16, combbPara17, combbPara18, combbPara19, combbPara20, combbPara21, combbPara22, combbPara23 };
            pdtPara_Cbb = new ComboBox[] { combbParaP0, combbParaP1, combbParaP2, combbParaP3, combbParaP4, combbParaP5, combbParaP6 };

            sysPara_txt = new TextBox[] { txtPara0, txtPara1, txtPara2, txtPara3, txtPara4, txtPara5, txtPara6, txtPara7, txtPara8, txtPara9, txtPara10, txtPara11, txtPara12,
                txtPara13, txtPara14, txtPara15, txtPara16, txtPara17, txtPara18, txtPara19, txtPara20, txtPara21, txtPara22, txtPara23,
                txtPara24, txtPara25, txtPara26, txtPara27, txtPara28, txtPara29, txtPara30, txtPara31, txtPara32, txtPara33, txtPara34, txtPara35, txtPara36, txtPara37, txtPara38,
                txtPara39, txtPara40, txtPara41, txtPara42, txtPara43, txtPara44, txtPara45, txtPara46, txtPara47 , txtPara48 ,txtPara49};
            pdtPara_txt = new TextBox[] { txtParaP0, txtParaP1, txtParaP2, txtParaP3, txtParaP4, txtParaP5, txtParaP6, txtParaP7, txtParaP8, txtParaP9, txtParaP10, txtParaP11,
                txtParaP12, txtParaP13, txtParaP14, txtParaP15, txtParaP16, txtParaP17, txtParaP18, txtParaP19, txtParaP20, txtParaP21, txtParaP22, txtParaP23, txtParaP24,
                txtParaP25, txtParaP26, txtParaP27, txtParaP28, txtParaP29, txtParaP30, txtParaP31,txtParaP32, txtParaP33, txtParaP34, txtParaP35, txtParaP36, txtParaP37,
                txtParaP38, txtParaP39, txtParaP40, txtParaP41, txtParaP42, txtParaP43, txtParaP44, txtParaP45, txtParaP46, txtParaP47};           
            UpdataParaUI();
            for (int i = 0; i < lifetimes.Length;i++ )
            {
                lifetimes[i] = int.Parse(Parameter.Para[89 + i]);
            }
                toolStatus.Text = "系统初始化...";
             toolError.Text = "";
             if (Screen.AllScreens.Count() > 1)
             {
                 Monitor moniShow = new Monitor();
                 //moniShow.FormBorderStyle = FormBorderStyle.None;
                 moniShow.StartPosition = FormStartPosition.Manual;
                 // moniShow.Size.Width = Screen.AllScreens[1].Bounds.Width;
                 //moniShow.Size.Height = Screen.AllScreens[1].Bounds.Height;
                 moniShow.Size = new System.Drawing.Size(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
                 moniShow.Location = Screen.AllScreens[0].Bounds.Location;
                 moniShow.Show();
                 moniShow.Controls.Add(dxfparser);
                 dxfparser.Dock = DockStyle.Fill;
             }
             else
             {
                 TabPage6.Controls.Add(dxfparser);
                 dxfparser.Location = new Point(5, 5);
             }
            Table.tableConfigFile = Parameter.TableFile;
            tabPage5.Controls.Add(tab);
            tab.Initial();
            timUIUpdata.Enabled = true;
            timUIUpdata.Start();
            timScan.Start();
            WR_ToSQL = new XHwritetoSQL.WriteToSQL(Parameter.Para[59]);
            feedwork.RunWorkerAsync();
            Mesh.WebApi.SendScanFinishToPLC += WebApi_SendScanFinishToPLC;
            Mesh.WebApi.LoadConfig();
            ushort com=0;
            if (Parameter.Para[7].Length>=3)
            ushort.TryParse(Parameter.Para[7].Remove(0, 3),out com);
            Mesh.WebApi.ScannerPort = com;
            Mesh.WebApi.ScannerBaud = 115200;
            Mesh.WebApi.ScannerDelay = int.Parse(Parameter.Para[97]);
            Ini.IniFile ini = new Ini.IniFile(Parameter.SysParaFile);
            string tempfile = ini.IniReadValue("lastfile", "1");
            Tab1.SelectedIndex = 6;
            Thread.Sleep(100);
            Tab1.SelectedIndex = 0;
            TabControlShow(Tab1);
            Tab1.SelectedIndex = 0;
            if(File.Exists(tempfile))
            {
                string[] temp1 = tempfile.Remove(tempfile.Length - 4, 4).Split('.', '\\');
                temp1[0] = tempfile.Remove(tempfile.Length - 4, 4);
                cmbProgName.Items.Add(temp1[temp1.Length - 1]);
                cmbProgName.SelectedItem = temp1[temp1.Length - 1];
                LoadProgFile(tempfile);
                UpdateGrid(0, DataGrid);
                SavePointData(0);
                dxfparser.OutlinePic(DxfParser.DxfShow.outline, DxfShow.ElementList);
                dxfparser.ShowPic();
                labItemTotal.Text = "产品总数:" + cmbProgName.Items.Count;
                string pathtemp = Parameter.CameraPath + cmbProgName.Text + ".xml";
                visionView.LoadVisionFile(pathtemp, true);
                if (Parameter.Para[193].Trim() != "0")
                {
                    visionView.CoordinateTransform.CameraParam.OffsetX = float.Parse(Parameter.Para[193]);
                    visionView.CoordinateTransform.CameraParam.OffsetY = float.Parse(Parameter.Para[194]);
                }
                txtProdName.Text = cmbProgName.Text;
            }
        }
        short[] almdata = new short[20];
        string lastAlmMess;
        short[] alarmList_GetPlcAlarmData()
        {
            return almdata;
        }

        void alarmList_NewAlarm(string obj)
        {
            lastAlmMess = obj;
        }

        void WebApi_SendScanFinishToPLC()
        {
            lxScannerOK = true;
            Delay(500);
        }

        void deviceServerMonitor_Elapsed(object sender, ElapsedEventArgs e)
        {
            deviceServerMonitor.Stop();
            if (!deviceServer.ServerState)
            {
                deviceServer.IniSender(Parameter.Para[11], 8500);
            }
            deviceServer.SendDataSet(Parameter.Para[51], 1000, 0);
            deviceServer.SendDataSet(Parameter.Para[53], 1000, 1);
            deviceServer.SendDataSet("V6.1.0.18", 1000, 3);
            deviceServer.SendDataSet(Parameter.Para[50], 1000, 2);
            deviceServer.SendDataSet(txtProdName.Text, 1001, 0);
            DBhelperData[1] = cmbProgName.Text;
            deviceServer.SendDataSet((Points.points.Count-2).ToString(), 1001, 1);
            deviceServer.SendDataSet(txtSeriNum.Text, 1001, 2);
            
            if (lastAlmMess.Length > 0)
            {
                deviceServer.SendDataSet(lastAlmMess, 2009, 0);
                deviceStatus = 9;
            }
            deviceServer.SendDataSet(deviceStatus.ToString(), 2000, 0);
            deviceServer.SendDataSet(allCount.ToString(), 2001, 0);
            deviceServer.SendDataSet(allErrorCount.ToString(), 2001, 1);
            float a=1;
            if(allCount!=0)
             a= ((float)allCount - allErrorCount) / allCount;
            deviceServer.SendDataSet(a.ToString("P"), 2001, 2);
            deviceServer.SendDataSet(productCount.ToString(), 2001, 3);
                    
            deviceServer.SendDataSet(pdcode, 3000, 0);
            //deviceServer.SendDataSet(pdCount.ToString(), 3000, 1);
            //deviceServer.SendDataSet(pdErrorCode.ToString(), 3000, 2);
            int command = deviceServer.ClientCommand;
            if (command != 0 &  !auto)
            {
                string str = deviceServer.CliUpdateProd;
                string[] temp = str.Split(',');
                cmbProgName.Items.Clear();
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i] != "")
                        DownLoadCSVfile(temp[i]);
                }
                if (cmbProgName.Items.Count > 0)
                {
                    cmbProgName.SelectedIndex = 0;
                }
            }
            deviceServerMonitor.Start();
        }
        string DownLoadCSVfile(string file)
        {
        //    char[] split = new char[] { '/', '\\' };
        //    string[] temp = file.Split(split);
        //    int n = temp.Length;
        //    int m = temp[n - 1].Length;
        //    string name = temp[n - 1].Remove(m - 4);
        //    cmbProgName.Items.Add(name);
        //    string ip = txtFTPip.Text.Trim();
        //    string user = txtFTPuser.Text.Trim();
        //    string password = txtFTPpassword.Text.Trim();
        //    FtpWebRequest ftp;
        //    FtpWebResponse response = null;
        //    Stream inStream = null;
        //    Stream outStream = null;
        //    string filename = Path + "\\pdlist\\" + name + ".csv";
        //    string fileshow = @"D:\工艺文件\ftp\" + name + ".csv";
        //    //Console.WriteLine("准备连接服务器......");
        //    try
        //    {
        //        ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ip + "/" + file));
        //        ftp.Credentials = new NetworkCredential(user, password);//指定用户名和密码
        //        ftp.Method = WebRequestMethods.Ftp.DownloadFile;
        //        ftp.UseBinary = true;
        //        response = (FtpWebResponse)ftp.GetResponse();
        //        inStream = response.GetResponseStream();
        //        if (File.Exists(filename))
        //        {
        //            File.Delete(filename);
        //        }
        //        File.Create(filename).Close();

        //        outStream = File.OpenWrite(filename);
        //        byte[] buffer = new byte[4096];
        //        int size = 0;
        //        while ((size = inStream.Read(buffer, 0, 4096)) > 0)
        //        {
        //            outStream.Write(buffer, 0, size);
        //        }
        //        return "0";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.ToString();
        //    }
        //    finally
        //    {
        //        if (inStream != null)
        //            inStream.Close();
        //        if (outStream != null)
        //            outStream.Close();
        //        if (response != null)
        //            response.Close();
        //        string[] pathlist = System.IO.Directory.GetFiles(@"D:\工艺文件\ftp");
        //        foreach (string strFile in pathlist)
        //        {
        //            System.IO.File.Delete(strFile);
        //        }
        //        File.Copy(filename, fileshow);
        //    }
            return "";

        }
        
        void PLCmonitor_Elapsed(object sender, ElapsedEventArgs e)
        {
            PLCmonitor.Stop();
            if(PLClink!=0)
            {
                PLClink = modbusPlc.Link(Parameter.Para[6], 0, Parameter.Para[54], 502, "LFL2014-MODBUSTCP-V1");
            }
            if(PLClink==0)
            {
                short[] tempR = new short[50];
                short re = modbusPlc.Read(ModbusTCP.ModbusTCP.Memory.DataArea, 100, 50, ref tempR);
                if (re == 0)
                {
                    tempR.CopyTo(mw100, 0);
                }
               
                modbusPlc.Write(ModbusTCP.ModbusTCP.Memory.DataArea, 0, 50, mw0);
            }
            if (Parameter.Para[2] == "ALX")
            {
                if (siemensLink != 0)
                {
                    siemensLink = siemensPLC.Link(Parameter.Para[6], 0, "192.168.0.20", 502, "LFL2014-MODBUSTCP-V1");
                }
                if (siemensLink == 0)
                {
                    short[] tempR = new short[100];
                    short re = siemensPLC.Read(ModbusTCP.ModbusTCP.Memory.DataArea, 0, 100, ref tempR);
                    if (re == 0)
                    {
                        tempR.CopyTo(ReadFromSiemens, 0);
                    }
                    WriteToSiemens[0] = 100;
                    WriteToSiemens[1] = 101;
                    siemensPLC.Write(ModbusTCP.ModbusTCP.Memory.DataArea, 100, 100, WriteToSiemens);
                }

            }
            PLCmonitor.Start();
        }
        private void UpdataParaUI()
        {

            for (int i = 0; i < sysPara_Cbb.Length; i++)
            {
                sysPara_Cbb[i].Text = Parameter.Para[i];
            }
            for (int i = 0; i < sysPara_txt.Length; i++)
            {
                sysPara_txt[i].Text = Parameter.Para[i + 50];
            }
            for (int i = 0; i < pdtPara_Cbb.Length; i++)
            {
                pdtPara_Cbb[i].Text = Parameter.Para[i + 100];
            } for (int i = 0; i < pdtPara_txt.Length; i++)
            {
                pdtPara_txt[i].Text = Parameter.Para[i + 150];
            } 
            
        }
        private void butParaAck_Click(object sender, EventArgs e)
        {
            
                for (int i = 0; i < sysPara_Cbb.Length; i++)
                {
                    Parameter.Para[i] = sysPara_Cbb[i].Text;
                }
                for (int i = 0; i < sysPara_txt.Length; i++)
                {
                    Parameter.Para[i + 50] = sysPara_txt[i].Text;
                }
                MessageBox.Show("设备参数已保存!");
           
            Parameter.Para[250] = LineControlToPLC[4].ToString();
            Parameter.Para[251] = LineControlToPLC[22].ToString();
            Parameter.Para[252] = LineControlToPLC[26].ToString();
            Parameter.Para[253] = LineControlToPLC[28].ToString();
            Parameter.Para[254] = LineControlToPLC[30].ToString();
            Parameter.Para[255] = LineControlToPLC[32].ToString();
            //visionView.TakePhoto();
            //visionView
            //visionView.GetResult();
          
               
            //visionView.TransformMachineCoordinateWithRotationErrorCorrect()
            
                for (int i = 0; i < pdtPara_Cbb.Length; i++)
                {
                    Parameter.Para[i + 100] = pdtPara_Cbb[i].Text;
                } for (int i = 0; i < pdtPara_txt.Length; i++)
                {
                    Parameter.Para[i + 150] = pdtPara_txt[i].Text;
                }
                int n = cbb_TableSelect.SelectedIndex;
                if (n == 0)
                {
                    if (cmbProgName.Text != "")
                    {
                        SaveProgFlie(0, cmbProgName.Text, DataGrid, DxfShow.outline, false);
                    }
                }
                if (n == 1)
                {
                    if (cmbProgName2.Text != "")
                    {                        
                       SaveProgFlie(1, cmbProgName2.Text, DataGrid2, DxfShow.outline2,false);                      
                    }
                }
            
            if (Parameter.Para[193].Trim() != "0")
            {
                visionView.CoordinateTransform.CameraParam.OffsetX = float.Parse(Parameter.Para[193]);
                visionView.CoordinateTransform.CameraParam.OffsetY = float.Parse(Parameter.Para[194]);
            }
            Parameter.SavePara(Parameter.SysParaFile);
            GetSDJpara(0);
            GetSDJpara(1);
            
        }

        #region//CAD解析
        private void butOpenDxf_Click(object sender, EventArgs e)
        {
            if (!licenseCheck())
            {
                return;
            }
            DxfEdit dxfEdit = new DxfEdit();
            dxfEdit.ShowDialog();
            if (dxfEdit.Yes)
            {
                butKeyPanle_Click(null, null);
                OpenFileDialog of = new OpenFileDialog();
                of.FileName = "";
                of.Title = "打开DXF加工文件";
                of.Filter = "加工文件(*.dxf)|*.dxf";
                List<string> DataList = new List<string>();
                if (of.ShowDialog() == DialogResult.OK)
                {
                    DxfShow.outline.Clear();
                    DxfShow.ElementList.Clear();
                    DxfShow.outline2.Clear();
                    DxfShow.ElementList2.Clear();
                    DxfShow.OrgPoint =new string[2];
                    DxfShow.ReferencePoint = new string[2];
                    DxfShow.Lines.Clear();
                   dxfparser.transport(of.FileName, ref DataList);
                   string[] data = DataList.ToArray();
                    if(data==null)
                    {
                        MessageBox.Show("解析图形出错!");
                        return;
                    }
                    int m=1;
                    if (DxfEdit.Table1Enable)
                    {
                        cbb_TableSelect.SelectedIndex = 0;
                        DataGrid.Rows.Clear();
                        cmbProgName.Items.Clear();
                    }
                    if (DxfEdit.Table2Enable)
                    {
                        cbb_TableSelect.SelectedIndex = 1;
                        DataGrid2.Rows.Clear();
                        cmbProgName2.Items.Clear();
                    }
                    //List<DxfParser.DxfShow.PointDraw> list = new List<DxfParser.DxfShow.PointDraw>();
                    for(int i=0;i<data.Length;i++)
                    {
                        string[] temp = data[i].Split(',');
                        string sdj,mode,axis;
                        DxfParser.DxfShow.PointDraw pointDraw=new DxfParser.DxfShow.PointDraw();
                        #region//圆解析
                        if (!DxfEdit.LineMode)
                        {
                            if (temp[1] == "20")
                            {
                                DxfParser.DxfShow.OutLine tempOL = new DxfParser.DxfShow.OutLine();
                                if (temp[0] == "CC")
                                {
                                    tempOL.Type = "CC";
                                    tempOL.CenterX = float.Parse(temp[2]);
                                    tempOL.CenterY = float.Parse(temp[3]);
                                    tempOL.R = float.Parse(temp[4]);
                                    tempOL.StartAngle = 0;
                                    tempOL.EndAngle = 360;
                                    tempOL.C = Color.LightBlue;
                                } if (temp[0] == "CL")
                                {
                                    tempOL.Type = "CL";
                                    tempOL.StartX = float.Parse(temp[2]);
                                    tempOL.StartY = float.Parse(temp[3]);
                                    tempOL.EndX = float.Parse(temp[4]);
                                    tempOL.EndY = float.Parse(temp[5]);
                                    tempOL.C = Color.LightBlue;
                                }
                                if (temp[0] == "CA")
                                {
                                    tempOL.Type = "CA";
                                    tempOL.CenterX = float.Parse(temp[2]);
                                    tempOL.CenterY = float.Parse(temp[3]);
                                    tempOL.R = float.Parse(temp[4]);
                                    tempOL.StartAngle = float.Parse(temp[5]);
                                    tempOL.EndAngle = float.Parse(temp[6]);
                                    tempOL.C = Color.LightBlue;
                                }
                                if (DxfEdit.Table1Enable)
                                {
                                    DxfShow.outline.Add(tempOL);
                                }
                                if (DxfEdit.Table2Enable)
                                {
                                    DxfShow.outline2.Add(tempOL);
                                }
                            }
                            else if (temp[0] == "CC")
                            {
                                int layer = int.Parse(temp[1]);
                                if (layer >= 100)
                                {
                                    axis = "B轴";
                                    layer = layer - 100;
                                }
                                else
                                    axis = "A轴";
                                if (layer - 10 >= 0)
                                {
                                    sdj = "2#送钉机";
                                    if (layer - 10 > 0)
                                    {
                                        mode = "避障模式";
                                    }
                                    else
                                        mode = "普通模式";
                                }
                                else
                                {
                                    sdj = "1#送钉机";
                                    if (layer > 0)
                                    {
                                        mode = "避障模式";
                                    }
                                    else
                                        mode = "普通模式";
                                }

                                pointDraw.x = float.Parse(temp[2]);
                                pointDraw.y = float.Parse(temp[3]);
                                pointDraw.r = float.Parse(temp[4]);
                                pointDraw.color = Color.White;
                                pointDraw.name = (m).ToString();
                                if (Math.Abs(pointDraw.r - 1.7) < 0.05)
                                {
                                    if (DxfEdit.Table1Enable)
                                    {
                                        DxfShow.ElementList.Insert(0, pointDraw);
                                        DxfShow.OrgPoint[0] = temp[2];
                                        DxfShow.OrgPoint[1] = temp[3];
                                        DataGrid.Rows.Insert(0, temp[2], temp[3], "0", axis, ScrewType[2], mode);
                                    }
                                    if (DxfEdit.Table2Enable)
                                    {
                                        DxfShow.ElementList2.Insert(0, pointDraw);
                                        DxfShow.ReferencePoint[0] = temp[2];
                                        DxfShow.ReferencePoint[1] = temp[3];
                                        DataGrid2.Rows.Insert(0, temp[2], temp[3], "0", axis, ScrewType[2], mode);
                                    }
                                }
                                else if (Math.Abs(pointDraw.r - 1.8) < 0.05)
                                {
                                    if (DxfEdit.Table1Enable)
                                    {
                                        if (Math.Abs(DxfShow.ElementList[0].r - 1.7) < 0.05)
                                        {
                                            DxfShow.ElementList.Insert(1, pointDraw);
                                            DataGrid.Rows.Insert(1, temp[2], temp[3], "0", axis, ScrewType[2], mode);
                                        }
                                        else
                                        {
                                            DxfShow.ElementList.Insert(0, pointDraw);
                                            DataGrid.Rows.Insert(0, temp[2], temp[3], "0", axis, ScrewType[2], mode);
                                        }
                                    }
                                    if (DxfEdit.Table2Enable)
                                    {
                                        if (Math.Abs(DxfShow.ElementList2[0].r - 1.7) < 0.05)
                                        {
                                            DxfShow.ElementList2.Insert(1, pointDraw);
                                            DataGrid2.Rows.Insert(1, temp[2], temp[3], "0", axis, ScrewType[2], mode);
                                        }
                                        else
                                        {
                                            DxfShow.ElementList2.Insert(0, pointDraw);
                                            DataGrid2.Rows.Insert(0, temp[2], temp[3], "0", axis, ScrewType[2], mode);
                                        }
                                    }
                                }
                                else
                                {
                                    if (DxfEdit.Table1Enable)
                                    {
                                        DxfShow.ElementList.Add(pointDraw);
                                        DataGrid.Rows.Add(temp[2], temp[3], "0", axis, sdj, mode);
                                    }
                                    if (DxfEdit.Table2Enable)
                                    {
                                        DxfShow.ElementList2.Add(pointDraw);
                                        DataGrid2.Rows.Add(temp[2], temp[3], "0", axis, sdj, mode);
                                    }
                                }
                                m++;
                            }
                        }
#endregion//
                        #region//直线解析
                        if (DxfEdit.LineMode)
                        {
                            if (temp[1] == "20")
                            {
                                DxfParser.DxfShow.OutLine tempOL = new DxfParser.DxfShow.OutLine();
                                if (temp[0] == "CC")
                                {
                                    tempOL.Type = "CC";
                                    tempOL.CenterX = float.Parse(temp[2]);
                                    tempOL.CenterY = float.Parse(temp[3]);
                                    tempOL.R = float.Parse(temp[4]);
                                    tempOL.StartAngle = 0;
                                    tempOL.EndAngle = 360;
                                    tempOL.C = Color.LightBlue;
                                } if (temp[0] == "CL")
                                {
                                    tempOL.Type = "CL";
                                    tempOL.StartX = float.Parse(temp[2]);
                                    tempOL.StartY = float.Parse(temp[3]);
                                    tempOL.EndX = float.Parse(temp[4]);
                                    tempOL.EndY = float.Parse(temp[5]);
                                    tempOL.C = Color.LightBlue;
                                }
                                if (temp[0] == "CA")
                                {
                                    tempOL.Type = "CA";
                                    tempOL.CenterX = float.Parse(temp[2]);
                                    tempOL.CenterY = float.Parse(temp[3]);
                                    tempOL.R = float.Parse(temp[4]);
                                    tempOL.StartAngle = float.Parse(temp[5]);
                                    tempOL.EndAngle = float.Parse(temp[6]);
                                    tempOL.C = Color.LightBlue;
                                }
                                if (DxfEdit.Table1Enable)
                                {
                                    DxfShow.outline.Add(tempOL);
                                }
                                if (DxfEdit.Table2Enable)
                                {
                                    DxfShow.outline2.Add(tempOL);
                                }
                            }
                            else if(temp[1] == "30")
                            {
                                if (temp[0] == "CC")
                                {
                                    DxfShow.StartPoint.X=float.Parse(temp[2]);
                                    DxfShow.StartPoint.Y =float.Parse(temp[3]);
                                }
                                DxfParser.DxfShow.OutLine tempOL = new DxfParser.DxfShow.OutLine();
                               
                                 if (temp[0] == "CL")
                                {
                                    tempOL.Type = "CL";
                                    tempOL.StartX = float.Parse(temp[2]);
                                    tempOL.StartY = float.Parse(temp[3]);
                                    tempOL.EndX = float.Parse(temp[4]);
                                    tempOL.EndY = float.Parse(temp[5]);
                                    tempOL.C = Color.LightBlue;
                                    DxfShow.Lines.Add(tempOL);
                                }
                                 
                            }
                            else if (temp[0] == "CC")
                            {
                                int layer = int.Parse(temp[1]);
                                if (layer >= 100)
                                {
                                    pointDraw.axis = "B轴";
                                    layer = layer - 100;
                                }
                                else
                                    pointDraw.axis = "A轴";
                                if (layer - 10 >= 0)
                                {
                                    pointDraw.sdj = "2#送钉机";
                                    if (layer - 10 > 0)
                                    {
                                        pointDraw.mode = "避障模式";
                                    }
                                    else
                                       pointDraw.mode = "普通模式";
                                }
                                else
                                {
                                    pointDraw.sdj = "1#送钉机";
                                    if (layer > 0)
                                    {
                                        pointDraw.mode = "避障模式";
                                    }
                                    else
                                        pointDraw.mode = "普通模式";
                                }

                                pointDraw.x = float.Parse(temp[2]);
                                pointDraw.y = float.Parse(temp[3]);
                                pointDraw.r = float.Parse(temp[4]);
                                pointDraw.color = Color.White;
                                pointDraw.name = (m).ToString();
                                if (Math.Abs(pointDraw.r - 1.7) < 0.05)
                                {
                                    if (DxfEdit.Table1Enable)
                                    {
                                        DxfShow.ElementList.Insert(0, pointDraw);
                                        DxfShow.OrgPoint[0] = temp[2];
                                        DxfShow.OrgPoint[1] = temp[3];
                                       // DataGrid.Rows.Insert(0, temp[2], temp[3], "0", axis, ScrewType[2], mode);
                                    }
                                    if (DxfEdit.Table2Enable)
                                    {
                                        DxfShow.ElementList2.Insert(0, pointDraw);
                                        DxfShow.ReferencePoint[0] = temp[2];
                                        DxfShow.ReferencePoint[1] = temp[3];
                                      //  DataGrid2.Rows.Insert(0, temp[2], temp[3], "0", axis, ScrewType[2], mode);
                                    }
                                }
                                else if (Math.Abs(pointDraw.r - 1.8) < 0.05)
                                {
                                    if (DxfEdit.Table1Enable)
                                    {
                                        if (Math.Abs(DxfShow.ElementList[0].r - 1.7) < 0.05)
                                        {
                                            DxfShow.ElementList.Insert(1, pointDraw);
                                         //   DataGrid.Rows.Insert(1, temp[2], temp[3], "0", axis, ScrewType[2], mode);
                                        }
                                        else
                                        {
                                            DxfShow.ElementList.Insert(0, pointDraw);
                                        //    DataGrid.Rows.Insert(0, temp[2], temp[3], "0", axis, ScrewType[2], mode);
                                        }
                                    }
                                    if (DxfEdit.Table2Enable)
                                    {
                                        if (Math.Abs(DxfShow.ElementList2[0].r - 1.7) < 0.05)
                                        {
                                            DxfShow.ElementList2.Insert(1, pointDraw);
                                           // DataGrid2.Rows.Insert(1, temp[2], temp[3], "0", axis, ScrewType[2], mode);
                                        }
                                        else
                                        {
                                            DxfShow.ElementList2.Insert(0, pointDraw);
                                           // DataGrid2.Rows.Insert(0, temp[2], temp[3], "0", axis, ScrewType[2], mode);
                                        }
                                    }

                                }
                                else
                                {
                                    if (DxfEdit.Table1Enable)
                                    {
                                        DxfShow.ElementList.Add(pointDraw);
                                      //  DataGrid.Rows.Add(temp[2], temp[3], "0", axis, sdj, mode);
                                    }
                                    if (DxfEdit.Table2Enable)
                                    {
                                        DxfShow.ElementList2.Add(pointDraw);
                                      //  DataGrid2.Rows.Add(temp[2], temp[3], "0", axis, sdj, mode);
                                    }
                                }
                                m++;
                            }
                           
                        }
                        
                        #endregion
                    }
                    if (DxfEdit.LineMode)
                    {
                        int startNumber = 0;
                        if (Math.Abs(DxfShow.ElementList[0].r - 1.7) < 0.05 && Math.Abs(DxfShow.ElementList[1].r - 1.8) < 0.05)
                        {
                            startNumber = 2;
                            if (DxfEdit.Table1Enable)
                            {
                                DataGrid.Rows.Insert(0, DxfShow.ElementList[0].x, DxfShow.ElementList[0].y, "0", DxfShow.ElementList[0].axis, ScrewType[2], DxfShow.ElementList[0].mode);
                                DataGrid.Rows.Insert(1, DxfShow.ElementList[1].x, DxfShow.ElementList[1].y, "0", DxfShow.ElementList[1].axis, ScrewType[2], DxfShow.ElementList[1].mode);
                            }
                            if (DxfEdit.Table2Enable)
                            {
                                DataGrid2.Rows.Insert(0, DxfShow.ElementList[0].x, DxfShow.ElementList[0].y, "0", DxfShow.ElementList[0].axis, ScrewType[2], DxfShow.ElementList[0].mode);
                                DataGrid2.Rows.Insert(1, DxfShow.ElementList[1].x, DxfShow.ElementList[1].y, "0", DxfShow.ElementList[1].axis, ScrewType[2], DxfShow.ElementList[1].mode);
                            }
                        }
                        PointF lastPoint = DxfShow.StartPoint;
                        for (int i = startNumber; i < DxfShow.ElementList.Count; i++)
                        {
                            if (Math.Abs(lastPoint.X - DxfShow.ElementList[i].x)< 0.05 & Math.Abs(DxfShow.StartPoint.Y - DxfShow.ElementList[i].y) < 0.05)
                           {
                               if (DxfEdit.Table1Enable)
                               {
                                   DataGrid.Rows.Add(DxfShow.ElementList[i].x, DxfShow.ElementList[i].y, "0", DxfShow.ElementList[i].axis, DxfShow.ElementList[i].sdj, DxfShow.ElementList[i].mode);
                               }
                               if (DxfEdit.Table2Enable)
                               {
                                   DataGrid2.Rows.Add(DxfShow.ElementList2[i].x, DxfShow.ElementList2[i].y, "0", DxfShow.ElementList2[i].axis, DxfShow.ElementList2[i].sdj, DxfShow.ElementList2[i].mode);
                               }
                               break;
                           }
                        }
                            for (int i = 0; i < DxfShow.Lines.Count; i++)
                            {
                                if (Math.Abs(lastPoint.X - DxfShow.Lines[i].StartX) < 0.05 & Math.Abs(lastPoint.Y - DxfShow.Lines[i].StartY) < 0.05)
                                {
                                    string[] tempStr = new string[2];
                                    tempStr[0] = DxfShow.Lines[i].EndX.ToString();
                                    tempStr[1] = DxfShow.Lines[i].EndY.ToString();
                                    lastPoint.X = DxfShow.Lines[i].EndX;
                                    lastPoint.Y = DxfShow.Lines[i].EndY;

                                    for (int j = startNumber; j < DxfShow.ElementList.Count; j++)
                                    {
                                        if (Math.Abs(lastPoint.X - DxfShow.ElementList[j].x) < 0.05 & Math.Abs(lastPoint.Y - DxfShow.ElementList[j].y) < 0.05)
                                        {
                                            if (DxfEdit.Table1Enable)
                                            {
                                                DataGrid.Rows.Add(DxfShow.ElementList[j].x, DxfShow.ElementList[j].y, "0", DxfShow.ElementList[j].axis, DxfShow.ElementList[j].sdj, DxfShow.ElementList[j].mode);
                                            }
                                            if (DxfEdit.Table2Enable)
                                            {
                                                DataGrid2.Rows.Add(DxfShow.ElementList2[j].x, DxfShow.ElementList2[j].y, "0", DxfShow.ElementList2[j].axis, DxfShow.ElementList2[j].sdj, DxfShow.ElementList2[j].mode);
                                            }
                                            DxfShow.Lines.RemoveAt(i);
                                            i=-1;
                                            break;
                                        }
                                    }                                 
                                }
                                else if (Math.Abs(lastPoint.X - DxfShow.Lines[i].EndX) < 0.05 & Math.Abs(lastPoint.Y - DxfShow.Lines[i].EndY) < 0.05)
                                {
                                    string[] tempStr = new string[2];
                                    tempStr[0] = DxfShow.Lines[i].StartX.ToString();
                                    tempStr[1] = DxfShow.Lines[i].StartY.ToString();
                                    lastPoint.X = DxfShow.Lines[i].StartX;
                                    lastPoint.Y = DxfShow.Lines[i].StartY;

                                    for (int j = startNumber; j < DxfShow.ElementList.Count; j++)
                                    {
                                        if (Math.Abs(lastPoint.X - DxfShow.ElementList[j].x) < 0.05 & Math.Abs(lastPoint.Y - DxfShow.ElementList[j].y) < 0.05)
                                        {
                                            if (DxfEdit.Table1Enable)
                                            {
                                                DataGrid.Rows.Add(DxfShow.ElementList[j].x, DxfShow.ElementList[j].y, "0", DxfShow.ElementList[j].axis, DxfShow.ElementList[j].sdj, DxfShow.ElementList[j].mode);
                                            }
                                            if (DxfEdit.Table2Enable)
                                            {
                                                DataGrid2.Rows.Add(DxfShow.ElementList2[j].x, DxfShow.ElementList2[j].y, "0", DxfShow.ElementList2[j].axis, DxfShow.ElementList2[j].sdj, DxfShow.ElementList2[j].mode);
                                            }
                                            DxfShow.Lines.RemoveAt(i);
                                            i=-1;
                                            break;
                                        }
                                    }                                
                                }                              
                            }
                    }
                    //ShowDataList = DxfShow.ElementList.ToArray();
                    if (Parameter.Para[100] != "多产品")
                    {
                        cmbProgName.Items.Clear();
                    }
                    
                    if (DxfEdit.Table1Enable)
                    {
                        cmbProgName.Items.Add(DxfEdit.ProgName);
                        cmbProgName.SelectedItem = DxfEdit.ProgName;
                        txtProdName.Text = cmbProgName.Text;
                        dxfparser.OutlinePic(DxfShow.outline, DxfShow.ElementList);
                        dxfparser.ShowPic(0);
                        UpdateGrid(0,DataGrid);
                        SavePointData(0);
                    }
                    if (DxfEdit.Table2Enable)
                    {
                        cmbProgName2.Items.Add(DxfEdit.ProgName);
                        cmbProgName2.SelectedItem = DxfEdit.ProgName;
                        txtProdName.Text = cmbProgName2.Text;
                        dxfparser.OutlinePic(DxfShow.outline2, DxfShow.ElementList2);
                        dxfparser.ShowPic(1);
                        UpdateGrid(0,DataGrid2);
                        SavePointData(1);
                    }
                                  
                    
                   
                }
            }
        }
        static FileStream FileReset(Stream fStream, string fileName)
        {
            //关闭文件流       
            if (fStream != null)
            {
                fStream.Close();
            }
            //删除文件         
            File.Delete(fileName);
            //新建文件流         
            return new FileStream(fileName, FileMode.OpenOrCreate,
                FileAccess.ReadWrite, FileShare.None);
        }
      

        public void UpdateGrid(short FirstRow,DataGridView dgv)
        {
            short p = 0;
            for (p = 0; p <= dgv.Rows.Count - 1; p++)
            {
                dgv.Rows[p].HeaderCell.Value = (p + 1).ToString();
                dgv.Rows[p].Height = 22;
                if (p %2==0)
                {
                    dgv.Rows[p].DefaultCellStyle.BackColor = Color.FromArgb(200, 255, 255); 
                }
                else
                {
                    dgv.Rows[p].DefaultCellStyle.BackColor = Color.White; 
                }
               if(p==FirstRow)
               {
                   dgv.Rows[p].DefaultCellStyle.BackColor = Color.Yellow; 
               }
            }

        }
        private void btn_Mirror_Click(object sender, EventArgs e)
        {
            dxfparser.Mirror();
        }

        private void btn_R90_Click(object sender, EventArgs e)
        {
            dxfparser.Rotate90();
        }

        private void btn_R180_Click(object sender, EventArgs e)
        {
            dxfparser.Rotate180();
        }

        private void btn_R270_Click(object sender, EventArgs e)
        {
            dxfparser.Rotate270();
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            //dxfparser.Xoff = product_Offset.X;
            //dxfparser.Yoff = product_Offset.Y;
            int n= dxfparser.tabControl1.SelectedIndex;
            dxfparser.ZoomUp();
            dxfparser.ShowPic(n);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int n = dxfparser.tabControl1.SelectedIndex;
            dxfparser.ZoomDown();
            dxfparser.ShowPic(n);
        }
        #endregion
        #region//页面切换
        private void butPage1_Click(object sender, EventArgs e)
        {
            butPage1.BackColor = Color.Green;
            butPage2.BackColor = Color.Green;
            butPage3.BackColor = Color.Green;
            butPage4.BackColor = Color.Green;
            butPage5.BackColor = Color.Green;
            butPage6.BackColor = Color.Green;
            butPage7.BackColor = Color.Green;
            butPage8.BackColor = Color.Green;
            Button temp=(Button)sender;
            int n = int.Parse(temp.Tag.ToString());
            if(n==7)
            {
                if(!PassworkOK)
                {
                    MessageBox.Show("请先输入密码！");
                    return;
                }
            }

            if (n >= 6&(Parameter.Para[0]=="无"|Parameter.Para[0]=="桌面双平台"|Parameter.Para[0]=="桌面单平台"))
            {
                Tab1.SelectedIndex = n-1;
            }
            else
            {
                Tab1.SelectedIndex = n;
            }
            temp.BackColor = Color.Lime;
           
        }

        private void butExit_Click(object sender, EventArgs e)
        {
            for(int i=0;i<lifetimes.Length;i++)
            {
                Parameter.Para[89 + i] = lifetimes[i].ToString();
            }
            Parameter.SavePara(Parameter.SysParaFile);
            ToolRun(false, false);
            visionView.CloseCamera();
            dxfparser.Dispose();
            transport.Dispose();
            visionView.Dispose();
            this.Dispose();
            Application.Exit();
            Environment.Exit(0);
            
        }
        #endregion
        #region//工艺数据页面相关操作
       
        private void butImport_Click(object sender, EventArgs e)
        {
            if(!licenseCheck())
            {
                return;
            }
            int n=cbb_TableSelect.SelectedIndex;
            if (n == -1)
            {
                MessageBox.Show("请选择工作平台");
            }
            if (n == 0)
            {
                bool re = LoadProgFile();
                if (re)
                {
                    UpdateGrid(0,DataGrid);
                    SavePointData(0);
                    dxfparser.OutlinePic(DxfParser.DxfShow.outline, DxfShow.ElementList);
                    dxfparser.ShowPic();
                    labItemTotal.Text = "产品总数:" + cmbProgName.Items.Count;
                    string pathtemp = Parameter.CameraPath + cmbProgName.Text + ".xml";
                    visionView.LoadVisionFile(pathtemp, true);
                    if (Parameter.Para[193].Trim() != "0")
                    {
                        visionView.CoordinateTransform.CameraParam.OffsetX = float.Parse(Parameter.Para[193]);
                        visionView.CoordinateTransform.CameraParam.OffsetY = float.Parse(Parameter.Para[194]);
                    }
                    txtProdName.Text = cmbProgName.Text;
                }
            }
            if (n == 1)
            {
                bool re = LoadProgFile(1);
                if (re)
                {
                    UpdateGrid(0, DataGrid2);
                    SavePointData(1);
                    dxfparser.OutlinePic(DxfParser.DxfShow.outline2, DxfShow.ElementList2);
                    dxfparser.ShowPic(1);
                    labItemTotal.Text = "产品总数:" + cmbProgName.Items.Count;
                    string pathtemp = Parameter.CameraPath + cmbProgName2.Text + ".xml";
                    visionView.LoadVisionFile(pathtemp, true);
                    if (Parameter.Para[193].Trim() != "0")
                    {
                        visionView.CoordinateTransform.CameraParam.OffsetX = float.Parse(Parameter.Para[193]);
                        visionView.CoordinateTransform.CameraParam.OffsetY = float.Parse(Parameter.Para[194]);
                    }
                    txtProdName.Text = cmbProgName2.Text;
                }
            }
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("确定删除改产品?", "提示!", MessageBoxButtons.YesNo))
            {
                return;
            }
            int n = cbb_TableSelect.SelectedIndex;
            string file = "";
            if (n == 0)
            {
                file = Parameter.ProgFilePath + cmbProgName.Text + ".csv";
                cmbProgName.Items.Remove(cmbProgName.Text);
                DataGrid.Rows.Clear();
            }
            if (n == 1)
            {
                file = Parameter.ProgFilePath + cmbProgName2.Text + ".csv";
                cmbProgName2.Items.Remove(cmbProgName2.Text);
                DataGrid2.Rows.Clear();
            }
                
            if (DialogResult.Yes== MessageBox.Show("是否删除产品文件?","提示!" , MessageBoxButtons.YesNo))
            {
                
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
           
        }
        private void butExport_Click(object sender, EventArgs e)
        {
            if (cbb_TableSelect.SelectedIndex == 0)
            {
                int n = DataGrid.RowCount;
                if (n <= 0)
                {
                    MessageBox.Show("没有生产数据!");
                }
                SaveProgFlie(0,cmbProgName.Text,DataGrid,DxfShow.outline);
                SavePointData(0);
            }
            if (cbb_TableSelect.SelectedIndex == 1)
            {
                int n = DataGrid2.RowCount;
                if (n <= 0)
                {
                    MessageBox.Show("没有生产数据!");
                }
                SaveProgFlie(1, cmbProgName2.Text, DataGrid2, DxfShow.outline2);
                SavePointData(1);
            }
            if(Parameter.Para[4]=="启用")
            {
                visionView.SaveVisionFile(Parameter.CameraPath + cmbProgName.Text + ".xml", true);
            }
        }
        private void btnInsertDown_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("正在插入点位,是否继续？", "提示！", MessageBoxButtons.YesNo))
            {


                if (!licenseCheck())
                {
                    return;
                }
                int n = cbb_TableSelect.SelectedIndex;
                if (n < 0)
                    return;
                if (n == 0)
                {
                    int i = DataGrid.CurrentRow.Index;
                    if (i < 0)
                        return;
                    float x = Table.axisStatus[0].CurrentPos;
                    float y = Table.axisStatus[1].CurrentPos;
                    float z = Table.axisStatus[2].CurrentPos;
                    string axis = AxisType[0];
                    string screwtype = ScrewType[0];
                    string workmode = WorkMode[0];
                    DataGrid.Rows.Insert(i, x, y, z, axis, screwtype, workmode);
                }
                if (n == 1)
                {
                    int i = DataGrid2.CurrentRow.Index;
                    if (i < 0)
                        return;
                    float x = Table.axisStatus[0].CurrentPos;
                    float y = Table.axisStatus[3].CurrentPos;
                    float z = Table.axisStatus[2].CurrentPos;
                    string axis = AxisType[0];
                    string screwtype = ScrewType[0];
                    string workmode = WorkMode[0];
                    DataGrid2.Rows.Insert(i, x, y, z, axis, screwtype, workmode);
                }
            }
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("正在删除点位,确定删除？", "提示！", MessageBoxButtons.YesNo))
            {

                int n = cbb_TableSelect.SelectedIndex;
                if (n < 0)
                    return;
                if (n == 0)
                {
                    int i = DataGrid.CurrentRow.Index;
                    if (i < 0)
                        return;
                    DataGrid.Rows.RemoveAt(i);
                }
                if (n == 1)
                {
                    int i = DataGrid2.CurrentRow.Index;
                    if (i < 0)
                        return;
                    DataGrid2.Rows.RemoveAt(i);
                }
            }
        }
        private void SaveProgFlie(int index,string name, DataGridView dgv, List<DxfShow.OutLine> outline,bool showDialog=true)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "EXCEL文件(*.csv)|*.csv";
            DialogResult dr;
            sf.InitialDirectory = Parameter.ProgFilePath;
            string fileName = "";
            sf.FileName = name;
            if (showDialog|lastFile=="")
            {
                dr = sf.ShowDialog();
                if (dr != DialogResult.OK)
                    return;
                butKeyPanle_Click(null, null);
                    if (sf.FileName == "")
                    {
                        return;
                    }
                    fileName = sf.FileName;            
            }
            else
                fileName = lastFile;
            Parameter.Para[250] = LineControlToPLC[4].ToString();
            Parameter.Para[251] = LineControlToPLC[22].ToString();
            Parameter.Para[252] = LineControlToPLC[26].ToString();
            Parameter.Para[253] = LineControlToPLC[28].ToString();
            Parameter.Para[254] = LineControlToPLC[30].ToString();
            Parameter.Para[255] = LineControlToPLC[32].ToString();
            StreamWriter sw = new StreamWriter(fileName);
            lastFile = fileName;
            sw.WriteLine("点位数据Start");
            for (int i = 0; i < dgv.RowCount; i++)
            {
                string str = dgv.Rows[i].Cells[0].Value.ToString();
                for (int j = 1; j < dgv.ColumnCount; j++)
                {
                    if (dgv.Rows[i].Cells[j].Value==null)
                    {
                        dgv.Rows[i].Cells[j].Value = " ";
                    }
                    str += "," + dgv.Rows[i].Cells[j].Value.ToString();
                }
                sw.WriteLine(str);
            }
            sw.WriteLine("点位数据End");
            sw.WriteLine("产品参数Start");
            for (int i = 100; i < Parameter.Para.Length; i++)
            {
                sw.WriteLine("{0}={1}", i.ToString(), Parameter.Para[i]);
            }
            sw.WriteLine("产品参数End");
            sw.WriteLine("产品轮廓Start");
            for (int i = 0; i < outline.Count; i++)
            {
                string str = outline[i].Type + "," + outline[i].CenterX + "," + outline[i].CenterY
                    + "," + outline[i].StartX + "," + outline[i].StartY
                    + "," + outline[i].EndX + "," + outline[i].EndY
                    + "," + outline[i].StartAngle + "," + outline[i].EndAngle
                     + "," + outline[i].R + "," + outline[i].C;
                sw.WriteLine(str);
            }
            sw.WriteLine("产品轮廓End");
            sw.WriteLine("坐标校正Start");
            sw.WriteLine("起点," + startPoint[index].index + ',' + startPoint[index].X + ',' + startPoint[index].Y);
            sw.WriteLine("参考点," + endPoint[index].index + ',' + endPoint[index].X + ',' + endPoint[index].Y);
            sw.WriteLine("坐标校正End");
            // sw.WriteLine(product_Offset.IsSet.ToString());
            //sw.WriteLine(product_Offset.X.ToString());
            //sw.WriteLine(product_Offset.Y.ToString());
            //sw.WriteLine(LineControlToPLC[4].ToString());
            sw.Dispose();
            sw.Close();
            SavePointData(index);
            PointF temporg = new PointF(startPoint[0].X, startPoint[0].Y);
            PointF tempend = new PointF(endPoint[0].X, endPoint[0].Y);
            if (endPoint[0].index < DataGrid.RowCount)
            {
                PointF temppoint = new PointF(float.Parse(DataGrid.Rows[endPoint[0].index].Cells[0].Value.ToString()), float.Parse(DataGrid.Rows[endPoint[0].index].Cells[1].Value.ToString()));
                double off = CoordinateChange.SetProductCoordinate(temporg, temppoint, temporg, tempend);
            }
            AxisParaUpdata();

            MessageBox.Show("产品参数参数已保存!");
        }
        bool oldFile = false;
        private bool LoadProgFile(int tableNum=0)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "EXCEL文件(*.csv)|*.csv|螺钉机文件(*.xscw)|*.xscw|数据文件(*.pdxf)|*.pdxf";
            of.InitialDirectory = Parameter.ProgFilePath;
            of.FileName = "";
            oldFile = false;
            if (of.ShowDialog() == DialogResult.OK)
            {
                string fileTemp = of.SafeFileName.Remove(of.SafeFileName.Length - 4);
                string[]strs = of.SafeFileName.Split('.');
                if (strs[1] == "xscw")
                {
                    fileTemp = strs[0];
                    oldFile = true;
                }
               
                ////if (cmbProgName.Items.Contains(fileTemp) & Parameter.Para[100] == "多产品")
                ////{
                ////    MessageBox.Show("文明名已存在！");
                ////    return false;d
                ////}
                if (tableNum == 0)
                {
                    DxfShow.outline.Clear();
                    DataGrid.Rows.Clear();
                    StreamReader sr = new StreamReader(of.FileName);
                    
                    while (sr.Peek() != -1)
                    {
                        if (oldFile)
                        {
                            #region//刘芳亮老版本工艺文件
                            if (sr.ReadLine().Contains("BEGIN,"))
                            {
                                sr.ReadLine();
                                object[] temp;
                                do
                                {
                                    temp = sr.ReadLine().Split(',');
                                    if ((string)temp[0] != "END")
                                    {
                                        string axis=temp[5]=="0"?"A轴":"B轴";
                                        string sdj="1#送钉机";
                                        if(temp[6]=="1")
                                        {
                                            sdj="2#送钉机";
                                        }else if(temp[6]=="2")
                                        {
                                            sdj="3#送钉机";
                                        }
                                        string mode=temp[8]=="0"?"普通模式":"避障模式";
                                        DataGrid.Rows.Add(temp[1],temp[2],temp[3],axis,sdj,mode);
                                    }
                                }
                                while ((string)temp[0] != "END");
                            }
                            #endregion
                        }
                        else
                        {
                            #region//新版本
                            if (sr.ReadLine() == "点位数据Start")
                            {
                                object[] temp;
                                do
                                {
                                    temp = sr.ReadLine().Split(',');
                                    if ((string)temp[0] != "点位数据End")
                                        DataGrid.Rows.Add(temp);
                                }
                                while ((string)temp[0] != "点位数据End");
                            }
                            if (sr.ReadLine() == "产品参数Start")
                            {
                                object[] temp;
                                do
                                {
                                    temp = sr.ReadLine().Split('=');
                                    if ((string)temp[0] != "产品参数End")
                                        Parameter.Para[int.Parse(temp[0].ToString())] = (string)temp[1];
                                }
                                while ((string)temp[0] != "产品参数End");
                            }

                            if (sr.ReadLine() == "产品轮廓Start")
                            {
                                object[] temp;
                                do
                                {
                                    temp = sr.ReadLine().Split(',');
                                    if ((string)temp[0] != "产品轮廓End")
                                    {
                                        DxfShow.OutLine tempOL = new DxfShow.OutLine();
                                        tempOL.Type = (string)temp[0];
                                        tempOL.CenterX = float.Parse(temp[1].ToString());
                                        tempOL.CenterY = float.Parse(temp[2].ToString());
                                        tempOL.StartX = float.Parse(temp[3].ToString());
                                        tempOL.StartY = float.Parse(temp[4].ToString());
                                        tempOL.EndX = float.Parse(temp[5].ToString());
                                        tempOL.EndY = float.Parse(temp[6].ToString());
                                        tempOL.StartAngle = float.Parse(temp[7].ToString());
                                        tempOL.EndAngle = float.Parse(temp[8].ToString());
                                        tempOL.R = float.Parse(temp[9].ToString());
                                        //if (temp[10].GetType() == Color)
                                        //{
                                        //    tempOL.C = (Color)temp[10];
                                        //}
                                        DxfShow.outline.Add(tempOL);
                                    }
                                }
                                while ((string)temp[0] != "产品轮廓End");
                            }
                            if (sr.ReadLine() == "坐标校正Start")
                            {
                                string[] temp;
                                do
                                {
                                    temp = sr.ReadLine().Split(',');
                                    if (temp[0] != "坐标校正End")
                                    {
                                        if (temp[0] == "起点")
                                        {
                                            startPoint[tableNum].index = int.Parse(temp[1]);
                                            startPoint[tableNum].X = float.Parse(temp[2]);
                                            startPoint[tableNum].Y = float.Parse(temp[3]);
                                        }
                                        if (temp[0] == "参考点")
                                        {
                                            endPoint[tableNum].index = int.Parse(temp[1]);
                                            endPoint[tableNum].X = float.Parse(temp[2]);
                                            endPoint[tableNum].Y = float.Parse(temp[3]);
                                        }
                                    }
                                }
                                while (temp[0] != "坐标校正End");
                            }
                            #endregion
                        }
                          
                        //if (sr.ReadLine() == "true")
                        //    product_Offset.IsSet = true;
                        //else
                        //    product_Offset.IsSet = false;
                        // LineControlToPLC[4] = short.Parse(sr.ReadLine());
                    }

                    transport.Parameters.TargetLineAdjustWidth = short.Parse(Parameter.Para[250]) / 100;
                    transport.Parameters.FlipAxisZFetchHeight = float.Parse(Parameter.Para[251]) / 100;
                    transport.Parameters.FlipAxisXFetchPosition = float.Parse(Parameter.Para[252]) / 100;
                    transport.Parameters.FlipAxisYFetchPosition = float.Parse(Parameter.Para[253]) / 100;
                    transport.Parameters.FlipAxisZPlacePosition = float.Parse(Parameter.Para[254]) / 100;
                    transport.Parameters.FlipAxisYPlacePosition = float.Parse(Parameter.Para[255]) / 100;
                    //LineControlToPLC[4] = short.Parse(Parameter.Para[250]);
                    //LineControlToPLC[22] = short.Parse(Parameter.Para[251]);
                    //LineControlToPLC[26] = short.Parse(Parameter.Para[252]);
                    //LineControlToPLC[28] = short.Parse(Parameter.Para[253]);
                    //LineControlToPLC[30] = short.Parse(Parameter.Para[254]);
                    //LineControlToPLC[32] = short.Parse(Parameter.Para[255]);
                    string[] temp1 = of.SafeFileName.Split('.');
                    temp1[0] = of.SafeFileName.Remove(of.SafeFileName.Length - 4, 4);
                    if (Parameter.Para[100] != "多产品")
                    {
                        cmbProgName.Items.Clear();
                    }
                    if (Parameter.Para[193].Trim() != "0")
                    {
                        visionView.CoordinateTransform.CameraParam.OffsetX = float.Parse(Parameter.Para[193]);
                        visionView.CoordinateTransform.CameraParam.OffsetY = float.Parse(Parameter.Para[194]);
                    }
                    cmbProgName.Items.Add(temp1[0]);
                    cmbProgName.SelectedItem = temp1[0];
                    UpdataParaUI();
                    Ini.IniFile ini = new Ini.IniFile(Parameter.SysParaFile);
                    ini.IniWriteValue("lastfile", "1", of.FileName);
                    PointF temporg = new PointF(startPoint[0].X, startPoint[0].Y);
                    PointF tempend = new PointF(endPoint[0].X, endPoint[0].Y);
                    if (endPoint[0].index < DataGrid.RowCount)
                    {
                        PointF temppoint = new PointF(float.Parse(DataGrid.Rows[endPoint[0].index].Cells[0].Value.ToString()), float.Parse(DataGrid.Rows[endPoint[0].index].Cells[1].Value.ToString()));
                        CoordinateChange.SetProductCoordinate(temporg, temppoint, temporg, tempend);

                    }
                    return true;
                }
                if (tableNum == 1)
                {
                    DxfShow.outline2.Clear();
                    DataGrid2.Rows.Clear();
                    StreamReader sr = new StreamReader(of.FileName);
                    while (sr.Peek() != -1)
                    {
                        if (sr.ReadLine() == "点位数据Start")
                        {
                            object[] temp;
                            do
                            {
                                temp = sr.ReadLine().Split(',');
                                if ((string)temp[0] != "点位数据End")
                                    DataGrid2.Rows.Add(temp);
                            }
                            while ((string)temp[0] != "点位数据End");

                        }
                        if (sr.ReadLine() == "产品参数Start")
                        {
                            object[] temp;
                            do
                            {
                                temp = sr.ReadLine().Split('=');
                                if ((string)temp[0] != "产品参数End")
                                    Parameter.Para[int.Parse(temp[0].ToString())] = (string)temp[1];
                            }
                            while ((string)temp[0] != "产品参数End");
                        }

                        if (sr.ReadLine() == "产品轮廓Start")
                        {
                            object[] temp;
                            do
                            {
                                temp = sr.ReadLine().Split(',');
                                if ((string)temp[0] != "产品轮廓End")
                                {
                                    DxfShow.OutLine tempOL = new DxfShow.OutLine();
                                    tempOL.Type = (string)temp[0];
                                    tempOL.CenterX = float.Parse(temp[1].ToString());
                                    tempOL.CenterY = float.Parse(temp[2].ToString());
                                    tempOL.StartX = float.Parse(temp[3].ToString());
                                    tempOL.StartY = float.Parse(temp[4].ToString());
                                    tempOL.EndX = float.Parse(temp[5].ToString());
                                    tempOL.EndY = float.Parse(temp[6].ToString());
                                    tempOL.StartAngle = float.Parse(temp[7].ToString());
                                    tempOL.EndAngle = float.Parse(temp[8].ToString());
                                    tempOL.R = float.Parse(temp[9].ToString());
                                    //if (temp[10].GetType() == Color)
                                    //{
                                    //    tempOL.C = (Color)temp[10];
                                    //}
                                    DxfShow.outline2.Add(tempOL);
                                }
                            }
                            while ((string)temp[0] != "产品轮廓End");
                        }
                        //if (sr.ReadLine() == "true")
                        //    product_Offset.IsSet = true;
                        //else
                        //    product_Offset.IsSet = false;
                        // LineControlToPLC[4] = short.Parse(sr.ReadLine());
                    }
                    transport.Parameters.TargetLineAdjustWidth = short.Parse(Parameter.Para[250]) / 100;
                    transport.Parameters.FlipAxisZFetchHeight = short.Parse(Parameter.Para[251]) / 100;
                    transport.Parameters.FlipAxisXFetchPosition = short.Parse(Parameter.Para[252]) / 100;
                    transport.Parameters.FlipAxisYFetchPosition = short.Parse(Parameter.Para[253]) / 100;
                    transport.Parameters.FlipAxisZPlacePosition = short.Parse(Parameter.Para[254]) / 100;
                    transport.Parameters.FlipAxisYPlacePosition = short.Parse(Parameter.Para[255]) / 100;
                    //LineControlToPLC[4] = short.Parse(Parameter.Para[250]);
                    //LineControlToPLC[22] = short.Parse(Parameter.Para[251]);
                    //LineControlToPLC[26] = short.Parse(Parameter.Para[252]);
                    //LineControlToPLC[28] = short.Parse(Parameter.Para[253]);
                    //LineControlToPLC[30] = short.Parse(Parameter.Para[254]);
                    //LineControlToPLC[32] = short.Parse(Parameter.Para[255]);
                    string[] temp1 = of.SafeFileName.Split('.');
                    if (Parameter.Para[100] != "多产品")
                    {
                        cmbProgName2.Items.Clear();
                    }
                    if (Parameter.Para[193].Trim() != "0")
                    {
                        visionView.CoordinateTransform.CameraParam.OffsetX = float.Parse(Parameter.Para[193]);
                        visionView.CoordinateTransform.CameraParam.OffsetY = float.Parse(Parameter.Para[194]);
                    }
                    cmbProgName2.Items.Add(temp1[0]);
                    cmbProgName2.SelectedItem = temp1[0];
                    UpdataParaUI();
                    Ini.IniFile ini = new Ini.IniFile(Parameter.SysParaFile);
                    ini.IniWriteValue("lastfile", "2", of.FileName);
                    //PointF temporg = new PointF(startPoint[0].X, startPoint[0].Y);
                    //PointF tempend = new PointF(endPoint[0].X, endPoint[0].Y);
                    //PointF temppoint = new PointF(float.Parse(DataGrid.Rows[n - 1].Cells[0].Value.ToString()), float.Parse(DataGrid.Rows[n - 1].Cells[1].Value.ToString()));
                    //CoordinateChange.SetProductCoordinate(temporg, temppoint, temporg, tempend);
                    return true;
                }
               
            }
            return false;
        }
        private bool LoadProgFile(string path,int tableNum=0)
        {
            if (tableNum == 0)
            {
                

                DxfShow.outline.Clear();
                DataGrid.Rows.Clear();
                StreamReader sr = new StreamReader(path);
                while (sr.Peek() != -1)
                {
                    if (sr.ReadLine() == "点位数据Start")
                    {
                        object[] temp;
                        do
                        {
                            temp = sr.ReadLine().Split(',');
                            if ((string)temp[0] != "点位数据End")
                                DataGrid.Rows.Add(temp);
                        }
                        while ((string)temp[0] != "点位数据End");

                    }
                    if (sr.ReadLine() == "产品参数Start")
                    {
                        object[] temp;
                        do
                        {
                            temp = sr.ReadLine().Split('=');
                            if ((string)temp[0] != "产品参数End")
                                Parameter.Para[int.Parse(temp[0].ToString())] = (string)temp[1];
                        }
                        while ((string)temp[0] != "产品参数End");
                    }

                    if (sr.ReadLine() == "产品轮廓Start")
                    {
                        object[] temp;
                        do
                        {
                            temp = sr.ReadLine().Split(',');
                            if ((string)temp[0] != "产品轮廓End")
                            {
                                DxfShow.OutLine tempOL = new DxfShow.OutLine();
                                tempOL.Type = (string)temp[0];
                                tempOL.CenterX = float.Parse(temp[1].ToString());
                                tempOL.CenterY = float.Parse(temp[2].ToString());
                                tempOL.StartX = float.Parse(temp[3].ToString());
                                tempOL.StartY = float.Parse(temp[4].ToString());
                                tempOL.EndX = float.Parse(temp[5].ToString());
                                tempOL.EndY = float.Parse(temp[6].ToString());
                                tempOL.StartAngle = float.Parse(temp[7].ToString());
                                tempOL.EndAngle = float.Parse(temp[8].ToString());
                                tempOL.R = float.Parse(temp[9].ToString());
                                //if (temp[10].GetType() == Color)
                                //{
                                //    tempOL.C = (Color)temp[10];
                                //}
                                DxfShow.outline.Add(tempOL);
                            }
                        }
                        while ((string)temp[0] != "产品轮廓End");
                    }
                    if (sr.ReadLine() == "坐标校正Start")
                    {
                        string[] temp;
                        do
                        {
                            temp = sr.ReadLine().Split(',');
                            if (temp[0] != "坐标校正End")
                            {
                                if (temp[0] == "起点")
                                {
                                    startPoint[tableNum].index = int.Parse(temp[1]);
                                    startPoint[tableNum].X = float.Parse(temp[2]);
                                    startPoint[tableNum].Y = float.Parse(temp[3]);
                                }
                                if (temp[0] == "参考点")
                                {
                                    endPoint[tableNum].index = int.Parse(temp[1]);
                                    endPoint[tableNum].X = float.Parse(temp[2]);
                                    endPoint[tableNum].Y = float.Parse(temp[3]);
                                }
                            }
                        }
                        while (temp[0] != "坐标校正End");
                    }
                    //if (sr.ReadLine() == "true")
                    //    product_Offset.IsSet = true;
                    //else
                    //    product_Offset.IsSet = false;
                    // LineControlToPLC[4] = short.Parse(sr.ReadLine());
                }

                transport.Parameters.TargetLineAdjustWidth = short.Parse(Parameter.Para[250]) / 100;
                transport.Parameters.FlipAxisZFetchHeight = short.Parse(Parameter.Para[251]) / 100;
                transport.Parameters.FlipAxisXFetchPosition = short.Parse(Parameter.Para[252]) / 100;
                transport.Parameters.FlipAxisYFetchPosition = short.Parse(Parameter.Para[253]) / 100;
                transport.Parameters.FlipAxisZPlacePosition = short.Parse(Parameter.Para[254]) / 100;
                transport.Parameters.FlipAxisYPlacePosition = short.Parse(Parameter.Para[255]) / 100;
                //LineControlToPLC[4] = short.Parse(Parameter.Para[250]);
                //LineControlToPLC[22] = short.Parse(Parameter.Para[251]);
                //LineControlToPLC[26] = short.Parse(Parameter.Para[252]);
                //LineControlToPLC[28] = short.Parse(Parameter.Para[253]);
                //LineControlToPLC[30] = short.Parse(Parameter.Para[254]);
                //LineControlToPLC[32] = short.Parse(Parameter.Para[255]);
                
                
                if (Parameter.Para[193].Trim() != "0")
                {
                    visionView.CoordinateTransform.CameraParam.OffsetX = float.Parse(Parameter.Para[193]);
                    visionView.CoordinateTransform.CameraParam.OffsetY = float.Parse(Parameter.Para[194]);
                }
                //cmbProgName.Items.Add(temp1[0]);
                //cmbProgName.SelectedItem = temp1[0];
                UpdataParaUI();
                
                PointF temporg = new PointF(startPoint[0].X, startPoint[0].Y);
                PointF tempend = new PointF(endPoint[0].X, endPoint[0].Y);
                if (endPoint[0].index < DataGrid.RowCount)
                {
                    PointF temppoint = new PointF(float.Parse(DataGrid.Rows[endPoint[0].index].Cells[0].Value.ToString()), float.Parse(DataGrid.Rows[endPoint[0].index].Cells[1].Value.ToString()));
                    CoordinateChange.SetProductCoordinate(temporg, temppoint, temporg, tempend);
                }
                return true;
            }
            if (tableNum == 1)
            {
                DxfShow.outline2.Clear();
                DataGrid2.Rows.Clear();
                StreamReader sr = new StreamReader(path);
                while (sr.Peek() != -1)
                {
                    if (sr.ReadLine() == "点位数据Start")
                    {
                        object[] temp;
                        do
                        {
                            temp = sr.ReadLine().Split(',');
                            if ((string)temp[0] != "点位数据End")
                                DataGrid2.Rows.Add(temp);
                        }
                        while ((string)temp[0] != "点位数据End");

                    }
                    if (sr.ReadLine() == "产品参数Start")
                    {
                        object[] temp;
                        do
                        {
                            temp = sr.ReadLine().Split('=');
                            if ((string)temp[0] != "产品参数End")
                                Parameter.Para[int.Parse(temp[0].ToString())] = (string)temp[1];
                        }
                        while ((string)temp[0] != "产品参数End");
                    }

                    if (sr.ReadLine() == "产品轮廓Start")
                    {
                        object[] temp;
                        do
                        {
                            temp = sr.ReadLine().Split(',');
                            if ((string)temp[0] != "产品轮廓End")
                            {
                                DxfShow.OutLine tempOL = new DxfShow.OutLine();
                                tempOL.Type = (string)temp[0];
                                tempOL.CenterX = float.Parse(temp[1].ToString());
                                tempOL.CenterY = float.Parse(temp[2].ToString());
                                tempOL.StartX = float.Parse(temp[3].ToString());
                                tempOL.StartY = float.Parse(temp[4].ToString());
                                tempOL.EndX = float.Parse(temp[5].ToString());
                                tempOL.EndY = float.Parse(temp[6].ToString());
                                tempOL.StartAngle = float.Parse(temp[7].ToString());
                                tempOL.EndAngle = float.Parse(temp[8].ToString());
                                tempOL.R = float.Parse(temp[9].ToString());
                                //if (temp[10].GetType() == Color)
                                //{
                                //    tempOL.C = (Color)temp[10];
                                //}
                                DxfShow.outline2.Add(tempOL);
                            }
                        }
                        while ((string)temp[0] != "产品轮廓End");
                    }
                    //if (sr.ReadLine() == "true")
                    //    product_Offset.IsSet = true;
                    //else
                    //    product_Offset.IsSet = false;
                    //product_Offset.X = float.Parse(sr.ReadLine());
                    //product_Offset.Y = float.Parse(sr.ReadLine());
                    // LineControlToPLC[4] = short.Parse(sr.ReadLine());
                }
                LineControlToPLC[4] = short.Parse(Parameter.Para[250]);
                LineControlToPLC[22] = short.Parse(Parameter.Para[251]);
                LineControlToPLC[26] = short.Parse(Parameter.Para[252]);
                LineControlToPLC[28] = short.Parse(Parameter.Para[253]);
                LineControlToPLC[30] = short.Parse(Parameter.Para[254]);
                LineControlToPLC[32] = short.Parse(Parameter.Para[255]);
                if (Parameter.Para[193].Trim() != "0")
                {
                    visionView.CoordinateTransform.CameraParam.OffsetX = float.Parse(Parameter.Para[193]);
                    visionView.CoordinateTransform.CameraParam.OffsetY = float.Parse(Parameter.Para[194]);
                }
                return true;
            }
            return false;
        }

        public void SavePointData(int index)
        {
            if (index == 0)
            {
                Points.points.Clear();
                DxfShow.ElementList.Clear();
                for (int i = 0; i < DataGrid.RowCount; i++)
                {
                    Points.Point tempPoint = new Points.Point();
                    DxfShow.PointDraw tempDraw = new DxfShow.PointDraw();
                    tempPoint.X = float.Parse(DataGrid.Rows[i].Cells[0].Value.ToString());
                    tempPoint.Y = float.Parse(DataGrid.Rows[i].Cells[1].Value.ToString());
                    tempPoint.Z = float.Parse(DataGrid.Rows[i].Cells[2].Value.ToString());
                    if (DataGrid.Rows[i].Cells[3].Value.ToString() == "A轴")
                        tempPoint.Axis = 0;
                    else
                        tempPoint.Axis = 1;
                    if (DataGrid.Rows[i].Cells[4].Value.ToString() == "1#送钉机")
                        tempPoint.ScrewType = 0;
                    else if (DataGrid.Rows[i].Cells[4].Value.ToString() == "2#送钉机")
                        tempPoint.ScrewType = 1;
                    else if (DataGrid.Rows[i].Cells[4].Value.ToString() == "相机")
                        tempPoint.ScrewType = 2;
                    else
                        tempPoint.ScrewType = 0;

                    tempPoint.WorkMode = DataGrid.Rows[i].Cells[5].Value.ToString() == "普通模式" ? 0 : 1;
                    if (DataGrid.Rows[i].Cells[7].Value!=null)
                    {
                        if (DataGrid.Rows[i].Cells[7].Value.ToString()!=" ")
                        tempPoint.floatCheck = float.Parse(DataGrid.Rows[i].Cells[7].Value.ToString());
                    }
                    tempDraw.x = tempPoint.X;
                    tempDraw.y = tempPoint.Y;
                    tempDraw.r = 2;
                    tempDraw.color = Color.White;
                    tempDraw.name = (i + 1).ToString();
                    DxfShow.ElementList.Add(tempDraw);
                    Points.points.Add(tempPoint);
                }
                ElementListOrg.Clear();
                ElementListOrg.AddRange(DxfShow.ElementList);
                
            }
            if(index==1)
            {
                Points.points2.Clear();
                DxfShow.ElementList2.Clear();
                for (int i = 0; i < DataGrid2.RowCount; i++)
                {
                    Points.Point tempPoint = new Points.Point();
                    DxfShow.PointDraw tempDraw = new DxfShow.PointDraw();
                    tempPoint.X = float.Parse(DataGrid2.Rows[i].Cells[0].Value.ToString());
                    tempPoint.Y = float.Parse(DataGrid2.Rows[i].Cells[1].Value.ToString());
                    tempPoint.Z = float.Parse(DataGrid2.Rows[i].Cells[2].Value.ToString());
                    if (DataGrid2.Rows[i].Cells[3].Value.ToString() == "A轴")
                        tempPoint.Axis = 0;
                    else
                        tempPoint.Axis = 1;
                    if (DataGrid2.Rows[i].Cells[4].Value.ToString() == "1#送钉机")
                        tempPoint.ScrewType = 0;
                    else if (DataGrid2.Rows[i].Cells[4].Value.ToString() == "2#送钉机")
                        tempPoint.ScrewType = 1;
                    else if (DataGrid2.Rows[i].Cells[4].Value.ToString() == "相机")
                        tempPoint.ScrewType = 2;
                    else
                        tempPoint.ScrewType = 0;

                    tempPoint.WorkMode = DataGrid2.Rows[i].Cells[5].Value.ToString() == "普通模式" ? 0 : 1;
                    if (DataGrid2.Rows[i].Cells[7].Value != null)
                    {
                        if (DataGrid2.Rows[i].Cells[7].Value.ToString() != " ")
                            tempPoint.floatCheck = float.Parse(DataGrid2.Rows[i].Cells[7].Value.ToString());
                    }
                    tempDraw.x = tempPoint.X;
                    tempDraw.y = tempPoint.Y;
                    tempDraw.r = 2;
                    tempDraw.color = Color.White;
                    tempDraw.name = (i + 1).ToString();
                    DxfShow.ElementList2.Add(tempDraw);
                    Points.points2.Add(tempPoint);
                }
                ElementListOrg2.Clear();
                ElementListOrg2.AddRange(DxfShow.ElementList2);
            }
        }
        public  void LoadPointData(int tableNum=0)
        {
            if (tableNum == 0)
            {
                DataGrid.Rows.Clear();
                for (int i = 0; i < Points.points.Count; i++)
                {
                    DataGrid.Rows.Add(Points.points[i].X, Points.points[i].Y, Points.points[i].Z, AxisType[Points.points[i].Axis], ScrewType[Points.points[i].ScrewType], WorkMode[Points.points[i].WorkMode],"",Points.points[i].floatCheck);

                }

                UpdateGrid(0, DataGrid);
            }
            if (tableNum == 1)
            {
                DataGrid2.Rows.Clear();
                for (int i = 0; i < Points.points2.Count; i++)
                {
                    DataGrid2.Rows.Add(Points.points2[i].X, Points.points2[i].Y, Points.points2[i].Z, AxisType[Points.points2[i].Axis], ScrewType[Points.points2[i].ScrewType], WorkMode[Points.points2[i].WorkMode], "", Points.points2[i].floatCheck);
              
                 }
                UpdateGrid(0, DataGrid2);
            }
        }
   
        private void butDataSet_Click(object sender, EventArgs e)
        {
            int n = cbb_TableSelect.SelectedIndex;
            
            if(n==-1)
            {
                MessageBox.Show("请先选择平台!");
            }
            DGV_Edit.tableIndex = n;
            if (n == 0)
            {
                if (DataGrid.CurrentRow.Index < 0)
                    MessageBox.Show("请先选择行！");
                selectionindex = DataGrid.CurrentRow.Index;
                DGV_Edit edit = new DGV_Edit();
                edit.ShowDialog();
                //edit.Focus();
                LoadPointData(0);               
            }
            if(n==1)
            {
                if (DataGrid2.CurrentRow.Index < 0)
                    MessageBox.Show("请先选择行！");
                selectionindex = DataGrid2.CurrentRow.Index;
                DGV_Edit edit = new DGV_Edit();
                edit.ShowDialog();
                //edit.Focus();
                LoadPointData(1);      
            }
        }

        private void butSetPoint_Click(object sender, EventArgs e)
        {
            int n = cbb_TableSelect.SelectedIndex;
            if (n == 0)
            {
                if (DataGrid.CurrentRow.Index >= 0)
                {
                    startNum = DataGrid.CurrentRow.Index;
                    MessageBox.Show("平台1起点设置成功");
                }
            }
            if (n == 1)
            {
                if (DataGrid2.CurrentRow.Index >= 0)
                {
                    startNum = DataGrid2.CurrentRow.Index;
                    MessageBox.Show("平台2起点设置成功");
                }
            }
        }

        private void butGoHome_Click(object sender, EventArgs e)
        {
            if(spindleRun)
            {
                MessageBox.Show("主轴运行中,请稍后再试!");
                return;
            }
            tab.GotoPoint(0,0, 0, 0, 0.2f);
        }

        private void butGoto_Click(object sender, EventArgs e)
        {
            int tableNum = cbb_TableSelect.SelectedIndex;
            int n=0;
            if(tableNum==0)
            n=DataGrid.CurrentRow.Index;
            if (tableNum == 1)
                n = DataGrid2.CurrentRow.Index;
            if(n<0)
            {
                MessageBox.Show("请先选择点位!");
                return;
            }
            if (spindleRun)
            {
                MessageBox.Show("主轴运行中,请稍后再试!");
                return;
            }
            if (!Table.TableHomeOK)
            {
                MessageBox.Show("设备未回零！");
                return;
            }
            tab.WriteDO(Table.DO.CLD_Dianpi1, false);
            tab.WriteDO(Table.DO.CLD_Dianpi2, false);
            Delay(200);
            float x = 0;
            float y = 0;
            float z = 0;
            int axis = 0;
            int mode=0;

            PointF inPoint = new PointF(Points.points[n].X, Points.points[n].Y);
            PointF outPoint = new PointF();
                
            if (tableNum == 0)
            {
                inPoint = new PointF(Points.points[n].X, Points.points[n].Y);
                if (n < 2)
                {
                   
                    x = inPoint.X;
                    y = inPoint.Y;
                }
                else
                {
                    CoordinateChange.GetProductCoordinate(inPoint, out outPoint);
                    x = outPoint.X;
                    y = outPoint.Y;
                }
                //if()
                 z = Points.points[n].Z;
                 axis = Points.points[n].Axis;
                 mode = Points.points[n].ScrewType;
            }
            if(tableNum==1)
            {
                inPoint = new PointF(Points.points2[n].X, Points.points2[n].Y);
                CoordinateChange.GetProductCoordinate(inPoint, out outPoint);
                x = outPoint.X;
                y = outPoint.Y;
                z = Points.points2[n].Z;
                axis = Points.points2[n].Axis;
                mode = Points.points2[n].ScrewType;
                CoordinateChange.GetProductCoordinate(inPoint, out outPoint);
            }
            
            lbloffX.Text = (outPoint.X - inPoint.X).ToString("f2");
            lbloffY.Text = (outPoint.Y - inPoint.Y).ToString("f2");
            if (axis == 1)
            {
                x += float.Parse(Parameter.Para[83]);
                y += float.Parse(Parameter.Para[84]);
            }
            
            tab.SafetyH = 0;
            if (mode == 2)
            {
                float xoff = float.Parse(Parameter.Para[81]);
                float yoff = float.Parse(Parameter.Para[82]);
                tab.GotoPoint(tableNum,x + xoff, y + yoff, 0, 0.15f);
            }
            else
            { tab.GotoPoint(tableNum,x, y, 0, 0.15f); }
            if(DialogResult.Yes==MessageBox.Show("是否执行Z下降动作？","提示",MessageBoxButtons.YesNo))
            {
                tab.GotoZ(z, 0.3f, 1);
            }
        }
        #endregion
        private void butKeyPanle_Click(object sender, EventArgs e)
        {
            Process[] pProcess = Process.GetProcesses();
            int i;
            for (i = 0; i <= pProcess.Length - 1; i++)
            {
                if (pProcess[i].ProcessName == "osk")
                {
                    pProcess[i].Kill();
                }
            }
            Process pr =Process.Start("osk.exe");
        }

        #region//手动操作
        private void butManuFeedOn_Click(object sender, EventArgs e)
        {
            int screwIndex = cmbManuFeedSel.SelectedIndex;
            if (screwIndex < 0)
                return;
            if (screwIndex == 0)
            {
                if (tab.ReadDo(Table.DO.SDJ_Change) & Parameter.Para[12] != "组合双送钉机")
                {
                    tab.WriteDO(Table.DO.SDJ_Change, false);
                    Delay(int.Parse(Parameter.Para[63]));
                }
                getScrew1 = true;
            }
            if (screwIndex == 1)
            {
                if (!tab.ReadDo(Table.DO.SDJ_Change) & Parameter.Para[12] != "组合双送钉机")
                {
                    tab.WriteDO(Table.DO.SDJ_Change, true);
                    Delay(int.Parse(Parameter.Para[63]));
                }
                getScrew2 = true;
            }
        }

        private void butFeedOpera1_Click(object sender, EventArgs e)
        {
            bool value=false;
            int screwIndex = cmbManuFeedSel.SelectedIndex;
            if (screwIndex < 0)
                return;
            if(screwIndex==0)
            { 
                value=tab.ReadDo(Table.DO.SDJ1_Blow);
            tab.WriteDO(Table.DO.SDJ1_Blow, !value);
            }
            if (screwIndex == 1)
            {
                value = tab.ReadDo(Table.DO.SDJ2_Blow);
                tab.WriteDO(Table.DO.SDJ2_Blow, !value);
            }
                
        }
        private void button30_Click(object sender, EventArgs e)
        {
            bool value = false;
            int screwIndex = cmbManuFeedSel.SelectedIndex;
            if (screwIndex < 0)
                return;
            if (screwIndex == 0)
            {
                value = tab.ReadDo(Table.DO.SDJ1_BlowStop);
                tab.WriteDO(Table.DO.SDJ1_BlowStop, !value);
            }
            //if (screwIndex == 1)
            //{
            //    value = tab.ReadDo(Table.DO.SDJ2_BlowStop);
            //    tab.WriteDO(Table.DO.SDJ2_BlowStop, !value);
            //}
        }

        private void butFeedOpera2_Click(object sender, EventArgs e)
        {
            bool value = false;
            int screwIndex = cmbManuFeedSel.SelectedIndex;
            if (screwIndex < 0)
                return;
            if (screwIndex == 0)
            {
                value = tab.ReadDo(Table.DO.SDJ1_Gouding);
                tab.WriteDO(Table.DO.SDJ1_Gouding, !value);
            }
            if (screwIndex == 1)
            {
                value = tab.ReadDo(Table.DO.SDJ2_Gouding);
                tab.WriteDO(Table.DO.SDJ2_Gouding, !value);
            }
        }

        private void butFeedOpera3_Click(object sender, EventArgs e)
        {
            bool value = false;
            int screwIndex = cmbManuFeedSel.SelectedIndex;
            if (screwIndex < 0)
                return;
            if (screwIndex == 0)
            {
                value = tab.ReadDo(ThreeAxisTable.Table.DO.SDJ1_Badong);
                tab.WriteDO(ThreeAxisTable.Table.DO.SDJ1_Badong, !value);
                tab.WriteDO(Table.DO.SDJ1_GuntongF, false);
            }
            if (screwIndex == 1)
            {
                value = tab.ReadDo(ThreeAxisTable.Table.DO.SDJ2_Badong);
                tab.WriteDO(ThreeAxisTable.Table.DO.SDJ2_Badong, !value);
                tab.WriteDO(Table.DO.SDJ2_GuntongF, false);
            }
        }

        private void butFeedOpera4_Click(object sender, EventArgs e)
        {
            bool value = false;
            int screwIndex = cmbManuFeedSel.SelectedIndex;
            if (screwIndex < 0)
                return;
            if (screwIndex == 0)
            {
                value = tab.ReadDo(ThreeAxisTable.Table.DO.SDJ1_GuntongF);
                tab.WriteDO(ThreeAxisTable.Table.DO.SDJ1_Badong, !value);
                tab.WriteDO(Table.DO.SDJ1_GuntongF, !value);
            }
            if (screwIndex == 1)
            {
                value = tab.ReadDo(ThreeAxisTable.Table.DO.SDJ2_GuntongF);
                tab.WriteDO(ThreeAxisTable.Table.DO.SDJ2_Badong, !value);
                tab.WriteDO(Table.DO.SDJ2_GuntongF, !value);
            }
        }

        private void butVacuumOnOff_Click(object sender, EventArgs e)
        {
            bool value = false;
            int screwIndex = cmbManuToolSel.SelectedIndex;
            if (screwIndex < 0)
                return;

            if (screwIndex == 0)
            {
                value = tab.ReadDo(ThreeAxisTable.Table.DO.CLD_vacuo);
                tab.WriteDO(ThreeAxisTable.Table.DO.CLD_vacuo, !value);
            }
            if (screwIndex == 1)
            {
                value = tab.ReadDo(ThreeAxisTable.Table.DO.CLD_vacuo2);
                tab.WriteDO(ThreeAxisTable.Table.DO.CLD_vacuo2, !value);
            }
        }

        private void butToolBlowAir_Click(object sender, EventArgs e)
        {
            bool value = false;
            int screwIndex = cmbManuToolSel.SelectedIndex;
            if (screwIndex < 0)
                return;

            if (screwIndex == 0)
            {
                value = tab.ReadDo(ThreeAxisTable.Table.DO.CLD_Blow1);
                tab.WriteDO(ThreeAxisTable.Table.DO.CLD_Blow1, !value);
            }
            if (screwIndex == 1)
            {
                value = tab.ReadDo(ThreeAxisTable.Table.DO.CLD_Blow2);
                tab.WriteDO(ThreeAxisTable.Table.DO.CLD_Blow2, !value);
            }
        }

        private void butToolClear_Click(object sender, EventArgs e)
        {
            if (cmbManuToolSel.SelectedIndex == 1)
                ToolClear(1);
            else
                ToolClear(0);
        }

        private void butToolReset_Click(object sender, EventArgs e)
        {
            ToolReset();
        }

        private void butToolMode1_Click(object sender, EventArgs e)
        {
            bool value = false;
            int screwIndex = cmbManuToolSel.SelectedIndex;
            if (screwIndex < 0)
                return;

            if (screwIndex == 0)
            {
                value = tab.ReadDo(ThreeAxisTable.Table.DO.CLD_Bizhang1);
                tab.WriteDO(ThreeAxisTable.Table.DO.CLD_Bizhang1, !value);
            }
            if (screwIndex == 1)
            {
                value = tab.ReadDo(ThreeAxisTable.Table.DO.CLD_Bizhang2);
                tab.WriteDO(ThreeAxisTable.Table.DO.CLD_Bizhang2, !value);
            }
        }

        private void grpToolManu_Enter(object sender, EventArgs e)
        {

        }

        private void butPushScrewA_Click(object sender, EventArgs e)
        {
            butPushScrewA.Enabled = false;
            bool value = false;
            int screwIndex = cmbManuToolSel.SelectedIndex;
            if (screwIndex < 0)
                return;

            if (screwIndex == 0)
            {
                if(tab.ReadDo(Table.DO.BaxisEnable))
                {
                    MessageBox.Show("B轴使能中，禁止切换A轴推钉");
                    butPushScrewA.Enabled = true;
                    return;
                }
                value = tab.ReadDo(ThreeAxisTable.Table.DO.CLD_PushA1);
                tab.WriteDO(ThreeAxisTable.Table.DO.CLD_PushA1, !value);
                tab.WriteDO(ThreeAxisTable.Table.DO.CLD_PushA2, value);

                if (tab.ReadDo(Table.DO.CLD_PushA1))
                {
                    int temp = timeGetTime() + 800;
                    do
                    {
                        Thread.Sleep(50);
                    }
                    while (temp >= timeGetTime() & !tab.ReadDi(Table.DI.CLD_TuidingEND));
                    if (!tab.ReadDi(Table.DI.CLD_TuidingEND))
                    {
                        almdata[0] = dataChange.SetBitValue(almdata[0], 5, true);//推钉气缸动点异常
                    }
                    else
                    {
                        getScrew1 = false;
                        getScrew2 = false;
                        songding1Step = 0;
                    }
                }
                else
                {
                    int temp = timeGetTime() + 800;
                    do
                    {
                        Thread.Sleep(50);
                    }
                    while (temp >= timeGetTime() & !tab.ReadDi(Table.DI.CLD_TuidingORG));
                    if (!tab.ReadDi(Table.DI.CLD_TuidingORG))
                    {
                       almdata[0] = dataChange.SetBitValue(almdata[0],6,true);//推钉气缸原点异常
                    }
                    else
                    {
                        getScrew1 = false;
                        getScrew2 = false;
                        songding1Step = 0;
                    }
                }
            }
            if (screwIndex == 1)
            {
                if (!tab.ReadDo(Table.DO.BaxisEnable))
                {
                    MessageBox.Show("A轴使能中，禁止切换b轴推钉");
                    butPushScrewA.Enabled = true;
                    return;
                }
                value = tab.ReadDo(ThreeAxisTable.Table.DO.CLD_PushB1);
                tab.WriteDO(ThreeAxisTable.Table.DO.CLD_PushB1, !value);
                tab.WriteDO(ThreeAxisTable.Table.DO.CLD_PushB2, value);

                if (tab.ReadDo(Table.DO.CLD_PushB1))
                {
                    int temp = timeGetTime() + 1200;
                    do
                    {
                        Thread.Sleep(50);
                    }
                    while (temp >= timeGetTime() & !tab.ReadDi(Table.DI.CLD_Tuiding2END));
                    if (!tab.ReadDi(Table.DI.CLD_Tuiding2END))
                    {
                        almdata[1]=dataChange.SetBitValue(almdata[1],2,true);//推钉气缸动点异常
                    }

                }
                else
                {
                    int temp = timeGetTime() + 800;
                    do
                    {
                        Thread.Sleep(50);
                    }
                    while (temp >= timeGetTime() & !tab.ReadDi(Table.DI.CLD_Tuiding2ORG));
                    if (!tab.ReadDi(Table.DI.CLD_Tuiding2ORG))
                    {
                        almdata[1] = dataChange.SetBitValue(almdata[1], 3, true);//推钉气缸原点异常
                    }

                }
            }
            
            
            butPushScrewA.Enabled = true;
           
        }

        private void butToolUpDn_Click(object sender, EventArgs e)
        {
            bool value = false;
            int screwIndex = cmbManuToolSel.SelectedIndex;
            if (screwIndex < 0)
                return;
            if (screwIndex == 0)
            {
                value = tab.ReadDo(ThreeAxisTable.Table.DO.CLD_Dianpi1);
                tab.WriteDO(ThreeAxisTable.Table.DO.CLD_Dianpi1, !value);
              
            }
            if (screwIndex == 1)
            {
                value = tab.ReadDo(ThreeAxisTable.Table.DO.CLD_Dianpi2);
                tab.WriteDO(ThreeAxisTable.Table.DO.CLD_Dianpi2, !value);
            }
        }

        private void butManuTesting_Click(object sender, EventArgs e)
        {
            GC.Collect();
            int screwIndex = cmbManuToolSel.SelectedIndex;
            if (screwIndex < 0)
                return;
            if (chkTest.Checked)
            {
                if (screwIndex == 0)
                {
                    AxisParaUpdata(2);
                    UnlockScrew(0);
                    ToolRun(false, false);
                }
                if (screwIndex == 1)
                {
                    AxisParaUpdata(2);
                    UnlockScrew(0);
                    ToolRun(false, false);
                }
            }
            else
            {
                if (screwIndex == 0)
                {
                    AxisParaUpdata(0);
                    //Delay(100);
                    LockScrew();
                    ToolRun(false, false);
                }
                if (screwIndex == 1)
                {
                    AxisParaUpdata(1);
                    //Delay(100);
                    LockScrew(1);
                    ToolRun(false, false);
                }
            }
        }

        private void butUndoSilo_Click(object sender, EventArgs e)//拆钉伸缩
        {
            bool value = false;
            value = tab.ReadDo(ThreeAxisTable.Table.DO.CaremaLift);
            tab.WriteDO(ThreeAxisTable.Table.DO.CaremaLift, !value);
        }

        private void butFwdRotMA_MouseDown(object sender, MouseEventArgs e)
        {
            int n=cmbManuToolSel.SelectedIndex;
            AxisParaUpdata(n);
            if(ckbTriJog.Checked)
            {
                ToolRun(true, true);
            }else
            ToolRun(false, true);
        }

        private void butFwdRotMA_MouseUp(object sender, MouseEventArgs e)
        {
            ToolRun(false, false);
        }

        private void butRevRotMA_Click(object sender, EventArgs e)
        {

        }
#endregion
        #region//送钉机相关
        public struct SDJpara
        {
            public int GouDingcount;
            public int GouDingdelay;
            public int QueDingCount;
            public int LuoDingCheckDelay;
            public int GouDingBackDelay;
            public int BlowTime;
            public int GouDingCurrentCount;
            public int QueDingCurrentCount;
            public bool LengthCheckMode;
            public bool ErrorFlag;
            public int TuidingDelay;
        }
        public SDJpara[] sdjpara =new SDJpara[2];
        int songding1Step = 0;
        int pushCount = 0;
        int[] baidong1Delay = new int[2];
        int[] songding1Delay = new int[5];
        int[] LockScrewDelay = new int[5];
        bool screwTypeError = false,clearFlag=false;
        private void GetSDJpara(int index)
        {        
            if(index==0)
            {
                sdjpara[0].GouDingcount = int.Parse(Parameter.Para[65]);
                sdjpara[0].GouDingdelay = int.Parse(Parameter.Para[66]);
                sdjpara[0].QueDingCount = int.Parse(Parameter.Para[67]);
                sdjpara[0].LuoDingCheckDelay = int.Parse(Parameter.Para[68]);
                sdjpara[0].GouDingBackDelay=int.Parse(Parameter.Para[69]);
                sdjpara[0].BlowTime = int.Parse(Parameter.Para[70]);
                sdjpara[0].LengthCheckMode = int.Parse(Parameter.Para[195]) == 1 ? true : false;
                sdjpara[0].TuidingDelay = int.Parse(Parameter.Para[162]);

            }
            if(index==1)
            {
                sdjpara[1].GouDingcount = int.Parse(Parameter.Para[73]);
                sdjpara[1].GouDingdelay = int.Parse(Parameter.Para[74]);
                sdjpara[1].QueDingCount = int.Parse(Parameter.Para[75]);
                sdjpara[1].LuoDingCheckDelay = int.Parse(Parameter.Para[76]);
                sdjpara[1].GouDingBackDelay = int.Parse(Parameter.Para[77]);
                sdjpara[1].BlowTime = int.Parse(Parameter.Para[78]);
                sdjpara[1].LengthCheckMode = int.Parse(Parameter.Para[196]) == 1 ? true : false;
                sdjpara[1].TuidingDelay = int.Parse(Parameter.Para[188]);
            }
            pushCount = int.Parse(Parameter.Para[62]);
            if(pushCount<0|pushCount>5)
            {
                pushCount = 2;
            }
        }
        private bool GetScrew(int index)
        {
                        sdjpara[0].GouDingCurrentCount = 0;
                        sdjpara[0].QueDingCurrentCount = 0;
                        GetSDJpara(index);
                        Table.DI luoliaoCheck =Table.DI.SDJ1_LL;
                        //Table.DI typeCheck = Table.DI.SDJ1_Type;
                        ushort luoliao = 250;
                        if (index == 1)
                        {
                           // typeCheck = Table.DI.SDJ2_Type;
                            sdjpara[0] = sdjpara[1];
                        }
                     mark:
                        if (index == 0)
                        {

                            if (Parameter.Para[12] == "组合双送钉机")
                            {                               
                                tab.WriteDO(Table.DO.SDJ2_Gouding, false);                              
                            }
                            //if (sdjpara[0].ErrorFlag)
                            //{
                            //    sdjpara[0].ErrorFlag = false;
                            //    screwTypeError = true;
                            //}
                            tab.WriteDO(Table.DO.SDJ1_Gouding, true);                            
                            luoliaoCheck = Table.DI.SDJ1_LL;
                        }else 
                        {
                            //if (sdjpara[1].ErrorFlag)
                            //{
                            //    sdjpara[1].ErrorFlag = false;
                            //    screwTypeError = true;
                            //}
                            if (Parameter.Para[12] == "双送钉机"|Parameter.Para[3]=="双")
                            {
                                luoliaoCheck = Table.DI.SDJ2_LL;

                                luoliao = 252;
                            }
                            tab.WriteModBus_int(luoliao,1,new int[]{0});
                            if (Parameter.Para[12] == "组合双送钉机")
                            {
                                tab.WriteDO(Table.DO.SDJ1_Gouding, false);
                            }
                            tab.WriteDO(Table.DO.SDJ2_Gouding, true);
                            
                        }
                        #region//参数检测
                        if (sdjpara[0].GouDingdelay <= 100 | sdjpara[0].GouDingdelay >= 2000)
                        { sdjpara[0].GouDingdelay = 500; }
                        if (sdjpara[0].GouDingcount < 1)
                            sdjpara[0].GouDingcount = 3;
                        if (sdjpara[0].LuoDingCheckDelay <= 0 | sdjpara[0].LuoDingCheckDelay > 2000)
                        {
                            sdjpara[0].LuoDingCheckDelay = 1000;
                        }
                        if (sdjpara[0].QueDingCount <= 0 | sdjpara[0].QueDingCount > 10)
                        {
                            sdjpara[0].QueDingCount = 5;
                        }
                        #endregion
                        songding1Delay[0] = timeGetTime() + sdjpara[0].GouDingdelay;
                        songding1Delay[1] = timeGetTime() + sdjpara[0].LuoDingCheckDelay;
                        int temp = 0;

                        while (!tab.ReadDi(luoliaoCheck) & temp <= songding1Delay[1])
                        {
                            if (Parameter.Para[0] == "桌面双平台")
                            {
                                if (tab.ReadModBus_int(luoliao) == 1)
                                {
                                    break;
                                }
                            }
                            temp = timeGetTime();
                        }
                        if (temp > songding1Delay[1])
                        {
                            if (Parameter.Para[12] == "组合双送钉机")
                            {
                                #region//组合送钉机
                                if (index == 0)
                                {
                                    while (!tab.ReadDi(Table.DI.SDJ1_GD_ORG) && timeGetTime() <= songding1Delay[0])
                                    {
                                        Thread.Sleep(50);
                                    }
                                    if (!tab.ReadDi(Table.DI.SDJ1_GD_ORG))
                                    {
                                        sdjpara[0].GouDingCurrentCount++;
                                    }
                                    else
                                    {
                                        sdjpara[0].QueDingCurrentCount++;
                                    }
                                    if (sdjpara[0].QueDingCurrentCount >= sdjpara[0].QueDingCount)
                                    {
                                        almdata[0] = dataChange.SetBitValue(almdata[0], 0, true);//送钉机1缺钉报警
                                    }
                                    if (sdjpara[0].GouDingCurrentCount >= sdjpara[0].GouDingcount)
                                    {
                                        almdata[0] = dataChange.SetBitValue(almdata[0], 1, true);//送钉机1卡钉报警
                                    }
                                    if (!dataChange.GetBitValue(almdata[0],0) && !dataChange.GetBitValue(almdata[0],1))
                                    {
                                        tab.WriteDO(Table.DO.SDJ1_Gouding, false);
                                        Thread.Sleep(1000);
                                        goto mark;
                                    }
                                }
                                else
                                {
                                    while (!tab.ReadDi(Table.DI.SDJ2_GD_ORG) && timeGetTime() <= songding1Delay[0])
                                    {
                                        Thread.Sleep(50);
                                    }
                                    if (!tab.ReadDi(Table.DI.SDJ2_GD_ORG))
                                    {
                                        sdjpara[0].GouDingCurrentCount++;
                                    }
                                    else
                                    {
                                        sdjpara[0].QueDingCurrentCount++;
                                    }
                                    if (sdjpara[0].QueDingCurrentCount >= sdjpara[0].QueDingCount)
                                    {
                                        almdata[0] = dataChange.SetBitValue(almdata[0], 2, true);//送钉机2缺钉报警
                                    }
                                    if (sdjpara[0].GouDingCurrentCount >= sdjpara[0].GouDingcount)
                                    {
                                        almdata[0] = dataChange.SetBitValue(almdata[0], 3, true);//送钉机2卡钉报警
                                    }
                                    if (!dataChange.GetBitValue(almdata[0],2) && !dataChange.GetBitValue(almdata[0],3))
                                    {
                                        tab.WriteDO(Table.DO.SDJ2_Gouding, false);
                                        Thread.Sleep(1000);
                                        goto mark;
                                    }
                                }
                                #endregion
                            }else
                            {
                                #region\\双送钉机
                                if (index == 0)
                                {
                                    tab.WriteDO(Table.DO.SDJ1_Gouding, false);
                                    songding1Delay[2] = timeGetTime() + sdjpara[0].GouDingBackDelay;
                                    while (!tab.ReadDi(Table.DI.SDJ1_GD_ORG) && timeGetTime() <= songding1Delay[2])
                                    {
                                        Thread.Sleep(50);
                                    }
                                    if (!tab.ReadDi(Table.DI.SDJ1_GD_ORG))
                                    {
                                        sdjpara[0].GouDingCurrentCount++;
                                    }
                                    else
                                    {
                                        sdjpara[0].QueDingCurrentCount++;
                                    }
                                    if (sdjpara[0].QueDingCurrentCount >= sdjpara[0].QueDingCount)
                                    {
                                        almdata[0] = dataChange.SetBitValue(almdata[0], 0, true);//送钉机1缺钉报警
                                    }
                                    if (sdjpara[0].GouDingCurrentCount >= sdjpara[0].GouDingcount)
                                    {
                                        almdata[0] = dataChange.SetBitValue(almdata[0], 1, true);//送钉机1卡钉报警
                                    }
                                    if (!dataChange.GetBitValue(almdata[0],0) && !dataChange.GetBitValue(almdata[0],1))
                                    {
                                        goto mark;
                                    }
                                }
                                else
                                {
                                    tab.WriteDO(Table.DO.SDJ2_Gouding, false);
                                    Delay(500);
                                    songding1Delay[2] = timeGetTime() + sdjpara[0].GouDingBackDelay;
                                    while (!tab.ReadDi(Table.DI.SDJ2_GD_ORG) && timeGetTime() <= songding1Delay[2])
                                    {
                                        Thread.Sleep(50);
                                    }
                                    if (!tab.ReadDi(Table.DI.SDJ2_GD_ORG))
                                    {
                                        sdjpara[0].GouDingCurrentCount++;
                                    }
                                    else
                                    {
                                        sdjpara[0].QueDingCurrentCount++;
                                    }
                                    if (sdjpara[0].QueDingCurrentCount >= sdjpara[0].QueDingCount)
                                    {
                                        almdata[0] = dataChange.SetBitValue(almdata[0], 2, true);//送钉机2缺钉报警
                                    }
                                    if (sdjpara[0].GouDingCurrentCount >= sdjpara[0].GouDingcount)
                                    {
                                        almdata[0] = dataChange.SetBitValue(almdata[0], 3, true);//送钉机2卡钉报警
                                    }
                                    if (!dataChange.GetBitValue(almdata[0], 0) && !dataChange.GetBitValue(almdata[0], 1))
                                    {
                                        goto mark;
                                    }
                                }
                                #endregion
                            }                          
                            return false;
                        }else
                        {
                            if(Parameter.Para[12]!="组合双送钉机")
                            {
                                Table.DI goudingBackOK = Table.DI.SDJ1_GD_ORG;
                                if (index == 0)
                                {
                                    tab.WriteDO(Table.DO.SDJ1_Gouding, false);
                                    
                                }
                                if (index == 1)
                                {
                                    tab.WriteDO(Table.DO.SDJ2_Gouding, false);
                                    goudingBackOK = Table.DI.SDJ2_GD_ORG;
                                }
                                songding1Delay[3]=timeGetTime()+sdjpara[0].GouDingBackDelay;
                                while (!tab.ReadDi(goudingBackOK))
                                {
                                    Thread.Sleep(50);
                                   if(timeGetTime()>=songding1Delay[3])
                                   {
                                      // ALM.AlmBoolList[]
                                       break;
                                   }
                                }                               
                            }
                        }
                        return true;                      
        }
        private bool GetScrew2(int index)
        {
            sdjpara[0].GouDingCurrentCount = 0;
            sdjpara[0].QueDingCurrentCount = 0;
            GetSDJpara(index);
            Table.DI luoliaoCheck = Table.DI.SDJ1_LL;
            ushort luoliao = 250;
            if (index == 1)
            {
                // typeCheck = Table.DI.SDJ2_Type;
                sdjpara[0] = sdjpara[1];
            }
        mark:
            if (index == 0)
            {
                tab.WriteDO(Table.DO.SDJ2_Gouding, false);
                tab.WriteDO(Table.DO.SDJ1_Gouding, true);
                luoliaoCheck = Table.DI.SDJ1_LL;
                tab.WriteModBus_int(luoliao, 1, new int[] { 0 });
            }
            else
            {
                luoliaoCheck = Table.DI.SDJ1_LL;
                luoliao = 250;
                tab.WriteModBus_int(luoliao, 1, new int[] { 0 });
                tab.WriteDO(Table.DO.SDJ1_Gouding, false);
                tab.WriteDO(Table.DO.SDJ2_Gouding, true);
            }
            #region//参数检测
            if (sdjpara[0].GouDingdelay <= 100 | sdjpara[0].GouDingdelay >= 2000)
            { sdjpara[0].GouDingdelay = 500; }
            if (sdjpara[0].GouDingcount < 1)
                sdjpara[0].GouDingcount = 3;
            if (sdjpara[0].LuoDingCheckDelay <= 0 | sdjpara[0].LuoDingCheckDelay > 2000)
            {
                sdjpara[0].LuoDingCheckDelay = 1000;
            }
            if (sdjpara[0].QueDingCount <= 0 | sdjpara[0].QueDingCount > 10)
            {
                sdjpara[0].QueDingCount = 5;
            }
            #endregion
            songding1Delay[0] = timeGetTime() + sdjpara[0].GouDingdelay;
            songding1Delay[1] = timeGetTime() + sdjpara[0].LuoDingCheckDelay;
            int temp = 0;

            while (!tab.ReadDi(luoliaoCheck) & temp <= songding1Delay[1])
            {
              
                    if (tab.ReadModBus_int(luoliao) == 1)
                    {
                        break;
                    }      
                temp = timeGetTime();
            }
            if (temp > songding1Delay[1])
            {
                #region\\震动盘组合送钉机
                if (index == 0)
                {
                    tab.WriteDO(Table.DO.SDJ1_Gouding, false);
                    songding1Delay[2] = timeGetTime() + sdjpara[0].GouDingBackDelay;
                    while (!tab.ReadDi(Table.DI.SDJ1_GD_ORG) || timeGetTime() <= songding1Delay[2])
                    {
                        Thread.Sleep(50);
                    }
                    if (!tab.ReadDi(Table.DI.SDJ1_GD_ORG))
                    {
                        sdjpara[0].GouDingCurrentCount++;
                    }
                    else
                    {
                        sdjpara[0].QueDingCurrentCount++;
                    }
                    if (sdjpara[0].QueDingCurrentCount >= sdjpara[0].QueDingCount)
                    {
                        almdata[0] = dataChange.SetBitValue(almdata[0], 0, true);//送钉机1缺钉报警
                    }
                    if (sdjpara[0].GouDingCurrentCount >= sdjpara[0].GouDingcount)
                    {
                        almdata[0] = dataChange.SetBitValue(almdata[0], 1, true);//送钉机1卡钉报警
                    }
                    if (!dataChange.GetBitValue(almdata[0], 0) && !dataChange.GetBitValue(almdata[0], 1))
                    {
                        goto mark;
                    }
                }
                else
                {
                    tab.WriteDO(Table.DO.SDJ2_Gouding, false);
                    Delay(500);
                    songding1Delay[2] = timeGetTime() + sdjpara[0].GouDingBackDelay;
                    while (!tab.ReadDi(Table.DI.SDJ2_GD_ORG) || timeGetTime() <= songding1Delay[2])
                    {
                        Thread.Sleep(50);
                    }
                    if (!tab.ReadDi(Table.DI.SDJ2_GD_ORG))
                    {
                        sdjpara[0].GouDingCurrentCount++;
                    }
                    else
                    {
                        sdjpara[0].QueDingCurrentCount++;
                    }
                    if (sdjpara[0].QueDingCurrentCount >= sdjpara[0].QueDingCount)
                    {
                        almdata[0] = dataChange.SetBitValue(almdata[0], 2, true);//送钉机2缺钉报警
                    }
                    if (sdjpara[0].GouDingCurrentCount >= sdjpara[0].GouDingcount)
                    {
                        almdata[0] = dataChange.SetBitValue(almdata[0], 3, true);//送钉机2卡钉报警
                    }
                    if (!dataChange.GetBitValue(almdata[0], 0) && !dataChange.GetBitValue(almdata[0], 1))
                    {
                        goto mark;
                    }
                }
                #endregion
                return false;
            }
            else
            {
                Thread.Sleep(200);
                Table.DI goudingBackOK = Table.DI.SDJ1_GD_ORG;
                if (index == 0)
                {
                    tab.WriteDO(Table.DO.SDJ1_Gouding, false);

                }
                if (index == 1)
                {
                    tab.WriteDO(Table.DO.SDJ2_Gouding, false);
                    goudingBackOK = Table.DI.SDJ2_GD_ORG;
                }
                songding1Delay[3] = timeGetTime() + sdjpara[0].GouDingBackDelay;
                while (!tab.ReadDi(goudingBackOK))
                {
                    Thread.Sleep(50);
                    if (timeGetTime() >= songding1Delay[3])
                    {
                        break;
                    }
                }

            }
            return true;
        }
        private void BlowScrew(int index)
        {
            if (sdjpara[index].BlowTime <= 0 | sdjpara[index].BlowTime > 1000)
            {
                sdjpara[index].BlowTime = 400;
            }
          //  tab.WriteDO(Table.DO.dustCollecting, true);
            if(index==0)
            {
                if (Parameter.Para[23] == "启用")
                {
                    Thread.Sleep(300);
                    if (!tab.ReadDi(Table.DI.SDJ1_Type,Parameter.Para[195]=="1"))
                    {
                        screwTypeError = true;
                        
                    }
                    
                    tab.WriteDO(Table.DO.SDJ1_BlowStop, true);
                    Thread.Sleep(400);
                    tab.WriteDO(Table.DO.SDJ1_BlowStop, false);
                    Thread.Sleep(100);
                }
                else if (Parameter.Para[12] == "震动盘组合送钉机")
                {
                    tab.WriteDO(Table.DO.SDJ1_BlowStop, true);
                    Thread.Sleep(400);
                    tab.WriteDO(Table.DO.SDJ1_BlowStop, false);
                    Thread.Sleep(100);
                }
               
                tab.WriteDO(Table.DO.SDJ1_Blow, true);
            }
            if (index == 1)
            {
                if (Parameter.Para[23] == "启用")
                {
                    Thread.Sleep(300);
                    if (tab.ReadDi(Table.DI.SDJ1_Type, Parameter.Para[196] == "1"))
                    {
                        screwTypeError = true;
                    }
                    tab.WriteDO(Table.DO.SDJ1_BlowStop, true);
                    Thread.Sleep(400);
                   
                    tab.WriteDO(Table.DO.SDJ1_BlowStop, false);
                    Thread.Sleep(100);
                }
                else if (Parameter.Para[12] == "震动盘组合送钉机")
                {
                    tab.WriteDO(Table.DO.SDJ1_BlowStop, true);
                    Thread.Sleep(400);
                    tab.WriteDO(Table.DO.SDJ1_BlowStop, false);
                    Thread.Sleep(100);
                }
                if (Parameter.Para[12] == "组合双送钉机" | Parameter.Para[12] == "震动盘组合送钉机")
                {
                    tab.WriteDO(Table.DO.SDJ1_Blow, true);
                }else
                tab.WriteDO(Table.DO.SDJ2_Blow, true);
            }
           // tab.WriteDO(Table.DO.dustCollecting, false);
            Thread.Sleep(sdjpara[index].BlowTime);
            tab.WriteDO(Table.DO.SDJ1_Blow, false);
            tab.WriteDO(Table.DO.SDJ2_Blow, false);
            tab.WriteDO(Table.DO.SDJ1_Gouding, false);
            tab.WriteDO(Table.DO.SDJ2_Gouding, false);
            tab.WriteDO(Table.DO.SDJ1_BlowStop, false);
        }
        private bool PushScrew(int index)
        {
            
             if (index == 1 &Parameter.Para[3]=="双")
            {              
                while (tab.ReadDo(Table.DO.CLD_Dianpi2))
                {
                    Thread.Sleep(5);
                }
                int temp = timeGetTime() + 3000;     
                while (!tab.ReadDi(Table.DI.Dianpi2ORG) & temp > timeGetTime())
                {
                    Thread.Sleep(5);
                    Mstatus = "等待B轴电批到达原点后推钉";
                }
                 while(ClearScrew)
                 {
                     Thread.Sleep(200);
                 }
                //if (!tab.ReadDi(Table.DI.Dianpi2ORG))
                //{
                //    ALM.AlmBoolList[14] = true;
                //    return false;
                //}
                feedmessage.Text = "B轴开始推钉";
                if (tab.ReadDi(Table.DI.CLD_Tuiding2END))
                {
                    tab.WriteDO(Table.DO.CLD_PushB1, false);
                    tab.WriteDO(Table.DO.CLD_PushB2, true);
                  //  Delay(sdjpara[0].TuidingDelay);
                    return true;
                }
                else if (tab.ReadDi(Table.DI.CLD_Tuiding2ORG))
                {
                    tab.WriteDO(Table.DO.CLD_PushB2, false);
                    tab.WriteDO(Table.DO.CLD_PushB1, true);
                 //   Delay(sdjpara[0].TuidingDelay);
                    return true;
                }
                else
                {
                    almdata[1] = dataChange.SetBitValue(almdata[1], 0, true);//推钉气缸感应器异常
                    return false;
                }
            }
             else
             {
                while (tab.ReadDo(Table.DO.CLD_Dianpi1))
                 {
                     Thread.Sleep(5);
                 }
                 int temp = timeGetTime() + 4000;     
                 while (!tab.ReadDi(Table.DI.DianpiORG) & temp >= timeGetTime())
                 {
                     Thread.Sleep(5);
                     Mstatus = "等待A轴电批到达原点后推钉";
                 }
                 while (ClearScrew)
                 {
                     Thread.Sleep(200);
                     Mstatus = "等待清钉完成";
                 }
                 //if (!tab.ReadDi(Table.DI.DianpiORG))
                 //{
                 //    ALM.AlmBoolList[7] = true;
                 //    return false;
                 //}
                 feedmessage.Text = "A轴开始推钉";
                 if (tab.ReadDi(Table.DI.CLD_TuidingEND))
                 {
                     tab.WriteDO(Table.DO.CLD_PushA1, false);
                     tab.WriteDO(Table.DO.CLD_PushA2, true);
                 //    Delay(sdjpara[1].TuidingDelay);
                     return true;
                 }
                 else if (tab.ReadDi(Table.DI.CLD_TuidingORG))
                 {
                     tab.WriteDO(Table.DO.CLD_PushA2, false);
                     tab.WriteDO(Table.DO.CLD_PushA1, true);
                //     Delay(sdjpara[1].TuidingDelay);
                     return true;
                 }
                 else
                 {
                     almdata[0] =dataChange.SetBitValue(almdata[0],4, true);//推钉气缸感应器异常
                     return false;
                 }
             }
            
            
        }
        private void feedwork_DoWork(object sender, DoWorkEventArgs e)
        {
            int pushCurrentCount = 0;
            tab.WriteDO(Table.DO.SDJ_Zhendongqi,true);
            tab.SongdingjiMode = Parameter.Para[12];
            if (Parameter.Para[19] == "滚筒上料")
                tab.GetScrewMode = "滚筒";
            else
                tab.GetScrewMode = "";
            while(!feedwork.CancellationPending)
            {
                bool auto = tab.ReadDi(ThreeAxisTable.Table.DI.Auto);
                if(true)
                {                  
                    if (songding1Step == 0 & (getScrew1 | getScrew2))
                    {
                        if (Parameter.Para[17] == "复锁")
                        {
                            getScrew1 = false;
                            getScrew2 = false;
                        }
                        else
                        {
                            if (combbPara15.SelectedIndex >= 2 & combbPara15.SelectedIndex < 4)
                            {
                                getScrew1 = false;
                                getScrew2 = false;
                            }
                            else
                            {
                                if (Unscrew)
                                {
                                    getScrew1 = false;
                                    getScrew2 = false;
                                }
                                else
                                    songding1Step = 1;
                            }
                        }
                    }
                    int index = 0;
                    if (getScrew1)
                    {
                      
                        if (tab.ReadDo(Table.DO.SDJ_Change) & Parameter.Para[12] != "组合双送钉机")
                        {
                            tab.WriteDO(Table.DO.SDJ_Change, false);
                            Thread.Sleep(int.Parse(Parameter.Para[63]));
                        }
                        index = 0;
                    }
                    else if (getScrew2)
                    {
                        if (!tab.ReadDo(Table.DO.SDJ_Change) & Parameter.Para[12] != "组合双送钉机")
                        {
                            tab.WriteDO(Table.DO.SDJ_Change, true);
                            Thread.Sleep(int.Parse(Parameter.Para[63]));
                        }
                        index = 1;
                    }
                    if (songding1Step == 1 & !(dataChange.GetBitValue(almdata[0], 0) | dataChange.GetBitValue(almdata[0], 1) | dataChange.GetBitValue(almdata[0], 2) | dataChange.GetBitValue(almdata[0], 3)))
                    {
                        if (Parameter.Para[12] != "震动盘组合送钉机")
                        {
                            if (GetScrew(index))
                            {
                                songding1Step = 3;
                            }
                        }
                        else
                        {
                            if (GetScrew2(index))
                            {
                                songding1Step = 3;
                            }
                        }
                    }
                    if(songding1Step==2)
                    {
                       
                    }
                    if(songding1Step==3)
                    {
                        BlowScrew(index);
                        Delay(sdjpara[0].TuidingDelay);
                        songding1Step = 4;
                        if (Parameter.Para[12] == "组合双送钉机")
                        songding1Delay[2] = sdjpara[0].GouDingBackDelay + timeGetTime();
                    }
                    if (songding1Step == 4 & !(dataChange.GetBitValue(almdata[0], 4) | dataChange.GetBitValue(almdata[1], 1)))
                    {
                       
                        if (PushScrew(index))
                        {
                            songding1Step = 5;
                        }                   
                    }
                    if (songding1Step == 5 & !(dataChange.GetBitValue(almdata[0], 5) | dataChange.GetBitValue(almdata[0], 6) | dataChange.GetBitValue(almdata[1], 2) | dataChange.GetBitValue(almdata[1], 3)))
                    {
                      
                        if (index == 0|Parameter.Para[3]!="双")
                        {
                            if (tab.ReadDo(Table.DO.CLD_PushA1))
                            {
                                int temp = timeGetTime() + 2000;
                                feedmessage.Text = "a轴等待推钉到位";
                                while (!tab.ReadDi(Table.DI.CLD_TuidingEND))
                                {
                                    labDebugInfor.Text = (temp - timeGetTime()).ToString();
                                   if(timeGetTime()>temp)
                                   {
                                       break;
                                   }
                                }
                               
                                if (!tab.ReadDi(Table.DI.CLD_TuidingEND))
                                {
                                    
                                    pushCurrentCount++;
                                    feedmessage.Text = "A轴推钉失败,重试+" + pushCurrentCount.ToString();
                                    if (pushCurrentCount < pushCount)
                                    {
                                        tab.WriteDO(Table.DO.CLD_PushA1, false);
                                        tab.WriteDO(Table.DO.CLD_PushA2, true);
                                        Thread.Sleep(500);
                                        songding1Step = 4;
                                    }
                                    else
                                    {
                                        almdata[0] = dataChange.SetBitValue(almdata[0], 5, true);//推钉气缸动点异常
                                    }
                                }
                                else
                                {
                                    pushCurrentCount = 0;
                                    getScrew1 = false;
                                    getScrew2 = false;
                                    songding1Step = 0;
                                }
                            }
                            else
                            {
                                int temp = timeGetTime() + 1000;

                                feedmessage.Text = "等待推钉到位";
                                while ( !tab.ReadDi(Table.DI.CLD_TuidingORG))
                                {
                                    labDebugInfor.Text = (temp - timeGetTime()).ToString();
                                    if(temp < timeGetTime())
                                    {
                                        break;
                                    }
                                }
                               
                                if (!tab.ReadDi(Table.DI.CLD_TuidingORG))
                                {
                                    pushCurrentCount++;
                                    feedmessage.Text = "推钉失败,重试+" + pushCurrentCount.ToString();
                                    if (pushCurrentCount < pushCount)
                                    {
                                        tab.WriteDO(Table.DO.CLD_PushA1, true);
                                        tab.WriteDO(Table.DO.CLD_PushA2, false);
                                        Thread.Sleep(200);
                                        songding1Step = 4;
                                    }
                                    else
                                    {

                                        almdata[0] = dataChange.SetBitValue(almdata[0], 6, true);//推钉气缸原点异常
                                    }

                                }
                                else
                                {
                                    pushCurrentCount = 0;
                                    getScrew1 = false;
                                    getScrew2 = false;
                                    songding1Step = 0;
                                }
                            }
                        }
                        else
                        {
                            if (tab.ReadDo(Table.DO.CLD_PushB1))
                            {
                                int temp = timeGetTime() + 1000;
                                feedmessage.Text = "等待推钉到位";
                                while ( !tab.ReadDi(Table.DI.CLD_Tuiding2END))
                                {
                                    labDebugInfor.Text = (temp - timeGetTime()).ToString();
                                   if(temp < timeGetTime())
                                   {
                                       break;
                                   }
                                }
                               
                                if (!tab.ReadDi(Table.DI.CLD_Tuiding2END))
                                {
                                    pushCurrentCount++;
                                    feedmessage.Text = "推钉失败,重试+" + pushCurrentCount.ToString();
                                    if (pushCurrentCount < pushCount)
                                    {
                                        tab.WriteDO(Table.DO.CLD_PushB1, false);
                                        tab.WriteDO(Table.DO.CLD_PushB2, true);
                                        Thread.Sleep(200);
                                        songding1Step = 4;
                                    }
                                    else
                                    {
                                        almdata[1] = dataChange.SetBitValue(almdata[1], 2, true) ;//推钉气缸动点异常
                                    }
                                }
                                else
                                {
                                    getScrew1 = false;
                                    getScrew2 = false;
                                    songding1Step = 0;
                                }
                            }
                            else
                            {
                                int temp = timeGetTime() + 1000;
                                feedmessage.Text = "等待推钉到位";                               
                                while ( !tab.ReadDi(Table.DI.CLD_Tuiding2ORG))
                                {
                                    labDebugInfor.Text = (temp - timeGetTime()).ToString();
                                   if( temp < timeGetTime())
                                   {
                                       break;
                                   }
                                }
                               
                                if (!tab.ReadDi(Table.DI.CLD_Tuiding2ORG))
                                {
                                    pushCurrentCount++;
                                    feedmessage.Text = "推钉失败,重试+" + pushCurrentCount.ToString();
                                    if (pushCurrentCount < pushCount)
                                    {
                                        tab.WriteDO(Table.DO.CLD_PushB1, true);
                                        tab.WriteDO(Table.DO.CLD_PushB2, false);
                                        Thread.Sleep(200);
                                        songding1Step = 4;
                                    }
                                    else
                                    {
                                        almdata[1] = dataChange.SetBitValue(almdata[1], 3, true); ;//推钉气缸原点异常
                                    }
                                }
                                else
                                {
                                    getScrew1 = false;
                                    getScrew2 = false;
                                    songding1Step = 0;
                                }
                            }
                        }
                      while(songding1Delay[2]>timeGetTime())
                      {
                          Thread.Sleep(100);
                      }
                    }

                }
            }
        }
        #endregion

        private void butStart_Click(object sender, EventArgs e)
        {
            if(Parameter.Para[18]=="N.M")
            {
               if(float.Parse(Parameter.Para[154])>3.8)
               {
                   MessageBox.Show("当前拧螺钉参数中力矩单位为N.M，A轴设定参数超出范围(0-3.81)，请先确认参数再启动");
                   return;
               }
               if (float.Parse(Parameter.Para[180]) > 3.8 & Parameter.Para[3] == "双")
               {
                   MessageBox.Show("当前拧螺钉参数中力矩单位为N.M，B轴设定参数超出范围(0-3.81)，请先确认参数再启动");
                   return;
               }              
            }
            if (Parameter.Para[18] == "百分比")
            {
                int value= int.Parse(Parameter.Para[154]);
                if (value > 300 | value<=3.8)
                {
                    MessageBox.Show("当前拧螺钉参数中力矩单位为电机百分比，A轴设定参数超出范围（4-300），请先确认参数再启动");
                    return;
                }
                value = int.Parse(Parameter.Para[180]);
                if (value > 300 | value <= 3.8)
                {
                    if (Parameter.Para[3] == "双")
                    {
                        MessageBox.Show("当前拧螺钉参数中力矩单位为电机百分比，B轴设定参数超出范围（4-300），请先确认参数再启动");
                        return;
                    }
                }
            }

            if (Parameter.Para[0] == "桌面双平台")
            {
                int n = cbb_TableSelect.SelectedIndex;
                if(n==0)
                {
                    if (tab.ReadDi(Table.DI.Table1Get) | testMode != 0)
                    {
                        table1Run = true;
                        tab.WriteDO(Table.DO.CLD_PushB1, true);
                        table2Run = false;
                        Delay(500);
                    }
                }
                if (n == 1)
                {
                    if (tab.ReadDi(Table.DI.Table2Get) | testMode != 0)
                    {
                        table1Run = false;
                        table2Run = true;
                        tab.WriteDO(Table.DO.CLD_PushB2, true);
                        Delay(500);
                    }
                }
                StartCheck();
                return;
            }
            for (int i = 0; i < lifetimes.Length;i++)
            {
                int temp = int.Parse(Parameter.Para[85 + i]);
                if(temp<lifetimes[i])
                {
                    MessageBox.Show("耗材寿命达到上限！");
                    return;
                }
            }
                if (DataGrid.RowCount <= 0)
                {

                    MessageBox.Show("未添加产品数据！");
                    return;
                }
            if(!auto)
            {
                MessageBox.Show("启动前请先进入到自动状态！");
                return;
            }
            if (!feedwork.IsBusy)
            {
                feedwork.RunWorkerAsync();
            }
            if (!string.IsNullOrWhiteSpace(lastAlmMess))
            {
                MessageBox.Show("启动前先排除报警！");
                return;
            }
            //if(!product_Offset.IsSet)
            //{
            //    MessageBox.Show("产品起点未校正，请先校正起点！");
            //    return;
            //}
            if (Parameter.Para[22] == "手动扫码" || Parameter.Para[22] == "爱立信扫码")
            {
                if(txtManuCode.Text.Length<5)
                {
                    MessageBox.Show("检测到已启用扫码功能，请先扫码，在生产！");
                    return;
                }
            }
            if (bgwWork.IsBusy)
            {
                MessageBox.Show("生产进行中！请勿重复启动！");
                return;
            }
            if(Parameter.Para[4]=="启用")
            {
                if(Points.points[0].ScrewType!=2)
                {
                    MessageBox.Show("机器视觉已启用,未检测到视觉拍照点位，请取消视觉或添加拍照点位！");
                    return;
                }
            }
            if (startNum != 0)
            {
                if (DialogResult.No == MessageBox.Show("检测到上次工作到" + (startNum + 1).ToString() + "点,是否继续当前工作？", "提示！", MessageBoxButtons.YesNo))
                {
                    startNum = 0;
                    run = false;
                }
            }          
            bgwWork.RunWorkerAsync();
        }

        private void StartCheck()
        {
            if (DataGrid.RowCount <= 0 & table1Run)
            {
                table1Run = false;
                MessageBox.Show("平台1没有产品数据！");
               
            }
            for (int i = 0; i < lifetimes.Length; i++)
            {
                int temp = int.Parse(Parameter.Para[85 + i]);
                if (temp < lifetimes[i])
                {
                    MessageBox.Show("耗材寿命达到上限！");
                    return;
                }
            }
            if (DataGrid2.RowCount <= 0 & table2Run)
            {
                table2Run = false;
                MessageBox.Show("平台2没有产品数据！");          
            }
            if (!auto)
            {
                MessageBox.Show("启动前请先取消暂停状态！");
                table2Run = false;
                table1Run = false;
            }
            //if (!string.IsNullOrWhiteSpace(lastAlmMess))
            if(!string.IsNullOrWhiteSpace(lastAlmMess))
            {
                MessageBox.Show("启动前先排除报警！");
                table2Run = false;
                table1Run = false;
            }
            if (startNum != 0&table1Run)
            {
                if (DialogResult.No == MessageBox.Show("检测到上次工作到" + (startNum + 1).ToString() + "点,是否继续当前工作？", "提示！", MessageBoxButtons.YesNo))
                {
                    startNum = 0;                   
                }
               
            }
            if (startNum2 != 0 & table2Run)
            {
                if (DialogResult.No == MessageBox.Show("检测到上次工作到" + (startNum2 + 1).ToString() + "点,是否继续当前工作？", "提示！", MessageBoxButtons.YesNo))
                {
                    startNum2 = 0;
                }
               
            }
            if (!feedwork.IsBusy)
            {
                feedwork.RunWorkerAsync();
            }
            if(table1Run|table2Run)
            {
                if(!bgwWork.IsBusy)
                bgwWork.RunWorkerAsync();
            }
            
        }
        private void ToolReset()
        {
            tab.WriteDO(Table.DO.CLD_vacuo, false);
            tab.WriteDO(Table.DO.CLD_vacuo2, false);
            tab.WriteDO(Table.DO.CLD_Blow1, false);
            tab.WriteDO(Table.DO.CLD_Blow2, false);
            tab.WriteDO(Table.DO.CLD_Dianpi1, false);
            tab.WriteDO(Table.DO.CLD_Dianpi2, false);
            tab.GotoZ(0,tab.SpeedRate);
            //tab.WriteDO()
        }
        private void ToolRun(bool index1,bool index2,bool dir=false)
        {
            tab.WriteDO(0, true);
            tab.WriteDO(1, dir);
            if(Parameter.Para[0]!="桌面双平台")
            {
                tab.WriteDO(2, true);
            }
            tab.WriteDO(Table.DO.ServoA_Speed0, index1);
            tab.WriteDO(Table.DO.ServoA_Torque0, index1);
            tab.WriteDO(Table.DO.ServoA_Speed1, index2);
            tab.WriteDO(Table.DO.ServoA_Torque1, index2);
        }
        private void ToolClear(int index)
        {
            tab.GotoSystemPoint(index + 1,cameraSpd);
            ToolRun(true, false);
            int clearTime = int.Parse(Parameter.Para[98]);
            //if(clearTime<=100)
            //{
            //    clearTime = 500;
            //}
            if (index == 0)
            {
                if (Parameter.Para[3] == "双" & tab.ReadDo(Table.DO.BaxisEnable))
                {
                    //ToolRun(true, false);
                    Thread.Sleep(1000);
                    tab.WriteDO(Table.DO.BaxisEnable, false);
                    Thread.Sleep(1000);
                }
                tab.WriteDO(Table.DO.CLD_Bizhang1, false);
                Delay(300);
                tab.WriteDO(Table.DO.CLD_Dianpi1, true);
                Delay(500);
                tab.WriteDO(Table.DO.CLD_Dianpi1, false);
                Delay(500);
                PushScrew(index);
                Delay(500);
                tab.WriteDO(Table.DO.CLD_Dianpi1, true);
                Delay(500);
                tab.WriteDO(Table.DO.CLD_Dianpi1, false);
                ToolRun(false, false);
                Delay(500);
                hadScrew = false;
            }
            else
            {
                if (Parameter.Para[3] == "双" & !tab.ReadDo(Table.DO.BaxisEnable))
                {
                    //ToolRun(true, false);
                    Thread.Sleep(1000);
                    tab.WriteDO(Table.DO.BaxisEnable, true);
                    Thread.Sleep(1000);
                }
                tab.WriteDO(Table.DO.CLD_Bizhang1, false);
                Delay(500);                  
                tab.WriteDO(Table.DO.CLD_Dianpi2, true);
                Delay(500);
                tab.WriteDO(Table.DO.CLD_Dianpi2, false);
                Delay(500);
                PushScrew(index);
                Delay(500);
                tab.WriteDO(Table.DO.CLD_Dianpi2, true);
                Delay(500);
                tab.WriteDO(Table.DO.CLD_Dianpi2, false);
                ToolRun(false, false);
                Delay(500);
            }
            tab.GotoZ(0, tab.SpeedRate);
            hadScrew = false;
        }
        bool hadScrew = false;
        int currentParaIndex = -1;
        Stopwatch cycle = new Stopwatch();
        private void AxisParaUpdata(int mode=0,float k=1)
        {
            float[] torques = new float[3];
            if (mode == 0)
            {
                axisPara[0] =(int)( int.Parse(Parameter.Para[150]) * 10*k);
                axisPara[1] = (int)(int.Parse(Parameter.Para[153]) * 10 * k);
                axisPara[2] = (int)(int.Parse(Parameter.Para[157]) * 10 * k);
                //axisPara[3] = int.Parse(Parameter.Para[151]);
                //axisPara[4] = int.Parse(Parameter.Para[154]);
                //axisPara[5] = int.Parse(Parameter.Para[158]);
                torques[0] = float.Parse(Parameter.Para[151]) + float.Parse(Parameter.Para[93]);
                torques[1] = float.Parse(Parameter.Para[154]) + float.Parse(Parameter.Para[93]);
                torques[2] = float.Parse(Parameter.Para[158]) + float.Parse(Parameter.Para[93]);
                axisPara[6] = int.Parse(Parameter.Para[152]);
                axisPara[7] = int.Parse(Parameter.Para[155]);
                axisPara[8] = int.Parse(Parameter.Para[159]);
                axisPara[9] = int.Parse(Parameter.Para[156]);
                axisPara[10] = int.Parse(Parameter.Para[160]);
                axisPara[11] = int.Parse(Parameter.Para[161]);//最短拧螺钉时间
            }if(mode==1)
            {
                axisPara[0] = (int)(int.Parse(Parameter.Para[176]) * 10 * k);
                axisPara[1] = (int)(int.Parse(Parameter.Para[179]) * 10 * k);
                axisPara[2] = (int)(int.Parse(Parameter.Para[183]) * 10 * k);
                //axisPara[3] = int.Parse(Parameter.Para[177]);
                //axisPara[4] = int.Parse(Parameter.Para[180]);
                //axisPara[5] = int.Parse(Parameter.Para[184]);
                torques[0] = float.Parse(Parameter.Para[177])+float.Parse(Parameter.Para[93]);
                torques[1] = float.Parse(Parameter.Para[180]) + float.Parse(Parameter.Para[93]);
                torques[2] = float.Parse(Parameter.Para[184]) + float.Parse(Parameter.Para[93]);
                axisPara[6] = int.Parse(Parameter.Para[178]);
                axisPara[7] = int.Parse(Parameter.Para[181]);
                axisPara[8] = int.Parse(Parameter.Para[185]);
                axisPara[9] = int.Parse(Parameter.Para[182]);
                axisPara[10] = int.Parse(Parameter.Para[186]);
                axisPara[11] = int.Parse(Parameter.Para[187]);//最短拧螺钉时间
            }
            if (mode==2)
            {
                axisPara[0] = int.Parse(Parameter.Para[170]) * 10;
                axisPara[1] = int.Parse(Parameter.Para[172]) * 10;
                // axisPara[2] = int.Parse(Parameter.Para[157]) * 10;
                torques[0] = float.Parse(Parameter.Para[171]) + float.Parse(Parameter.Para[93]);
                torques[1] = float.Parse(Parameter.Para[173]) + float.Parse(Parameter.Para[93]);
                //axisPara[5] = int.Parse(Parameter.Para[158]);
                //axisPara[6] = int.Parse(Parameter.Para[152]);
                //axisPara[7] = int.Parse(Parameter.Para[155]);
                //axisPara[8] = int.Parse(Parameter.Para[159]);
                //axisPara[9] = int.Parse(Parameter.Para[156]);
                //axisPara[10] = int.Parse(Parameter.Para[160]);
            }
            if(Parameter.Para[18]=="N.M")
            {
                //float off = float.Parse(Parameter.Para[93]);
                axisPara[3] = (int)((torques[0]) * 100 / 1.27);
                axisPara[4] = (int)(torques[1] * 100 / 1.27);
                axisPara[5] = (int)(torques[2] * 100 / 1.27);
            }else
            {
                axisPara[3] = (int)(torques[0]);
                axisPara[4] = (int)(torques[1]);
                axisPara[5] = (int)(torques[2]);
            }
            currentParaIndex = mode;
            tab.WriteModBus_int(440, 6, axisPara);
            int[] temp=new int[1]{1};
            tab.WriteModBus_int(459, 1, temp);
            Thread.Sleep(100);
            tab.WriteModBus_int(459, 0, temp);
            //int i1= tab.ReadModBus_int(440);
            //int i2 = tab.ReadModBus_int(442);
            //int i3 = tab.ReadModBus_int(444);
            //int i4 = tab.ReadModBus_int(446);
            //int i5 = tab.ReadModBus_int(448);
            //int i6 = tab.ReadModBus_int(450);
        }
        float cameraSpd = 0.3f;
        int _curIndex = 0;
        private void bgwWork_DoWork(object sender, DoWorkEventArgs e)
        {
            cameraSpd = float.Parse(Parameter.Para[197]) / 100;
            if(cameraSpd<0.1f)
            {
                cameraSpd = tab.SpeedRate;
            }
            Points.Point[] workPoint=Points.points.ToArray();
            CoordCorrectionPoint ccPointStart = startPoint[0];
            CoordCorrectionPoint ccPointEnd = endPoint[0];
            afurl.faults.Clear();
            bool floatTeach = false;
            float currentLength = 0;
            int floatCheckTime = int.Parse(Parameter.Para[94]);
            if(combbPara15.SelectedIndex==4)
            floatTeach = true;
            hadScrew = false;
            if (table2Run)
            {
                workPoint = Points.points2.ToArray();
                ccPointStart = startPoint[1];
                ccPointEnd = endPoint[1];
                startNum = startNum2;
            }           
            if (!auto)
                return;
            #region//翻转线体部分
            if (Parameter.Para[0]=="双面线")
            {
                while (!productReady)
                {
                    if (productOKtest | !string.IsNullOrWhiteSpace(lastAlmMess) | !auto)
                    { break; }
                    Mstatus = "等待线体产品到位";
                    Thread.Sleep(100);
                }
                if (!auto | !string.IsNullOrWhiteSpace(lastAlmMess))
                {
                    run = false;
                    table1Run = false;
                    table2Run = false;
                    return;
                }
            }

            while(safetyLight)
            {
                Mstatus = "光幕异常...";
                almdata[1] = dataChange.SetBitValue(almdata[1], 5, true);
                Thread.Sleep(300);
            }
            almdata[1] = dataChange.SetBitValue(almdata[1], 5, false);
            run = true;
            Mstatus = "正在复位主轴...";
            ToolReset();
            if (Parameter.Para[14] == "启动时清钉")
            {
                if (Parameter.Para[3]=="双"&tab.ReadDo(Table.DO.BaxisEnable))
                    ToolClear(1);
                else 
                ToolClear(0);
              
                //if(Parameter.Para[3]=="双")
                //{
                //    ToolClear(1);
                //    if (Points.points[0].Axis == 0)
                //    {
                //        ToolRun(true, false);
                //        Thread.Sleep(200);
                //        tab.WriteDO(Table.DO.BaxisEnable, true);
                //        Thread.Sleep(100);
                //        ToolRun(false, false);
                //    }
                //}
            }
            if (Parameter.Para[105] == "反面" & !dataChange.GetBitValue(mw100[5], 5))
            {
                tab.GotoSystemPoint(3);              
                while (!productReady)
                {
                    if (productOKtest | !string.IsNullOrWhiteSpace(lastAlmMess) | !auto)
                    { break; }
                    Mstatus = "等待线体产品到位";
                    Thread.Sleep(100);
                }
                if (!auto | !string.IsNullOrWhiteSpace(lastAlmMess))
                {
                    return;
                }
                while(!Table.Canturn)
                {
                    if (productOKtest | !string.IsNullOrWhiteSpace(lastAlmMess) | !auto)
                    { break; }
                    Mstatus = "主轴翻转安全位异常";
                    Thread.Sleep(100);
                }
                if (!auto | !string.IsNullOrWhiteSpace(lastAlmMess))
                {
                    return;
                }
                while (safetyLight)
                {
                    Mstatus = "光幕异常...";
                    almdata[1] = dataChange.SetBitValue(almdata[1], 5, true);
                    Thread.Sleep(300);
                }
                almdata[1] = dataChange.SetBitValue(almdata[1], 5, false);
                turnGo = true;
                while (!dataChange.GetBitValue(mw100[5],13))
                {
                    if (productOKtest | !string.IsNullOrWhiteSpace(lastAlmMess) | !auto)
                    { break; }
                    Mstatus = "等待翻转启动";
                    Thread.Sleep(200);
                    
                }
                if (productOKtest | !string.IsNullOrWhiteSpace(lastAlmMess) | !auto)
                {
                    turnGo = false;
                    return;
                }
                Delay(100);
                turnGo = false;
               
                while (!dataChange.GetBitValue(mw100[5], 5))
                {
                    
                    Mstatus = "等待反面标志";
                    Thread.Sleep(100);
                }
                if (!auto | !string.IsNullOrWhiteSpace(lastAlmMess))
                {
                    return;
                }
            }
            #endregion
            mw0[1] = dataChange.SetBitValue(mw0[1], 9, true);
            
            float x=0;
            float y=0 ;
            float z=0;
            int axis=0;
            int screwType=0 ;
            int workMode=0;
            int lastWorkMode = 0;
                x = workPoint[0].X;
                y = workPoint[0].Y;
                z = workPoint[0].Z;
                axis = workPoint[0].Axis;
                screwType = workPoint[0].ScrewType;
                workMode = workPoint[0].WorkMode;
          
            if(axis==1)
            {
                x += float.Parse(Parameter.Para[83]);
                y += float.Parse(Parameter.Para[84]);
            }
           // int LastScrewType = 0;
            workErrorNum = 0;
            workFloatNum = 0;
            workLooseNum = 0;
            float forbit = float.Parse(Parameter.Para[96]);
            if(forbit>1|forbit<0.2)
            {
                forbit = 1;
             }
            //if (Unscrew)
            //{
            //    AxisParaUpdata(2);
            //}
            //else
            //{ AxisParaUpdata(screwType); }
           
            int errorCount = 0;
            
            
            if (!auto | !string.IsNullOrWhiteSpace(lastAlmMess))
            {
                return;
            }
            databaseList[7] = DateTime.Now.ToString();
           
           
            #region//视觉启用
            if (screwType == 2&Parameter.Para[4]=="启用"&startNum==0)
            {
                if(Parameter.Para[5]=="爱立信")
                {
                    while (!productReady)
                    {
                        if (productOKtest | !string.IsNullOrWhiteSpace(lastAlmMess) | !auto)
                        { return; }
                        Mstatus = "等待线体产品到位";
                        Thread.Sleep(100);
                    }
                }

                if (Parameter.Para[21] == "启用")
                {
                    tab.WriteDO(Table.DO.CaremaLift, true);
                    Thread.Sleep(int.Parse(Parameter.Para[95]));
                }
                while (safetyLight)
                {
                    Mstatus = "光幕异常...";
                    
                    almdata[1] = dataChange.SetBitValue(almdata[1], 5, true);
                    Thread.Sleep(300);
                }
                almdata[1] = dataChange.SetBitValue(almdata[1], 5, false);
                    int NextScrew = workPoint[2].ScrewType;
                    int NextAxis = workPoint[2].Axis;
                    if (NextScrew == 1)
                    {
                        if (!tab.ReadDo(Table.DO.SDJ_Change) & Parameter.Para[12] != "组合双送钉机")
                        {
                            tab.WriteDO(Table.DO.SDJ_Change, true);
                            Thread.Sleep(int.Parse(Parameter.Para[63]));
                        }
                        getScrew2 = true;
                    }
                    else
                    {
                        if (tab.ReadDo(Table.DO.SDJ_Change) & Parameter.Para[12] != "组合双送钉机")
                        {
                            tab.WriteDO(Table.DO.SDJ_Change, false);
                            Thread.Sleep(int.Parse(Parameter.Para[63]));
                        }
                        getScrew1 = true;
                    }
                    hadScrew = true;             
                Mstatus = "视觉纠偏中...";
                float[] cameraOffset = new float[2] { float.Parse(Parameter.Para[81]), float.Parse(Parameter.Para[82]) };
                if (Parameter.Para[0] != "无")
                {
                    while (!productReady)
                    {
                        if (productOKtest | !string.IsNullOrWhiteSpace(lastAlmMess) | !auto)
                        { break; }
                        Mstatus = "等待线体产品到位";
                        Thread.Sleep(100);
                    }
                    if (!auto | !string.IsNullOrWhiteSpace(lastAlmMess))
                    {
                        return;
                    }
                }
                tab.WriteDO(Table.DO.CameraLight, true);
                tab.GotoPoint(0, x + cameraOffset[0], y + cameraOffset[1], z, cameraSpd);
                Thread.Sleep(500);
                
                Thread.Sleep(int.Parse(Parameter.Para[99]));

                bool re= visionView.TakePhoto(0);
                int count=0;
               
                while(!re&count<3)
                {
                    Thread.Sleep(200);
                    re = visionView.TakePhoto(0);
                    count++;
                }
                if (!re)
                {
                    run = true;
                    return;
                }
                if (!auto | !string.IsNullOrWhiteSpace(lastAlmMess))
                {
                    return;
                }
                Thread.Sleep(50);
                Dictionary<string, object> result = new Dictionary<string, object>();
                visionView.GetResult(0, 0, result);
                if(result.Count<0)
                {
                    almdata[0] = dataChange.SetBitValue(almdata[0], 15, true);
                    Mstatus = "原点拍照失败..";
                    return;
                }
                //return;
                PointF visionPoint = new PointF();
                PointF photoPoint = new PointF(Table.axisStatus[0].CurrentPos, Table.axisStatus[1].CurrentPos);
                PointF fristPoint = new PointF();
               
                visionPoint.X = float.Parse(result["X(mm)"].ToString());
                visionPoint.Y = float.Parse(result["Y(mm)"].ToString());
                visionView.TransformVisionCoordinateToMachineCoordinate(visionPoint, photoPoint, out fristPoint);

                PointF OrgPoint1 = new PointF(x, y);
                float allowOFF = float.Parse(Parameter.Para[58]);
                if (Math.Abs(OrgPoint1.X - fristPoint.X) > allowOFF | Math.Abs(OrgPoint1.Y - fristPoint.Y) > allowOFF)
                {
                    almdata[1] = dataChange.SetBitValue(almdata[1], 0, true);
                    Mstatus = "原点位置偏差大于允许误差";
                    MessageBox.Show("原点位置:" + OrgPoint1.X.ToString() + "," + OrgPoint1.Y.ToString() + "拍照位置:" + fristPoint.X.ToString() + "," + fristPoint.Y.ToString());
                    run = true;
                    return;
                    almdata[1] = dataChange.SetBitValue(almdata[1], 0, false);
                }
                if (!auto | !string.IsNullOrWhiteSpace(lastAlmMess))
                {
                    return;
                }
                while (safetyLight)
                {
                    Mstatus = "光幕异常...";
                    almdata[1] = dataChange.SetBitValue(almdata[1], 5, true);
                    Thread.Sleep(500);
                }
                almdata[1] = dataChange.SetBitValue(almdata[1], 5, false);
                x = workPoint[1].X;
                y = workPoint[1].Y;
                z = workPoint[1].Z;
                axis = workPoint[1].Axis;
                screwType = workPoint[1].ScrewType;
                workMode = workPoint[1].WorkMode;
                PointF OrgPoint2 = new PointF(x, y);
                tab.GotoPoint(0, x + cameraOffset[0], y + cameraOffset[1], z, cameraSpd);
                Thread.Sleep(int.Parse(Parameter.Para[99]));
                //return;
                visionView.TakePhoto(1);
                Thread.Sleep(100);
                visionView.GetResult(1, 0, result);
                if (result.Count < 0)
                {
                    Mstatus = "参考点拍照失败..";
                   
                    return;
                }
                if (!auto | !string.IsNullOrWhiteSpace(lastAlmMess))
                {
                    Mstatus = "手动或报警";
                    return;
                }
                visionPoint.X = float.Parse(result["X(mm)"].ToString());
                visionPoint.Y = float.Parse(result["Y(mm)"].ToString());
                PointF secondPoint = new PointF();
                photoPoint = new PointF(Table.axisStatus[0].CurrentPos, Table.axisStatus[1].CurrentPos);
                visionView.TransformVisionCoordinateToMachineCoordinate(visionPoint, photoPoint, out secondPoint);
                if (Math.Abs(OrgPoint2.X - secondPoint.X) > allowOFF | Math.Abs(OrgPoint2.Y - secondPoint.Y) > allowOFF)
                {
                    almdata[1] = dataChange.SetBitValue(almdata[1], 0, true);
                    Mstatus = "参考点位置偏差大于允许误差";
                    MessageBox.Show("参考点位置:" + OrgPoint2.X.ToString() + "," + OrgPoint2.Y.ToString() + "拍照位置:" + secondPoint.X.ToString() + "," + secondPoint.Y.ToString());
                    almdata[1] = dataChange.SetBitValue(almdata[1], 0, false);
                    return;
                }
                if (!auto | !string.IsNullOrWhiteSpace(lastAlmMess))
                {
                    return;
                }
                CoordinateChange.SetProductCoordinate(OrgPoint1, OrgPoint2, fristPoint, secondPoint);
               // return;
                tab.WriteDO(Table.DO.CameraLight, false);
                tab.WriteDO(Table.DO.CaremaLift, false);
                if (startNum == 0)
                {
                    startNum = 2;
                }
                Mstatus = "视觉拍照完成";
            }

            if ( Parameter.Para[4] != "启用")
            {
                int snum=0;
                if (table2Run)
                    snum = startNum2;
                else
                    snum = startNum;
                int NextScrew = workPoint[snum].ScrewType;
                int NextAxis = workPoint[snum].Axis;
                
            }
            #endregion
            cycle.Reset();
            cycle.Start();
            run = true;
            if (Parameter.Para[0] != "无" & Parameter.Para[0] != "桌面双平台")
            {
                while (!productReady)
                {

                    if (productOKtest | !string.IsNullOrWhiteSpace(lastAlmMess) | !auto)
                    { break; }
                    Mstatus = "等待线体产品到位";
                    Thread.Sleep(100);
                }
                if (!auto | !string.IsNullOrWhiteSpace(lastAlmMess))
                {
                    return;
                }
            }
            #region//立讯扫码
            if (startNum == 0 | (startNum == 2 & Parameter.Para[4] == "启用"))
            {
                if (Parameter.Para[22] == "启用")
                {

                    tab.GotoSystemPoint(4);
                    int scannerCount=0;
                retry:  Mesh.WebApi.ReadCode();
                    int timedelay = timeGetTime() + int.Parse(Parameter.Para[97]) + 1000;                   
                    while (!lxScannerOK)
                    {
                        Mstatus = "等待扫码完成或MES数据返回";
                        Thread.Sleep(100);
                        if (!string.IsNullOrWhiteSpace(lastAlmMess) | !auto)
                        { return; }
                        
                        if(timeGetTime()>timedelay)
                        {
                            if (scannerCount<3)
                            {
                                scannerCount++;
                                goto retry;
                            }
                            almdata[1] = dataChange.SetBitValue(almdata[1], 14, true);
                        }
                    }
                    txtSeriNum.Text = Mesh.WebApi.BarCode.ToString();
                }
            }
            lxScannerOK = false;
#endregion

            #region//显示图片清零
            if (startNum == 0 | (startNum == 2 & Parameter.Para[4] == "启用"))
            {
                if (table2Run)
                {
                    for (int i = 0; i < DataGrid2.RowCount; i++)
                    {
                        DataGrid2.Rows[i].Cells[6].Value = "";
                    }
                    DxfShow.ElementList2.Clear();
                    DxfShow.ElementList2.AddRange(ElementListOrg2);
                    dxfparser.OutlinePic(DxfParser.DxfShow.outline2, DxfShow.ElementList2);
                    dxfparser.ShowPic(1);
                }
                else
                {
                    for (int i = 0; i < DataGrid.RowCount; i++)
                    {
                        DataGrid.Rows[i].Cells[6].Value = "";
                    }
                    DxfShow.ElementList.Clear();
                    DxfShow.ElementList.AddRange(ElementListOrg);
                    dxfparser.OutlinePic(DxfParser.DxfShow.outline, DxfShow.ElementList);
                    dxfparser.ShowPic(0);
                }
            }
            #endregion

            workPointNum = 0;
                Mstatus = "点位生产开始";
               
                productStartTime = timeGetTime();
                DBhelperData[2] = DateTime.Now.ToString();
                if (Parameter.Para[2] == "DF")
                {
                    afurl.sninfo.StartTime = DateTime.Now;
                }
                #region\\点位生产
                if (tab.ReadDo(Table.DO.CaremaLift))
                {
                    tab.WriteDO(Table.DO.CaremaLift, false);
                    Thread.Sleep(500);
                }
            for (int i = startNum; i < workPoint.Length; i++)
            {
                if(tab.Estop)
                {
                    return;
                }
                if (Parameter.Para[4] == "启用")
                {
                    _curIndex = i - 1;
                }
                else
                    _curIndex = i + 1;
                List<string> time = new List<string>();
                while (safetyLight)
                {
                    Mstatus = "光幕异常...";
                    almdata[1] = dataChange.SetBitValue(almdata[1], 5, true);
                    Thread.Sleep(100);
                }
                if (i>=1)
                {
                    lastWorkMode=workPoint[i-1].WorkMode;
                }
                almdata[1] = dataChange.SetBitValue(almdata[1], 5, false);
                workPointNum++;
                int result;
                while (workPoint[i].ScrewType == 2)
                { i = i + 1; }
                Mstatus = i.ToString() + "任务开始";
                startNum = i;
                while (pause | !string.IsNullOrWhiteSpace(lastAlmMess))
                {
                    if (!almtime.IsRunning & !string.IsNullOrWhiteSpace(lastAlmMess))
                    {
                        almtime.Restart();
                    }
                    Mstatus = "暂停或报警中";
                    Thread.Sleep(10);
                    if (bgwWork.CancellationPending | !auto | !string.IsNullOrWhiteSpace(lastAlmMess))
                    {
                        break;
                    }
                }
                if (!auto | !string.IsNullOrWhiteSpace(lastAlmMess))
                {
                    return;
                }
                if (almtime.IsRunning)
                    almtime.Stop();
                if (bgwWork.CancellationPending | !auto)
                {
                    return;
                }
                PointTime = timeGetTime();
                time.Add("打螺钉开始" + (timeGetTime() - PointTime).ToString());
                x = workPoint[i].X;
                y = workPoint[i].Y;
                z = workPoint[i].Z;
                axis = workPoint[i].Axis;
                screwType = workPoint[i].ScrewType;
                workMode = workPoint[i].WorkMode;

                while (safetyLight)
                {
                    Mstatus = "光幕异常...";
                    almdata[1] = dataChange.SetBitValue(almdata[1], 5, true);
                    Thread.Sleep(300);
                }
                almdata[1] = dataChange.SetBitValue(almdata[1], 5, false);

                if (Unscrew)
                {
                    if (currentParaIndex != 2)
                    {
                        AxisParaUpdata(2);
                    }
                }
                else
                {
                    if ((screwType != currentParaIndex)||(workMode!= lastWorkMode))
                    {
                        AxisParaUpdata(screwType, forbit);
                    }
                }



                time.Add("参数下载时间：" + (timeGetTime() - PointTime).ToString());
                if (Parameter.Para[17] != "螺母测试")
                {
                    #region//主轴切换
                    if (Parameter.Para[3] == "双")
                    {
                        //if (currentParaIndex != axis & !Unscrew)
                        //{
                        //    AxisParaUpdata(axis);
                        //}
                        if (axis == 1)
                        {
                            if (!tab.ReadDo(Table.DO.BaxisEnable))
                            {
                                tab.WriteDO(Table.DO.CLD_Bizhang1, false);
                                tab.WriteDO(Table.DO.CLD_Bizhang2, false);
                                ToolRun(true, false);
                                Thread.Sleep(200);
                                tab.WriteDO(Table.DO.BaxisEnable, true);
                                Thread.Sleep(100);

                            }
                        }
                        if (axis == 0)
                        {
                            if (tab.ReadDo(Table.DO.BaxisEnable))
                            {
                                tab.WriteDO(Table.DO.CLD_Bizhang1, false);
                                tab.WriteDO(Table.DO.CLD_Bizhang2, false);
                                ToolRun(true, false);
                                Thread.Sleep(200);
                                tab.WriteDO(Table.DO.BaxisEnable, false);
                                Thread.Sleep(100);
                            }
                        }
                    }
                    #endregion
                    time.Add("主轴切换时间：" + (timeGetTime() - PointTime).ToString());
                    #region//寿命技术
                    if (axis == 0)
                    {
                        lifetimes[0]++;
                        lifetimes[1]++;
                    }
                    else if (axis == 1)
                    {
                        lifetimes[2]++;
                        lifetimes[3]++;
                    }
                    #endregion
                    time.Add("寿命计数时间：" + (timeGetTime() - PointTime).ToString());
                    #region//拆螺钉
                    if (Unscrew | Parameter.Para[17] == "复锁")
                    {
                        if (axis == 0)
                        {
                            if (tab.ReadDo(Table.DO.CLD_Bizhang1))
                            {
                                tab.WriteDO(Table.DO.CLD_Bizhang1, false);
                                Thread.Sleep(200);
                            }
                            //tab.WriteDO(Table.DO.CLD_Dianpi1, true);
                            tab.WriteDO(Table.DO.CLD_vacuo, true);
                        }

                        if (axis == 1)
                        {
                            if (tab.ReadDo(Table.DO.CLD_Bizhang2))
                            {
                                tab.WriteDO(Table.DO.CLD_Bizhang2, false);
                                Thread.Sleep(200);
                            }
                            //tab.WriteDO(Table.DO.CLD_Dianpi2, true);
                            tab.WriteDO(Table.DO.CLD_vacuo2, true);
                        }
                        z = z - float.Parse(Parameter.Para[55]);
                        while (safetyLight)
                        {
                            Mstatus = "光幕异常...";
                            almdata[1] = dataChange.SetBitValue(almdata[1], 5, true);
                            Thread.Sleep(300);
                        }
                        almdata[1] = dataChange.SetBitValue(almdata[1], 5, false);
                        goto Unscrewflag;
                    }
                    #endregion
                    time.Add("拆螺钉判断时间：" + (timeGetTime() - PointTime).ToString());
                    #region//送钉
                    if (combbPara15.SelectedIndex != 2)
                    {
                        if (!hadScrew)
                        {
                            if (screwType == 1)
                            {
                                //if (!tab.ReadDo(Table.DO.SDJ_Change) & Parameter.Para[12] != "组合双送钉机")
                                //{
                                //    tab.WriteDO(Table.DO.SDJ_Change, true);
                                //    Delay(int.Parse(Parameter.Para[63]));
                                //}
                                getScrew2 = true;
                            }
                            else
                            {
                                //if (tab.ReadDo(Table.DO.SDJ_Change) & Parameter.Para[12] != "组合双送钉机")
                                //{
                                //    tab.WriteDO(Table.DO.SDJ_Change, false);
                                //    Delay(int.Parse(Parameter.Para[63]));
                                //}
                                getScrew1 = true;
                            }
                        }
                    }
                    #endregion

                    #region//避障
                    if (workMode == 1)
                    {
                        if (axis == 0)
                            tab.WriteDO(Table.DO.CLD_Bizhang1, false);
                        else
                            tab.WriteDO(Table.DO.CLD_Bizhang2, false);
                        z = z - float.Parse(Parameter.Para[55]);
                    }
                    else
                    {
                        if (axis == 0)
                        {
                            tab.WriteDO(Table.DO.CLD_Bizhang1, true);
                        }
                        else
                        {
                            tab.WriteDO(Table.DO.CLD_Bizhang2, true);
                        }
                    }
                    #endregion
                    time.Add("避障切换时间：" + (timeGetTime() - PointTime).ToString());
                }
                tab.SafetyH = z - float.Parse(Parameter.Para[175]);
                if (Parameter.Para[4] == "启用")
                {
                    PointF intemp = new PointF(x, y);
                    PointF outtemp = new PointF();
                    CoordinateChange.GetProductCoordinate(intemp, out outtemp);
                    x = outtemp.X;
                    y = outtemp.Y;
                    lbloffX.Text = (outtemp.X - intemp.X).ToString("f2");
                    lbloffY.Text = (outtemp.Y - intemp.Y).ToString("f2");
                }
                else if (CoordinateChange.ProductAngle != 0)
                {
                    PointF intemp = new PointF(x, y);
                    PointF outtemp = new PointF();

                    CoordinateChange.GetProductCoordinate(intemp, out outtemp);
                    x = outtemp.X;
                    y = outtemp.Y;
                    lbloffX.Text = (outtemp.X - intemp.X).ToString("f2");
                    lbloffY.Text = (outtemp.Y - intemp.Y).ToString("f2");
                }
                if (axis == 1)
                {
                    x += float.Parse(Parameter.Para[83]);
                    y += float.Parse(Parameter.Para[84]);
                }
                if (!auto | !string.IsNullOrWhiteSpace(lastAlmMess))
                {
                    return;
                }
                Mstatus = "平台定位中！";
                while (safetyLight)
                {
                    Mstatus = "光幕异常...";
                    almdata[1] = dataChange.SetBitValue(almdata[1], 5, true);
                    Thread.Sleep(500);
                }
                almdata[1] = dataChange.SetBitValue(almdata[1], 5, false);
                time.Add("坐标计算时间：" + (timeGetTime() - PointTime).ToString());
            Unscrewflag:
                int re = 0,retry=0;
              if (table1Run)
                    re= tab.GotoPoint(0, x, y, z, tab.SpeedRate, 0.85f);
                else if (table2Run)
                {
                    re=tab.GotoPoint(1, x, y, z, tab.SpeedRate, 0.85f);
                }
                else
                {
                    re=tab.GotoPoint(0, x, y, z, tab.SpeedRate, 0.85f);
                }
               
                time.Add("定位时间：" + (timeGetTime() - PointTime).ToString());
                if (Unscrew)
                {
                    if (axis == 0)
                    {
                        tab.WriteDO(Table.DO.CLD_Dianpi1, true);
                    }
                    if (axis == 1)
                    {
                        tab.WriteDO(Table.DO.CLD_Dianpi2, true);
                    }

                }
                while (safetyLight)
                {
                    Mstatus = "光幕异常...";
                    almdata[1] = dataChange.SetBitValue(almdata[1], 5, true);
                    Thread.Sleep(300);
                }
                almdata[1] = dataChange.SetBitValue(almdata[1], 5, false);
                int timedata = timeGetTime() + 3000;
                if (!Unscrew & Parameter.Para[17] != "螺母测试")
                {
                    if (Parameter.Para[17] != "复锁")
                    {
                        if (axis == 0)
                        {
                            while (!tab.ReadDi(Table.DI.DianpiORG) & timedata > timeGetTime())
                            {
                                Mstatus = "等待A轴电批原点信号...";
                                //Thread.Sleep(10);
                            }
                            if (!tab.ReadDi(Table.DI.DianpiORG))
                            {
                                almdata[0] = dataChange.SetBitValue(almdata[0], 7, true);
                                Thread.Sleep(20);
                            }
                        }
                        else
                        {
                            while (!tab.ReadDi(Table.DI.Dianpi2ORG) & timedata > timeGetTime())
                            {
                                Mstatus = "等待A轴电批原点信号...";
                                //Thread.Sleep(10);
                            }
                            if (!tab.ReadDi(Table.DI.Dianpi2ORG))
                            {
                                almdata[0] = dataChange.SetBitValue(almdata[0], 14, true);
                                Thread.Sleep(20);
                            }
                        }
                    }
                    time.Add("等待主轴到达原点：" + (timeGetTime() - PointTime).ToString());
                    if (combbPara15.SelectedIndex != 2)
                    {
                        while ((getScrew1 | getScrew2))
                        {
                            Mstatus = "等待送钉完成";
                            Thread.Sleep(50);
                            if (!auto)
                            {
                                return;
                            }
                        }
                        if (Parameter.Para[23] == "启用" & screwTypeError)
                        {
                            screwTypeError = false;
                            clearFlag = true;
                        }
                        time.Add("等待送钉完成时间：" + (timeGetTime() - PointTime).ToString());
                        if (bgwWork.CancellationPending | !auto)
                        {
                            return;
                        }
                        if (!auto | !string.IsNullOrWhiteSpace(lastAlmMess))
                        {
                            return;
                        }
                        if ((i < workPoint.Length - 1)&!clearFlag)
                        {
                            int NextScrew = workPoint[i + 1].ScrewType;
                            int NextAxis = workPoint[i + 1].Axis;
                            if (NextScrew == 1)
                            {
                                //if (!tab.ReadDo(Table.DO.SDJ_Change) & Parameter.Para[12] != "组合双送钉机")
                                //{
                                //    tab.WriteDO(Table.DO.SDJ_Change, true);
                                //    Delay(int.Parse(Parameter.Para[63]));
                                //}
                                getScrew2 = true;
                            }
                            else
                            {
                                //if (tab.ReadDo(Table.DO.SDJ_Change) & Parameter.Para[12] != "组合双送钉机")
                                //{
                                //    tab.WriteDO(Table.DO.SDJ_Change, false);
                                //    Delay(int.Parse(Parameter.Para[63]));
                                //}
                                getScrew1 = true;
                            }
                            hadScrew = true;
                        }
                    }
                    if (!clearFlag)
                    {


                        if (workMode != 0)
                        {
                            if (axis == 0)
                            {
                                tab.WriteDO(Table.DO.CLD_vacuo, true);
                            }
                            if (axis == 1)
                            {
                                tab.WriteDO(Table.DO.CLD_vacuo2, true);
                            }
                        }
                        if (!auto | !string.IsNullOrWhiteSpace(lastAlmMess))
                        {
                            return;
                        }
                        time.Add("真空判断时间：" + (timeGetTime() - PointTime).ToString());
                        Mstatus = "锁螺钉启动";
                        result = LockScrew(axis);
                        time.Add("锁螺钉时间：" + (timeGetTime() - PointTime).ToString());
                    }
                    else
                    {
                        result = -10;
                    }
                }
                else if (Parameter.Para[17] != "螺母测试")
                {
                    Mstatus = "拆螺钉启动";
                    result = UnlockScrew();
                    tab.WriteDO(Table.DO.CLD_vacuo, false);
                    tab.WriteDO(Table.DO.CLD_vacuo2, false);
                    time.Add("拆螺钉时间：" + (timeGetTime() - PointTime).ToString());
                }
                else
                {
                    Mstatus = "螺母测试启动";
                    result = NutCheck();
                    time.Add("螺母测试时间：" + (timeGetTime() - PointTime).ToString());
                }
                startNum = i + 1;
                if (!auto)
                {
                    return;
                }
                DxfShow.PointDraw tempPointDraw = new DxfShow.PointDraw();
                Mstatus = "结果显示中";
                if (table1Run)
                {
                    tempPointDraw = DxfShow.ElementList[i];
                }
                else if (table2Run)
                {
                    tempPointDraw = DxfShow.ElementList2[i];
                }
                else
                    tempPointDraw = DxfShow.ElementList[i];
                while (safetyLight)
                {
                    Mstatus = "光幕异常...";
                    almdata[1] = dataChange.SetBitValue(almdata[1], 5, true);
                    Thread.Sleep(200);
                }
                almdata[1] = dataChange.SetBitValue(almdata[1], 5, false);
                //    floatCheckData.Add(new float[]{x, y, Table.A_Status.CurrentLength});
                //    int n = floatCheckData.Count;
                //if(n>10)
                //{
                //    floatCheckData.RemoveAt(0);
                //    n--;
                //}
                //if(n>=10)
                //{
                //    float offset=float.Parse(Parameter.Para[57]);
                //    bool[] re=CoordinateChange.floatCheck(floatCheckData, offset);
                //    if(re[n-1]&result==0)
                //    {
                //        result = -3;
                //    }
                //}

                if (floatTeach)
                {
                    time.Add("浮钉学习开始时间：" + (timeGetTime() - PointTime).ToString());
                    Thread.Sleep(floatCheckTime);
                    if (table2Run)
                    {
                        if (axis == 0)
                            DataGrid2.Rows[i].Cells[7].Value = Table.A_Status.CurrentLength.ToString("F2");
                        else
                            DataGrid2.Rows[i].Cells[7].Value = Table.A_Status.CurrentLength2.ToString("F2");
                    }
                    else
                    {
                        if (axis == 0)
                            DataGrid.Rows[i].Cells[7].Value = Table.A_Status.CurrentLength.ToString("F2");
                        else
                            DataGrid.Rows[i].Cells[7].Value = Table.A_Status.CurrentLength2.ToString("F2");
                    }
                    time.Add("浮钉学习结束时间：" + (timeGetTime() - PointTime).ToString());
                }
                time.Add("异常判定开始时间：" + (timeGetTime() - PointTime).ToString());
                if (result != 0)
                {

                    Mstatus = "判断滑牙中";
                    if (result == -2)
                    {
                        workLooseNum++;
                        if (table2Run)
                        {
                            DataGrid2.Rows[i].Cells[6].Value = "滑牙";
                            DataGrid2.Rows[i].Cells[6].Style.BackColor = Color.Red;

                        }
                        else
                        {
                            
                            DataGrid.Rows[i].Cells[6].Value = "滑牙";
                            DataGrid.Rows[i].Cells[6].Style.BackColor = Color.Red;
                        }
                        Fault fat = new Fault();
                        fat.Point = (i + 1).ToString();
                        fat.FaultInfo = "滑牙";
                        afurl.faults.Add(fat);
                    }
                    Mstatus = "判断浮钉中";
                    if (result == -3)
                    {
                        workFloatNum++;
                        if (table2Run)
                        {
                            DataGrid2.Rows[i].Cells[6].Value = "浮钉";
                            DataGrid2.Rows[i].Cells[6].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            
                            DataGrid.Rows[i].Cells[6].Value = "浮钉";
                            DataGrid.Rows[i].Cells[6].Style.BackColor = Color.Red;
                        }
                        Fault fat = new Fault();
                        fat.Point = (i + 1).ToString();
                        fat.FaultInfo = "浮钉";
                        afurl.faults.Add(fat);

                    }
                    Mstatus = "判断螺母松动";
                    if (result == -6)
                    {
                        DataGrid.Rows[i].Cells[6].Value = "螺母松动";
                        DataGrid.Rows[i].Cells[6].Style.BackColor = Color.Red;
                    }
                    errorCount++;
                    allErrorCount++;
                    tempPointDraw.color = Color.Red;
                }
                else
                {
                   
                    if (table2Run)
                    {
                        DataGrid2.Rows[i].Cells[6].Value = "正常完成";
                        DataGrid2.Rows[i].Cells[6].Style.BackColor = Color.Lime;
                        tempPointDraw.color = Color.Green;
                        if (Parameter.Para[9] == "开启")
                        {
                            Thread.Sleep(floatCheckTime);
                            if (axis == 0)
                            {
                                currentLength = Table.A_Status.CurrentLength;
                            }
                            if (axis == 1)
                            {
                                currentLength = Table.A_Status.CurrentLength2;
                            }
                            float right = float.Parse(DataGrid2.Rows[i].Cells[7].Value.ToString());
                            float offset = float.Parse(Parameter.Para[57]);
                            float temp = Math.Abs(right - currentLength);
                            if (temp > offset)
                            {
                                DataGrid2.Rows[i].Cells[6].Value = "浮钉";
                                DataGrid2.Rows[i].Cells[6].Style.BackColor = Color.Red;
                                tempPointDraw.color = Color.Red;
                                errorCount++;
                            }
                            else
                            {
                                Mstatus = "正常完成";
                                errorCount = 0;
                            }
                        }else
                        {
                            Mstatus = "正常完成";
                            errorCount = 0;
                        }
                    }
                    else
                    {
                        DataGrid.Rows[i].Cells[6].Value = "正常完成";
                        DataGrid.Rows[i].Cells[6].Style.BackColor = Color.Lime;
                        tempPointDraw.color = Color.Green;
                        if (Parameter.Para[9] == "开启")
                        {
                            Thread.Sleep(floatCheckTime);
                            if (axis == 0)
                            {
                                currentLength = Table.A_Status.CurrentLength;
                            }
                            if (axis == 1)
                            {
                                currentLength = Table.A_Status.CurrentLength2;
                            }
                            float right = float.Parse(DataGrid.Rows[i].Cells[7].Value.ToString());
                            float offset = float.Parse(Parameter.Para[57]);
                            float temp = Math.Abs(right - currentLength);
                            if (temp > offset)
                            {
                                DataGrid.Rows[i].Cells[6].Value = "浮钉" + temp.ToString("f2");
                                DataGrid.Rows[i].Cells[6].Style.BackColor = Color.Red;
                                tempPointDraw.color = Color.Red;
                                errorCount++;
                            }else
                            {
                                Mstatus = "正常完成";
                                errorCount = 0;
                            }
                            
                        }else
                        {
                            Mstatus = "正常完成";
                            errorCount = 0;
                        }
                    }
                }
                time.Add("异常判断结束时间：" + (timeGetTime() - PointTime).ToString());
                allCount++;
                tempPointDraw.name = (i + 1).ToString();
                //Console.WriteLine("result={0}", result);
                ToolRun(false, false);
                Mstatus = "锁螺钉完成";
                showPic = true;
                //Thread th = new Thread((writeData(i,tempPointDraw));
                //th.Start();

                if (table1Run)
                {
                    DxfShow.ElementList.RemoveAt(i);
                    DxfShow.ElementList.Insert(i, tempPointDraw);
                    dxfparser.OutlinePic(DxfParser.DxfShow.outline, DxfShow.ElementList);
                    dxfparser.ShowPic(0);
                }
                else if (table2Run)
                {
                    DxfShow.ElementList2.RemoveAt(i);
                    DxfShow.ElementList2.Insert(i, tempPointDraw);
                    dxfparser.OutlinePic(DxfParser.DxfShow.outline2, DxfShow.ElementList2);
                    dxfparser.ShowPic(1);
                }
                else
                {
                    DxfShow.ElementList.RemoveAt(i);
                    DxfShow.ElementList.Insert(i, tempPointDraw);
                    dxfparser.OutlinePic(DxfParser.DxfShow.outline, DxfShow.ElementList);
                    dxfparser.ShowPic(0);
                }           
                if (errorCount >= int.Parse(Parameter.Para[56]))
                {
                    almdata[0] = dataChange.SetBitValue(almdata[0], 8, true);
                    Mstatus = "连续异常报警";
                }
                if (workMode != 0)
                {
                    tab.WriteDO(Table.DO.CLD_vacuo, false);
                    tab.WriteDO(Table.DO.CLD_vacuo2, false);
                }
                time.Add("绘图时间：" + (timeGetTime() - PointTime).ToString());
                //if (result == -2)
                if ((result == -2|result==-10) & !(Unscrew | Parameter.Para[17] == "复锁" | Parameter.Para[17] == "螺母测试"))
                {
                    ClearScrew = true;
                    
                    tab.WriteDO(Table.DO.CLD_Dianpi1, false);
                    tab.WriteDO(Table.DO.CLD_Dianpi2, false);
                    Thread.Sleep(200);
                    tab.WriteDO(Table.DO.CLD_Bizhang1, false);
                    tab.WriteDO(Table.DO.CLD_Bizhang2, false);
                    Thread.Sleep(200);
                   
                    tab.SafetyH = 0;
                    if (screwType == 0)
                    { tab.GotoSystemPoint(1); }
                    else
                    { tab.GotoSystemPoint(2); }
                    if (axis == 0)
                    {
                        tab.WriteDO(Table.DO.CLD_Dianpi1, true);
                        Thread.Sleep(500);
                        tab.WriteDO(Table.DO.CLD_Dianpi1, false);
                        Thread.Sleep(500);
                        if (result == -10)
                        {
                            bool value = tab.ReadDo(ThreeAxisTable.Table.DO.CLD_PushA1);
                            tab.WriteDO(ThreeAxisTable.Table.DO.CLD_PushA1, !value);
                            tab.WriteDO(ThreeAxisTable.Table.DO.CLD_PushA2, value);
                            Thread.Sleep(500);
                        }
                        tab.WriteDO(Table.DO.CLD_Dianpi1, true);
                        Thread.Sleep(500);
                        tab.WriteDO(Table.DO.CLD_Dianpi1, false);

                    }
                    else
                    {
                        tab.WriteDO(Table.DO.CLD_Dianpi2, false);
                        Thread.Sleep(500);
                        tab.WriteDO(Table.DO.CLD_Dianpi2, false);
                        Thread.Sleep(500);
                        if (result == -10)
                        {
                            bool value = tab.ReadDo(ThreeAxisTable.Table.DO.CLD_PushB1);
                            tab.WriteDO(ThreeAxisTable.Table.DO.CLD_PushB1, !value);
                            tab.WriteDO(ThreeAxisTable.Table.DO.CLD_PushB2, value);
                            Thread.Sleep(500);
                        }
                        tab.WriteDO(Table.DO.CLD_Dianpi2, true);
                        Thread.Sleep(500);
                        tab.WriteDO(Table.DO.CLD_Dianpi2, false);
                    }
                    hadScrew = false;
                    if (result == -10)
                    {
                        i--;
                    }

                    Thread.Sleep(200);
                    ClearScrew = false;
                    clearFlag = false;
                    ToolRun(false, false);
                }
                
                if (Unscrew | Parameter.Para[17] == "复锁")
                {
                    tab.SafetyH = z - float.Parse(Parameter.Para[175]);                  
                }else
                {
                    tab.WriteDO(Table.DO.CLD_Dianpi1, false);
                    tab.WriteDO(Table.DO.CLD_Dianpi2, false);
                }
                if (z - tab.SafetyH < 3)
                {
                    tab.GotoZ(0, 1, 0.9f);
                }
                else
                {
                    tab.GotoZ(tab.SafetyH, 1, 0.9f);
                }
                time.Add("Z轴提升时间：" + (timeGetTime() - PointTime).ToString());
                labSingleTim.Text = ((timeGetTime() - PointTime) / 1000).ToString("f1");
                if (timeStep)
                    listBox1.Items.AddRange(time.ToArray());
            }
                #endregion
                tab.WriteDO(Table.DO.CLD_Dianpi1, false);
                tab.WriteDO(Table.DO.CLD_Dianpi2, false);
                tab.SafetyH = 0;
                Mstatus = "生产完成后回到零点";
                Delay(500);
            if (Parameter.Para[0] == "双面线")
            { tab.GotoSystemPoint(3); }
            else
            {
                if(table1Run)
                tab.GotoPoint(0,0, 0, 0, tab.SpeedRate);
                else if(table2Run)
                    tab.GotoPoint(1, 0, 0, 0, tab.SpeedRate);
                else
                    tab.GotoPoint(0, 0, 0, 0, tab.SpeedRate);
            }
           
            if (Parameter.Para[16] == "完成自动回零")
            {
                if (table1Run)
                    tab.TableFindHome(1);
                else if (table2Run)
                    tab.TableFindHome(2);
                else
                    tab.TableFindHome(0);
                
            }

            databaseList[8] = DateTime.Now.ToString();
            startNum = 0;
            run = false;
            //if (startNum == 0 | (startNum == 2 & Parameter.Para[4] == "启用"))
            //{
            //    //if (Parameter.Para[22] == "启用")
            //    //{
            //    //    Mesh.WebApi.StopMesh();                 
            //    //}
            //}

            productCount++;
            productDone = 1;
            Mstatus = "正在放行产品";
            Delay(2000);
            productDone = 0;
            Mstatus = "产品完成";
            Thread.Sleep(300);
        }
        bool ClearScrew;
    
        private void bgwWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Mstatus = "生产数据统计开始"; 
            Thread.Sleep(500);
            cycle.Stop();
            ToolRun(false, false,false);
            ToolRun(false, false, true);
            tab.WriteDO(Table.DO.CLD_Dianpi1, false);
            tab.WriteDO(Table.DO.CLD_Dianpi2, false);
            databaseList[0] = Parameter.Para[50];
            databaseList[1] = Parameter.Para[51];
            databaseList[2] = "锁螺丝";
            databaseList[3] = cmbProgName.Text;
            
            databaseList[4] = txtSeriNum.Text;
            databaseList[5] = workPointNum.ToString();
            databaseList[6] = (workLooseNum + workFloatNum).ToString();
            //databaseList[7] = Parameter.Para[51];
            //databaseList[8] = Parameter.Para[51];
            databaseList[9] = "0" ;
            databaseList[10] = "未命名";
            databaseList[11] = "滑伢";
            databaseList[12] = "浮钉";
            databaseList[13] = "其他";
            if(workFloatNum==1)
            {
                workLooseNum=0;
            }
            databaseList[14] = workLooseNum.ToString();
            databaseList[15] = workFloatNum.ToString();
            databaseList[16] = "0";
            databaseList[17] = "";

            DBhelperData[3] = DateTime.Now.ToString();
            DBhelperData[4] = Parameter.Para[50];
            DBhelperData[5] = Parameter.Para[53];
            DBhelperData[6] = Parameter.Para[51];
            DBhelperData[7] = Parameter.Para[53];
            DBhelperData[8] = Parameter.Para[11];
            DBhelperData[9] = "0";
            DBhelperData[10] = "0";
            DBhelperData[11] = "0";
            DBhelperData[12] = "0";
            DBhelperData[13] = "0";

            string str = databaseList[0];
            for (int i = 1; i < 18; i++)
            {
                str += "," + databaseList[i];
            }
            List<string> data = new List<string>();
            for (int i = 0; i < 14; i++)
            {
                if (i == 0)
                {
                    data.Add(productCode);
                }
                else
                {
                    data.Add(DBhelperData[i]);
                }
            }

            tab.SafetyH = 0;
            //(0产线名称,1设备名称,2工艺名称,3产品名称,4产品条码,5任务点数,6异常点数,7开始时间,8结束时间,";
            //trpt += "1故障时间,10操作员工号,11异常名称1,12异常名称2,13异常名称3,14异常数1,15异常数2,16异常数3,17备注) values ";

            #region//线体
          
               if (!run)
               {
                   dataBase.WriteToDataBase(str);
                   //writeToSQL(Parameter.Para[2], data);
                   //writeToArtificialStation(DataGrid);
                   Mstatus = "线体数据写入完成";
                   Thread.Sleep(500);
                   if (auto & !!string.IsNullOrWhiteSpace(lastAlmMess))
                   {
                       
                   }
                   if (!auto | !string.IsNullOrWhiteSpace(lastAlmMess))
                   {
                       return;
                   }
                   Mstatus = "循环启动中";
                   bgwWork.RunWorkerAsync();
               }
            #endregion
           run = false;
           
        }
        int imageNum = 0;
        string pdcode;
        private void writeToArtificialStation(DataGridView dgv)
        {
             string tempPath = null;
            if(!Directory.Exists( "C:\\DeviceImage"))
            {
                Directory.CreateDirectory("C:\\DeviceImage");
            }
             if (string.IsNullOrEmpty(pdcode)|pdcode=="-1")
             {
                 imageNum = imageNum + 100;
                 if (imageNum > 1000)
                 {
                     imageNum = 100;
                 }
                 tempPath = "C:\\DeviceImage\\" + imageNum.ToString() + ".csv";
             }
             else
             {
                 tempPath = "C:\\DeviceImage\\" + pdcode + ".csv";
             }
             if (File.Exists(tempPath))
             {
                 File.Delete(tempPath);
             }
             File.Create(tempPath).Close();
             StreamWriter sw = new StreamWriter(tempPath);
             sw.WriteLine(dgv.Rows[0].Cells[0].Value.ToString() + "," + dgv.Rows[0].Cells[1].Value.ToString() + ",zero");
             for (int i = 1; i < dgv.RowCount;i++)
             {
                 string result ="正常完成";
                 if (dgv.Rows[i].Cells[6].Value!=null)
                 {
                     result=dgv.Rows[i].Cells[6].Value.ToString();
                 }
                 sw.WriteLine(dgv.Rows[i].Cells[0].Value.ToString() + "," + dgv.Rows[i].Cells[1].Value.ToString() + "," + result);
             }
             sw.Dispose();
             sw.Close();
            
        }

        private void writeToSQL(string mode,List<string> data)
        {
            string re="-1";
            if (mode == "MT")
            {
                re = dbwr.InsertData(data, DBHelper.DeviceType.CoverPlateScrewMountingEquipment);

            }
            else if (mode == "XH")
            {
                re = WR_ToSQL.InsertData(databaseList.ToList(), XHwritetoSQL.DeviceType.CoverPlateScrewMountingEquipment);
            }
            else if(mode=="DF")
            {
                afurl.sninfo.SN = txtSeriNum.Text;
                afurl.sninfo.PN = cmbProgName.Text;

                afurl.sninfo.WorkStation =WorkStation.Threading ;
             afurl.sninfo.Pass = Pass.Pass;
             afurl.sninfo.PlanCount = workPointNum;
             afurl.sninfo.FailCount = (workLooseNum + workFloatNum);
             afurl.sninfo.StopTime = afurl.rf.GetDBDateTime();
             
             afurl.sninfo.Site = "螺钉机";
             afurl.sninfo.UserName = Parameter.Para[53];
             afurl.SendData();
                if(afurl.res.Success)
                {
                    Mstatus = "数据上传完成！";
                }
                else
                {
                    Mstatus = "数据上传失败！";
                }
                afurl.faults.Clear();
                return;
            }
            else
                return;

             if (string.IsNullOrEmpty(re))
             {
                 Mstatus = "数据上传完成！";
             }
             else
             {
                 Mstatus = "数据上传失败！";
             }//listBox1.Items.Add(message.Text
            
        }
        private short LockScrew(int axis=0)
        {
            Mstatus = "低速启动中";
            //while (Table.safetyLight)
            //{
            //    ToolRun(false, false);
            //    Mstatus = "光幕异常...";
            //    almdata[1] = dataChange.SetBitValue(almdata[1], 5, true);
            //    Thread.Sleep(10);
            //}
            //almdata[1] = dataChange.SetBitValue(almdata[1], 5, false);
            ToolRun(true, false);
            if (axis == 0)
            {
                tab.WriteDO(Table.DO.CLD_Dianpi1, true);
            }else
            {
                tab.WriteDO(Table.DO.CLD_Dianpi2, true);
            }
            LockScrewDelay[0] = timeGetTime() + axisPara[6];
            while (Math.Abs(Table.A_Status.CurrentTorque) < axisPara[3]-1)
            {
                if(timeGetTime()>=LockScrewDelay[0])
                {
                    break;
                }
                //while(Table.safetyLight)
                //{
                //    ToolRun(false, false); 
                //    Mstatus = "光幕异常...";
                //    almdata[1]=dataChange.SetBitValue(almdata[1],5,true);
                //    Thread.Sleep(10);
                //}
                //almdata[1] = dataChange.SetBitValue(almdata[1], 5, false);
            }
           // Console.WriteLine("低速段时间={0}", LockScrewDelay[0] - timeGetTime());
            Mstatus = "高速运行中";
            ToolRun(false, true);
            LockScrewDelay[1] = timeGetTime() + axisPara[7];
            LockScrewDelay[3] = timeGetTime() + axisPara[11];
            int n = 0;
            do
            {
                if (Math.Abs(Table.A_Status.CurrentTorque) >= axisPara[4] - 1)
                {
                    n++;
                }
                else
                    n = 0;
                if (timeGetTime() > LockScrewDelay[1])
                {
                    return -2;
                }
                //while (safetyLight)
                //{
                //    ToolRun(false, false);
                //    Mstatus = "光幕异常...";
                //    almdata[1] = dataChange.SetBitValue(almdata[1], 5, true);
                //    Thread.Sleep(10);
                //}
                //almdata[1] = dataChange.SetBitValue(almdata[1], 5, false);
                Thread.Sleep(10);
            }
            while (n<2);
            
            while(timeGetTime()<LockScrewDelay[3])
            {
                //while (safetyLight)
                //{
                //    ToolRun(false, false);
                //    Mstatus = "光幕异常...";
                //    almdata[1] = dataChange.SetBitValue(almdata[1], 5, true);
                //    Thread.Sleep(10);
                //}
                //almdata[1] = dataChange.SetBitValue(almdata[1], 5, false);
                //力矩到最短拧螺钉时间未到,继续等待
                //Delay(10);
            }
           
           // Console.WriteLine("高速段时间={0}", LockScrewDelay[1] - timeGetTime());
            if (axisPara[5] != 0)
            {
                Mstatus = "力矩冲击中";
                ToolRun(true, true);
                LockScrewDelay[2] = timeGetTime() + axisPara[8];//力矩冲击时间
                //LockScrewDelay[2] = timeGetTime() ;
                while (Math.Abs(Table.A_Status.CurrentTorque) < axisPara[5]-1)
                {
                   if(timeGetTime()>LockScrewDelay[2])
                   {
                       return -4;
                   }
                    //while (safetyLight)
                    //{
                    //    ToolRun(false, false);
                    //    Mstatus = "光幕异常...";
                    //    almdata[1] = dataChange.SetBitValue(almdata[1], 5, true);
                    //    Thread.Sleep(10);
                    //}
                    //almdata[1] = dataChange.SetBitValue(almdata[1], 5, false);
                   //Delay(10);
                }

                //  Console.WriteLine("最后速段时间={0}", LockScrewDelay[2] - timeGetTime());              
            }          
            return 0;

        }
        private short UnlockScrew(int index=0)
        {
            int lowSpeedDelay = int.Parse(Parameter.Para[174]);
            int hightSpeedDelay = int.Parse(Parameter.Para[192]);
            float length = float.Parse(Parameter.Para[166]);
            float pitch = float.Parse(Parameter.Para[167]);
            float hightSpeed = float.Parse(Parameter.Para[172]);
            if(index==1)
            {
                length = float.Parse(Parameter.Para[168]);
                pitch = float.Parse(Parameter.Para[169]);
            }
            Mstatus = "低速启动中";
            ToolRun(true, false,true);
            lowSpeedDelay += timeGetTime();
            while ((Math.Abs(Table.A_Status.CurrentTorque) < axisPara[3] - 1) | Math.Abs(Table.A_Status.CurrentSpeed) > 5)
            {
                if (timeGetTime() >= lowSpeedDelay)
                {
                    break;
                }
                //while (safetyLight)
                //{
                //    ToolRun(false, false);
                //    Mstatus = "光幕异常...";
                //    almdata[1] = dataChange.SetBitValue(almdata[1], 5, true);
                //    Thread.Sleep(100);
                //}
                //almdata[1] = dataChange.SetBitValue(almdata[1], 5, false);
                Mstatus = "等待力矩稳定";  
            }
            Mstatus = "高速运行中";

            ToolRun(false, true,true);
            //Delay(500);
            hightSpeedDelay += timeGetTime();
            float upzpos = Table.axisStatus[2].CurrentPos - length;
            float upzspd = hightSpeed * pitch / 60;
            tab.GotoZwithMM(upzpos, upzspd, 1);
            ToolRun(false, false, true);
            if (Parameter.Para[102] == "带收集模式" | Parameter.Para[102] == "收集加提升")
            {
                tab.GotoSystemPoint(1);
                tab.WriteDO(Table.DO.CLD_vacuo, false);
                tab.WriteDO(Table.DO.CLD_Blow1, true);
                Thread.Sleep(200);
                if (Parameter.Para[102] == "收集加提升")
                {
                    
                    tab.WriteDO(Table.DO.CLD_Dianpi1, false);
                    Thread.Sleep(500);
                    tab.WriteDO(Table.DO.CLD_Dianpi1, true);
                }                
                tab.WriteDO(Table.DO.CLD_Blow1, false);
            }
            if (Parameter.Para[102] == "无收集加提升")
            {
               
                tab.WriteDO(Table.DO.CLD_vacuo, false);
                tab.WriteDO(Table.DO.CLD_Blow1, true);
                Thread.Sleep(200);
               
                    tab.WriteDO(Table.DO.CLD_Dianpi1, false);
                    Thread.Sleep(500);
                    tab.WriteDO(Table.DO.CLD_Dianpi1, true);
                tab.WriteDO(Table.DO.CLD_Blow1, false);
            }
            ToolRun(false, false, false);
            return 0;
            

        }

        private short NutCheck(int index = 0)
        {
            Mstatus = "螺母检测一段启动中";
            ToolRun(true, false,true);
            
            LockScrewDelay[0] = timeGetTime() + axisPara[6];
            while (Math.Abs(Table.A_Status.CurrentTorque) < axisPara[3] - 1)
            {
                if (timeGetTime() >= LockScrewDelay[0])
                {
                    break;
                }
                while (safetyLight)
                {
                    ToolRun(false, false);
                    Mstatus = "光幕异常...";
                    almdata[1] = dataChange.SetBitValue(almdata[1], 5, true);
                    Thread.Sleep(100);
                }
                almdata[1] = dataChange.SetBitValue(almdata[1], 5, false);
            }
            // Console.WriteLine("低速段时间={0}", LockScrewDelay[0] - timeGetTime());
            Mstatus = "螺母检测二段检测中";
            ToolRun(false, true,true);
            Thread.Sleep(500);
            LockScrewDelay[1] = timeGetTime() + axisPara[7];
            LockScrewDelay[3] = timeGetTime() + axisPara[11];

            if (Math.Abs(Table.A_Status.CurrentTorque) < axisPara[4] - 1)
            {
                return -6;
            }
               
            // Console.WriteLine("高速段时间={0}", LockScrewDelay[1] - timeGetTime());
            //if (axisPara[5] != 0)
            //{
            //    Mstatus = "力矩冲击中";
            //    ToolRun(true, true);
            //    LockScrewDelay[2] = timeGetTime() + axisPara[8];//力矩冲击时间
            //    //LockScrewDelay[2] = timeGetTime() ;
            //    while (Math.Abs(Table.A_Status.CurrentTorque) < axisPara[5] - 1)
            //    {
            //        if (timeGetTime() > LockScrewDelay[2])
            //        {
            //            return -4;
            //        }
            //    }

            //    //  Console.WriteLine("最后速段时间={0}", LockScrewDelay[2] - timeGetTime());              
            //}
            Mstatus = "螺母检测完成";
            return 0;
        }
        private void butStop_Click(object sender, EventArgs e)
        {
            getScrew1 = false;
            getScrew2 = false;
            bgwWork.CancelAsync();
        }

        private void butPause_Click(object sender, EventArgs e)
        {
            pause = !pause;
        }

        private void butReset_Click(object sender, EventArgs e)
        {
            alarmList.ResetFlag=true;
            reset = true;
            almdata = new short[20];
            alarmList.AckAlarm("");
            Delay(500);
            alarmList.ResetFlag = false;
            reset = false;
        }
        public static bool PassworkOK  = false; 
        private void timUIUpdata_Tick(object sender, EventArgs e)
        {
            timUIUpdata.Stop();
            lbl_CurIndex.Text = _curIndex.ToString();
            label137.Text = lxScannerOK.ToString();
            toolDate.Text = DateTime.Now.ToString();
            testMode = combbPara15.SelectedIndex;
            if (!feedwork.IsBusy)
            {
                tab.WriteDO(Table.DO.SDJ_Zhendongqi, false);
            }
            if(testMode==-1)
            {
                testMode = 0;
            }
            //if (tab.ReadDi(Table.DI.SDJ1_Type) & tab.ReadDi(Table.DI.SDJ1_Type2))
            //{
            //    if (tab.ReadDi(Table.DI.SDJ1_Type2, sdjpara[0].LengthCheckMode))
            //    {
            //        sdjpara[0].ErrorFlag = true;
            //    }
            //}
            //if (tab.ReadDi(Table.DI.SDJ2_Type))
            //{
            //    if (tab.ReadDi(Table.DI.SDJ2_Type2, sdjpara[1].LengthCheckMode))
            //    {
            //        sdjpara[1].ErrorFlag = true;
            //    }
            //}
            if (Parameter.Para[10] == "开启")
            {
                safetyLight = Table.safetyLight;
                if (safetyLight)
                {
                    Mstatus = "光幕异常...";
                }
                Table.SafeCheckOpen = true;
            }
            else
            {
                Table.SafeCheckOpen = false;
            }
            for(int i=0;i<lifetimes.Length;i++)
            {
                sysPara_txt[39 + i].Text = lifetimes[i].ToString();
            }
            if (tab.ReadDi(Table.DI.Start) & !bgwWork.IsBusy)
            {

                butStart_Click(null, null);

            }
            if(tab.ReadDi(Table.DI.StartTable1)&!bgwWork.IsBusy)
            {
                if (tab.ReadDi(Table.DI.Table1Get) | testMode != 0)
                {
                    table1Run = true;
                    cbb_TableSelect.SelectedIndex = 0;
                    table2Run = false;
                    butStart_Click(null, null);
                    //StartCheck();
                }
            }
            if (tab.ReadDi(Table.DI.StartTable2) & !bgwWork.IsBusy)
            {
                if (tab.ReadDi(Table.DI.Table2Get) | testMode != 0)
                {
                    table1Run = false;
                    cbb_TableSelect.SelectedIndex = 1;
                    table2Run = true;
                    butStart_Click(null, null);
                    //StartCheck();
                }
            }
            if(Parameter.Para[0]=="桌面双平台"&!tab.ReadDi(Table.DI.Pause))
            {
                getScrew1 = false;
                getScrew2 = false;
                bgwWork.CancelAsync();
            }
            almdata[0] = dataChange.SetBitValue(almdata[0], 13, !Table.TableHomeOK);
            toolStatus.Text = Mstatus;
            if(Table.AlmSafety)
            {
                Mstatus = "安全光幕异常！";
            }
            if (auto)
            {
                GB1.Enabled = false;
                GB2.Enabled = false;
                GB3.Enabled = false;
                GBpara5.Enabled = false;
                GBpara1.Enabled = false;
                GBpara2.Enabled = false;
                GBpara3.Enabled = false;
                GBpara4.Enabled = false;
                GBpara5.Enabled = false;
                GBpara6.Enabled = false;
                GBpara7.Enabled = false;
                GBpara8.Enabled = false;
                GBpara9.Enabled = false;
                GBpara10.Enabled = false;
                GBpara11.Enabled = false;
                butOpenDxf.Enabled = false;
                tab.Enabled = false;
                toolMode.Text="自动中!";
                //tab.Enabled = false;
            }else
            {
                toolMode.Text = "手动中";
                ClearScrew = false;
                if (PassworkOK)
                {
                    GBpara1.Enabled = true;
                    GBpara2.Enabled = true;
                    GBpara3.Enabled = true;
                    GBpara4.Enabled = true;
                    GBpara5.Enabled = true;
                    GBpara6.Enabled = true;
                    GBpara7.Enabled = true;
                    GBpara8.Enabled = true;
                    GBpara9.Enabled = true;
                    GBpara10.Enabled = true;
                    GBpara11.Enabled = true;
                }

                    GB1.Enabled = true;
                    GB2.Enabled = true;
                    GB3.Enabled = true;                
                    butOpenDxf.Enabled = true;
                    tab.Enabled = true;
                
            }
            if(Parameter.Para[0]=="桌面双平台")
            {
                auto = !pause;
            }else
                auto = tab.ReadDi(Table.DI.Auto) & tab.ReadDi(Table.DI.EStop);
            if(cbb_TableSelect.SelectedIndex == 0)
            {
                labWorkTotal.Text = "产品螺孔:" + DataGrid.RowCount;
            }
            if (cbb_TableSelect.SelectedIndex == 1)
            {
                labWorkTotal.Text = "产品螺孔:" + DataGrid2.RowCount;
            }
            if(Table.axisStatus[0].LimitP)
            {
                almdata[1] = dataChange.SetBitValue(almdata[1], 6, true);
            }
            if (Table.axisStatus[0].LimitN)
            {
                almdata[1] = dataChange.SetBitValue(almdata[1], 7, true);
            }
            if (Table.axisStatus[1].LimitP)
            {
                almdata[1] = dataChange.SetBitValue(almdata[1], 8, true);
            }
            if (Table.axisStatus[1].LimitN)
            {
                almdata[1] = dataChange.SetBitValue(almdata[1], 9, true);
            }
            if (Table.axisStatus[2].LimitP)
            {
                almdata[1] = dataChange.SetBitValue(almdata[1], 10, true);
            }
            if (Table.axisStatus[2].LimitN)
            {
                almdata[1] = dataChange.SetBitValue(almdata[1], 11, true);
            }
            if (Parameter.Para[0] != "无")
            {
                short[] temp;

                if (Parameter.Para[0] == "单面线")
                {
                    tab.WriteModBus_int(200, 1, new int[] { 1 });
                    temp = tab.ReadModBus_short(100, 4);
                    temp.CopyTo(LineStatusFromPLC, 0);
                    tab.WriteModBus_int(0, 3, dataChange.ShortArrayToIntArray(LineControlToPLC));
                }
                else if (Parameter.Para[0] == "双面线" | Parameter.Para[0] == "新线体")
                {
                    tab.WriteModBus_int(200, 1, new int[] { 0 });
                    LineStatusFromPLC = mw100;
                    mw0 = LineControlToPLC;
                    if(productDone==1)
                    {
                        mw0[5] = (short)productDone;
                    }
                }
                if(Parameter.Para[0] == "双面线")
                productReady = dataChange.GetBitValue(LineStatusFromPLC[0], 5);
                if (Parameter.Para[0] == "新线体")
                {
                    if (Parameter.Para[5] == "爱立信")
                    productReady = dataChange.GetBitValue(LineStatusFromPLC[0], 5);
                   
                }
                

                codeRequest = dataChange.GetBitValue(LineStatusFromPLC[0], 15);
                almdata[2] = LineStatusFromPLC[2];
                almdata[3] = LineStatusFromPLC[7];
                almdata[4] = LineStatusFromPLC[16];


                transport.UpdateInputData(ref LineStatusFromPLC);
                transport.UpdateOutputData(ref LineControlToPLC);
                if (Parameter.Para[0] == "双面线" | Parameter.Para[0] == "新线体")
                {

                    LineControlToPLC[1] = dataChange.SetBitValue(LineControlToPLC[1], 9, Table.Canturn);
                    LineControlToPLC[1] = dataChange.SetBitValue(LineControlToPLC[1], 10, turnGo);
                    LineControlToPLC[1] = dataChange.SetBitValue(LineControlToPLC[1], 11, turnBack);
                }
                LineControlToPLC[2] = dataChange.SetBitValue(LineControlToPLC[2], 5, auto);
                LineControlToPLC[2] = dataChange.SetBitValue(LineControlToPLC[2], 6, !tab.ReadDi(Table.DI.EStop));
                LineControlToPLC[2] = dataChange.SetBitValue(LineControlToPLC[2], 7, reset);
                if (!tab.ReadDi(Table.DI.EStop))
                {
                    ToolRun(false, false);

                }
                if (productDone == 1)
                {
                    LineControlToPLC[5] = (short)productDone;
                }



            }
            else
            {
                tab.WriteModBus_int(200, 1, new int[] { 0 });
            }           
           
                labProdPeriod.Text = (cycle.ElapsedMilliseconds / 1000).ToString();
                //labSingleTim.Text = ((timeGetTime() - PointTime) / 1000).ToString("f2");
            
            if(pause)
            {
                butPause.BackColor = Color.Lime;
            }else
            { butPause.BackColor = BackColor; }
            for (int i = 0; i < 3;i++ )
            {
                if(!Table.axisStatus[i].Ready)
                {
                    almdata[0] = dataChange.SetBitValue(almdata[0], (ushort)(i + 9), true);
                    
                }
            }
            //if(tab.ReadDi(Table.DI.ServoRdyA))
            //{
            //    ALM.AlmBoolList[12] = true;
            //}
            if (cmbManuFeedSel.SelectedIndex == 0)
            {

                butFeedOpera1.BackColor = tab.ReadDo(Table.DO.SDJ1_Blow) == true ? Color.Lime : BackColor;

                butFeedOpera2.BackColor = tab.ReadDo(Table.DO.SDJ1_Gouding) == true ? Color.Lime : BackColor;

                butFeedOpera3.BackColor = tab.ReadDo(Table.DO.SDJ1_Badong) == true ? Color.Lime : BackColor;
                labFeedIndi1.BackColor = tab.ReadDi(Table.DI.SDJ1_LL) == true ? Color.Lime : BackColor;
                labFeedIndi2.BackColor = tab.ReadDi(Table.DI.SDJ1_GD_END) == true ? Color.Lime : BackColor;
                labFeedIndi3.BackColor = tab.ReadDi(Table.DI.SDJ1_GD_ORG) == true ? Color.Lime : BackColor;
                labFeedIndi4.BackColor = tab.ReadDi(Table.DI.SDJ1_Type2,sdjpara[0].LengthCheckMode) == true ? Color.Lime : BackColor;

            }
            else
            {
                butFeedOpera1.BackColor = tab.ReadDo(Table.DO.SDJ2_Blow) == true ? Color.Lime : BackColor;
                butFeedOpera2.BackColor = tab.ReadDo(Table.DO.SDJ2_Gouding) == true ? Color.Lime : BackColor;
                butFeedOpera3.BackColor = tab.ReadDo(Table.DO.SDJ2_Badong) == true ? Color.Lime : BackColor;
                labFeedIndi1.BackColor = tab.ReadDi(Table.DI.SDJ2_LL) == true ? Color.Lime : BackColor;
                labFeedIndi2.BackColor = tab.ReadDi(Table.DI.SDJ2_GD_END) == true ? Color.Lime : BackColor;
                labFeedIndi3.BackColor = tab.ReadDi(Table.DI.SDJ2_GD_ORG) == true ? Color.Lime : BackColor;
                labFeedIndi4.BackColor = tab.ReadDi(Table.DI.SDJ2_Type2, sdjpara[1].LengthCheckMode) == true ? Color.Lime : BackColor;
            }
            if(songding1Step==1)
            {
                if(getScrew1)
                labStateFeedA.Text = "钩钉中！";
                if (getScrew2)
                    labStateFeedB.Text = "钩钉中！";
            }
            if (songding1Step == 2)
            {
                if (getScrew1)
                    labStateFeedA.Text = "吹钉中！";
                if (getScrew2)
                    labStateFeedB.Text = "吹钉中！";
            }
            if (songding1Step == 3)
            {
                if (getScrew1)
                    labStateFeedA.Text = "切钉中！";
                if (getScrew2)
                    labStateFeedB.Text = "切钉中！";
            }
            btnScrewMode.BackColor = Unscrew == true ?Color.LightGray: Color.Lime;
            btnScrewMode.Text = Unscrew == true ? "拆钉模式" : "锁定模式";
            btnFeedChange.BackColor = tab.ReadDo(Table.DO.SDJ_Change) == true ? Color.Lime : BackColor;
            button17.BackColor = tab.ReadDo(Table.DO.BaxisEnable) == true ? Color.Lime : BackColor;
            //butDusk.BackColor = tab.ReadDo(Table.DO.dustCollecting) == true ? Color.Lime : BackColor;
            if (cmbManuToolSel.SelectedIndex == 0)
            {
                //butUndoSilo.BackColor = tab.ReadDo(Table.DO.CLD_Caiding) == true ? Color.Lime : BackColor;
                butVacuumOnOff.BackColor = tab.ReadDo(Table.DO.CLD_vacuo) == true ? Color.Lime : BackColor;
                butToolBlowAir.BackColor = tab.ReadDo(Table.DO.CLD_Blow1) == true ? Color.Lime : BackColor;
                butToolMode1.BackColor = tab.ReadDo(Table.DO.CLD_Bizhang1) == true ? Color.Lime : BackColor;
                butPushScrewA.BackColor = tab.ReadDo(Table.DO.CLD_PushA1) == true ? Color.Lime : BackColor;
                butToolUpDn.BackColor = tab.ReadDo(Table.DO.CLD_Dianpi1) == true ? Color.Lime : BackColor;
                labLengthMA.Text = Table.A_Status.CurrentLength.ToString("F2") + "MM";

            }
            else
            {
                //butUndoSilo.BackColor = tab.ReadDo(Table.DO.CLD_Caiding) == true ? Color.Lime : BackColor;
                butVacuumOnOff.BackColor = tab.ReadDo(Table.DO.CLD_vacuo) == true ? Color.Lime : BackColor;
                butToolBlowAir.BackColor = tab.ReadDo(Table.DO.CLD_Blow2) == true ? Color.Lime : BackColor;
                butToolMode1.BackColor = tab.ReadDo(Table.DO.CLD_Bizhang2) == true ? Color.Lime : BackColor;
                butPushScrewA.BackColor = tab.ReadDo(Table.DO.CLD_PushB1) == true ? Color.Lime : BackColor;
                butToolUpDn.BackColor = tab.ReadDo(Table.DO.CLD_Dianpi2) == true ? Color.Lime : BackColor;
                labLengthMA.Text = Table.A_Status.CurrentLength2.ToString("F2") + "MM";
            }
            butUndoSilo.BackColor = tab.ReadDo(Table.DO.CaremaLift) == true ? Color.Lime : BackColor;
            if(Table.A_Status.CurrentTorque>2|Table.A_Status.CurrentSpeed>10)
            {
                labStatusMA.Text = "运行中...";
            }else
            {
                labStatusMA.Text = "停止中...";
            }
            label144.Text= labSpeedMA.Text = Table.A_Status.CurrentSpeed / 10 + "R/M";
            if (Parameter.Para[18] == "N.M")
            {
             label141.Text= labTorqueMA.Text = (Table.A_Status.CurrentTorque*1.27/100-float.Parse(Parameter.Para[93])).ToString("f2")+"N.M";
            }else
                label141.Text=labTorqueMA.Text = Table.A_Status.CurrentTorque + " %";

            
            btnCameraLight.BackColor = tab.ReadDo(Table.DO.CameraLight) == true ? Color.Lime : BackColor;
            
             
           
            if(tab.ReadDi(Table.DI.Reset))
            {
                butReset_Click(null, null);
            }
            if (!auto)
            {
                deviceStatus = 1;
            }else
            {
                if(dataChange.GetBitValue(LineControlToPLC[2],8))
                {
                    deviceStatus = 2;
                }else if(!productReady)
                {
                    deviceStatus = 6;
                }else
                {
                    deviceStatus = 4;
                }
            }

                timUIUpdata.Start();
        }
  
        private void button2_Click(object sender, EventArgs e)
        {
            int n;
            bool re = int.TryParse(txtStartNumFix.Text, out n);
            if (!re)
            MessageBox.Show("起点数据为非法格式!");
            if (n <= 0)
            {
                MessageBox.Show("起点数据不能小于0");
            return;
            }
            PointF temp=new PointF();
            int m = cbb_tablePointSet.SelectedIndex;
            if (m == 0)
            {
                
                temp.X = Points.points[n - 1].X - Table.axisStatus[0].CurrentPos;
                temp.Y = Points.points[n - 1].Y - Table.axisStatus[1].CurrentPos;
                for (int i = 0; i < Points.points.Count; i++)
                {

                    DataGrid.Rows[i].Cells[0].Value = Points.points[i].X - temp.X;
                    DataGrid.Rows[i].Cells[1].Value = Points.points[i].Y - temp.Y;
                    if (Points.points[i].ScrewType != 2)
                    {
                        DataGrid.Rows[i].Cells[2].Value = Table.axisStatus[2].CurrentPos;
                    }
                }
                DxfShow.OutLine[] tempOL = DxfShow.outline.ToArray();
                for (int i = 0; i < tempOL.Length; i++)
                {
                    tempOL[i].CenterX -= temp.X;
                    tempOL[i].CenterY -= temp.Y;
                    tempOL[i].StartX -= temp.X;
                    tempOL[i].StartY -= temp.Y;
                    tempOL[i].EndX -= temp.X;
                    tempOL[i].EndY -= temp.Y;
                }
                DxfShow.outline.Clear();
                DxfShow.outline.AddRange(tempOL);
                startPoint[0].index=n-1;
                startPoint[0].X=Table.axisStatus[0].CurrentPos;
                startPoint[0].Y=Table.axisStatus[1].CurrentPos;
                SaveProgFlie(0, cmbProgName.Text, DataGrid, DxfShow.outline);
                SavePointData(0);
            }
            if (m == 1)
            {
                temp.X = Points.points2[n - 1].X - Table.axisStatus[0].CurrentPos;
                temp.Y = Points.points2[n - 1].Y - Table.axisStatus[3].CurrentPos;
                for (int i = 0; i < Points.points2.Count; i++)
                {

                    DataGrid2.Rows[i].Cells[0].Value = Points.points2[i].X - temp.X;
                    DataGrid2.Rows[i].Cells[1].Value = Points.points2[i].Y - temp.Y;
                    if (Points.points2[i].ScrewType != 2)
                    {
                        DataGrid2.Rows[i].Cells[2].Value = Table.axisStatus[2].CurrentPos;
                    }
                }
                DxfShow.OutLine[] tempOL = DxfShow.outline.ToArray();
                for (int i = 0; i < tempOL.Length; i++)
                {
                    tempOL[i].CenterX -= temp.X;
                    tempOL[i].CenterY -= temp.Y;
                    tempOL[i].StartX -= temp.X;
                    tempOL[i].StartY -= temp.Y;
                    tempOL[i].EndX -= temp.X;
                    tempOL[i].EndY -= temp.Y;
                }
                DxfShow.outline2.Clear();
                DxfShow.outline2.AddRange(tempOL);
                startPoint[1].index=n-1;
                startPoint[1].X=Table.axisStatus[0].CurrentPos;
                startPoint[1].Y=Table.axisStatus[3].CurrentPos;
                SaveProgFlie(1, cmbProgName2.Text, DataGrid2, DxfShow.outline2);
                SavePointData(1);
            }
            if (m == 2)
            {
                 if (Parameter.Para[4] == "启用")
                     return;
                 endPoint[0].index = n - 1;
                 endPoint[0].X = Table.axisStatus[0].CurrentPos;
                 endPoint[0].Y = Table.axisStatus[1].CurrentPos;
                 PointF temporg = new PointF(startPoint[0].X, startPoint[0].Y);
                 PointF tempend = new PointF(endPoint[0].X, endPoint[0].Y);
                 PointF temppoint=new PointF(float.Parse(DataGrid.Rows[n-1].Cells[0].Value.ToString()),float.Parse(DataGrid.Rows[n-1].Cells[1].Value.ToString()));
                 double off= CoordinateChange.SetProductCoordinate(temporg, temppoint, temporg, tempend);
                label86.Text = off.ToString("f2");
                if (off>1.5)
                {
                    MessageBox.Show("示教两点间距与图纸间距大于1.5，请核对图纸后重新示教");
                    return;
                }
                 SaveProgFlie(0, cmbProgName.Text, DataGrid, DxfShow.outline);
                 SavePointData(0);
                 
            }
             if (m == 3)
             {
                 if (Parameter.Para[4] == "启用")
                     return;
                 endPoint[1].index = n - 1;
                 endPoint[1].X = Table.axisStatus[0].CurrentPos;
                 endPoint[1].Y = Table.axisStatus[3].CurrentPos;
                 PointF temporg = new PointF(startPoint[1].X, startPoint[1].Y);
                 PointF tempend = new PointF(endPoint[1].X, endPoint[1].Y);
                 PointF temppoint = new PointF(float.Parse(DataGrid2.Rows[n - 1].Cells[0].Value.ToString()), float.Parse(DataGrid2.Rows[n - 1].Cells[1].Value.ToString()));
                 CoordinateChange.SetProductCoordinate(temporg, temppoint, temporg, tempend);
                 SaveProgFlie(1, cmbProgName2.Text, DataGrid2, DxfShow.outline2);
                 SavePointData(1);
             }
            
        }

        private void cmbProgName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Parameter.Para[100] == "多产品")
            {
                if (cbb_TableSelect.SelectedIndex == 0)
                {
                    txtProdName.Text = cmbProgName.Text;
                    string file = Parameter.ProgFilePath + cmbProgName.Text + ".csv";
                    if (File.Exists(file))
                    {                     
                        LoadProgFile(file);
                        UpdataParaUI();
                        UpdateGrid(0,DataGrid);
                        SavePointData(0);
                        dxfparser.ratio = 1;
                        dxfparser.OutlinePic(DxfParser.DxfShow.outline, DxfShow.ElementList);
                        dxfparser.ShowPic(0);
                    }
                }
                if (cbb_TableSelect.SelectedIndex == 1)
                {
                    txtProdName.Text = cmbProgName2.Text;
                    string file = Parameter.ProgFilePath + cmbProgName2.Text + ".csv";
                    if (File.Exists(file))
                    {
                        LoadProgFile(file,1);
                        UpdataParaUI();
                        UpdateGrid(0,DataGrid2);
                        SavePointData(1);
                        dxfparser.ratio = 1;
                        dxfparser.OutlinePic(DxfParser.DxfShow.outline2, DxfShow.ElementList2);
                        dxfparser.ShowPic(1);
                    }
                }
            }else
            {
                if (cbb_TableSelect.SelectedIndex == 0)
                {
                    txtProdName.Text = cmbProgName.Text;
                }
                if (cbb_TableSelect.SelectedIndex == 1)
                {
                    txtProdName.Text = cmbProgName2.Text;
                }
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            float x = Points.points[0].X;
            float y = Points.points[0].Y;
            float z = Points.points[0].Z;
            int axis = Points.points[0].Axis;
            int screwType = Points.points[0].ScrewType;
            int workMode = Points.points[0].WorkMode;
            tab.SafetyH = 0;
            float[] cameraOffset = new float[2] { float.Parse(Parameter.Para[81]), float.Parse(Parameter.Para[82])};
            tab.WriteDO(Table.DO.CameraLight, true);
            tab.GotoPoint(0,x + cameraOffset[0], y + cameraOffset[1], z, tab.SpeedRate);
            Delay(int.Parse(Parameter.Para[99]));
            //Thread.Sleep();
            bool re = visionView.TakePhoto(0);
            int count = 0;
            Delay(200);
            while (!re & count < 3)
            {
                Thread.Sleep(200);
                re = visionView.TakePhoto(0);
                count++;
            }
            if (!re)
                return;
            Delay(200);
            Dictionary<string, object> result = new Dictionary<string, object>();
            visionView.GetResult(0, 0, result);
            //return;
            PointF visionPoint = new PointF();
            PointF photoPoint = new PointF(x + cameraOffset[0], y + cameraOffset[1]);
            PointF fristPoint = new PointF();

            visionPoint.X = float.Parse(result["X(mm)"].ToString());
            visionPoint.Y = float.Parse(result["Y(mm)"].ToString());
            visionView.TransformVisionCoordinateToMachineCoordinate(visionPoint, photoPoint, out fristPoint);
            PointF OrgPoint1 = new PointF(x, y);
            x = Points.points[1].X;
            y = Points.points[1].Y;
            z = Points.points[1].Z;
            axis = Points.points[1].Axis;
            screwType = Points.points[1].ScrewType;
            workMode = Points.points[1].WorkMode;
            PointF OrgPoint2 = new PointF(x, y);
            tab.GotoPoint(0,x + cameraOffset[0], y + cameraOffset[1], z, tab.SpeedRate);
            Delay(int.Parse(Parameter.Para[99]));
            //return;
            visionView.TakePhoto(1);
            Delay(200);
            visionView.GetResult(1, 0, result);
            visionPoint.X = float.Parse(result["X(mm)"].ToString());
            visionPoint.Y = float.Parse(result["Y(mm)"].ToString());
            PointF secondPoint = new PointF();
            photoPoint = new PointF(x + cameraOffset[0], y + cameraOffset[1]);
            visionView.TransformVisionCoordinateToMachineCoordinate(visionPoint, photoPoint, out secondPoint);
            CoordinateChange.SetProductCoordinate(OrgPoint1, OrgPoint2, fristPoint, secondPoint);
            tab.WriteDO(Table.DO.CameraLight, false);
            MessageBox.Show((fristPoint.X - OrgPoint1.X).ToString() + (fristPoint.Y - OrgPoint1.Y).ToString());          
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int n = int.Parse(txtCameraImage.Text);
            bool re = visionView.TakePhoto(n-1);
            int count = 0;
            Delay(100);
            float[] cameraOffset = new float[2] { float.Parse(Parameter.Para[81]), float.Parse(Parameter.Para[82]) };
            
            Dictionary<string, object> result = new Dictionary<string, object>();
            visionView.GetResult(n-1, 0, result);
            //return;
            PointF visionPoint = new PointF();
            PointF photoPoint = new PointF(Table.axisStatus[0].CurrentPos, Table.axisStatus[1].CurrentPos);
            PointF fristPoint = new PointF();

            if(result.Count==0)
            {
                MessageBox.Show("未找到图像特征!");
                return;
            }

            visionPoint.X = float.Parse(result["X(mm)"].ToString());
            visionPoint.Y = float.Parse(result["Y(mm)"].ToString());
            visionView.TransformVisionCoordinateToMachineCoordinate(visionPoint, photoPoint, out fristPoint);

            MessageBox.Show("x = "+fristPoint.X.ToString() + ", y =" + fristPoint.Y.ToString());
            if(Points.points[n-1].ScrewType!=2)
            {
                MessageBox.Show("测量点位不是相机拍照点!");
                return;
            }
            float x =fristPoint.X-Points.points[n-1].X;
            float y =Points.points[n-1].Y- fristPoint.Y;
            
            MessageBox.Show("差值x=" + x.ToString() + ",y=" + y.ToString());
            if(n==1)
            {
                if(DialogResult.Yes==MessageBox.Show("是否将偏移值保存至视觉参数？","提示",MessageBoxButtons.YesNo))
                {
                    visionView.CoordinateTransform.CameraParam.OffsetX +=x ;
                    visionView.CoordinateTransform.CameraParam.OffsetY +=y ;
                    txtParaP43.Text = visionView.CoordinateTransform.CameraParam.OffsetX.ToString("f2");
                    txtParaP44.Text = visionView.CoordinateTransform.CameraParam.OffsetY.ToString("f2");
                    Parameter.Para[193] = txtParaP43.Text;
                    Parameter.Para[194] = txtParaP44.Text;
                }
            }
        }
        bool productOKtest;
        private void button10_Click(object sender, EventArgs e)
        {
            productOKtest = true;
            Delay(500);
            productOKtest = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int n=int.Parse(testPointNum.Text)-1;
            if(n<2)
                return;
            tab.SafetyH = 0;
            PointF inPoint=new PointF(Points.points[n].X,Points.points[n].Y);
            PointF outPoint=new PointF();
            float z=Points.points[n].Z;
            CoordinateChange.GetProductCoordinate(inPoint, out outPoint);
            lbloffX.Text = (outPoint.X - inPoint.X).ToString("f2");
            lbloffY.Text = (outPoint.Y - inPoint.Y).ToString("f2");

            if (Points.points[n].Axis == 1)
            {
                outPoint.X += float.Parse(Parameter.Para[83]);
                outPoint.Y += float.Parse(Parameter.Para[84]);
            }
            tab.GotoPoint(0,outPoint.X, outPoint.Y, 0, tab.SpeedRate);
            if(DialogResult.Yes==MessageBox.Show("是否执行Z下降动作？","提示",MessageBoxButtons.YesNo))
            {
                tab.GotoZ(z, 0.3f, 1);
            }
        }

        private void butGoHhomeManu_Click(object sender, EventArgs e)
        {
            bool value = false;
          
                //value = tab.ReadDo(ThreeAxisTable.Table.DO.dustCollecting);
                //tab.WriteDO(ThreeAxisTable.Table.DO.dustCollecting, !value);

           
        }

        private void GB4_Enter(object sender, EventArgs e)
        {

        }

        private void butUndoMode_Click(object sender, EventArgs e)
        {
            KeyForm kf = new KeyForm();
            kf.ShowDialog();
        }


        private void btnFeedChange_Click(object sender, EventArgs e)
        {
            bool value = tab.ReadDo(Table.DO.SDJ_Change);
            tab.WriteDO(Table.DO.SDJ_Change, !value);     
        }
        private void button17_Click(object sender, EventArgs e)
        {
            axisPara[0] = 3000;
            axisPara[3] = 30;
            tab.WriteModBus_int(440, 6, axisPara);
            tab.WriteDO(Table.DO.CLD_Bizhang1,false);
            tab.WriteDO(Table.DO.CLD_Bizhang2, false);
            int[] temp = new int[1] { 1 };
            tab.WriteModBus_int(459, 1, temp);
            Delay(200);
            bool value = tab.ReadDo(Table.DO.BaxisEnable);
            ToolRun(true,false);
            Delay(500);
            tab.WriteDO(Table.DO.BaxisEnable, !value);
            Delay(300);
            ToolRun(false, false);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            productDone = 1;
            Delay(500);
            productDone = 0;
        }

        private void timScan_Tick(object sender, EventArgs e)
        {
            if (bgwWork.IsBusy)
            {
                if (!string.IsNullOrWhiteSpace(lastAlmMess) | safetyLight)
                {
                    bool value = tab.ReadDo(Table.DO.RedLamp);
                    tab.WriteDO(Table.DO.RedLamp, !value);
                    tab.WriteDO(Table.DO.GreenLamp, false);
                    tab.WriteDO(Table.DO.YellowLamp, false);
                }
                else
                {
                    tab.WriteDO(Table.DO.GreenLamp, true);
                    tab.WriteDO(Table.DO.RedLamp, false);
                    tab.WriteDO(Table.DO.YellowLamp, false);
                }
            }
            else
            {
                if (auto)
                {
                    if (!string.IsNullOrWhiteSpace(lastAlmMess) | safetyLight)
                    {
                        tab.WriteDO(Table.DO.RedLamp, true);
                        tab.WriteDO(Table.DO.GreenLamp, false);
                        tab.WriteDO(Table.DO.YellowLamp, false);
                    }
                    else
                    {
                        bool value = tab.ReadDo(Table.DO.GreenLamp);
                        tab.WriteDO(Table.DO.GreenLamp, !value);
                        tab.WriteDO(Table.DO.YellowLamp, false);
                        tab.WriteDO(Table.DO.RedLamp, false);
                    }
                }else
                {
                    tab.WriteDO(Table.DO.YellowLamp, true);
                    tab.WriteDO(Table.DO.RedLamp, false);
                    tab.WriteDO(Table.DO.GreenLamp, false);
                }
            }
            bool re = tab.ReadDo(Table.DO.RedLamp);
            tab.WriteDO(Table.DO.Buzzer, re);

            if (!string.IsNullOrWhiteSpace(lastAlmMess))
            {
                toolError.Text = lastAlmMess;
            }
            else
            { toolError.Text = ""; }

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbb_TableSelect.SelectedIndex==0)
            {
                DataGrid2.Visible = false;
                DataGrid.Visible = true;
                cmbProgName2.Visible = false;
                cmbProgName.Visible = true;
                txtProdName.Text = cmbProgName.Text;
            }
            if (cbb_TableSelect.SelectedIndex == 1)
            {
                DataGrid.Visible = false;
                DataGrid2.Visible = true;
                cmbProgName.Visible = false;
                cmbProgName2.Visible = true;
                cmbProgName2.Location = cmbProgName.Location;
                DataGrid2.Location = DataGrid.Location;
                txtProdName.Text = cmbProgName2.Text;
            }
        }

        private void butScrewMode_Click(object sender, EventArgs e)
        {
            if (auto)
                return;
            if(Unscrew)
            { Unscrew = false; }
            else
            {
                Unscrew = true;
            }

        }

        private void butRevRotMA_MouseDown(object sender, MouseEventArgs e)
        {
            int n = cmbManuToolSel.SelectedIndex;
            if (ckbTriJog.Checked)
            {
                ToolRun(true, true,true);
            }
            else
            ToolRun(false, true,true);
        }

        private void butRevRotMA_MouseUp(object sender, MouseEventArgs e)
        {

            ToolRun(false, false, false);
        }

        private void LifeTimeReset(object sender, EventArgs e)
        {
            int n =int.Parse(((Button)sender).Tag.ToString());
            lifetimes[n] = 0;
        }

        private void btnCameraLight_Click(object sender, EventArgs e)
        {
             bool value = tab.ReadDo(ThreeAxisTable.Table.DO.CameraLight);
            tab.WriteDO(ThreeAxisTable.Table.DO.CameraLight, !value);
        }

        private void butFwdRotMA_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            bool value = tab.ReadDo(ThreeAxisTable.Table.DO.CLD_PushB1);
            tab.WriteDO(ThreeAxisTable.Table.DO.CLD_PushB1, !value);
        }

        private void btnJiaju_Click(object sender, EventArgs e)
        {
            bool value = tab.ReadDo(ThreeAxisTable.Table.DO.CLD_PushB2);
            tab.WriteDO(ThreeAxisTable.Table.DO.CLD_PushB2, !value);
        }

        public short[] redata2 = new short[5];
        public short[] wrdata = new short[5];
        private bool licenseCheck()
        {
            return true;
            if (GetPCmessage.MacAddress == "A4:02:B9:7F:83:AC" | GetPCmessage.MacAddress == "C8:5B:76:2E:B9:9E")
            {
                return true;
            }           
            float[] redata=new float[5];
            int re1=tab.ReadFlash(0,5,ref redata);
            
            float[] wrdata2=new float[5];
            for(int i=0;i<5;i++)
            {
                redata2[i]=(short)redata[i];
            }
            int re= lic.LicencePass(patchnum, Frm_welcome.ver, redata2, ref wrdata);
            if (re < 0)
            {
                if(DialogResult.OK==MessageBox.Show("设备未授权或授权到期，是否输入授权码？","授权管理",MessageBoxButtons.OKCancel))
                {
                    LicenceSet ls = new LicenceSet();
                    ls.ShowDialog(this);
                    if(!ls.check)
                    {
                        return false;
                    }

                }else
                {
                    return false;
                }
            }else if(re<7)
            {
                if (DialogResult.OK == MessageBox.Show("设备剩余有效期"+re.ToString() +"，是否输入授权码？", "授权管理", MessageBoxButtons.OKCancel))
                {
                    LicenceSet ls = new LicenceSet();
                    ls.ShowDialog(this);
                   
                }
                return true;
            }

            for (int i = 0; i < 5;i++ )
            {
                wrdata2[i] = wrdata[i];
            }
                tab.WriteFlash(0, 5, wrdata2);
                return true;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            float[] data = new float[] { 15,14,13,12,11 };
            
            int re0 = tab.WriteFlash(0, 5, data);
            //int re1 = tab.WriteFlash(1, 1, data1);
            //int re2 = tab.WriteFlash(2, 1, data2);
            //int re3 = tab.WriteFlash(3, 1, data3);
            //int re4 = tab.WriteFlash(4, 1, data4);
            //MessageBox.Show(re.ToString());
        }

        private void button20_Click(object sender, EventArgs e)
        {
            float[] data = new float[5] ;
            int re0 = tab.ReadFlash(0, 5, ref data);
           
           // MessageBox.Show(re.ToString());
        }

        private void toolError_Click(object sender, EventArgs e)
        {

        }

        private void combbPara10_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void GBpara8_Enter(object sender, EventArgs e)
        {

        }
        bool timeStep = false;
        private void button22_Click(object sender, EventArgs e)
        {          
               timeStep = !timeStep;
               listBox1.Visible = timeStep;
            if(listBox1.Visible==false)
            {
                listBox1.Items.Clear();
            }
        }

        private void combbPara18_TextChanged(object sender, EventArgs e)
        {
            if(commboxSectionIndex==-1)
            {
                commboxSectionIndex = combbPara18.SelectedIndex;
                return;
            }
            if(combbPara18.SelectedIndex==commboxSectionIndex)
            {
                return;
            }
            DialogResult re = MessageBox.Show("检测到系统正在更改力矩单位，是否转换数值？", "", MessageBoxButtons.YesNo);
            if (re == DialogResult.Yes)
            {
                if (combbPara18.SelectedIndex == 0)
                {
                    float value = float.Parse(Parameter.Para[154]);
                    if (value < 4)
                    {
                        value = float.Parse(Parameter.Para[151]);
                        txtParaP1.Text = (value * 100 / 1.27).ToString("f0");
                        value = float.Parse(Parameter.Para[154]);
                        txtParaP4.Text = (value * 100 / 1.27).ToString("f0");
                        value = float.Parse(Parameter.Para[158]);
                        txtParaP8.Text = (value * 100 / 1.27).ToString("f0");
                        value = float.Parse(Parameter.Para[177]);
                        txtParaP27.Text = (value * 100 / 1.27).ToString("f0");
                        value = float.Parse(Parameter.Para[180]);
                        txtParaP30.Text = (value * 100 / 1.27).ToString("f0");
                        value = float.Parse(Parameter.Para[184]);
                        txtParaP34.Text = (value * 100 / 1.27).ToString("f0");

                    }
                }
                if (combbPara18.SelectedIndex == 1)
                {
                    float value = float.Parse(Parameter.Para[154]);
                    if (value > 4)
                    {
                        value = float.Parse(Parameter.Para[151]);
                        txtParaP1.Text = (value * 1.27 / 100).ToString("f2");
                        value = float.Parse(Parameter.Para[154]);
                        txtParaP4.Text = (value * 1.27 / 100).ToString("f2");
                        value = float.Parse(Parameter.Para[158]);
                        txtParaP8.Text = (value * 1.27 / 100).ToString("f2");
                        value = float.Parse(Parameter.Para[177]);
                        txtParaP27.Text = (value * 1.27 / 100).ToString("f2");
                        value = float.Parse(Parameter.Para[180]);
                        txtParaP30.Text = (value * 1.27 / 100).ToString("f2");
                        value = float.Parse(Parameter.Para[184]);
                        txtParaP34.Text = (value * 1.27 / 100).ToString("f2");

                    }
                }
               
                MessageBox.Show("数据装换完成，请确认参数后，及时保存参数！");
            }
            commboxSectionIndex = combbPara18.SelectedIndex;
        }

        private void combbPara18_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DataGrid2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            int row = e.RowIndex;
            int Column = e.ColumnIndex;
            DataGrid2.Rows[row].Cells[Column].Value = null;
        }

        private void DataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            int row = e.RowIndex;
            int Column = e.ColumnIndex;
            DataGrid.Rows[row].Cells[Column].Value = null;
        }

 

        private void cbb_tablePointSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbb_tablePointSet.SelectedIndex)
            {
                case 0:
                    txtTableAdjustX.Text = startPoint[0].X.ToString();
                    txtTableAdjustY.Text = startPoint[0].Y.ToString();
                    break;
                case 1:
                    txtTableAdjustX.Text = startPoint[1].X.ToString();
                    txtTableAdjustY.Text = startPoint[1].Y.ToString();
                    break;
                case 2:
                    txtTableAdjustX.Text = endPoint[0].X.ToString();
                    txtTableAdjustY.Text = endPoint[0].Y.ToString();
                    break;
                case 3:
                    txtTableAdjustX.Text = endPoint[0].X.ToString();
                    txtTableAdjustY.Text = endPoint[0].Y.ToString();
                    break;
                default:
                    break;
            }
        }

 

 



        private void button23_Click(object sender, EventArgs e)
        {
          writeToSQL(Parameter.Para[2], databaseList.ToList());
        }

  

        private void button24_Click(object sender, EventArgs e)
        {
            if(!auto)
            {
                if (DialogResult.Yes == MessageBox.Show("确定移动到扫码点?", "提示", MessageBoxButtons.YesNo))
                {
                    tab.GotoSystemPoint(4);
                } 
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            Mesh.WebApi.ScannerPort = ushort.Parse(Parameter.Para[7].Remove(0, 3));
            Mesh.WebApi.ScannerBaud = 115200;
            Mesh.WebApi.ScannerDelay = int.Parse(Parameter.Para[97]);
            MessageBox.Show( Mesh.WebApi.ManualReadCode());
        }

        private void label66_Click(object sender, EventArgs e)
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {
            afurl.sninfo.StartTime = afurl.rf.GetDBDateTime();
            afurl.sninfo.StopTime = afurl.rf.GetDBDateTime();
            writeToSQL(Parameter.Para[2], databaseList.ToList());
            MessageBox.Show(afurl.res.Message);
        }

        private void bgwClear_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void DataGrid_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void DataGrid_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Clicks < 2) && (e.Button == MouseButtons.Left))
            {
                if ((e.ColumnIndex == -1) && (e.RowIndex > 1))
                    DataGrid.DoDragDrop(DataGrid.Rows[e.RowIndex], DragDropEffects.Move);
            }


        }
        int selectionIdx;
        private void DataGrid_DragDrop(object sender, DragEventArgs e)
        {
            int idx = GetRowFromPoint(e.X, e.Y);

            if (idx < 0) return;

            if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
            {
                DataGridViewRow row = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));
                DataGrid.Rows.Remove(row);
                selectionIdx = idx;
                DataGrid.Rows.Insert(idx, row);
            }

        }
        private int GetRowFromPoint(int x, int y)
        {
            for (int i = 0; i < DataGrid.RowCount; i++)
            {
                Rectangle rec = DataGrid.GetRowDisplayRectangle(i, false);

                if (DataGrid.RectangleToScreen(rec).Contains(x, y))
                    return i;
            }
            return -1;
        }

          private void DataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
          {
            if (selectionIdx > -1&&selectionIdx<DataGrid.RowCount)
            {
                DataGrid.Rows[selectionIdx].Selected = true;
                DataGrid.CurrentCell = DataGrid.Rows[selectionIdx].Cells[0];
            }
          }

          private void button26_Click(object sender, EventArgs e)
          {
              if (!auto)
              {
                  if (DialogResult.Yes == MessageBox.Show("确定移动到扫码点?", "提示", MessageBoxButtons.YesNo))
                  {
                      tab.GotoSystemPoint(0);
                  }
              }
          }

          private void button28_Click(object sender, EventArgs e)
          {
              int n = cmbManuToolSel.SelectedIndex;
              AxisParaUpdata(n);
              if (ckbTriJog.Checked)
              {
                  ToolRun(true, true);
              }
              else
                  ToolRun(false, true);
              Delay(5000);
              ToolRun(false, false);
   
          }

          private void button27_Click(object sender, EventArgs e)
          {
              int n = cmbManuToolSel.SelectedIndex;
              AxisParaUpdata(n);
              if (ckbTriJog.Checked)
              {
                  ToolRun(true, true,true);
              }
              else
                  ToolRun(false, true,true);
              Delay(500);
              ToolRun(false, false);
             
          }

          private void txtManuCode_KeyPress(object sender, KeyPressEventArgs e)
          {
              if (e.KeyChar == (char)13)
              {
                  if (Parameter.Para[22] == "手动扫码")
                  {
                      txtSeriNum.Text = txtManuCode.Text;
                      pdcode = txtManuCode.Text;
                  }
                  if (Parameter.Para[22] == "爱立信扫码")
                  {
                      txtSeriNum.Text = txtManuCode.Text;
                      string[] temp = txtManuCode.Text.Split('S');
                      if (temp.Length > 0)
                      {
                          pdcode = temp[temp.Length - 1];
                          byte[] btemp = Encoding.ASCII.GetBytes(pdcode);
                          short[] stemp = dataChange.toShortArray(btemp);
                          //for(int i=0;i<btemp.Length;i++)
                          //{
                          //    WriteToSiemens[i] = btemp[i];
                          //    if(i>=20)
                          //    {
                          //        break;
                          //    }
                          //}
                          WriteToSiemens[20] = 1;
                      }
                    

                  }
              }
          }

          private void grpButtonArea_Enter(object sender, EventArgs e)
          {

          }

          private void butGoto2_Click(object sender, EventArgs e)
          {

          }

          private void button29_Click(object sender, EventArgs e)
          {
               if(DialogResult.OK==MessageBox.Show("清楚授权会导致设备无法使用，是否继续？","授权管理",MessageBoxButtons.OKCancel))
               {
                   float[]data = new float[5];
                   tab.WriteFlash(0, 5, data);
               }
          }

          private void GB1_Enter(object sender, EventArgs e)
          {

          }

          private void btnSysReset_Click(object sender, EventArgs e)
          {
              tab.WriteDO(Table.DO.CLD_Dianpi1, false);
              tab.WriteDO(Table.DO.CLD_Dianpi2, false);
              tab.WriteDO(Table.DO.CLD_Blow1, false);
              tab.WriteDO(Table.DO.CLD_Blow2, false);
              tab.TableFindHome();
              alarmList.ResetFlag = true;
              reset = true;
              almdata = new short[20];
              alarmList.AckAlarm("");
              Delay(300);
              alarmList.ResetFlag = false;
              reset = false;

          }

          private void button21_Click(object sender, EventArgs e)
          {

          }

          private void cmbManuToolMode_SelectedIndexChanged(object sender, EventArgs e)
          {

          }

          private void label150_Click(object sender, EventArgs e)
          {

          }

          private void bgwReset_DoWork(object sender, DoWorkEventArgs e)
          {

          }

          private void TorqueTest_DoWork(object sender, DoWorkEventArgs e)
          {

          }

         

        


    }
}
