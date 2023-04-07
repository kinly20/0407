using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using System.Windows.Threading;
using System.ComponentModel;
using System.Data;
using System.Collections.ObjectModel;

namespace Dashboard.ViewModel
{
    class SubPage2ViewModel : CNotifyPropertyChange
    {

        DispatcherTimer timer;
        data data1 = new data();
        public SubPage2ViewModel()
        {
            refrashdata();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += timer1_Tick;
            timer.Start();
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            refrashdata();
        }

        public void cleardata()
        {
            _line1.Clear();
            _line2.Clear();
            _line3.Clear();
            _line4.Clear();
            _line5.Clear();
            _axisXLables.Clear();
        }

        public void adddata(DataTable dt, IChartValues line, DateTime dts, bool isFCT = false)
        {
            List<productchartdata> datas = new List<productchartdata>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                productchartdata data = new productchartdata();
                data.product = "";
                if (!isFCT)
                    data.productnum = int.Parse(dt.Rows[i]["productnum"].ToString());
                else
                    data.productnum = 1;
                data.time = Convert.ToDateTime(dt.Rows[i]["time"].ToString());

            }
            int hour = (System.DateTime.Now - dts).Hours + 1;

            for (int i = hour; i > 0; i--)
            {
                DateTime dtstart = System.DateTime.Now.AddHours(-i);
                DateTime dtend = System.DateTime.Now.AddHours(1 - i);
                line.Add(Convert.ToDouble(datas.Where(t => t.time > dtstart && t.time < dtend).ToList().Sum(t => t.productnum)));
            }

        }


        public void refrashdata()
        {
            //DataTable dt = StaticPlc.Getdtweekproduct();
            //DataTable dtday = StaticPlc.Getdtdayproduct();
            DataTable dtworkplannum = data1.SearchWorkPlanNum();//工作计划表
            string selecttimeplan = dtworkplannum.Rows[0][5].ToString();//选择的时段
            DataTable dttimeplan = SQLiteeDB.Getdatabyname(selecttimeplan);//时间安排分段表
            DateTime dts = Convert.ToDateTime(System.DateTime.Now.Date.ToShortDateString() + " " + dttimeplan.Rows[0][1].ToString().Replace(" ", "").Replace("：", ":"));
            //起始时间
            if (dts > System.DateTime.Now)
                dts = Convert.ToDateTime(System.DateTime.Now.Date.AddDays(-1).ToShortDateString() + " " + dttimeplan.Rows[0][1].ToString().Replace(" ", "").Replace("：", ":"));

            Dictionary<string, string> IOs = StaticPlc.GetIOIPInfos();
            DataTable dt1 = new DataTable();
            if (IOs.Count > 0)
                dt1 = data1.SearchProductdata(dts, System.DateTime.Now, IOs.Values.ToList()[0]);
            string product1 = "插件段"; //io信号 *1
            DataTable dt2 = new DataTable();
            if (IOs.Count > 1)
                dt2 = data1.SearchProductdata(dts, System.DateTime.Now, IOs.Values.ToList()[1]);
            string product2 = "焊接段";//io信号 *1
            DataTable dt3 = data1.SearchalldataFCT(dts, System.DateTime.Now, "");
            string product3 = "检测段";// FCT叠加
            DataTable dt4 = new DataTable();
            if (IOs.Count > 2)
                dt4 = data1.SearchProductdata(dts, System.DateTime.Now, IOs.Values.ToList()[2]);
            string product4 = "UV固化段";// io信号 *1
            DataTable dt5 = data1.SearchProductdataByType(dts, System.DateTime.Now, "焊锡机");//所有焊锡机
            string product5 = "装配段";//焊锡机 叠加



            cleardata();

            TopDataModel md = new TopDataModel(product1, product2, product3, product4, product5);
            _topdatamodel = md;



            adddata(dt1, _line1, dts);
            adddata(dt2, _line2, dts);
            adddata(dt3, _line3, dts, true);
            adddata(dt4, _line4, dts);
            adddata(dt5, _line5, dts);

            int hour = (System.DateTime.Now - dts).Hours + 1;

            for (int i = hour; i > 0; i--)
            {
                DateTime dtstart = System.DateTime.Now.AddHours(-i);
                _axisXLables.Add(dtstart.ToString());
            }
        }

        #region topdata

        private TopDataModel _topdatamodel;

        public TopDataModel TopDataModel
        {
            get
            {
                return _topdatamodel;
            }
            set
            {
                if (_topdatamodel != value)
                {
                    _topdatamodel = value;
                    this.NotifyPropertyChange("TopDataModel");
                }
            }
        }



        #endregion

        #region Chart1
        private IChartValues _line1 = new ChartValues<double>();
        public IChartValues Line1
        {
            get
            {
                return _line1;
            }
            set
            {
                if (_line1 != value)
                {
                    _line1 = value;
                    this.NotifyPropertyChange("Line1");
                }
            }
        }


        private IChartValues _line2 = new ChartValues<double>();
        public IChartValues Line2
        {
            get
            {
                return _line2;
            }
            set
            {
                if (_line2 != value)
                {
                    _line2 = value;
                    this.NotifyPropertyChange("Line2");
                }
            }
        }

        private IChartValues _line3 = new ChartValues<double>();
        public IChartValues Line3
        {
            get
            {
                return _line3;
            }
            set
            {
                if (_line3 != value)
                {
                    _line3 = value;
                    this.NotifyPropertyChange("Line3");
                }
            }
        }

        private IChartValues _line4 = new ChartValues<double>();
        public IChartValues Line4
        {
            get
            {
                return _line4;
            }
            set
            {
                if (_line4 != value)
                {
                    _line4 = value;
                    this.NotifyPropertyChange("Line4");
                }
            }
        }

        private IChartValues _line5 = new ChartValues<double>();
        public IChartValues Line5
        {
            get
            {
                return _line5;
            }
            set
            {
                if (_line5 != value)
                {
                    _line5 = value;
                    this.NotifyPropertyChange("Line5");
                }
            }
        }




        private List<string> _axisXLables = new List<string>();

        public List<string> AxisXLables
        {
            get { return _axisXLables; }
            set
            {
                if (_axisXLables != value)
                {

                    _axisXLables = value;
                    this.NotifyPropertyChange("AxisXLables");

                }
            }
        }
        #endregion




    }

    class TopDataModel : CNotifyPropertyChange
    {
        public TopDataModel(string value1, string value2, string value3, string value4, string value5)
        {
            _data1 = value1;
            _data2 = value2;
            _data3 = value3;
            _data4 = value4;
            _data5 = value5;
        }

        private string _data1 = "";

        public string Data1
        {
            get
            {
                return _data1;
            }
            set
            {
                if (_data1 != value)
                {
                    _data1 = value;
                    this.NotifyPropertyChange("Data1");
                }
            }
        }



        private string _data2 = "";

        public string Data2
        {
            get
            {
                return _data2;
            }
            set
            {
                if (_data2 != value)
                {
                    _data2 = value;
                    this.NotifyPropertyChange("Data2");
                }
            }
        }



        private string _data3 = "";

        public string Data3
        {
            get
            {
                return _data3;
            }
            set
            {
                if (_data3 != value)
                {
                    _data3 = value;
                    this.NotifyPropertyChange("Data3");
                }
            }
        }
        private string _data4 = "";

        public string Data4
        {
            get
            {
                return _data4;
            }
            set
            {
                if (_data4 != value)
                {
                    _data4 = value;
                    this.NotifyPropertyChange("Data4");
                }
            }
        }



        private string _data5 = "";

        public string Data5
        {
            get
            {
                return _data5;
            }
            set
            {
                if (_data5 != value)
                {
                    _data5 = value;
                    this.NotifyPropertyChange("Data5");
                }
            }
        }


    }


    public class productchartdata
    {
        public string product;
        public int productnum;
        public DateTime time;
    }

}
