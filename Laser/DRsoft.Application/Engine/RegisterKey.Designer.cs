namespace Engine
{
    partial class RegisterKey
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtSysID = new System.Windows.Forms.TextBox();
            this.btnAuth = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.DecryLicense = new System.Windows.Forms.TextBox();
            this.LicenseActive = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前平台系统ID：";
            // 
            // txtSysID
            // 
            this.txtSysID.Location = new System.Drawing.Point(309, 17);
            this.txtSysID.Name = "txtSysID";
            this.txtSysID.ReadOnly = true;
            this.txtSysID.Size = new System.Drawing.Size(426, 39);
            this.txtSysID.TabIndex = 2;
            // 
            // btnAuth
            // 
            this.btnAuth.Location = new System.Drawing.Point(400, 73);
            this.btnAuth.Name = "btnAuth";
            this.btnAuth.Size = new System.Drawing.Size(245, 46);
            this.btnAuth.TabIndex = 5;
            this.btnAuth.Text = "生成授权申请文件";
            this.btnAuth.UseVisualStyleBackColor = true;
            this.btnAuth.Click += new System.EventHandler(this.btnAuth_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 32);
            this.label3.TabIndex = 6;
            this.label3.Text = "请输入授权密钥:";
            // 
            // DecryLicense
            // 
            this.DecryLicense.Location = new System.Drawing.Point(309, 136);
            this.DecryLicense.Multiline = true;
            this.DecryLicense.Name = "DecryLicense";
            this.DecryLicense.Size = new System.Drawing.Size(426, 154);
            this.DecryLicense.TabIndex = 7;
            // 
            // LicenseActive
            // 
            this.LicenseActive.Location = new System.Drawing.Point(400, 308);
            this.LicenseActive.Name = "LicenseActive";
            this.LicenseActive.Size = new System.Drawing.Size(245, 46);
            this.LicenseActive.TabIndex = 8;
            this.LicenseActive.Text = "授权激活";
            this.LicenseActive.UseVisualStyleBackColor = true;
            this.LicenseActive.Click += new System.EventHandler(this.LicenseActive_Click);
            // 
            // RegisterKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 372);
            this.Controls.Add(this.LicenseActive);
            this.Controls.Add(this.DecryLicense);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAuth);
            this.Controls.Add(this.txtSysID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "RegisterKey";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "帝尔激光授权文件申请";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSysID;
        private System.Windows.Forms.Button btnAuth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DecryLicense;
        private System.Windows.Forms.Button LicenseActive;
    }
}