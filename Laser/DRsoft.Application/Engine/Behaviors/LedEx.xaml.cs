using System.ComponentModel;
using System.Windows.Media;

namespace Engine
{
    /// <summary>
    /// LedEx.xaml 的交互逻辑
    /// </summary>
    public partial class LedEx : UserControl
    {
        public LedEx()
        {
            InitializeComponent();
            this.Width = 50;
            this.Height = 50;
        }

        public static readonly DependencyProperty ValueStatusProperty = DependencyProperty.Register("ValueStatus", typeof(bool), typeof(LedEx)
 , new PropertyMetadata((bool)false, new PropertyChangedCallback(ValueChanges)));



        [Bindable(true)]
        [Category("Appearance")]
        public bool ValueStatus
        {
            get => (bool)GetValue(ValueStatusProperty);
            set => SetValue(ValueStatusProperty, value);
        }

        private static void ValueChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LedEx led = (LedEx)d;
            if (led != null)
            {
                bool oldbool = (bool)e.OldValue;
                bool newbool = (bool)e.NewValue;
                led.UpdataValueground(newbool);
            }
        }
        private void UpdataValueground(bool newbool)
        {
            if (newbool)
            {
                this.GridColor.Background = EndBackground;
            }
            else
                this.GridColor.Background = StartBackground;
        }


        public static readonly DependencyProperty StartBackgroundProperty =
         DependencyProperty.Register("StartBackground", typeof(Brush), typeof(LedEx), new PropertyMetadata(Brushes.WhiteSmoke));

        [Bindable(true)]
        [Category("Appearance")]
        public Brush StartBackground
        {
            get => (Brush)GetValue(StartBackgroundProperty);
            set => SetValue(StartBackgroundProperty, value);
        }


        public static readonly DependencyProperty EndBackgroundProperty =
 DependencyProperty.Register("EndBackground", typeof(Brush), typeof(LedEx), new PropertyMetadata(Brushes.WhiteSmoke));

        [Bindable(true)]
        [Category("Appearance")]
        public Brush EndBackground
        {
            get => (Brush)GetValue(EndBackgroundProperty);
            set => SetValue(EndBackgroundProperty, value);
        }

    }
}
