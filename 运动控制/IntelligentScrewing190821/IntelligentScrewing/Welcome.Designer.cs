namespace IntelligentScrewing
{
    partial class Frm_welcome
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_welcome));
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.labVer = new System.Windows.Forms.Label();
            this.labInfor = new System.Windows.Forms.Label();
            this.labDeviceName = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.labPatchNum = new System.Windows.Forms.Label();
            this.labComName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // Timer1
            // 
            this.Timer1.Interval = 500;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // labVer
            // 
            this.labVer.AutoSize = true;
            this.labVer.BackColor = System.Drawing.Color.Transparent;
            this.labVer.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labVer.ForeColor = System.Drawing.Color.Navy;
            this.labVer.Location = new System.Drawing.Point(294, 6);
            this.labVer.Name = "labVer";
            this.labVer.Size = new System.Drawing.Size(95, 25);
            this.labVer.TabIndex = 124;
            this.labVer.Text = "V6.1.0.18";
            // 
            // labInfor
            // 
            this.labInfor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labInfor.ForeColor = System.Drawing.Color.Red;
            this.labInfor.Location = new System.Drawing.Point(38, 216);
            this.labInfor.Name = "labInfor";
            this.labInfor.Size = new System.Drawing.Size(324, 28);
            this.labInfor.TabIndex = 123;
            this.labInfor.Text = "labInfor";
            this.labInfor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labDeviceName
            // 
            this.labDeviceName.AutoSize = true;
            this.labDeviceName.BackColor = System.Drawing.Color.Transparent;
            this.labDeviceName.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labDeviceName.ForeColor = System.Drawing.Color.Navy;
            this.labDeviceName.Location = new System.Drawing.Point(9, 4);
            this.labDeviceName.Name = "labDeviceName";
            this.labDeviceName.Size = new System.Drawing.Size(180, 28);
            this.labDeviceName.TabIndex = 122;
            this.labDeviceName.Text = "高速智能锁螺钉机";
            // 
            // picLogo
            // 
            this.picLogo.ErrorImage = ((System.Drawing.Image)(resources.GetObject("picLogo.ErrorImage")));
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(117, 48);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(166, 165);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 121;
            this.picLogo.TabStop = false;
            // 
            // labPatchNum
            // 
            this.labPatchNum.AutoSize = true;
            this.labPatchNum.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPatchNum.ForeColor = System.Drawing.Color.Maroon;
            this.labPatchNum.Location = new System.Drawing.Point(6, 253);
            this.labPatchNum.Name = "labPatchNum";
            this.labPatchNum.Size = new System.Drawing.Size(56, 14);
            this.labPatchNum.TabIndex = 120;
            this.labPatchNum.Text = "SN:1501";
            // 
            // labComName
            // 
            this.labComName.AutoSize = true;
            this.labComName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labComName.ForeColor = System.Drawing.Color.Navy;
            this.labComName.Location = new System.Drawing.Point(157, 253);
            this.labComName.Name = "labComName";
            this.labComName.Size = new System.Drawing.Size(238, 14);
            this.labComName.TabIndex = 119;
            this.labComName.Text = "武汉心浩智能科技有限公司 版权所有";
            this.labComName.Visible = false;
            // 
            // Frm_welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 270);
            this.ControlBox = false;
            this.Controls.Add(this.labVer);
            this.Controls.Add(this.labInfor);
            this.Controls.Add(this.labDeviceName);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.labPatchNum);
            this.Controls.Add(this.labComName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_welcome";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Frm_welcome_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Timer Timer1;
        internal System.Windows.Forms.Label labVer;
        internal System.Windows.Forms.Label labInfor;
        internal System.Windows.Forms.Label labDeviceName;
        internal System.Windows.Forms.PictureBox picLogo;
        internal System.Windows.Forms.Label labPatchNum;
        internal System.Windows.Forms.Label labComName;
    }
}

