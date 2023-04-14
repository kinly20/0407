using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Engine
{
    /// <summary>
    /// Mesa.xaml 的交互逻辑
    /// </summary>
    public partial class Mesa : UserControl
    {
        CurrentPos NowPos = CurrentPos.S;
        public Mesa()
        {
            InitializeComponent();
            mgrid.Background = IsBackground;
        }

        public static readonly DependencyProperty FromSToAProperty = DependencyProperty.Register("FromSToA", typeof(bool), typeof(Mesa), new PropertyMetadata((bool)false, new PropertyChangedCallback(ValueChanges)));
        public static readonly DependencyProperty FromAToBProperty = DependencyProperty.Register("FromAToB", typeof(bool), typeof(Mesa), new PropertyMetadata((bool)false, new PropertyChangedCallback(AToBChanges)));
        public static readonly DependencyProperty FromAToEProperty = DependencyProperty.Register("FromAToE", typeof(bool), typeof(Mesa), new PropertyMetadata((bool)false, new PropertyChangedCallback(AToEChanges)));
        public static readonly DependencyProperty FromSToBProperty = DependencyProperty.Register("FromSToB", typeof(bool), typeof(Mesa), new PropertyMetadata((bool)false, new PropertyChangedCallback(SToBChanges)));
        public static readonly DependencyProperty FromBToEProperty = DependencyProperty.Register("FromBToE", typeof(bool), typeof(Mesa), new PropertyMetadata((bool)false, new PropertyChangedCallback(BToEChanges))); 
        public static readonly DependencyProperty IsEnableyProperty = DependencyProperty.Register("IsEnabley", typeof(bool), typeof(Mesa), new PropertyMetadata((bool)false, new PropertyChangedCallback(EnableChanges)));
        public static readonly DependencyProperty IsWidthProperty = DependencyProperty.Register("IsWidth", typeof(Int32), typeof(Mesa), new PropertyMetadata((Int32)200, new PropertyChangedCallback(WidthChanges)));
        public static readonly DependencyProperty IsHeightProperty = DependencyProperty.Register("IsHeight", typeof(int), typeof(Mesa), new PropertyMetadata((int)400, new PropertyChangedCallback(HeightChanges)));
        public static readonly DependencyProperty CompleteProperty = DependencyProperty.Register("Complete", typeof(bool), typeof(Mesa), new PropertyMetadata((bool)false, new PropertyChangedCallback(CompleteChanges)));
        public static readonly DependencyProperty FeedBackFinishProperty = DependencyProperty.Register("FeedBackFinish", typeof(bool), typeof(Mesa));
        public static readonly DependencyProperty IsBackgroundProperty = DependencyProperty.Register("IsBackground", typeof(Brush), typeof(Mesa), new PropertyMetadata((Brushes.LightBlue),new PropertyChangedCallback(BackgroundChanges)));
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(Mesa), new PropertyMetadata(false));
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(Mesa), new PropertyMetadata(false));

        [Bindable(true)]
        [Category("Appearance")]
        public bool IsEnabley
        {
            get { return (bool)GetValue(IsEnableyProperty); }
            set
            {
                SetValue(IsEnableyProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set
            {
                SetValue(IsBusyProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set
            {
                SetValue(IsActiveProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public Brush IsBackground
        {
            get { return (Brush)GetValue(IsBackgroundProperty); }
            set
            {
                SetValue(IsBackgroundProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public bool FeedBackFinish
        {
            get { return (bool)GetValue(FeedBackFinishProperty); }
            set
            {
                SetValue(FeedBackFinishProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public bool FromSToA
        {
            get { return (bool)GetValue(FromSToAProperty); }
            set
            {
                SetValue(FromSToAProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public bool FromAToB
        {
            get { return (bool)GetValue(FromAToBProperty); }
            set
            {
                SetValue(FromAToBProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public bool FromAToE
        {
            get { return (bool)GetValue(FromAToEProperty); }
            set
            {
                SetValue(FromAToEProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public bool FromSToB
        {
            get { return (bool)GetValue(FromSToBProperty); }
            set
            {
                SetValue(FromSToBProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public bool FromBToE
        {
            get { return (bool)GetValue(FromBToEProperty); }
            set
            {
                SetValue(FromBToEProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public Int32 IsWidth
        {
            get { return (int)GetValue(IsWidthProperty); }
            set
            {
                SetValue(IsWidthProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public int IsHeight
        {
            get { return (int)GetValue(IsHeightProperty); }
            set
            {
                SetValue(IsHeightProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public int Complete
        {
            get { return (int)GetValue(CompleteProperty); }
            set
            {
                SetValue(CompleteProperty, value);
            }
        }

        private bool run = false;
        public bool Run
        {
            get { return run; }
            set { run = value; }
        }

        private static void ValueChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Mesa led = (Mesa)d;
            if (led != null)
            {
                bool newbool = (bool)e.NewValue;
                led.UpdataSToA(newbool);
            }
        }

        private void UpdataSToA(bool state)
        {
            if (state)
            {
                IsBusy = true;
                IsActive= true;
                FeedBackFinish = false;
                NowPos = CurrentPos.A;
                Run = true;
                DoubleAnimation daWidth = new DoubleAnimation();
                //  daWidth.From = 50;
                daWidth.To = 450;

                Duration time = new Duration(TimeSpan.FromSeconds(3));
                daWidth.Duration = time;
                this.BeginAnimation(WidthProperty, daWidth);

                DoubleAnimation da = new DoubleAnimation();
                da.To = -275;
                da.BeginTime = TimeSpan.FromSeconds(3);
                Duration times = new Duration(TimeSpan.FromSeconds(2));
                da.Duration = times;
                da.Completed -= new EventHandler(MoveComplete);
                da.Completed += new EventHandler(MoveComplete);
                tt.X = 0;
                tt.BeginAnimation(TranslateTransform.XProperty, da);
            }
        }

        private static void SToBChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Mesa led = (Mesa)d;
            if (led != null)
            {
                bool newbool = (bool)e.NewValue;
                led.UpdatasToB(newbool);
            }
        }

        private void UpdatasToB(bool state)
        {
            if (state)
            {
                IsBusy = true;
                IsActive = true;
                FeedBackFinish = false;
                NowPos = CurrentPos.B;
                Run = true;
                DoubleAnimation daWidth = new DoubleAnimation();
                //  daWidth.From = 50;
                daWidth.To = 450;
                Duration time = new Duration(TimeSpan.FromSeconds(3));
                daWidth.Duration = time;
                this.BeginAnimation(WidthProperty, daWidth);


                DoubleAnimation da = new DoubleAnimation();
                da.To = -910;
                da.BeginTime = TimeSpan.FromSeconds(3);
                Duration times = new Duration(TimeSpan.FromSeconds(5));
                da.Duration = times;
                da.Completed -= new EventHandler(MoveComplete);
                da.Completed += new EventHandler(MoveComplete);
                tt.X = 0;
                tt.BeginAnimation(TranslateTransform.XProperty, da);
            }
        }
        private static void AToBChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Mesa led = (Mesa)d;
            if (led != null)
            {
                bool newbool = (bool)e.NewValue;
                led.UpdataAToB(newbool);
            }
        }

        private void UpdataAToB(bool state)
        {
            if (state)
            {
                IsBusy = true;
                IsActive = true;
                FeedBackFinish = false;
                NowPos = CurrentPos.B;
                DoubleAnimation da = new DoubleAnimation();
                da.To = -910;
                Duration times = new Duration(TimeSpan.FromSeconds(5));
                da.Duration = times;
                da.Completed -= new EventHandler(MoveComplete);
                da.Completed += new EventHandler(MoveComplete);
                tt.X = 0;
                tt.BeginAnimation(TranslateTransform.XProperty, da);
            }
        }

        private static void AToEChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Mesa led = (Mesa)d;
            if (led != null)
            {
                bool newbool = (bool)e.NewValue;
                led.UpdataAToE(newbool);
            }
        }

        private void UpdataAToE(bool state)
        {
            if (state)
            {
                IsBusy = true;
                IsActive = true;
                FeedBackFinish = false;
                NowPos = CurrentPos.E;
                DoubleAnimation da = new DoubleAnimation();
                da.To = -1800;
                Duration times = new Duration(TimeSpan.FromSeconds(10));
                da.Duration = times;
                da.Completed -= new EventHandler(MoveComplete);
                da.Completed += new EventHandler(MoveComplete);
                tt.X = 0;
                tt.BeginAnimation(TranslateTransform.XProperty, da);
            }
        }


        private static void BToEChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Mesa led = (Mesa)d;
            if (led != null)
            {
                bool newbool = (bool)e.NewValue;
                led.UpdataBToE(newbool);
            }
        }

        private void UpdataBToE(bool state)
        {
            if (state)
            {
                IsBusy = true;
                IsActive = true;
                FeedBackFinish = false;
                NowPos = CurrentPos.E;
                DoubleAnimation da = new DoubleAnimation();
                da.To =-1800;
                Duration times = new Duration(TimeSpan.FromSeconds(6));
                da.Duration = times;
                da.Completed -= new EventHandler(MoveComplete);
                da.Completed += new EventHandler(MoveComplete);
                tt.X = 0;
                tt.BeginAnimation(TranslateTransform.XProperty, da);
            }
        }


        private static void EnableChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Mesa led = (Mesa)d;
            //if (led != null)
            //{
            //    bool newbool = (bool)e.NewValue;
            //    led.UpdataEnable(newbool);
            //}
        }
        public void UpdataEnable(bool state)
        {
            if (state)
                this.Visibility = Visibility.Visible;
            else
                this.Visibility = Visibility.Hidden;
        }

        private static void WidthChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Mesa led = (Mesa)d;
            if (led != null)
            {
                int newbool = (int)e.NewValue;
                led.UpdataWidth(newbool);
            }
        }
        public void UpdataWidth(int state)
        {
            //if (!Run)
            //    mgrid.Width = state;
        }

        private static void HeightChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Mesa led = (Mesa)d;
            if (led != null)
            {
                int newbool = (int)e.NewValue;
                led.UpdataHeight(newbool);
            }
        }
        public void UpdataHeight(int state)
        {
            //if(!Run)
            //   mgrid.Height = state;
        }

        private static void BackgroundChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Mesa led = (Mesa)d;
            if (led != null)
            {
                Brush newbool = (Brush)e.NewValue;
                led.UpdataBackground(newbool);
            }
        }
        public void UpdataBackground(Brush state)
        {
            mgrid.Background = state;
        }

        private static void CompleteChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Mesa led = (Mesa)d;
            if (led != null)
            {
                bool newbool = (bool)e.NewValue;
                led.UpdataComplete(newbool);
            }
        }
        public void UpdataComplete(bool state)
        {
            if (state)
            {
                if (NowPos == CurrentPos.A)
                {
                    //this.Background = Brushes.Blue;
                    DoubleAnimation da = new DoubleAnimation();
                    da.To = -2200;
                    Duration times = new Duration(TimeSpan.FromSeconds(10));
                    da.Duration = times;
                    da.Completed -= new EventHandler(ReturnToZ);
                    da.Completed += new EventHandler(ReturnToZ);
                    tt.X = 0;
                    tt.BeginAnimation(TranslateTransform.XProperty, da);
                }
                else if(NowPos == CurrentPos.B)
                {
                   // this.Background = Brushes.Blue;
                    DoubleAnimation da = new DoubleAnimation();
                    da.To = -2200;
                    Duration times = new Duration(TimeSpan.FromSeconds(6));
                    da.Duration = times;
                    da.Completed -= new EventHandler(ReturnToZ);
                    da.Completed += new EventHandler(ReturnToZ);
                    tt.X = 0;
                    tt.BeginAnimation(TranslateTransform.XProperty, da);
                }
                else if (NowPos == CurrentPos.E)
                {
                    DoubleAnimation da = new DoubleAnimation();
                    da.To = -1800;
                    Duration times = new Duration(TimeSpan.FromSeconds(0.5));
                    da.Duration = times;
                    da.Completed -= new EventHandler(ReturnToZ);
                    da.Completed += new EventHandler(ReturnToZ);
                    tt.X = 0;
                    tt.BeginAnimation(TranslateTransform.XProperty, da);
                }
            }
        }

        private void ReturnToZ(object? obj,EventArgs e)
        {
            IsActive = false;
            FeedBackFinish = true;
            DoubleAnimation daWidth = new DoubleAnimation();
            daWidth.To = IsWidth;
            Duration time = new Duration(TimeSpan.FromSeconds(0.5));
            daWidth.Duration = time;
            this.BeginAnimation(WidthProperty, daWidth);

            DoubleAnimation da = new DoubleAnimation();
            da.To = 0;
            Duration times = new Duration(TimeSpan.FromSeconds(3));
            da.Duration = times;
            tt.X = 0;
            tt.BeginAnimation(TranslateTransform.XProperty, da);
        }

        private void MoveComplete(object? obj, EventArgs e)
        {
            IsBusy = false;
        }
    }

    public enum CurrentPos
    {
        S=1,
        A,
        B,
        E
    }
}
