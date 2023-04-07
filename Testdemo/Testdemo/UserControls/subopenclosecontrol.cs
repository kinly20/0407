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

namespace Testdemo.UserControls
{
    public partial class subopenclosecontrol : UserControl
    {
        public event EventHandler<addrint> SendCmd;
        public string _addr;
        public subopenclosecontrol(string addr, string name)
        {
            InitializeComponent();
            lb_name.Text = name;
            _addr = addr;
        }

        public static int count = 0;
        public static string newname()
        {
            count++;
            return "openclose" + count.ToString();
        }

        private void bt_open_Click(object sender, EventArgs e)
        {
            bt_open.Enabled = false;
            sendcmd("1");
            bt_open.Enabled = true;
        }

        private void bt_close_Click(object sender, EventArgs e)
        {
            bt_close.Enabled = false;
            sendcmd("0");
            bt_close.Enabled = true;
        }

        private void sendcmd(string data)
        {
            addrint eventArgs = new addrint();
            eventArgs.addr = _addr; eventArgs.value = int.Parse(data);
            SendCmd(null, eventArgs);
        }

        public void changgestatus(string status)
        {
            switch (status)
            {
                case "open":
                    bt_open.BackColor = Color.GreenYellow;
                    bt_close.BackColor = Color.WhiteSmoke;
                    break;
                case "close":
                    bt_close.BackColor = Color.GreenYellow;
                    bt_open.BackColor = Color.WhiteSmoke;
                    break;
                case "disconect":
                    bt_close.BackColor = Color.WhiteSmoke;
                    bt_open.BackColor = Color.WhiteSmoke;
                    break;
            }
        }
    }


}
