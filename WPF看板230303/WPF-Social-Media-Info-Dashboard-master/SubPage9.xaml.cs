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
using Dashboard.ViewModel;
using System.Data;

namespace Dashboard
{
    /// <summary>
    /// SubPage9.xaml 的交互逻辑
    /// </summary>
    public partial class SubPage9 : Page
    {
        private SubPage9ViewModel m_viewModel = null;
        data data1 = new data();
        public SubPage9()
        {
            m_viewModel = new SubPage9ViewModel();
            this.DataContext = m_viewModel;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button border = sender as Button;
            string name = border.Tag.ToString();

            if (cbproduct.SelectedIndex < 0)
            {
                MessageBox.Show("请选择调宽产品！");
                return;
            }
            DataTable dt = data1.SearchRecipeByNameAndProduct(name, cbproduct.SelectedValue.ToString());
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(name + "暂无" + cbproduct.SelectedValue.ToString() + "的配方值！");
                return;
            }
            StaticPlc.ChanggeWidth(name, short.Parse(dt.Rows[0]["recipe"].ToString()));

        }
    }
}
