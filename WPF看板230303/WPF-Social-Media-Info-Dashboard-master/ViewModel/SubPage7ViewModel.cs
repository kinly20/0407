using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.ComponentModel;
using System.Data;
using Dashboard;
using System.Collections.ObjectModel;

namespace Dashboard.ViewModel
{
    class SubPage7ViewModel : CNotifyPropertyChange
    {
        data data1 = new data();
        DispatcherTimer timer;
        readonly string Machine = System.Configuration.ConfigurationManager.AppSettings["首页产量机型"].ToString();
        public SubPage7ViewModel()
        {
            loaddata();
            loadsubdata();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer1_Tick;
            timer.Start();
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            loaddata();
        }

        public void loaddata()
        {
            try
            {
                DateTime timenow = System.DateTime.Now;
                string date = timenow.Date.ToString("yyyy年MM月dd日");
                string time = timenow.ToString("HH:mm:ss");

                TimeModel ti = new TimeModel(date, time);
                TimeModel = ti;

                if (timenow.Second % 10 == 0)//10秒一次
                {
                    loadsubdata();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }


        public void loadsubdata()
        {
            try
            {
                DateTime timenow = System.DateTime.Now;
                List<bool> datas = StaticPlc.Getstatus();
                DataTable dtproducttimeinfo = StaticPlc.getdtproducttime();
                //Dictionary<string, string> dt = StaticPlc.GetIPInfos();



                DataTable dtworkplannum = data1.SearchWorkPlanNum();//工作计划表
                DataTable dtworkproductplan = data1.SearchWorkProductPlan();//产品计划表
                string selecttimeplan = dtworkplannum.Rows[0][5].ToString();//选择的时段
                DataTable dttimeplan = SQLiteeDB.Getdatabyname(selecttimeplan);//时间安排分段表
              
                DateTime dts = Convert.ToDateTime(System.DateTime.Now.Date.ToShortDateString() + " " + dttimeplan.Rows[0][1].ToString().Replace(" ", "").Replace("：", ":"));
                DateTime dte = Convert.ToDateTime(System.DateTime.Now.Date.ToShortDateString() + " " + dttimeplan.Rows[0][2].ToString().Replace(" ", "").Replace("：", ":"));//非24小时
                DataTable dttrueproducttoday = new DataTable();
                //int dayflag = 0;
                if (dte > dts)//不跨天
                { }
                else if (timenow < dts)//跨天且已经在第二天
                {
                    //dayflag = 1;
                    dts = Convert.ToDateTime(System.DateTime.Now.Date.AddDays(-1).ToShortDateString() + " " + dttimeplan.Rows[0][1].ToString().Replace(" ", "").Replace("：", ":"));
                }
                else//跨天还在当天
                {
                    //dayflag = 2;
                    dte = Convert.ToDateTime(System.DateTime.Now.Date.AddDays(1).ToShortDateString() + " " + dttimeplan.Rows[0][2].ToString().Replace(" ", "").Replace("：", ":"));
                }
                dttrueproducttoday = data1.SearchProductdata(dts, dte, Machine);//测试数据 实际当前产量
                int trueproducttoday = 0;//测试数据 实际当前产量
                for (int i = 0; i < dttrueproducttoday.Rows.Count; i++)
                {
                    trueproducttoday += int.Parse(dttrueproducttoday.Rows[i]["productnum"].ToString());
                }



                int totalnum = 0;//计划总产量
                string productnow = "";//当前产品
                string meternow = "";//当前节拍
                string productnext = "";//下个产品
                ProductplanModels = new ObservableCollection<ProductPlanModel>();//生产产品计划表

                for (int i = 0; i < dtworkproductplan.Rows.Count; i++)
                {
                    string id = dtworkproductplan.Rows[i][0].ToString();
                    string name = dtworkproductplan.Rows[i][1].ToString();
                    int num = int.Parse(dtworkproductplan.Rows[i][2].ToString());
                    totalnum += num;
                    DataRow[] drs = dtproducttimeinfo.Select("type ='" + name + "'");
                    string meter = drs.Length > 0 ? drs[0]["meter"].ToString() : "10";

                    int numnow = ProductplanModels.Sum(t => t.Productplannum);
                    int numtrue = 0;
                    if (trueproducttoday > numnow + num)//已经超出该计划
                        numtrue = num;
                    else if (trueproducttoday < numnow)//未抵达该计划
                        numtrue = 0;
                    else//该计划生产中
                    {
                        numtrue = trueproducttoday - numnow;//该计划已经生产个数
                        productnow = name;//当前产品
                        meternow = meter;//当前节拍
                        productnext = dtworkproductplan.Rows.Count > (i + 1) ? dtworkproductplan.Rows[i + 1][1].ToString() : "";//下个产品
                    }

                    //计算当前到哪个产品 
                    ProductPlanModel ppm = new ProductPlanModel(id, (i + 1).ToString(), name, num, numtrue, meter, num * Convert.ToDouble(meter)); ;
                    ProductplanModels.Add(ppm);
                }




                double nowtotalproduct = getplantotalproduct(timenow, dttimeplan);//实时计划产量

                TimeplanModels = new ObservableCollection<TimePlanModel>();
                for (int i = 0; i < dttimeplan.Rows.Count; i++)
                {
                    string id = dttimeplan.Rows[i][0].ToString();
                    string times = dttimeplan.Rows[i][1].ToString().Replace(" ", "").Replace("：", ":");
                    string timee = dttimeplan.Rows[i][2].ToString().Replace(" ", "").Replace("：", ":");
                    DateTime timestart = Convert.ToDateTime(System.DateTime.Now.Date.ToShortDateString() + " " + times);
                    DateTime timeend = Convert.ToDateTime(System.DateTime.Now.Date.ToShortDateString() + " " + timee);
                    double planproducts = getplantotalproduct(timestart, dttimeplan);
                    double planproducte = getplantotalproduct(timeend, dttimeplan);
                    double productes = planproducte - planproducts;
                    //计算时段产量 未完成
                    DataRow[] rows = null;
                    int data = 0;

                    if (timestart > dte)
                        timestart = Convert.ToDateTime(System.DateTime.Now.Date.AddDays(-1).ToShortDateString() + " " + times);
                    if (timestart < dts)
                        timestart = Convert.ToDateTime(System.DateTime.Now.Date.AddDays(1).ToShortDateString() + " " + times);
                    if (timeend > dte)
                        timeend = Convert.ToDateTime(System.DateTime.Now.Date.AddDays(-1).ToShortDateString() + " " + timee);
                    if (timeend < dts)
                        timeend = Convert.ToDateTime(System.DateTime.Now.Date.AddDays(1).ToShortDateString() + " " + timee);
                    rows = dttrueproducttoday.Select("time >'" + timestart.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "' and time <'" + timeend.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "'");

                    for (int j = 0; j < rows.Length; j++)
                    {
                        data += int.Parse(rows[j]["productnum"].ToString());
                    }

                    TimePlanModel ppm = new TimePlanModel(id, (i + 1).ToString(), times, timee, int.Parse(productes.ToString().Split('.')[0]), data); ;
                    TimeplanModels.Add(ppm);
                }

                MainModel main = new MainModel(dtworkplannum.Rows[0][1].ToString(),
                   dtworkplannum.Rows[0][2].ToString(), dtworkplannum.Rows[0][3].ToString(),
                   dtworkplannum.Rows[0][4].ToString(), selecttimeplan, totalnum.ToString(),
                  nowtotalproduct / totalnum, trueproducttoday / totalnum,
                   productnow, productnext, meternow, nowtotalproduct, trueproducttoday);
                CurbModel = main;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        private TimeModel _timeModel;

        public TimeModel TimeModel
        {
            get
            {
                return _timeModel;
            }
            set
            {
                _timeModel = value;
                this.NotifyPropertyChange("TimeModel");
            }
        }


        private MainModel _curbModel;

        public MainModel CurbModel
        {
            get
            {
                return _curbModel;
            }
            set
            {
                _curbModel = value;
                this.NotifyPropertyChange("CurbModel");
            }
        }

        private ObservableCollection<ProductPlanModel> _productplanModels;

        public ObservableCollection<ProductPlanModel> ProductplanModels
        {
            get { return _productplanModels; }
            set
            {
                _productplanModels = value;
                this.NotifyPropertyChange("ProductplanModels");
            }
        }

        private ObservableCollection<TimePlanModel> _timeplanModels;

        public ObservableCollection<TimePlanModel> TimeplanModels
        {
            get { return _timeplanModels; }
            set
            {
                _timeplanModels = value;
                this.NotifyPropertyChange("TimeplanModels");
            }
        }

        //得到实时预计产量
        public double getplantotalproduct(DateTime dtime, DataTable dttimeplan)
        {
            try
            {
                DateTime dttimestart = Convert.ToDateTime(System.DateTime.Now.Date.ToShortDateString() + " " + dttimeplan.Rows[0][1].ToString());
                TimeSpan ts = dtime - dttimestart;
                double Seconds = ts.TotalSeconds;
                double alltime = 0; double product = 0;
                for (int i = 0; i < ProductplanModels.Count; i++)
                {
                    double totalsecond = ProductplanModels[i].Alltime;
                    if (Seconds > alltime + totalsecond)
                    {
                        alltime += totalsecond;
                        product += ProductplanModels[i].Productplannum;
                        continue;
                    }
                    else
                    {
                        product = product + (Seconds - alltime) / double.Parse(ProductplanModels[i].Pertime);
                        return product;
                    }
                }
                return product;
            }
            catch
            {
                return 0;
            }
            return 0;
        }

    }

    public class TimeModel : CNotifyPropertyChange
    {
        public TimeModel(string date, string time)
        {
            _date = date;
            _time = time;
        }

        private string _date;
        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                this.NotifyPropertyChange("Date");
            }
        }

        private string _time;
        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                this.NotifyPropertyChange("Time");
            }
        }

    }

    public class MainModel : CNotifyPropertyChange
    {
        public MainModel(string inname, string innum, string outname, string outnum,
            string selecttimeplan, string totalproductnum, double planpercent, double truepercent, string productnow,
            string productnext, string meter, double planproduct, double trueproduct)
        {
            _inname = inname;
            _innum = innum;
            _outname = outname;
            _outnum = outnum;
            _selecttimeplan = "生产班次: " + selecttimeplan;
            _totalproductnum = "今日计划：" + totalproductnum + " 台";
            _planpercent = int.Parse((planpercent * 100).ToString().Split('.')[0]);
            _truepercent = int.Parse((truepercent * 100).ToString().Split('.')[0]);
            _productnow = productnow;
            _productnext = productnext;
            _meter = meter;
            _planproduct = int.Parse(planproduct.ToString().Split('.')[0]);
            _trueproduct = int.Parse(trueproduct.ToString());
            _product = _trueproduct - _planproduct;
            double _productpercent1 = planproduct == 0 ? 0 : trueproduct * 100 / planproduct;
            _productpercent = int.Parse(_productpercent1.ToString().Split('.')[0]);
        }


        private string _inname;
        public string Inname
        {
            get { return _inname; }
            set
            {
                _inname = value;
                this.NotifyPropertyChange("Inname");
            }
        }

        private string _innum;
        public string Innum
        {
            get { return _innum; }
            set
            {
                _innum = value;
                this.NotifyPropertyChange("Innum");
            }
        }

        private string _outname;
        public string Outname
        {
            get { return _outname; }
            set
            {
                _outname = value;
                this.NotifyPropertyChange("Outname");
            }
        }

        private string _outnum;
        public string Outnum
        {
            get { return _outnum; }
            set
            {
                _outnum = value;
                this.NotifyPropertyChange("Outnum");
            }
        }

        private string _selecttimeplan;
        public string Selecttimeplan
        {
            get { return _selecttimeplan; }
            set
            {
                _selecttimeplan = value;
                this.NotifyPropertyChange("Selecttimeplan");
            }
        }

        private string _totalproductnum;
        public string Totalproductnum
        {
            get { return _totalproductnum; }
            set
            {
                _totalproductnum = value;
                this.NotifyPropertyChange("Totalproductnum");
            }
        }

        private int _planpercent;
        public int Planpercent
        {
            get { return _planpercent; }
            set
            {
                _planpercent = value;
                this.NotifyPropertyChange("Planpercent");
            }
        }

        private int _truepercent;
        public int Truepercent
        {
            get { return _truepercent; }
            set
            {
                _truepercent = value;
                this.NotifyPropertyChange("Truepercent");
            }
        }

        private string _productnow;
        public string Productnow
        {
            get { return _productnow; }
            set
            {
                _productnow = value;
                this.NotifyPropertyChange("Productnow");
            }
        }



        private string _productnext;
        public string Productnext
        {
            get { return _productnext; }
            set
            {
                _productnext = value;
                this.NotifyPropertyChange("Productnext");
            }
        }

        private string _meter;
        public string Meter
        {
            get { return _meter; }
            set
            {
                _meter = value;
                this.NotifyPropertyChange("Meter");
            }
        }

        private int _planproduct;
        public int Planproduct
        {
            get { return _planproduct; }
            set
            {
                _planproduct = value;
                this.NotifyPropertyChange("Planproduct");
            }
        }

        private int _trueproduct;
        public int Trueproduct
        {
            get { return _trueproduct; }
            set
            {
                _trueproduct = value;
                this.NotifyPropertyChange("Trueproduct");
            }
        }

        private int _product;
        public int Product
        {
            get { return _product; }
            set
            {
                _product = value;
                this.NotifyPropertyChange("Product");
            }
        }

        private int _productpercent;
        public int Productpercent
        {
            get { return _productpercent; }
            set
            {
                _productpercent = value;
                this.NotifyPropertyChange("Product");
            }
        }
    }

    public class ProductPlanModel : CNotifyPropertyChange
    {
        public ProductPlanModel(string id, string sort, string productname, int productplannum, int truenum, string pertime, double alltime)
        {
            _id = id;
            _sort = sort;
            _productname = productname;
            _productplannum = productplannum;
            _truenum = truenum;
            _pertime = pertime;
            _alltime = alltime;
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                this.NotifyPropertyChange("Id");
            }
        }

        private string _sort;
        public string Sort
        {
            get { return _sort; }
            set
            {
                _sort = value;
                this.NotifyPropertyChange("Sort");
            }
        }

        private string _productname;
        public string Productname
        {
            get { return _productname; }
            set
            {
                _productname = value;
                this.NotifyPropertyChange("Productname");
            }
        }

        private int _truenum;
        public int Truenum
        {
            get { return _truenum; }
            set
            {
                _truenum = value;
                this.NotifyPropertyChange("Truenum");
            }
        }

        private int _productplannum;
        public int Productplannum
        {
            get { return _productplannum; }
            set
            {
                _productplannum = value;
                this.NotifyPropertyChange("Productplannum");
            }
        }


        private string _pertime;
        public string Pertime
        {
            get { return _pertime; }
            set
            {
                _pertime = value;
                this.NotifyPropertyChange("Pertime");
            }
        }

        private double _alltime;
        public double Alltime
        {
            get { return _alltime; }
            set
            {
                _alltime = value;
                this.NotifyPropertyChange("Alltime");
            }
        }


    }

    public class TimePlanModel : CNotifyPropertyChange
    {
        public TimePlanModel(string id, string sort, string timestart, string timeend, int planproduct, int trueproduct)
        {
            _id = "第" + id + "时段";
            _sort = "第" + sort + "时段";
            _timestart = timestart;
            _timeend = timeend;
            _timeshow = "【" + timestart + "~" + timeend + "】";
            _planproduct = "目标：" + planproduct + " 台";
            _trueproduct = "实际：" + trueproduct + " 台";
            _product = "差异：" + (trueproduct - planproduct).ToString() + " 台";


        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                this.NotifyPropertyChange("Id");
            }
        }

        private string _sort;
        public string Sort
        {
            get { return _sort; }
            set
            {
                _sort = value;
                this.NotifyPropertyChange("Sort");
            }
        }

        private string _timestart;
        public string Timestart
        {
            get { return _timestart; }
            set
            {
                _timestart = value;
                this.NotifyPropertyChange("Timestart");
            }
        }

        private string _timeend;
        public string Timeend
        {
            get { return _timeend; }
            set
            {
                _timeend = value;
                this.NotifyPropertyChange("Timeend");
            }
        }

        private string _timeshow;
        public string Timeshow
        {
            get { return _timeshow; }
            set
            {
                _timeshow = value;
                this.NotifyPropertyChange("Timeshow");
            }
        }

        private string _planproduct;
        public string Planproduct
        {
            get { return _planproduct; }
            set
            {
                _planproduct = value;
                this.NotifyPropertyChange("Planproduct");
            }
        }

        private string _trueproduct;
        public string Trueproduct
        {
            get { return _trueproduct; }
            set
            {
                _trueproduct = value;
                this.NotifyPropertyChange("Trueproduct");
            }
        }

        private string _product;
        public string Product
        {
            get { return _product; }
            set
            {
                _product = value;
                this.NotifyPropertyChange("Product");
            }
        }
    }
}
