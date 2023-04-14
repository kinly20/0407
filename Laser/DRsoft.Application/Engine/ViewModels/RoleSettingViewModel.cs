using Caliburn.Micro;
using Engine.Transfer;
using System.Collections.ObjectModel;
using Engine.Models;
using DRsoft.Engine.Model.DataBaseTable;
using DRsoft.Runtime.Core.Platform.Logging;
using DRsoft.Runtime.Core.Platform.Events;



namespace Engine.ViewModels
{
    public class RoleSettingViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainWindowViewModel Mwvm;
        public LoginPageViewModel Login;
        public Login Lg = new Login();

        public RoleSettingViewModel(IWindowManager iwindow, MainWindowViewModel mwm, LoginPageViewModel login)
        {
            this.WindowManager = iwindow;
            this.Mwvm = mwm;
            this.Login = login;

            Logins = new ObservableCollection<Login>();

            foreach (var lg in from us in Login.UserManagers where us.UserName != "Administrator" && us.UserName != "Observer"
                               select new Login
                     {
                         UserName = us.UserName,
                         Password = us.Password,
                         ParamSetLimit = us.ParamSetLimit,
                         DebugLimit = us.DebugLimit,
                         MarkingLimit = us.MarkingLimit,
                         PhotoLimit = us.PhotoLimit
                     })
            {
                Logins.Add(lg);
            }
        }


        #region 属性绑定
        private ObservableCollection<Login> _logins = new ObservableCollection<Login>();
        public ObservableCollection<Login> Logins
        {
            get => _logins;
            set
            {
                _logins = value;
                NotifyOfPropertyChange(() => Logins);
            }
        }

        private string? _userName = "";
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

        private int _selectIndex;
        public int SelectIndex
        {
            get => _selectIndex;
            set
            {
                _selectIndex = value;
                NotifyOfPropertyChange(() => SelectIndex);
            }
        }

        #endregion

        public void AddUser()
        {
            if (UserName == "" || Password == "")
            {
                MessageBox.Show("用户名或密码不能为空!");
                this.Mwvm.Log?.Info("授权页面-用户名或密码不能为空!");
            }
            else
            {
                if (Login.ComMsg.Contains(UserName!))
                {
                    MessageBox.Show("与系统中的用户名重名!");
                    this.Mwvm.Log?.Info("授权页面-与系统中的用户名重名!");
                    return;
                }
                var ul = new UserLogin
                {
                    UserName = UserName,
                    Password = Password,
                    DebugLimit = false,
                    ParamSetLimit = false,
                    MarkingLimit = false,
                    PhotoLimit = false
                };
                Login.ComMsg.Add(ul.UserName!);
                Login.UserManagers.Add(ul);
                Login.Idatabase!.Create(new UserLogin
                {
                    UserName = ul.UserName,
                    Password = ul.Password,
                    DebugLimit = ul.DebugLimit,
                    ParamSetLimit = ul.ParamSetLimit,
                    MarkingLimit = ul.MarkingLimit,
                    PhotoLimit = ul.PhotoLimit
                });
                var lg = new Login
                {
                    UserName = UserName,
                    Password = Password,
                    DebugLimit = false,
                    ParamSetLimit = false,
                    MarkingLimit = false,
                    PhotoLimit = false
                };
                Logins.Add(lg);
                Login.UserLog.Add(ul.UserName!, ul.Password!);
                MessageBox.Show($"新增用户名:{ul.UserName}成功!");
                this.Mwvm.Log?.Info($"授权页面-新增用户名:{ul.UserName}成功!");
            }
        }
        public void DeleteUser()
        {
            if (SelectIndex < 0)
                return;
            var name = Logins[SelectIndex].UserName;
            Login.UserLog.Remove(name!);
            Login.ComMsg.Remove(name!);
            try
            {
                foreach (var ul in Login.UserManagers.Where(ul => ul.UserName == name))
                {
                    Login.UserManagers.Remove(ul);
                    Login.Idatabase!.Delete(new UserLogin
                    {
                        UserName = ul.UserName,
                        Password = ul.Password,
                        DebugLimit = ul.DebugLimit,
                        ParamSetLimit = ul.ParamSetLimit,
                        MarkingLimit = ul.MarkingLimit,
                        PhotoLimit = ul.PhotoLimit
                    });
                    break;
                }
            }
            // ReSharper disable once RedundantCatchClause
            catch
            {
                throw;
            }
            // Login.userManagers.Remove()
            Logins.RemoveAt(SelectIndex);
        }

        private RelayCommand _closeWindowCommand = null!;
        public RelayCommand CloseWindowCommand
        {
            get
            {
                if (_closeWindowCommand == null) _closeWindowCommand = new RelayCommand((_) => CloseWindow());
                return _closeWindowCommand;
            }
            set => _closeWindowCommand = value;
        }

        private void CloseWindow()
        {
            Login.Visibilitys = Visibility.Hidden;
            try
            {
                foreach (var lg in Logins)
                {
                    Login.Idatabase!.Update(new UserLogin
                    {
                        UserName = lg.UserName,
                        Password = lg.Password,
                        DebugLimit = (bool)lg.DebugLimit,
                        ParamSetLimit = (bool)lg.ParamSetLimit,
                        MarkingLimit = (bool)lg.MarkingLimit,
                        PhotoLimit = (bool)lg.PhotoLimit
                    });
                    var uu = Login.UserManagers.Find(q => q.UserName == lg.UserName);
                    uu!.DebugLimit = (bool)lg.DebugLimit;
                    uu.ParamSetLimit = (bool)lg.ParamSetLimit;
                    uu.MarkingLimit = (bool)lg.MarkingLimit;
                    uu.PhotoLimit = (bool)lg.PhotoLimit;

                }
            }
            catch
            {
                MessageBox.Show("数据库连接失败!");
                this.Mwvm.Log?.Info($"授权页面-数据库连接失败!");
            }
        }
    }
}
