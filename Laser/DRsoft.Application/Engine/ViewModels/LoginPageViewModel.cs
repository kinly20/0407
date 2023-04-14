using Caliburn.Micro;
using Engine.Models;
using DRsoft.Runtime.Core.DataBase.Common.Extentions;
using DRsoft.Runtime.Core.DBService.Interface;
using DRsoft.Runtime.Core.Platform.Ioc;
using DRsoft.Engine.Model.DataBaseTable;
using DRsoft.Runtime.Core.Platform.Logging;
using DRsoft.Runtime.Core.Platform.Events;

namespace Engine.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainWindowViewModel Mwvm;
        public IDataBase? Idatabase;
        public Dictionary<string, string> UserLog = new Dictionary<string, string>();
        public List<UserLogin> UserManagers = new List<UserLogin>();
        public readonly ILog Log;
        public PubSubEvent<Login> RoleChangeEventManager;
        public Login CurrentRole =new Login();
        public LoginPageViewModel(IWindowManager iwindow, MainWindowViewModel mwm)
        {
            this.WindowManager = iwindow;
            this.Mwvm = mwm;
            Log = LogProvider.GetLogger(this.GetType());
            //添加用户名称
            ComMsg = new List<string>();
            try
            {
                RoleChangeEventManager = Mwvm.Aggregator.GetEvent<PubSubEvent<Login>>();
                Idatabase = ServiceProviderManager.Instance.ServiceProvider.GetService<IDataBase>();
                var userListTable = new List<UserLogin>();//Idatabase!.GetAll<UserLogin>();
                Array ararayUserListTable = userListTable.ToArray();
                UserManagers = ararayUserListTable.CastToList<UserLogin>();
                foreach (var ul in UserManagers)
                {
                    ComMsg.Add(ul.UserName!);
                    UserLog.Add(ul.UserName!, ul.Password!);
                }
                if (!ComMsg.Contains("Administrator"))
                {
                    Idatabase!.Create(new UserLogin
                    {
                        UserName = "Administrator",
                        Password = "123",
                        DebugLimit = true,
                        ParamSetLimit = true,
                        MarkingLimit = true,
                        PhotoLimit = true
                    });
                    var ul = new UserLogin
                    {
                        UserName = "Administrator",
                        Password = "123",
                        DebugLimit = false,
                        ParamSetLimit = false,
                        MarkingLimit = false,
                        PhotoLimit = false
                    };
                    UserManagers.Add(ul);
                    ComMsg.Add(ul.UserName);
                    UserLog.Add(ul.UserName!, ul.Password!);
                }
                if(!ComMsg.Contains("Observer"))
                {
                    Idatabase!.Create(new UserLogin
                    {
                        UserName = "Observer",
                        Password = "123",
                        DebugLimit = false,
                        ParamSetLimit = false,
                        MarkingLimit = false,
                        PhotoLimit = false
                    });
                    var ul = new UserLogin
                    {
                        UserName = "Observer",
                        Password = "123",
                        DebugLimit = false,
                        ParamSetLimit = false,
                        MarkingLimit = false,
                        PhotoLimit = false
                    };
                    UserManagers.Add(ul);
                    ComMsg.Add(ul.UserName);
                    UserLog.Add(ul.UserName!, ul.Password!);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"数据库连接失败!{ex}");
                this.Mwvm.Log?.ErrorFormat($"登录页面-数据库连接失败!{ex}");
            }
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

        private string? _userName = "Administrator";
        public string? UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }
        private string? _password;
        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }
        private List<string> _comMsg = null!;
        public List<string> ComMsg
        {
            get => _comMsg;
            set
            {
                _comMsg = value;
                NotifyOfPropertyChange(() => ComMsg);
            }
        }


        private string? _newUserName = "";
        public string? NewUserName
        {
            get => _newUserName;
            set
            {
                _newUserName = value;
                NotifyOfPropertyChange(() => NewUserName);
            }
        }
        private string? _newPassword;
        public string? NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                NotifyOfPropertyChange(() => NewPassword);
            }
        }

        private bool _debugLimit;
        public bool DebugLimit
        {
            get => _debugLimit;
            set
            {
                _debugLimit = value;
                NotifyOfPropertyChange(() => DebugLimit);
            }
        }

        private bool _paramSetLimit;
        public bool ParamSetLimit
        {
            get => _paramSetLimit;
            set
            {
                _paramSetLimit = value;
                NotifyOfPropertyChange(() => ParamSetLimit);
            }
        }

        private Visibility? _visibility = Visibility.Hidden;
        public Visibility? Visibilitys
        {
            get => _visibility;
            set
            {
                _visibility = value;
                NotifyOfPropertyChange(() => Visibilitys);
            }
        }

        private string? _limitContext = "";
        public string? LimitContext
        {
            get => _limitContext;
            set
            {
                _limitContext = value;
                NotifyOfPropertyChange(() => LimitContext);
            }
        }

        #endregion

        public void Login()
        {
            if (UserLog.ContainsKey(UserName!) && UserLog[UserName!] == Password)
            {
                foreach (UserLogin user in UserManagers)
                {
                    if (user.UserName == UserName && user.Password == Password)
                    {
                        Const.CurrentRole = user;
                        CurrentRole.UserName= user.UserName;
                        CurrentRole.Password = user.Password;
                        CurrentRole.DebugLimit= user.DebugLimit;
                        CurrentRole.MarkingLimit= user.MarkingLimit;
                        CurrentRole.PhotoLimit= user.PhotoLimit;
                        CurrentRole.ParamSetLimit= user.ParamSetLimit;
                        RoleChangeEventManager.Publish(CurrentRole);
                        Mwvm.UserName = "用户名: " + CurrentRole.UserName;
                        break;
                    }
                }
                if (UserName == "Administrator")
                {
                    Visibilitys = Visibility.Visible;
                }
                else
                    Visibilitys = Visibility.Hidden;

            }
            else
            {
                MessageBox.Show("用户名或密码错误!");
                this.Mwvm.Log?.ErrorFormat("登录页面-用户名或密码错误!");
            }
        }
        public void Cancel()
        {
            Visibilitys = Visibility.Hidden;
        }
        public void Confirm()
        {
            if (NewUserName == "" || NewPassword == "")
            {
                MessageBox.Show("用户名或密码不能为空!");
                this.Mwvm.Log?.ErrorFormat("登录页面-用户名或密码不能为空!");
            }
            else
            {
                if (ComMsg.Contains(NewUserName!))
                {
                    MessageBox.Show("与系统中的用户名重名!");
                    this.Mwvm.Log?.ErrorFormat("登录页面-与系统中的用户名重名!");
                    return;
                }
                var ul = new UserLogin
                {
                    UserName = NewUserName,
                    Password = NewPassword,
                    DebugLimit = DebugLimit,
                    ParamSetLimit = ParamSetLimit
                };
                ComMsg.Add(ul.UserName!);
                Idatabase!.Create(new UserLogin
                {
                    UserName = ul.UserName,
                    Password = ul.Password,
                    DebugLimit = DebugLimit,
                    ParamSetLimit = ParamSetLimit
                });
                Log.Info($"新增用户名:{ul.UserName}");
                UserLog.Add(ul.UserName!, ul.Password!);
                MessageBox.Show($"新增用户名:{ul.UserName}成功!");
                this.Mwvm.Log?.Info($"登录页面-新增用户名:{ul.UserName}成功!");
                Visibilitys = Visibility.Hidden;
            }
        }

        public void NewCancel()
        {
            Visibilitys = Visibility.Hidden;
        }

        public void CheckLimit()
        {
            var role = new RoleSettingViewModel(WindowManager, Mwvm, this);
            WindowManager.ShowDialogAsync(role);
      
        }
    }
}
