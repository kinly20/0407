using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testdemo.UserControls
{
    public partial class subdisplaycontrol : UserControl
    {
        public subdisplaycontrol(string addr, string name)
        {
            InitializeComponent();
            lbaddr.Text = addr;
            lbname.Text = name;
        }
        public static int count = 0;
        public static string newname()
        {
            count++;
            return "cro" + count.ToString();
        }
        public void changgestatus(string status)
        {
            switch (status)
            {
                case "●":
                    lbstatus.ForeColor = Color.Black;
                    break;
                case "√":
                    lbstatus.ForeColor = Color.Green;
                    break;
                case "×":
                    lbstatus.ForeColor = Color.Red;
                    break;

            }
            lbstatus.Text = status;
        }

    }
}
