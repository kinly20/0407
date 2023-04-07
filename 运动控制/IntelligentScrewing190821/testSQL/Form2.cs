using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace testSQL
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            GetIP();
        }
        
        XHwritetoSQL.WriteToSQL wts;
        private void GetIP()
        {
            string hostName = Dns.GetHostName();//本机名   
            System.Net.IPAddress[] addressList = Dns.GetHostByName(hostName).AddressList;//会警告GetHostByName()已过期，我运行时且只返回了一个IPv4的地址   
            //System.Net.IPAddress[] addressList = Dns.GetHostAddresses(hostName);//会返回所有地址，包括IPv4和IPv6   
            foreach (IPAddress ip in addressList)
            {
               comboBox1.Items.Add(ip.ToString());
               
            }
            if(comboBox1.Items.Count>0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            wts = new XHwritetoSQL.WriteToSQL(comboBox1.Text);
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> list=new List<string>();
            list.Add("自动线2");//1
             list.Add("设备1");//2
             list.Add("工艺1");//3//1
             list.Add("产品");//4
             list.Add("123456");//5
             list.Add("111");//6
             list.Add("1");//7
             list.Add(DateTime.Now.ToString());//8
             list.Add(DateTime.Now.ToString());//9
             list.Add("10");//10
             list.Add("9527");//11
             list.Add("A");//12
             list.Add("B");//13
             list.Add("C");//14
             list.Add("1");//15
             list.Add("2");//16
             list.Add("3");//17
            list.Add("XX");//18
            MessageBox.Show( wts.InsertData(list, XHwritetoSQL.DeviceType.ANTPortConnectorToInstallThDevice));
        }

    }
}
