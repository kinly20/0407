
namespace ICD.UserControls
{
    partial class subsendcmdcontrol
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
            this.lbaddr = new System.Windows.Forms.Label();
            this.lbname = new System.Windows.Forms.Label();
            this.bt_send = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.37643F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.62357F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.Controls.Add(this.lbaddr, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbname, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.bt_send, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(324, 36);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // lbaddr
            // 
            this.lbaddr.AutoSize = true;
            this.lbaddr.Location = new System.Drawing.Point(5, 2);
            this.lbaddr.Name = "lbaddr";
            this.lbaddr.Size = new System.Drawing.Size(43, 17);
            this.lbaddr.TabIndex = 0;
            this.lbaddr.Text = "label1";
            // 
            // lbname
            // 
            this.lbname.AutoSize = true;
            this.lbname.Location = new System.Drawing.Point(78, 2);
            this.lbname.Name = "lbname";
            this.lbname.Size = new System.Drawing.Size(43, 17);
            this.lbname.TabIndex = 1;
            this.lbname.Text = "label2";
            // 
            // bt_send
            // 
            this.bt_send.Location = new System.Drawing.Point(268, 5);
            this.bt_send.Name = "bt_send";
            this.bt_send.Size = new System.Drawing.Size(48, 23);
            this.bt_send.TabIndex = 2;
            this.bt_send.Text = "Send";
            this.bt_send.UseVisualStyleBackColor = true;
            this.bt_send.Click += new System.EventHandler(this.bt_send_Click);
            // 
            // sendcmdcontrol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "sendcmdcontrol";
            this.Size = new System.Drawing.Size(331, 45);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbaddr;
        private System.Windows.Forms.Label lbname;
        private System.Windows.Forms.Button bt_send;
    }
}
