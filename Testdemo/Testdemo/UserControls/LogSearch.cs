using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testdemo.Class;

namespace Testdemo.UserControls
{
    public partial class LogSearch : UserControl
    {
        public LogSearch()
        {
            InitializeComponent();
        }

        private void bt_search_Click(object sender, EventArgs e)
        {
            DateTime dateTime = dateTimePicker1.Value;
            listBox1.Items.Clear();
            string[] logs = Log.readlog(dateTime);
            if (logs != null)
            {
                foreach (string log in logs)
                    listBox1.Items.Add(log);
            }
        }
    }
}
