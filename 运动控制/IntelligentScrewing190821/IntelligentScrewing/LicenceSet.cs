using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntelligentScrewing
{
    public partial class LicenceSet : Form
    {
        public LicenceSet()
        {
            InitializeComponent();
        }
        public bool check = false;
        private void butCancel_Click(object sender, EventArgs e)
        {
            check = false;
            this.Close();
        }

        private void butAck_Click(object sender, EventArgs e)
        {
            MainForm mf = (MainForm)this.Owner;
            int re= mf.lic.SetLicence(txtCheckNum.Text, mf.patchnum, Frm_welcome.ver, mf.redata2, ref mf.wrdata);
            if(re<0)
            {
                MessageBox.Show("授权码长度错误或授权失败！");
                return;
            }
            check = true;
            this.Close();
            
        }

        private void LicenceSet_Load(object sender, EventArgs e)
        {
             MainForm mf = (MainForm)this.Owner;
             txtServerID.Text= mf.lic.GetSN("HCP", mf.patchnum, Frm_welcome.ver, mf.redata2);
        }

        private void txtServerID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCheckNum_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
