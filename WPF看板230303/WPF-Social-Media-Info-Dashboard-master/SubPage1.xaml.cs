using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard
{
    /// <summary>
    /// SubPage1.xaml 的交互逻辑
    /// </summary>
    public partial class SubPage1 : Page
    {
        public SubPage1()
        {
            InitializeComponent();
            loaddata();
        }


        public void loaddata()
        {

            List<AttributeModel> products = StaticPlc.GetProduct();
            if (products.Count > 0)
            {
                tb_name1.Text = products[0].product;
                tb_p1.Text = products[0].Attribute1;
                tb_p2.Text = products[0].Attribute2;
                tb_p3.Text = products[0].Attribute3;
            }
            if (products.Count > 1)
            {
                tb_name2.Text = products[1].product;
                tb_p4.Text = products[1].Attribute1;
                tb_p5.Text = products[1].Attribute2;
                tb_p6.Text = products[1].Attribute3;
            }
            if (products.Count > 2)
            {
                tb_name3.Text = products[2].product;
                tb_p7.Text = products[2].Attribute1;
                tb_p8.Text = products[2].Attribute2;
                tb_p9.Text = products[2].Attribute3;
            }
        }

    }

    public class AttributeModel
    {
        public string product { get; set; }

        public string Attribute1 { get; set; }

        public string Attribute2 { get; set; }

        public string Attribute3 { get; set; }

    }
}
