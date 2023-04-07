using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ServiceModel;
using System.Threading;

namespace Publisher
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public partial class FormPublisher : Form, IPublisher, IDisposable
    {
        //定义回调客户端集合
        public static List<IPublisherEvents> ClientCallbackList { get; set; }

        public FormPublisher()
        {
            InitializeComponent();
            ClientCallbackList = new List<IPublisherEvents>();
        }

        //寄宿服务
        private ServiceHost _host = null;
        private void FormPublisher_Load(object sender, EventArgs e)
        {
            _host = new ServiceHost(typeof(Publisher.FormPublisher));
            _host.Open();
            this.label1.Text = "信息服务开启成功.";
        }

        //关闭窗体
        private void FormPublisher_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_host != null)
            {
                _host.Close();
                IDisposable host = _host as IDisposable;
                host.Dispose();
            }
        }

        //发布消息
        private void btn_Publish_Click(object sender, EventArgs e)
        {

            var list = Publisher.FormPublisher.ClientCallbackList;
            if (list == null || list.Count == 0)
                return;
            lock (list)
            {
                foreach (var client in list)
                {
                    client.PublishMessage(this.txt_Message.Text);
                }
            }

        }


        //实现订阅
        public void Subscriber(string clientID, string clientName)
        {
            var client = OperationContext.Current.GetCallbackChannel<IPublisherEvents>();
            var sessionid = OperationContext.Current.SessionId;
            MessageBox.Show(string.Format("客户端{0} 开始订阅消息。", clientName));
            OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);
            ClientCallbackList.Add(client);

        }


        //取消订阅
        public void UnSubscriber(string clientID, string clientName)
        {
            var client = OperationContext.Current.GetCallbackChannel<IPublisherEvents>();
            var sessionid = OperationContext.Current.SessionId;
            MessageBox.Show(string.Format("客户端{0}取消订阅消息", clientName));
            OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);
            ClientCallbackList.Remove(client);
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
