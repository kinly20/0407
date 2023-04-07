
namespace ICD.UserControls
{
    partial class subtrackbarcontrol
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
            this.trackBarspeed = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bt_select = new System.Windows.Forms.Button();
            this.tb_speed = new System.Windows.Forms.TextBox();
            this.tb_location = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarspeed)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBarspeed
            // 
            this.trackBarspeed.Location = new System.Drawing.Point(67, 3);
            this.trackBarspeed.Name = "trackBarspeed";
            this.trackBarspeed.Size = new System.Drawing.Size(117, 24);
            this.trackBarspeed.TabIndex = 0;
            this.trackBarspeed.ValueChanged += new System.EventHandler(this.trackBarspeed_Scroll);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.55497F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.44502F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel1.Controls.Add(this.bt_select, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.trackBarspeed, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_speed, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_location, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(324, 30);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // bt_select
            // 
            this.bt_select.Location = new System.Drawing.Point(3, 3);
            this.bt_select.Name = "bt_select";
            this.bt_select.Size = new System.Drawing.Size(56, 23);
            this.bt_select.TabIndex = 4;
            this.bt_select.Text = "横移轴";
            this.bt_select.UseVisualStyleBackColor = true;
            this.bt_select.Click += new System.EventHandler(this.btselect_Click);
            // 
            // tb_speed
            // 
            this.tb_speed.Enabled = false;
            this.tb_speed.Location = new System.Drawing.Point(190, 3);
            this.tb_speed.Name = "tb_speed";
            this.tb_speed.Size = new System.Drawing.Size(56, 23);
            this.tb_speed.TabIndex = 2;
            // 
            // tb_location
            // 
            this.tb_location.Enabled = false;
            this.tb_location.Location = new System.Drawing.Point(258, 3);
            this.tb_location.Name = "tb_location";
            this.tb_location.Size = new System.Drawing.Size(56, 23);
            this.tb_location.TabIndex = 5;
            // 
            // subtrackbarcontrol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "subtrackbarcontrol";
            this.Size = new System.Drawing.Size(330, 36);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarspeed)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarspeed;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tb_speed;
        private System.Windows.Forms.Button bt_select;
        private System.Windows.Forms.TextBox tb_location;
    }
}
