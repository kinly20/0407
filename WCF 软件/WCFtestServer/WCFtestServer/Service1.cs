using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Threading;
using System.Timers;

namespace WCFtestServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public partial class Service1 : ServiceBase, IPublisher, IDisposable
    {
        private System.Timers.Timer Timer1;
        //线程
        private Thread DataResumeThread = null;
        private bool isDataResumeThread = false;
        private Log alog = new Log();
        //定义回调客户端集合
        public static List<IPublisherEvents> ClientCallbackList { get; set; }

        //寄宿服务
        private ServiceHost _host = null;


        private void ProcessLog(string log)
        {
            alog.msg = log;
            alog.dte = DateTime.Now;
            alog.WriteLog();
        }
        public Service1()
        {
            InitializeComponent();
            ClientCallbackList = new List<IPublisherEvents>();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (_host != null)
                {
                    _host.Close();
                    IDisposable host = _host as IDisposable;
                    host.Dispose();
                }

                _host = new ServiceHost(typeof(WCFtestServer.Service1));
                _host.Open();
                ProcessLog("信息服务开启成功");

                Timer1 = new System.Timers.Timer();
                Timer1.Interval = 1000;
                Timer1.Enabled = true;
                Timer1.Elapsed += new ElapsedEventHandler(trInit_Tick);
                Timer1.Start();
            }
            catch (Exception ex)
            {
                ProcessLog(ex.ToString());
            }


        }

        private void trInit_Tick(object sender, EventArgs e)//服务正式进程
        {
            if (!isDataResumeThread)
            {
                DataResumeThread = new Thread(new ThreadStart(delegate { EnergyDataResume(); }));
                DataResumeThread.Name = "DataResumeThread";
                isDataResumeThread = true;
                DataResumeThread.IsBackground = true;

                if (!DataResumeThread.IsAlive)
                {
                    DataResumeThread.Start();
                }
            }

            Timer1.Enabled = false;
        }

        private void EnergyDataResume()
        {
            while (isDataResumeThread)
            {

                PublishMSG(System.DateTime.Now.ToLongTimeString());
                Thread.Sleep(10000);//
            }
        }

        protected override void OnStop()
        {
            if (_host != null)
            {
                _host.Close();
                IDisposable host = _host as IDisposable;
                host.Dispose();
            }
        }

        //发布消息
        private void PublishMSG(string msg)
        {
            Timer1.Enabled = false;
            try
            {
                var list = WCFtestServer.Service1.ClientCallbackList;
                if (list == null || list.Count == 0)
                    return;
                lock (list)
                {
                    foreach (var client in list)
                    {
                        client.PublishMessage(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessLog("发送错误:" + ex.ToString());
            }
            ProcessLog("发送信息:" + msg);
            Timer1.Enabled = true;
        }

        //实现订阅
        public void Subscriber(string clientID, string clientName)
        {
            try
            {
                var client = OperationContext.Current.GetCallbackChannel<IPublisherEvents>();
                var sessionid = OperationContext.Current.SessionId;
                //MessageBox.Show(string.Format("客户端{0} 开始订阅消息。", clientName));
                ProcessLog(string.Format("客户端{0} 开始订阅消息。", clientName));
                OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);
                ClientCallbackList.Add(client);
            }
            catch (Exception ex)
            {
                ProcessLog("实现订阅错误" + ex.ToString());
            }

        }


        //取消订阅
        public void UnSubscriber(string clientID, string clientName)
        {
            try
            {
                var client = OperationContext.Current.GetCallbackChannel<IPublisherEvents>();
                var sessionid = OperationContext.Current.SessionId;
                ProcessLog(string.Format("客户端{0}取消订阅消息", clientName));
                OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);
                ClientCallbackList.Remove(client);
            }
            catch (Exception ex)
            {
                ProcessLog("取消订阅错误:" + ex.ToString());
            }
        }


        //关闭通道，移除回调客户端
        void Channel_Closing(object sender, EventArgs e)
        {
            lock (ClientCallbackList)
            {
                ClientCallbackList.Remove((IPublisherEvents)sender);
            }
        }
    }
}
