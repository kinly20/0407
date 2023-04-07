using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Data;
using System.IO;
using System.Configuration;
using MyControl.Parameters;
using MyControl.tools;
using HslCommunication.Profinet;
using System.Threading;
using HslCommunication.Profinet.Melsec;
using HslCommunication;
using System.Windows.Threading;
using System.Linq;

namespace Dashboard
{
    public static class StaticPlc
    {
        static string addr1 = System.Configuration.ConfigurationManager.AppSettings["螺钉机焊锡机状态地址"].ToString();
        static string addr2 = System.Configuration.ConfigurationManager.AppSettings["螺钉机焊锡机报警地址"].ToString();
        static string addr3 = System.Configuration.ConfigurationManager.AppSettings["螺钉机产量地址"].ToString();
        static string addr4 = System.Configuration.ConfigurationManager.AppSettings["接驳台触发地址"].ToString();
        static string addr5 = System.Configuration.ConfigurationManager.AppSettings["接驳台配方地址"].ToString();
        static string addr6 = System.Configuration.ConfigurationManager.AppSettings["IO信号器脉冲地址"].ToString();

        static List<AttributeModel> ProductInfo = new List<AttributeModel>();
        static Dictionary<string, string> IPInfo = new Dictionary<string, string>();//螺钉机 焊锡机等
        static Dictionary<string, string> IPInfo2 = new Dictionary<string, string>();//IO信号器
        static Dictionary<string, string> IPInfo3 = new Dictionary<string, string>();//接驳台

        static List<Trag> ProductTrags = new List<Trag>();//螺钉机 焊锡机等
        static List<int> Products = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static List<Trag> IOTrags = new List<Trag>();//IO信号器
        static List<Trag> ConnectTrags = new List<Trag>();//接驳台

        static DispatcherTimer timer;
        //static MelsecMcNet melsec_net = new MelsecMcNet();
        static PLC[] plcs = new PLC[30];//螺钉机 焊锡机等
        static PLC[] plcs2 = new PLC[30];//IO信号器
        static PLC[] plcs3 = new PLC[30];//接驳台


        static string AllAlarm = "";


        static DataTable dtweek = new DataTable();
        static DataTable dtday = new DataTable();
        static List<bool> Alarms = new List<bool>() { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        static List<bool> Status = new List<bool>() { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        static DataTable dtproducttime = new DataTable();


        static data data1 = new data();
        public static void Load()
        {
            try
            {
                data data = new data();
                //string link = System.Environment.CurrentDirectory;
                //data.LoadProgFile(ProductInfo, link + @"\参数表.csv");
                data.Loadip(IPInfo);
                data.Loadip(IPInfo2, "IO信号器");
                data.Loadip(IPInfo3, "接驳台");


                //dtweek = data1.SearchProductdata(System.DateTime.Now.AddDays(-7), System.DateTime.Now, "");
                //dtday = data1.SearchProductdata(System.DateTime.Now.AddDays(-1), System.DateTime.Now, "");
                dtproducttime = SQLiteeDB.Getproducttime();

                BuildMCplc(IPInfo, plcs);
                BuildMCplc(IPInfo2, plcs2);
                BuildMCplc(IPInfo3, plcs3);


                for (int i = 0; i < IPInfo.Count; i++)
                {
                    Trag subtrig = new Trag();
                    subtrig.ID = i;
                    subtrig.RisingEdge += Trig_RisingEdge;
                    ProductTrags.Add(subtrig);
                }

                for (int i = 0; i < IPInfo2.Count; i++)
                {
                    Trag subtrig = new Trag();
                    subtrig.ID = 100 + i;
                    subtrig.RisingEdge += Trig_RisingEdge2;
                    IOTrags.Add(subtrig);
                }

                //for (int i = 0; i < IPInfo3.Count; i++)
                //{
                //    Trag subtrig = new Trag();
                //    subtrig.ID = 200 + i;
                //    subtrig.RisingEdge += Trig_RisingEdge;
                //    ConnectTrags.Add(subtrig);
                //}


                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer1_Tick;
                timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void timer1_Tick(object sender, EventArgs e)
        {

            //报表内容更新
            int second = System.DateTime.Now.Second % 10;
            int second2 = System.DateTime.Now.Second;
            if (second == 5)
            {
                data data1 = new data();
                dtweek = data1.SearchProductdata(System.DateTime.Now.AddDays(-7), System.DateTime.Now, "");
                dtday = data1.SearchProductdata(System.DateTime.Now.AddDays(-1), System.DateTime.Now, "");
                dtproducttime = SQLiteeDB.Getproducttime();
            }


            for (int i = 0; i < IPInfo.Count; i++)
            {
                if (second2 == 5)
                {
                    //Random rd = new Random();
                    // p = System.DateTime.Now.Minute * 10 + rd.Next(1, 10);
                    ProductTrags[i].Value = plcs[i].plcNet.MW0[int.Parse(addr3.Split('.')[0])] != Products[i];//从数据库读取地址
                    //ProductTrags[i].Value = p != Products[i];//从数据库读取地址
                }
                Alarms[i] = dataChange.GetBitValue(plcs[i].plcNet.MW0[int.Parse(addr2.Split('.')[0])], addr2.Split('.').Length > 1 ? int.Parse(addr2.Split('.')[1]) : 0);//从数据库读取地址
                Status[i] = dataChange.GetBitValue(plcs[i].plcNet.MW0[int.Parse(addr1.Split('.')[0])], addr1.Split('.').Length > 1 ? int.Parse(addr1.Split('.')[1]) : 0);//从数据库读取地址
            }
            for (int i = 0; i < IPInfo2.Count; i++)
            {
                IOTrags[i].Value = dataChange.GetBitValue(plcs2[i].plcNet.MW0[int.Parse(addr6.Split('.')[0])], 0);//赋值IO信号地址 固定地址
                //Log.writelog(plcs2[i].RemoteIP + plcs2[i].plcNet.MW0[153].ToString());
                //IOTrags[i].Value = true;
            }

        }

        //static int p = 0;
        public static void Trig_RisingEdge(object sender, EventArgs e)//收集产量数据 并写入mysql
        {
            try
            {
                Trag sct = sender as Trag;
                int id = sct.ID;

                int productold = Products[id];
                int productnow = plcs[id].plcNet.MW0[int.Parse(addr3)];
                string pro = IPInfo.Values.ToList()[id];
                if (productold == 0)
                { }
                else if (productnow > productold)
                {
                    data1.InsertProductdata(pro, productnow - productold);
                }
                else
                {
                    data1.InsertProductdata(pro, productnow);
                }
                Products[id] = productnow;
                ProductTrags[id].Value = false;
            }
            catch (Exception ex)
            {
                //Log.writelog("扫码" + ex.ToString());
            }
        }

        public static void Trig_RisingEdge2(object sender, EventArgs e)//收集IO信号数据 并写入mysql
        {
            try
            {
                Trag sct = sender as Trag;
                int id = sct.ID - 100;

                string name = IPInfo2.Values.ToList()[id];

                data1.InsertProductdata(name, 1);
            }
            catch (Exception ex)
            {

            }
        }



        //public static int readplc(string addr, int value)
        //{
        //    int back = plcs[0].ReadShort(addr);

        //    string backvalue = tentotwo(back);
        //    string subbackvalue = backvalue.Substring(8 - value - 1, 1);//11001100
        //    return int.Parse(subbackvalue);
        //}

        //public static string tentotwo(int value)
        //{
        //    string value2 = "";
        //    if (value > 0)
        //    {
        //        while (value > 0)
        //        {
        //            value2 = value % 2 + value2;
        //            value = value / 2;
        //        }
        //    }
        //    else
        //        value2 = "0";
        //    while (value2.Length < 8)
        //        value2 = "0" + value2;
        //    return value2.ToString();
        //}

        public static void BuildMCplc(Dictionary<string, string> IPInfo, PLC[] plclist)
        {
            for (int i = 0; i < IPInfo.Count; i++)
            {
                string IP = IPInfo.Keys.ToList()[i];

                //plc = new PLC();
                //plc.RemoteIP = IP;
                //plc.Initial(@"D:\AutoMationtest1\AddressInPLC1.csv");


                plclist[i] = new PLC();
                plclist[i].RemoteIP = IP;
                plclist[i].Initial("AddressInPLC1.csv");


            }

            //plc = new PLC();
            //plc.RemoteIP = MachineIP;
            //plc.Initial("AddressInPLC1.csv");

        }

        public static string getAllAlarm()
        {
            for (int i = 0; i < Alarms.Count; i++)
            {
                if (Alarms[i])
                {
                    AllAlarm = IPInfo.Values.ToList()[i] + " 报警";
                    return AllAlarm;
                }
            }
            return "";
        }

        public static List<AlarmModel> Getalarms()
        {
            List<AlarmModel> alarms = new List<AlarmModel>();
            for (int i = 0; i < IPInfo.Count; i++)
            {
                if (Alarms[i])
                {
                    AlarmModel al = new AlarmModel();
                    al.Id = i.ToString();
                    al.addr = IPInfo.Keys.ToList()[i];
                    al.text = IPInfo.Values.ToList()[i];
                    alarms.Add(al);
                }
            }
            return alarms;
        }
        public static Dictionary<string, string> GetIPInfos()//螺钉机等信息
        {
            return IPInfo;
        }

        public static Dictionary<string, string> GetIOIPInfos()//螺钉机等信息
        {
            return IPInfo2;
        }

        public static Dictionary<string, string> GetConnectIPInfos()//接驳台信息
        {
            return IPInfo3;
        }


        public static DataTable Getdtweekproduct()
        {
            return dtweek;
        }

        public static DataTable Getdtdayproduct()
        {
            return dtday;
        }

        public static List<AttributeModel> GetProduct()
        {
            return ProductInfo;
        }

        public static List<bool> Getstatus()
        {
            return Status;
        }

        public static DataTable getdtproducttime()
        {
            return dtproducttime;
        }

        public static bool ChanggeWidth(string name, short peifang)//接驳台调宽
        {
            for (int i = 0; i < IPInfo3.Count; i++)
            {
                if (IPInfo3.Values.ToList()[i] == name)
                {
                    plcs3[i].plcNet.MW0[int.Parse(addr4.Split('.')[0])] = 1;//模拟地址
                    plcs3[i].WriteShort(addr4.Split('.')[0], 1);
                    plcs3[i].plcNet.MW0[int.Parse(addr5.Split('.')[0])] = peifang;
                    plcs3[i].WriteShort(addr5.Split('.')[0], peifang);
                    System.Threading.Thread.Sleep(1000);
                    plcs3[i].plcNet.MW0[int.Parse(addr4.Split('.')[0])] = 0;
                    plcs3[i].WriteShort(addr4.Split('.')[0], 0);
                    return true;
                }
            }
            return false;
        }
    }
}
