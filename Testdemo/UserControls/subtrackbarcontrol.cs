using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICD.Class;

namespace ICD.UserControls
{
    public partial class subtrackbarcontrol : UserControl //纯定制界面
    {
        public event EventHandler<addrint> SendCmd;
        public string _addr1;
        public string _addr2;
        public string _addr3;
        public string _standard;

       
        public subtrackbarcontrol(string addr1, string addr2, string addr3, string standard)
        {
            InitializeComponent();
           
            _addr1 = addr1;
            _addr2 = addr2;
            _addr3 = addr3;
            _standard = standard;
        }

        public static int count = 0;
        public static string newname()
        {
            count++;
            return "track" + count.ToString();
        }

        public void changgeselect(bool data)
        {
            if (data)
                bt_select.BackColor = Color.GreenYellow;
            else
                bt_select.BackColor = Color.WhiteSmoke;
        }

        public void changgedataspeed(string data)
        {
            tb_speed.Text = data;
			if(data!="???")
               trackBarspeed.Value = int.Parse(data)/ int.Parse(_standard) * 10;
        }

        public void changgedatalocation(string data)
        {
            tb_location.Text = data;
        }

        private void btselect_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            string name = btn.Parent.Parent.Name;
            name = name.Substring(5, name.Length - 5);
            sendcmd(_addr1,int.Parse(name));
               
           
        }

        private void trackBarspeed_Scroll(object sender, EventArgs e)
        {
            int value = trackBarspeed.Value;
            sendcmd(_addr2,value * int.Parse(_standard) / 10);
           
        }
		
		 public void sendcmd(string addr,int data)
        {

            addrint eventArgs = new addrint();
            eventArgs.addr = addr; eventArgs.value = data;
            SendCmd(null, eventArgs);
        }
    }
}
