using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICD.Class;
using System.Threading;

namespace ICD.UserControls
{
    public partial class PointDebug : UserControl
    {
        System.Threading.Thread thread;
        System.Windows.Forms.Timer time;
        public PointDebug()
        {
            InitializeComponent();
        }


        #region plc link code
        HslCommunicationClass communicationClass;
        private void button5_Click(object sender, EventArgs e)
        {
            string ip = textBox1.Text;
            communicationClass = new HslCommunicationClass(ip);
            communicationClass.Connect();
            if (!communicationClass.isconnect)
            {
                changebutton(false);
                MessageBox.Show("connect fail");
            }
            else
            {
                changebutton(true);
            }

        }

        public void changebutton(bool value)
        {
            button6.Enabled = value;
            button7.Enabled = value;
            button8.Enabled = value;
            button9.Enabled = value;
            button10.Enabled = value;
        }


        private void button6_Click(object sender, EventArgs e)
        {
            communicationClass = new HslCommunicationClass("");
            if (communicationClass.isconnect)
                communicationClass.DisConnect();
            button10_Click(null, null);
            changebutton(false);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string addr = textBox2.Text;
            string text = textBox3.Text;
            string type = comboBox1.Text;
            string msg = string.Empty;
            if (type == "string" || type == "")
                communicationClass.WriteValue(addr, text, out msg);
            else if (type == "int")
                communicationClass.WriteValue(addr, int.Parse(text), out msg);
            else if (type == "float")
                communicationClass.WriteValue(addr, float.Parse(text), out msg);
            else if (type == "double")
                communicationClass.WriteValue(addr, double.Parse(text), out msg);
            else if (type == "short")
                communicationClass.WriteValue(addr, Convert.ToInt16(text), out msg);
            else if (type == "byte")
                communicationClass.WriteValue(addr, Convert.ToByte(text), out msg);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string addr = textBox2.Text;

            string msg = string.Empty;



            string type = comboBox2.Text;

            if (type == "string" || type == "")
            {
                string back = "";
                communicationClass.ReadValueString(addr, out back, out msg);
                textBox4.Text = back.ToString();
            }
            else if (type == "int")
            {
                int back = 0;
                communicationClass.ReadValue(addr, out back, out msg);
                textBox4.Text = back.ToString();
            }
            else if (type == "float")
            {
                float back = 0;
                communicationClass.ReadValue(addr, out back, out msg);
                textBox4.Text = back.ToString();
            }
            else if (type == "double")
            {
                double back = 0;
                communicationClass.ReadValue(addr, out back, out msg);
                textBox4.Text = back.ToString();
            }
            else if (type == "short")
            {
                short back = 0;
                communicationClass.ReadValue(addr, out back, out msg);
                textBox4.Text = back.ToString();
            }
            else if (type == "byte")
            {
                byte back = 0;
                communicationClass.ReadValue(addr, out back, out msg);
                textBox4.Text = back.ToString();
            }

            //communicationClass.ReadValuetest(addr, out backstring, out msg);


            //short[] backvalues = new short[clength];
            //short result = communicationClass.readMulitDatas(int.Parse(addr), clength, ref backvalues);

            //textBox4.Text = backvalues.Select(t => t.ToString("X2")).ToString();
        }

        private delegate void FlushClient();
        private void button9_Click(object sender, EventArgs e)
        {
            time = new System.Windows.Forms.Timer();
            time.Interval = 5000;
            time.Tick += delegate
            {

                //Task mm = new Task(() =>
                //{
                //    button8_Click(null, null);
                //});
                //mm.Start();

                ThreadStart obj = new System.Threading.ThreadStart(startlisten);
                thread = new Thread(obj);
                thread.Start();
            };
            time.Start();
        }

        public void startlisten()
        {
            FlushClient fc = new FlushClient(substartlisten);
            this.Invoke(fc);
        }

        public void substartlisten()
        {
            button8_Click(null, null);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (time.Enabled)
                time.Stop();
        }

        #endregion
    }
}
