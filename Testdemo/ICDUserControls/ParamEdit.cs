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
    public partial class ParamEdit : UserControl
    {
        public ParamEdit()
        {
            InitializeComponent();//△▽
            onOffButton1.ClickCmd += onOffButton1_Onclick;
            onOffButton2.ClickCmd += onOffButton2_Onclick;
            onOffButton3.ClickCmd += onOffButton3_Onclick;
            onOffButton4.ClickCmd += onOffButton4_Onclick;
            onOffButton5.ClickCmd += onOffButton5_Onclick;
        }

        public void onOffButton1_Onclick(object sender, bool istrue)
        { 

        }

        public void onOffButton2_Onclick(object sender, bool istrue)
        {

        }

        public void onOffButton3_Onclick(object sender, bool istrue)
        {

        }

        public void onOffButton4_Onclick(object sender, bool istrue)
        {

        }

        public void onOffButton5_Onclick(object sender, bool istrue)
        {

        }
    }
}
