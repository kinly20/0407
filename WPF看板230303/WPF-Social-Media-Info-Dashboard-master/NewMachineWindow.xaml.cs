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

namespace Dashboard
{
    /// <summary>
    /// NewMachineWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewMachineWindow : Window
    {
        data data1 = new data();
        DataTable dt = new DataTable();
        public NewMachineWindow()
        {
            InitializeComponent();
            dt = data1.SearchMachineType();
            List<string> strs = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strs.Add(dt.Rows[i][1].ToString());
            }
            cblist.ItemsSource = strs;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbip.Text) || string.IsNullOrEmpty(tbname.Text))
            {
                MessageBox.Show("机台网址和机台名不能为空！");
            }
            else if (cblist.SelectedIndex == -1)
            {
                MessageBox.Show("请选择机台类型！");
            }
            else
            {
                //data data1 = new data();
                DataTable dt = data1.SearchAllMachine();
                DataRow[] dr = dt.Select("text= '" + tbname.Text.Trim() + "'");
                DataRow[] dr2 = dt.Select("ip= '" + tbip.Text.Trim() + "'");
                if (dr.Length == 0 && dr2.Length == 0)
                {
                    data1.InsertMachine(tbip.Text, tbname.Text, dt.Rows[cblist.SelectedIndex][0].ToString());
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("机台或者IP已经存在！");
                }
            }


        }
    }
}
