namespace Publisher
{
    partial class FormPublisher
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
            this.txt_Message = new System.Windows.Forms.TextBox();
            this.btn_Publish = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_Message
            // 
            this.txt_Message.Location = new System.Drawing.Point(28, 44);
            this.txt_Message.Multiline = true;
            this.txt_Message.Name = "txt_Message";
            this.txt_Message.Size = new System.Drawing.Size(243, 51);
            this.txt_Message.TabIndex = 0;
            // 
            // btn_Publish
            // 
            this.btn_Publish.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Publish.Location = new System.Drawing.Point(279, 44);
            this.btn_Publish.Name = "btn_Publish";
            this.btn_Publish.Size = new System.Drawing.Size(75, 51);
            this.btn_Publish.TabIndex = 1;
            this.btn_Publish.Text = "发布";
            this.btn_Publish.UseVisualStyleBackColor = true;
            this.btn_Publish.Click += new System.EventHandler(this.btn_Publish_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // FormPublisher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 125);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Publish);
            this.Controls.Add(this.txt_Message);
            this.Name = "FormPublisher";
            this.Text = "发布者";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPublisher_FormClosing);
            this.Load += new System.EventHandler(this.FormPublisher_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Message;
        private System.Windows.Forms.Button btn_Publish;
        private System.Windows.Forms.Label label1;
    }
}

