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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Threading;

namespace Dashboard
{
    /// <summary>
    /// SubPage6.xaml 的交互逻辑
    /// </summary>
    public partial class SubPage6 : Page
    {
        private DispatcherTimer timer;
        private AlarmDBModel alarmList;
        public SubPage6()
        {
            InitializeComponent();
            alarmList = new AlarmDBModel();
            loadalarmList();
            dta1.DataContext = alarmList;
            Loaded += new RoutedEventHandler(Window_Loaded);
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer1_Tick;
            timer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            loadalarmList();
        }

        public void loadalarmList()
        {
            alarmList = new AlarmDBModel();
            List<AlarmModel> alarms = StaticPlc.Getalarms();
            for (int i = 0; i < alarms.Count; i++)
            {
                AlarmModel US = new AlarmModel();//id,stationid,code,result,time
                US.Id = (i + 1).ToString();
                US.addr = alarms[i].addr;
                US.text = alarms[i].text;
                alarmList.AlarmList.Add(US);
            }
        }
    }


    public class AlarmModel
    {
        public string Id { get; set; }

        public string addr { get; set; }

        public string text { get; set; }
       
    }

   

    public class AlarmDBModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        private ObservableCollection<AlarmModel> alarmList = new ObservableCollection<AlarmModel>();
        public ObservableCollection<AlarmModel> AlarmList
        {
            get { return alarmList; }
            set
            {
                alarmList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AlarmList"));
            }
        }


   
    }
}
