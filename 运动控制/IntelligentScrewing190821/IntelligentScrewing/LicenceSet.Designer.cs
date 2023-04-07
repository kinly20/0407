namespace IntelligentScrewing
{
    partial class LicenceSet
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
            this.txtServerID = new System.Windows.Forms.TextBox();
            this.butCancel = new System.Windows.Forms.Button();
            this.butAck = new System.Windows.Forms.Button();
            this.txtCheckNum = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtServerID
            // 
            this.txtServerID.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtServerID.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtServerID.Location = new System.Drawing.Point(158, 7);
            this.txtServerID.Name = "txtServerID";
            this.txtServerID.ReadOnly = true;
            this.txtServerID.Size = new System.Drawing.Size(187, 23);
            this.txtServerID.TabIndex = 12;
            this.txtServerID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtServerID.TextChanged += new System.EventHandler(this.txtServerID_TextChanged);
            // 
            // butCancel
            // 
            this.butCancel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butCancel.Location = new System.Drawing.Point(77, 87);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(71, 32);
            this.butCancel.TabIndex = 11;
            this.butCancel.Text = "取  消";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butAck
            // 
            this.butAck.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butAck.Location = new System.Drawing.Point(233, 87);
            this.butAck.Name = "butAck";
            this.butAck.Size = new System.Drawing.Size(71, 32);
            this.butAck.TabIndex = 10;
            this.butAck.Text = "确  定";
            this.butAck.UseVisualStyleBackColor = true;
            this.butAck.Click += new System.EventHandler(this.butAck_Click);
            // 
            // txtCheckNum
            // 
            this.txtCheckNum.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCheckNum.Location = new System.Drawing.Point(158, 44);
            this.txtCheckNum.Name = "txtCheckNum";
            this.txtCheckNum.Size = new System.Drawing.Size(187, 23);
            this.txtCheckNum.TabIndex = 9;
            this.txtCheckNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCheckNum.TextChanged += new System.EventHandler(this.txtCheckNum_TextChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label2.Location = new System.Drawing.Point(24, 47);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(142, 19);
            this.Label2.TabIndex = 8;
            this.Label2.Text = "请输入授权码：";
            this.Label2.Click += new System.EventHandler(this.Label2_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label1.Location = new System.Drawing.Point(44, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(123, 19);
            this.Label1.TabIndex = 7;
            this.Label1.Text = "软件序列号：";
            this.Label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // LicenceSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 136);
            this.Controls.Add(this.txtServerID);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butAck);
            this.Controls.Add(this.txtCheckNum);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Name = "LicenceSet";
            this.Text = "LicenceSet";
            this.Load += new System.EventHandler(this.LicenceSet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtServerID;
        internal System.Windows.Forms.Button butCancel;
        internal System.Windows.Forms.Button butAck;
        internal System.Windows.Forms.TextBox txtCheckNum;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
    }
}