using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfSunnyUI.ViewModels
{
    public class ButtonViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainWindowViewModel Mwvm;
        public ButtonViewModel(IWindowManager iwindow, MainWindowViewModel mwm)
        {
            this.WindowManager = iwindow;
            this.Mwvm = mwm;
            Titlename1 = "Button";
            Titlename2 = "SymbolButton";
            Titlename3 = "SymbolButton按钮组";
            Titlename4 = "SwitchButton";
            Titlename5 = "ImageButton";
            LineColor = "Blue";

            Color backcolor = (Color)ColorConverter.ConvertFromString("#50A0FF");
            ButtonBackColor = new SolidColorBrush(backcolor);
            Color overcolor = (Color)ColorConverter.ConvertFromString("#73B3E1");
            MouseOverColor = new SolidColorBrush(overcolor);
            Color presscolor = (Color)ColorConverter.ConvertFromString("#40A8E4");
            MouseCapturedColor= new SolidColorBrush(presscolor);

            Color backcolor2 = (Color)ColorConverter.ConvertFromString("#6EBE28");
            ButtonBackColor2 = new SolidColorBrush(backcolor2);
            Color overcolor2 = (Color)ColorConverter.ConvertFromString("#8BCB53");
            MouseOverColor2 = new SolidColorBrush(overcolor2);
            Color presscolor2 = (Color)ColorConverter.ConvertFromString("#589820");
            MouseCapturedColor2 = new SolidColorBrush(presscolor2);

            Color backcolor3 = (Color)ColorConverter.ConvertFromString("#8C8C8C");
            ButtonBackColor3 = new SolidColorBrush(backcolor3);
            Color overcolor3 = (Color)ColorConverter.ConvertFromString("#A3A3A3");
            MouseOverColor3 = new SolidColorBrush(overcolor3);
            Color presscolor3 = (Color)ColorConverter.ConvertFromString("#707070");
            MouseCapturedColor3 = new SolidColorBrush(presscolor3);

            Color backcolor4 = (Color)ColorConverter.ConvertFromString("#DC9B28");
            ButtonBackColor4 = new SolidColorBrush(backcolor4);
            Color overcolor4 = (Color)ColorConverter.ConvertFromString("#E3AF53");
            MouseOverColor4 = new SolidColorBrush(overcolor4);
            Color presscolor4 = (Color)ColorConverter.ConvertFromString("#B07C20");
            MouseCapturedColor4 = new SolidColorBrush(presscolor4);

            Color backcolor5 = (Color)ColorConverter.ConvertFromString("#E65050");
            ButtonBackColor5 = new SolidColorBrush(backcolor5);
            Color overcolor5 = (Color)ColorConverter.ConvertFromString("#EB7373");
            MouseOverColor5 = new SolidColorBrush(overcolor5);
            Color presscolor5 = (Color)ColorConverter.ConvertFromString("#B84040");
            MouseCapturedColor5 = new SolidColorBrush(presscolor5);
        }

         

        /// <summary>
        /// 属性
        /// </summary>
        private string? _titlename1;
        public string? Titlename1
        {
            get => _titlename1;
            set
            {
                _titlename1 = value;
                NotifyOfPropertyChange(() => Titlename1);
            }
        }

        private string? _titlename2;
        public string? Titlename2
        {
            get => _titlename2;
            set
            {
                _titlename2 = value;
                NotifyOfPropertyChange(() => Titlename2);
            }
        }

        private string? _titlename3;
        public string? Titlename3
        {
            get => _titlename3;
            set
            {
                _titlename3 = value;
                NotifyOfPropertyChange(() => Titlename3);
            }
        }

        private string? _titlename4;
        public string? Titlename4
        {
            get => _titlename4;
            set
            {
                _titlename4 = value;
                NotifyOfPropertyChange(() => Titlename4);
            }
        }

        private string? _titlename5;
        public string? Titlename5
        {
            get => _titlename5;
            set
            {
                _titlename5 = value;
                NotifyOfPropertyChange(() => Titlename5);
            }
        }

        /// <summary>
        /// 线颜色 默认蓝色
        /// </summary>
        private string? _linecolor;
        public string? LineColor
        {
            get => _linecolor;
            set
            {
                _linecolor = value;
                NotifyOfPropertyChange(() => LineColor);
            }
        }
        #region color1
        /// <summary>
        /// 按钮背景色 默认透明
        /// </summary>
        private SolidColorBrush? _buttonbackcolor;
        public SolidColorBrush? ButtonBackColor
        {
            get => _buttonbackcolor;
            set
            {
                _buttonbackcolor = value;
                NotifyOfPropertyChange(() => ButtonBackColor);
            }
        }
        /// <summary>
        /// 按钮边框色 默认透明
        /// </summary>
        private SolidColorBrush? _bordercolor;
        public SolidColorBrush? BorderColor
        {
            get => _bordercolor;
            set
            {
                _bordercolor = value;
                NotifyOfPropertyChange(() => BorderColor);
            }
        }

        /// <summary>
        /// 按钮鼠标停留色 默认透明
        /// </summary>
        private SolidColorBrush? _mouseovercolor;
        public SolidColorBrush? MouseOverColor
        {
            get => _mouseovercolor;
            set
            {
                _mouseovercolor = value;
                NotifyOfPropertyChange(() => MouseOverColor);
            }
        }
        /// <summary>
        /// 按钮选中色 默认透明
        /// </summary>
        private SolidColorBrush? _mousecapturedcolor;
        public SolidColorBrush? MouseCapturedColor
        {
            get => _mousecapturedcolor;
            set
            {
                _mousecapturedcolor = value;
                NotifyOfPropertyChange(() => MouseCapturedColor);
            }
        }
        #endregion

        #region color2
        /// <summary>
        /// 按钮背景色 默认透明
        /// </summary>
        private SolidColorBrush? _buttonbackcolor2;
        public SolidColorBrush? ButtonBackColor2
        {
            get => _buttonbackcolor2;
            set
            {
                _buttonbackcolor2 = value;
                NotifyOfPropertyChange(() => ButtonBackColor2);
            }
        }
        /// <summary>
        /// 按钮边框色 默认透明
        /// </summary>
        private SolidColorBrush? _bordercolor2;
        public SolidColorBrush? BorderColor2
        {
            get => _bordercolor2;
            set
            {
                _bordercolor2 = value;
                NotifyOfPropertyChange(() => BorderColor2);
            }
        }

        /// <summary>
        /// 按钮鼠标停留色 默认透明
        /// </summary>
        private SolidColorBrush? _mouseovercolor2;
        public SolidColorBrush? MouseOverColor2
        {
            get => _mouseovercolor2;
            set
            {
                _mouseovercolor2 = value;
                NotifyOfPropertyChange(() => MouseOverColor2);
            }
        }
        /// <summary>
        /// 按钮选中色 默认透明
        /// </summary>
        private SolidColorBrush? _mousecapturedcolor2;
        public SolidColorBrush? MouseCapturedColor2
        {
            get => _mousecapturedcolor2;
            set
            {
                _mousecapturedcolor2 = value;
                NotifyOfPropertyChange(() => MouseCapturedColor2);
            }
        }
        #endregion

        #region color3
        /// <summary>
        /// 按钮背景色 默认透明
        /// </summary>
        private SolidColorBrush? _buttonbackcolor3;
        public SolidColorBrush? ButtonBackColor3
        {
            get => _buttonbackcolor3;
            set
            {
                _buttonbackcolor3 = value;
                NotifyOfPropertyChange(() => ButtonBackColor3);
            }
        }
        /// <summary>
        /// 按钮边框色 默认透明
        /// </summary>
        private SolidColorBrush? _bordercolor3;
        public SolidColorBrush? BorderColor3
        {
            get => _bordercolor3;
            set
            {
                _bordercolor3 = value;
                NotifyOfPropertyChange(() => BorderColor3);
            }
        }

        /// <summary>
        /// 按钮鼠标停留色 默认透明
        /// </summary>
        private SolidColorBrush? _mouseovercolor3;
        public SolidColorBrush? MouseOverColor3
        {
            get => _mouseovercolor3;
            set
            {
                _mouseovercolor3 = value;
                NotifyOfPropertyChange(() => MouseOverColor3);
            }
        }
        /// <summary>
        /// 按钮选中色 默认透明
        /// </summary>
        private SolidColorBrush? _mousecapturedcolor3;
        public SolidColorBrush? MouseCapturedColor3
        {
            get => _mousecapturedcolor3;
            set
            {
                _mousecapturedcolor3 = value;
                NotifyOfPropertyChange(() => MouseCapturedColor3);
            }
        }
        #endregion

        #region color4
        /// <summary>
        /// 按钮背景色 默认透明
        /// </summary>
        private SolidColorBrush? _buttonbackcolor4;
        public SolidColorBrush? ButtonBackColor4
        {
            get => _buttonbackcolor4;
            set
            {
                _buttonbackcolor4 = value;
                NotifyOfPropertyChange(() => ButtonBackColor4);
            }
        }
        /// <summary>
        /// 按钮边框色 默认透明
        /// </summary>
        private SolidColorBrush? _bordercolor4;
        public SolidColorBrush? BorderColor4
        {
            get => _bordercolor4;
            set
            {
                _bordercolor4 = value;
                NotifyOfPropertyChange(() => BorderColor4);
            }
        }

        /// <summary>
        /// 按钮鼠标停留色 默认透明
        /// </summary>
        private SolidColorBrush? _mouseovercolor4;
        public SolidColorBrush? MouseOverColor4
        {
            get => _mouseovercolor4;
            set
            {
                _mouseovercolor4 = value;
                NotifyOfPropertyChange(() => MouseOverColor4);
            }
        }
        /// <summary>
        /// 按钮选中色 默认透明
        /// </summary>
        private SolidColorBrush? _mousecapturedcolor4;
        public SolidColorBrush? MouseCapturedColor4
        {
            get => _mousecapturedcolor4;
            set
            {
                _mousecapturedcolor4 = value;
                NotifyOfPropertyChange(() => MouseCapturedColor4);
            }
        }
        #endregion

        #region color5
        /// <summary>
        /// 按钮背景色 默认透明
        /// </summary>
        private SolidColorBrush? _buttonbackcolor5;
        public SolidColorBrush? ButtonBackColor5
        {
            get => _buttonbackcolor5;
            set
            {
                _buttonbackcolor5 = value;
                NotifyOfPropertyChange(() => ButtonBackColor5);
            }
        }
        /// <summary>
        /// 按钮边框色 默认透明
        /// </summary>
        private SolidColorBrush? _bordercolor5;
        public SolidColorBrush? BorderColor5
        {
            get => _bordercolor5;
            set
            {
                _bordercolor5 = value;
                NotifyOfPropertyChange(() => BorderColor5);
            }
        }

        /// <summary>
        /// 按钮鼠标停留色 默认透明
        /// </summary>
        private SolidColorBrush? _mouseovercolor5;
        public SolidColorBrush? MouseOverColor5
        {
            get => _mouseovercolor5;
            set
            {
                _mouseovercolor5 = value;
                NotifyOfPropertyChange(() => MouseOverColor5);
            }
        }
        /// <summary>
        /// 按钮选中色 默认透明
        /// </summary>
        private SolidColorBrush? _mousecapturedcolor5;
        public SolidColorBrush? MouseCapturedColor5
        {
            get => _mousecapturedcolor5;
            set
            {
                _mousecapturedcolor5 = value;
                NotifyOfPropertyChange(() => MouseCapturedColor5);
            }
        }
        #endregion


    }
}
