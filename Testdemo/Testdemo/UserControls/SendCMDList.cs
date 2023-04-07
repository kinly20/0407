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
    public partial class SendCMDList : UserControl
    {
        HslCommunicationClass communicationClass;
        public string _ip;
        List<area> areas;
        public bool isconnect = false;
        public SendCMDList(string ip, bool connect)
        {
            InitializeComponent();
            _ip = ip;

            loaddata();

            if (!connect)
            {
                communicationClass = new HslCommunicationClass(ip);
                communicationClass.Connect();
                if (!communicationClass.isconnect)
                {
                    //MessageBox.Show("connect fail");
                    isconnect = false;
                    return;
                }
                else
                {
                    isconnect = true;
                }
            }
        }

        public void loaddata()
        {
            areas = Class.ReadXML.GetXml("命令下发");

            for (int i = 0; i < areas.Count; i++)
            {
                TabPage tabpage = new TabPage();

                tabpage.Text = areas[i].name;
                for (int j = 0; j < areas[i].points.Count; j++)
                {
                    subsendcmdcontrol con = new subsendcmdcontrol(areas[i].points[j].addr, areas[i].points[j].name, areas[i].points[j].cmd, areas[i].points[j].cmdtype);
                    con.SendCmd += sendcmd;
                    con.Name = subdisplaycontrol.newname();
                    if (j % 2 == 0)
                        con.Location = new System.Drawing.Point(1, j / 2 * 37);
                    else
                        con.Location = new System.Drawing.Point(330, j / 2 * 37);


                    tabpage.Controls.Add(con);
                }
                tabControl1.Controls.Add(tabpage);

            }
        }

        public void sendcmd(object sender, addrdouble e)
        {
            if (!isconnect || !communicationClass.isconnect)
            {
                MessageBox.Show("connect fail!");
            }
            else
            {
                string msg = "";
                communicationClass.WriteValue(e.addr, e.value, out msg);
            }
        }
    }
}
