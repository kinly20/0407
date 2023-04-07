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

namespace Dashboard
{
    /// <summary>
    /// SubPage5.xaml 的交互逻辑
    /// </summary>
    public partial class SubPage5 : Page
    {
      

        private SubPage5ViewModel m_viewModel = null;
        public SubPage5()
        {
            m_viewModel = new SubPage5ViewModel();
            this.DataContext = m_viewModel;
            InitializeComponent();
        }
    }
}
