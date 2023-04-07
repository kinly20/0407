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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dockPanel1.DockLeftPortion = 0.5;
            dockPanel1.DockRightPortion = 0.2;
            dockPanel1.DockTopPortion = 0.2;
            dockPanel1.DockBottomPortion = 0.2;

            Form2 frm = new Form2();
            frm.changgefather_event += new Form2.changgefather(FatherDatachangge);
            frm.Show(dockPanel1, DockState.Document);
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Text = "新的窗口";
            frm.changgefather_event += new Form2.changgefather(FatherDatachangge);
            frm.Show(dockPanel1,DockState.Document);
        }


        public void FatherDatachangge(string text)
        {
            textBox2.Text = text;
        }


        public delegate void Sonchangedata(string data);

        public event Sonchangedata Sonchanggedata_event;
        private void button2_Click(object sender, EventArgs e)
        {
            Sonchanggedata_event(textBox1.Text);
        }
    }
}
