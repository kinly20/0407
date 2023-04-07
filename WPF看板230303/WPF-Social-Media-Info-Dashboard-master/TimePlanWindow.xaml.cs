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
using Dashboard.ViewModel;
using System.Data;
using System.Text.RegularExpressions;

namespace Dashboard
{
    /// <summary>
    /// TimePlanWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TimePlanWindow : Window
    {
        TimePlanModel2 _model = null;
        string _tablename = "";
        data data1 = new data();
        public TimePlanWindow(TimePlanModel2 model,string tablename)
        {
            _model = model;
            _tablename = tablename;

            InitializeComponent();
            loaddata();
        }

        public void loaddata()
        {
            if (_model != null && _model.Timestart != "")
            {
                tbsort.Text = _model.Sort;
                //tbsort.IsEnabled = false;
                tbtimes.Text = _model.Timestart;
                tbtimee.Text = _model.Timeend;
            }
            else
            {
                tbsort.Text = _model.Sort;
                //tbsort.IsEnabled = false;
                btdelete.Visibility = Visibility.Hidden;
            }
        }

        private void Sure_Click(object sender, RoutedEventArgs e)
        {
            string stime = tbtimes.Text.Trim();
            string etime = tbtimee.Text.Trim();

            try
            {
                DateTime timestart = Convert.ToDateTime(System.DateTime.Now.Date.ToShortDateString() + " " + stime);
                DateTime timeend = Convert.ToDateTime(System.DateTime.Now.Date.ToShortDateString() + " " + etime);
            }
            catch
            {
                MessageBox.Show("时间格式不正确，正确格式举例 08:00");
                return;
            }
            //判断个数是否未填
            //保存数据
            if (_model.Id == "")
            {
                SQLiteeDB.Inserttimeplan(tbsort.Text.Trim(), stime, etime, _tablename);
            }
            else
            {
                SQLiteeDB.Edittimeplan(_model.Id, stime, etime,_tablename);
            }
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            SQLiteeDB.Deletetimeplan(_model.Id, _tablename);
            this.Close();
        }

      
    }
}
