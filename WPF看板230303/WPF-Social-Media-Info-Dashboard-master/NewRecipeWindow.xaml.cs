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
    /// NewRecipeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewRecipeWindow : Window
    {
        data data1 = new data();
        DataTable dt = new DataTable();
        public NewRecipeWindow()
        {
            InitializeComponent();
            dt = data1.SearchAllIPDataByType("接驳台");
            List<string> strs = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strs.Add(dt.Rows[i][2].ToString());
            }
            cblist.ItemsSource = strs;
        }

        private void tbnum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbproduct.Text) || string.IsNullOrEmpty(tbrecipe.Text))
            {
                MessageBox.Show("产品和配方值不能为空！");
            }
            else if (cblist.SelectedIndex == -1)
            {
                MessageBox.Show("请选择接驳台！");
            }
            else
            {
                data1.InsertRecipe(dt.Rows[cblist.SelectedIndex][0].ToString(), tbproduct.Text.Trim(), int.Parse(tbrecipe.Text));
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
