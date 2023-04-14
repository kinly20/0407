using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml.Linq;


namespace Engine
{
    /// <summary>
    /// Granty.xaml 的交互逻辑
    /// </summary>
    public partial class Granty :UserControl
    {
        public Granty()
        {
            InitializeComponent();
            this.Background = Brushes.Transparent;
   
        }
        DoubleAnimation mainAnimation=new DoubleAnimation();

        public static readonly DependencyProperty StartColorProperty = DependencyProperty.Register("StartColor", typeof(Brush), typeof(Granty),new FrameworkPropertyMetadata((Brush)Brushes.LightPink, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty EndColorProperty = DependencyProperty.Register("EndColor", typeof(Brush), typeof(Granty));

        public static readonly DependencyProperty IscheckOneProperty = DependencyProperty.Register("IscheckOne", typeof(bool), typeof(Granty), new PropertyMetadata((bool)false, new PropertyChangedCallback(ValueChanges)));
        public static readonly DependencyProperty IscheckTwoProperty = DependencyProperty.Register("IscheckTwo", typeof(bool), typeof(Granty),new PropertyMetadata((bool)false, new PropertyChangedCallback(ValueChanges)));
        public static readonly DependencyProperty IscheckThreeProperty = DependencyProperty.Register("IscheckThree", typeof(bool), typeof(Granty), new PropertyMetadata((bool)false, new PropertyChangedCallback(ValueChanges)));
        public static readonly DependencyProperty IscheckFourProperty = DependencyProperty.Register("IscheckFour", typeof(bool), typeof(Granty), new PropertyMetadata((bool)false, new PropertyChangedCallback(ValueChanges)));
        public static readonly DependencyProperty IscheckFiveProperty = DependencyProperty.Register("IscheckFive", typeof(bool), typeof(Granty), new PropertyMetadata((bool)false, new PropertyChangedCallback(ValueChanges)));
        public static readonly DependencyProperty IscheckSixProperty = DependencyProperty.Register("IscheckSix", typeof(bool), typeof(Granty), new PropertyMetadata((bool)false, new PropertyChangedCallback(ValueChanges)));
        public static readonly DependencyProperty IsErrorProperty = DependencyProperty.Register("IsError", typeof(bool), typeof(Granty), new PropertyMetadata((bool)false, new PropertyChangedCallback(ErrorStateChange)));

        public static readonly DependencyProperty CurrentIdProperty = DependencyProperty.Register("CurrentId", typeof(int), typeof(Granty), new PropertyMetadata((int)0));
        public static readonly DependencyProperty NameIdProperty = DependencyProperty.Register("NameId", typeof(string), typeof(Granty), new PropertyMetadata("", new PropertyChangedCallback(NameChange)));
        public static readonly DependencyProperty OrdNumberProperty = DependencyProperty.Register("OrdNumber", typeof(int), typeof(Granty), new PropertyMetadata(0, new PropertyChangedCallback(OrdNumberChange)));
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(string), typeof(Granty), new PropertyMetadata("Idle", new PropertyChangedCallback(StateChange)));
        public static readonly DependencyProperty ErrorMsgProperty = DependencyProperty.Register("ErrorMsg", typeof(string), typeof(Granty), new PropertyMetadata("龙门正常", new PropertyChangedCallback(ErrorChange)));
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(Granty), new PropertyMetadata(false));

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
        public Brush StartColor
        {
            get { return (Brush)GetValue(StartColorProperty); }
            set
            {
                SetValue(StartColorProperty, value);
            }
        }
        [Bindable(true)]
        [Category("Appearance")]
        public Brush EndColor
        {
            get { return (Brush)GetValue(EndColorProperty); }
            set { SetValue(EndColorProperty, value);}
        }
        [Bindable(true)]
        [Category("Appearance")]
        public bool IscheckOne
        {
            get { return (bool)GetValue(IscheckOneProperty); }
            set
            {
                SetValue(IscheckOneProperty, value);
            }
        }
        [Bindable(true)]
        [Category("Appearance")]
        public bool IscheckTwo
        {
            get { return (bool)GetValue(IscheckTwoProperty); }
            set
            {
                SetValue(IscheckTwoProperty, value);
            }
        }
        [Bindable(true)]
        [Category("Appearance")]
        public bool IscheckThree
        {
            get { return (bool)GetValue(IscheckThreeProperty); }
            set
            {
                SetValue(IscheckThreeProperty, value);
            }
        }
        [Bindable(true)]
        [Category("Appearance")]
        public bool IscheckFour
        {
            get { return (bool)GetValue(IscheckFourProperty); }
            set
            {
                SetValue(IscheckFourProperty, value);
            }
        }
        [Bindable(true)]
        [Category("Appearance")]
        public bool IscheckFive
        {
            get { return (bool)GetValue(IscheckFiveProperty); }
            set
            {
                SetValue(IscheckFiveProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public bool IscheckSix
        {
            get { return (bool)GetValue(IscheckSixProperty); }
            set
            {
                SetValue(IscheckSixProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public bool IsError
        {
            get { return (bool)GetValue(IsErrorProperty); }
            set
            {
                SetValue(IsErrorProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public int CurrentId
        {
            get { return (int)GetValue(CurrentIdProperty); }
            set
            {
                SetValue(CurrentIdProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public string NameId
        {
            get { return (string)GetValue(NameIdProperty); }
            set
            {
                SetValue(NameIdProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public int OrdNumber
        {
            get { return (int)GetValue(OrdNumberProperty); }
            set
            {
                SetValue(OrdNumberProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public string State
        {
            get { return (string)GetValue(StateProperty); }
            set
            {
                SetValue(StateProperty, value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public string ErrorMsg
        {
            get { return (string)GetValue(ErrorMsgProperty); }
            set
            {
                SetValue(ErrorMsgProperty, value);
            }
        }

        private static void ValueChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Granty led = (Granty)d;
            DependencyProperty h =e.Property;
            if (led != null)
            {
                bool oldbool = (bool)e.OldValue;
                bool newbool = (bool)e.NewValue;
                int index = 0;
                switch(h.Name)
                {
                    case "IscheckOne":
                        index = 1;
                        break;
                    case "IscheckTwo":
                        index = 2;
                        break;
                    case "IscheckThree":
                        index = 3;
                        break;
                    case "IscheckFour":
                        index = 4;
                        break;
                    case "IscheckFive":
                        index = 5;
                        break;
                    case "IscheckSix":
                        index = 6;
                        break;
                }
                led.UpdataValueground(newbool,index);
            }
        }
        private void UpdataValueground(bool newbool,int index)
        {
            switch(index)
            {
                case 1:
                    if (newbool)
                    {
                        
                        this.Rec1.Fill = EndColor;
                    }
                    else
                        this.Rec1.Fill = StartColor;
                    break;
                case 2:
                    if (newbool)
                    {
                        this.Rec2.Fill = EndColor;
                    }
                    else
                        this.Rec2.Fill = StartColor;
                    break;
                case 3:
                    if (newbool)
                    {
                        this.Rec3.Fill = EndColor;
                    }
                    else
                        this.Rec3.Fill = StartColor;
                    break;
                case 4:
                    if (newbool)
                    {
                        this.Rec4.Fill = EndColor;
                    }
                    else
                        this.Rec4.Fill = StartColor;
                    break;
                case 5:
                    if (newbool)
                    {
                        this.Rec5.Fill = EndColor;
                    }
                    else
                        this.Rec5.Fill = StartColor;
                    break;
                case 6:
                    if (newbool)
                    {
                        this.Rec6.Fill = EndColor;
                    }
                    else
                        this.Rec6.Fill = StartColor;
                    break;
            }
        }

        private static void NameChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Granty led = (Granty)d;
            if (led != null)
            {

                string oldbool = (string)e.OldValue;
                string newbool = (string)e.NewValue;
                led.UpdataName(newbool);
            }
        }
        private void UpdataName( string name)
        {
            tbName.Text = name;
        }

        private static void OrdNumberChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Granty led = (Granty)d;
            if (led != null)
            {
                int oldbool = (int)e.OldValue;
                int newbool = (int)e.NewValue;
                int num=Math.Abs(oldbool-newbool);
                led.UpdataOrdNumber(newbool,num);
            }
        }
        private void UpdataOrdNumber(int  index,int num)
        {
            IsBusy = true;
            order.Text = index.ToString();
            if(index<=30 && index>=0)
            {
                DoubleAnimation daX=new DoubleAnimation();
                daX.To = 140 * index;
                Duration time=new Duration(TimeSpan.FromSeconds(0.5* num));
                daX.Duration = time;
                daX.Completed += new EventHandler(MoveComplete);
                this.tt.BeginAnimation(TranslateTransform.XProperty, daX);
            }
        }
        private static void ErrorStateChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Granty led = (Granty)d;
            if (led != null)
            {
                bool oldbool = (bool)e.OldValue;
                bool newbool = (bool)e.NewValue;
                led.UpdataErrorState(newbool);
            }
        }
        private void UpdataErrorState(bool newbool)
        {
            if (newbool)
            {
                MainGrid.Background = Brushes.Red;
            }
            else
            {
                MainGrid.Background = Brushes.Transparent;
            }
        }

        private static void StateChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Granty granty = (Granty)d;
            if (granty != null)
            {

                string oldstring = (string)e.OldValue;
                string newstring = (string)e.NewValue;
                granty.UpdateState(newstring);
            }
        }

        private void UpdateState(string msg)
        {
           this.tbState.Text= msg;
        }

        private static void ErrorChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Granty granty = (Granty)d;
            if (granty != null)
            {
                string oldstring = (string)e.OldValue;
                string newstring = (string)e.NewValue;
                granty.UpdateErrorMsg(newstring);
            }
        }
        private void UpdateErrorMsg(string msg)
        {
            this.maintooltip.Content= msg;
        }
        private void MoveComplete(object? obj, EventArgs e)
        {
            IsBusy = false;
        }
    }
}
