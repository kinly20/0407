using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ICD.ICDUserControls
{
    public partial class RecordSearch : UserControl
    {
        ICD.Class.MysqlCommonDB db = new Class.MysqlCommonDB();
        public RecordSearch()
        {
            InitializeComponent();
            dateTimePickerStart.Value = System.DateTime.Now.AddDays(-1);
            dateTimePickerEnd.Value = System.DateTime.Now;
        }

        private void bt_query_Click(object sender, EventArgs e)
        {
            DataTable dt = db.SearchData(dateTimePickerStart.Value, dateTimePickerEnd.Value);
            dataGridView1.DataSource = dt;
            int suc = 0; int fail = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["RESULT"].ToString() == "success")
                    suc++;
                else
                    fail++;
            }
            tb_total.Text = (suc + fail).ToString();
            tb_success.Text = (suc).ToString();
            tb_fail.Text = (fail).ToString();
        }

        private void bt_insert_Click(object sender, EventArgs e)//测试
        {
            db.InsertData("1123", "fail", "执行失败");
        }

        private void bt_reset_Click(object sender, EventArgs e)
        {
            dateTimePickerStart.Value = System.DateTime.Now.AddDays(-1);
            dateTimePickerEnd.Value = System.DateTime.Now;
            dataGridView1.DataSource = null;
            tb_total.Text = "0";
            tb_success.Text = "0";
            tb_fail.Text = "0";
        }

        public void btnToFile_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.RowCount <= 0)
            {
                MessageBox.Show("没有报表数据可导出！", "报表导出");
                return;
            }
            this.saveFileDialog1.Filter = "CSV格式(*.csv)|*.csv";
            this.saveFileDialog1.FileName = "ReportExport_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            this.saveFileDialog1.FilterIndex = 2;
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = this.saveFileDialog1.FileName;
                if (!this.ExportData(filePath, false))
                {
                    MessageBox.Show("导出文件失败！");
                }
            }
        }
        public bool ExportData(string Files, bool ClrData = false)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(Files, false, Encoding.Default);
                string str = string.Empty;
                if (this.dataGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i <= this.dataGridView1.Columns.Count - 1; i++)
                    {
                        str = str + this.dataGridView1.Columns[i].HeaderText + ",";
                    }
                    str += "\r\n";
                    for (int j = 0; j <= this.dataGridView1.Rows.Count - 1; j++)
                    {
                        for (int k = 0; k <= this.dataGridView1.ColumnCount - 1; k++)
                        {
                            str = str + this.dataGridView1.Rows[j].Cells[k].Value.ToString() + ",";
                        }
                        str += "\r\n";
                    }
                }
                str += "\r\n";
                writer.Write(str);
                writer.Flush();
                writer.Close();
                if (ClrData)
                {
                    this.dataGridView1.DataSource = null;
                }
                return true;
            }
            catch (Exception)
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
    }
}
