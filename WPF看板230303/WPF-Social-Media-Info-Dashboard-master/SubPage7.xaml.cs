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
    /// SubPage7.xaml 的交互逻辑
    /// </summary>
    public partial class SubPage7 : Page
    {
        private SubPage7ViewModel m_viewModel = null;
        public SubPage7()
        {
            m_viewModel = new SubPage7ViewModel();
            this.DataContext = m_viewModel;
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseEventArgs e)
        {
            WorkPlanWindow SUBView = new WorkPlanWindow();

            var dialog = SUBView.ShowDialog();
        }
    }
}
