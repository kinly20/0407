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

namespace Dashboard
{
    /// <summary>
    /// MeterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MeterWindow : Window
    {
        MeterModel _model = null;
        data data1 = new data();
        public MeterWindow(MeterModel model)
        {
            _model = model;
            InitializeComponent();
            loaddata();
        }

        public void loaddata()
        {
            if (_model != null && _model.Name != "")
            {
                tbmachine.Text = _model.Name;
                tbmeter.Text = _model.Meter;
            }
            else
            {
                btdelete.Visibility = Visibility.Hidden;
            }
        }

        private void Sure_Click(object sender, RoutedEventArgs e)
        {
            string name = tbmachine.Text.Trim();
            if (name.Length == 0)
            {
                MessageBox.Show("产品不能为空！");
                return;
            }
            //判断是否已知产品
            if (tbmeter.Text == "")
            {
                MessageBox.Show("节拍不能为空！");
                return;
            }
            try
            {
                double num = double.Parse(tbmeter.Text.Trim());
            }
            catch
            {
                MessageBox.Show("请输入正确的节拍！");
                return;
            }
            
            //保存数据
            if (_model != null && _model.Name != "")
            {
                SQLiteeDB.Editproducttime(_model.ID, name, tbmeter.Text.Trim());
            }
            else
            {
                SQLiteeDB.Insertproducttime(name, tbmeter.Text.Trim());
            }
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            SQLiteeDB.Deleteproducttime(_model.ID);
            this.Close();
        }

       
    }
}
