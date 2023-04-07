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

namespace Dashboard
{
    /// <summary>
    /// NewTableWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewTableWindow : Window
    {
        public NewTableWindow()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Sure_Click(object sender, RoutedEventArgs e)
        {
            string name = tbname.Text.Trim();
            SQLiteeDB.Createtable(name);
            this.Close();
        }
    }
}
