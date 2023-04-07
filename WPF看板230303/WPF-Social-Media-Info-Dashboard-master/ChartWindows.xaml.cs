using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using LiveCharts;

namespace Dashboard
{
    /// <summary>
    /// ChartWindows.xaml 的交互逻辑
    /// </summary>
    public partial class ChartWindows : Window
    {
        private string _machionname = "";
        data data1 = new data();
        public ChartWindows(string name)
        {
            _machionname = name;
            InitializeComponent();
            loadchartdata();
        }

        public void loadchartdata()
        {
            DateTime dtnow = System.DateTime.Now;
            DateTime dtmonth = System.DateTime.Now.AddDays(-30);
            DateTime dtyear = System.DateTime.Now.AddMonths(-12);


            DataTable dtmonthproduct = data1.SearchProductdata(dtmonth, dtnow, _machionname);//月产量
            DataTable dtyearproduct = data1.SearchProductdata(dtyear, dtnow, _machionname);//年产量
            ChartValues<int> days = new ChartValues<int> {
            getdata(dtmonthproduct,-29,"day"), getdata(dtmonthproduct,-28,"day"), getdata(dtmonthproduct,-27,"day"), getdata(dtmonthproduct,-26,"day"),
            getdata(dtmonthproduct,-25,"day"), getdata(dtmonthproduct,-24,"day"), getdata(dtmonthproduct,-23,"day"), getdata(dtmonthproduct,-22,"day"), getdata(dtmonthproduct,-21,"day"),
            getdata(dtmonthproduct,-20,"day"), getdata(dtmonthproduct,-19,"day"), getdata(dtmonthproduct,-18,"day"), getdata(dtmonthproduct,-17,"day"), getdata(dtmonthproduct,-16,"day"),
            getdata(dtmonthproduct,-15,"day"), getdata(dtmonthproduct,-14,"day"), getdata(dtmonthproduct,-13,"day"), getdata(dtmonthproduct,-12,"day"), getdata(dtmonthproduct,-11,"day"),
            getdata(dtmonthproduct,-10,"day"), getdata(dtmonthproduct,-9,"day"), getdata(dtmonthproduct,-8,"day"), getdata(dtmonthproduct,-7,"day"), getdata(dtmonthproduct,-6,"day"),
            getdata(dtmonthproduct,-5,"day"), getdata(dtmonthproduct,-4,"day"), getdata(dtmonthproduct,-3,"day"), getdata(dtmonthproduct,-2,"day"), getdata(dtmonthproduct,-1,"day"),
            getdata(dtmonthproduct,0,"day")
            };

            List<string> daysX = new List<string> {
                dtnow.AddDays(-29).ToString("MM-dd"), dtnow.AddDays(-28).ToString("MM-dd"), dtnow.AddDays(-27).ToString("MM-dd"), dtnow.AddDays(-26).ToString("MM-dd"), dtnow.AddDays(-25).ToString("MM-dd"),
                dtnow.AddDays(-24).ToString("MM-dd"), dtnow.AddDays(-23).ToString("MM-dd"), dtnow.AddDays(-22).ToString("MM-dd"), dtnow.AddDays(-21).ToString("MM-dd"), dtnow.AddDays(-20).ToString("MM-dd"),
                dtnow.AddDays(-19).ToString("MM-dd"), dtnow.AddDays(-18).ToString("MM-dd"), dtnow.AddDays(-17).ToString("MM-dd"), dtnow.AddDays(-16).ToString("MM-dd"), dtnow.AddDays(-15).ToString("MM-dd"),
                dtnow.AddDays(-14).ToString("MM-dd"), dtnow.AddDays(-13).ToString("MM-dd"), dtnow.AddDays(-12).ToString("MM-dd"), dtnow.AddDays(-11).ToString("MM-dd"), dtnow.AddDays(-10).ToString("MM-dd"),
                dtnow.AddDays(-9).ToString("MM-dd"), dtnow.AddDays(-8).ToString("MM-dd"), dtnow.AddDays(-7).ToString("MM-dd"), dtnow.AddDays(-6).ToString("MM-dd"), dtnow.AddDays(-5).ToString("MM-dd"),
                dtnow.AddDays(-4).ToString("MM-dd"), dtnow.AddDays(-3).ToString("MM-dd"), dtnow.AddDays(-2).ToString("MM-dd"), dtnow.AddDays(-1).ToString("MM-dd"), dtnow.AddDays(0).ToString("MM-dd") };

            ChartValues<int> months = new ChartValues<int> {  getdata(dtyearproduct,-11,"month"),
            getdata(dtyearproduct,-10,"month"), getdata(dtyearproduct,-9,"month"), getdata(dtyearproduct,-8,"month"), getdata(dtyearproduct,-7,"month"), getdata(dtyearproduct,-6,"month"),
            getdata(dtyearproduct,-5,"month"), getdata(dtyearproduct,-4,"month"), getdata(dtyearproduct,-3,"month"), getdata(dtyearproduct,-2,"month"), getdata(dtyearproduct,-1,"month"),
            getdata(dtyearproduct,0,"month")};

            List<string> daysX2 = new List<string> { dtnow.AddMonths(-11).ToString("yy-MM"), dtnow.AddMonths(-10).ToString("yy-MM"),
                dtnow.AddMonths(-9).ToString("yy-MM"), dtnow.AddMonths(-8).ToString("yy-MM"), dtnow.AddMonths(-7).ToString("yy-MM"), dtnow.AddMonths(-6).ToString("yy-MM"), dtnow.AddMonths(-5).ToString("yy-MM"),
                dtnow.AddMonths(-4).ToString("yy-MM"), dtnow.AddMonths(-3).ToString("yy-MM"), dtnow.AddMonths(-2).ToString("yy-MM"), dtnow.AddMonths(-1).ToString("yy-MM"), dtnow.AddMonths(0).ToString("yy-MM") };

            line1.Values = days;
            axis1.Labels = daysX;
            line27.Values = months;
            axis21.Labels = daysX2;
        }



        public int getdata(DataTable dt, int times, string type)
        {
            gettime(type, times, out DateTime dts, out DateTime dte);

            int product = 0;
            DataRow[] drs = dt.Select("time > '" + dts.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "' and time <'" + dte.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "'");
            for (int i = 0; i < drs.Length; i++)
            {
                product += int.Parse(drs[i]["productnum"].ToString());
            }
            return product;
        }

        public bool gettime(string type, int times, out DateTime dts, out DateTime dte)
        {
            if (type == "month")
            {
                dts = Convert.ToDateTime(System.DateTime.Now.AddMonths(times).ToString("yyyy-MM") + "-01 00:00:00");
                dte = Convert.ToDateTime(System.DateTime.Now.AddMonths(times).AddMonths(1).ToString("yyyy-MM") + "-01 00:00:00");
            }
            else //(type == "day")
            {
                dts = Convert.ToDateTime(System.DateTime.Now.AddDays(times).ToString("yyyy-MM-dd") + " 00:00:00");
                dte = Convert.ToDateTime(System.DateTime.Now.AddDays(times).AddDays(1).ToString("yyyy-MM-dd") + " 00:00:00");
            }
            return true;
        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            //OK NG等数据暂时无数据源
        }
    }
}
