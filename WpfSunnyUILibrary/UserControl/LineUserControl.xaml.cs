using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
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
    /// LineUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class LineUserControl : System.Windows.Controls.UserControl
    {
        //public static int length = 0;
        public LineUserControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(LineUserControl), new PropertyMetadata("Idle", new PropertyChangedCallback(TextChange)));
        public static readonly DependencyProperty LineColorProperty = DependencyProperty.Register("LineColor", typeof(string), typeof(LineUserControl), new PropertyMetadata(ColorChange));
        public static readonly DependencyProperty HightProperty = DependencyProperty.Register("Hight", typeof(int), typeof(LineUserControl), new PropertyMetadata(XYChange));
        public static new readonly DependencyProperty WidthProperty = DependencyProperty.Register("Width", typeof(int), typeof(LineUserControl), new PropertyMetadata(XYChange));

        [Bindable(true)]
        [Category("Appearance")]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public string LineColor
        {
            get { return (string)GetValue(LineColorProperty); }
            set
            {
                SetValue(LineColorProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public int Hight
        {
            get { return (int)GetValue(HightProperty); }
            set
            {
                SetValue(HightProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public new int Width
        {
            get { return (int)GetValue(WidthProperty); }
            set
            {
                SetValue(WidthProperty, value);
            }
        }



        private static void TextChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LineUserControl line = (LineUserControl)d;
            if (line != null)
            {

                string oldstring = (string)e.OldValue;
                string newstring = (string)e.NewValue;
                line.UpdateState(newstring);
            }
        }

        private void UpdateState(string text)
        {
            this.tbtext.Text = text;
            double num = this.line2.X2;
            this.line1.X1 = 0;
            this.line1.X2 = num / 2 - Text.Length * 5 -PublicClass.GetHanNumFromString(Text) * 1.4;
            this.line2.X1 = num / 2 + Text.Length * 5 + PublicClass.GetHanNumFromString(Text) * 1.4;
            this.line2.X2 = num;
            //UpdataXY(length, 2);
        }

        private static void ColorChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LineUserControl line = (LineUserControl)d;
            if (line != null)
            {

                string oldstring = (string)e.OldValue;
                string newstring = (string)e.NewValue;
                line.UpdateColor(newstring);
            }
        }

        private void UpdateColor(string colorinfo)
        {
            Color color = (Color)ColorConverter.ConvertFromString(colorinfo);
            this.tbtext.Foreground = new SolidColorBrush(color);
            line1.Stroke = new SolidColorBrush(color);
            line2.Stroke = new SolidColorBrush(color);
        }

        private static void XYChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LineUserControl line = (LineUserControl)d;
            DependencyProperty h = e.Property;
            if (line != null)
            {
                int oldbool = (int)e.OldValue;
                int newvalue = (int)e.NewValue;
                int index = 0;
                switch (h.Name)
                {
                    case "Hight":
                        index = 1;
                        break;
                    case "Width":
                        index = 2;
                        //length = newvalue;
                        break;
                }
                line.UpdataXY(newvalue, index);
            }
        }
        private void UpdataXY(int num, int index)
        {
            switch (index)
            {
                case 1:
                    this.line1.Y1 = num / 2;
                    this.line1.Y2 = num / 2;
                    this.line2.Y1 = num / 2;
                    this.line2.Y2 = num / 2;
                    break;
                case 2:
                    this.line1.X1 = 0;
                    this.line1.X2 = num / 2 - Text.Length * 5 - PublicClass.GetHanNumFromString(Text) * 1.4;
                    this.line2.X1 = num / 2 + Text.Length * 5 + PublicClass.GetHanNumFromString(Text) * 1.4;
                    this.line2.X2 = num;
                    break;
            }

        }

      

    }
}
