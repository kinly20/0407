namespace Engine.Models
{
    public class NewAlarm : NotifyBase
    {
        private int? _key;
        public int? Key
        {
            get => _key;
            set
            {
                _key = value;
                NotifyOfPropertyChange(() => Key);
            }
        }

        private string? _values;
        public string? Value
        {
            get => _values;
            set
            {
                _values = value;
                NotifyOfPropertyChange(() => Value);
            }
        }

        private string? _errorTime;

        public string? ErrorTime
        {
            get => _errorTime;
            set
            {
                _errorTime = value;
                NotifyOfPropertyChange(() => ErrorTime);
            }
        }
        //public string? ErrorTime { get; set; }

    }
}
