using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WpfSunnyUILibrary;
using WpfSunnyUILibrary.Class;

namespace WpfSunnyUI.ViewModels
{
    public class ComboboxViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainWindowViewModel Mwvm;
        public ComboboxViewModel(IWindowManager iwindow, MainWindowViewModel mwm)
        {
            this.WindowManager = iwindow;
            this.Mwvm = mwm;

            Titlename1 = "CheckBox";
            Titlename2 = "CheckBoxGroup";
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

        /// <summary>
        /// 属性
        /// </summary>
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

    }
}
