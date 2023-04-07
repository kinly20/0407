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
    public partial class subsendcmdcontrol : UserControl
    {
        public event EventHandler<addrdouble> SendCmd;
        public string _addr;
        public string _cmd;
        public string _cmdtype;
        public subsendcmdcontrol(string addr, string name, string cmd, string cmdtype)
        {
            InitializeComponent();
            lbaddr.Text = addr;
            lbname.Text = name;
            _addr = addr;
            _cmd = cmd;
            _cmdtype = cmdtype;
            bt_send.Tag = cmd + "," + cmdtype;
        }

        public static int count = 0;
        public static string newname()
        {
            count++;
            return "send" + count.ToString();
        }

        private void bt_send_Click(object sender, EventArgs e)
        {
            bt_send.Enabled = false;
            string tag = bt_send.Tag.ToString(); string msg = string.Empty;

            addrdouble eventArgs = new addrdouble();
            eventArgs.addr = _addr; eventArgs.value = double.Parse(_cmd);
            SendCmd(null, eventArgs);
            bt_send.Enabled = true;

            //if (_cmdtype == "string" || _cmdtype == "")
            //    communicationClass.WriteValue(_addr, _cmd, out msg);
            //else if (_cmdtype == "int")
            //    communicationClass.WriteValue(_addr, int.Parse(_cmd), out msg);
            //else if (_cmdtype == "float")
            //    communicationClass.WriteValue(_addr, float.Parse(_cmd), out msg);
            //else if (_cmdtype == "double")
            //    communicationClass.WriteValue(_addr, double.Parse(_cmd), out msg);
            //else if (_cmdtype == "short")
            //    communicationClass.WriteValue(_addr, Convert.ToInt16(_cmd), out msg);
            //else if (_cmdtype == "byte")
            //    communicationClass.WriteValue(_addr, Convert.ToByte(_cmd), out msg);


        }

      
       
    }
}
