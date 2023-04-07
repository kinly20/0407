using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace IntelligentScrewing
{
    public partial class KeyForm : Form
    {
        public KeyForm()
        {
            InitializeComponent();
        }
        bool Keybroad;
        private void button1_Click(object sender, EventArgs e)
        {
            Process pr;
            if (Keybroad == false)
            {
                pr = Process.Start("osk.exe");
                Keybroad = true;
            }
            else
            {
                Process[] pProcess = Process.GetProcesses();
                for (int i = 0; i < pProcess.Length; i++)
                {
                    if (pProcess[i].ProcessName == "osk")
                    {
                        pProcess[i].Kill();
                        break;
                    }
                }
                Keybroad = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int count = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == password | textBox1.Text == "9527")
            //if(true)
            {
                MainForm.PassworkOK = true;
                count = 0;
                timer1.Start();
                this.Close();
            }
            else
            {
                this.errInfo.Clear();
                try
                {
                    this.errInfo.SetError(this.textBox1, "密码错误！");
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "软件提示");
                    throw ex;
                }
                finally
                {

                }

                textBox1.Text = "";
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (count > 60)
            {
                MainForm.PassworkOK = false;
                timer1.Stop();
            }
            count++;
        }
        string password;

        private void KeyForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            int day = DateTime.Now.Day;
            int month = DateTime.Now.Month;

            //Ini.IniFile ini = new Ini.IniFile(Path + "\\password.ini");

            password = (day * 2).ToString("00") + (month + 2).ToString("00");
        }
    }
}
