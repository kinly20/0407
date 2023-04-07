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
    public partial class TestTubeSetting : UserControl
    {
        public TestTubeSetting()
        {
            InitializeComponent();
            LoadTestTubeName();
            LoadTestTubeClickAction();
            bt_set.BackColor = Color.GreenYellow;
        }

        public void LoadTestTubeClickAction()
        {

            foreach (Control tro in groupBox2.Controls)
            {
                if (tro.GetType() == typeof(TestTube))
                {
                    TestTube ttro = tro as TestTube;
                    ttro.ClickCmd += TestTubeClickAction;
                }
            }

        }


        public void TestTubeClickAction(object obj, ICD.ICDUserControls.TestTube.picvalue pic)
        {
            PictureBox pbox = obj as PictureBox;
            string name = pbox.Parent.Name.Substring(8, pbox.Parent.Name.Length - 8);

            string ABC = name.Substring(0, 1);
            string num = name.Substring(1, name.Length - 1);

            foreach (Control tro in groupBox2.Controls)
            {
                if (tro.GetType() == typeof(TestTube))
                {
                    TestTube ttro = tro as TestTube;

                    string troname = ttro.Name.Substring(8, ttro.Name.Length - 8);
                    string ABC2 = troname.Substring(0, 1);
                    string num2 = troname.Substring(1, troname.Length - 1);

                    if (int.Parse(num2) > int.Parse(num) || int.Parse(num2) == int.Parse(num) && compareABC(ABC, ABC2))
                    {
                        ttro.SetColor(TestTube.picvalue.lightblue);
                    }
                    //ttro.ClickCmd += TestTubeClickAction;
                }
            }
        }

        public bool compareABC(string A, string B)
        {
            A = A.Replace("A", "1").Replace("B", "2").Replace("C", "3").Replace("D", "4").Replace("E", "5").Replace("F", "6");
            B = B.Replace("A", "1").Replace("B", "2").Replace("C", "3").Replace("D", "4").Replace("E", "5").Replace("F", "6");
            return int.Parse(B) > int.Parse(A);
        }

        public void LoadTestTubeName()
        {
            foreach (Control tro in groupBox2.Controls)
            {
                if (tro.GetType() == typeof(TestTube))
                {
                    TestTube ttro = tro as TestTube;//testTubeC4
                    ttro.SetName(tro.Name.Substring(8, tro.Name.Length - 8));
                }
            }

        }

        private void bt_run_Click(object sender, EventArgs e)
        {
            foreach (Control tro in groupBox2.Controls)
            {
                if (tro.GetType() == typeof(TestTube))
                {
                    TestTube ttro = tro as TestTube;//testTubeC4
                    ttro.SetEdite(false);
                }
            }
            bt_run.BackColor = Color.GreenYellow;
                bt_set.BackColor = Color.WhiteSmoke;
        }

        private void bt_set_Click(object sender, EventArgs e)
        {
            foreach (Control tro in groupBox2.Controls)
            {
                if (tro.GetType() == typeof(TestTube))
                {
                    TestTube ttro = tro as TestTube;//testTubeC4
                    ttro.SetEdite(true);
                }
            }
            bt_run.BackColor = Color.WhiteSmoke;
            bt_set.BackColor = Color.GreenYellow;
        }

        private void bt_reset_Click(object sender, EventArgs e)
        {
            foreach (Control tro in groupBox2.Controls)
            {
                if (tro.GetType() == typeof(TestTube))
                {
                    TestTube ttro = tro as TestTube;//testTubeC4
                    ttro.SetEdite(true);
                    ttro.SetColor(TestTube.picvalue.white);
                }
            }
            bt_run.BackColor = Color.WhiteSmoke;
            bt_set.BackColor = Color.GreenYellow;
        }
    }
}
