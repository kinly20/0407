using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testdemo.UserControls;
using Testdemo.Class;

namespace Testdemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timerBottomScan.Enabled = true;
            panelTop.BringToFront();
            panelLeft.BringToFront();
            panelBelow.BringToFront();
            buildtab();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult re = MessageBox.Show("此操作将会退出程序\n确认退出请选<是>", "退出程序", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (re != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }
            //operate.OperateOSK();
            //ExitProgram();
            Environment.Exit(0);
        }

        public StatusDisplay sd; public StatusDisplay sd2; public StatusDisplay sd3;//几个实时监控
         

        private void buildtab()
        {
            //string ip = Testdemo.Class.ReadXML.getkeybyname("IP");
            //bool connect = false;
            ////状态显示
            //sd = new StatusDisplay(ip, "状态显示", true);//1
            //tabPage1.Controls.Add(sd);
            //if (!sd.isconnect)
            //{
            //    //MessageBox.Show("connect fail");
            //    connect = false;
            //    this.bt_connectstatus.Image = global::Testdemo.Resource1.Delete;
            //}
            //else
            //{
            //    connect = true;
            //    this.bt_connectstatus.Image = global::Testdemo.Resource1.accept;
            //}
            ////IO监控
            //sd2 = new StatusDisplay(ip, "IO监控", true);//2
            //tabPage2.Controls.Add(sd2);
            //sd2.stop();
            ////参数配置
            //sd3 = new StatusDisplay(ip, "参数配置", true);//3
            //tabPage3.Controls.Add(sd3);
            //sd3.stop();
            ////命令发送 后期  配方加载
            //Recipe rc = new Recipe(ip);
            //tabPage4.Controls.Add(rc);
            ////SendCMDList scl = new SendCMDList(ip,connect);
            ////tabPage4.Controls.Add(scl);
            ////报警信息
            //ErrorList el = new ErrorList(ip, true);//4
            //tabPage5.Controls.Add(el);
            ////el.stop();
            ////记录查询

            ////机械手

            ////LOG查询
            //LogSearch ls = new LogSearch();
            //tabPage7.Controls.Add(ls);
            //单点调试
            PointDebug pd = new PointDebug();
            tabPage8.Controls.Add(pd);

            //用户登录

            //主页面
        }


        private void TimerBottomScan_Tick(object sender, EventArgs e)
        //界面底部信息监控   100ms
        {
            labData.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss dddd");
            //if (!sd.getconnect())
            //{
            //    this.bt_connectstatus.Image = global::Testdemo.Resource1.Delete;
            //}
            //else
            //{
            //    this.bt_connectstatus.Image = global::Testdemo.Resource1.accept;
            //}
        }


        private void BtnKeyboard_Click(object sender, EventArgs e)
        {
            bt_keyboard.Enabled = false;
            Testdemo.Class.Operate operate = new Class.Operate();
            operate.OperateOSK(true);
            bt_keyboard.Enabled = true;
        }



        private void TopButton_Click(object sender, EventArgs e)
        {
            if (sender is Button currentBtn)
            {
                switch (Convert.ToInt16(currentBtn.Tag))
                {
                    case 11:
                        WindowState = FormWindowState.Minimized;
                        break;
                    case 12:
                        Close();
                        break;
                }
            }
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            if (sender is Button currentBtn)
            {
                foreach (Button btn in panelLeft.Controls)
                {
                    btn.BackColor = Color.WhiteSmoke;
                }
                string select = currentBtn.AccessibleDescription;
                tabControlMain.SelectTab(int.Parse(select));
                if (select == "0")
                {
                    sd.run();sd2.stop(); sd3.stop();
                }
                else if (select == "1")
                {
                    sd.stop(); sd2.run(); sd3.stop();
                }
                else if (select == "2")
                {
                    sd.stop(); sd2.stop(); sd3.run();
                }
                currentBtn.BackColor = Color.GreenYellow;

            }
        }

        private void bt_connectstatus_Click(object sender, EventArgs e)
        {
            bt_connectstatus.Enabled = false;

            tabPage1.Controls.Clear();
            tabPage2.Controls.Clear();
            tabPage3.Controls.Clear();
            tabPage4.Controls.Clear();
            tabPage5.Controls.Clear();
            tabPage6.Controls.Clear();
            tabPage7.Controls.Clear();
            tabPage8.Controls.Clear();
            buildtab();
            bt_connectstatus.Enabled = true;
        }

        private void bt_user_Click(object sender, EventArgs e)
        {
            //List<area>  areas = Class.ReadXML.GetXml("配方设置");
            //Testdemo.Class.WriteXML.writexml(areas);
        }
    }
}
