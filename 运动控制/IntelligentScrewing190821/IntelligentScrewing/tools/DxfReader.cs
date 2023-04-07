using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Drawing;
using System.ComponentModel;
using System.Data;

namespace IntelligentScrewing.tools
{
    public class DxfReader
    {
        private FileStream fs;
        private StreamReader sr;

        private ArrayList EllipseList = new ArrayList();     
        private ArrayList splineList = new ArrayList();
        private ArrayList LayerList = new ArrayList();//图层

        private ArrayList CircleList = new ArrayList();
        private ArrayList LineList = new ArrayList();
        private ArrayList ArcList = new ArrayList();//弧线
       
        private ArrayList Outline_LineList = new ArrayList();
        private ArrayList Outline_ArcList = new ArrayList();//弧线
        private ArrayList Outline_CircleList = new ArrayList();
        private float[] markZero = new float[2];
        private string[] str = new string[2];
        private string CSVfile;
        private int count;
        string[] result;
        public string Layer = "20";

        private double leftx, lefty, rightx, righty;
        float Max_x = -10000, Min_x = 10000, Max_y = -10000, Min_y = 10000;
      
        public struct Point
        {
            public float x;
            public float y;
            public float r;
            public Color c;
        }
        
        private  void ReadPair()
        {
            result = null;
            string code = sr.ReadLine().Trim();
            string codedata = sr.ReadLine().Trim();
            count += 2;
             result = new string[2] { code, codedata };
            
        }
        private void read()
        {
            while (sr.Peek()!=-1)
            {
                ReadPair(); str=result;
                if(str[1]=="SECTION")
                {
                    ReadPair(); str=result;
                    switch(str[1])
                    {
                        //case "HEADER": Readheader();
                        //    break;
                        //case "TABLES": ReadTable();
                        //    break;
                        case "ENTITIES": ReadEntities();
                            break;
                    }
                }
            }
            sr.Close();
            fs.Close();
            count = 0;
        }
       private void ReadEntities()
        {
            while (str[1] != "ENDSEC")
            {
                switch(str[1])
            {
                case "LINE": ReadLine();
                    break;
                case "ARC": ReadArc();
                    break;
                case "CIRCLE": ReadCircle();
                    break;
                case "ELLIPSE": ReadArc();
                    break;
                //case "LWPOLYLINE": ReadLine();
                //    break;
                //case "SPLINE": ReadLine();
                //    break;
                default: ReadPair(); str=result;
                    break;
            }
            }
        }
        private void ReadArc()
       {
           ARC newarc = new ARC();
            while(str[1]!="ENDSEC")
            {
                ReadPair(); str=result;
                switch(str[0])
                {
                    case "8": newarc.LName = str[1];
                        break;
                    case "10": newarc.CenterX = float.Parse(str[1]);
                        break;
                    case "20": newarc.CenterY = float.Parse(str[1]);
                        break;
                    case "40": newarc.Radiu = float.Parse(str[1]);
                        break;
                    case "50": newarc.SAngle = float.Parse(str[1]);
                        break;
                    case "51": newarc.EAngle = float.Parse(str[1]);
                        break;
                    case "370": newarc.Lwidth = float.Parse(str[1]);
                        break;
                    case "0": 
                        ArcList.Add(newarc);
                        return;
                        break;
                }
            }
       }
        private void ReadCircle()
        {
            ARC newarc = new ARC();
            while (str[1] != "ENDSEC")
            {
                ReadPair(); str=result;
                switch (str[0])
                {
                    case "8": newarc.LName = str[1];
                        break;
                    case "10": newarc.CenterX = float.Parse(str[1]);
                        break;
                    case "20": newarc.CenterY = float.Parse(str[1]);
                        break;
                    case "40": newarc.Radiu = float.Parse(str[1]);
                        break;
                    case "50": newarc.SAngle = float.Parse(str[1]);
                        break;
                    case "51": newarc.EAngle = float.Parse(str[1]);
                        break;
                    case "370": newarc.Lwidth = float.Parse(str[1]);
                        break;
                    case "0": 
                            CircleList.Add(newarc);
                         return;
                        break;
                }
            }
        }
        private void ReadLine()
        {
            LINE newarc = new LINE();
            while (str[1] != "ENDSEC")
            {
                ReadPair(); str=result;
                switch (str[0])
                {
                    case "8": newarc.LName = str[1];
                        break;
                    case "10": newarc.StartX = float.Parse(str[1]);
                        break;
                    case "20": newarc.StartY = float.Parse(str[1]);
                        break;
                    case "11": newarc.EndX = float.Parse(str[1]);
                        break;
                    case "21": newarc.EndY = float.Parse(str[1]);
                        break;
                    case "62": newarc.Colornum = float.Parse(str[1]);
                        break;
                    case "370": newarc.Lwidth = float.Parse(str[1]);
                        break;
                    case "0":                       
                            LineList.Add(newarc);
                        return;
                      // break;
                }
            }
        }
        public string transport(string InPath,string OutPath)       
        {
            ArcList.Clear();
            LineList.Clear();
            LayerList.Clear();
            EllipseList.Clear();
            CircleList.Clear();
            Outline_ArcList.Clear();
            Outline_CircleList.Clear();
            Outline_LineList.Clear();
            if(File.Exists(InPath))
            {
                fs = new FileStream(InPath, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs);
                read();              
                if(File.Exists(OutPath))
                {
                    File.Delete(OutPath);
                }
                File.Create(OutPath).Close();
                StreamWriter sw = new StreamWriter(OutPath);
                string swline;
                for(int i=0;i<ArcList.Count;i++)
                {
                    ARC arc=(ARC)ArcList[i];
                    swline = "CA," + arc.LName + "," + arc.CenterX + "," + arc.CenterY + "," + arc.Radiu + "," + arc.SAngle + "," + arc.EAngle;
                    sw.WriteLine(swline);
                }
                for (int i = 0; i < CircleList.Count; i++)
                {
                    ARC arc = (ARC)CircleList[i];
                    swline = "CC," + arc.LName + "," + arc.CenterX + "," + arc.CenterY + "," + arc.Radiu + "," + arc.SAngle + "," + arc.EAngle;
                    sw.WriteLine(swline);
                }
                for (int i = 0; i < LineList.Count; i++)
                {
                    LINE line = (LINE)LineList[i];
                    swline = "CL," + line.LName + "," + line.StartX + "," + line.StartY + "," + line.EndX+ "," + line.EndY + "," + line.Lwidth;
                    sw.WriteLine(swline);
                }
                sw.Close();
                sw.Dispose();
                GetArea();
                CSVfile = OutPath;
                return "0";              
            }
            else
            {
                return "源文件不存在！";
            }        
        }

        private void GetArea()
        {
            if (CircleList != null)
            {
                for (int i = 0; i < CircleList.Count; i++)
                {
                    float x1 = ((ARC)CircleList[i]).CenterX + ((ARC)CircleList[i]).Radiu;
                    float x2 = ((ARC)CircleList[i]).CenterX - ((ARC)CircleList[i]).Radiu;
                    float y1 = ((ARC)CircleList[i]).CenterY + ((ARC)CircleList[i]).Radiu;
                    float y2 = ((ARC)CircleList[i]).CenterY - ((ARC)CircleList[i]).Radiu;
                    if (Max_x < x1)
                    {
                        Max_x = x1;
                    }
                    if (Min_x >= x2)
                    {
                        Min_x = x2;
                    }
                    if (Max_y < y1)
                    {
                        Max_y = y1;
                    }
                    if (Min_y >= y2)
                    {
                        Min_y = y2;
                    }
                }
            }
            if (LineList != null)
            {
                for (int i = 0; i < LineList.Count; i++)
                {
                    float x1 = ((LINE)LineList[i]).StartX;
                    float x2 = ((LINE)LineList[i]).EndX;
                    float y1 = ((LINE)LineList[i]).StartY;
                    float y2 = ((LINE)LineList[i]).EndY;
                    if (Max_x < x2)
                    {
                        Max_x = x2;
                    }
                    if (Min_x >= x1)
                    {
                        Min_x = x1;
                    }
                    if (Max_x < x1)
                    {
                        Max_x = x1;
                    }
                    if (Min_x >= x2)
                    {
                        Min_x = x2;
                    }
                    if (Max_y < y1)
                    {
                        Max_y = y1;
                    }
                    if (Min_y >= y2)
                    {
                        Min_y = y2;
                    }
                    if (Max_y < y2)
                    {
                        Max_y = y2;
                    }
                    if (Min_y >= y1)
                    {
                        Min_y = y1;
                    }
                }
            }
           
        }     
      
        public Bitmap OutPic(bool mode,Point[] points=null,float Markx=0,float Marky=0)  //true=显示所有
        {           
            Bitmap bmp = new Bitmap((int)(Max_x-Min_x),(int)(Max_y-Min_y)); //建立对应的bmp图
            Graphics g = Graphics.FromImage(bmp); //建立工作区
            Pen pen = new Pen(Color.White);//画笔颜色      
            g.FillRectangle(Brushes.DimGray, 0, 0, (int)(Max_x - Min_x), (int)(Max_y - Min_y));//填充背景
            for (int i = 0; i < ArcList.Count; i++)
            {
                if (((ARC)ArcList[i]).LName == "99"|mode)
                {
                    if (((ARC)ArcList[i]).SAngle < ((ARC)ArcList[i]).EAngle)
                    {
                        float x = ((ARC)ArcList[i]).CenterX - ((ARC)ArcList[i]).Radiu;
                        float y = ((ARC)ArcList[i]).CenterY - ((ARC)ArcList[i]).Radiu;
                        g.DrawArc(pen, x - Min_x, y - Min_y, ((ARC)ArcList[i]).Radiu * 2, ((ARC)ArcList[i]).Radiu * 2, ((ARC)ArcList[i]).SAngle, ((ARC)ArcList[i]).EAngle - ((ARC)ArcList[i]).SAngle);
                    }
                    else
                    {
                        float x = ((ARC)ArcList[i]).CenterX - ((ARC)ArcList[i]).Radiu;
                        float y = ((ARC)ArcList[i]).CenterY - ((ARC)ArcList[i]).Radiu;
                        g.DrawArc(pen, x - Min_x, y - Min_y, ((ARC)ArcList[i]).Radiu * 2, ((ARC)ArcList[i]).Radiu * 2, ((ARC)ArcList[i]).SAngle, ((ARC)ArcList[i]).EAngle - ((ARC)ArcList[i]).SAngle + 360);
                    }
                  
                }
            }
            for (int i = 0; i < CircleList.Count; i++)
            {
                if (((ARC)CircleList[i]).LName == "99" | mode)
                {
                    float x = ((ARC)CircleList[i]).CenterX - ((ARC)CircleList[i]).Radiu;
                    float y = ((ARC)CircleList[i]).CenterY - ((ARC)CircleList[i]).Radiu;
                    g.DrawArc(pen, x - Min_x, y - Min_y, ((ARC)CircleList[i]).Radiu * 2, ((ARC)CircleList[i]).Radiu * 2, 0, 360);                  
                }
                if (((ARC)CircleList[i]).Radiu == 5.8)
                {
                    markZero[0] = ((ARC)CircleList[i]).CenterX;
                    markZero[1] = ((ARC)CircleList[i]).CenterY;
                }
            }
            for (int i = 0; i < LineList.Count; i++)
            {
                if (((LINE)LineList[i]).LName == "99" | mode)
                {
                    g.DrawLine(pen, ((LINE)LineList[i]).StartX - Min_x, ((LINE)LineList[i]).StartY - Min_y, ((LINE)LineList[i]).EndX - Min_x, ((LINE)LineList[i]).EndY - Min_y);
                }
            }
            if (points != null)
            {
                float Xoff = Markx-markZero[0] ;
                float Yoff = Marky - markZero[1];
                for (int i = 0; i < points.Length; i++)
                {                   
                    pen = new Pen(points[i].c);
                    SolidBrush sldbrush = new SolidBrush(points[i].c);
                    float x = points[i].x-Xoff;
                    float y = points[i].y-Yoff;
                    float r = points[i].r;
                    g.FillEllipse(sldbrush, x-r, y-r, 2 * r, 2 * r);                  
                    if(i>0)
                    {
                        pen = new Pen(Brushes.Blue);
                        g.DrawLine(pen, points[i - 1].x - Xoff, points[i - 1].x - Yoff, x, y);
                    }
                }
            }
            return bmp;
        }
        
        public class ARC
        {
            public string LName;
            public float CenterX;
            public float CenterY;
            public float Radiu;
            public float SAngle;
            public float EAngle;
            public float Lwidth;
        }
        public class LINE
        {
            public string LName;
            public float StartX;
            public float StartY;
            public float EndX;
            public float EndY;
            public float Colornum;
            public float Lwidth;
        }
    }    
}
