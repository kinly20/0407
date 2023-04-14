using System.ComponentModel;
using System.Windows.Media;


namespace Engine.Behaviors
{
    class CheckEx:CheckBox
    {
        public static readonly DependencyProperty ValueStatusProperty = DependencyProperty.Register("ValueStatus", typeof(bool), typeof(CheckEx)
, new PropertyMetadata(false, ValueChanges));


        [Bindable(true)]
        [Category("Appearance")]
        public bool ValueStatus
        {
            get => (bool)GetValue(ValueStatusProperty);
            set => SetValue(ValueStatusProperty, value);
        }

        private static void ValueChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var led = (CheckEx)d;
            if (led == null) return;
            var newbool = (bool)e.NewValue;
            led.UpdataValueground(newbool);
        }
        private void UpdataValueground(bool newbool)
        {
            if (newbool)
            {
                this.Background = EndBackground;
            }
            else
                this.Background = StartBackground;
        }


        public static readonly DependencyProperty StartBackgroundProperty =
         DependencyProperty.Register("StartBackground", typeof(Brush), typeof(CheckEx), new PropertyMetadata(Brushes.White));

        [Bindable(true)]
        [Category("Appearance")]
        public Brush StartBackground
        {
            get => (Brush)GetValue(StartBackgroundProperty);
            set => SetValue(StartBackgroundProperty, value);
        }


        public static readonly DependencyProperty EndBackgroundProperty =
 DependencyProperty.Register("EndBackground", typeof(Brush), typeof(CheckEx), new PropertyMetadata(Brushes.White));

        [Bindable(true)]
        [Category("Appearance")]
        public Brush EndBackground
        {
            get => (Brush)GetValue(EndBackgroundProperty);
            set => SetValue(EndBackgroundProperty, value);
        }

    }
}
