using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICD.Class;

namespace ICD.UserControls
{
    public partial class Recipe : UserControl
    {
        public List<keepstatusclass> keepstatus = new List<keepstatusclass>();
        HslCommunicationClass communicationClass;
        string _ip;
        public bool isconnect;
        List<area> areas;
        public Recipe(string ip)
        {
            InitializeComponent();
            _ip = ip;
            loaddata();

            //if (!connect)
            //{
            //communicationClass = new HslCommunicationClass(ip);
            //communicationClass.Connect();
            //if (!communicationClass.isconnect)
            //{
            //    isconnect = false;
            //    return;
            //}
            //else
            //{
            //    isconnect = true;
            //}
            //}
        }

        public void loaddata()
        {
            areas = Class.ReadXML.GetXml("配方设置");
            for (int i = 0; i < areas.Count; i++)
            {
                TabPage tabpage = new TabPage();

                tabpage.Text = areas[i].name;
                for (int j = 0; j < areas[i].points.Count; j++)
                {
                    string controlname = "";

                    subeditdatacontrol con3 = new subeditdatacontrol(areas[i].points[j].addr, areas[i].points[j].name, areas[i].points[j].cmd, areas[i].points[j].unit, "配方设置");
                    con3.Name = subeditdatacontrol.newname();
                    controlname = con3.Name;
                    if (j % 2 == 0)
                        con3.Location = new System.Drawing.Point(1, j / 2 * 37);
                    else
                        con3.Location = new System.Drawing.Point(330, j / 2 * 37);
                    tabpage.Controls.Add(con3);



                    keepstatusclass kclass = new keepstatusclass();
                    kclass.controlname = controlname;
                    kclass.area = areas[i].name;
                    kclass.addr = areas[i].points[j].addr;
                    kclass.displayname = areas[i].points[j].name;
                    kclass.unit = areas[i].points[j].unit;
                    kclass.status = "●";
                    keepstatus.Add(kclass);

                }

                tabControl1.Controls.Add(tabpage);

            }
        }

        public int getareanum(string area)
        {
            for (int i = 0; i < areas.Count; i++)
            {
                if (area == areas[i].name)
                    return i;
            }
            return 0;
        }
        public subeditdatacontrol geteditcontrol(string area, string controlname)
        {
            TabPage tabpage = tabControl1.TabPages[getareanum(area)];
            foreach (subeditdatacontrol subcontrol in tabpage.Controls)
            {
                if (subcontrol.Name == controlname)
                    return subcontrol;
            }
            return null;
        }

        private void bt_getnewdata_Click(object sender, EventArgs e)
        {
            bt_getnewdata.Enabled = false;
            communicationClass = new HslCommunicationClass(_ip);
            communicationClass.Connect();
            if (communicationClass.isconnect)
                for (int i = 0; i < keepstatus.Count; i++)
                {
                    double back = 0; string msg = string.Empty;
                    communicationClass.ReadValue(keepstatus[i].addr, out back, out msg);
                    geteditcontrol(keepstatus[i].area, keepstatus[i].controlname).changgedata(back.ToString());
                }
            bt_getnewdata.Enabled = true;
        }

        public void bt_sendcmd_Click(object sender, EventArgs e)
        {

            bt_sendcmd.Enabled = false;
            communicationClass = new HslCommunicationClass(_ip);
            communicationClass.Connect();
            if (communicationClass.isconnect)
            {
                int selectindex = tabControl1.SelectedIndex;
                string area = areas[selectindex].name;

                for (int i = 0; i < areas.Count; i++)
                {
                    if (areas[i].name == area)
                    {
                        areas[i].points.Clear();
                        List<point> points = new List<point>();
                        for (int j = 0; j < keepstatus.Count; j++)
                        {
                            subeditdatacontrol subcon = geteditcontrol(keepstatus[j].area, keepstatus[j].controlname);
                            string areaname = keepstatus[j].area;
                            string addr = keepstatus[j].addr;
                            string name = keepstatus[j].displayname;
                            string data = subcon.getdata();
                            string unit = subcon.getunit();
                            point po = new point();
                            po.addr = addr;
                            po.name = name;
                            po.unit = unit;
                            po.cmd = data;
                            if (areaname == area)
                            {
                                double back = Convert.ToDouble(data); string msg = string.Empty;
                                communicationClass.WriteValue(addr, back, out msg);
                                points.Add(po);
                            }
                            areas[i].points = points;
                        }
                    }
                }
                WriteXML.writexml(areas);
            }
            //List<area> areas = new List<area>();
            //List<point> points = new List<point>();
            //for (int i = 0; i < keepstatus.Count; i++)
            //{
            //    subeditdatacontrol subcon = geteditcontrol(keepstatus[i].area, keepstatus[i].controlname);
            //    string area = keepstatus[i].area;
            //    string addr = keepstatus[i].addr;
            //    string name = keepstatus[i].displayname;
            //    string data  = subcon.getdata(); 
            //    string unit= subcon.getunit();
            //    point po = new point();
            //    po.addr = addr;
            //    po.name = name;
            //    po.unit = unit;
            //    po.cmd = data;
            //    points.Add(po);
            //    if (i == keepstatus.Count-1 || area != keepstatus[i + 1].area)
            //    {
            //        area carea = new area();
            //        carea.name = area;

            //        List<point> points2 = points;
            //        carea.points = points2;
            //        areas.Add(carea);
            //        points.Clear();
            //    }
            //}
            //WriteXML.writexml(areas);



            //communicationClass = new HslCommunicationClass(_ip);
            //communicationClass.Connect();
            //if (communicationClass.isconnect)
            //    for (int i = 0; i < keepstatus.Count; i++)
            //    {
            //        double back = Convert.ToDouble(geteditcontrol(keepstatus[i].area, keepstatus[i].controlname).getdata()); string msg = string.Empty;
            //        communicationClass.WriteValue(keepstatus[i].addr, back, out msg);
            //        //geteditcontrol(keepstatus[i].area, keepstatus[i].controlname).changgedata(back.ToString());
            //    }
            bt_sendcmd.Enabled = true;
        }
    }
}
