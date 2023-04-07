using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Timers;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Net;
using System.Xml;
//using System.Net.NetworkInformation;



namespace SFServer
{
    partial class TestService : ServiceBase
    {
        //public static string conn = System.Configuration.ConfigurationSettings.AppSettings["conn"].ToString() == null ?
        //    "Database=poms;Data Source=localhost;User Id=root;Password=123456;pooling=false;CharSet=utf8;port=3306"
        //    : System.Configuration.ConfigurationSettings.AppSettings["conn"].ToString();

        //private DateTime RunServerTime = new DateTime();
        private System.Timers.Timer trInit;

        private System.Timers.Timer trBack;


        private bool servicePaused = false;



        //实例化LOG
        //private Log alog = new Log();



        //
        private Thread DataBackThread = null;
        private bool isDataBackThread = false;



        PLC plc = new PLC();
        //互斥锁
        private static Mutex mutex = null;

        ////Socket协议
        //IPEndPoint ipep;
        //Socket clientSocket;
        string plcip = System.Configuration.ConfigurationManager.AppSettings["plcip"].ToString() == null ?
            "127.0.0.1" : System.Configuration.ConfigurationManager.AppSettings["plcip"].ToString();
        int plcport = System.Configuration.ConfigurationManager.AppSettings["plcport"].ToString() == null ?
            7789 : Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["plcport"].ToString());

        string agvip = System.Configuration.ConfigurationManager.AppSettings["agv1ip"].ToString() == null ?
           "127.0.0.1" : System.Configuration.ConfigurationManager.AppSettings["agv1ip"].ToString();
        int agvport = System.Configuration.ConfigurationManager.AppSettings["agv1port"].ToString() == null ?
            8088 : Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["agv1port"].ToString());

        string Locationleftfrom = System.Configuration.ConfigurationManager.AppSettings["Locationleftfrom"].ToString();
        string Locationrightfrom = System.Configuration.ConfigurationManager.AppSettings["Locationrightfrom"].ToString();

        string Locationleftmiddlelocation = System.Configuration.ConfigurationManager.AppSettings["Locationleftmiddlelocation"].ToString();
        string Locationrightmiddlelocation = System.Configuration.ConfigurationManager.AppSettings["Locationrightmiddlelocation"].ToString();
        string Locationleftmiddleroom = System.Configuration.ConfigurationManager.AppSettings["Locationleftmiddleroom"].ToString();
        string Locationrightmiddleroom = System.Configuration.ConfigurationManager.AppSettings["Locationrightmiddleroom"].ToString();

        string Locationleft = System.Configuration.ConfigurationManager.AppSettings["Locationleft"].ToString();
        string Locationright = System.Configuration.ConfigurationManager.AppSettings["Locationright"].ToString();
        string Locationleftto = System.Configuration.ConfigurationManager.AppSettings["Locationleftto"].ToString();
        string Locationrightto = System.Configuration.ConfigurationManager.AppSettings["Locationrightto"].ToString();

        int waittime = System.Configuration.ConfigurationManager.AppSettings["waittime"].ToString() == null ?
            20 : Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["waittime"].ToString());

        string num = System.Configuration.ConfigurationManager.AppSettings["蛋车个数"].ToString();

        string emptylocation = System.Configuration.ConfigurationManager.AppSettings["emptylocation"].ToString();
        string filllocation = System.Configuration.ConfigurationManager.AppSettings["filllocation"].ToString();

        //记录日志
        private void ProcessLog(string log)
        {
            //alog.msg = log;
            //alog.dte = DateTime.Now;
            Log.writelog(log);
        }

        public TestService()
        {
            InitializeComponent();

            plc = new PLC();
            plc.RemoteIP = plcip;
            plc.Initial(@"D:\AutoMationtest1\AddressInPLC1.csv");

            if (servicePaused == false)
            {
                mutex = new Mutex();
            }
        }



        protected override void OnStart(string[] args)//开始服务
        {
            //NewSocket();

            trInit = new System.Timers.Timer();
            trInit.Interval = 1000;
            trInit.Enabled = true;
            trInit.Elapsed += new ElapsedEventHandler(trInit_Tick);
            trInit.Start();
            // TODO: 在此处添加代码以启动服务。
        }

        private void trInit_Tick(object sender, EventArgs e)//服务正式进程
        {
            trInit.Enabled = false;


            try
            {
                //开始
                trBack = new System.Timers.Timer();
                trBack.Interval = 1000;
                trBack.Enabled = true;
                trBack.Elapsed += new ElapsedEventHandler(trBack_Tick);
                trBack.Start();


            }
            catch (Exception ex)
            {
                ProcessLog("启动线程失败，原因" + ex.ToString());
            }
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }



        private void trBack_Tick(object sender, EventArgs e)
        {
            if (!isDataBackThread)
            {
                DataBackThread = new Thread(new ThreadStart(delegate { EnergyDataBack(); }));
                DataBackThread.Name = "DataBackThread";
                isDataBackThread = true;
                DataBackThread.IsBackground = true;

                if (!DataBackThread.IsAlive)
                {
                    DataBackThread.Start();
                }
            }

            trBack.Enabled = false;
        }



        private void EnergyDataBack()
        {
            while (isDataBackThread)
            {
                SocketBack();
                //ProcessLog(System.DateTime.Now.ToString());


                Thread.Sleep(3000);//15秒
            }
        }

        string taskidleftbefore = ""; //请求AGV送料架到左缓存点
        string taskidleft = ""; //请求AGV从左缓存点送料架到左蛋车
        string taskidleft2 = ""; //请求AGV到左蛋车取走料架

        string taskidrightbefore = ""; //请求AGV送料架到右缓存点
        string taskidright = ""; //请求AGV从右缓存点送料架到右蛋车
        string taskidright2 = ""; //请求AGV到右蛋车取走料架

        //int count = 0;
        public void SocketBack()
        {

            try
            {
                int valuestop = readplc("505", 4);//总复位标志

                if (valuestop == 1)
                {
                    taskidleftbefore = ""; //请求AGV送料架到左缓存点
                    taskidleft = ""; //请求AGV从左缓存点送料架到左蛋车
                    taskidleft2 = ""; //请求AGV到左蛋车取走料架

                    taskidrightbefore = ""; //请求AGV送料架到右缓存点
                    taskidright = ""; //请求AGV从右缓存点送料架到右蛋车
                    taskidright2 = ""; //请求AGV到右蛋车取走料架
                    return;
                }

                if (emptylocation != "")
                {
                    string[] emptylocations = emptylocation.Split(',');
                    for (int i = 0; i < emptylocations.Length; i++)
                    {
                        HTTP.Editsite(emptylocations[i], 0);
                    }
                }

                if (filllocation != "")
                {
                    string[] filllocations = filllocation.Split(',');
                    for (int i = 0; i < filllocations.Length; i++)
                    {
                        HTTP.Editsite(filllocations[i], 1);
                    }
                }

                if (Locationleftfrom != "" && HTTP.Getsite(0, 0, Locationleftmiddleroom) && String.IsNullOrEmpty(taskidleftbefore))
                {
                    taskidleftbefore = HTTP.SetOrder(Locationleftfrom, Locationleftmiddlelocation);//下发任务
                    ProcessLog("任务1下发任务请求AGV送料架到左缓存" + taskidleftbefore);
                }


                int value = readplc("505", 5);
                int value2 = readplc("505", 6);
                if (Locationleftmiddleroom != "" && Locationleft != "" && value == 1 && String.IsNullOrEmpty(taskidleft)) //请求AGV送料架到左蛋车
                {
                    taskidleft = HTTP.SetOrder(Locationleftmiddleroom, Locationleft);//下发任务
                    ProcessLog("任务2下发任务请求AGV送料架到左蛋车" + taskidleft);
                }

                if (Locationleft != "" && Locationleftto != "" && value2 == 1 && String.IsNullOrEmpty(taskidleft2)) //请求AGV到左蛋车取走料架
                {
                    taskidleft2 = HTTP.SetOrder(Locationleft, Locationleftto);//下发任务
                    ProcessLog("任务3下发任务请求AGV到左蛋车取走料架" + taskidleft2);
                }
                else if (Locationleftto == "" && value2 == 1)//后照专用 取走跳过
                {
                    //System.Threading.Thread.Sleep(3000);
                    ProcessLog("任务3任务完成请求AGV到左蛋车取走料架" + taskidleft2);
                    writeplc("505.1", true);
                    //System.Threading.Thread.Sleep(300);
                    //writeplc("505.1", true);
                    System.Threading.Thread.Sleep(1500);
                    writeplc("505.1", false);
                    //System.Threading.Thread.Sleep(300);
                    //writeplc("505.1", false);
                    //System.Threading.Thread.Sleep(300);
                    //writeplc("505.1", false);
                    //System.Threading.Thread.Sleep(300);
                    //writeplc("505.1", false);
                    //System.Threading.Thread.Sleep(300);
                    //writeplc("505.1", false);
                }



                //根据已有任务列表查询任务状态


                if (!String.IsNullOrEmpty(taskidleftbefore))
                {
                    bool result = HTTP.SearchOrderNewPost(taskidleftbefore);
                    if (result)
                    {
                        ProcessLog("任务1任务完成请求AGV送料架到左缓存点" + taskidleftbefore);
                        taskidleftbefore = "";
                    }
                }
                if (!String.IsNullOrEmpty(taskidleft))
                {
                    bool result = HTTP.SearchOrderNewPost(taskidleft);
                    if (result)
                    {
                        ProcessLog("任务2任务完成请求AGV送料架到左蛋车" + taskidleft);
                        System.Threading.Thread.Sleep(waittime * 1000);
                        writeplc("505.0", true);
                        //System.Threading.Thread.Sleep(300);
                        //writeplc("505.0", true);
                        System.Threading.Thread.Sleep(1500);
                        writeplc("505.0", false);
                        //System.Threading.Thread.Sleep(300);
                        //writeplc("505.0", false);
                        //System.Threading.Thread.Sleep(300);
                        //writeplc("505.0", false);
                        //System.Threading.Thread.Sleep(300);
                        //writeplc("505.0", false);
                        //System.Threading.Thread.Sleep(300);
                        //writeplc("505.0", false);
                        taskidleft = "";
                    }
                }
                if (!String.IsNullOrEmpty(taskidleft2))
                {
                    bool result = HTTP.SearchOrderNewPostFast(taskidleft2);
                    result = true;
                    System.Threading.Thread.Sleep(3000);
                    if (result)
                    {
                        ProcessLog("任务3任务完成请求AGV到左蛋车取走料架" + taskidleft2);
                        writeplc("505.1", true);
                        //System.Threading.Thread.Sleep(300);
                        //writeplc("505.1", true);
                        System.Threading.Thread.Sleep(1500);
                        writeplc("505.1", false);
                        //System.Threading.Thread.Sleep(300);
                        //writeplc("505.1", false);
                        //System.Threading.Thread.Sleep(300);
                        //writeplc("505.1", false);
                        //System.Threading.Thread.Sleep(300);
                        //writeplc("505.1", false);
                        //System.Threading.Thread.Sleep(300);
                        //writeplc("505.1", false);

                        taskidleft2 = "";

                    }
                }



                if (num == "2")
                {
                    if (Locationrightfrom != "" && HTTP.Getsite(0, 0, Locationrightmiddleroom) && String.IsNullOrEmpty(taskidrightbefore))
                    {
                        taskidrightbefore = HTTP.SetOrder(Locationrightfrom, Locationrightmiddlelocation);//下发任务
                        ProcessLog("任务4下发任务请求AGV送料架到右缓存" + taskidrightbefore);
                    }

                    int valueright = readplc("505", 7);
                    int valueright2 = readplc("506", 0);
                    if (Locationrightmiddleroom != "" && Locationright != "" && valueright == 1 && (taskidright == null || taskidright == "")) //请求AGV送料架到右蛋车
                    {
                        taskidright = HTTP.SetOrder(Locationrightmiddleroom, Locationright);//下发任务
                        ProcessLog("任务5下发任务请求AGV送料架到右蛋车" + taskidright);
                    }

                    if (Locationright != "" && Locationrightto != "" && valueright2 == 1 && String.IsNullOrEmpty(taskidright2)) //请求AGV到右蛋车取走料架
                    {
                        taskidright2 = HTTP.SetOrder(Locationright, Locationrightto);//下发任务
                        ProcessLog("任务6下发任务请求AGV到右蛋车取走料架" + taskidright2);
                    }


                    //根据已有任务列表查询任务状态
                    if (!String.IsNullOrEmpty(taskidrightbefore))
                    {
                        bool result = HTTP.SearchOrderNewPost(taskidrightbefore);
                        if (result)
                        {
                            ProcessLog("任务4任务完成请求AGV送料架到右缓存点" + taskidrightbefore);
                            taskidrightbefore = "";
                        }
                    }
                    if (!String.IsNullOrEmpty(taskidright))
                    {
                        bool result = HTTP.SearchOrderNewPost(taskidright);
                        if (result)
                        {
                            ProcessLog("任务5任务完成请求AGV送料架到右蛋车" + taskidright);
                            System.Threading.Thread.Sleep(waittime * 1000);
                            //writeplc("505.2", true);
                            //System.Threading.Thread.Sleep(300);
                            writeplc("505.2", true);
                            System.Threading.Thread.Sleep(1500);
                            writeplc("505.2", false);
                            //System.Threading.Thread.Sleep(300);
                            //writeplc("505.2", false);
                            //System.Threading.Thread.Sleep(300);
                            //writeplc("505.2", false);
                            //System.Threading.Thread.Sleep(300);
                            //writeplc("505.2", false);
                            //System.Threading.Thread.Sleep(300);
                            //writeplc("505.2", false);
                            taskidright = "";
                        }
                    }
                    if (!String.IsNullOrEmpty(taskidright2))
                    {
                        bool result = HTTP.SearchOrderNewPostFast(taskidright2);
                        result = true;
                        System.Threading.Thread.Sleep(3000);
                        if (result)
                        {
                            ProcessLog("任务6任务完成请求AGV到右蛋车取走料架" + taskidright2);
                            //writeplc("505.3", true);
                            //System.Threading.Thread.Sleep(300);
                            writeplc("505.3", true);
                            System.Threading.Thread.Sleep(1500);
                            writeplc("505.3", false);
                            //System.Threading.Thread.Sleep(300);
                            //writeplc("505.3", false);
                            //System.Threading.Thread.Sleep(300);
                            //writeplc("505.3", false);
                            //System.Threading.Thread.Sleep(300);
                            //writeplc("505.3", false);
                            //System.Threading.Thread.Sleep(300);
                            //writeplc("505.3", false);
                            taskidright2 = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessLog("主程序执行失败" + ex.ToString());
            }
        }

        //AGV送料架到左蛋车完成  MW15900.0  
        //AGV到左蛋车取走料架完成  MW15900.1

        //AGV送料架到右蛋车完成  MW15901.0
        //AGV到右蛋车取走料架完成  MW15901.1

        //AGV准备就绪   MW15902.0






        //请求AGV送料架到左蛋车    MW15910.0
        //请求AGV到左蛋车取走料架  MW15910.1

        //请求AGV送料架到右蛋车    MW15911.0
        //请求AGV到右蛋车取走料架  MW15911.1


        public string buildnewtask(string leftright)
        {

            return leftright + System.DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        //public bool ping()
        //{
        //    Ping ping = new Ping();
        //    PingReply pingReply = ping.Send(agvip);
        //    if (pingReply.Status == IPStatus.Success)
        //        return true;
        //    else
        //        return false;
        //}





        //public void sendplc(string addr, short value)
        //{
        //    plc.plcNet.MW0[int.Parse(addr)] = value;
        //    plc.WriteShort(addr, value);
        //    //ProcessLog("写入plc成功，地址" + addr + "写入值" + value);
        //    int back = plc.ReadShort(addr);
        //    //if (back == value)
        //    //    ProcessLog("写入plc成功，地址" + addr + "写入值" + back);
        //    //else
        //    //    ProcessLog("写入值" + value + "失败,地址" + addr + "当前值" + back);
        //    //HslCommunicationClass communicationClass;
        //    //communicationClass = new HslCommunicationClass(ip);
        //    //communicationClass.Connect();
        //    //if (!communicationClass.isconnect)
        //    //{
        //    //    ProcessLog("写plc链接失败");
        //    //}
        //    //else
        //    //{
        //    //    string msg = "";
        //    //    communicationClass.WriteValue(addr, value, out msg);
        //    //}
        //}

        public void writeplc(string addr, bool value)
        {
            for (int i = 0; i < 10; i++)
            {
                plc.WriteBit(addr, value);

                string addr1 = addr.Substring(0, addr.IndexOf('.'));
                int addr2 = int.Parse(addr.Substring(addr.IndexOf('.') + 1, 1));
                int back = readplc(addr1, addr2);
                if (value && back == 1)
                    return;
                if (!value && back == 0)
                    return;
                Thread.Sleep(300);
            }
            ProcessLog("尝试写入plc失败，地址" + addr + "值" + value);
            return;
        }

        public int readplc(string addr, int value)
        {
            int back = plc.ReadShort(addr);

            //ProcessLog("读取plc成功，地址" + addr + "返回值" + back);
            string backvalue = tentotwo(back);
            string subbackvalue = backvalue.Substring(8 - value - 1, 1);//11001100
            //ProcessLog("读取plc成功，地址" + addr + "." + value + "返回值" + subbackvalue);
            return int.Parse(subbackvalue);
        }

        public string tentotwo(int value)
        {
            string value2 = "";
            if (value > 0)
            {
                while (value > 0)
                {
                    value2 = value % 2 + value2;
                    value = value / 2;
                }
            }
            else
                value2 = "0";
            while (value2.Length < 8)
                value2 = "0" + value2;
            return value2.ToString();
        }






    }
}
