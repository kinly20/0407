using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using cszmcaux;
using ThreeAxisTable.tools;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace ThreeAxisTable
{
    public partial class Table: UserControl
    {
        IntPtr mHandle;
        public bool Auto;
        public float SpeedRate=0.1f;
        public static string tableConfigFile= @"D:\NewAutoScrew\TableConfig.ini";
        public float SafetyH = 0;
        public bool Estop;
        public CheckBox[] Input = new CheckBox[64];
         public CheckBox[] Output = new CheckBox[64];
        //public float SecondSafetyH = 30;
        [DllImport("winmm.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int timeGetTime();
        public static bool Canturn = false, safetyLight=false ,AlmSafety=false;
        public struct AxisParameter
        {
            public int MaxSpeed ;
            public int RotatePulse;
            public float AccTime ;
            public float DecTime;
            public float mm_r;
            public float SoftLimitN ;
            public float SoftLimitP;
            public int PulseDir;
            public int HomeWithZP;
            public int Plimit;
            public int Nlimit;
            public int Orgflag;
            public int Sramp;
        }
        public struct AxisStatus
        {
            public bool Busing;
            public bool LimitP;
            public bool LimitN;
            public bool Org;
            public float CurrentPos ;
            public bool Ready ;
            public float CurrentTorque;
            public float CurrentSpeed;
            public int Code;
            public float CurrentLength;
            public float CurrentLength2;
        }
        public static AxisStatus A_Status;
        public struct Position
        {
            public float X;
            public float Y;
            public float Z;
            public float R;
        }
        public enum DI
        {
            StartTable1=0,
            XLimitP = 1,
            XLimitN = 2,
            XOrg = 3,
            StartTable2=4,
            YLimitP = 5,
            YLimitN = 6,
            YOrg = 7,
            Pause=8,
            ZLimitP = 9,
            ZLimitN = 10,
            ZOrg = 11,
            Start=12,
            EStop=14,
            DianpiORG=15,
            CLD_TuidingORG=16,
            CLD_TuidingEND=17,
            Reset=18,
            Y2Org=19,
            Auto=20,
            Y2LimitP=21,
            Y2LimitN = 22,
            SafetyLight=23,
            ServoRdyX = 24,
            ServoRdyY = 25,
            ServoRdyZ = 26,
            CLD_Tuiding2ORG = 32,
            CLD_Tuiding2END = 33,
            ServoRdyA = 34,
            Dianpi2ORG = 35,
            SDJ1_CK=36,
            SDJ1_LL=37,
            SDJ1_GD_END=38,
            SDJ1_GD_ORG=39,
            SDJ1_Type=40,//螺钉长度确认
            SDJ2_CK = 41,
            SDJ2_LL = 42,
            SDJ2_GD_END = 43,
            SDJ2_GD_ORG = 44,
            Table1Get=45,//桌面试螺钉机启动1
            SDJ2_Type=45,//
            Table2Get=46,//桌面试螺钉机启动2
            SDJ1_Type2 = 46,//1#螺钉长度确认2
            SDJ2_Type2 = 47//2#螺钉长度确认2
            


        }
        public enum DO
        {
            ServoOnA = 0,
            ServoDirA=1,
            RunLamp1 = 2,
            //dustCollecting=6,
            ServoRst = 3,
            BaxisEnable=4,
            CaremaLift=5,
            RunLamp2=6,
            RunAlm = 7,
            ServoOnX = 8,
            ServoOnY = 9,
            ServoOnZ = 10,
            ServoOnY2 = 11,
        
            ServoA_Speed0=32,
            ServoA_Speed1 = 33,
            ServoA_Torque0 = 34,
            ServoA_Torque1 = 35,
            CLD_Bizhang1=36,
            CLD_Dianpi1=37,
            CLD_Dianpi2 = 38,
            CLD_vacuo=39,
            GreenLamp=40,
            YellowLamp=41,
            RedLamp=42,
            CLD_PushA1=43,
            CLD_Bizhang2 = 44,
            CLD_Blow1=45,
            CLD_PushA2=46,
            Buzzer=47,
            SDJ1_Badong=48,
            SDJ1_Gouding=49,
            SDJ1_Blow=50,
            SDJ_Zhendongqi=51,
            SDJ2_Badong = 52,
            SDJ2_Gouding = 53,
            SDJ2_Blow = 54,
            SDJ_Change=55,
            CLD_PushB1 = 56,
            SDJ1_BlowStop=56,
            CLD_PushB2 = 57,
            CLD_vacuo2=58,
            CLD_Blow2 = 59,
            CLD_Caiding=60,
            SDJ1_GuntongF=61,
            SDJ2_GuntongF = 62,
            CameraLight=63
        }
        public static AxisParameter[] axisParameter=new AxisParameter[4];
        public static AxisStatus[] axisStatus = new AxisStatus[4];
        public static Position[] ApplyPoint = new Position[6];
        public string SongdingjiMode ="组合双送钉机";
        private System.Timers.Timer getCardStatus = new System.Timers.Timer(80);
        
        string PathConfig = Application.StartupPath + "\\TableConfig.ini";
        string IP;
        public static bool TableHomeOK=false;
        public ushort UsingAxis;
        public int tableMode = 0;
        public string GetScrewMode = "滚筒";
        //public int GetScrewMode=0;
        float homeHightSpeed = 0.1f;
        public bool luoliaocheck1, luoliaocheck2;
        int Baidong1Step = 0,Baidong2Step=0,Guntong1Step=0,Guntong2Step=0;
        int[] baidong1Delay = new int[5], baidong2Delay = new int[5];
        int license = 1;
        public static bool SafeCheckOpen;
        private float measureRangeA, measureRangeB;
        public Table()
        {
            InitializeComponent();
        }
       
        private void LoadTableConfig(string ConfigPath = @"D:\NewAutoScrew\TableConfig.ini")//加载配置参数
        {
            Ini.IniFile ini = new Ini.IniFile(ConfigPath);
            IP=ini.IniReadValue("Card", "IP");
            UsingAxis =ushort.Parse(ini.IniReadValue("Card", "UsingAxis"));
            
            for(int i=0;i<4;i++)
            {
                axisParameter[i].MaxSpeed = int.Parse(ini.IniReadValue(i.ToString(), "MaxSpeed"));
                axisParameter[i].RotatePulse = int.Parse(ini.IniReadValue(i.ToString(), "RotatePulse"));
                axisParameter[i].AccTime = float.Parse(ini.IniReadValue(i.ToString(), "AccTime"));
                axisParameter[i].DecTime = float.Parse(ini.IniReadValue(i.ToString(), "DecTime"));
                axisParameter[i].mm_r = float.Parse(ini.IniReadValue(i.ToString(), "PulseUnit"));
                axisParameter[i].SoftLimitN = float.Parse(ini.IniReadValue(i.ToString(), "SoftLimitN"));
                axisParameter[i].SoftLimitP = float.Parse(ini.IniReadValue(i.ToString(), "SoftLimitP"));
                axisParameter[i].PulseDir = int.Parse(ini.IniReadValue(i.ToString(), "PulseDir"));
                axisParameter[i].HomeWithZP = int.Parse(ini.IniReadValue(i.ToString(), "HomeWithZP"));
                axisParameter[i].Orgflag = int.Parse(ini.IniReadValue(i.ToString(), "Orgflag"));
                axisParameter[i].Plimit=int.Parse(ini.IniReadValue(i.ToString(), "Plimit"));
                axisParameter[i].Nlimit=int.Parse(ini.IniReadValue(i.ToString(), "Nlimit"));
                axisParameter[i].Sramp = int.Parse(ini.IniReadValue(i.ToString(), "Sramp"));
            
            }
            measureRangeA = float.Parse(ini.IniReadValue("A", "MeasureRange"));
            measureRangeB = float.Parse(ini.IniReadValue("B", "MeasureRange"));
            if (measureRangeA==0)
            {
                measureRangeA = 50;
            }
            if (measureRangeB == 0)
            {
                measureRangeB = 50;
            }
            
            homeHightSpeed= float.Parse(ini.IniReadValue("home", "HSpeed"));
            if(homeHightSpeed<=0.02)
            {
                homeHightSpeed = 0.1f;
            }

            ApplyPoint[0].X = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[0].ToString(), "X"));
            ApplyPoint[0].Y = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[0].ToString(), "Y"));
            ApplyPoint[0].Z = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[0].ToString(), "Z"));
            ApplyPoint[0].R = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[0].ToString(), "R"));
            ApplyPoint[1].X = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[1].ToString(), "X"));
            ApplyPoint[1].Y = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[1].ToString(), "Y"));
            ApplyPoint[1].Z = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[1].ToString(), "Z"));
            ApplyPoint[1].R = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[1].ToString(), "R"));
            ApplyPoint[2].X = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[2].ToString(), "X"));
            ApplyPoint[2].Y = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[2].ToString(), "Y"));
            ApplyPoint[2].Z = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[2].ToString(), "Z"));
            ApplyPoint[2].R = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[2].ToString(), "R"));
            ApplyPoint[3].X = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[3].ToString(), "X"));
            ApplyPoint[3].Y = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[3].ToString(), "Y"));
            ApplyPoint[3].Z = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[3].ToString(), "Z"));
            ApplyPoint[3].R = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[3].ToString(), "R"));
            ApplyPoint[4].X = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[4].ToString(), "X"));
            ApplyPoint[4].Y = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[4].ToString(), "Y"));
            ApplyPoint[4].Z = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[4].ToString(), "Z"));
            ApplyPoint[4].R = float.Parse(ini.IniReadValue(comboSupplyPoints.Items[4].ToString(), "R"));
            license = int.Parse(ini.IniReadValue("license", "1"));
            if(license!=1)
            {
                ini.IniWriteValue("license", "1", "1");
            }
        }
        public bool Initial()
        {
            LoadTableConfig(tableConfigFile);
            zmcaux.ZAux_OpenEth(IP, out mHandle);
            if((int)mHandle==0)
            {
                MessageBox.Show("控制器连接失败，请检查网络设置！", "警告！");
                return false;
            }
            
            if (license != 1)
            {
                float[] data = new float[] { 1, 1, 1, 1, 1 };
                WriteFlash(0, 5, data);
            }
            byte[] iostate = new byte[4];
            zmcaux.ZAux_Modbus_Set0x(mHandle, 20000, 4, iostate);
            for(int i=0;i<4;i++)
            {
                if (axisParameter[i].HomeWithZP == 1)
                {
                    zmcaux.ZAux_Direct_SetAtype(mHandle, i, 7);//轴类型
                }
                else
                {
                    zmcaux.ZAux_Direct_SetAtype(mHandle, i, 1);//轴类型
                }
                zmcaux.ZAux_Direct_SetUnits(mHandle, i, 1);
                zmcaux.ZAux_Direct_SetInvertStep(mHandle, i, 1);//脉冲+方向
                if (i < 3)
                {
                    zmcaux.ZAux_Direct_SetDatumIn(mHandle, i, 4 * i + 3);//设置轴原点信号
                    zmcaux.ZAux_Direct_SetFwdIn(mHandle, i, 4 * i + 1);//设置轴正限位信号
                    zmcaux.ZAux_Direct_SetRevIn(mHandle, i, 4 * i + 2);//设置轴负限位信号
                    zmcaux.ZAux_Direct_SetInvertIn(mHandle, 4 * i + 1, axisParameter[i].Plimit);
                    zmcaux.ZAux_Direct_SetInvertIn(mHandle, 4 * i + 2, axisParameter[i].Nlimit);
                    zmcaux.ZAux_Direct_SetInvertIn(mHandle, 4 * i + 3, axisParameter[i].Orgflag);
                }
                if(i==3)
                {
                    zmcaux.ZAux_Direct_SetDatumIn(mHandle, i,  19);//设置轴原点信号
                    zmcaux.ZAux_Direct_SetFwdIn(mHandle, i, 21);//设置轴正限位信号
                    zmcaux.ZAux_Direct_SetRevIn(mHandle, i, 19);//设置轴负限位信号
                }
                
                
                

                float acc = axisParameter[i].MaxSpeed  / axisParameter[i].AccTime;//设置加速度
                float dec = axisParameter[i].MaxSpeed / axisParameter[i].DecTime;//设置减速度
                zmcaux.ZAux_Direct_SetAccel(mHandle, i, acc);//设置加速度
                zmcaux.ZAux_Direct_SetDecel(mHandle, i, dec);//设置减速度
                zmcaux.ZAux_Direct_SetSramp(mHandle, i, axisParameter[i].Sramp);//设置斜率

            }
            if(tableMode==1)
            {
                
                zmcaux.ZAux_Direct_SetRevIn(mHandle, 1, 7);              
                zmcaux.ZAux_Direct_SetRevIn(mHandle, 1, 8);
                butManuRN.Visible = true;
                butManuRP.Visible = true;
                labCoordR.Visible = true;
                labHomeR.Visible = true;
                labLmtN3.Visible = true;
                labLmtP3.Visible = true;
                Label6.Visible = true;
                labAxisRdR.Visible = true;
                Button temp = new Button();
                temp.Location = butManuRN.Location;
                butManuRN.Location = butManuRP.Location;
                butManuRP.Location = temp.Location;
                temp.Location = butManuYN.Location;
                butManuYN.Location = butManuYP.Location;
                butManuYP.Location = temp.Location;
                butManuYN.Text = "Y1-";
                butManuYP.Text = "Y1+";

            }
            
            ServoOn(true);
            for (int i = 0; i < 64; i++)
            {
                Input[i] = new CheckBox();
                Output[i] = new CheckBox();
                Input[i].Text = i.ToString();
                int m = i / 8; int n = i % 8;
                tableLayoutPanel1.Controls.Add(Input[i], n, m);

                tableLayoutPanel2.Controls.Add(Output[i], n, m);
                Output[i].Text = i.ToString();
            }
            getCardStatus.Elapsed += getCardStatus_Elapsed;
            getCardStatus.Start();
            timGUI.Enabled = true;
            timGUI.Start();
            songdingji.RunWorkerAsync();
            rdbManuStep.Checked = true;
            rdbStep01.Checked = true;
            return true;
        }
        bool pause = false;
        void getCardStatus_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            getCardStatus.Stop();
            int runstate = 0;
            uint axisState = 0;
            for (int i = 0; i < 4; i++)
            {             
                zmcaux.ZAux_Direct_GetIfIdle(mHandle, i, ref runstate);
                axisStatus[i].Busing = runstate == 0 ? true : false;
                float temp = 0;
                zmcaux.ZAux_Direct_GetDpos(mHandle, i, ref temp);
                axisStatus[i].CurrentPos = temp * axisParameter[i].mm_r / axisParameter[i].RotatePulse;
                if(i<3)
                {
                zmcaux.ZAux_Direct_GetIn(mHandle, 4*i+1, ref axisState);
                axisStatus[i].LimitP = axisState == 0 ? true : false;
                zmcaux.ZAux_Direct_GetIn(mHandle, 4 * i + 2, ref axisState);
                axisStatus[i].LimitN = axisState == 0 ? true : false;
                zmcaux.ZAux_Direct_GetIn(mHandle, 4 * i + 3, ref axisState);
                axisStatus[i].Org = axisState == 0 ? true : false;
                zmcaux.ZAux_Direct_GetIn(mHandle, i + 24, ref axisState);
                if(i==2)
                {
                    if(tableMode==1)
                    {
                        axisStatus[i].Ready = true;
                    }else
                    axisStatus[i].Ready = axisState == 1 ? true : false;
                }
                if(i!=2)
                axisStatus[i].Ready = axisState == 1 ? true : false;
                zmcaux.ZAux_Direct_GetAxisStatus(mHandle, i,ref axisStatus[i].Code);
                }
                if(i==3)
                {
                    zmcaux.ZAux_Direct_GetIn(mHandle, 21, ref axisState);
                    axisStatus[i].LimitP = axisState == 0 ? true : false;
                    zmcaux.ZAux_Direct_GetIn(mHandle, 22, ref axisState);
                    axisStatus[i].LimitN = axisState == 0 ? true : false;
                    zmcaux.ZAux_Direct_GetIn(mHandle, 19, ref axisState);
                    axisStatus[i].Org = axisState == 0 ? true : false;
                    zmcaux.ZAux_Direct_GetIn(mHandle, i + 24, ref axisState);
                    axisStatus[i].Ready = axisState == 1 ? true : false;
                    zmcaux.ZAux_Direct_GetAxisStatus(mHandle, i, ref axisStatus[i].Code);
                }
            }
            //zmcaux.ZAux_Direct_GetIn(mHandle,(int)DI.ServoRdyA,ref axisState);
            //axisStatus[3].Ready = axisState == 1 ? true : false;
            //zmcaux.ZAux_Direct_GetDpos(mHandle, 3, ref axisStatus[3].CurrentPos);
            if (!(axisStatus[0].Ready&axisStatus[1].Ready&axisStatus[2].Ready))
            {
                TableHomeOK = false;
            }
            A_Status.CurrentSpeed = ReadModBus_int(480);
            A_Status.CurrentTorque = ReadModBus_int(482);
            A_Status.CurrentLength = ReadLength(0) * measureRangeA / 4096;
            A_Status.CurrentLength2 = ReadLength(1) * measureRangeB / 4096;
            if (Math.Abs(axisStatus[0].CurrentPos - ApplyPoint[3].X) < 0.1 && Math.Abs(axisStatus[1].CurrentPos - ApplyPoint[3].Y) < 0.1 && Math.Abs(axisStatus[2].CurrentPos - ApplyPoint[3].Z) < 0.1)
            {
                Canturn = true;
            }
            if (SafeCheckOpen)
            {
                if (!ReadDi(Table.DI.SafetyLight))
                { safetyLight = true; }
                else if (safetyLight & ReadDi(DI.Reset))
                {
                    safetyLight = false;
                }
            }
            else
                safetyLight = false;
            //Console.WriteLine("A_Status.CurrentSpeed={0}", A_Status.CurrentSpeed);
            //Console.WriteLine("A_Status.CurrentTorque={0}", A_Status.CurrentTorque);
            //luoliaocheck1 = ReadDi(DI.SDJ1_LL);
            //luoliaocheck2 = ReadDi(DI.SDJ2_LL);
            getCardStatus.Start();
        }
        public void ServoOn(bool status)
        {
            if (tableMode == 0)
            {
                if (dataChange.ShortGetBit(UsingAxis, 0))
                {
                    WriteDO(DO.ServoOnX, status);
                }
                if (dataChange.ShortGetBit(UsingAxis, 1))
                {
                    WriteDO(DO.ServoOnY, status);
                }
                if (dataChange.ShortGetBit(UsingAxis, 2))
                {
                    WriteDO(DO.ServoOnZ, status);
                }
                if (dataChange.ShortGetBit(UsingAxis, 3))
                {
                    WriteDO(DO.ServoOnA, status);
                }
                SVon = status;
            }
            if(tableMode==1)
            {
                if (dataChange.ShortGetBit(UsingAxis, 0))
                {
                    WriteDO(DO.ServoOnX, false);
                }
                if (dataChange.ShortGetBit(UsingAxis, 1))
                {
                    WriteDO(DO.ServoOnY, false);
                }
                if (dataChange.ShortGetBit(UsingAxis, 2))
                {
                    WriteDO(DO.ServoOnZ, false);
                }
                if (dataChange.ShortGetBit(UsingAxis, 3))
                {
                    WriteDO(DO.ServoOnA, false);
                }
            }

        }
        #region//IO
        public bool ReadDi(DI index,bool Inversion=false)
        {
            uint value = 0;
            bool result = false;
            zmcaux.ZAux_Direct_GetIn(mHandle, (int)index, ref value);
            result = value == 1 ? true : false;
            if (Inversion)
            { return !result; }
            return result;              
        }
        public bool ReadDi(int index, bool Inversion = false)
        {
            uint value = 0;
            bool result = false;
            zmcaux.ZAux_Direct_GetIn(mHandle, index, ref value);
            result = value == 1 ? true : false;
            if (Inversion)
                return !result;
            return result;       
        }
        public bool ReadDo(DO index)
        {
            uint value = 0;
            zmcaux.ZAux_Direct_GetOp(mHandle, (int)index, ref value);
            if (value == 0)
                return false;
            return true;
        }
        public bool ReadDo(int index)
        {
            uint value = 0;
            zmcaux.ZAux_Direct_GetOp(mHandle, index, ref value);
            if (value == 0)
                return false;
            return true;
        }
        public void WriteDO(int index, bool val)
        {
            if (val)
            {
                zmcaux.ZAux_Direct_SetOp(mHandle, index, 1);
            }else
            {
                zmcaux.ZAux_Direct_SetOp(mHandle, index, 0);
            }
        }
        public void WriteDO(DO index, bool val)
        {
            if (val)
            {
                zmcaux.ZAux_Direct_SetOp(mHandle, (int)index, 1);
            }
            else
            {
                zmcaux.ZAux_Direct_SetOp(mHandle, (int)index, 0);
            }
        }
        public int ReadModBus_int(ushort index)
        {
            int temp=0;
        zmcaux.ZAux_Modbus_Get4x_Long(mHandle, index, 1, ref temp);
        return temp;
        }
        public short[] ReadModBus_short(ushort index,ushort number)
        {
            int[] temp=new int[number];
            for (int i = 0; i < number; i++)
            {
                zmcaux.ZAux_Modbus_Get4x_Long(mHandle, (ushort)(index+2*i), 1, ref temp[i]);
            }
            int n = temp.Length;
            short[] data = new short[2 * n];
            for(int i=0;i<n;i++)
            {
                data[2 * i] = (short)(temp[i] & (0x0000ffff));
                data[2 * i + 1] = (short)(temp[i] >> 16);
            }
            return data;
        }  
        public void WriteModBus_int(ushort StartIndex,ushort count, int[] data)
        {
            zmcaux.ZAux_Modbus_Set4x_Long(mHandle, StartIndex, count, data);
        }

        public int WriteFlash(ushort flashID,uint count,float[] data)
        {
            int re=0;
            for (int i = 0; i < count;i++ )
            {
                float[] Wdata = new float[] { data[i] };
                re+=zmcaux.ZAux_FlashWritef(mHandle, (ushort)(flashID+i), 1, Wdata);
            }
            return re;
                
        }

        public int ReadFlash(ushort flashID, uint count,ref float[] data)
        {
            uint index=0;
            int re = 0;
            //float[] values = new float[10];
            //for (int i = 0; i < 10; i++)
            //    values[i] = 0;
            //IntPtr val = Marshal.AllocHGlobal(10*sizeof(float));
            //Marshal.Copy(values, 0, val, 10);
            for (int i = 0; i < count; i++)
            {

                re += zmcaux.ZAux_FlashReadf(mHandle, (ushort)(flashID + i), 1, ref data[i], ref index);
            }
            return re;
            
            
            //Marshal.Copy(val, values, 0, (int)index);
            //Marshal.FreeHGlobal(val);

            return 0;

            //return zmcaux.ZAux_FlashReadf(mHandle, flashID, count, ref data, ref index);
        } 
           
        public void WriteVrInt(int StartIndex,int value)
        {
            float temp = value;
            zmcaux.ZAux_Direct_SetVrf(mHandle, StartIndex, 1, ref temp);
        }
        public int ReadVrInt(int StartIndex)
        {
            int temp = 0;
            zmcaux.ZAux_Direct_GetVrInt(mHandle, StartIndex, 1, ref temp);
            return temp;
        }
        #endregion
        bool SVon = false;
        private void butManuServoOn_Click(object sender, EventArgs e)
        {
            if((int)mHandle==0)
            {
                MessageBox.Show("控制器连接失败！");
                return;
            }
            if(Auto)
            {
                MessageBox.Show("自动状态下，禁止更改伺服使能信号！");
                return;
            }

            ServoOn(!SVon);
            
           
        }      
        private void timGUI_Tick(object sender, EventArgs e)
        {
            timGUI.Stop();
            labSpeedDisp.Text =(SpeedRate * 100).ToString("f0")+" %";
            labHomeX.BackColor = axisStatus[0].Org == false ? Color.Lime : Color.Red;
            labHomeY.BackColor = axisStatus[1].Org == false ? Color.Lime : Color.Red;
            labHomeZ.BackColor = axisStatus[2].Org == false ? Color.Lime : Color.Red;
            labHomeR.BackColor = axisStatus[3].Org == false ? Color.Lime : Color.Red;
            labLmtP0.BackColor = axisStatus[0].LimitP == false ? Color.Lime : Color.Red;
            labLmtP1.BackColor = axisStatus[1].LimitP == false ? Color.Lime : Color.Red;
            labLmtP2.BackColor = axisStatus[2].LimitP == false ? Color.Lime : Color.Red;
            labLmtP3.BackColor = axisStatus[3].LimitP == false ? Color.Lime : Color.Red;
            labLmtN0.BackColor = axisStatus[0].LimitN == false ? Color.Lime : Color.Red;
            labLmtN1.BackColor = axisStatus[1].LimitN == false ? Color.Lime : Color.Red;
            labLmtN2.BackColor = axisStatus[2].LimitN == false ? Color.Lime : Color.Red;
            labLmtN3.BackColor = axisStatus[3].LimitN == false ? Color.Lime : Color.Red;
            labAxisRdX.BackColor = axisStatus[0].Ready == true ? Color.Lime : Color.Red;
            labAxisRdY.BackColor = axisStatus[1].Ready == true ? Color.Lime : Color.Red;
            labAxisRdZ.BackColor = axisStatus[2].Ready == true ? Color.Lime : Color.Red;
            labAxisRdR.BackColor = axisStatus[3].Ready == true ? Color.Lime : Color.Red;
            btnManuServoOn.BackColor = SVon == true ? Color.Lime : Color.Red;
            labCoordX.Text = axisStatus[0].CurrentPos.ToString("f2");
            labCoordY.Text = axisStatus[1].CurrentPos.ToString("f2");
            labCoordZ.Text = axisStatus[2].CurrentPos.ToString("f2");
            labCoordR.Text = axisStatus[3].CurrentPos.ToString("f2");    
              Estop=!ReadDi(DI.EStop);
            if (axisStatus[0].Busing | axisStatus[1].Busing | axisStatus[2].Busing | axisStatus[3].Busing)
            {
                labMoveing.BackColor = Color.Lime;
                if (Estop)
                {
                    MoveStop(0);
                    MoveStop(1);
                    MoveStop(2);
                    MoveStop(3);                 
                }
                if(safetyLight)
                {
                   
                    MovePause();
                    pause = true;
                }
            }
            else
                labMoveing.BackColor = Color.White;
            if((pause&!safetyLight))
            {
                MoveResume();
                pause = false;
            }
            if(Auto)
            {               
                btnManuServoOn.Enabled = false;
                btnGohome.Enabled = false;
                btnHomeFind.Enabled = false;
                btnStopHome.Enabled = false;
                grpJog.Enabled = false;
                grpLength.Enabled = false;
                grpMode.Enabled = false;
            }else
            {
                btnManuServoOn.Enabled = true;
                btnGohome.Enabled = true;
                btnHomeFind.Enabled = true;
                btnStopHome.Enabled = true;
                grpJog.Enabled = true;
                grpLength.Enabled = true;
                grpMode.Enabled = true;
            }
            
            if (groupBox2.Visible)
            {
                for (int i = 0; i < 64; i++)
                {
                    Input[i].Checked = ReadDi(i);
                    Output[i].Checked = ReadDo(i);
                }
            }
            labHomeFind.ForeColor = TableHomeOK == true ? Color.Lime : Color.Red;
           
            timGUI.Start();
        }      
        #region//手动相关
        private void MoveRelative(int axis, float distance, float StartSPDrate = 0.05f, float SPDrate = 0.2f)
        {
            if ((int)mHandle == 0)
                return;
            MovePara(axis, StartSPDrate, SPDrate);
            float dis = distance*axisParameter[axis].RotatePulse / axisParameter[axis].mm_r;
            zmcaux.ZAux_Direct_Singl_Move(mHandle, axis, dis);
        }

        private void MoveJog(int axis,int dir)
        {
            float dis = 0.1f;
            if(!(dir==1|dir==-1))
            {
                dir = 1;
            }
            if(SpeedRate>0.3)
            {
                SpeedRate = 0.3f;
            }
            float spd = axisParameter[axis].MaxSpeed * SpeedRate/2;

            if(rdbManuLine.Checked)
            {
                dis = 50*dir;
            }else if(rdbStep01.Checked)
            {
                dis = 0.1f*dir;
            }
            else if (rdbStep05.Checked)
            {
                dis = 0.5f * dir;
            }
            else if (rdbStep10.Checked)
            {
                dis = 1 * dir;
            }
            else if (rdbStep50.Checked)
            {
                dis = 5 * dir;
            }
            MoveRelative(axis, dis, 0.05f, SpeedRate);
        }
        private void MoveStop(int axis)
        {
            if ((int)mHandle == 0)
                return;
            zmcaux.ZAux_Direct_Singl_Cancel(mHandle, axis, 2);
        }
        private void butManu_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            int i =int.Parse(btn.Tag.ToString());
            int dir = 1;
            if(i>3)
            { i = i - 4;
            dir = -1;
            }
            MoveJog(i, dir);
        }
        private void btnManu_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            int i = int.Parse(btn.Tag.ToString());
            if(i>3)
            { i = i - 4;}
            if(rdbManuLine.Checked)
             MoveStop(i);
        }
        private void btnManu_MouseUp(object sender, EventArgs e)
        {

        }
        private void butSpeedP_Click(object sender, EventArgs e)
        {
            if (SpeedRate > 0.9)
            {
                SpeedRate = 1;
            }
            else
                SpeedRate += 0.1f;
        }
        private void butSpeedN_Click(object sender, EventArgs e)
        {
            if (SpeedRate <= 0.1)
            {
                SpeedRate = 0.1f;
            }
            else
                SpeedRate -= 0.1f;
        }
        #endregion
        #region//回零相关
        private void btnHomeFind_Click(object sender, EventArgs e)
        {
            if(Auto)
            {
                MessageBox.Show("自动状态下静止回零！");
                return;
            }
            WriteDO(Table.DO.CLD_Dianpi1, false);
            WriteDO(Table.DO.CLD_Dianpi2, false);
            WriteDO(DO.CLD_Blow1, false);
            WriteDO(DO.CLD_Blow2, false);

           string re=TableFindHome();
            if(re!="")
            {
                MessageBox.Show(re.ToString());
                return;
            }
        }

        private void FindHome(int axis, float hightSpeedrate = 0.1f, float lowSpeedrate = 0.02f)
        {
            if ((int)mHandle == 0)
                return;
            int mode = 14;
            if(tableMode==1)
            {
                if(axis==0)
                mode = 13;              
            }
            if (hightSpeedrate > 0.4f | hightSpeedrate <= 0)
                hightSpeedrate = 0.4f;
            if (lowSpeedrate > 0.05f | lowSpeedrate <= 0)
                lowSpeedrate = 0.05f;
            if (axisParameter[axis].HomeWithZP == 1)
            { mode = 16; }
            MovePara(axis, 0.01f, hightSpeedrate);
            zmcaux.ZAux_Direct_SetCreep(mHandle, axis, axisParameter[axis].MaxSpeed*lowSpeedrate);
            zmcaux.ZAux_Direct_Singl_Datum(mHandle, axis, mode);
        }
        public string TableFindHome(int mode=0)
        {
           
            if((int)mHandle==0)
            {
                return "-1,未连接控制器！";
            }
            FindHome(2);
           // Delay(10000);
            do
            {
                Delay(50);
            } while (axisStatus[2].Code != 64);
            int time = timeGetTime() + 8000;
            while(axisStatus[2].Code!=0&timeGetTime()<time)
            {
                Delay(50);
            }
            if(timeGetTime()>time)
            {
                MoveStop(2);
                return "-2,Z轴回零超时！";
            }
            FindHome(0);
            //Delay(10000);
            if (mode != 2)
            {
                FindHome(1);
               // Delay(10000);
            }
            if ( mode == 2)
            {
                FindHome(3);
            }
            time = timeGetTime() + 10000;
            //Delay(100);
            while (axisStatus[0].Busing | timeGetTime() > time)
            {
                   Delay(100);
            }
            if (mode != 2)
            {
                while (axisStatus[1].Busing | timeGetTime() > time)
                {
                       Delay(50);
                }
            }
            if ( mode == 2)
            {
                while (axisStatus[3].Busing | timeGetTime() > time)
                {
                      Delay(50);
                }
            }
            if (timeGetTime() > time)
            {
                return "-3,xy回零超时！";
            }
            TableHomeOK = true;
            return "";       
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

        private void btnStopHome_Click(object sender, EventArgs e)
        {
            zmcaux.ZAux_Direct_Rapidstop(mHandle, 2);
        }
        #endregion
        #region//定位相关
       
        private void buttonSaveSupply_Click(object sender, EventArgs e)
        {
            int n=comboSupplyPoints.SelectedIndex;
            if (n < 0)
                return;

            if (DialogResult.Yes == MessageBox.Show("确定保存当前点位为" + comboSupplyPoints.Text + "?", "提示！", MessageBoxButtons.YesNo))
            {
                ApplyPoint[n].X = axisStatus[0].CurrentPos;
                ApplyPoint[n].Y = axisStatus[1].CurrentPos;
                ApplyPoint[n].Z = axisStatus[2].CurrentPos;
                ApplyPoint[n].R = axisStatus[3].CurrentPos;
                Ini.IniFile ini = new Ini.IniFile(tableConfigFile);
                ini.IniWriteValue(comboSupplyPoints.Text, "X", axisStatus[0].CurrentPos.ToString());
                ini.IniWriteValue(comboSupplyPoints.Text, "Y", axisStatus[1].CurrentPos.ToString());
                ini.IniWriteValue(comboSupplyPoints.Text, "Z", axisStatus[2].CurrentPos.ToString());
                ini.IniWriteValue(comboSupplyPoints.Text, "R", axisStatus[3].CurrentPos.ToString());
            }
        }
        private void MovePara(int axis, float StartSPDrate = 0.05f, float SPDrate = 0.2f)
        {
            if (StartSPDrate > 0.2 | StartSPDrate < 0)
            {
                StartSPDrate = 0.05f;
            }
            if (StartSPDrate > 1 | StartSPDrate < 0)
            {
                StartSPDrate = 0.2f;
            }
            float startSpeed = axisParameter[axis].MaxSpeed * StartSPDrate;
            float speed = axisParameter[axis].MaxSpeed * SPDrate;
            zmcaux.ZAux_Direct_SetLspeed(mHandle, axis, startSpeed);
            zmcaux.ZAux_Direct_SetSpeed(mHandle, axis, speed);
            //float acc = speed / axisParameter[axis].AccTime;//设置加速度
            //zmcaux.ZAux_Direct_SetAccel(mHandle, axis, acc);//设置加速度
            //zmcaux.ZAux_Direct_SetDecel(mHandle, axis, acc);//设置减速度

        }
        private void MoveABS(int axis, float distance, float StartSPDrate = 0.05f, float SPDrate = 0.2f)
        {
            MovePara(axis, StartSPDrate, SPDrate);
            float dis = distance * axisParameter[axis].RotatePulse / axisParameter[axis].mm_r;
            zmcaux.ZAux_Direct_Singl_MoveAbs(mHandle, axis, dis);//  正运动 
        }
        public void MovePause(int mode=0)
        {           
            for (int i = 0; i < 3; i++)
            {
                int[] axislist = new int[] { i};
                zmcaux.ZAux_Direct_Base(mHandle, 1, axislist);
                zmcaux.ZAux_Direct_MovePause(mHandle, 0);
            }
        }
        public void MoveResume()
        {
          
            for (int i = 0; i < 3; i++)
            {
                int[] axislist = new int[] { i };
                zmcaux.ZAux_Direct_Base(mHandle, 1, axislist);
                zmcaux.ZAux_Direct_MoveResume(mHandle);
            }

            zmcaux.ZAux_Direct_MoveResume(mHandle);
        }
        public int GotoPoint(int tableindex,float X,float Y,float Z,float SpeedRate,float finRate=1,string checkSafy="关闭")
        {
            int retryTime = 0;
            if (SpeedRate > 1)
                SpeedRate = 1;
            if (SpeedRate < 0)
                SpeedRate = 0.2f;
            if (axisStatus[2].CurrentPos > SafetyH+1)
            {
                if (checkSafy == "启用")
                {
                    while (safetyLight)
                    {
                        Thread.Sleep(100);
                    }
                }
                MoveABS(2, SafetyH, 0.05f, SpeedRate);
                int time = timeGetTime();
                while (Math.Abs(SafetyH - axisStatus[2].CurrentPos) >=2)
                {
                    if (Estop)
                        break;
                    Thread.Sleep(50);
                    if (timeGetTime() > time + 4000)
                    {
                        break;//Z轴到第一安全点超时
                    }
                    
                }
            }
            if (Estop)
                return -10;

            int re;
            if (checkSafy == "启用")
            {
                while (safetyLight)
                {
                    Thread.Sleep(100);
                }
            }
            retryXY: if (tableindex == 0)
            {
                re = GotoXY(X, Y, SpeedRate, finRate);
            }else
            {
                re = GotoXY2(X, Y, SpeedRate, finRate);
            }
           
            if ( re!= 0)
            {
                retryTime++;
                if (retryTime <= 1)
                {
                    Delay(200);
                    goto retryXY;
                }
                else
                {
                    return -2;
                }
            }
            if (Estop)
                return -19;
        retryZ: re = GotoZ(Z, SpeedRate, finRate);
            if (re != 0)
            {
                retryTime++;
                if (retryTime <= 1)
                {
                    Delay(200);
                    goto retryZ;
                }else
                {
                    return -3;
                }
            }      
            return 0;
        }
        public int GotoXY(float X,float Y,float SpeedRate,float finRate=1)
        {
            float XLength = Math.Abs(axisStatus[0].CurrentPos - X);
            float YLength = Math.Abs(axisStatus[1].CurrentPos - Y);
            float XSpeedRate = SpeedRate, YSpeedRate = SpeedRate;
            if (XLength==0|YLength==0)
            {
                XSpeedRate = SpeedRate;
                YSpeedRate = SpeedRate;
            }
            else if (XLength >= YLength)
            {
                XSpeedRate = SpeedRate;
                YSpeedRate = XSpeedRate * YLength / XLength;
            }
            else if (YLength >= XLength)
            {
                YSpeedRate = SpeedRate;
                XSpeedRate = SpeedRate * XLength / YLength;
            }
            if (Estop)
                return -19;
            MoveABS(0, X, 0.05f, XSpeedRate);
            MoveABS(1, Y, 0.05f, YSpeedRate);
            int time = timeGetTime();
            while (axisStatus[0].Busing | axisStatus[1].Busing)
            {
                if (timeGetTime() > time + 8000)
                {
                    return -2;//XY定位运动启动异常
                }
            }
            time = timeGetTime();
            
            while(Math.Abs(axisStatus[0].CurrentPos - X)>=5|Math.Abs(axisStatus[1].CurrentPos - Y)>=5)
            {
                if (timeGetTime() > time + 8000)
                {
                    return -3;//XY定位超时
                }
                if (Estop)
                    return -19;
            }           
            return 0;
        }
        public int GotoXY2(float X, float Y2, float SpeedRate, float finRate = 1)
        {
            float XLength = Math.Abs(axisStatus[0].CurrentPos - X);
            float Y2Length = Math.Abs(axisStatus[3].CurrentPos - Y2);
            float XSpeedRate, Y2SpeedRate;
            if (XLength >= Y2Length)
            {
                XSpeedRate = SpeedRate;
                if (XLength == 0)
                { Y2SpeedRate = SpeedRate; }
                else
                    Y2SpeedRate = XSpeedRate * Y2Length / XLength;
            }
            else
            {
                Y2SpeedRate = SpeedRate;
                if (Y2Length == 0)
                    XSpeedRate = SpeedRate;
                else
                    XSpeedRate = SpeedRate * XLength / Y2Length;
            }
            MoveABS(0, X, 0.05f, XSpeedRate);
            MoveABS(3, Y2, 0.05f, Y2SpeedRate);
            int time = timeGetTime();
            while (axisStatus[0].Busing | axisStatus[3].Busing)
            {
                if (timeGetTime() > time + 10000)
                {
                    return -2;//XY定位运动启动异常
                }
                
            }
            
            time = timeGetTime();
            while (Math.Abs(axisStatus[0].CurrentPos - X) >= XLength * (1 - finRate) + 0.05 | Math.Abs(axisStatus[3].CurrentPos - Y2) >= Y2Length * (1 - finRate) + 0.05)
            {
                if (timeGetTime() > time + 8000)
                {
                    return -3;//XY定位超时
                }
            }
            while (Math.Abs(axisStatus[0].CurrentPos - X) >= 5 | Math.Abs(axisStatus[3].CurrentPos - Y2) >= 5)
            {
                if (timeGetTime() > time + 8000)
                {
                    return -3;//XY定位超时
                }
            }
            return 0;
        }
        public int GotoZ(float Z,float SpeedRate,float finRate=1)
        {
            int time = timeGetTime();
           
            do
            {
                MoveABS(2, Z, 0.05f, SpeedRate);
                Thread.Sleep(50);
                if(Math.Abs(Z - axisStatus[2].CurrentPos) < 0.5)
                {
                    break;
                }
            }
            while (!axisStatus[2].Busing);
            while (Math.Abs(Z - axisStatus[2].CurrentPos) >= 4)
            {
                Thread.Sleep(10);
                if (timeGetTime() > time + 5000)
                {
                    return -1;//Z轴定位超时
                }
                
               
            }
            


            return 0;
        }

        public int GotoZwithMM(float Z, float Speed, float finRate = 1)
        {
            int time = timeGetTime();
            float zSpeed=Speed*axisParameter[2].RotatePulse / axisParameter[2].mm_r;
            float dis = Z * axisParameter[2].RotatePulse / axisParameter[2].mm_r;
            zmcaux.ZAux_Direct_SetLspeed(mHandle, 2, 0);
            zmcaux.ZAux_Direct_SetSpeed(mHandle, 2, zSpeed);
            zmcaux.ZAux_Direct_Singl_MoveAbs(mHandle, 2, dis);            
            float length = Math.Abs(Z - axisStatus[2].CurrentPos);
            while (Math.Abs(Z - axisStatus[2].CurrentPos) > length * (1 - finRate) + 0.01)
            {
                Delay(50);
                if (timeGetTime() > time + 8000)
                {
                    return -1;//Z轴定位超时
                }
            }
            while (Math.Abs(Z - axisStatus[2].CurrentPos) >= 3)
            {
                Delay(50);
                if (timeGetTime() > time + 8000)
                {
                    return -1;//Z轴定位超时
                }
            }

            return 0;
        }

        #endregion
        private void buttonSupplyPointVerify_Click(object sender, EventArgs e)
        {
            if(comboSupplyPoints.SelectedIndex<0)
            {
                MessageBox.Show("请选择点位类型！");
                return;
            }
            if(!TableHomeOK)
            {
                MessageBox.Show("设备未回零！");
                return;
            }
            WriteDO(DO.CLD_Dianpi1, false);
            WriteDO(DO.CLD_Dianpi2, false);
            Delay(200);
            SafetyH = 0;
            float x=0, y=0, z=0, r=0;
            LoadTableConfig(tableConfigFile);
            if (DialogResult.Yes == MessageBox.Show("确定移动到" + comboSupplyPoints.Text + "?", "提示", MessageBoxButtons.YesNo))
            {
                int n= comboSupplyPoints.SelectedIndex;
                {
                    x = ApplyPoint[n].X;
                    y = ApplyPoint[n].Y;
                    z = ApplyPoint[n].Z;
                    r = ApplyPoint[n].R;
                }
                SpeedRate = 0.1f;
                GotoZ(0, SpeedRate);
                GotoPoint(0, x, y, z, SpeedRate);           
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (!TableHomeOK)
            {
                MessageBox.Show("设备未回零！");
                return;
            }
            WriteDO(DO.CLD_Dianpi1, false);
            WriteDO(DO.CLD_Dianpi2, false);
            Thread.Sleep(200);
            SafetyH = 0;
            float x = 0, y = 0, z = 0, r = 0;
            if (DialogResult.Yes == MessageBox.Show("确定移动到" + button3.Text + "?", "提示", MessageBoxButtons.YesNo))
            {
                int n = comboSupplyPoints.SelectedIndex;
                {
                    x = ApplyPoint[0].X;
                    y = ApplyPoint[0].Y;
                    z = ApplyPoint[0].Z;
                    r = ApplyPoint[0].R;
                }
                GotoZ(0, SpeedRate);
                GotoPoint(0, x, y, z, SpeedRate);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!TableHomeOK)
            {
                MessageBox.Show("设备未回零！");
                return;
            }
            WriteDO(DO.CLD_Dianpi1, false);
            WriteDO(DO.CLD_Dianpi2, false);
            Delay(200);
            SafetyH = 0;
            float x = 0, y = 0, z = 0, r = 0;
            if (DialogResult.Yes == MessageBox.Show("确定移动到" + button4.Text + "?", "提示", MessageBoxButtons.YesNo))
            {
                int n = comboSupplyPoints.SelectedIndex;
                {
                    x = ApplyPoint[1].X;
                    y = ApplyPoint[1].Y;
                    z = ApplyPoint[1].Z;
                    r = ApplyPoint[1].R;
                }
                SpeedRate = 0.1f;
                GotoZ(0, SpeedRate);
                GotoPoint(0, x, y, z, SpeedRate);
            }
        }

        public void GotoSystemPoint(int index,float spd=0.5f)
        {
            if (index >= comboSupplyPoints.Items.Count)
                return;
            float x = 0, y = 0, z = 0, r = 0;
            int n = index;
            {
                x = ApplyPoint[n].X;
                y = ApplyPoint[n].Y;
                z = ApplyPoint[n].Z;
                r = ApplyPoint[n].R;
            }
            GotoPoint(0, x, y, z, spd);               
        }
        public float ReadLength(int index)
        {
            float result=0;
            zmcaux.ZAux_Direct_GetAD(mHandle, index, ref result);
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(groupBox2.Location.X!=5)
            {
                groupBox2.Location = new Point(5, 5);
                groupBox2.Visible = true;
            }else
            {
                groupBox2.Location = new Point(420, 380);
                groupBox2.Visible = false;
            }
        }

        private void Table_Load(object sender, EventArgs e)
        {
           
        }

        private void comboSupplyPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void rdbStep01_CheckedChanged(object sender, EventArgs e)
        {
            rdbManuStep.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (groupBox2.Location.X != 5)
            {
                groupBox2.Location = new Point(5, 5);
                groupBox2.Visible = true;
            }
            else
            {
                groupBox2.Location = new Point(420, 380);
                groupBox2.Visible = false;
            }
        }

        private void btnGohome_Click(object sender, EventArgs e)
        {
            if (Auto)
                return;
            WriteDO(DO.CLD_Dianpi1, false);
            WriteDO(DO.CLD_Dianpi2, false);
            GotoZ(0, SpeedRate);
            GotoPoint(0,0, 0, 0, SpeedRate);
            //GotoPoint(1, 0, 0, 0, SpeedRate); 
        }

        private void songdingji_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                Thread.Sleep(50);
                if (SongdingjiMode == "组合双送钉机")
                {
                    if (Baidong1Step == 0)
                    {
                        if (!ReadDi(Table.DI.SDJ1_CK) | !ReadDi(Table.DI.SDJ2_CK))
                        {
                            WriteDO(Table.DO.SDJ1_Badong, true);
                            Baidong1Step = 1;
                            baidong1Delay[0] = timeGetTime() + 1000;
                            if (GetScrewMode == "滚筒")
                            {
                                baidong1Delay[0] += 19000;
                            }
                        }
                    }
                    if (Baidong1Step == 1 & baidong1Delay[0] <= timeGetTime())
                    {
                        WriteDO(Table.DO.SDJ1_Badong, false);
                        Baidong1Step = 2;
                        baidong1Delay[1] = timeGetTime() + 1000;
                    }
                    if (Baidong1Step == 2 & baidong1Delay[1] <= timeGetTime())
                    {
                        if (GetScrewMode == "滚筒")
                        {
                            Baidong1Step = 3;
                        }
                        else
                            Baidong1Step = 0;
                    }
                    if (Baidong1Step == 3)
                    {
                        WriteDO(Table.DO.SDJ1_Badong, true);
                        WriteDO(Table.DO.SDJ1_GuntongF, true);
                        baidong1Delay[2] = timeGetTime() + 5000;
                        Baidong1Step = 4;
                    }
                    if (Baidong1Step == 4 && baidong1Delay[2] <= timeGetTime())
                    {
                        WriteDO(Table.DO.SDJ1_Badong, false);
                        WriteDO(Table.DO.SDJ1_GuntongF, false);
                        Baidong1Step = 0;
                    }
                }
                else
                {
                    if (Baidong1Step == 0)
                    {
                        if (!ReadDi(Table.DI.SDJ1_CK))
                        {
                            WriteDO(Table.DO.SDJ1_Badong, true);
                            Baidong1Step = 1;
                            baidong1Delay[0] = timeGetTime() + 1000;
                            if (GetScrewMode == "滚筒")
                            {
                                baidong1Delay[0] += 19000;
                            }
                        }
                    }
                    if (Baidong1Step == 1 & baidong1Delay[0] <= timeGetTime())
                    {
                        WriteDO(Table.DO.SDJ1_Badong, false);
                        Baidong1Step = 2;
                        baidong1Delay[1] = timeGetTime() + 1000;
                        
                    }
                    if (Baidong1Step == 2 & baidong1Delay[1] <= timeGetTime())
                    {
                        if (GetScrewMode == "滚筒")
                        {
                            Baidong1Step = 3;
                        }
                        else
                            Baidong1Step = 0;
                    }
                    if (Baidong1Step == 3)
                    {
                        WriteDO(Table.DO.SDJ1_Badong, true);
                        WriteDO(Table.DO.SDJ1_GuntongF, true);
                        baidong1Delay[2] = timeGetTime() + 5000;
                        Baidong1Step = 4;
                    }
                    if (Baidong1Step == 4 && baidong1Delay[2] <= timeGetTime())
                    {
                        WriteDO(Table.DO.SDJ1_Badong, false);
                        WriteDO(Table.DO.SDJ1_GuntongF, false);
                        Baidong1Step = 0;
                    }
                    if (Baidong2Step == 0)
                    {
                        if (!ReadDi(Table.DI.SDJ2_CK))
                        {
                            WriteDO(Table.DO.SDJ2_Badong, true);
                            Baidong2Step = 1;
                            baidong2Delay[0] = timeGetTime() + 1000;
                            if (GetScrewMode == "滚筒")
                            {
                                baidong2Delay[0] += 19000;
                            }
                        }
                    }
                    if (Baidong2Step == 1 & baidong2Delay[0] <= timeGetTime())
                    {
                        WriteDO(Table.DO.SDJ2_Badong, false);
                        Baidong2Step = 2;
                        baidong2Delay[1] = timeGetTime() + 1000;
                       
                    }
                    if (Baidong2Step == 2 & baidong2Delay[1] <= timeGetTime())
                    {
                        if (GetScrewMode == "滚筒")
                        {
                            Baidong2Step = 3;
                        }
                        else
                            Baidong2Step = 0;
                    }
                    if (Baidong2Step == 3)
                    {
                        WriteDO(Table.DO.SDJ2_Badong, true);
                        WriteDO(Table.DO.SDJ2_GuntongF, true);
                        baidong2Delay[2] = timeGetTime() + 5000;
                        Baidong2Step = 4;
                    }
                    if (Baidong2Step == 4 && baidong2Delay[2] <= timeGetTime())
                    {
                        WriteDO(Table.DO.SDJ2_Badong, false);
                        WriteDO(Table.DO.SDJ2_GuntongF, false);
                        Baidong2Step = 0;
                    }
                }
            }
        }

       
    }
}
