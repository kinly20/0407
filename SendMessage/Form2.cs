using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace SendMessage
{
    public partial class Form2 : DockContent
    {
        public Form2()
        {
            InitializeComponent();
        }
        public delegate void changgefather(string text);
        public event changgefather changgefather_event;
        private void button1_Click(object sender, EventArgs e)
        {
            changgefather_event(textBox1.Text);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Parent.Parent;
            form1.Sonchanggedata_event += new Form1.Sonchangedata(sonchanggedata);
        }

        public void sonchanggedata(string data)
        {
            textBox2.Text = data;
        }
    }
}
