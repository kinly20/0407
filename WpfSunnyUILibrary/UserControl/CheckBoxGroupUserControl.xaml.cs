using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfSunnyUILibrary.Class;
using static System.Net.Mime.MediaTypeNames;

namespace WpfSunnyUILibrary.UserControl
{
    /// <summary>
    /// CheckBoxGroupUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class CheckBoxGroupUserControl : System.Windows.Controls.UserControl
    {
        public CheckBoxGroupUserControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty CheckBoxListProperty = DependencyProperty.Register("CheckBoxList", typeof(List<checkboxvalue>), typeof(CheckBoxGroupUserControl), new PropertyMetadata(CheckBoxListChange));

        [Bindable(true)]
        [Category("Appearance")]
        public List<checkboxvalue> CheckBoxList
        {
            get { return (List<checkboxvalue>)GetValue(CheckBoxListProperty); }
            set
            {
                SetValue(CheckBoxListProperty, value);
            }
        }





        private static void CheckBoxListChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckBoxGroupUserControl cbg = (CheckBoxGroupUserControl)d;
            if (cbg != null)
            {

                List<checkboxvalue> oldstring = (List<checkboxvalue>)e.OldValue;
                List<checkboxvalue> newstring = (List<checkboxvalue>)e.NewValue;
                if (oldstring != newstring)
                    cbg.UpdateCheckBoxList(newstring);
            }
        }

        private void UpdateCheckBoxList(List<checkboxvalue> checkboxlistinfo)
        {
            double splitdistance = 0;

            for (int i = 0; i < checkboxlistinfo.Count; i++)
            {
                int maxlength = PublicClass.GetTotalNumFromString(checkboxlistinfo[i].name);
                splitdistance = maxlength > splitdistance ? maxlength : splitdistance;
            }
            splitdistance = splitdistance * 9 + 60;
            for (int i = 0; i < checkboxlistinfo.Count; i++)
            {
                string checkbox = checkboxlistinfo[i].name;
                int row = i / 2;
                int col = i % 2;
                CheckBox cb = new CheckBox();
                cb.Click += check_Click;
                cb.IsChecked = checkboxlistinfo[i].ischeck;
                Style myStyle = (Style)this.FindResource("CheckBoxStyle");//TabItemStyle 这个样式是引用的资源文件中的样式名称
                cb.Style = myStyle;
                cb.Name = checkbox;


                Thickness myThickness = new Thickness(col * splitdistance + 30, 20 + row * 35, 0, 0);
                cb.Margin = myThickness;
                grid.Children.Add(cb);
            }
            int rowtotal = checkboxlistinfo.Count / 2;
            selectall.Margin = new Thickness(28, 20 + rowtotal * 35, 0, 0);
            selectnone.Margin = new Thickness(128, 20 + rowtotal * 35, 0, 0);
            selectopposite.Margin = new Thickness(228, 20 + rowtotal * 35, 0, 0);
            this.Height = 70 + rowtotal * 35;
            Border1.Height = this.Height;
        }




        private void selectall_Click(object sender, RoutedEventArgs e)
        {
            Button? bt = sender as Button;
            Grid? grid = bt?.Parent as Grid;
            foreach (UIElement ui in grid.Children)
            {
                if (ui.GetType().Name == "CheckBox")
                    (ui as CheckBox).IsChecked = true;
            }

            for (int i = 0; i < CheckBoxList.Count; i++)
                CheckBoxList[i].ischeck = true;
        }

        private void selectnone_Click(object sender, RoutedEventArgs e)
        {
            Button? bt = sender as Button;
            Grid? grid = bt?.Parent as Grid;
            foreach (UIElement ui in grid?.Children)
            {
                if (ui.GetType().Name == "CheckBox")
                    (ui as CheckBox).IsChecked = false;
            }
            for (int i = 0; i < CheckBoxList.Count; i++)
                CheckBoxList[i].ischeck = false;
        }

        private void selectopposite_Click(object sender, RoutedEventArgs e)
        {
            Button? bt = sender as Button;
            Grid? grid = bt?.Parent as Grid;
            foreach (UIElement ui in grid?.Children)
            {
                if (ui.GetType().Name == "CheckBox")
                {
                    CheckBox cb = (ui as CheckBox);
                    bool? check = cb.IsChecked;
                    cb.IsChecked = !check;
                    CheckBoxList.FirstOrDefault(t => t.name == cb.Name).ischeck = (bool)cb.IsChecked;
                }
            }

        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            CheckBoxList.FirstOrDefault(t => t.name == cb.Name).ischeck = (bool)cb.IsChecked;
        }
    }
}
