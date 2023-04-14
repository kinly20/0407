using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using WpfSunnyUI.Views;

namespace WpfSunnyUI.ViewModels
{
    public class MainWindowViewModel : Conductor<object>
    {
        public readonly IWindowManager WindowManager;
        public MainWindowViewModel Mwvm;

        public MainWindowViewModel(IWindowManager windowManager)
        {
            Mwvm = this;
            this.WindowManager = windowManager;
           
            SelectTab = 0;
            ButtonViewContent = new ButtonViewModel(WindowManager, Mwvm);
            CheckBoxViewContent = new CheckBoxViewModel(WindowManager, Mwvm);
            ComboboxViewContent=new ComboboxViewModel(WindowManager, Mwvm);

        }
        /// <summary>
        /// 属性
        /// </summary>
        /// 
        private ButtonViewModel? _buttonViewContent;
        public ButtonViewModel? ButtonViewContent
        {
            get => _buttonViewContent;
            set { _buttonViewContent = value; NotifyOfPropertyChange(() => ButtonViewContent); }
        }

        private CheckBoxViewModel? _checkboxViewContent;
        public CheckBoxViewModel? CheckBoxViewContent
        {
            get => _checkboxViewContent;
            set { _checkboxViewContent = value; NotifyOfPropertyChange(() => CheckBoxViewContent); }
        }

        private ComboboxViewModel? _comboboxViewContent;
        public ComboboxViewModel? ComboboxViewContent
        {
            get => _comboboxViewContent;
            set { _comboboxViewContent = value; NotifyOfPropertyChange(() => ComboboxViewContent); }
        }




        private int? _selectTab;
        public int? SelectTab
        {
            get => _selectTab;
            set
            {
                _selectTab = value;
                NotifyOfPropertyChange(() => SelectTab);
            }
        }

     

        /// <summary>
        /// 事件
        /// </summary>
        public void Changge()
        {
            if (SelectTab < 3)
                SelectTab++;
            else
                SelectTab = 0;
          
        }

        public void Close()
        {
        }
    }
}
