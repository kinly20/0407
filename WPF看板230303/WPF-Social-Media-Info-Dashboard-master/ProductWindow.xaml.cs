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
    /// ProductWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProductWindow : Window
    {
        TypeNumModel _model = null;
        data data1 = new data();
        public ProductWindow(TypeNumModel model)
        {
            _model = model;
            InitializeComponent();
            loaddata();
        }

        public void loaddata()
        {
            if (_model != null && _model.Name != "")
            {
                tsort.Text = _model.Sort;
                tsort.IsEnabled = false;
                tbproduct.Text = _model.Name;
                tbnum.Text = _model.Num.ToString();
            }
            else
            {
                tsort.Text = _model.Sort;
                tsort.IsEnabled = false;
                btdelete.Visibility = Visibility.Hidden;
            }
        }

        private void Sure_Click(object sender, RoutedEventArgs e)
        {
            string name = tbproduct.Text.Trim();
            DataTable dt = SQLiteeDB.Getproducttimebyname(name);
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("产品不存在，请先录入该产品节拍！");
                return;
            }
            //判断是否已知产品
            if (tbnum.Text == "")
            {
                MessageBox.Show("请输入生产数量！");
                return;
            }
            try
            {
                int num = int.Parse(tbnum.Text.Trim());
            }
            catch
            {
                MessageBox.Show("请输入正确的生产数量！");
                return;
            }
            //判断个数是否未填
            //保存数据
            if (_model.ID == "")
            {
                //SQLiteeDB.Insertproducttime(name, tbnum.Text.Trim());
                data1.AddWorkProductPlan(name, int.Parse(tbnum.Text.Trim()));
            }
            else
            {
                data1.EditWorkProductPlan(name, int.Parse(tbnum.Text.Trim()), int.Parse(_model.ID.ToString()));
                //SQLiteeDB.Editproducttime(_model.ID, name, tbnum.Text.Trim());
            }
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            data1.DeleteWorkProductPlan(int.Parse(_model.ID.ToString()));
            this.Close();
        }

        private void tbnum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
