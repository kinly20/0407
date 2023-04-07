namespace IntelligentScrewing
{
    partial class DxfEdit
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
            this.butClose = new System.Windows.Forms.Button();
            this.butEnter = new System.Windows.Forms.Button();
            this.cmbDxfMirror = new System.Windows.Forms.ComboBox();
            this.cmbDxfTurn = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.butCancel = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtProgName = new System.Windows.Forms.TextBox();
            this.ckb_table1 = new System.Windows.Forms.CheckBox();
            this.ckb_table2 = new System.Windows.Forms.CheckBox();
            this.butKeyPanle = new System.Windows.Forms.Button();
            this.cmbLineMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // butClose
            // 
            this.butClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.butClose.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butClose.Location = new System.Drawing.Point(269, 9);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(74, 32);
            this.butClose.TabIndex = 110;
            this.butClose.Text = "关  闭";
            this.butClose.UseVisualStyleBackColor = false;
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // butEnter
            // 
            this.butEnter.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butEnter.Location = new System.Drawing.Point(269, 90);
            this.butEnter.Name = "butEnter";
            this.butEnter.Size = new System.Drawing.Size(74, 32);
            this.butEnter.TabIndex = 109;
            this.butEnter.Text = "确  认";
            this.butEnter.UseVisualStyleBackColor = true;
            this.butEnter.Click += new System.EventHandler(this.butEnter_Click);
            // 
            // cmbDxfMirror
            // 
            this.cmbDxfMirror.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDxfMirror.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDxfMirror.FormattingEnabled = true;
            this.cmbDxfMirror.Items.AddRange(new object[] {
            "无",
            "以Y轴镜像(X坐标取反）",
            "以X轴镜像(Y坐标取反）",
            "XY都镜像(X取反Y取反)"});
            this.cmbDxfMirror.Location = new System.Drawing.Point(103, 94);
            this.cmbDxfMirror.Name = "cmbDxfMirror";
            this.cmbDxfMirror.Size = new System.Drawing.Size(148, 24);
            this.cmbDxfMirror.TabIndex = 108;
            // 
            // cmbDxfTurn
            // 
            this.cmbDxfTurn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDxfTurn.Enabled = false;
            this.cmbDxfTurn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDxfTurn.FormattingEnabled = true;
            this.cmbDxfTurn.Items.AddRange(new object[] {
            "不旋转",
            "旋转90度",
            "旋转180度",
            "旋转270度"});
            this.cmbDxfTurn.Location = new System.Drawing.Point(103, 53);
            this.cmbDxfTurn.Name = "cmbDxfTurn";
            this.cmbDxfTurn.Size = new System.Drawing.Size(148, 24);
            this.cmbDxfTurn.TabIndex = 107;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label2.Location = new System.Drawing.Point(21, 56);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(88, 16);
            this.Label2.TabIndex = 106;
            this.Label2.Text = "图形旋转：";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label1.Location = new System.Drawing.Point(21, 97);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(88, 16);
            this.Label1.TabIndex = 105;
            this.Label1.Text = "图形镜像：";
            // 
            // butCancel
            // 
            this.butCancel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butCancel.Location = new System.Drawing.Point(269, 49);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(74, 32);
            this.butCancel.TabIndex = 104;
            this.butCancel.Text = "取  消";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label4.Location = new System.Drawing.Point(21, 16);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(88, 16);
            this.Label4.TabIndex = 103;
            this.Label4.Text = "产品名称：";
            // 
            // txtProgName
            // 
            this.txtProgName.Location = new System.Drawing.Point(103, 12);
            this.txtProgName.Name = "txtProgName";
            this.txtProgName.Size = new System.Drawing.Size(148, 23);
            this.txtProgName.TabIndex = 111;
            // 
            // ckb_table1
            // 
            this.ckb_table1.AutoSize = true;
            this.ckb_table1.Checked = true;
            this.ckb_table1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_table1.Location = new System.Drawing.Point(48, 176);
            this.ckb_table1.Name = "ckb_table1";
            this.ckb_table1.Size = new System.Drawing.Size(61, 18);
            this.ckb_table1.TabIndex = 112;
            this.ckb_table1.Text = "平台1";
            this.ckb_table1.UseVisualStyleBackColor = true;
            // 
            // ckb_table2
            // 
            this.ckb_table2.AutoSize = true;
            this.ckb_table2.Location = new System.Drawing.Point(154, 176);
            this.ckb_table2.Name = "ckb_table2";
            this.ckb_table2.Size = new System.Drawing.Size(61, 18);
            this.ckb_table2.TabIndex = 113;
            this.ckb_table2.Text = "平台2";
            this.ckb_table2.UseVisualStyleBackColor = true;
            // 
            // butKeyPanle
            // 
            this.butKeyPanle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butKeyPanle.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.butKeyPanle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butKeyPanle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butKeyPanle.ForeColor = System.Drawing.Color.Green;
            this.butKeyPanle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butKeyPanle.Location = new System.Drawing.Point(269, 135);
            this.butKeyPanle.Name = "butKeyPanle";
            this.butKeyPanle.Size = new System.Drawing.Size(74, 31);
            this.butKeyPanle.TabIndex = 272;
            this.butKeyPanle.Text = "键盘";
            this.butKeyPanle.UseVisualStyleBackColor = false;
            this.butKeyPanle.Click += new System.EventHandler(this.butKeyPanle_Click);
            // 
            // cmbLineMode
            // 
            this.cmbLineMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLineMode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbLineMode.FormattingEnabled = true;
            this.cmbLineMode.Items.AddRange(new object[] {
            "圆",
            "直线"});
            this.cmbLineMode.Location = new System.Drawing.Point(103, 139);
            this.cmbLineMode.Name = "cmbLineMode";
            this.cmbLineMode.Size = new System.Drawing.Size(148, 24);
            this.cmbLineMode.TabIndex = 274;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(21, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 273;
            this.label3.Text = "解析模式：";
            // 
            // DxfEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 206);
            this.Controls.Add(this.cmbLineMode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.butKeyPanle);
            this.Controls.Add(this.ckb_table2);
            this.Controls.Add(this.ckb_table1);
            this.Controls.Add(this.txtProgName);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.butEnter);
            this.Controls.Add(this.cmbDxfMirror);
            this.Controls.Add(this.cmbDxfTurn);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.Label4);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "DxfEdit";
            this.Text = "DxfEdit";
            this.Load += new System.EventHandler(this.DxfEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button butClose;
        internal System.Windows.Forms.Button butEnter;
        internal System.Windows.Forms.ComboBox cmbDxfMirror;
        internal System.Windows.Forms.ComboBox cmbDxfTurn;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button butCancel;
        internal System.Windows.Forms.Label Label4;
        private System.Windows.Forms.TextBox txtProgName;
        private System.Windows.Forms.CheckBox ckb_table1;
        private System.Windows.Forms.CheckBox ckb_table2;
        internal System.Windows.Forms.Button butKeyPanle;
        internal System.Windows.Forms.ComboBox cmbLineMode;
        internal System.Windows.Forms.Label label3;
    }
}