using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testdemo.Class;
using System.Threading;

namespace Testdemo.UserControls
{
    public partial class StatusDisplay : UserControl
    {
        public List<keepstatusclass> keepstatus = new List<keepstatusclass>();
        List<area> areas; List<Motorarea> motorareas;
        string _pagename;
        string _ip;
        public bool isconnect = false;

        public bool getconnect()
        {
            if (!communicationClass.isconnect)
            {
                //communicationClass = new HslCommunicationClass(_ip);
                //communicationClass.Connect();
                //if (!communicationClass.isconnect)
                isconnect = false;
                //else
                //{
                //    isconnect = true;
                //    beginget();
                //}
            }
            else
            {
                isconnect = true;
            }
            return isconnect;
        }
        public StatusDisplay(string ip, string pagename, bool connect)
        {
            InitializeComponent();
            _pagename = pagename;
            _ip = ip;
            loaddata();

            if (connect)
            {
                communicationClass = new HslCommunicationClass(ip);
                communicationClass.Connect();
                if (!communicationClass.isconnect)
                {
                    //MessageBox.Show("connect fail");
                    isconnect = false;
                    return;
                }
                else
                {
                    isconnect = true;
                    beginget();
                }
            }
            //labStatus[i].ForeColor = dataChange.GetBitValue(PlcDataLowBit, i) ? Color.LimeGreen : Color.Gray;
        }

        public void loaddata()
        {
            areas = Class.ReadXML.GetXml(_pagename);
            if (_pagename == "状态显示")//定制电机拖拉界面
                motorareas = Class.ReadXML.GetMotorXml();

            for (int i = 0; i < areas.Count; i++)
            {
                TabPage tabpage = new TabPage();

                tabpage.Text = areas[i].name;
                for (int j = 0; j < areas[i].points.Count; j++)
                {
                    string controlname = "";
                    switch (_pagename)
                    {
                        case "IO监控":
                            subdisplaycontrol con = new subdisplaycontrol(areas[i].points[j].addr, areas[i].points[j].name);
                            con.Name = subdisplaycontrol.newname();
                            controlname = con.Name;
                            if (j % 2 == 0)
                                con.Location = new System.Drawing.Point(1, j / 2 * 37);
                            else
                                con.Location = new System.Drawing.Point(330, j / 2 * 37);
                            tabpage.Controls.Add(con);
                            break;
                        case "状态显示":
                            subopenclosecontrol con2 = new subopenclosecontrol(areas[i].points[j].addr, areas[i].points[j].name);
                            con2.SendCmd += sendcmd;
                            con2.Name = subopenclosecontrol.newname();
                            controlname = con2.Name;
                            if (j % 2 == 0)
                                con2.Location = new System.Drawing.Point(1, j / 2 * 37);
                            else
                                con2.Location = new System.Drawing.Point(330, j / 2 * 37);
                            tabpage.Controls.Add(con2);
                            break;
                        case "参数配置":
                            subeditdatacontrol con3 = new subeditdatacontrol(areas[i].points[j].addr, areas[i].points[j].name, "???", areas[i].points[j].unit, "参数配置");
                            con3.SendCmd += sendcmd;
                            con3.Name = subeditdatacontrol.newname();
                            controlname = con3.Name;
                            if (j % 2 == 0)
                                con3.Location = new System.Drawing.Point(1, j / 2 * 37);
                            else
                                con3.Location = new System.Drawing.Point(330, j / 2 * 37);
                            tabpage.Controls.Add(con3);
                            break;

                    }

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
            if (motorareas != null)
                for (int i = 0; i < motorareas.Count; i++)
                {
                    TabPage tabpage = new TabPage();

                    tabpage.Text = motorareas[i].name;

                    substarcktitle subtitle = new substarcktitle();
                    subtitle.Location = new System.Drawing.Point(1, 0);
                    tabpage.Controls.Add(subtitle);
                    subtitle.Location = new System.Drawing.Point(330, 0);
                    substarcktitle subtitle2 = new substarcktitle();
                    tabpage.Controls.Add(subtitle2);

                    for (int j = 0; j < motorareas[i].points.Count; j++)
                    {
                        string controlname = "";
                        subtrackbarcontrol con = new subtrackbarcontrol(motorareas[i].points[j].addr, motorareas[i].points[j].addrspeed, motorareas[i].points[j].addrlocation, motorareas[i].points[j].standard);
                        con.SendCmd += sendcmd;
                        con.Name = subtrackbarcontrol.newname();
                        controlname = con.Name;
                        if (j % 2 == 0)
                            con.Location = new System.Drawing.Point(1, (j / 2 + 1) * 37);
                        else
                            con.Location = new System.Drawing.Point(330, (j / 2 + 1) * 37);
                        tabpage.Controls.Add(con);

                        keepstatusclass kclass = new keepstatusclass();
                        kclass.controlname = controlname;
                        kclass.area = motorareas[i].name;
                        kclass.addr = motorareas[i].points[j].addr;
                        kclass.displayname = motorareas[i].points[j].name;
                        kclass.addrspeed = motorareas[i].points[j].addrspeed;
                        kclass.addrlocation = motorareas[i].points[j].addrlocation;
                        kclass.standard = motorareas[i].points[j].standard;
                        keepstatus.Add(kclass);
                    }
                    tabControl1.Controls.Add(tabpage);
                }
        }

        public void sendcmd(object sender, addrint e)
        {
            if (!isconnect || !communicationClass.isconnect)
            {
                MessageBox.Show("connect fail!");
            }
            else
            {
                string msg = "";
                communicationClass.WriteValue(e.addr, e.value, out msg);
            }
        }

        public void sendcmd(object sender, addrdouble e)
        {
            if (!isconnect || !communicationClass.isconnect)
            {
                MessageBox.Show("connect fail!");
            }
            else
            {
                string msg = "";
                communicationClass.WriteValue(e.addr, e.value, out msg);
            }
        }

        //get area num
        public int getareanum(string area)
        {
            for (int i = 0; i < areas.Count; i++)
            {
                if (area == areas[i].name)
                    return i;
            }
            for (int i = 0; i < motorareas.Count; i++)
            {
                if (area == motorareas[i].name)
                    return i + areas.Count;
            }
            return 0;
        }
        //get control
        public subdisplaycontrol getdisplaycontrol(string area, string controlname)
        {
            TabPage tabpage = tabControl1.TabPages[getareanum(area)];
            foreach (subdisplaycontrol subcontrol in tabpage.Controls)
            {
                if (subcontrol.Name == controlname)
                    return subcontrol;
            }
            return null;
        }

        public subtrackbarcontrol gettrackcontrol(string area, string controlname)
        {
            TabPage tabpage = tabControl1.TabPages[getareanum(area)];
            foreach (Control subcontrol in tabpage.Controls)
            {
                if (subcontrol.Name != null && subcontrol.Name != "substarcktitle")
                {
                    subtrackbarcontrol subcontrolnow = subcontrol as subtrackbarcontrol;
                    if (subcontrolnow.Name == controlname)
                        return subcontrolnow;
                }
            }
            return null;
        }

        public subopenclosecontrol getopenclosecontrol(string area, string controlname)
        {
            TabPage tabpage = tabControl1.TabPages[getareanum(area)];
            foreach (subopenclosecontrol subcontrol in tabpage.Controls)
            {
                if (subcontrol.Name == controlname)
                    return subcontrol;
            }
            return null;
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

        System.Threading.Thread thread;
        System.Windows.Forms.Timer time;
        private delegate void FlushClient();
        HslCommunicationClass communicationClass;
        //thread here
        public void beginget()
        {
            time = new System.Windows.Forms.Timer();
            time.Interval = int.Parse(Testdemo.Class.ReadXML.getkeybyname("状态显示刷新间隔"));
            time.Tick += delegate
            {
                ThreadStart obj = new System.Threading.ThreadStart(startlisten);
                thread = new Thread(obj);
                thread.Start();
            };
            time.Start();
        }

        public void run()
        {
            if (communicationClass.isconnect)
                time.Start();
        }

        public void stop()
        {
            if (communicationClass != null && communicationClass.isconnect)
                time.Stop();
        }
        public void startlisten()
        {
            if (communicationClass.isconnect)
            {
                FlushClient fc = new FlushClient(substartlisten);
                this.Invoke(fc);
            }
        }

        public void substartlisten()
        {

            for (int i = 0; i < keepstatus.Count; i++)
            {
                int back = 0; string msg = string.Empty;
                if (!communicationClass.isconnect)
                {
                    if (keepstatus[i].controlname.IndexOf("track") >= 0)
                    {
                        gettrackcontrol(keepstatus[i].area, keepstatus[i].controlname).changgedataspeed("???");
                        gettrackcontrol(keepstatus[i].area, keepstatus[i].controlname).changgedatalocation("???");
                    }
                    else if (_pagename == "IO监控")
                    {
                        getdisplaycontrol(keepstatus[i].area, keepstatus[i].controlname).changgestatus("●");
                    }
                    else if (_pagename == "状态显示")
                    {
                        getopenclosecontrol(keepstatus[i].area, keepstatus[i].controlname).changgestatus("disconect");
                    }
                    else if (_pagename == "参数配置")
                        geteditcontrol(keepstatus[i].area, keepstatus[i].controlname).changgedata("???");
                }
                else
                {
                    communicationClass.ReadValue(keepstatus[i].addr, out back, out msg);

                    if (keepstatus[i].controlname.IndexOf("track") >= 0)
                    {
                        if (keepstatus[i].controlname.Substring(5, keepstatus[i].controlname.Length - 5) == back.ToString())
                            gettrackcontrol(keepstatus[i].area, keepstatus[i].controlname).changgeselect(true);
                        else
                            gettrackcontrol(keepstatus[i].area, keepstatus[i].controlname).changgeselect(false);

                        communicationClass.ReadValue(keepstatus[i].addrspeed, out back, out msg);
                        double backfloat = 0.1;
                        communicationClass.ReadValue(keepstatus[i].addrlocation, out backfloat, out msg);
                        gettrackcontrol(keepstatus[i].area, keepstatus[i].controlname).changgedataspeed(back.ToString());
                        gettrackcontrol(keepstatus[i].area, keepstatus[i].controlname).changgedatalocation(backfloat.ToString());
                    }
                    else if (_pagename == "IO监控")
                    {
                        if (back == 1)
                            getdisplaycontrol(keepstatus[i].area, keepstatus[i].controlname).changgestatus("√");
                        else if (back == 0)
                            getdisplaycontrol(keepstatus[i].area, keepstatus[i].controlname).changgestatus("×");
                    }
                    else if (_pagename == "状态显示")
                    {
                        if (back == 1)
                            getopenclosecontrol(keepstatus[i].area, keepstatus[i].controlname).changgestatus("open");
                        else if (back == 0)
                            getopenclosecontrol(keepstatus[i].area, keepstatus[i].controlname).changgestatus("close");
                    }
                    else if (_pagename == "参数配置")
                        geteditcontrol(keepstatus[i].area, keepstatus[i].controlname).changgedata(back.ToString());


                }

            }
            if (!communicationClass.isconnect)
            {
                for (int i = 0; i < keepstatus.Count; i++)
                {
                    int back = 0; string msg = string.Empty;
                    if (!communicationClass.isconnect)
                    {
                        if (keepstatus[i].controlname.IndexOf("track") >= 0)
                        {
                            gettrackcontrol(keepstatus[i].area, keepstatus[i].controlname).changgedataspeed("???");
                            gettrackcontrol(keepstatus[i].area, keepstatus[i].controlname).changgedatalocation("???");
                        }
                        else if (_pagename == "IO监控")
                        {
                            getdisplaycontrol(keepstatus[i].area, keepstatus[i].controlname).changgestatus("●");
                        }
                        else if (_pagename == "状态显示")
                        {
                            getopenclosecontrol(keepstatus[i].area, keepstatus[i].controlname).changgestatus("disconect");
                        }
                        else if (_pagename == "参数配置")
                            geteditcontrol(keepstatus[i].area, keepstatus[i].controlname).changgedata("???");
                    }
                }
                stop();
            }
        }

        private void stopget()
        {
            if (time.Enabled)
                time.Stop();
        }


    }



    public class keepstatusclass
    {
        public string controlname;
        public string area;
        public string addr;
        public string displayname;
        public string unit;
        public string status;
        public string addrspeed;
        public string addrlocation;
        public string standard;
    }
}
