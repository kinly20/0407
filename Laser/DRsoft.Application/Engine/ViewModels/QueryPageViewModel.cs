using Caliburn.Micro;
using Engine.Models;
using System.Collections.ObjectModel;
using DRsoft.Engine.Model.DataBaseTable;
using DRsoft.Runtime.Core.DataBase.Common;
using DRsoft.Runtime.Core.DataBase.Common.Extentions;
using DRsoft.Runtime.Core.Platform.Logging;

namespace Engine.ViewModels
{
    public class QueryPageViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainWindowViewModel Mwvm;
        //记录报警的ID
        public List<DirtyTable> DirtyTables = new List<DirtyTable>();
        public List<ProductDefectTable> ProductDefectTables = new List<ProductDefectTable>();
        public bool ErrorInitial = false;

        public QueryPageViewModel(IWindowManager iwindow, MainWindowViewModel mwm)
        {
            this.WindowManager = iwindow;
            this.Mwvm = mwm;
            StartDate = DateTime.Now;
            DefectStartDate = DateTime.Now;
            EndDate = DateTime.Now + new TimeSpan(1, 0, 0);
            DefectEndDate = DateTime.Now + new TimeSpan(1, 0, 0);
            Time = DateTime.Now;
            DefectTime = DateTime.Now;
            IdNumber = 0;
            DefectIdNumber = 0;
            DirtyTableList = new ObservableCollection<DirtyTableItem>();
            ProductDefectTableList = new ObservableCollection<ProductDefectTableItem>();
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


        private ObservableCollection<DirtyTableItem> _dirtyTableList = null!;
        public ObservableCollection<DirtyTableItem> DirtyTableList
        {
            get => _dirtyTableList;
            set
            {
                _dirtyTableList = value;
                NotifyOfPropertyChange(() => DirtyTableList);
            }
        }

        private ObservableCollection<ProductDefectTableItem> _productDefectTableList = null!;
        public ObservableCollection<ProductDefectTableItem> ProductDefectTableList
        {
            get => _productDefectTableList;
            set
            {
                _productDefectTableList = value;
                NotifyOfPropertyChange(() => ProductDefectTableList);
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

        private DateTime _defectstartdate;
        public DateTime DefectStartDate
        {
            get => _defectstartdate;
            set { _defectstartdate = value; NotifyOfPropertyChange(() => DefectStartDate); }
        }

        private DateTime _defectenddate;
        public DateTime DefectEndDate
        {
            get => _defectenddate;
            set { _defectenddate = value; NotifyOfPropertyChange(() => DefectEndDate); }
        }

        private DateTime _defecttime;
        public DateTime DefectTime
        {
            get => _defecttime;
            set { _defecttime = value; NotifyOfPropertyChange(() => DefectTime); }
        }

        private string? _defectvalidatingTime;
        public string? DefectValidatingTime
        {
            get => _defectvalidatingTime;
            set { _defectvalidatingTime = value; NotifyOfPropertyChange(() => DefectValidatingTime); }
        }

        private DateTime? _futureValidatingDate;
        public DateTime? FutureValidatingDate
        {
            get => _futureValidatingDate;
            set { _futureValidatingDate = value; NotifyOfPropertyChange(() => FutureValidatingDate); }
        }

        private int _idNumber;
        public int IdNumber
        {
            get => _idNumber;
            set
            {
                _idNumber = value;
                NotifyOfPropertyChange(() => IdNumber);
            }
        }

        private int _defectIdNumber;
        public int DefectIdNumber
        {
            get => _defectIdNumber;
            set
            {
                _defectIdNumber = value;
                NotifyOfPropertyChange(() => DefectIdNumber);
            }
        }
        #endregion


        #region 脏污查询

        public void DataTimeCheck()
        {
            DirtyTableList.Clear();
            var date = EndDate - StartDate;
            if (date.TotalDays > 30)
            {
                MessageBox.Show("只能查询近30天的数据");
                this.Mwvm.Log?.Info("脏污查询-只能查询近30天的数据");
                return;
            }
            try
            {
                var where = LinqHelper.True<DirtyTable>();
                where = where.And(x => x.Time > StartDate && x.Time < EndDate);
                var idcheckResult = this.Mwvm.Idatabase!.InvokeList("TimeCheck", where);
                Array arrayIdCheckResult = idcheckResult.Data.ToArray();
                if (arrayIdCheckResult.Length > 0)
                {
                    DirtyTableList.Clear();
                    foreach (DirtyTable? item in arrayIdCheckResult)
                    {
                        var tableItem = new DirtyTableItem
                        {
                            Id = item!.id,
                            GroupId = item.GroupId,
                            SilicaId = item.SilicaId,
                            MachineId = item.MachineId,
                            WorkStationId = item.WorkStationId,
                            LaserId = item.LaserId,
                            IsDirty = item.IsDirty,
                            DirtyX = item.DirtyX,
                            DirtyY = item.DirtyY,
                            DirtyWidth = item.DirtyWidth,
                            DirtyHeight = item.DirtyHeight,
                            PadX = item.PadX,
                            PadY = item.PadY,
                            Time = item.Time
                        };
                        DirtyTableList.Add(tableItem);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"数据库异常,请尝试恢复数据库文件{e}");
                this.Mwvm.Log?.ErrorFormat($"脏污查询-数据库异常,请尝试恢复数据库文件{e}");
            }
        }

        public void IdCheck()
        {
            DirtyTableList.Clear();
            if (IdNumber <= 0)
            {
                MessageBox.Show("主键非法,无法查询数据");
                this.Mwvm.Log?.ErrorFormat("脏污查询-主键非法,无法查询数据");
                return;
            }
            try
            {
                var where = LinqHelper.True<DirtyTable>();
                where = where.And(x => x.id == IdNumber.ToString());
                var idcheckResult = this.Mwvm.Idatabase!.InvokeList("IdCheck", where);
                Array arrayIdCheckResult = idcheckResult.Data.ToArray();
                if (arrayIdCheckResult.Length <= 0) return;
                DirtyTableList.Clear();
                foreach (DirtyTable? item in arrayIdCheckResult)
                {
                    var tableItem = new DirtyTableItem
                    {
                        Id = item!.id,
                        GroupId = item.GroupId,
                        SilicaId = item.SilicaId,
                        MachineId = item.MachineId,
                        WorkStationId = item.WorkStationId,
                        LaserId = item.LaserId,
                        IsDirty = item.IsDirty,
                        DirtyX = item.DirtyX,
                        DirtyY = item.DirtyY,
                        DirtyWidth = item.DirtyWidth,
                        DirtyHeight = item.DirtyHeight,
                        PadX = item.PadX,
                        PadY = item.PadY,
                        Time = item.Time
                    };
                    DirtyTableList.Add(tableItem);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"数据库异常,请尝试恢复数据库文件{e}");
                this.Mwvm.Log?.ErrorFormat($"脏污查询-数据库异常,请尝试恢复数据库文件{e}");
            }
        }

        public void Export()
        {
            if (DirtyTableList.Count == 0)
            {
                MessageBox.Show("没有相应的数据!");
                this.Mwvm.Log?.Info("脏污查询-没有相应的数据!");
                return;
            }
            var readPath = Directory.GetCurrentDirectory() + "\\DirtyTable.txt";
            if (!File.Exists(readPath))
            {
                File.Create(readPath).Close();
            }
            var text = "";
            foreach (DirtyTableItem dirtytable in DirtyTableList)
            {
                text += "加工时间       "
                        + dirtytable.Time
                        + ",主键ID        "
                        + dirtytable.Id
                        + ",组件ID      "
                        + dirtytable.GroupId
                        + ",硅胶膜ID        "
                        + dirtytable.SilicaId
                        + ",机台ID        "
                        + dirtytable.MachineId
                        + ",工位1~12        "
                        + dirtytable.WorkStationId
                        + ",激光器编号1~12        "
                        + dirtytable.LaserId
                        + ",是否存在脏污        "
                        + dirtytable.IsDirty
                        + ",缺陷矩形中心x坐标值        "
                        + dirtytable.DirtyX
                        + ",缺陷矩形中心y坐标值        "
                        + dirtytable.DirtyY
                        + ",缺陷矩形宽度        "
                        + dirtytable.DirtyWidth
                        + ",缺陷矩形高度        "
                        + dirtytable.DirtyHeight
                        + ",产品焊接X坐标        "
                        + dirtytable.PadX
                        + ",产品焊接Y坐标        "
                        + dirtytable.PadY
                        + "."
                        + "\r\n";
            }
            var sd = new StreamReader(readPath);
            var reader = sd.ReadToEnd();
            if (reader == "")
            {
                sd.Dispose();
                var fs = new FileStream(readPath, FileMode.Append, FileAccess.Write);
                var bytes = new UTF8Encoding(true).GetBytes(text);
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
                fs.Dispose();
                MessageBox.Show($"导出文本成功！输出地址:{readPath}");
                this.Mwvm.Log?.Info($"脏污查询-导出文本成功！输出地址:{readPath}");
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
                this.Mwvm.Log?.Info($"脏污查询-导出文本成功！输出地址:{readPath}");
            }

        }

        #endregion


        #region 产品缺陷查询

        public void DefectDataTimeCheck()
        {
            ProductDefectTableList.Clear();
            var date = DefectEndDate - DefectStartDate;
            if (date.TotalDays > 30)
            {
                MessageBox.Show("只能查询近30天的数据");
                this.Mwvm.Log?.Info("产品缺陷查询-只能查询近30天的数据");
                return;
            }
            try
            {
                var where = LinqHelper.True<ProductDefectTable>();
                where = where.And(x => x.Time > DefectStartDate && x.Time < DefectEndDate);
                var defectcheckResult = this.Mwvm.Idatabase!.InvokeList("DefectCheck", where);
                Array arrayDefectCheckResult = defectcheckResult.Data.ToArray();
                if (arrayDefectCheckResult.Length <= 0) return;
                ProductDefectTableList.Clear();
                foreach (ProductDefectTable? item in arrayDefectCheckResult)
                {
                    var tableItem = new ProductDefectTableItem
                    {
                        Id = item!.id,
                        GroupId = item.GroupId,
                        SilicaId = item.SilicaId,
                        WorkStationId = item.WorkStationId,
                        LaserId = item.LaserId,
                        PadX = item.PadX,
                        PadY = item.PadY,
                        Time = item.Time
                    };
                    ProductDefectTableList.Add(tableItem);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show($"数据库异常,请尝试恢复数据库文件{e}");
                this.Mwvm.Log?.ErrorFormat($"产品缺陷查询-数据库异常,请尝试恢复数据库文件{e}");
            }
        }

        public void DefectIdCheck()
        {
            ProductDefectTableList.Clear();
            if (DefectIdNumber <= 0)
            {
                MessageBox.Show("主键非法,无法查询数据");
                this.Mwvm.Log?.ErrorFormat("产品缺陷查询-主键非法,无法查询数据");
                return;
            }
            try
            {
                var where = LinqHelper.True<ProductDefectTable>();
                where = where.And(x => x.id == DefectIdNumber.ToString());
                var defectIdcheckResult = this.Mwvm.Idatabase!.InvokeList("DefectIdCheck", where);
                Array arrayDefectIdCheckResult = defectIdcheckResult.Data.ToArray();
                if (arrayDefectIdCheckResult.Length <= 0) return;
                ProductDefectTableList.Clear();
                foreach (ProductDefectTable? item in arrayDefectIdCheckResult)
                {
                    var tableItem = new ProductDefectTableItem
                    {
                        Id = item!.id,
                        GroupId = item.GroupId,
                        SilicaId = item.SilicaId,
                        WorkStationId = item.WorkStationId,
                        LaserId = item.LaserId,
                        PadX = item.PadX,
                        PadY = item.PadY,
                        Time = item.Time
                    };
                    ProductDefectTableList.Add(tableItem);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"数据库异常,请尝试恢复数据库文件{e}");
                this.Mwvm.Log?.ErrorFormat($"产品缺陷查询-数据库异常,请尝试恢复数据库文件{e}");
            }
        }

        public void DefectExport()
        {
            if (ProductDefectTableList.Count == 0)
            {
                MessageBox.Show("没有相应的数据!");
                return;
            }
            var readPath = Directory.GetCurrentDirectory() + "\\ProductDefectTable.txt";
            if (!File.Exists(readPath))
            {
                File.Create(readPath).Close();
            }
            var text = ProductDefectTableList.Aggregate("", (current, defecttable) => current + ("加工时间       " + defecttable.Time + ",主键ID        " + defecttable.Id + ",组件ID      " + defecttable.GroupId + ",硅胶膜ID        " + defecttable.SilicaId + ",机台ID        " + defecttable.WorkStationId + ",激光器编号1~12        " + defecttable.LaserId + ",产品焊接X坐标        " + defecttable.PadX + ",产品焊接Y坐标        " + defecttable.PadY + "." + "\r\n"));
            var sd = new StreamReader(readPath);
            var reader = sd.ReadToEnd();
            if (reader == "")
            {
                sd.Dispose();
                var fs = new FileStream(readPath, FileMode.Append, FileAccess.Write);
                var bytes = new UTF8Encoding(true).GetBytes(text);
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
                fs.Dispose();
                MessageBox.Show($"导出文本成功！输出地址:{readPath}");
                this.Mwvm.Log?.Info($"产品缺陷查询-导出文本成功！输出地址:{readPath}");
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
                this.Mwvm.Log?.Info($"产品缺陷查询-导出文本成功！输出地址:{readPath}");
            }
        }

        #endregion
    }
}
