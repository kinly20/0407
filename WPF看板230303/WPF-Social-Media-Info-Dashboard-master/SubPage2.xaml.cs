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
    /// SubPage2.xaml 的交互逻辑
    /// </summary>
    public partial class SubPage2 : Page
    {


        private SubPage2ViewModel m_viewModel = null;
      
        public SubPage2()
        {
            m_viewModel = new SubPage2ViewModel();
            this.DataContext = m_viewModel;
            InitializeComponent();

           
        }

       
    }
}
