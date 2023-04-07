
namespace Testdemo.UserControls
{
    partial class subeditdatacontrol
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lb_name = new System.Windows.Forms.Label();
            this.lb_unit = new System.Windows.Forms.Label();
            this.tb_data = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.42857F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel1.Controls.Add(this.lb_name, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lb_unit, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_data, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(319, 36);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // lb_name
            // 
            this.lb_name.AutoSize = true;
            this.lb_name.Location = new System.Drawing.Point(5, 2);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(43, 17);
            this.lb_name.TabIndex = 0;
            this.lb_name.Text = "label1";
            // 
            // lb_unit
            // 
            this.lb_unit.AutoSize = true;
            this.lb_unit.Location = new System.Drawing.Point(265, 2);
            this.lb_unit.Name = "lb_unit";
            this.lb_unit.Size = new System.Drawing.Size(43, 17);
            this.lb_unit.TabIndex = 2;
            this.lb_unit.Text = "label1";
            // 
            // tb_data
            // 
            this.tb_data.Location = new System.Drawing.Point(190, 5);
            this.tb_data.Name = "tb_data";
            this.tb_data.Size = new System.Drawing.Size(67, 23);
            this.tb_data.TabIndex = 3;
            this.tb_data.Text = "???";
            this.tb_data.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tb_data_MouseClick);
            // 
            // subeditdatacontrol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "subeditdatacontrol";
            this.Size = new System.Drawing.Size(328, 42);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.Label lb_unit;
        private System.Windows.Forms.TextBox tb_data;
    }
}
