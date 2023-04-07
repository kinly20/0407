
namespace ICD.UserControls
{
    partial class subopenclosecontrol
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
            this.bt_open = new System.Windows.Forms.Button();
            this.bt_close = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.48092F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.51908F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.Controls.Add(this.lb_name, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.bt_open, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.bt_close, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(319, 36);
            this.tableLayoutPanel1.TabIndex = 4;
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
            // bt_open
            // 
            this.bt_open.Location = new System.Drawing.Point(205, 5);
            this.bt_open.Name = "bt_open";
            this.bt_open.Size = new System.Drawing.Size(47, 23);
            this.bt_open.TabIndex = 1;
            this.bt_open.Text = "开";
            this.bt_open.UseVisualStyleBackColor = true;
            this.bt_open.Click += new System.EventHandler(this.bt_open_Click);
            // 
            // bt_close
            // 
            this.bt_close.Location = new System.Drawing.Point(264, 5);
            this.bt_close.Name = "bt_close";
            this.bt_close.Size = new System.Drawing.Size(47, 23);
            this.bt_close.TabIndex = 2;
            this.bt_close.Text = "关";
            this.bt_close.UseVisualStyleBackColor = true;
            this.bt_close.Click += new System.EventHandler(this.bt_close_Click);
            // 
            // subopenclosecontrol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "subopenclosecontrol";
            this.Size = new System.Drawing.Size(328, 42);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.Button bt_open;
        private System.Windows.Forms.Button bt_close;
    }
}
