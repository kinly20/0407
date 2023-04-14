using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WpfSunnyUILibrary.Class
{
    public class ModelClass
    {
    }

    public class checkboxvalue
    {
        public checkboxvalue(string _name, bool _ischeck)
        {
            name = _name;
            ischeck = _ischeck;
        }
        public string name
        {
            get;
            set;
        }

        public bool ischeck
        {
            get;
            set;
        }
    }
}
