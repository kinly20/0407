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

namespace Dashboard
{
    /// <summary>
    /// SubPage3.xaml 的交互逻辑
    /// </summary>
    public partial class SubPage3 : Page
    {
        private UserDBModel userdb;
        public SubPage3()
        {
            InitializeComponent();
            userdb = new UserDBModel();
            dta1.DataContext = userdb;
            date1.SelectedDate = System.DateTime.Now.AddDays(-1);
            date2.SelectedDate = System.DateTime.Now;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            data data = new data();
            string code = tb_code.Text;
            DateTime dts = Convert.ToDateTime(date1.SelectedDate.ToString());
            DateTime dte = Convert.ToDateTime(date2.SelectedDate.ToString()).AddDays(1);
            DataTable dt = data.Searchalldata(dts, dte, code);

            if (dt != null)
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserModel US = new UserModel();//id,stationid,code,result,time
                    US.Id = (userdb.UserDB.Count + 1).ToString();
                    US.station = dt.Rows[i][5].ToString();
                    US.code = dt.Rows[i][2].ToString();
                    US.num = "";
                    US.result = dt.Rows[i][3].ToString() == "0" ? "成功" : "失败"; //成功0 / 失败1
                    US.time = dt.Rows[i][4].ToString();
                    userdb.UserDB.Add(US);
                }

            DataTable dt2 = data.SearchProductdata(dts, dte, code);

            if (dt2 != null)
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    UserModel US = new UserModel();//id,stationid,code,result,time
                    US.Id = (userdb.UserDB.Count + 1).ToString();
                    US.station = dt2.Rows[i][3].ToString();
                    US.code = "";
                    US.num = dt2.Rows[i][1].ToString();
                    US.result = "成功"; //成功0 / 失败1
                    US.time = dt2.Rows[i][2].ToString();
                    userdb.UserDB.Add(US);
                }
        }


    }

    public class UserModel
    {
        public string Id { get; set; }

        public string station { get; set; }

        public string code { get; set; }

        public string num { get; set; }

        public string result { get; set; }

        public string time { get; set; }
    }

    public class UserDBModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        private ObservableCollection<UserModel> userdb = new ObservableCollection<UserModel>();
        public ObservableCollection<UserModel> UserDB
        {
            get { return userdb; }
            set
            {
                userdb = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserDB"));
            }
        }


        private ObservableCollection<UserModel> _selectsubModel;

        public ObservableCollection<UserModel> SelectsubModel
        {
            get { return _selectsubModel; }
            set
            {
                _selectsubModel = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectsubModel"));
            }
        }
    }
}
