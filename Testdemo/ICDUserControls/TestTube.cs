using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICD.ICDUserControls
{
    public partial class TestTube : UserControl
    {
        public event EventHandler<picvalue> ClickCmd;
        public picvalue _crupicvalue = picvalue.white;
        public string _name = "A1";
        public bool _edit = true;
        public TestTube()
        {
            InitializeComponent();
        }

        public enum picvalue
        {
            white = 0,
            lightblue = 1,
            darkblue = 2,
            green = 3,
            red = 4
        }

        public void SetName(string name)
        {
            _name = name;
            lb_name.Text = _name;
        }

        public void SetColor(picvalue color)
        {
            _crupicvalue = color;
            switch (_crupicvalue)
            {
                case picvalue.white:
                    this.pictureBox1.Image = global::ICD.Resource1.白色;
                    break;
                case picvalue.lightblue:
                    this.pictureBox1.Image = global::ICD.Resource1.浅蓝;
                    break;
                case picvalue.darkblue:
                    this.pictureBox1.Image = global::ICD.Resource1.深蓝;
                    break;
                case picvalue.green:
                    this.pictureBox1.Image = global::ICD.Resource1.绿色;
                    break;
                case picvalue.red:
                    this.pictureBox1.Image = global::ICD.Resource1.红色;
                    break;

            }

        }

        public void SetEdite(bool edit)
        {
            _edit = edit;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (_edit)
            {
                this.pictureBox1.Image = global::ICD.Resource1.浅蓝;
                ClickCmd(sender, picvalue.lightblue);
            }
        }

        private void lb_name_Click(object sender, EventArgs e)
        {
            if (_edit)
            {
                this.pictureBox1.Image = global::ICD.Resource1.浅蓝;
                ClickCmd(pictureBox1, picvalue.lightblue);
            }
        }
    }
}
