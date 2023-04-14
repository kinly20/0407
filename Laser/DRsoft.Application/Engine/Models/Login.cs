namespace Engine.Models
{
    public class Login: NotifyBase
    {
        private string? _userName;
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

        private bool? _debugLimit;
        public bool? DebugLimit
        {
            get => _debugLimit;
            set
            {
                _debugLimit = value;
                NotifyOfPropertyChange(() => DebugLimit);
            }
        }

        private bool? _paramSetLimit;
        public bool? ParamSetLimit
        {
            get => _paramSetLimit;
            set 
            {
                _paramSetLimit = value;
                NotifyOfPropertyChange(() => ParamSetLimit);
            }
        }

        private bool? _markingLimit;
        public bool? MarkingLimit
        {
            get => _markingLimit;
            set
            {
                _markingLimit = value;
                NotifyOfPropertyChange(() => MarkingLimit);
            }
        }

        private bool? _photoLimit;
        public bool? PhotoLimit
        {
            get => _photoLimit;
            set
            {
                _photoLimit = value;
                NotifyOfPropertyChange(() => PhotoLimit);
            }
        }
    }
}
