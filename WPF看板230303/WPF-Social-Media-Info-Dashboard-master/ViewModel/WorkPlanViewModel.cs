using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.ComponentModel;
using System.Data;
using Dashboard;
using System.Collections.ObjectModel;

namespace Dashboard.ViewModel
{
    class WorkPlanViewModel : CNotifyPropertyChange
    {
        data data1 = new data();
        public WorkPlanViewModel()
        {
            loaddata();
        }

        public void loaddata()
        {
            loadworkdata();


            loadproductdata();

            loadmeterdata();

            loadtimenamedata();





        }

        public void loadworkdata()
        {
            DataTable dtworkplannum = data1.SearchWorkPlanNum();//工作计划表

            WorkModel work = new WorkModel(dtworkplannum.Rows[0][1].ToString(),
               dtworkplannum.Rows[0][2].ToString(), dtworkplannum.Rows[0][3].ToString(),
               dtworkplannum.Rows[0][4].ToString(), dtworkplannum.Rows[0][5].ToString());
            Cworkmodel = work;
        }

        public void loadproductdata()
        {
            DataTable dtworkproductplan = data1.SearchWorkProductPlan();//产品计划表
            ObservableCollection<TypeNumModel> subs = new ObservableCollection<TypeNumModel>();
            for (int i = 0; i < dtworkproductplan.Rows.Count; i++)
            {
                TypeNumModel sub = new TypeNumModel(dtworkproductplan.Rows[i][0].ToString(), (i + 1).ToString(), dtworkproductplan.Rows[i][1].ToString(), int.Parse(dtworkproductplan.Rows[i][2].ToString()));
                subs.Add(sub);
            }
            _showTypeNumModels = new ObservableCollection<TypeNumModel>();
            ShowTypeNumModels = subs;
            if (subs.Count > 0)
                SelectTypeNum = subs[0];
        }

        public void loadmeterdata()
        {
            DataTable dtproducttimeinfo = SQLiteeDB.Getproducttime();//节拍表
            ObservableCollection<MeterModel> meters = new ObservableCollection<MeterModel>();
            for (int i = 0; i < dtproducttimeinfo.Rows.Count; i++)
            {
                MeterModel sub = new MeterModel(dtproducttimeinfo.Rows[i]["id"].ToString(), dtproducttimeinfo.Rows[i]["type"].ToString(), dtproducttimeinfo.Rows[i]["meter"].ToString());
                meters.Add(sub);
            }
            ShowMeterModels = meters;
            Selectmeter = meters[0];
        }

        public void loadtimenamedata()
        {
            ObservableCollection<string> tablenames = new ObservableCollection<string>();
            DataTable dttablename = SQLiteeDB.Gettablename();//时间安排分段表 表名
            for (int i = 0; i < dttablename.Rows.Count; i++)
            {
                string tablename = dttablename.Rows[i][0].ToString();
                if (tablename == "sqlite_sequence" || tablename == "product")
                    continue;
                tablenames.Add(tablename);
            }
            Tablenames = tablenames;
            Selecttimeplanname = tablenames.Count > 0 ? tablenames[0] : "";
        }

        public void changgetable()
        {
            ShowTimePlanModel = new ObservableCollection<TimePlanModel2>();
            DataTable dttimeplan = SQLiteeDB.Getdatabyname(Selecttimeplanname);//时间安排分段表
            for (int i = 0; i < dttimeplan.Rows.Count; i++)
            {
                string id = dttimeplan.Rows[i][0].ToString();
                string times = dttimeplan.Rows[i][1].ToString().Replace(" ","").Replace("：",":");
                string timee = dttimeplan.Rows[i][2].ToString().Replace(" ", "").Replace("：", ":");
                TimePlanModel2 ppm = new TimePlanModel2(id, (i + 1).ToString(), times, timee); ;
                ShowTimePlanModel.Add(ppm);
            }
        }


        private RelayCommand _productCommand;
        public RelayCommand ProductCommand
        {
            get
            {
                if (_productCommand == null)
                    _productCommand = new RelayCommand(
                       new Action<object>(
                           o => productCommand()));
                return _productCommand;
            }
        }


        public void productCommand()
        {
            ProductWindow SUBView = new ProductWindow(SelectTypeNum);

            var dialog = SUBView.ShowDialog();


            loadproductdata();

        }

        private RelayCommand _addproductCommand;
        public RelayCommand AddProductCommand
        {
            get
            {
                if (_addproductCommand == null)
                    _addproductCommand = new RelayCommand(
                       new Action<object>(
                           o => addproductCommand()));
                return _addproductCommand;
            }
        }


        public void addproductCommand()
        {
            int num = ShowTypeNumModels.Count;
            TypeNumModel tnm = new TypeNumModel("", (num + 1).ToString(), "", 0);
            ProductWindow SUBView = new ProductWindow(tnm);

            var dialog = SUBView.ShowDialog();

            loadproductdata();
        }

        private RelayCommand _metercommand;
        public RelayCommand Metercommand
        {
            get
            {
                if (_metercommand == null)
                    _metercommand = new RelayCommand(
                       new Action<object>(
                           o => metercommand()));
                return _metercommand;
            }
        }

        public void metercommand()
        {
            MeterWindow SUBView = new MeterWindow(Selectmeter);

            var dialog = SUBView.ShowDialog();


            loadmeterdata();
        }

        private RelayCommand _addmetercommand;
        public RelayCommand AddmeterCommand
        {
            get
            {
                if (_addmetercommand == null)
                    _addmetercommand = new RelayCommand(
                       new Action<object>(
                           o => addmetercommand()));
                return _addmetercommand;
            }
        }

        public void addmetercommand()
        {
            MeterWindow SUBView = new MeterWindow(null);

            var dialog = SUBView.ShowDialog();


            loadmeterdata();
        }

        private RelayCommand _timecommand;
        public RelayCommand TimeCommand
        {
            get
            {
                if (_timecommand == null)
                    _timecommand = new RelayCommand(
                       new Action<object>(
                           o => timecommand()));
                return _timecommand;
            }
        }

        public void timecommand()
        {
            TimePlanWindow SUBView = new TimePlanWindow(SelectTimePlanModel,Selecttimeplanname);

            var dialog = SUBView.ShowDialog();


            //loadtimenamedata();
            changgetable();
        }

        private RelayCommand _addtimecommand;
        public RelayCommand AddTimeCommand
        {
            get
            {
                if (_addtimecommand == null)
                    _addtimecommand = new RelayCommand(
                       new Action<object>(
                           o => addtimecommand()));
                return _addtimecommand;
            }
        }

        public void addtimecommand()
        {
            TimePlanModel2 tpm2 = new TimePlanModel2("", (ShowTimePlanModel.Count + 1).ToString(), "", "");
            TimePlanWindow SUBView = new TimePlanWindow(tpm2, Selecttimeplanname);

            var dialog = SUBView.ShowDialog();


            //loadtimenamedata();
            changgetable();
        }

        private RelayCommand _savecommand;
        public RelayCommand SaveCommand
        {
            get
            {
                if (_savecommand == null)
                    _savecommand = new RelayCommand(
                       new Action<object>(
                           o => savecommand()));
                return _savecommand;
            }
        }

        public void savecommand()
        {
            data1.EditWorkPlanNum(Cworkmodel.Inname,int.Parse( Cworkmodel.Innum), Cworkmodel.Outname, int.Parse(Cworkmodel.Outnum), Cworkmodel.Selecttimeplan);

            loaddata();

        }

        private RelayCommand _addTableCommand;
        public RelayCommand AddTableCommand
        {
            get
            {
                if (_addTableCommand == null)
                    _addTableCommand = new RelayCommand(
                       new Action<object>(
                           o => addTableCommand()));
                return _addTableCommand;
            }
        }

        public void addTableCommand()
        {
            NewTableWindow dia = new NewTableWindow();
            dia.ShowDialog();

            loadtimenamedata();
            changgetable();
        }

        

        private WorkModel _cworkmodel;
        public WorkModel Cworkmodel
        {
            get
            {
                return _cworkmodel;
            }
            set
            {
                _cworkmodel = value;
                this.NotifyPropertyChange("Cworkmodel");
            }
        }

        private TypeNumModel _selectTypeNum;
        public TypeNumModel SelectTypeNum
        {
            get
            {
                return _selectTypeNum;
            }
            set
            {
                _selectTypeNum = value;
                this.NotifyPropertyChange("SelectTypeNum");
            }
        }

        private ObservableCollection<TypeNumModel> _showTypeNumModels;

        public ObservableCollection<TypeNumModel> ShowTypeNumModels
        {
            get { return _showTypeNumModels; }
            set
            {
                _showTypeNumModels = value;
                this.NotifyPropertyChange("ShowTypeNumModels");
            }
        }


        private MeterModel _selectmeter;
        public MeterModel Selectmeter
        {
            get
            {
                return _selectmeter;
            }
            set
            {
                _selectmeter = value;
                this.NotifyPropertyChange("Selectmeter");
            }
        }

        private ObservableCollection<MeterModel> _showMeterModels;

        public ObservableCollection<MeterModel> ShowMeterModels
        {
            get { return _showMeterModels; }
            set
            {
                _showMeterModels = value;
                this.NotifyPropertyChange("ShowMeterModels");
            }
        }


        private ObservableCollection<string> _tablenames;

        public ObservableCollection<string> Tablenames
        {
            get { return _tablenames; }
            set
            {
                _tablenames = value;
                this.NotifyPropertyChange("Tablenames");
            }
        }

        private string _selecttimeplanname;

        public string Selecttimeplanname
        {
            get { return _selecttimeplanname; }
            set
            {
                _selecttimeplanname = value;
                changgetable();
                this.NotifyPropertyChange("Selecttimeplanname");
            }
        }



        private ObservableCollection<TimePlanModel2> _showTimePlanModel;

        public ObservableCollection<TimePlanModel2> ShowTimePlanModel
        {
            get { return _showTimePlanModel; }
            set
            {
                _showTimePlanModel = value;
                this.NotifyPropertyChange("ShowTimePlanModel");
            }
        }

        private TimePlanModel2 _selectTimePlanModel;

        public TimePlanModel2 SelectTimePlanModel
        {
            get { return _selectTimePlanModel; }
            set
            {
                _selectTimePlanModel = value;
                this.NotifyPropertyChange("SelectTimePlanModel");
            }
        }
    }

    public class WorkModel : CNotifyPropertyChange
    {
        public WorkModel(string inname, string innum, string outname, string outnum,
            string selecttimeplan)
        {
            _inname = inname;
            _innum = innum;
            _outname = outname;
            _outnum = outnum;
            _selecttimeplan = selecttimeplan;
        }

        private string _inname;
        public string Inname
        {
            get { return _inname; }
            set
            {
                _inname = value;
                this.NotifyPropertyChange("Inname");
            }
        }

        private string _innum;
        public string Innum
        {
            get { return _innum; }
            set
            {
                _innum = value;
                this.NotifyPropertyChange("Innum");
            }
        }

        private string _outname;
        public string Outname
        {
            get { return _outname; }
            set
            {
                _outname = value;
                this.NotifyPropertyChange("Outname");
            }
        }

        private string _outnum;
        public string Outnum
        {
            get { return _outnum; }
            set
            {
                _outnum = value;
                this.NotifyPropertyChange("Outnum");
            }
        }

        private string _selecttimeplan;
        public string Selecttimeplan
        {
            get { return _selecttimeplan; }
            set
            {
                _selecttimeplan = value;
                this.NotifyPropertyChange("Selecttimeplan");
            }
        }
    }

    public class TypeNumModel : CNotifyPropertyChange
    {
        public TypeNumModel(string id, string sort, string name, int num)
        {
            _id = id;
            _sort = sort;
            _name = name;
            _num = num;
        }

        private string _id;
        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                this.NotifyPropertyChange("ID");
            }
        }

        private string _sort;
        public string Sort
        {
            get { return _sort; }
            set
            {
                _sort = value;
                this.NotifyPropertyChange("Sort");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                this.NotifyPropertyChange("Name");
            }
        }

        private int _num;
        public int Num
        {
            get { return _num; }
            set
            {
                _num = value;
                this.NotifyPropertyChange("Num");
            }
        }
    }


    public class MeterModel : CNotifyPropertyChange
    {
        public MeterModel(string id, string name, string meter)
        {
            _id = id;
            _name = name;
            _meter = meter;
        }

        private string _id;
        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                this.NotifyPropertyChange("ID");
            }
        }



        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                this.NotifyPropertyChange("Name");
            }
        }

        private string _meter;
        public string Meter
        {
            get { return _meter; }
            set
            {
                _meter = value;
                this.NotifyPropertyChange("Meter");
            }
        }
    }


    public class TimePlanModel2 : CNotifyPropertyChange
    {
        public TimePlanModel2(string id, string sort, string timestart, string timeend)
        {
            _id = id;
            _sort = sort;
            _timestart = timestart;
            _timeend = timeend;
            _timeshow = "【" + timestart + "~" + timeend + "】";
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                this.NotifyPropertyChange("Id");
            }
        }

        private string _sort;
        public string Sort
        {
            get { return _sort; }
            set
            {
                _sort = value;
                this.NotifyPropertyChange("Sort");
            }
        }

        private string _timestart;
        public string Timestart
        {
            get { return _timestart; }
            set
            {
                _timestart = value;
                this.NotifyPropertyChange("Timestart");
            }
        }

        private string _timeend;
        public string Timeend
        {
            get { return _timeend; }
            set
            {
                _timeend = value;
                this.NotifyPropertyChange("Timeend");
            }
        }

        private string _timeshow;
        public string Timeshow
        {
            get { return _timeshow; }
            set
            {
                _timeshow = value;
                this.NotifyPropertyChange("Timeshow");
            }
        }


    }
}
