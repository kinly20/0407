using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThreeAxisTable;

namespace TestTable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ThreeAxisTable.Table table = new ThreeAxisTable.Table();
        DxfParser.DxfShow dxfParser = new DxfParser.DxfShow();
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Controls.Add(table);
            table.Initial();
            //this.Controls.Add(dxfParser);
            
        }

        public bool[] AlmList = new bool[100];
        private bool almflag;
        public bool Almflag
        {
            get
            {
                for (int i = 0; i < AlmList.Length; i++)
                {
                    almflag = almflag & AlmList[i];
                }
                return almflag;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            string inpath = null;
            string outpath = @"D:\123.csv";
            Bitmap bmp = null;
            of.Filter = @"dxf文件（*.dxf)|*.dxf";
            dxfParser.rotation =0;
            DxfParser.DxfShow.xNegation = true;
            DxfParser.DxfShow.yNegation = true;
            if (of.ShowDialog() == DialogResult.OK)
            {
                inpath = of.FileName;
                //dxfParser.transport(inpath, outpath);
                //dxfParser.ShowPic();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //dxfParser.ShowPic(RotateFlipType.RotateNoneFlipNone);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //table.WriteDO(0, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Almflag.ToString());
            //table.WriteDO(2, true);
            //table.WriteDO(Table.DO.ServoA_Speed0, true);
            //table.WriteDO(Table.DO.ServoA_Torque0, true);
            //table.WriteDO(Table.DO.ServoA_Speed1, false);
            //table.WriteDO(Table.DO.ServoA_Torque1, false);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AlmList[5] = true;
            MessageBox.Show(Almflag.ToString());
            //table.WriteDO(2, true);
            //table.WriteDO(Table.DO.ServoA_Speed0, false);
            //table.WriteDO(Table.DO.ServoA_Torque0, false);
            //table.WriteDO(Table.DO.ServoA_Speed1, true);
            //table.WriteDO(Table.DO.ServoA_Torque1, true);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //table.WriteDO(2, true);
            //table.WriteDO(Table.DO.ServoA_Speed0, true);
            //table.WriteDO(Table.DO.ServoA_Torque0, true);
            //table.WriteDO(Table.DO.ServoA_Speed1, true);
            //table.WriteDO(Table.DO.ServoA_Torque1, true);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //table.WriteDO(2, true);
            //table.WriteDO(Table.DO.ServoA_Speed0, false);
            //table.WriteDO(Table.DO.ServoA_Torque0, false);
            //table.WriteDO(Table.DO.ServoA_Speed1, false);
            //table.WriteDO(Table.DO.ServoA_Torque1, false);
           // short temp = Convert.ToInt16("11",IFormatProvider);

        }
       /// <summary>        /// 设定Int数据中某一位的值
        /// </summary>
         /// <param name="value">位设定前的值</param>
         /// <param name="index">32位数据的从右向左的偏移位索引(0~31)</param>
         /// <param name="bitValue">true设该位为1,false设为0</param>
         /// <returns>返回位设定后的值</returns>
       
    }
}
