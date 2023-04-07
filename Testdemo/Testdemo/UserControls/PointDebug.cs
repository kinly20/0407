using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testdemo.Class;
using System.Threading;
using System.Net.NetworkInformation;
using Newtonsoft.Json;

namespace Testdemo.UserControls
{
    public partial class PointDebug : UserControl
    {
        System.Threading.Thread thread;
        System.Windows.Forms.Timer time;

        PLC plc = new PLC();
        public PointDebug()
        {
            InitializeComponent();
        }


        #region plc link code
        HslCommunicationClass communicationClass;
        private void button5_Click(object sender, EventArgs e)
        {
            string ip = textBox1.Text;
            //communicationClass = new HslCommunicationClass(ip);
            //communicationClass.Connect();
            //if (!communicationClass.isconnect)
            //{
            //    changebutton(false);
            //    MessageBox.Show("connect fail");
            //}
            //else
            //{
            //    changebutton(true);
            //}


            plc = new PLC();
            plc.RemoteIP = ip;
            plc.Initial(@"D:\AutoMationtest\AddressInPLC1.csv");

            changebutton(true);
        }

        public void changebutton(bool value)
        {
            button6.Enabled = value;
            button7.Enabled = value;
            button8.Enabled = value;
            button9.Enabled = value;
            button10.Enabled = value;
        }


        private void button6_Click(object sender, EventArgs e)
        {
            communicationClass = new HslCommunicationClass("");
            if (communicationClass.isconnect)
                communicationClass.DisConnect();
            button10_Click(null, null);
            changebutton(false);
        }

        private void button7_Click(object sender, EventArgs e)
        {

            //plc.WriteBit("MW15900.0", true);
            plc.plcNet.MW0[15900] = 1;
            plc.WriteShort("15900", 1);



            //string addr = textBox2.Text;
            //string text = textBox3.Text;
            //string type = comboBox1.Text;
            //string msg = string.Empty;
            //if (type == "string" || type == "")
            //    communicationClass.WriteValue(addr, text, out msg);
            //else if (type == "int")
            //    communicationClass.WriteValue(addr, int.Parse(text), out msg);
            //else if (type == "float")
            //    communicationClass.WriteValue(addr, float.Parse(text), out msg);
            //else if (type == "double")
            //    communicationClass.WriteValue(addr, double.Parse(text), out msg);
            //else if (type == "short")
            //    communicationClass.WriteValue(addr, Convert.ToInt16(text), out msg);
            //else if (type == "byte")
            //    communicationClass.WriteValue(addr, Convert.ToByte(text), out msg);
            //else if (type == "bool")
            //    communicationClass.WriteValue(addr, Convert.ToBoolean(text), out msg);
        }

        private void button8_Click(object senderK, EventArgs e)
        {
            //bool SS = MyControl.tools.dataChange.GetBitValue(plc.plcNet.MW0[5000], 0);
            //bool xinhao = plc.plcNet.GetBit("MW12.0");
            //bool xinhao22 = plc.plcNet.GetBit("MW0.4");
            //bool xinhao2 = plc.plcNet.GetBit("MW15900.0");
            int productid2 = plc.ReadShort("MW520");

            int productid = plc.ReadShort("MW521");
            int productidsss = plc.ReadShort("MW522");//0 .0.1都是0    1.0为1  .1为0   2  .0为0  .1为1   3 .0.1都是1
            int productid33 = plc.ReadShort("MW15900");
            //int productnum = plc.ReadShort("MW30004");
            //int productiddds = plc.ReadShort("MW722");

            int ssd = 1;
            //short value = Convert.ToInt16(plc.plcNet.g("522") ? 1 : 0);//?

            //textBox4.Text = value.ToString();

            //short value = Convert.ToInt16(plc.plcNet.GetBit("15900.0") ? 1 : 0);//?

            //textBox4.Text = value.ToString();



            //string addr = textBox2.Text;

            //string msg = string.Empty;



            //string type = comboBox2.Text;

            //if (type == "string" || type == "")
            //{
            //    string back = "";
            //    communicationClass.ReadValueString(addr, out back, out msg);
            //    textBox4.Text = back.ToString();
            //}
            //else if (type == "int")
            //{
            //    int back = 0;
            //    communicationClass.ReadValue(addr, out back, out msg);
            //    textBox4.Text = back.ToString();
            //}
            //else if (type == "float")
            //{
            //    float back = 0;
            //    communicationClass.ReadValue(addr, out back, out msg);
            //    textBox4.Text = back.ToString();
            //}
            //else if (type == "double")
            //{
            //    double back = 0;
            //    communicationClass.ReadValue(addr, out back, out msg);
            //    textBox4.Text = back.ToString();
            //}
            //else if (type == "short")
            //{
            //    short back = 0;
            //    communicationClass.ReadValue(addr, out back, out msg);
            //    textBox4.Text = back.ToString();
            //}
            //else if (type == "byte")
            //{
            //    byte back = 0;
            //    communicationClass.ReadValue(addr, out back, out msg);
            //    textBox4.Text = back.ToString();
            //}
            //else if (type == "bool")
            //{
            //    bool back = false;
            //    communicationClass.ReadValue(addr, out back, out msg);
            //    textBox4.Text = back.ToString();
            //}

            //communicationClass.ReadValuetest(addr, out backstring, out msg);


            //short[] backvalues = new short[clength];
            //short result = communicationClass.readMulitDatas(int.Parse(addr), clength, ref backvalues);

            //textBox4.Text = backvalues.Select(t => t.ToString("X2")).ToString();
        }

        private delegate void FlushClient();
        private void button9_Click(object sender, EventArgs e)
        {
            time = new System.Windows.Forms.Timer();
            time.Interval = 5000;
            time.Tick += delegate
            {

                //Task mm = new Task(() =>
                //{
                //    button8_Click(null, null);
                //});
                //mm.Start();

                ThreadStart obj = new System.Threading.ThreadStart(startlisten);
                thread = new Thread(obj);
                thread.Start();
            };
            time.Start();
        }

        public void startlisten()
        {
            FlushClient fc = new FlushClient(substartlisten);
            this.Invoke(fc);
        }

        public void substartlisten()
        {
            button8_Click(null, null);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (time.Enabled)
                time.Stop();
        }

        #endregion


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
        string Locationleft = System.Configuration.ConfigurationManager.AppSettings["Locationleft"].ToString();
        string Locationright = System.Configuration.ConfigurationManager.AppSettings["Locationright"].ToString();
        string Locationleftto = System.Configuration.ConfigurationManager.AppSettings["Locationleftto"].ToString();
        string Locationrightto = System.Configuration.ConfigurationManager.AppSettings["Locationrightto"].ToString();

        string num = System.Configuration.ConfigurationManager.AppSettings["蛋车个数"].ToString();
        string taskidleft = ""; //请求AGV送料架到左蛋车
        string taskidleft2 = ""; //请求AGV到左蛋车取走料架
        string taskidright = ""; //请求AGV送料架到右蛋车
        string taskidright2 = ""; //请求AGV到右蛋车取走料架


        public class error
        {
            public List<int> error_codes;
        }


        public void SocketBack()
        {
            if (ping())
                sendplc("15902", 1);
            else
                sendplc("15902", 0);


            int value = readplc("15910");
            if (value == 0)
            { }
            else if (value == 1 && String.IsNullOrEmpty(taskidleft)) //请求AGV送料架到左蛋车
            {
                string newtask = buildnewtask("LEFT");//生成任务唯一id  读取p点信息和动作信息
                taskidleft = newtask;
                //string Location = Locationleft;
                //string Location = Blockleft;
                taskidleft = HTTP.SetOrder(Locationleftfrom, Locationleft);//下发任务
                //
            }
            else if (value == 2 && String.IsNullOrEmpty(taskidleft2)) //请求AGV到左蛋车取走料架
            {
                string newtask = buildnewtask("LEFT");//生成任务唯一id  读取p点信息和动作信息
                taskidleft2 = newtask;
                //string Location = Locationleft;
                //string Block = Blockleft2;
                taskidleft2 = HTTP.SetOrder(Locationleft, Locationleftto);//下发任务
            }
            else if (value == 3) //同时请求  不可能出现
            { }


            //根据已有任务列表查询任务状态
            if (!String.IsNullOrEmpty(taskidleft))
            {
                bool result = HTTP.SearchOrderNew(taskidleft);
                if (result)
                {
                    sendplc("15900", 1); //AGV送料架到左蛋车完成 MW15900.0
                    taskidleft = "";
                }
            }
            if (!String.IsNullOrEmpty(taskidleft2))
            {
                bool result = HTTP.SearchOrderNew(taskidleft2);
                if (result)
                {
                    sendplc("15900", 2);
                    taskidleft2 = "";
                }
            }



            if (num == "2")
            {
                int valueright = readplc("15911");
                if (valueright == 0)
                { }
                else if (valueright == 1 && String.IsNullOrEmpty(taskidright)) //请求AGV送料架到右蛋车
                {
                    string newtask = buildnewtask("RIGHT");//生成任务唯一id  读取p点信息和动作信息
                    taskidright = newtask;
                    //string Location = Locationright;
                    //string Block = Blockright;
                    taskidright = HTTP.SetOrder(Locationrightfrom, Locationright);//下发任务
                }
                else if (valueright == 2 && String.IsNullOrEmpty(taskidright2)) //请求AGV到右蛋车取走料架
                {
                    string newtask = buildnewtask("RIGHT");//生成任务唯一id  读取p点信息和动作信息
                    taskidright2 = newtask;
                    //string Location = Locationright;
                    //string Block = Blockright2;
                    taskidright2 = HTTP.SetOrder(Locationright, Locationrightto);//下发任务
                }
                else if (valueright == 3) //同时请求  不可能出现
                { }

                //根据已有任务列表查询任务状态
                if (!String.IsNullOrEmpty(taskidright))
                {
                    bool result = HTTP.SearchOrderNew(taskidright);
                    if (result)
                    {
                        sendplc("15901", 1);
                        taskidright = "";
                    }
                }
                if (!String.IsNullOrEmpty(taskidright2))
                {
                    bool result = HTTP.SearchOrderNew(taskidright2);
                    if (result)
                    {
                        sendplc("15901", 1);
                        taskidright2 = "";
                    }
                }
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

        public bool ping()
        {
            Ping ping = new Ping();
            PingReply pingReply = ping.Send(agvip);
            if (pingReply.Status == IPStatus.Success)
                return true;
            else
                return false;
        }

        //public string sendagv(string ip, string port, string head, string data)
        //{
        //    AGVclass aGVclass = new AGVclass(ip, port, head, data);
        //    string back = aGVclass.sendcmd();
        //    if (back == "")
        //        ProcessLog("agv链接失败");
        //    return back;
        //}



        public void sendplc(string addr, short value)
        {
            plc.plcNet.MW0[int.Parse(addr)] = value;
            plc.WriteShort(addr, value);
            //ProcessLog("写入plc成功，地址" + addr + "写入值" + value);
            int back = plc.ReadShort(addr);
            if (back == value)
                ProcessLog("写入plc成功，地址" + addr + "写入值" + back);
            else
                ProcessLog("写入值" + value + "失败,地址" + addr + "当前值" + back);
            //HslCommunicationClass communicationClass;
            //communicationClass = new HslCommunicationClass(ip);
            //communicationClass.Connect();
            //if (!communicationClass.isconnect)
            //{
            //    ProcessLog("写plc链接失败");
            //}
            //else
            //{
            //    string msg = "";
            //    communicationClass.WriteValue(addr, value, out msg);
            //}
        }

        public int readplc(string addr)
        {
            int back = plc.ReadShort(addr);

            ProcessLog("读取plc成功，地址" + addr + "返回值" + back);
            return back;
            //HslCommunicationClass communicationClass;
            //communicationClass = new HslCommunicationClass(ip);
            //communicationClass.Connect();
            //if (!communicationClass.isconnect)
            //{
            //    ProcessLog("读取plc链接失败");
            //    return false;
            //}
            //else
            //{

            //    bool back = false; string msg = "";
            //    communicationClass.ReadValue(addr, out back, out msg);
            //    ProcessLog("读取plc成功，地址" + addr + "返回值" + back + "信息" + msg);
            //    return back;
            //}
        }

        public void ProcessLog(string text)
        {
            int aa = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            //return;

            //string urlc = "http://" + agvip + ":" + agvport + "/orders?page=1&size=5&orderBy=createTime&orderMethod=descending";
            //HTTP.HttpGet(urlc);
            HTTP.SetOrderOld("taskleft02ww12", "LM6", "LM8", "blockleft02ww12");

            return;
            string url = "http://" + agvip + ":" + agvport + "/clearAllErrors";
            //string url2 = "http://" + agvip + ":" + agvport + "/robotsStatus";
            //HTTP.HttpGet(url2);

            List<int> ss = new List<int> { 52103 };
            error ssa = new error();
            ssa.error_codes = ss;
            string postDataStr = JsonConvert.SerializeObject(ssa);

            HTTP.HttpPost2(url, postDataStr);



            //plc = new PLC();
            //plc.RemoteIP = plcip;
            //plc.Initial(@"D:\AutoMationtest\AddressInPLC1.csv");

            //while (true)
            //{
            //    SocketBack();
            //}
        }
        private void button2_Click(object sender, EventArgs e)
        {
            bool result = HTTP.SearchOrderNew(taskidleft);
            //string url2 = "http://" + agvip + ":" + 8088 + "/robotsStatus";
            //HTTP.HttpGet(url2);
            return;
            //HTTP.SearchOrder("taskleft02ww12");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string siteid = HTTP.Getsite(0, 0, "VHP-07");
        }









        private void button4_Click(object sender, EventArgs e)
        {
            taskidleft = HTTP.SetOrder(Locationleftfrom, Locationleft);//下发任务

            //string siteid = HTTP.Getsite(0, 1, "VHP-07");// VHP-07、VHP-08   IL-TRIM-LM01
           
            //HTTP.SetOrderNew1(siteid, "Loc-1016");//AP1016 AP1019
        }

        private void button11_Click(object sender, EventArgs e)
        {
            taskidleft2 = HTTP.SetOrder(Locationleft, Locationleftto);//下发任务

            //HTTP.SetOrderNew2("Loc-1016", "TEMP");//AP1016 AP1019  // TEMP
        }

        private void button12_Click(object sender, EventArgs e)
        {
            taskidright = HTTP.SetOrder(Locationrightfrom, Locationright);//下发任务
            //string siteid = HTTP.Getsite(0, 1, "VHP-07");// VHP-07、VHP-08   IL-TRIM-LM01
            //HTTP.SetOrderNew1(siteid, "Loc-1019");//AP1016 AP1019
        }

        private void button13_Click(object sender, EventArgs e)
        {
            taskidright2 = HTTP.SetOrder(Locationright, Locationrightto);//下发任务
          
            //HTTP.SetOrderNew2("Loc-1019", "TEMP");//AP1016 AP1019  // TEMP
        }
    }
}
