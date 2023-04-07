using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICD.UserControls;
using ICD.Class;
using ICD.ICDUserControls;

namespace ICD
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
            ParamEdit pe = new ParamEdit();
            tabPage1.Controls.Add(pe);

            SysSetting.IOmonitor io = new SysSetting.IOmonitor();
            tabPage2.Controls.Add(io);

            //NeedleParam np = new NeedleParam();
            //tabPage3.Controls.Add(np);
            tabPage3.Controls.Add(new SysSetting.AxisControlScrew());
            //tabPage5.Controls.Add(new SysSetting.AxisControlGetliquid());
            //tabPage6.Controls.Add(new SysSetting.Connect());
            //tabPage7.Controls.Add(new SysSetting.IOmonitor());

            TestTubeSetting tts = new TestTubeSetting();
            tabPage4.Controls.Add(tts);

            RecordSearch rs = new RecordSearch();
            tabPage6.Controls.Add(rs);

            //参数配置
            //sd3 = new StatusDisplay("127.0.0.1", "参数配置", true);//3
            HoneywellScanner hs = new HoneywellScanner();
            tabPage7.Controls.Add(hs);

            Auth au = new Auth();
            tabPage8.Controls.Add(au);




            
           

            //LOG查询
            //LogSearch ls = new LogSearch();
            //tabPage7.Controls.Add(ls);
          
        }


        private void TimerBottomScan_Tick(object sender, EventArgs e)
        //界面底部信息监控   100ms
        {
            labData.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss dddd");
            //if (!sd.getconnect())
            //{
            //    this.bt_connectstatus.Image = global::ICD.Resource1.Delete;
            //}
            //else
            //{
            //    this.bt_connectstatus.Image = global::ICD.Resource1.accept;
            //}
        }


        private void BtnKeyboard_Click(object sender, EventArgs e)
        {
            bt_keyboard.Enabled = false;
            ICD.Class.Operate operate = new Class.Operate();
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
                //if (select == "0")
                //{
                //    sd.run();sd2.stop(); sd3.stop();
                //}
                //else if (select == "1")
                //{
                //    sd.stop(); sd2.run(); sd3.stop();
                //}
                //else if (select == "2")
                //{
                //    sd.stop(); sd2.stop(); sd3.run();
                //}
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
            //ICD.Class.WriteXML.writexml(areas);
        }



    }
}
