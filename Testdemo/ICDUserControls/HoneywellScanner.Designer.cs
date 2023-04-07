
namespace ICD.ICDUserControls
{
    partial class HoneywellScanner
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
            this.components = new System.ComponentModel.Container();
            this.lboxScan = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.timerTrIg = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnLink = new System.Windows.Forms.Button();
            this.bgwScanner = new System.ComponentModel.BackgroundWorker();
            this.bt_onekeyread = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lboxScan
            // 
            this.lboxScan.FormattingEnabled = true;
            this.lboxScan.ItemHeight = 15;
            this.lboxScan.Location = new System.Drawing.Point(19, 14);
            this.lboxScan.Name = "lboxScan";
            this.lboxScan.Size = new System.Drawing.Size(324, 319);
            this.lboxScan.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(364, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "端口选择:";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("黑体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(466, 102);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 43);
            this.button1.TabIndex = 12;
            this.button1.Text = "扫码测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timerTrIg
            // 
            this.timerTrIg.Enabled = true;
            this.timerTrIg.Tick += new System.EventHandler(this.timerTrig_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(363, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "条码枪IP:";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(446, 32);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(127, 25);
            this.txtIP.TabIndex = 14;
            this.txtIP.Text = "192.168.1.200";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(446, 62);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(127, 25);
            this.txtPort.TabIndex = 15;
            this.txtPort.Text = "23";
            // 
            // btnLink
            // 
            this.btnLink.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLink.Font = new System.Drawing.Font("黑体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLink.Location = new System.Drawing.Point(349, 102);
            this.btnLink.Name = "btnLink";
            this.btnLink.Size = new System.Drawing.Size(107, 43);
            this.btnLink.TabIndex = 16;
            this.btnLink.Text = "连接";
            this.btnLink.UseVisualStyleBackColor = true;
            this.btnLink.Click += new System.EventHandler(this.btnLink_Click);
            // 
            // bgwScanner
            // 
            this.bgwScanner.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwScanner_DoWork);
            // 
            // bt_onekeyread
            // 
            this.bt_onekeyread.Location = new System.Drawing.Point(364, 181);
            this.bt_onekeyread.Name = "bt_onekeyread";
            this.bt_onekeyread.Size = new System.Drawing.Size(134, 33);
            this.bt_onekeyread.TabIndex = 17;
            this.bt_onekeyread.Text = "一键读取条码";
            this.bt_onekeyread.UseVisualStyleBackColor = true;
            this.bt_onekeyread.Click += new System.EventHandler(this.onekeyread_Click);
            // 
            // HoneywellScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bt_onekeyread);
            this.Controls.Add(this.btnLink);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lboxScan);
            this.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HoneywellScanner";
            this.Size = new System.Drawing.Size(586, 350);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lboxScan;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timerTrIg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPort;
        public System.Windows.Forms.Button btnLink;
        private System.ComponentModel.BackgroundWorker bgwScanner;
        private System.Windows.Forms.Button bt_onekeyread;
    }
}
