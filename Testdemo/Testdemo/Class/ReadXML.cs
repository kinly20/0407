using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Testdemo.Class
{
    public class ReadXML
    {
        public static string path = AppDomain.CurrentDomain.BaseDirectory + "xml\\";
        //public static string displayfile = "statusdisplay.xml";
        //public static string sendcmdfile = "sendcmd.xml";
        public static Dictionary<string, string> xmlsettings = new Dictionary<string, string>();


        public static Dictionary<string, string> GetSystemSetting()
        {
            if (xmlsettings.Count > 0)
                return xmlsettings;

            XmlDocument xmlDoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;//忽略文档里面的注释
            string file = "SystemSetting.xml";

            XmlReader reader = XmlReader.Create(path + file, settings);
            xmlDoc.Load(reader);

            XmlNode root = xmlDoc.SelectSingleNode("Setting");
            XmlNodeList list = root.ChildNodes;
            for (int i = 0; i < list.Count; i++)
            {
                string name = list[i].Attributes["Name"].Value;
                string key = list[i].Attributes["Key"].Value;
                xmlsettings.Add(name, key);
            }
            return xmlsettings;
        }

        public static string getkeybyname(string name)
        {
            return GetSystemSetting()[name];
        }
        public static List<area> GetXml(string pagename)//状态显示，IO监控，参数配置，报警配置
        {

            XmlDocument xmlDoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;//忽略文档里面的注释
            string file = pagename + ".xml";
            //if (type == "statusdisplay")
            //    file = displayfile;
            //else if (type == "sendcmd")
            //    file = sendcmdfile;

            XmlReader reader = XmlReader.Create(path + file, settings);
            xmlDoc.Load(reader);

            XmlNode root = xmlDoc.SelectSingleNode("list");
            XmlNodeList arealist = root.ChildNodes;
            List<area> areas = new List<area>();
            for (int i = 0; i < arealist.Count; i++)
            {
                area areanow = new area();
                string name = arealist[i].Attributes["Name"].Value; areanow.name = name;
                List<point> points = new List<point>();
                XmlNodeList pointlist = arealist[i].ChildNodes;
                for (int j = 0; j < pointlist.Count; j++)
                {
                    point pointnow = new point();
                    //io监控
                    string pointname = pointlist[j].Attributes["name"].Value; pointnow.name = pointname;
                    string pointaddr = pointlist[j].Attributes["addr"].Value; pointnow.addr = pointaddr;
                    if (pagename == "参数配置" )
                    {
                        string unit = pointlist[j].Attributes["unit"].Value; pointnow.unit = unit;
                    }
                    if ( pagename == "配方设置")
                    {
                        string cmd = pointlist[j].Attributes["cmd"].Value; pointnow.cmd = cmd;
                        string unit = pointlist[j].Attributes["unit"].Value; pointnow.unit = unit;
                    }
                    if (pagename == "命令下发")
                    {
                        string cmd = pointlist[j].Attributes["cmd"].Value; pointnow.cmd = cmd;
                        string cmdtype = pointlist[j].Attributes["cmdtype"].Value; pointnow.cmdtype = cmdtype;
                    }
                    points.Add(pointnow);

                }
                areanow.points = points;
                areas.Add(areanow);
            }

            return areas;
        }

        public static List<Motorarea> GetMotorXml()//状态显示，电机
        {

            XmlDocument xmlDoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;//忽略文档里面的注释
            string file = "电机轴.xml";
            //if (type == "statusdisplay")
            //    file = displayfile;
            //else if (type == "sendcmd")
            //    file = sendcmdfile;

            XmlReader reader = XmlReader.Create(path + file, settings);
            xmlDoc.Load(reader);

            XmlNode root = xmlDoc.SelectSingleNode("list");
            XmlNodeList arealist = root.ChildNodes;
            List<Motorarea> areas = new List<Motorarea>();
            for (int i = 0; i < arealist.Count; i++)
            {
                Motorarea areanow = new Motorarea();
                string name = arealist[i].Attributes["Name"].Value; areanow.name = name;
                List<Motorpoint> points = new List<Motorpoint>();
                XmlNodeList pointlist = arealist[i].ChildNodes;
                for (int j = 0; j < pointlist.Count; j++)
                {
                    Motorpoint pointnow = new Motorpoint();
                    //io监控
                    string pointname = pointlist[j].Attributes["name"].Value; pointnow.name = pointname;
                    string pointaddr = pointlist[j].Attributes["addr"].Value; pointnow.addr = pointaddr;
                    string standard = pointlist[j].Attributes["standard"].Value; pointnow.standard = standard;
                    string addrspeed = pointlist[j].Attributes["addrspeed"].Value; pointnow.addrspeed = addrspeed;
                    string addrlocation = pointlist[j].Attributes["addrlocation"].Value; pointnow.addrlocation = addrlocation;
                    points.Add(pointnow);

                }
                areanow.points = points;
                areas.Add(areanow);
            }

            return areas;
        }
    }

    public class WriteXML
    {
        public static string commonpath = AppDomain.CurrentDomain.BaseDirectory + "xml\\";
        public static void writexml(List<area> areas)
        {
            string path = commonpath + @"配方设置.xml";
            XmlDocument xmlDoc = new XmlDocument();//新建XML文件
            xmlDoc.Load(path);//加载XML文件

            XmlNode xm = xmlDoc.SelectSingleNode("list");
            xm.RemoveAll();
            for (int i = 0; i < areas.Count; i++)
            {
                XmlElement xelKey = xmlDoc.CreateElement("area");

                XmlAttribute xelType = xmlDoc.CreateAttribute("Name");
                xelType.InnerText = areas[i].name;//"配方3"
                xelKey.SetAttributeNode(xelType);


                for (int j = 0; j < areas[i].points.Count; j++)
                {
                    XmlElement xelpoint = xmlDoc.CreateElement("point");

                    XmlAttribute xeladdr = xmlDoc.CreateAttribute("addr");
                    xeladdr.InnerText = areas[i].points[j].addr; //"AW1"
                    xelpoint.SetAttributeNode(xeladdr);
                    XmlAttribute xelName = xmlDoc.CreateAttribute("name");
                    xelName.InnerText = areas[i].points[j].name;//"监控new"
                    xelpoint.SetAttributeNode(xelName);
                    XmlAttribute xelCmd = xmlDoc.CreateAttribute("cmd");
                    xelCmd.InnerText = areas[i].points[j].cmd;//"1"
                    xelpoint.SetAttributeNode(xelCmd);
                    XmlAttribute xelUnit = xmlDoc.CreateAttribute("unit");
                    xelUnit.InnerText = areas[i].points[j].unit; //"ms"
                    xelpoint.SetAttributeNode(xelUnit);

                    xelKey.AppendChild(xelpoint);
                }

                xm.AppendChild(xelKey);

            }
            xmlDoc.Save(path);
        }
    }

    public class area
    {
        public string name;
        public List<point> points;
    }
    public class point
    {
        public string name;
        public string addr;
        public string unit;
        public string cmd;
        public string cmdtype;
    }

    public class Motorarea
    {
        public string name;
        public List<Motorpoint> points;
    }

    public class Motorpoint
    {
        public string name;
        public string addr;
        public string standard;
        public string addrspeed;
        public string addrlocation;
    }
}
