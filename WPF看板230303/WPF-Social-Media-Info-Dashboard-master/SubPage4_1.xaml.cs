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
using System.Windows.Threading;
using System.Linq;
using Dashboard.ViewModel;

namespace Dashboard
{
    /// <summary>
    /// SubPage4_1.xaml 的交互逻辑
    /// </summary>
    public partial class SubPage4_1 : Page
    {

        private SubPage4ViewModel m_viewModel = null;
        public SubPage4_1()
        {
            m_viewModel = new SubPage4ViewModel();
            this.DataContext = m_viewModel;
            InitializeComponent();
        }



        private void PIC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            Border border = sender as Border;
            string name = border.Tag.ToString();
           

            ChartWindows subView = new ChartWindows(name);

            var dialog = subView.ShowDialog();

           
        }


    }


}
