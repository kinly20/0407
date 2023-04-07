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
    public partial class DxfEdit : Form
    {
        public DxfEdit()
        {
            InitializeComponent();
        }
        public static float Rotate;
        public static string ProgName;
        public static bool Table1Enable, Table2Enable,LineMode;
        public bool Yes;
        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butEnter_Click(object sender, EventArgs e)
        {
            if(txtProgName.Text.Trim()=="")
            {
                MessageBox.Show("产品名称不能为空!");
                return;
            }
            string str =Parameter.ProgFilePath + txtProgName + ".csv";
            if(System.IO.File.Exists(str))
            {
                MessageBox.Show("产品名称重复,请更换文件名!");
                return;
            }
            ProgName = txtProgName.Text;
            Rotate = cmbDxfTurn.SelectedIndex * 90;
            if (cmbDxfMirror.SelectedIndex == 1)
            {
               DxfParser.DxfShow.xNegation= true;
               DxfParser.DxfShow.yNegation = false;
            }
            else if (cmbDxfMirror.SelectedIndex == 2)
            {
                DxfParser.DxfShow.xNegation = false;
                DxfParser.DxfShow.yNegation = true;
            }else if(cmbDxfMirror.SelectedIndex == 3)
            {
                DxfParser.DxfShow.xNegation = true;
                DxfParser.DxfShow.yNegation = true;
            }
            else
            {
                DxfParser.DxfShow.xNegation = false;
                DxfParser.DxfShow.yNegation = false;
            }
            if(cmbLineMode.SelectedIndex==1)
            {
                LineMode = true;
            }
            if(ckb_table1.Checked)
            {
                Table1Enable = true;
            }
            else
            {
                Table1Enable = false;
            }
            if (ckb_table2.Checked)
            {
                Table2Enable = true;
            }
            else
            {
                Table2Enable = false;
            }
            Yes = true;
            this.Close();
        }

        private void DxfEdit_Load(object sender, EventArgs e)
        {
            Yes = false;
            cmbDxfMirror.SelectedIndex = 3;           
            cmbDxfTurn.SelectedIndex = 0;
            cmbLineMode.SelectedIndex = 0;

        }

        private void butKeyPanle_Click(object sender, EventArgs e)
        {
            Process[] pProcess = Process.GetProcesses();
            int i;
            for (i = 0; i <= pProcess.Length - 1; i++)
            {
                if (pProcess[i].ProcessName == "osk")
                {
                    pProcess[i].Kill();
                }
            }
            Process pr = Process.Start("osk.exe");
        }
    }
}
