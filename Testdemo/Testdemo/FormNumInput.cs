using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testdemo
{
    public partial class FormNumInput : Form
    {
        public string data = "0";
        public FormNumInput()
        {
            InitializeComponent();
            textBox1.Text = data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkvalue();
            data += "1";
            textBox1.Text = data;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkvalue();
            data += "2";
            textBox1.Text = data;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            checkvalue();
            data += "3";
            textBox1.Text = data;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            checkvalue();
            data += "4";
            textBox1.Text = data;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            checkvalue();
            data += "5";
            textBox1.Text = data;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            checkvalue();
            data += "6";
            textBox1.Text = data;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            checkvalue();
            data += "7";
            textBox1.Text = data;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            checkvalue();
            data += "8";
            textBox1.Text = data;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            checkvalue();
            data += "9";
            textBox1.Text = data;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            checkvalue();
            data += "0";
            textBox1.Text = data;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (data.IndexOf(".") <= 0)
            {
                data += ".";
                textBox1.Text = data;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (data.IndexOf("-") <= 0)
            {
                data = "-" + data;
                textBox1.Text = data;
            }
            else
            {
                data = data.Substring(1, data.Length - 1);
                textBox1.Text = data;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (data.Length == 1)
            {
                data = "0";
                textBox1.Text = data;
            }
            else if (data.Length > 0)
            {
                data = data.Substring(0, data.Length - 1);
                textBox1.Text = data;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            data = "0";
            textBox1.Text = data;
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void checkvalue()
        {
            if (data == "0")
            {
                data = "";
            }
        }
    }
}
