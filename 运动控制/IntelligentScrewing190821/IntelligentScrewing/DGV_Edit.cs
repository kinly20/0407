using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace IntelligentScrewing
{
    public partial class DGV_Edit : Form
    {
        public DGV_Edit()
        {
            InitializeComponent();
        }

        public static int startIndex = 0, changeCount = 1,tableIndex=0;

        private void DGV_Edit_Load(object sender, EventArgs e)
        {
            //this.Width = 414;
            try
            {
                int n = MainForm.selectionindex; 
                txtScrewNum.Text =(n+1).ToString();
                if (tableIndex == 0)
                {
                    txtCrdX.Text = Points.points[n].X.ToString();
                    txtCrdY.Text = Points.points[n].Y.ToString();
                    txtCrdZ.Text = Points.points[n].Z.ToString();
                    cmbTools.Items.AddRange(MainForm.AxisType);
                    cmbTools.SelectedIndex = Points.points[n].Axis;
                    cmbFeeder.Items.AddRange(MainForm.ScrewType);
                    cmbFeeder.SelectedIndex = Points.points[n].ScrewType;
                    cmbWorkMode.Items.AddRange(MainForm.WorkMode);
                    cmbWorkMode.SelectedIndex = Points.points[n].WorkMode;
                }
                if (tableIndex == 1)
                {
                    txtCrdX.Text = Points.points2[n].X.ToString();
                    txtCrdY.Text = Points.points2[n].Y.ToString();
                    txtCrdZ.Text = Points.points2[n].Z.ToString();
                    cmbTools.Items.AddRange(MainForm.AxisType);
                    cmbTools.SelectedIndex = Points.points2[n].Axis;
                    cmbFeeder.Items.AddRange(MainForm.ScrewType);
                    cmbFeeder.SelectedIndex = Points.points2[n].ScrewType;
                    cmbWorkMode.Items.AddRange(MainForm.WorkMode);
                    cmbWorkMode.SelectedIndex = Points.points2[n].WorkMode;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //txtCrdX.Text = mf.DataGrid.CurrentRow.Cells[0].Value.ToString();
            //txtCrdX.Text = mf.DataGrid.CurrentRow.Cells[0].Value.ToString();
            //txtCrdX.Text = mf.DataGrid.CurrentRow.Cells[0].Value.ToString();
        }
        private void UpdataPoint(int n)
        {

            if (tableIndex == 0)
            {
                if (n >= Points.points.Count)
                {
                    MessageBox.Show("点位超出范围!");
                    return;
                }
                txtCrdX.Text = Points.points[n].X.ToString();
                txtCrdY.Text = Points.points[n].Y.ToString();
                txtCrdZ.Text = Points.points[n].Z.ToString();
                cmbTools.Items.AddRange(MainForm.AxisType);
                cmbTools.SelectedIndex = Points.points[n].Axis;
                cmbFeeder.Items.AddRange(MainForm.ScrewType);
                cmbFeeder.SelectedIndex = Points.points[n].ScrewType;
                cmbWorkMode.Items.AddRange(MainForm.WorkMode);
                cmbWorkMode.SelectedIndex = Points.points[n].WorkMode;
            }
            if (tableIndex == 1)
            {
                if (n >= Points.points2.Count)
                {
                    MessageBox.Show("点位超出范围!");
                    return;
                }
                txtCrdX.Text = Points.points2[n].X.ToString();
                txtCrdY.Text = Points.points2[n].Y.ToString();
                txtCrdZ.Text = Points.points2[n].Z.ToString();
                cmbTools.Items.AddRange(MainForm.AxisType);
                cmbTools.SelectedIndex = Points.points2[n].Axis;
                cmbFeeder.Items.AddRange(MainForm.ScrewType);
                cmbFeeder.SelectedIndex = Points.points2[n].ScrewType;
                cmbWorkMode.Items.AddRange(MainForm.WorkMode);
                cmbWorkMode.SelectedIndex = Points.points2[n].WorkMode;
            }
        }
 
        private void butUper_Click(object sender, EventArgs e)
        {
            int index = int.Parse(txtScrewNum.Text);
            if (index == 0)
                return;
            txtScrewNum.Text = (index - 1).ToString();
            UpdataPoint(index - 1);
        }

        private void butDown_Click(object sender, EventArgs e)
        {
            

            int index = int.Parse(txtScrewNum.Text);
            txtScrewNum.Text = (index + 1).ToString();
            UpdataPoint(index);
        }
       
    
        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void butEnter_Click(object sender, EventArgs e)
        {
            Points.Point temp = new Points.Point();
            try
            {
                temp.X = float.Parse(txtCrdX.Text);
                temp.Y = float.Parse(txtCrdY.Text);
                temp.Z = float.Parse(txtCrdZ.Text);
                temp.Axis = cmbTools.SelectedIndex;
                temp.ScrewType = cmbFeeder.SelectedIndex;
                temp.WorkMode = cmbWorkMode.SelectedIndex;
                //changeCount = int.Parse(txtCount.Text);
                int index = int.Parse(txtScrewNum.Text) - 1;
                if (tableIndex == 0)
                {
                    Points.points.RemoveAt(index);
                    Points.points.Insert(index, temp);
                }
                if(tableIndex==1)
                {
                    Points.points2.RemoveAt(index);
                    Points.points2.Insert(index, temp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            MessageBox.Show("更新点位成功!");
            this.Close();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            Points.Point temp = new Points.Point();
            try
            {
                if (tableIndex == 0)
                {

                    changeCount = int.Parse(txtCount.Text);
                    int index = int.Parse(txtScrewNum.Text) - 1;
                    int endindex = index + changeCount;
                    if (endindex > Points.points.Count)
                        endindex = Points.points.Count;
                    for (int i = index; i < endindex; i++)
                    {
                        if (i == index)
                        {
                            temp.X = float.Parse(txtCrdX.Text);
                            temp.Y = float.Parse(txtCrdY.Text);
                        }
                        else
                        {
                            temp.X = Points.points[i].X;
                            temp.Y = Points.points[i].Y;
                        }

                        temp.Z = ckb0.Checked == true ? float.Parse(txtCrdZ.Text) : Points.points[i].Z;
                        temp.Axis = ckb1.Checked == true ? cmbTools.SelectedIndex : Points.points[i].Axis;
                        temp.ScrewType = ckb2.Checked == true ? cmbFeeder.SelectedIndex : Points.points[i].ScrewType;
                        temp.WorkMode = ckb3.Checked == true ? cmbWorkMode.SelectedIndex : Points.points[i].WorkMode;
                        Points.points.RemoveAt(i);
                        Points.points.Insert(i, temp);
                    }
                }
                if (tableIndex == 1)
                {
                    changeCount = int.Parse(txtCount.Text);
                    int index = int.Parse(txtScrewNum.Text) - 1;
                    int endindex = index + changeCount;
                    if (endindex > Points.points2.Count)
                        endindex = Points.points2.Count;
                    for (int i = index; i < endindex; i++)
                    {
                        if (i == index)
                        {
                            temp.X = float.Parse(txtCrdX.Text);
                            temp.Y = float.Parse(txtCrdY.Text);
                        }
                        else
                        {
                            temp.X = Points.points2[i].X;
                            temp.Y = Points.points2[i].Y;
                        }

                        temp.Z = ckb0.Checked == true ? float.Parse(txtCrdZ.Text) : Points.points2[i].Z;
                        temp.Axis = ckb1.Checked == true ? cmbTools.SelectedIndex : Points.points2[i].Axis;
                        temp.ScrewType = ckb2.Checked == true ? cmbFeeder.SelectedIndex : Points.points2[i].ScrewType;
                        temp.WorkMode = ckb3.Checked == true ? cmbWorkMode.SelectedIndex : Points.points2[i].WorkMode;
                        Points.points2.RemoveAt(i);
                        Points.points2.Insert(i, temp);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            MessageBox.Show("更新点位成功!");
            this.Close();
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
