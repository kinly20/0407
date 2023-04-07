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
using System.Threading;

namespace ICD.UserControls
{
    public partial class ErrorList : UserControl
    {
        HslCommunicationClass communicationClass;
        System.Threading.Thread thread;
        System.Windows.Forms.Timer time;
        private delegate void FlushClient();

        List<area> areas;
        List<classdrid> griddate = new List<classdrid>();
        List<classdrid> oldgriddate = new List<classdrid>();
        string _ip;
        public ErrorList(string ip, bool connect)
        {
            InitializeComponent();
            _ip = ip;

            loaddata();
            if (connect)
            {
                communicationClass = new HslCommunicationClass(ip);
                communicationClass.Connect();
                if (!communicationClass.isconnect)
                {
                    return;
                }
                else
                {
                    beginget();
                }
            }
            //DataGridViewRow drRow1 = new DataGridViewRow();
            //drRow1.CreateCells(this.dataGridView1);
            //drRow1.Cells[0].Value = 1;
            //drRow1.Cells[1].Value = "测试";
            //drRow1.Cells[2].Value = true;
            //this.dataGridView1.Rows.Add(drRow1);


            //this.dataGridView1.AllowUserToAddRows = false;//不自动产生最后的新行

            //griddate = new List<classdrid>();
            //newdata(1, "ss", System.DateTime.Now, System.DateTime.Now, "WA1");
            //this.dataGridView1.DataSource = ListToDt(griddate);


            //this.dataGridView1.Columns[0].HeaderText = "报警序号";
            //this.dataGridView1.Columns[0].Width = 100;
            //this.dataGridView1.Columns[1].HeaderText = "报警内容";
            //this.dataGridView1.Columns[1].Width = 300;
            //this.dataGridView1.Columns[2].HeaderText = "报警时间";
            //this.dataGridView1.Columns[2].Width = 160;


            //newdata(2, "ss", System.DateTime.Now, System.DateTime.Now, "WA1");
            //this.dataGridView1.DataSource = ListToDt(griddate);


        }


        public void loaddata()
        {
            areas = Class.ReadXML.GetXml("报警配置");
        }

        public class classdrid
        {
            public int sort;
            public string text;
            public DateTime stime;
            public DateTime etime;
            public string addr;
        }

        public void newdata(int sort, string text, DateTime stime, DateTime etime, string addr)
        {
            classdrid data = new classdrid();
            data.sort = sort;
            data.text = text;
            data.stime = stime;
            data.etime = etime;
            data.addr = addr;
            griddate.Add(data);
            oldgriddate.Add(data);
            Log.writelog("发生报警 " + text + " " + stime + " " + addr);
        }

        public void removedata(string addr)
        {
            griddate.RemoveAll(t => t.addr == addr);
            oldgriddate.FirstOrDefault(t => t.addr == addr).etime = System.DateTime.Now;
        }

        public DataTable ListToDt(List<classdrid> data)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("报警序号");
            dt.Columns.Add("报警内容");
            dt.Columns.Add("报警时间");
            for (int i = 0; i < data.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["报警序号"] = data[i].sort;
                dr["报警内容"] = data[i].text;
                dr["报警时间"] = data[i].stime;
                dt.Rows.Add(dr);
                dt.AcceptChanges();
            }
            return dt;
        }

        public DataTable ListToDtold(List<classdrid> data)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("报警序号");
            dt.Columns.Add("报警内容");
            dt.Columns.Add("报警时间");
            dt.Columns.Add("结束时间");
            dt.Columns.Add("状态");
            for (int i = 0; i < data.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["报警序号"] = data[i].sort;
                dr["报警内容"] = data[i].text;
                dr["报警时间"] = data[i].stime;
                dr["结束时间"] = data[i].etime == new DateTime() ? "" : data[i].etime;
                dr["状态"] = data[i].stime == new DateTime() ? "已处理" : "未处理";
                dt.Rows.Add(dr);
                dt.AcceptChanges();
            }
            return dt;
        }

        public void run()
        {
            if (communicationClass.isconnect)
                time.Start();
        }

        public void stop()
        {
            if (communicationClass != null && communicationClass.isconnect)
                time.Stop();
        }
        public void beginget()
        {
            time = new System.Windows.Forms.Timer();
            time.Interval = int.Parse(ICD.Class.ReadXML.getkeybyname("状态显示刷新间隔"));
            time.Tick += delegate
            {
                ThreadStart obj = new System.Threading.ThreadStart(startlisten);
                thread = new Thread(obj);
                thread.Start();
            };
            time.Start();
        }


        public void startlisten()
        {
            if (communicationClass.isconnect)
            {
                FlushClient fc = new FlushClient(substartlisten);
                this.Invoke(fc);
            }
        }


        public static int count = 0;
        public void substartlisten()
        {
            for (int i = 0; i < areas.Count; i++)
            {
                for (int j = 0; j < areas[i].points.Count; j++)
                {
                    string addr = areas[i].points[j].addr;
                    string text = areas[i].points[j].name;

                    short back = 0; string msg = string.Empty;
                    communicationClass.ReadValue(addr, out back, out msg);
                    if (back == 1)//报警状态
                    {
                        if (griddate.Where(t => t.addr == addr).Count() == 0)
                        {
                            count++;
                            newdata(count, text, System.DateTime.Now, new DateTime(), addr);
                        }
                    }
                    else if (back == 0)
                    {
                        if (griddate.Where(t => t.addr == addr).Count() == 1)
                        {
                            removedata(addr);
                        }
                    }
                }
            }
        }

        private void bt_reset_Click(object sender, EventArgs e)
        {
            string msg = string.Empty; int value = 0;
            if (dataGridView1.SelectedRows != null && dataGridView1.SelectedCells.Count != 0)
            {
                string sortid = dataGridView1.SelectedCells[0].ToString();
                if (dataGridView1.SelectedRows != null)
                    if (communicationClass.isconnect)
                    {
                        classdrid one = griddate.FirstOrDefault(t => t.sort == int.Parse(sortid));
                        communicationClass.WriteValue(one.addr, value, out msg);
                        Log.writelog("处理报警 " + one.text + " " + one.stime + " " + one.addr);
                    }
            }
            else
                MessageBox.Show("请先选中需要处理的报警");
        }

        private void bt_search_Click(object sender, EventArgs e)
        {
            DateTime time = dateTimePicker1.Value;
            DataTable dt = ListToDtold(oldgriddate.Where(t => t.stime.Date == time.Date).ToList());
            dataGridView2.DataSource = dt;

        }
    }

}
