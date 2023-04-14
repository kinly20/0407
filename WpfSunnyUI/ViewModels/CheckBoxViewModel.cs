using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Animation;
using WpfSunnyUILibrary;
using WpfSunnyUILibrary.Class;
using static System.Net.Mime.MediaTypeNames;

namespace WpfSunnyUI.ViewModels
{
    public class CheckBoxViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainWindowViewModel Mwvm;
        public CheckBoxViewModel(IWindowManager iwindow, MainWindowViewModel mwm)
        {
            this.WindowManager = iwindow;
            this.Mwvm = mwm;

            Titlename1 = "CheckBox";
            Titlename2 = "CheckBoxGroup";

            CheckBoxList = new List<checkboxvalue> {
                new checkboxvalue("checkbox1", false),
                new checkboxvalue("checkbox2", true),
                new checkboxvalue("checkbox3", false),
                new checkboxvalue("checkbox4", false),
                new checkboxvalue("checkbox5", false),
                new checkboxvalue("checkbox6", false),
                new checkboxvalue("checkbox7", false),
                new checkboxvalue("checkbox8", false),
                new checkboxvalue("checkbox9", false),
                new checkboxvalue("checkbox10", false),
                new checkboxvalue("checkbox11", false),
                new checkboxvalue("checkbox12", false)
             };


            Color backcolor = (Color)ColorConverter.ConvertFromString("#50A0FF");
            ButtonBackColor = new SolidColorBrush(backcolor);
            Color overcolor = (Color)ColorConverter.ConvertFromString("#73B3E1");
            MouseOverColor = new SolidColorBrush(overcolor);
            Color presscolor = (Color)ColorConverter.ConvertFromString("#40A8E4");
            MouseCapturedColor = new SolidColorBrush(presscolor);
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



        /// <summary>
        /// checkbox列表
        /// </summary>
        private List<checkboxvalue>? _checkboxList;
        public List<checkboxvalue>? CheckBoxList
        {
            get => _checkboxList;
            set
            {
                _checkboxList = value;
                NotifyOfPropertyChange(() => CheckBoxList);
            }
        }


        /// <summary>
        /// 事件
        /// </summary>
        public void showselect()
        {
            string text = "";
            for (int i = 0; i < CheckBoxList.Count; i++)
            {
                if (CheckBoxList[i].ischeck)
                    text += CheckBoxList[i].name + ";";
            }
            System.Windows.MessageBox.Show(text.ToString());
        }
    }
}
