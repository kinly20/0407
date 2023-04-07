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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;

namespace Dashboard
{
    /// <summary>
    /// SubPage4.xaml 的交互逻辑
    /// </summary>
    public partial class SubPage4 : Page
    {
        public SubPage4()
        {
           
            InitializeComponent();
          
        }


        ////private IChartValues setTempCollect = new ChartValues<double>();

        ////public IChartValues SetTempCollect
        ////{
        ////    get
        ////    {
        ////        setTempCollect.Add(21.2);
        ////        setTempCollect.Add(22.2);
        ////        setTempCollect.Add(21.2);
        ////        setTempCollect.Add(22.2);
        ////        return setTempCollect;
        ////    }
        ////    set
        ////    {
        ////        if (setTempCollect != value)
        ////        {
        ////            setTempCollect = value;
        ////        }
        ////    }
        ////}


        ////private IChartValues getTempCollect = new ChartValues<double>();

        ////public IChartValues GetTempCollect
        ////{
        ////    get
        ////    {
        ////        getTempCollect.Add(31.2);
        ////        getTempCollect.Add(32.2);
        ////        getTempCollect.Add(31.2);
        ////        getTempCollect.Add(32.2);
        ////        return getTempCollect;
        ////    }
        ////    set
        ////    {
        ////        if (getTempCollect != value)
        ////        {
        ////            getTempCollect = value;
        ////        }
        ////    }
        ////}
        ////private IChartValues setRateCollect = new ChartValues<double>();

        ////public IChartValues SetRateCollect
        ////{
        ////    get
        ////    {
        ////        setRateCollect.Add(41.2);
        ////        setRateCollect.Add(42.2);
        ////        setRateCollect.Add(41.2);
        ////        setRateCollect.Add(42.2);
        ////        return setRateCollect;
        ////    }
        ////    set
        ////    {
        ////        if (setRateCollect != value)
        ////        {
        ////            setRateCollect = value;
        ////        }
        ////    }
        ////}
        ////private IChartValues getRateCollect = new ChartValues<double>();

        ////public IChartValues GetRateCollect
        ////{
        ////    get
        ////    {
        ////        getRateCollect.Add(51.2);
        ////        getRateCollect.Add(52.2);
        ////        getRateCollect.Add(51.2);
        ////        getRateCollect.Add(52.2);
        ////        return getRateCollect;
        ////    }
        ////    set
        ////    {
        ////        if (getRateCollect != value)
        ////        {
        ////            getRateCollect = value;
        ////        }
        ////    }
        ////}

        ////private List<string> axisXLables = new List<string>();

        ////public List<string> AxisXLables
        ////{
        ////    get
        ////    {
        ////        axisXLables.Add("4");
        ////        axisXLables.Add("6");
        ////        axisXLables.Add("8");
        ////        axisXLables.Add("10");
        ////        return axisXLables;
        ////    }
        ////    set
        ////    {
        ////        if (axisXLables != value)
        ////        {

        ////            axisXLables = value;

        ////        }
        ////    }
        ////}

        private void TrendView_Loaded(object sender, RoutedEventArgs e)
        {
            ////line1.Values = SetTempCollect;
            ////line2.Values = GetTempCollect;
            ////line3.Values = SetRateCollect;
            ////line4.Values = GetRateCollect;
            ////axis1.Labels = AxisXLables;
        }
    }
}
