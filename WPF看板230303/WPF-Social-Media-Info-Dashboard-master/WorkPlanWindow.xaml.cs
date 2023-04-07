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
using System.Text.RegularExpressions;

namespace Dashboard
{
    /// <summary>
    /// WorkPlanWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WorkPlanWindow : Window
    {
        private WorkPlanViewModel m_viewModel = null;
        public WorkPlanWindow()
        {
            m_viewModel = new WorkPlanViewModel();
            this.DataContext = m_viewModel;
            InitializeComponent();
        }


        private void tbnum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
