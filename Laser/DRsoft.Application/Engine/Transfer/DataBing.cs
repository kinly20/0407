using System.ComponentModel;

// ReSharper disable All

namespace Engine.Transfer
{
    public class DataBing : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private List<AlarmNew> alarmLis = new List<AlarmNew>();

        public List<AlarmNew> AlarmLis
        {
            get => alarmLis;
            set
            {
                alarmLis = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this,
                        new PropertyChangedEventArgs("AlarmLis")); //当Name的属性值发生改变时，PropertyChanged事件触发
                }
            }
        }
    }

    public class AlarmNew
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
        public string? ErrorTime { get; set; }
    }
}