using Caliburn.Micro;
using DRsoft.Runtime.Core.Platform.Events;
using Engine.Models;
using MaterialDesignThemes.Wpf;
using System.Windows.Threading;

namespace Engine.ViewModels.MainPageComponent
{
    public class MainMenuViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainWindowViewModel Mwvm;
        public List<NavigationItem> NavigationList { get; set; }

        public MainMenuViewModel(IWindowManager windowManager, MainWindowViewModel wvm)
        {
            this.WindowManager = windowManager;
            this.Mwvm = wvm;

            NavigationList = new List<NavigationItem>()
            {
                new NavigationItem()
                {
                    Title = "主页",
                    SelectedIcon = PackIconKind.Home,
                    UnselectedIcon = PackIconKind.HomeOutline
                },
                new NavigationItem()
                {
                    Title = "参数",
                    SelectedIcon = PackIconKind.Settings,
                    UnselectedIcon = PackIconKind.SettingsOutline
                },
                new NavigationItem()
                {
                    Title = "调试",
                    SelectedIcon = PackIconKind.Tools,
                    UnselectedIcon = PackIconKind.Tools
                },
                new NavigationItem()
                {
                    Title = "查询",
                    SelectedIcon = PackIconKind.DatabaseSearch,
                    UnselectedIcon = PackIconKind.DatabaseSearch
                },
                new NavigationItem()
                {
                    Title = "报警",
                    SelectedIcon = PackIconKind.AlarmLight,
                    UnselectedIcon = PackIconKind.AlarmLightOutline
                },
                new NavigationItem()
                {
                    Title = "登录",
                    SelectedIcon = PackIconKind.Login,
                    UnselectedIcon = PackIconKind.Logout
                },
                new NavigationItem()
                {
                    Title = "关于",
                    SelectedIcon = PackIconKind.Information,
                    UnselectedIcon = PackIconKind.InformationOutline
                }
            };

        }

        public void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dispatcher = Dispatcher.CurrentDispatcher;
            dispatcher.InvokeAsync(() =>
            {
                Mwvm.Menu_SelectionChanged(sender, e);
            });
        }

        #region 属性绑定

        private InteractiveDataModel? _interactiveData;

        public InteractiveDataModel? InteractiveData
        {
            get
            {
                _interactiveData = this.Mwvm.InteractiveData;
                return _interactiveData;
            }
            set
            {
                _interactiveData = value;
                NotifyOfPropertyChange(() => InteractiveData);
                this.Mwvm.InteractiveData = _interactiveData;
            }
        }


        #endregion

        #region 命令绑定

        #endregion
    }
}
