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
    public partial class OnOffButton : UserControl
    {
        public bool Istrue;
        public event EventHandler<bool> ClickCmd;
        public OnOffButton()
        {
            Istrue = true;
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Istrue = !Istrue;
            if (Istrue)
                this.pictureBox1.Image = global::ICD.Resource1.On;
            else
                this.pictureBox1.Image = global::ICD.Resource1.Off;
            ClickCmd(null, Istrue);
        }
    }
}
