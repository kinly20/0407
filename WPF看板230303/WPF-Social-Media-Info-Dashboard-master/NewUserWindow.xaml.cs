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
using Dashboard;
using Dashboard.ViewModel;
using System.Data;

namespace Dashboard
{
    /// <summary>
    /// NewUserWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewUserWindow : Window
    {
        //public newdemosubModel _newuser = new newdemosubModel("", "", "", "");
        public NewUserWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbpassword.Text)|| string.IsNullOrEmpty(tbuser.Text))
            {
                MessageBox.Show("用户名密码不能为空！");
            }
            else
            {
                data data1 = new data();
                DataTable dt= data1.SearchAllUser();
                DataRow[] dr = dt.Select("user= '" + tbuser.Text.Trim() + "'");
                if (dr.Length == 0)
                {
                    data1.InsertUser(tbuser.Text, tbpassword.Text);
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户已经存在！");
                }
            }
            
            
        }
    }
}
