using System;
using System.Windows.Forms;
using System.ServiceModel;
using System.Threading;
using Subscriber.WcfDuplexService;

namespace Subscriber
{
    public partial class FormSubscriber : Form, IPublisherCallback
    {
        PublisherClient proxy = null;
   
        public FormSubscriber()
        {
             InitializeComponent();
             InstanceContext instance = new InstanceContext(this);
             proxy = new PublisherClient(instance);

             btn_cancle.Enabled = false;
        }

        public void PublishMessage(string message)
        {
            string msg = string.Format("来自服务端的广播消息 : {0}",message);
            lst_getMsg.Items.Add(msg);
         }

        //订阅消息
        private void btn_ok_Click(object sender, EventArgs e)
        {
            btn_ok.Enabled = false;
            btn_cancle.Enabled = true;

            string ClientID = System.Guid.NewGuid().ToString();
            string ClientName = this.textBox1.Text;
            proxy.Subscriber(ClientID, ClientName);              
        }

        //取消订阅
        private void btn_cancle_Click(object sender, EventArgs e)
        {
            btn_ok.Enabled = true;
            btn_cancle.Enabled = false;

            string ClientID = System.Guid.NewGuid().ToString();
            string ClientName = this.textBox1.Text;
            proxy.UnSubscriber(ClientID, ClientName);
        }
    }
}
