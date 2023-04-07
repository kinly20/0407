
namespace Testdemo.UserControls
{
    partial class Recipe
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.bt_getnewdata = new System.Windows.Forms.Button();
            this.bt_sendcmd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(681, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // bt_getnewdata
            // 
            this.bt_getnewdata.Location = new System.Drawing.Point(394, 459);
            this.bt_getnewdata.Name = "bt_getnewdata";
            this.bt_getnewdata.Size = new System.Drawing.Size(125, 30);
            this.bt_getnewdata.TabIndex = 1;
            this.bt_getnewdata.Text = "获取最新数据";
            this.bt_getnewdata.UseVisualStyleBackColor = true;
            this.bt_getnewdata.Click += new System.EventHandler(this.bt_getnewdata_Click);
            // 
            // bt_sendcmd
            // 
            this.bt_sendcmd.Location = new System.Drawing.Point(549, 459);
            this.bt_sendcmd.Name = "bt_sendcmd";
            this.bt_sendcmd.Size = new System.Drawing.Size(125, 30);
            this.bt_sendcmd.TabIndex = 2;
            this.bt_sendcmd.Text = "保存配方并下发";
            this.bt_sendcmd.UseVisualStyleBackColor = true;
            this.bt_sendcmd.Click += new System.EventHandler(this.bt_sendcmd_Click);
            // 
            // Recipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bt_sendcmd);
            this.Controls.Add(this.bt_getnewdata);
            this.Controls.Add(this.tabControl1);
            this.Name = "Recipe";
            this.Size = new System.Drawing.Size(700, 500);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button bt_getnewdata;
        private System.Windows.Forms.Button bt_sendcmd;
    }
}
