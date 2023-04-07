using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DataBase
{
	[DesignerGenerated]
	public class DataBase : UserControl
	{
		public struct ERRINF
		{
			public string Type;

			public short Count;
		}

		private delegate void AddItemsDelegate();

		private OleDbConnection oleCon;

		private SqlConnection sqlCon;

		private List<string> EquipCmbItems = new List<string>();

		private List<string> ProdCmbItems = new List<string>();

		private List<string> BarcodeCmbItems = new List<string>();

		private List<string> ErrorName = new List<string>();

		private List<int> ErrorCount = new List<int>();

		private short DataBaseType = -1;

		private bool DataBaseLink;

		private string AccFileName = "";

		private string SqlServerIP = "";

		private string SqlDbName = "";

		private DataBase.AddItemsDelegate AddItems;

		private Container components;

		private Label label9;

		private GroupBox groupBox2;

		private Button butDeleteDB;

		private Button butLinkDB;

		private Label labRealTime;

		private Label label7;

		private Label label6;

		private Label labTotalTime;

		private Label labMachineRation;

		private Label label5;

		private DateTimePicker dtpFinish;

		private ComboBox cmbBarCode;

		private ComboBox cmbMode;

		private ComboBox cmbProduct;

		private ComboBox cmbEquipID;

		private Label label2;

		private Label label8;

		private DateTimePicker dtpStart;

		private Label label4;

		private Button butDataQuery;

		private SaveFileDialog saveFileDialog1;

		private Button butToFile;

		private GroupBox groupBox1;

		private Label labTotalPoint;

		private Label label10;

		private Label labPassration;

		private Label labTotalProduct;

		private BackgroundWorker bgwAddItems;

		private Label label3;

		private Label label1;

		private Label label119;

		private Label label120;

		private DataGridView DataGridView1;

		internal Chart Chart1;

		private Label labTotalError;

		private Label Label12;

		private Label labPointPeriod;

		private Label Label15;

		private Label labProdPeriod;

		private Label Label13;

		private Button butClearCondi;

		private Button Button1;

		private Label labErrCount3;

		private Label labErrCount2;

		private Label labErrCount1;

		private Label labErrName3;

		private Label labErrName2;

		private Label labErrName1;

		internal TextBox TextBox1;

		private Label labFaultTime;

		private Label labWaitTime;

		private Label Label16;

		private Label Label17;
        private BackgroundWorker bgwExport;
        private ProgressBar progressBar1;

		internal Panel Panel1;

		public DataBase()
		{
			this.InitializeComponent();
            dtpStart.Value = DateAndTime.Now;
		}

		private string StrDate(string str)
		{
			string mydate = str.Replace("/", "-");
			string dateNew = mydate.Substring(0, str.IndexOf(" "));
			string dateNew2 = mydate.Substring(str.IndexOf(" ") + 1);
			char[] C = new char[]
			{
				'-'
			};
			char[] D = new char[]
			{
				':'
			};
			string[] dateinfo = dateNew.Split(C);
			string[] dateinfo2 = dateNew2.Split(D);
			if (dateinfo[1].Trim().Length <= 1)
			{
				dateNew = dateinfo[0] + "-0" + dateinfo[1];
			}
			else
			{
				dateNew = dateinfo[0].ToString() + "-" + dateinfo[1];
			}
			if (dateinfo[2].Trim().Length <= 1)
			{
				dateNew = dateNew + "-0" + dateinfo[2];
			}
			else
			{
				dateNew = dateNew + "-" + dateinfo[2];
			}
			if (dateinfo2[0].Trim().Length <= 1)
			{
				dateNew2 = "0" + dateinfo2[0];
			}
			else
			{
				dateNew2 = dateinfo2[0];
			}
			if (dateinfo2[1].Trim().Length <= 1)
			{
				dateNew2 = dateNew2 + ":0" + dateinfo2[1];
			}
			else
			{
				dateNew2 = dateNew2 + ":" + dateinfo2[1];
			}
			if (dateinfo2[2].Trim().Length <= 1)
			{
				dateNew2 = dateNew2 + ":0" + dateinfo2[2];
			}
			else
			{
				dateNew2 = dateNew2 + ":" + dateinfo2[2];
			}
			return dateNew + " " + dateNew2;
		}

		public void btnDataQuery_Click(object sender, EventArgs e)
		{
			this.labTotalTime.Text = "----";
			this.labRealTime.Text = "----";
			this.labWaitTime.Text = "----";
			this.labFaultTime.Text = "----";
			this.labMachineRation.Text = "----";
			this.labTotalProduct.Text = "----";
			this.labTotalPoint.Text = "----";
			this.labPassration.Text = "----";
			this.labProdPeriod.Text = "----";
			this.labPointPeriod.Text = "----";
			this.labErrName1.Text = "";
			this.labErrName2.Text = "";
			this.labErrName3.Text = "";
			this.labErrCount1.Text = "";
			this.labErrCount2.Text = "";
			this.labErrCount3.Text = "";
			this.EquipCmbItems.Clear();
			this.ProdCmbItems.Clear();
			this.BarcodeCmbItems.Clear();
			this.ErrorName.Clear();
			this.ErrorCount.Clear();
			this.DataGridView1.DataSource = null;
			if (this.cmbMode.SelectedIndex < 0)
			{
				this.cmbMode.SelectedIndex = 0;
			}
			int totalProduc = 0;
			int totalnum = 0;
			int totalerror = 0;
			object rpttable = null;
			if (this.dtpFinish.Value <= this.dtpStart.Value)
			{
				Interaction.MsgBox("起止时间选择冲突！", MsgBoxStyle.Exclamation, "数据查询");
				return;
			}
			string date = this.StrDate(this.dtpStart.Value.ToString());
			string date2 = this.StrDate(this.dtpFinish.Value.ToString());
			int Readresult = (int)this.ReadFromDataBase(date, date2, this.cmbEquipID.Text, this.cmbProduct.Text, this.cmbBarCode.Text, ref rpttable);
			this.DataGridView1.DataSource = rpttable;
			if (Readresult < 0)
			{
				Interaction.MsgBox("数据库查询出错！", MsgBoxStyle.Exclamation, "数据查询");
				return;
			}
			if (rpttable == null)
			{
				Interaction.MsgBox("没有符合条件的数据！", MsgBoxStyle.Exclamation, "数据查询");
				return;
			}
			string totaltime = Convert.ToString((this.dtpFinish.Value - this.dtpStart.Value).ToString());
			string subtotaltime = totaltime;
			if (subtotaltime.IndexOf(".") == 8)
			{
				totaltime = subtotaltime.Substring(0, 8);
			}
			else
			{
				totaltime = subtotaltime.Substring(0, subtotaltime.IndexOf(".") + 1);
				subtotaltime = subtotaltime.Substring(subtotaltime.IndexOf(".") + 1);
				totaltime += subtotaltime.Substring(0, 8);
			}
			string str6 = string.Empty;
			string str7 = string.Empty;
			DateTime dte = default(DateTime);
			if (this.DataGridView1.Rows.Count > 0)
			{
				string strtemp = string.Empty;
				for (int i = 0; i <= this.DataGridView1.Rows.Count - 1; i++)
				{
					strtemp = this.DataGridView1.Rows[i].Cells[4].Value.ToString().Trim();
					if (!string.IsNullOrEmpty(strtemp) && !this.ProdCmbItems.Contains(strtemp))
					{
						this.ProdCmbItems.Add(strtemp);
					}
					strtemp = this.DataGridView1.Rows[i].Cells[3].Value.ToString().Trim();
					if (!string.IsNullOrEmpty(strtemp) && !this.EquipCmbItems.Contains(strtemp))
					{
						this.EquipCmbItems.Add(strtemp);
					}
					strtemp = this.DataGridView1.Rows[i].Cells[5].Value.ToString().Trim();
					if (!string.IsNullOrEmpty(strtemp) && !this.BarcodeCmbItems.Contains(strtemp))
					{
						this.BarcodeCmbItems.Add(strtemp);
					}
				}
				for (int i = 0; i <= this.DataGridView1.Rows.Count - 1; i++)
				{
					totalProduc++;
					str6 = Convert.ToString(this.DataGridView1.Rows[i].Cells[6].Value.ToString());
					if (string.IsNullOrEmpty(str6))
					{
						totalnum = totalnum;
					}
					else
					{
						totalnum += Convert.ToInt32(str6.Trim());
					}
					str7 = Convert.ToString(this.DataGridView1.Rows[i].Cells[7].Value.ToString());
					if (string.IsNullOrEmpty(str7))
					{
						totalerror = totalerror;
					}
					else
					{
						totalerror += Convert.ToInt32(str7.Trim());
					}
					if (Convert.ToDateTime(this.DataGridView1.Rows[i].Cells[9].Value) >= Convert.ToDateTime(this.DataGridView1.Rows[i].Cells[8].Value))
					{
						dte += Convert.ToDateTime(this.DataGridView1.Rows[i].Cells[9].Value) - Convert.ToDateTime(this.DataGridView1.Rows[i].Cells[8].Value);
					}
					else
					{
						dte += Convert.ToDateTime(this.DataGridView1.Rows[i].Cells[8].Value) - Convert.ToDateTime(this.DataGridView1.Rows[i].Cells[9].Value);
					}
				}
				if (!this.bgwAddItems.IsBusy)
				{
					this.bgwAddItems.RunWorkerAsync();
				}
			}
			if (this.DataGridView1.Rows.Count > 0)
			{
				string strtemp2 = string.Empty;
				for (int i = 0; i <= this.DataGridView1.Rows.Count - 1; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						strtemp2 = this.DataGridView1.Rows[i].Cells[11 + j].Value.ToString().Trim();
						string temp = this.DataGridView1.Rows[i].Cells[14 + j].Value.ToString().Trim();
						if (!string.IsNullOrEmpty(strtemp2) & !string.IsNullOrEmpty(temp))
						{
							int.Parse(temp);
						}
						if (!string.IsNullOrEmpty(strtemp2) && !this.ErrorName.Contains(strtemp2))
						{
							this.ErrorName.Add(strtemp2);
						}
						short errind = (short)this.ErrorName.IndexOf(strtemp2);
						if (this.ErrorCount.Count <= (int)errind)
						{
							this.ErrorCount.Add(0);
						}
						if (!string.IsNullOrEmpty(temp))
						{
							List<int> errorCount;
							int index;
							(errorCount = this.ErrorCount)[index = (int)errind] = errorCount[index] + int.Parse(temp);
						}
					}
				}
			}
			if (totalProduc != 0)
			{
				this.labTotalProduct.Text = totalProduc.ToString();
			}
			else
			{
				this.labTotalProduct.Text = "----";
			}
			if (totalnum != 0)
			{
				this.labTotalPoint.Text = totalnum.ToString();
			}
			else
			{
				this.labTotalPoint.Text = "----";
			}
			if (totalerror != 0 && totalnum != 0)
			{
				float passrate = (float)Math.Round((double)(totalnum - totalerror) / (double)totalnum * 100.0, 2);
				this.labPassration.Text = passrate.ToString() + "%";
				this.labTotalError.Text = totalerror.ToString();
			}
			else
			{
				this.labPassration.Text = "----";
				this.labTotalError.Text = "----";
			}
			if (this.ErrorName.Count >= 1)
			{
				this.labErrName1.Text = this.ErrorName[0] + "：";
			}
			if (this.ErrorCount.Count >= 1)
			{
				this.labErrCount1.Text = this.ErrorCount[0].ToString();
			}
			if (this.ErrorName.Count >= 2)
			{
				this.labErrName2.Text = this.ErrorName[1] + "：";
			}
			if (this.ErrorCount.Count >= 2)
			{
				this.labErrCount2.Text = this.ErrorCount[1].ToString();
			}
			if (this.ErrorName.Count >= 3)
			{
				this.labErrName3.Text = this.ErrorName[2] + "：";
			}
			if (this.ErrorCount.Count >= 3)
			{
				this.labErrCount3.Text = this.ErrorCount[2].ToString();
			}
			int timetotal = 0;
			int timereal = 0;
			int faulttime = 0;
			if (!string.IsNullOrEmpty(totaltime))
			{
				string strtemp3 = string.Empty;
				if (totaltime.Contains("."))
				{
					timetotal = (int)(Convert.ToDouble(totaltime.Substring(0, totaltime.IndexOf("."))) * 24.0 * 60.0 * 60.0);
					strtemp3 = totaltime.Substring(0, totaltime.IndexOf(".")) + "天";
					totaltime = totaltime.Substring(totaltime.IndexOf(".") + 1);
				}
				timetotal += (int)(Convert.ToDouble(totaltime.Substring(0, totaltime.IndexOf(":"))) * 60.0 * 60.0);
				strtemp3 = strtemp3 + totaltime.Substring(0, totaltime.IndexOf(":")) + "时";
				totaltime = totaltime.Substring(totaltime.IndexOf(":") + 1);
				timetotal += (int)(Convert.ToDouble(totaltime.Substring(0, totaltime.IndexOf(":"))) * 60.0);
				strtemp3 = strtemp3 + totaltime.Substring(0, totaltime.IndexOf(":")) + "分";
				totaltime = totaltime.Substring(totaltime.IndexOf(":") + 1);
				timetotal += (int)Convert.ToDouble(totaltime);
				strtemp3 = strtemp3 + totaltime + Convert.ToString("秒");
				this.labTotalTime.Text = strtemp3;
			}
			else
			{
				this.labTotalTime.Text = "--天--时--分--秒";
			}
			string dt = dte.ToString();
			if (this.DataGridView1.Rows.Count > 0)
			{
				int dates = 0;
				string dat = this.StrDate(dte.ToString());
				if (dat.Substring(0, dat.IndexOf("-")) == "0001")
				{
					dat = dat.Substring(dat.IndexOf("-") + 1);
				}
				else
				{
					dates += Convert.ToInt32(dat.Substring(0, dat.IndexOf("-")).ToString()) * 365;
					dat = dat.Substring(dat.IndexOf("-") + 1);
				}
				if (dat.Substring(0, dat.IndexOf("-")) == "01")
				{
					dat = dat.Substring(dat.IndexOf("-") + 1);
				}
				else
				{
					dates += Convert.ToInt32(dat.Substring(0, dat.IndexOf("-")).ToString()) * 30;
					dat = dat.Substring(dat.IndexOf("-") + 1);
				}
				if (dat.Substring(0, dat.IndexOf(" ")) == "01")
				{
					dat = dat.Substring(dat.IndexOf(" ") + 1);
				}
				else
				{
					dates += Convert.ToInt32(dat.Substring(0, dat.IndexOf(" ")).ToString());
					dat = dat.Substring(dat.IndexOf(" ") + 1);
				}
				dat += "秒";
				string strtemp4 = dat.Substring(0, dat.IndexOf(":")) + "时";
				dat = dat.Substring(dat.IndexOf(":") + 1);
				strtemp4 += dat.Replace(":", "分");
				if (dates == 0)
				{
					this.labRealTime.Text = strtemp4;
				}
				else
				{
					this.labRealTime.Text = Convert.ToString(dates) + "天" + strtemp4;
				}
				dt = this.labRealTime.Text;
				DateTime ustim = default(DateTime);
				int ustimint = 0;
				for (int i = 0; i <= this.DataGridView1.Rows.Count - 1; i++)
				{
					if (Convert.ToDateTime(this.DataGridView1.Rows[i].Cells[9].Value) >= Convert.ToDateTime(this.DataGridView1.Rows[i].Cells[8].Value))
					{
						ustim += Convert.ToDateTime(this.DataGridView1.Rows[i].Cells[9].Value) - Convert.ToDateTime(this.DataGridView1.Rows[i].Cells[8].Value);
					}
					else
					{
						ustim += Convert.ToDateTime(this.DataGridView1.Rows[i].Cells[8].Value) - Convert.ToDateTime(this.DataGridView1.Rows[i].Cells[9].Value);
					}
					ustimint += ustim.Hour * 3600 + ustim.Minute * 60 + ustim.Second;
				}
				if (totalProduc > 0 & ustimint > 0)
				{
					this.labProdPeriod.Text = Strings.Format((double)ustimint / (double)totalProduc, "#0").ToString() + "秒";
				}
				else
				{
					this.labProdPeriod.Text = "----";
				}
				if (totalnum > 0 & ustimint > 0)
				{
					this.labPointPeriod.Text = Strings.Format((double)ustimint / (double)totalnum, "#0.0").ToString() + "秒";
				}
				else
				{
					this.labPointPeriod.Text = "----";
				}
			}
			else
			{
				this.labProdPeriod.Text = "----";
				this.labPointPeriod.Text = "----";
				this.labRealTime.Text = "--天--时--分--秒";
			}
			if (this.DataGridView1.Rows.Count > 0)
			{
				if (dt.Contains("天"))
				{
					timereal = Convert.ToInt32(dt.Substring(0, dt.IndexOf("天"))) * 24 * 60 * 60;
					dt = dt.Substring(dt.IndexOf("天") + 1);
				}
				timereal += Convert.ToInt32(dt.Substring(0, dt.IndexOf("时"))) * 60 * 60;
				dt = dt.Substring(dt.IndexOf("时") + 1);
				timereal += Convert.ToInt32(dt.Substring(0, dt.IndexOf("分"))) * 60;
				dt = dt.Substring(dt.IndexOf("分") + 1);
				timereal += Convert.ToInt32(dt.Substring(0, dt.IndexOf("秒")));
				this.labMachineRation.Text = Math.Round((double)timereal / (double)timetotal * 100.0, 2).ToString() + "%";
			}
			else
			{
				this.labMachineRation.Text = "----";
			}
			int[] tval = new int[4];
			if (this.DataGridView1.Rows.Count > 0)
			{
				for (int i = 0; i <= this.DataGridView1.RowCount - 1; i++)
				{
					faulttime += (int)Conversion.Val(this.DataGridView1.Rows[i].Cells[10].Value);
				}
				tval[0] = faulttime / 86400;
				tval[1] = Convert.ToInt32((faulttime - tval[0] * 86400) / 3600);
				tval[2] = Convert.ToInt32((faulttime - tval[0] * 86400 - tval[1] * 3600) / 60);
				tval[3] = faulttime - tval[0] * 86400 - tval[1] * 3600 - tval[2] * 60;
				this.labFaultTime.Text = string.Concat(new string[]
				{
					tval[0].ToString(),
					"天",
					tval[1].ToString(),
					"时",
					tval[2].ToString(),
					"分",
					tval[3].ToString(),
					"秒"
				});
				int waittime = timetotal - timereal;
				tval[0] = waittime / 86400;
				tval[1] = Convert.ToInt32((waittime - tval[0] * 86400) / 3600);
				tval[2] = Convert.ToInt32((waittime - tval[0] * 86400 - tval[1] * 3600) / 60);
				tval[3] = waittime - tval[0] * 86400 - tval[1] * 3600 - tval[2] * 60;
				this.labWaitTime.Text = string.Concat(new string[]
				{
					tval[0].ToString(),
					"天",
					tval[1].ToString(),
					"时",
					tval[2].ToString(),
					"分",
					tval[3].ToString(),
					"秒"
				});
			}
			else
			{
				this.labFaultTime.Text = "----";
				this.labWaitTime.Text = "----";
			}
			new List<DataBase.ERRINF>();
			List<string> xdata = new List<string>();
			List<string> ydata = new List<string>();
			if (totalnum == 0)
			{
				return;
			}
			xdata.Add("1");
			ydata.Add(Strings.Format((double)(totalnum - totalerror), "#0.00"));
			if (this.ErrorName.Count >= 1)
			{
				xdata.Add("1");
				ydata.Add(Strings.Format(this.ErrorCount[0], "#0.00"));
			}
			if (this.ErrorName.Count >= 2)
			{
				xdata.Add("1");
				ydata.Add(Strings.Format(this.ErrorCount[1], "#0.00"));
			}
			if (this.ErrorName.Count >= 3)
			{
				xdata.Add("1");
				ydata.Add(Strings.Format(this.ErrorCount[2], "#0.00"));
			}
			this.Chart1.Series[0].Points.DataBindXY(xdata, new IEnumerable[]
			{
				ydata
			});
			if (ydata.Count >= 1)
			{
				this.Chart1.Series[0].Points[0].Color = Color.Green;
			}
			if (ydata.Count >= 2)
			{
				this.Chart1.Series[0].Points[1].Color = Color.Red;
			}
			if (ydata.Count >= 3)
			{
				this.Chart1.Series[0].Points[2].Color = Color.OrangeRed;
			}
			if (ydata.Count >= 4)
			{
				this.Chart1.Series[0].Points[3].Color = Color.Orange;
			}
		}

		public void bgwAddItems_DoWork(object sender, DoWorkEventArgs e)
		{
			if (base.InvokeRequired)
			{
				this.AddItems = new DataBase.AddItemsDelegate(this.AddcomboBoxItems);
				base.BeginInvoke(this.AddItems);
			}
		}

		private void AddcomboBoxItems()
		{
			if (this.ProdCmbItems != null)
			{
				this.cmbProduct.Items.Clear();
				this.cmbProduct.Items.AddRange(this.ProdCmbItems.ToArray());
			}
			if (this.EquipCmbItems != null)
			{
				this.cmbEquipID.Items.Clear();
				this.cmbEquipID.Items.AddRange(this.EquipCmbItems.ToArray());
			}
			if (this.BarcodeCmbItems != null)
			{
				this.cmbBarCode.Items.Clear();
				this.cmbBarCode.Items.AddRange(this.BarcodeCmbItems.ToArray());
			}
		}

		public void btnToFile_Click(object sender, EventArgs e)
		{
			if (this.DataGridView1.RowCount <= 0)
			{
				MessageBox.Show("没有报表数据可导出！", "报表导出");
				return;
			}
			this.saveFileDialog1.Filter = "CSV格式(*.csv)|*.csv";
			this.saveFileDialog1.FileName = "ReportExport_" + DateTime.Now.ToString("yyyyMMddHHmmss");
			this.saveFileDialog1.FilterIndex = 2;
			if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
                progressBar1.Visible = true;
				
                bgwExport.RunWorkerAsync();

                //if (!this.ExportData(filePath, false))
                //{
                //    MessageBox.Show("导出文件失败！");
                //}
			}
		}
        private void bgwExport_DoWork(object sender, DoWorkEventArgs e)
        {
            string filePath = this.saveFileDialog1.FileName;
            progressBar1.Maximum = DataGridView1.Rows.Count - 1;
            ExportData(filePath, false);
        }
        private void bgwExport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = progressNum;
        }

        private void bgwExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Visible = false;
        }

        int progressNum = 0;
		public bool ExportData(string Files, bool ClrData = false)
		{
			StreamWriter writer = null;
			try
			{
				writer = new StreamWriter(Files, false, Encoding.UTF8);
				string str = string.Empty;
				if (this.DataGridView1.Rows.Count > 0)
				{
                    str="序号,日期,产线名称,设备名称,工艺名称,产品名称,产品条码,任务点数,异常点数,良率,开始时间,结束时间,故障时间,滑牙,浮钉,其他";
                    writer.WriteLine(str);
					for (int j = 0; j <= this.DataGridView1.Rows.Count - 1; j++)
                    {
                        str = string.Empty;
                        progressNum = j;
                        DateTime dat = DateTime.Parse(DataGridView1.Rows[j].Cells[8].Value.ToString());
                        DateTime dat2 = DateTime.Parse(DataGridView1.Rows[j].Cells[9].Value.ToString());
                        int tNum = int.Parse(DataGridView1.Rows[j].Cells[6].Value.ToString());
                        int eNum = int.Parse(DataGridView1.Rows[j].Cells[7].Value.ToString());
                        int yield = (tNum - eNum) / tNum;
                        str = DataGridView1.Rows[j].Cells[0].Value.ToString() + "," + dat.ToShortDateString() + "," + DataGridView1.Rows[j].Cells[1].Value.ToString() + "," + DataGridView1.Rows[j].Cells[2].Value.ToString() + "," + DataGridView1.Rows[j].Cells[3].Value.ToString() + "," + DataGridView1.Rows[j].Cells[4].Value.ToString() + "," + DataGridView1.Rows[j].Cells[5].Value.ToString() + "," + DataGridView1.Rows[j].Cells[6].Value.ToString() + "," + DataGridView1.Rows[j].Cells[7].Value.ToString() + "," + yield + "," + dat.TimeOfDay + "," + dat2.TimeOfDay + "," + DataGridView1.Rows[j].Cells[10].Value.ToString() + "," + DataGridView1.Rows[j].Cells[14].Value.ToString() + "," + DataGridView1.Rows[j].Cells[15].Value.ToString() ;						
                        writer.WriteLine(str);
					}
				}
				writer.Close();
				if (ClrData)
				{
					this.DataGridView1.DataSource = null;
				}
				return true;
			}
			catch (Exception ex)
			{

			}
			finally
			{
				if (writer != null)
				{
					writer.Close();
				}
			}
			return false;
		}

		public void butClearCondi_Click(object sender, EventArgs e)
		{
			if (this.cmbMode.SelectedIndex < 0)
			{
				this.cmbMode.SelectedIndex = 0;
			}
			if (!this.DataBaseLink || this.cmbMode.SelectedIndex != (int)this.DataBaseType)
			{
				this.LinkDb(this.AccFileName, "sa", "autolinexh");
			}
			this.cmbProduct.Text = "";
			this.cmbEquipID.Text = "";
			this.cmbBarCode.Text = "";
		}

		public void butLinkDB_Click(object sender, EventArgs e)
		{
			if (this.cmbMode.SelectedIndex < 0)
			{
				this.cmbMode.SelectedIndex = 0;
			}
			bool link = this.LinkDb("C:\\Automation\\rptdb.accdb", "sa", "autolinexh");
			MessageBox.Show(Convert.ToString(link));
		}

		public bool LinkDb(string DBFilePath = "C:\\AutoMation\\rptdb.accdb", string User = "sa", string key = "autolinexh")
		{
			this.DataBaseLink = false;
			string Constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + DBFilePath + "'";
			try
			{
				this.oleCon = new OleDbConnection(Constr);
				this.oleCon.Open();
				if (this.oleCon.State == ConnectionState.Open)
				{
					this.AccFileName = DBFilePath;
					this.DataBaseLink = true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				return false;
			}
			return this.DataBaseLink;
		}

		public bool CloseDb()
		{
			bool closeResult = false;
			try
			{
				if (this.oleCon.State == ConnectionState.Open)
				{
					this.oleCon.Close();
					this.oleCon.Dispose();
				}
				closeResult = true;
				this.DataBaseLink = false;
			}
			catch (Exception)
			{
				closeResult = false;
			}
			return closeResult;
		}

		public void Button1_Click(object sender, EventArgs e)
		{
			string tstr = "自动线-SQL,设备-SQL,工艺SQL,产品-SQL,123456,100,5,2017-02-18 08:15:15,2017-02-18 08:17:15,";
			tstr += "20,123,错漏装,高度超差,位置超差,1,2,2,备";
			List<string> na = new List<string>();
			List<int> cn = new List<int>();
			na.Add("错误1");
			na.Add("错误2");
			cn.Add(5);
			cn.Add(10);
			MessageBox.Show(this.WriteToDataBase(tstr).ToString());
		}

		public short WriteToDataBase(string Para)
		{
			short result = -1;
			if (!this.DataBaseLink)
			{
				return result;
			}
			string[] strpara = Para.Split(new char[]
			{
				',',
				'，'
			});
			new List<string>();
			if (strpara.Count<string>() != 18)
			{
				return -2;
			}
			string sqlrpt = string.Empty;
			string arg_55_0 = string.Empty;
			string trpt = "insert into tb_AutomationLine(产线名称,设备名称,工艺名称,产品名称,产品条码,任务点数,异常点数,开始时间,结束时间,";
			trpt += "故障时间,操作员工号,异常名称1,异常名称2,异常名称3,异常数1,异常数2,异常数3,备注) values ";
			string tstr = "( ";
			for (int i = 0; i <= 17; i++)
			{
				tstr = tstr + "'" + strpara[i] + "'";
				if (i < 17)
				{
					tstr += ",";
				}
			}
			tstr += ");";
			sqlrpt = trpt + tstr;
			try
			{
				OleDbCommand cmd = new OleDbCommand(sqlrpt, this.oleCon);
				cmd.CommandTimeout = 3;
				if (this.oleCon.State != ConnectionState.Open)
				{
					this.oleCon.Open();
				}
				if (cmd.ExecuteNonQuery() > 0)
				{
					result = 0;
				}
				else
				{
					result = 2;
				}
			}
			catch (Exception)
			{
				result = 1;
			}
			return result;
		}

		public void butDeleteDB_Click(object sender, EventArgs e)
		{
			short test = this.DeleteDataByBarCode("123456");
			MessageBox.Show(Convert.ToString(test));
		}

		public short DeleteDataByBarCode(string BarCode)
		{
			short result = -1;
			if (!this.DataBaseLink)
			{
				return result;
			}
			string strsql = "delete from tb_AutomationLine where 产品条码='" + BarCode + "'";
			try
			{
				OleDbCommand olecmd = new OleDbCommand(strsql, this.oleCon);
				olecmd.CommandTimeout = 3;
				if (this.oleCon.State != ConnectionState.Open)
				{
					this.oleCon.Open();
				}
				if (olecmd.ExecuteNonQuery() > 0)
				{
					result = 0;
				}
				else
				{
					result = 2;
				}
			}
			catch (Exception)
			{
				result = 1;
			}
			return result;
		}

		public short DeleteDataByDate(string StrDate)
		{
			short result = -1;
			if (!this.DataBaseLink)
			{
				return result;
			}
			string strsql = "delete from tb_AutomationLine where 结束时间<#" + StrDate + "#";
			try
			{
				OleDbCommand olecmd = new OleDbCommand(strsql, this.oleCon);
				olecmd.CommandTimeout = 3;
				if (this.oleCon.State != ConnectionState.Open)
				{
					this.oleCon.Open();
				}
				if (olecmd.ExecuteNonQuery() > 0)
				{
					result = 0;
				}
				else
				{
					result = 2;
				}
			}
			catch (Exception)
			{
				result = 1;
			}
			return result;
		}

		public bool GetDateTime(ref DateTime bdate, ref DateTime edate)
		{
			bdate = this.dtpStart.Value;
			edate = this.dtpFinish.Value;
			return true;
		}

		public bool SetDataTime(DateTime bdate, DateTime edate)
		{
			this.dtpStart.Value = bdate;
			this.dtpFinish.Value = edate;
			return true;
		}

		public short ReadFromDataBase(string Date1, string Date2, string EquipID, string Product, string BarCode, ref object ReportTable)
		{
			short result = -1;
			if (!this.DataBaseLink)
			{
				return result;
			}
			string sqlrpt = string.Empty;
			string sqlerr = string.Empty;
			string tstr = string.Empty;
			string tsel = "ID as 序号,产线名称,设备名称,工艺名称,产品名称,产品条码,任务点数,异常点数,开始时间,结束时间,故障时间 ,异常名称1,异常名称2,异常名称3,异常数1,异常数2,异常数3";
			if (EquipID == "" && Product == "" && BarCode == "")
			{
				sqlrpt = string.Concat(new string[]
				{
					"select ",
					tsel,
					" from tb_AutomationLine where 开始时间 between #",
					Date1,
					"# and #",
					Date2,
					"#"
				});
			}
			else
			{
				sqlrpt = "SELECT " + tsel + " from tb_AutomationLine where ";
				if (EquipID != "")
				{
					tstr = "工艺名称='" + EquipID + "' ";
					if (Product != "")
					{
						tstr = tstr + "and 产品名称='" + Product + "' ";
					}
					if (BarCode != "")
					{
						tstr = tstr + "and 产品条码='" + BarCode + "' ";
					}
				}
				else if (Product != "")
				{
					tstr = "产品名称='" + Product + "' ";
					if (BarCode != "")
					{
						tstr = tstr + "and 产品条码='" + BarCode + "' ";
					}
				}
				else if (BarCode != "")
				{
					tstr = "产品条码='" + BarCode + "' ";
				}
				string text = sqlrpt;
				sqlrpt = string.Concat(new string[]
				{
					text,
					tstr,
					"and 开始时间 between #",
					Date1,
					"# and #",
					Date2,
					"#"
				});
				string text2 = sqlerr;
				sqlerr = string.Concat(new string[]
				{
					text2,
					tstr,
					"and 开始时间 between #",
					Date1,
					"# and #",
					Date2,
					"#"
				});
			}
			try
			{
				OleDbDataAdapter oleda = new OleDbDataAdapter(sqlrpt, this.oleCon);
				DataSet dsrpt = new DataSet();
				if (oleda.Fill(dsrpt) > 0)
				{
					ReportTable = dsrpt.Tables[0].DefaultView;
					result = 0;
				}
				else
				{
					result = 1;
				}
			}
			catch (Exception)
			{
				result = -3;
			}
			return result;
		}

		private void DataBase_Load(object sender, EventArgs e)
		{
		}

		[DebuggerNonUserCode]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.components != null)
				{
					this.components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		[DebuggerStepThrough]
		private void InitializeComponent()
		{
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.labMachineRation = new System.Windows.Forms.Label();
            this.labRealTime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labTotalTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.butDeleteDB = new System.Windows.Forms.Button();
            this.butLinkDB = new System.Windows.Forms.Button();
            this.dtpFinish = new System.Windows.Forms.DateTimePicker();
            this.cmbBarCode = new System.Windows.Forms.ComboBox();
            this.cmbMode = new System.Windows.Forms.ComboBox();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.cmbEquipID = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.butDataQuery = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.butToFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labPointPeriod = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labProdPeriod = new System.Windows.Forms.Label();
            this.labPassration = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.labTotalPoint = new System.Windows.Forms.Label();
            this.labTotalProduct = new System.Windows.Forms.Label();
            this.bgwAddItems = new System.ComponentModel.BackgroundWorker();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label119 = new System.Windows.Forms.Label();
            this.label120 = new System.Windows.Forms.Label();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.Chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.labErrCount3 = new System.Windows.Forms.Label();
            this.labErrCount2 = new System.Windows.Forms.Label();
            this.labErrCount1 = new System.Windows.Forms.Label();
            this.labTotalError = new System.Windows.Forms.Label();
            this.labErrName3 = new System.Windows.Forms.Label();
            this.labErrName2 = new System.Windows.Forms.Label();
            this.labErrName1 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.butClearCondi = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.labFaultTime = new System.Windows.Forms.Label();
            this.labWaitTime = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.bgwExport = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).BeginInit();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(148, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 14);
            this.label9.TabIndex = 1;
            this.label9.Text = "总工作点：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.labMachineRation);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox2.Location = new System.Drawing.Point(835, 144);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(188, 103);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设备效率";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(16, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 14);
            this.label6.TabIndex = 1;
            this.label6.Text = "设备利用率：";
            this.label6.Visible = false;
            // 
            // labMachineRation
            // 
            this.labMachineRation.AutoSize = true;
            this.labMachineRation.ForeColor = System.Drawing.Color.Blue;
            this.labMachineRation.Location = new System.Drawing.Point(107, 42);
            this.labMachineRation.Name = "labMachineRation";
            this.labMachineRation.Size = new System.Drawing.Size(28, 14);
            this.labMachineRation.TabIndex = 1;
            this.labMachineRation.Text = "---";
            this.labMachineRation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labMachineRation.Visible = false;
            // 
            // labRealTime
            // 
            this.labRealTime.BackColor = System.Drawing.Color.Transparent;
            this.labRealTime.ForeColor = System.Drawing.Color.Blue;
            this.labRealTime.Location = new System.Drawing.Point(562, 317);
            this.labRealTime.Name = "labRealTime";
            this.labRealTime.Size = new System.Drawing.Size(174, 17);
            this.labRealTime.TabIndex = 1;
            this.labRealTime.Text = "--天--时--分--秒";
            this.labRealTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(459, 297);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "查询时间跨度：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labTotalTime
            // 
            this.labTotalTime.BackColor = System.Drawing.Color.Transparent;
            this.labTotalTime.ForeColor = System.Drawing.Color.Blue;
            this.labTotalTime.Location = new System.Drawing.Point(562, 297);
            this.labTotalTime.Name = "labTotalTime";
            this.labTotalTime.Size = new System.Drawing.Size(174, 17);
            this.labTotalTime.TabIndex = 1;
            this.labTotalTime.Text = "--天--时--分--秒";
            this.labTotalTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(459, 317);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "产品生产时间：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // butDeleteDB
            // 
            this.butDeleteDB.ForeColor = System.Drawing.Color.Black;
            this.butDeleteDB.Location = new System.Drawing.Point(265, 394);
            this.butDeleteDB.Name = "butDeleteDB";
            this.butDeleteDB.Size = new System.Drawing.Size(74, 32);
            this.butDeleteDB.TabIndex = 13;
            this.butDeleteDB.Text = "数据删除";
            this.butDeleteDB.UseVisualStyleBackColor = true;
            this.butDeleteDB.Visible = false;
            this.butDeleteDB.Click += new System.EventHandler(this.butDeleteDB_Click);
            // 
            // butLinkDB
            // 
            this.butLinkDB.ForeColor = System.Drawing.Color.Black;
            this.butLinkDB.Location = new System.Drawing.Point(66, 394);
            this.butLinkDB.Name = "butLinkDB";
            this.butLinkDB.Size = new System.Drawing.Size(74, 32);
            this.butLinkDB.TabIndex = 14;
            this.butLinkDB.Text = "数据连接";
            this.butLinkDB.UseVisualStyleBackColor = true;
            this.butLinkDB.Visible = false;
            this.butLinkDB.Click += new System.EventHandler(this.butLinkDB_Click);
            // 
            // dtpFinish
            // 
            this.dtpFinish.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFinish.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpFinish.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpFinish.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFinish.Location = new System.Drawing.Point(548, 42);
            this.dtpFinish.Name = "dtpFinish";
            this.dtpFinish.Size = new System.Drawing.Size(190, 26);
            this.dtpFinish.TabIndex = 18;
            // 
            // cmbBarCode
            // 
            this.cmbBarCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbBarCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbBarCode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbBarCode.FormattingEnabled = true;
            this.cmbBarCode.Location = new System.Drawing.Point(628, 111);
            this.cmbBarCode.Name = "cmbBarCode";
            this.cmbBarCode.Size = new System.Drawing.Size(110, 24);
            this.cmbBarCode.TabIndex = 23;
            // 
            // cmbMode
            // 
            this.cmbMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbMode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbMode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbMode.FormattingEnabled = true;
            this.cmbMode.Items.AddRange(new object[] {
            "0-本地"});
            this.cmbMode.Location = new System.Drawing.Point(500, 73);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(82, 24);
            this.cmbMode.TabIndex = 24;
            // 
            // cmbProduct
            // 
            this.cmbProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbProduct.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbProduct.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(628, 73);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(110, 24);
            this.cmbProduct.TabIndex = 25;
            // 
            // cmbEquipID
            // 
            this.cmbEquipID.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbEquipID.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbEquipID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbEquipID.FormattingEnabled = true;
            this.cmbEquipID.Location = new System.Drawing.Point(500, 111);
            this.cmbEquipID.Name = "cmbEquipID";
            this.cmbEquipID.Size = new System.Drawing.Size(82, 24);
            this.cmbEquipID.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(461, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 19;
            this.label2.Text = "数据：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(148, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 14);
            this.label8.TabIndex = 1;
            this.label8.Text = "总产品数：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpStart
            // 
            this.dtpStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpStart.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpStart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(548, 10);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(190, 26);
            this.dtpStart.TabIndex = 15;
            this.dtpStart.Value = new System.DateTime(2015, 1, 6, 11, 31, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(589, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 20;
            this.label4.Text = "条码：";
            // 
            // butDataQuery
            // 
            this.butDataQuery.ForeColor = System.Drawing.Color.Black;
            this.butDataQuery.Location = new System.Drawing.Point(562, 380);
            this.butDataQuery.Name = "butDataQuery";
            this.butDataQuery.Size = new System.Drawing.Size(74, 32);
            this.butDataQuery.TabIndex = 13;
            this.butDataQuery.Text = "开始查询";
            this.butDataQuery.UseVisualStyleBackColor = true;
            this.butDataQuery.Click += new System.EventHandler(this.btnDataQuery_Click);
            // 
            // butToFile
            // 
            this.butToFile.ForeColor = System.Drawing.Color.Black;
            this.butToFile.Location = new System.Drawing.Point(662, 380);
            this.butToFile.Name = "butToFile";
            this.butToFile.Size = new System.Drawing.Size(74, 32);
            this.butToFile.TabIndex = 14;
            this.butToFile.Text = "导出文件";
            this.butToFile.UseVisualStyleBackColor = true;
            this.butToFile.Click += new System.EventHandler(this.btnToFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labPointPeriod);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.labProdPeriod);
            this.groupBox1.Controls.Add(this.labPassration);
            this.groupBox1.Controls.Add(this.Label13);
            this.groupBox1.Controls.Add(this.Label15);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox1.Location = new System.Drawing.Point(836, 269);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 104);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "生产统计";
            // 
            // labPointPeriod
            // 
            this.labPointPeriod.AutoSize = true;
            this.labPointPeriod.BackColor = System.Drawing.Color.White;
            this.labPointPeriod.ForeColor = System.Drawing.Color.Blue;
            this.labPointPeriod.Location = new System.Drawing.Point(113, 57);
            this.labPointPeriod.Name = "labPointPeriod";
            this.labPointPeriod.Size = new System.Drawing.Size(35, 14);
            this.labPointPeriod.TabIndex = 36;
            this.labPointPeriod.Text = "----";
            this.labPointPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(6, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 14);
            this.label10.TabIndex = 1;
            this.label10.Text = "合格率：";
            // 
            // labProdPeriod
            // 
            this.labProdPeriod.AutoSize = true;
            this.labProdPeriod.BackColor = System.Drawing.Color.White;
            this.labProdPeriod.ForeColor = System.Drawing.Color.Blue;
            this.labProdPeriod.Location = new System.Drawing.Point(113, 32);
            this.labProdPeriod.Name = "labProdPeriod";
            this.labProdPeriod.Size = new System.Drawing.Size(35, 14);
            this.labProdPeriod.TabIndex = 34;
            this.labProdPeriod.Text = "----";
            this.labProdPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labPassration
            // 
            this.labPassration.AutoSize = true;
            this.labPassration.ForeColor = System.Drawing.Color.Blue;
            this.labPassration.Location = new System.Drawing.Point(109, 79);
            this.labPassration.Name = "labPassration";
            this.labPassration.Size = new System.Drawing.Size(28, 14);
            this.labPassration.TabIndex = 1;
            this.labPassration.Text = "---";
            this.labPassration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.BackColor = System.Drawing.Color.White;
            this.Label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label13.Location = new System.Drawing.Point(44, 32);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(77, 14);
            this.Label13.TabIndex = 33;
            this.Label13.Text = "产品节拍：";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.BackColor = System.Drawing.Color.White;
            this.Label15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label15.Location = new System.Drawing.Point(44, 57);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(77, 14);
            this.Label15.TabIndex = 35;
            this.Label15.Text = "点位节拍：";
            // 
            // labTotalPoint
            // 
            this.labTotalPoint.AutoSize = true;
            this.labTotalPoint.BackColor = System.Drawing.Color.White;
            this.labTotalPoint.ForeColor = System.Drawing.Color.Blue;
            this.labTotalPoint.Location = new System.Drawing.Point(220, 31);
            this.labTotalPoint.Name = "labTotalPoint";
            this.labTotalPoint.Size = new System.Drawing.Size(35, 14);
            this.labTotalPoint.TabIndex = 1;
            this.labTotalPoint.Text = "----";
            this.labTotalPoint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labTotalProduct
            // 
            this.labTotalProduct.AutoSize = true;
            this.labTotalProduct.BackColor = System.Drawing.Color.White;
            this.labTotalProduct.ForeColor = System.Drawing.Color.Blue;
            this.labTotalProduct.Location = new System.Drawing.Point(220, 7);
            this.labTotalProduct.Name = "labTotalProduct";
            this.labTotalProduct.Size = new System.Drawing.Size(35, 14);
            this.labTotalProduct.TabIndex = 1;
            this.labTotalProduct.Text = "----";
            this.labTotalProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bgwAddItems
            // 
            this.bgwAddItems.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwAddItems_DoWork);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(589, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 21;
            this.label3.Text = "产品：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(461, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 22;
            this.label1.Text = "设备：";
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label119.Location = new System.Drawing.Point(461, 17);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(70, 14);
            this.label119.TabIndex = 17;
            this.label119.Text = "起始时间:";
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label120.Location = new System.Drawing.Point(461, 49);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(70, 14);
            this.label120.TabIndex = 16;
            this.label120.Text = "结束时间:";
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
            this.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView1.DefaultCellStyle = dataGridViewCellStyle18;
            this.DataGridView1.Location = new System.Drawing.Point(3, 3);
            this.DataGridView1.Name = "DataGridView1";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.DataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.DataGridView1.RowHeadersVisible = false;
            this.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle20;
            this.DataGridView1.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DataGridView1.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView1.RowTemplate.Height = 23;
            this.DataGridView1.Size = new System.Drawing.Size(450, 437);
            this.DataGridView1.TabIndex = 27;
            // 
            // Chart1
            // 
            chartArea4.Name = "ChartArea1";
            this.Chart1.ChartAreas.Add(chartArea4);
            legend4.Enabled = false;
            legend4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend4.IsTextAutoFit = false;
            legend4.Name = "Legend1";
            this.Chart1.Legends.Add(legend4);
            this.Chart1.Location = new System.Drawing.Point(-6, -5);
            this.Chart1.Name = "Chart1";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            series4.IsValueShownAsLabel = true;
            series4.Label = "#PERCENT{P2}";
            series4.LabelForeColor = System.Drawing.Color.Yellow;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.Chart1.Series.Add(series4);
            this.Chart1.Size = new System.Drawing.Size(156, 155);
            this.Chart1.TabIndex = 30;
            this.Chart1.Text = "Chart1";
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.White;
            this.Panel1.Controls.Add(this.labErrCount3);
            this.Panel1.Controls.Add(this.labErrCount2);
            this.Panel1.Controls.Add(this.labErrCount1);
            this.Panel1.Controls.Add(this.labTotalError);
            this.Panel1.Controls.Add(this.labTotalPoint);
            this.Panel1.Controls.Add(this.labTotalProduct);
            this.Panel1.Controls.Add(this.labErrName3);
            this.Panel1.Controls.Add(this.labErrName2);
            this.Panel1.Controls.Add(this.labErrName1);
            this.Panel1.Controls.Add(this.Label12);
            this.Panel1.Controls.Add(this.label9);
            this.Panel1.Controls.Add(this.label8);
            this.Panel1.Controls.Add(this.Chart1);
            this.Panel1.Location = new System.Drawing.Point(465, 141);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(274, 148);
            this.Panel1.TabIndex = 31;
            // 
            // labErrCount3
            // 
            this.labErrCount3.AutoSize = true;
            this.labErrCount3.BackColor = System.Drawing.Color.White;
            this.labErrCount3.ForeColor = System.Drawing.Color.Blue;
            this.labErrCount3.Location = new System.Drawing.Point(220, 127);
            this.labErrCount3.Name = "labErrCount3";
            this.labErrCount3.Size = new System.Drawing.Size(35, 14);
            this.labErrCount3.TabIndex = 38;
            this.labErrCount3.Text = "----";
            this.labErrCount3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labErrCount2
            // 
            this.labErrCount2.AutoSize = true;
            this.labErrCount2.BackColor = System.Drawing.Color.White;
            this.labErrCount2.ForeColor = System.Drawing.Color.Blue;
            this.labErrCount2.Location = new System.Drawing.Point(220, 103);
            this.labErrCount2.Name = "labErrCount2";
            this.labErrCount2.Size = new System.Drawing.Size(35, 14);
            this.labErrCount2.TabIndex = 36;
            this.labErrCount2.Text = "----";
            this.labErrCount2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labErrCount1
            // 
            this.labErrCount1.AutoSize = true;
            this.labErrCount1.BackColor = System.Drawing.Color.White;
            this.labErrCount1.ForeColor = System.Drawing.Color.Blue;
            this.labErrCount1.Location = new System.Drawing.Point(220, 79);
            this.labErrCount1.Name = "labErrCount1";
            this.labErrCount1.Size = new System.Drawing.Size(35, 14);
            this.labErrCount1.TabIndex = 34;
            this.labErrCount1.Text = "----";
            this.labErrCount1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labTotalError
            // 
            this.labTotalError.AutoSize = true;
            this.labTotalError.BackColor = System.Drawing.Color.White;
            this.labTotalError.ForeColor = System.Drawing.Color.Blue;
            this.labTotalError.Location = new System.Drawing.Point(220, 55);
            this.labTotalError.Name = "labTotalError";
            this.labTotalError.Size = new System.Drawing.Size(35, 14);
            this.labTotalError.TabIndex = 32;
            this.labTotalError.Text = "----";
            this.labTotalError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labErrName3
            // 
            this.labErrName3.AutoSize = true;
            this.labErrName3.BackColor = System.Drawing.Color.White;
            this.labErrName3.ForeColor = System.Drawing.Color.Black;
            this.labErrName3.Location = new System.Drawing.Point(148, 127);
            this.labErrName3.Name = "labErrName3";
            this.labErrName3.Size = new System.Drawing.Size(56, 14);
            this.labErrName3.TabIndex = 37;
            this.labErrName3.Text = "异常3：";
            this.labErrName3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labErrName2
            // 
            this.labErrName2.AutoSize = true;
            this.labErrName2.BackColor = System.Drawing.Color.White;
            this.labErrName2.ForeColor = System.Drawing.Color.Black;
            this.labErrName2.Location = new System.Drawing.Point(148, 103);
            this.labErrName2.Name = "labErrName2";
            this.labErrName2.Size = new System.Drawing.Size(56, 14);
            this.labErrName2.TabIndex = 35;
            this.labErrName2.Text = "异常2：";
            this.labErrName2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labErrName1
            // 
            this.labErrName1.AutoSize = true;
            this.labErrName1.BackColor = System.Drawing.Color.White;
            this.labErrName1.ForeColor = System.Drawing.Color.Black;
            this.labErrName1.Location = new System.Drawing.Point(148, 79);
            this.labErrName1.Name = "labErrName1";
            this.labErrName1.Size = new System.Drawing.Size(56, 14);
            this.labErrName1.TabIndex = 33;
            this.labErrName1.Text = "异常1：";
            this.labErrName1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.BackColor = System.Drawing.Color.White;
            this.Label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label12.Location = new System.Drawing.Point(148, 55);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(77, 14);
            this.Label12.TabIndex = 31;
            this.Label12.Text = "总异常点：";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // butClearCondi
            // 
            this.butClearCondi.ForeColor = System.Drawing.Color.Black;
            this.butClearCondi.Location = new System.Drawing.Point(461, 380);
            this.butClearCondi.Name = "butClearCondi";
            this.butClearCondi.Size = new System.Drawing.Size(74, 32);
            this.butClearCondi.TabIndex = 32;
            this.butClearCondi.Text = "清除选择";
            this.butClearCondi.UseVisualStyleBackColor = true;
            this.butClearCondi.Click += new System.EventHandler(this.butClearCondi_Click);
            // 
            // Button1
            // 
            this.Button1.ForeColor = System.Drawing.Color.Black;
            this.Button1.Location = new System.Drawing.Point(165, 394);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(74, 32);
            this.Button1.TabIndex = 33;
            this.Button1.Text = "数据写入";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Visible = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(8, 473);
            this.TextBox1.Multiline = true;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(753, 100);
            this.TextBox1.TabIndex = 35;
            // 
            // labFaultTime
            // 
            this.labFaultTime.BackColor = System.Drawing.Color.Transparent;
            this.labFaultTime.ForeColor = System.Drawing.Color.Blue;
            this.labFaultTime.Location = new System.Drawing.Point(562, 357);
            this.labFaultTime.Name = "labFaultTime";
            this.labFaultTime.Size = new System.Drawing.Size(174, 17);
            this.labFaultTime.TabIndex = 38;
            this.labFaultTime.Text = "--天--时--分--秒";
            this.labFaultTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labWaitTime
            // 
            this.labWaitTime.BackColor = System.Drawing.Color.Transparent;
            this.labWaitTime.ForeColor = System.Drawing.Color.Blue;
            this.labWaitTime.Location = new System.Drawing.Point(562, 337);
            this.labWaitTime.Name = "labWaitTime";
            this.labWaitTime.Size = new System.Drawing.Size(174, 17);
            this.labWaitTime.TabIndex = 39;
            this.labWaitTime.Text = "--天--时--分--秒";
            this.labWaitTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label16
            // 
            this.Label16.BackColor = System.Drawing.Color.Transparent;
            this.Label16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label16.Location = new System.Drawing.Point(459, 357);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(110, 17);
            this.Label16.TabIndex = 36;
            this.Label16.Text = "生产故障时间：";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label17
            // 
            this.Label17.BackColor = System.Drawing.Color.Transparent;
            this.Label17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label17.Location = new System.Drawing.Point(459, 337);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(110, 17);
            this.Label17.TabIndex = 37;
            this.Label17.Text = "设备待料时间：";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bgwExport
            // 
            this.bgwExport.WorkerReportsProgress = true;
            this.bgwExport.WorkerSupportsCancellation = true;
            this.bgwExport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwExport_DoWork);
            this.bgwExport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwExport_ProgressChanged);
            this.bgwExport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwExport_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(265, 177);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(253, 23);
            this.progressBar1.TabIndex = 40;
            this.progressBar1.Visible = false;
            // 
            // DataBase
            // 
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.labFaultTime);
            this.Controls.Add(this.labWaitTime);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.cmbMode);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.butClearCondi);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.butDeleteDB);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.butLinkDB);
            this.Controls.Add(this.labRealTime);
            this.Controls.Add(this.labTotalTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpFinish);
            this.Controls.Add(this.cmbBarCode);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbProduct);
            this.Controls.Add(this.cmbEquipID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.butDataQuery);
            this.Controls.Add(this.butToFile);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label119);
            this.Controls.Add(this.label120);
            this.Controls.Add(this.DataGridView1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "DataBase";
            this.Size = new System.Drawing.Size(744, 447);
            this.Load += new System.EventHandler(this.DataBase_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

       
        
	}
}
