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
    public partial class subeditdatacontrol : UserControl
    {
        public event EventHandler<addrdouble> SendCmd;
        public string _addr;
        public string _pagename;

        public subeditdatacontrol(string addr, string name, string cmd, string unit, string pagename)
        {
            InitializeComponent();
            _addr = addr;
            _pagename = pagename;
            tb_data.Text = cmd;
            lb_name.Text = name;
            lb_unit.Text = unit;
        }

        public static int count = 0;
        public static string newname()
        {
            count++;
            return "edit" + count.ToString();
        }

        public void changgedata(string data)
        {
            tb_data.Text = data;
        }

        public string getdata()
        {
            return tb_data.Text;
        }

        public string getunit()
        {
            return lb_unit.Text;
        }

        private void tb_data_MouseClick(object sender, MouseEventArgs e)
        {
            FormNumInput form = new FormNumInput();
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                string data = form.data;
                tb_data.Text = data;

                //参数配置下发命令 配方统一父界面下发
                if (_pagename == "参数配置")
                    sendcmd(data);
            }
        }

        public void sendcmd(string data)
        {
            if (data == "???")
                return;
            addrdouble eventArgs = new addrdouble();
            eventArgs.addr = _addr; eventArgs.value = double.Parse(data);
            SendCmd(null, eventArgs);
        }
    }
}
