
namespace ICD
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.bt_page8 = new System.Windows.Forms.Button();
            this.bt_page7 = new System.Windows.Forms.Button();
            this.bt_page6 = new System.Windows.Forms.Button();
            this.bt_page5 = new System.Windows.Forms.Button();
            this.bt_page4 = new System.Windows.Forms.Button();
            this.bt_page3 = new System.Windows.Forms.Button();
            this.bt_page2 = new System.Windows.Forms.Button();
            this.bt_page1 = new System.Windows.Forms.Button();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.panelBelow = new System.Windows.Forms.Panel();
            this.bt_connectstatus = new System.Windows.Forms.Button();
            this.bt_user = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.labData = new System.Windows.Forms.Label();
            this.bt_keyboard = new System.Windows.Forms.Button();
            this.timerBottomScan = new System.Windows.Forms.Timer(this.components);
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelLeft.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.panelBelow.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMinimize.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMinimize.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnMinimize.ForeColor = System.Drawing.Color.White;
            this.btnMinimize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMinimize.Location = new System.Drawing.Point(889, 1);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(1);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(66, 39);
            this.btnMinimize.TabIndex = 41;
            this.btnMinimize.TabStop = false;
            this.btnMinimize.Tag = "11";
            this.btnMinimize.Text = "最小化";
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.TopButton_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.BackColor = System.Drawing.Color.OrangeRed;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExit.Location = new System.Drawing.Point(957, 1);
            this.btnExit.Margin = new System.Windows.Forms.Padding(1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(66, 39);
            this.btnExit.TabIndex = 42;
            this.btnExit.TabStop = false;
            this.btnExit.Tag = "12";
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.TopButton_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.pictureBox1);
            this.panelTop.Controls.Add(this.btnExit);
            this.panelTop.Controls.Add(this.btnMinimize);
            this.panelTop.Location = new System.Drawing.Point(2, 2);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1024, 44);
            this.panelTop.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::ICD.Resource1.logo;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(154, 41);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 43;
            this.pictureBox1.TabStop = false;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.bt_page8);
            this.panelLeft.Controls.Add(this.bt_page7);
            this.panelLeft.Controls.Add(this.bt_page6);
            this.panelLeft.Controls.Add(this.bt_page5);
            this.panelLeft.Controls.Add(this.bt_page4);
            this.panelLeft.Controls.Add(this.bt_page3);
            this.panelLeft.Controls.Add(this.bt_page2);
            this.panelLeft.Controls.Add(this.bt_page1);
            this.panelLeft.Location = new System.Drawing.Point(2, 49);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(92, 724);
            this.panelLeft.TabIndex = 3;
            // 
            // bt_page8
            // 
            this.bt_page8.AccessibleDescription = "7";
            this.bt_page8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bt_page8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_page8.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bt_page8.ForeColor = System.Drawing.Color.Black;
            this.bt_page8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_page8.Location = new System.Drawing.Point(1, 345);
            this.bt_page8.Margin = new System.Windows.Forms.Padding(1);
            this.bt_page8.Name = "bt_page8";
            this.bt_page8.Size = new System.Drawing.Size(83, 39);
            this.bt_page8.TabIndex = 47;
            this.bt_page8.TabStop = false;
            this.bt_page8.Text = "权限设置";
            this.bt_page8.UseVisualStyleBackColor = false;
            this.bt_page8.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // bt_page7
            // 
            this.bt_page7.AccessibleDescription = "6";
            this.bt_page7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bt_page7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_page7.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bt_page7.ForeColor = System.Drawing.Color.Black;
            this.bt_page7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_page7.Location = new System.Drawing.Point(1, 296);
            this.bt_page7.Margin = new System.Windows.Forms.Padding(1);
            this.bt_page7.Name = "bt_page7";
            this.bt_page7.Size = new System.Drawing.Size(83, 39);
            this.bt_page7.TabIndex = 46;
            this.bt_page7.TabStop = false;
            this.bt_page7.Text = "扫码枪";
            this.bt_page7.UseVisualStyleBackColor = false;
            this.bt_page7.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // bt_page6
            // 
            this.bt_page6.AccessibleDescription = "5";
            this.bt_page6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bt_page6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_page6.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bt_page6.ForeColor = System.Drawing.Color.Black;
            this.bt_page6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_page6.Location = new System.Drawing.Point(1, 247);
            this.bt_page6.Margin = new System.Windows.Forms.Padding(1);
            this.bt_page6.Name = "bt_page6";
            this.bt_page6.Size = new System.Drawing.Size(83, 39);
            this.bt_page6.TabIndex = 45;
            this.bt_page6.TabStop = false;
            this.bt_page6.Text = "生产记录";
            this.bt_page6.UseVisualStyleBackColor = false;
            this.bt_page6.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // bt_page5
            // 
            this.bt_page5.AccessibleDescription = "4";
            this.bt_page5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bt_page5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_page5.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bt_page5.ForeColor = System.Drawing.Color.Black;
            this.bt_page5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_page5.Location = new System.Drawing.Point(1, 198);
            this.bt_page5.Margin = new System.Windows.Forms.Padding(1);
            this.bt_page5.Name = "bt_page5";
            this.bt_page5.Size = new System.Drawing.Size(83, 39);
            this.bt_page5.TabIndex = 44;
            this.bt_page5.TabStop = false;
            this.bt_page5.Text = "运动控制";
            this.bt_page5.UseVisualStyleBackColor = false;
            this.bt_page5.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // bt_page4
            // 
            this.bt_page4.AccessibleDescription = "3";
            this.bt_page4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bt_page4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_page4.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bt_page4.ForeColor = System.Drawing.Color.Black;
            this.bt_page4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_page4.Location = new System.Drawing.Point(1, 149);
            this.bt_page4.Margin = new System.Windows.Forms.Padding(1);
            this.bt_page4.Name = "bt_page4";
            this.bt_page4.Size = new System.Drawing.Size(83, 39);
            this.bt_page4.TabIndex = 43;
            this.bt_page4.TabStop = false;
            this.bt_page4.Text = "试管设置";
            this.bt_page4.UseVisualStyleBackColor = false;
            this.bt_page4.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // bt_page3
            // 
            this.bt_page3.AccessibleDescription = "2";
            this.bt_page3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bt_page3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_page3.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bt_page3.ForeColor = System.Drawing.Color.Black;
            this.bt_page3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_page3.Location = new System.Drawing.Point(1, 100);
            this.bt_page3.Margin = new System.Windows.Forms.Padding(1);
            this.bt_page3.Name = "bt_page3";
            this.bt_page3.Size = new System.Drawing.Size(83, 39);
            this.bt_page3.TabIndex = 42;
            this.bt_page3.TabStop = false;
            this.bt_page3.Text = "参数配置";
            this.bt_page3.UseVisualStyleBackColor = false;
            this.bt_page3.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // bt_page2
            // 
            this.bt_page2.AccessibleDescription = "1";
            this.bt_page2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bt_page2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_page2.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bt_page2.ForeColor = System.Drawing.Color.Black;
            this.bt_page2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_page2.Location = new System.Drawing.Point(1, 50);
            this.bt_page2.Margin = new System.Windows.Forms.Padding(1);
            this.bt_page2.Name = "bt_page2";
            this.bt_page2.Size = new System.Drawing.Size(83, 39);
            this.bt_page2.TabIndex = 41;
            this.bt_page2.TabStop = false;
            this.bt_page2.Text = "IO监控";
            this.bt_page2.UseVisualStyleBackColor = false;
            this.bt_page2.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // bt_page1
            // 
            this.bt_page1.AccessibleDescription = "0";
            this.bt_page1.BackColor = System.Drawing.Color.GreenYellow;
            this.bt_page1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_page1.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bt_page1.ForeColor = System.Drawing.Color.Black;
            this.bt_page1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_page1.Location = new System.Drawing.Point(1, 0);
            this.bt_page1.Margin = new System.Windows.Forms.Padding(1);
            this.bt_page1.Name = "bt_page1";
            this.bt_page1.Size = new System.Drawing.Size(83, 39);
            this.bt_page1.TabIndex = 40;
            this.bt_page1.TabStop = false;
            this.bt_page1.Text = "状态显示";
            this.bt_page1.UseVisualStyleBackColor = false;
            this.bt_page1.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlMain.Controls.Add(this.tabPage1);
            this.tabControlMain.Controls.Add(this.tabPage2);
            this.tabControlMain.Controls.Add(this.tabPage3);
            this.tabControlMain.Controls.Add(this.tabPage4);
            this.tabControlMain.Controls.Add(this.tabPage5);
            this.tabControlMain.Controls.Add(this.tabPage6);
            this.tabControlMain.Controls.Add(this.tabPage7);
            this.tabControlMain.Controls.Add(this.tabPage8);
            this.tabControlMain.Location = new System.Drawing.Point(69, 45);
            this.tabControlMain.Multiline = true;
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(948, 648);
            this.tabControlMain.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(26, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(918, 640);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(26, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(918, 640);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(26, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(918, 640);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(26, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(918, 640);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(26, 4);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(918, 640);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(26, 4);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(918, 640);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "6";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(26, 4);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(918, 640);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "7";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Location = new System.Drawing.Point(26, 4);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(918, 640);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "8";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // panelBelow
            // 
            this.panelBelow.Controls.Add(this.bt_connectstatus);
            this.panelBelow.Controls.Add(this.bt_user);
            this.panelBelow.Controls.Add(this.button3);
            this.panelBelow.Controls.Add(this.button2);
            this.panelBelow.Controls.Add(this.button1);
            this.panelBelow.Controls.Add(this.labData);
            this.panelBelow.Controls.Add(this.bt_keyboard);
            this.panelBelow.Location = new System.Drawing.Point(95, 699);
            this.panelBelow.Name = "panelBelow";
            this.panelBelow.Size = new System.Drawing.Size(697, 69);
            this.panelBelow.TabIndex = 5;
            // 
            // bt_connectstatus
            // 
            this.bt_connectstatus.AccessibleDescription = "6";
            this.bt_connectstatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_connectstatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bt_connectstatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_connectstatus.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bt_connectstatus.ForeColor = System.Drawing.Color.Black;
            this.bt_connectstatus.Image = global::ICD.Resource1.accept;
            this.bt_connectstatus.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.bt_connectstatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_connectstatus.Location = new System.Drawing.Point(288, 6);
            this.bt_connectstatus.Margin = new System.Windows.Forms.Padding(2);
            this.bt_connectstatus.Name = "bt_connectstatus";
            this.bt_connectstatus.Size = new System.Drawing.Size(67, 54);
            this.bt_connectstatus.TabIndex = 53;
            this.bt_connectstatus.TabStop = false;
            this.bt_connectstatus.Text = "连接状态";
            this.bt_connectstatus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.bt_connectstatus.UseVisualStyleBackColor = false;
            this.bt_connectstatus.Click += new System.EventHandler(this.bt_connectstatus_Click);
            // 
            // bt_user
            // 
            this.bt_user.AccessibleDescription = "6";
            this.bt_user.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_user.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bt_user.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_user.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bt_user.ForeColor = System.Drawing.Color.Black;
            this.bt_user.Image = global::ICD.Resource1.Icon_key;
            this.bt_user.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.bt_user.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_user.Location = new System.Drawing.Point(217, 6);
            this.bt_user.Margin = new System.Windows.Forms.Padding(2);
            this.bt_user.Name = "bt_user";
            this.bt_user.Size = new System.Drawing.Size(67, 54);
            this.bt_user.TabIndex = 52;
            this.bt_user.TabStop = false;
            this.bt_user.Text = "用户登录";
            this.bt_user.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.bt_user.UseVisualStyleBackColor = false;
            this.bt_user.Click += new System.EventHandler(this.bt_user_Click);
            // 
            // button3
            // 
            this.button3.AccessibleDescription = "6";
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("黑体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Image = global::ICD.Resource1.Icon_keyboard;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button3.Location = new System.Drawing.Point(4, 6);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(67, 54);
            this.button3.TabIndex = 51;
            this.button3.TabStop = false;
            this.button3.Text = "键盘";
            this.button3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.AccessibleDescription = "6";
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("黑体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Image = global::ICD.Resource1.Icon_keyboard;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button2.Location = new System.Drawing.Point(75, 6);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 54);
            this.button2.TabIndex = 50;
            this.button2.TabStop = false;
            this.button2.Text = "键盘";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.AccessibleDescription = "6";
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("黑体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Image = global::ICD.Resource1.Icon_keyboard;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(146, 6);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 54);
            this.button1.TabIndex = 49;
            this.button1.TabStop = false;
            this.button1.Text = "键盘";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // labData
            // 
            this.labData.AutoSize = true;
            this.labData.BackColor = System.Drawing.Color.LightCyan;
            this.labData.Font = new System.Drawing.Font("微软雅黑", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labData.ForeColor = System.Drawing.Color.Black;
            this.labData.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labData.Location = new System.Drawing.Point(459, 27);
            this.labData.Margin = new System.Windows.Forms.Padding(1);
            this.labData.Name = "labData";
            this.labData.Size = new System.Drawing.Size(104, 24);
            this.labData.TabIndex = 48;
            this.labData.Text = "2019-01-01";
            this.labData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_keyboard
            // 
            this.bt_keyboard.AccessibleDescription = "6";
            this.bt_keyboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_keyboard.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bt_keyboard.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_keyboard.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bt_keyboard.ForeColor = System.Drawing.Color.Black;
            this.bt_keyboard.Image = global::ICD.Resource1.Icon_keyboard;
            this.bt_keyboard.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.bt_keyboard.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_keyboard.Location = new System.Drawing.Point(359, 6);
            this.bt_keyboard.Margin = new System.Windows.Forms.Padding(2);
            this.bt_keyboard.Name = "bt_keyboard";
            this.bt_keyboard.Size = new System.Drawing.Size(67, 54);
            this.bt_keyboard.TabIndex = 47;
            this.bt_keyboard.TabStop = false;
            this.bt_keyboard.Text = "键盘";
            this.bt_keyboard.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.bt_keyboard.UseVisualStyleBackColor = false;
            this.bt_keyboard.Click += new System.EventHandler(this.BtnKeyboard_Click);
            // 
            // timerBottomScan
            // 
            this.timerBottomScan.Enabled = true;
            this.timerBottomScan.Tick += new System.EventHandler(this.TimerBottomScan_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panelBelow);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelLeft.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.panelBelow.ResumeLayout(false);
            this.panelBelow.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panelBelow;
        private System.Windows.Forms.Button bt_page1;
        private System.Windows.Forms.Button bt_page6;
        private System.Windows.Forms.Button bt_page5;
        private System.Windows.Forms.Button bt_page4;
        private System.Windows.Forms.Button bt_page3;
        private System.Windows.Forms.Button bt_page2;
        private System.Windows.Forms.Button bt_page7;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Button bt_keyboard;
        private System.Windows.Forms.Timer timerBottomScan;
        private System.Windows.Forms.Label labData;
        private System.Windows.Forms.Button bt_connectstatus;
        private System.Windows.Forms.Button bt_user;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.Button bt_page8;
    }
}