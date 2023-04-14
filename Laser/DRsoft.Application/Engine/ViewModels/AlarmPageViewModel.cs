using Caliburn.Micro;
using Engine.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Threading;
using DRsoft.Runtime.Core.Platform.Logging;
using DRsoft.Engine.Model.DataBaseTable;
using DRsoft.Runtime.Core.DataBase.Common;
using Engine.ViewModels.MainPageComponent;
using DRsoft.Runtime.Core.DataBase.Common.Extentions;
using System.Threading;

namespace Engine.ViewModels
{
    public class AlarmPageViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainWindowViewModel Mwvm;

        //读取报警配置
        public Dictionary<int, string> AlarmRecord = new Dictionary<int, string>();
        //记录报警的ID
        public List<int> RecordIds = new List<int>();
        public List<AlmListTable> AlmListTables = new List<AlmListTable>();
        public bool ErrorInitial;
        MainControlPanelViewModel value;

        private readonly DispatcherTimer _alramRefresh = new DispatcherTimer();
        public AlarmPageViewModel(IWindowManager iwindow, MainWindowViewModel mwm)
        {
            AlamList = new ObservableCollection<NewAlarm>();
            _alramRefresh.Interval = TimeSpan.FromMilliseconds(500);
            _alramRefresh.Tick += Refresh!;


            this.WindowManager = iwindow;
            this.Mwvm = mwm;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now + new TimeSpan(1, 0, 0);
            Time = DateTime.Now;



            ErrorInitial = ReadAlarmConfig();
            if (!ErrorInitial)
                this.Mwvm.Log?.ErrorFormat("报警配置读取失败!");
            else
                _alramRefresh.Start();

            var mainpage = (MainPageViewModel)Mwvm.MainPageContent!;
            value = (MainControlPanelViewModel)mainpage.MainControlPanelViewContent!;
            ClearAlarm = value.ClearAlarm;

        }

        public void Refresh(object sender, EventArgs e)
        {
            ClearAlarm = value.ClearAlarm;
            if (!ErrorInitial) return;
            if (Mwvm?.Engine!.DataBase == null) return;
            for (var i = 0; i < Mwvm.Controller?.Alarms.AlarmList.Length; i++)
            {
                if (!Mwvm.Controller.Alarms.AlarmList[i]) continue;
                if (RecordIds.Contains(i)) continue;
                if (!AlarmRecord.ContainsKey(i)) continue;
                var alarm = new NewAlarm
                {
                    Value = AlarmRecord[i],
                    Key = i,
                    ErrorTime = DateTime.Now.ToString("yyyy/MM/dd h:mm:ss.fff")                 
                };
                if (AlamList.Count == 0)
                {
                    AlamList.Add(alarm);
                    RecordIds.Add(i);
                    //写入数据库
                    Mwvm.Idatabase!.Create(new AlmListTable
                    {
                        Message = alarm.Value,
                        DateTime = DateTime.Parse(alarm.ErrorTime),
                        number = alarm.Key
                    });
                    this.Mwvm.Log?.Info(alarm.Value);
                }
                else
                {
                    if (!RecordIds.Contains(i))
                    {
                        RecordIds.Add(i);
                        AlamList.Add(alarm);
                        //写入数据库
                        Mwvm.Idatabase!.Create(new AlmListTable
                        {
                            Message = alarm.Value,
                            DateTime = DateTime.Parse(alarm.ErrorTime),
                            number = alarm.Key
                        });
                        this.Mwvm.Log?.Info(alarm.Value);
                    }
                }
                if (AlamList.Count > 100)
                    AlamList.RemoveAt(99);
            }

            if (!ClearAlarm) return;
            Thread.Sleep(1000); //收到下发给下位机的错误清除后，等待复位
            AlamList.Clear();
            RecordIds.Clear();
            value.ClearAlarm = false;
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

        private List<AlarmMessage> _alarmmessagelist = null!;
        public List<AlarmMessage> AlarmMessageList
        {
            get => _alarmmessagelist;
            set { _alarmmessagelist = value; NotifyOfPropertyChange(() => AlarmMessageList); }
        }


        private ObservableCollection<NewAlarm> _alamlist = new ObservableCollection<NewAlarm>();
        public ObservableCollection<NewAlarm> AlamList
        {
            get => _alamlist;
            set
            {
                _alamlist = value;
                NotifyOfPropertyChange(() => AlamList);
            }
        }

        private ObservableCollection<NewAlarm> alamchecklist = new ObservableCollection<NewAlarm>();
        public ObservableCollection<NewAlarm> AlamCheckList
        {
            get => alamchecklist;
            set
            {
                alamchecklist = value;
                NotifyOfPropertyChange(() => AlamCheckList);
            }
        }



        private DateTime _startdate;
        public DateTime StartDate
        {
            get => _startdate;
            set { _startdate = value; NotifyOfPropertyChange(() => StartDate); }
        }

        private DateTime _enddate;
        public DateTime EndDate
        {
            get => _enddate;
            set { _enddate = value; NotifyOfPropertyChange(() => EndDate); }
        }

        private DateTime _time;
        public DateTime Time
        {
            get => _time;
            set { _time = value; NotifyOfPropertyChange(() => Time); }
        }

        private string? _validatingTime;
        public string? ValidatingTime
        {
            get => _validatingTime;
            set { _validatingTime = value; NotifyOfPropertyChange(() => ValidatingTime); }
        }

        private DateTime? _futureValidatingDate;
        public DateTime? FutureValidatingDate
        {
            get => _futureValidatingDate;
            set { _futureValidatingDate = value; NotifyOfPropertyChange(() => FutureValidatingDate); }
        }

        private bool _clearAlarm;
        public bool ClearAlarm
        {
            get => _clearAlarm;
            set
            {
                _clearAlarm = value;
                NotifyOfPropertyChange(() => ClearAlarm);
            }
        }

        #endregion

        public bool ReadAlarmConfig()
        {
            try
            {
                var readPath = Directory.GetCurrentDirectory() + "\\AlarmMessage.ini";
                string[] lines = File.ReadAllLines(readPath, Encoding.Default);
                foreach (var item in lines)
                {
                    //分割数据
                    var str = item.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    if (str.Length < 2)  //如果字符串长度小于2，直接跳过，执行下一循环
                    {
                        continue;
                    }

                    int id = int.Parse(str[0]);
                    string context = str[1];
                    AlarmRecord.TryAdd(id, context);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Check()
        {
            var date = EndDate - StartDate;
            if (date.Days < 0)
            {
                MessageBox.Show("起始日期不能大于终止日期!");
                this.Mwvm.Log?.Info("报警页面-起始日期不能大于终止日期!");
                return;
            }
            if (date.TotalDays > 30)
            {
                MessageBox.Show("只能查询30天的数据");
                this.Mwvm.Log?.Info("报警页面-只能查询30天的数据");
                return;
            }
            var where = LinqHelper.True<AlmListTable>();
            where = where.And(x => x.DateTime > StartDate && x.DateTime < EndDate);
            var dd = this.Mwvm.Idatabase!.InvokeList("dd", where);
            Array ddda = dd.Data.ToArray();
            if (ddda.Length <= 0) return;
            AlamList.Clear();
            RecordIds.Clear();
            AlmListTables = ddda.CastToList<AlmListTable>();
            foreach (var alarm in AlmListTables.Select(alm => new NewAlarm
            {
                Key = alm.number,
                Value = alm.Message,
                ErrorTime = alm.DateTime.ToString()
            }))
            {
                AlamCheckList.Add(alarm);
            }
        }

        public void Export()
        {
            if (AlamCheckList.Count == 0)
            {
                MessageBox.Show("导出没有相应的数据!");
                this.Mwvm.Log?.Info("报警页面-导出没有相应的数据!");
                return;
            }
            var readPath = Directory.GetCurrentDirectory() + "\\Alarm.txt";
            var text = AlamCheckList.Aggregate("", (current, alarm) => current + (alarm.ErrorTime + ":    " + "AlarmCode" + alarm.Key + ":    " + alarm.Value + "\r\n"));
            var sd = new StreamReader(readPath);
            string reader = sd.ReadToEnd();
            if (reader == "")
            {
                sd.Dispose();
                var fs = new FileStream(readPath, FileMode.Append, FileAccess.Write);
                var bytes = new UTF8Encoding(true).GetBytes(text);
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
                fs.Dispose();
                MessageBox.Show($"导出文本成功！输出地址:{readPath}");
                this.Mwvm.Log?.Info($"报警页面-导出文本成功！输出地址:{readPath}");
            }
            else
            {
                var results = MessageBox.Show("文本中已经有内容，是否覆盖？", "提示信息", MessageBoxButton.YesNo);
                if (results != MessageBoxResult.Yes) return;
                sd.Dispose();
                File.WriteAllText(readPath, string.Empty);
                var fs = new FileStream(readPath, FileMode.Append, FileAccess.Write);
                var bytes = new UTF8Encoding(true).GetBytes(text);
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
                fs.Dispose();
                MessageBox.Show($"导出文本成功！输出地址:{readPath}");
                this.Mwvm.Log?.Info($"报警页面-导出文本成功！输出地址:{readPath}");
            }

        }
    }
}
