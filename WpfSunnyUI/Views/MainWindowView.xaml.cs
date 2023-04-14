using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfSunnyUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button bt = sender as System.Windows.Controls.Button;
            string btname = bt.Name;
            ChanggeMenu(btname);
        }

        private void ChanggeMenu(string selectmenu)
        {
            string pagefilename = "WpfSunnyUI.SubPage";

            subChanggeMenu(btmenu1, false);
            subChanggeMenu(btmenu2, false);
            subChanggeMenu(btmenu3, false);
            subChanggeMenu(btmenu4, false);
            subChanggeMenu(btmenu5, false);
            subChanggeMenu(btmenu6, false);
            switch (selectmenu)
            {
                case "btmenu1":
                    subChanggeMenu(btmenu1, true);
                    pagefilename += "7";
                    break;
                case "btmenu2":
                    subChanggeMenu(btmenu2, true);
                    pagefilename += "4_1";
                    break;
                case "btmenu3":
                    subChanggeMenu(btmenu3, true);
                    pagefilename += "3";
                    break;
                case "btmenu4":
                    subChanggeMenu(btmenu4, true);
                    pagefilename += "8";
                    break;
                case "btmenu5":
                    subChanggeMenu(btmenu5, true);
                    pagefilename += "2";
                    break;
                case "btmenu6":
                    subChanggeMenu(btmenu6, true);
                    pagefilename += "9";
                    break;
            }

           
            //Type T = Type.GetType(pagefilename);
            //if (T != null)
            //{
            //    pagechange.Content = new Frame()
            //    {
            //        Content = Activator.CreateInstance(T)
            //    };
            //}
           


        }

        private void subChanggeMenu(System.Windows.Controls.Button bt, bool show)
        {
            if (show)
            {
                var style = this.TryFindResource("menuButtonSelect") as Style;
                bt.Style = style;
            }
            else
            {
                var style = this.TryFindResource("menuButton") as Style;
                bt.Style = style;
            }
        }

    }
}
