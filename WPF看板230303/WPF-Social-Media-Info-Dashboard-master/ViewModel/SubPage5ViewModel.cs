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
    class SubPage5ViewModel : CNotifyPropertyChange
    {
        data data1 = new data();
        public SubPage5ViewModel()
        {
            loaddata();
        }

        public void loaddata()
        {

            DataTable dt = data1.SearchAllUser();
            ObservableCollection<newdemosubModel> subs = new ObservableCollection<newdemosubModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                newdemosubModel sub = new newdemosubModel(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), "");
                subs.Add(sub);
            }
            _showsubModels = new ObservableCollection<newdemosubModel>();
            ShowsubModels = subs;
            if (subs.Count > 0)
                _selectsubModel = subs[0];
        }


        private RelayCommand _deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(
                       new Action<object>(
                           o => delete()));
                return _deleteCommand;
            }
        }

        public void delete()
        {
            if (!string.IsNullOrEmpty(_selectsubModel.User))
                data1.DeleteUser(_selectsubModel.User);
            loaddata();
            //_showsubModels.Remove(_selectsubModel);
            //_selectsubModel = _showsubModels[0];
        }

        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                    _addCommand = new RelayCommand(
                       new Action<object>(
                           o => add()));
                return _addCommand;
            }
        }

        public void add()
        {
            //newdemosubModel user = new newdemosubModel("", "", "", "");
            NewUserWindow studentView = new NewUserWindow();

            var dialog = studentView.ShowDialog();

            //if (dialog == true)
            //{

                loaddata();

            //}
        }
     

        private newdemosubModel _selectsubModel;

        public newdemosubModel SelectsubModel
        {
            get
            {
                return _selectsubModel;
            }
            set
            {
                _selectsubModel = value;
                this.NotifyPropertyChange("SelectsubModel");
            }
        }

        private ObservableCollection<newdemosubModel> _showsubModels;

        public ObservableCollection<newdemosubModel> ShowsubModels
        {
            get { return _showsubModels; }
            set
            {
                _showsubModels = value;
                this.NotifyPropertyChange("ShowsubModels");
            }
        }

    }

    public class newdemosubModel : CNotifyPropertyChange
    {
        public newdemosubModel(string id, string user, string password, string operate)
        {
            _id = id;
            _user = user;
            _password = password;
            _operate = operate;
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

        private string _user;
        public string User
        {
            get { return _user; }
            set
            {
                _user = value;
                this.NotifyPropertyChange("User");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                this.NotifyPropertyChange("Password");
            }
        }

        private string _operate;
        public string Operate
        {
            get { return _operate; }
            set
            {
                _operate = value;
                this.NotifyPropertyChange("Operate");
            }
        }
    }




}


