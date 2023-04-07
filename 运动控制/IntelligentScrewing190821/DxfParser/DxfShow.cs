using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Drawing;

namespace DxfParser
{
    public partial class DxfShow: UserControl
    {
        #region
        private FileStream fs;
        private StreamReader sr;

        private ArrayList EllipseList = new ArrayList();
        private ArrayList splineList = new ArrayList();
        private ArrayList LayerList = new ArrayList();//图层

        private ArrayList CircleList = new ArrayList();
        private ArrayList LineList = new ArrayList();
        private ArrayList ArcList = new ArrayList();//弧线

        public static List<OutLine> outline = new List<OutLine>();
        public static List<PointDraw> ElementList = new List<PointDraw>();
        public static List<OutLine> outline2 = new List<OutLine>();
        public static List<PointDraw> ElementList2 = new List<PointDraw>();
        public static PointF ZeroPos = new System.Drawing.PointF();
        public static PointF ZeroPos2 = new System.Drawing.PointF();
        public static Color[] ColorList = new System.Drawing.Color[10];

        public static string[] OrgPoint=new string[2];

        public static string[] ReferencePoint=new string[2];

        public static PointF StartPoint=new System.Drawing.PointF();
        public static List<OutLine> Lines = new List<OutLine>();

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        public struct OutLine
        {
            public string Type;
            public float CenterX;
            public float CenterY;
            public float StartX;
            public float StartY;
            public float EndX;
            public float EndY;
            public float StartAngle;
            public float EndAngle;
            public float R;          
            public Color C;
           
        }
        public struct PointDraw
        {
            public float x;
            public float y;
            public Color color;
            public float r;
            public string name;
            public string tag;
            public string txt;
            public string axis;
            public string mode;
            public string sdj;
        }

        //private ArrayList Outline_LineList = new ArrayList();
        //private ArrayList Outline_ArcList = new ArrayList();//弧线
        //private ArrayList Outline_CircleList = new ArrayList();
        
        private float[] markZero = new float[2];
        private string[] str = new string[2];
        private int count;
        string[] result;
        public string Layer = "20";
        public float rotation=0;
        public static bool xNegation;
        public static bool yNegation;
        
        float Max_x = -10000, Min_x = 10000, Max_y = -10000, Min_y = 10000;
        public float ratio=1;
        public float Xoff, Yoff;
        private void ReadPair()
        {
            result = null;
            string code = sr.ReadLine().Trim();
            string codedata = sr.ReadLine().Trim();
            count += 2;
            result = new string[2] { code, codedata };
        }
        private void read()
        {
            while (sr.Peek() != -1)
            {
                ReadPair(); str = result;
                if (str[1] == "SECTION")
                {
                    ReadPair(); str = result;
                    switch (str[1])
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
                switch (str[1])
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
                    default: ReadPair(); str = result;
                        break;
                }
            }
        }
        private void ReadArc()
        {
            ARC newarc = new ARC();
            while (str[1] != "ENDSEC")
            {
                ReadPair(); str = result;
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
                        if(rotation!=0)
                        {
                            double angle = rotation * Math.PI / 180;
                            double tempx = newarc.CenterX * Math.Cos(angle) + newarc.CenterY * Math.Sin(angle);
                            double tempy = newarc.CenterY * Math.Cos(angle) - newarc.CenterX * Math.Sin(angle);
                            newarc.CenterX = (float)tempx;
                            newarc.CenterY = (float)tempy;
                            newarc.SAngle = newarc.SAngle - rotation;
                            newarc.EAngle = newarc.EAngle - rotation;
                            if(newarc.SAngle<0)
                            {
                                newarc.SAngle = newarc.SAngle + 360;
                                newarc.EAngle = newarc.EAngle + 360;
                            }
                            //float temp = newarc.CenterX;
                            //newarc.CenterX = newarc.CenterY;
                            //newarc.CenterY = temp;
                            //temp = newarc.SAngle;
                            //newarc.SAngle = newarc.EAngle;
                            //newarc.EAngle = temp;
                        }
                        if(xNegation)
                        {
                            newarc.CenterX = 0 - newarc.CenterX;
                            float temp1 = 180 - newarc.SAngle;
                            float temp2 = 180 - newarc.EAngle;
                            if (temp1 < 0)
                                temp1 += 360;
                            if (temp2 < 0)
                                temp2 += 360;
                            newarc.SAngle = temp2;
                            newarc.EAngle = temp1;
                           
                        }
                        if(yNegation)
                        { newarc.CenterY = 0 - newarc.CenterY;
                        float temp1 = 360 - newarc.SAngle;
                        float temp2 = 360 - newarc.EAngle;
                        newarc.SAngle = temp2;
                        newarc.EAngle = temp1;
                        }
                        ArcList.Add(newarc);
                        return;
                        break;
                }
            }
        }
        private void ReadArc(string[] list)
        {
            ARC temp = new ARC();
            temp.CenterX = float.Parse(list[2]);
            temp.CenterY = float.Parse(list[3]);
            temp.Radiu = float.Parse(list[4]);
            temp.SAngle = float.Parse(list[5]);
            temp.EAngle = float.Parse(list[6]);
            ArcList.Add(temp);
        }
        private void ReadCircle(string[] list)
        {
            ARC temp = new ARC();
            temp.CenterX = float.Parse(list[2]);
            temp.CenterY = float.Parse(list[3]);
            temp.Radiu = float.Parse(list[4]);
            temp.SAngle = float.Parse(list[5]);
            temp.EAngle = float.Parse(list[6]);
            CircleList.Add(temp);
        }
        private void ReadCircle()
        {
            ARC newarc = new ARC();
            while (str[1] != "ENDSEC")
            {
                ReadPair(); str = result;
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
                        if(rotation!=0)
                        {
                            double angle = rotation * Math.PI / 180;
                            double tempx = newarc.CenterX * Math.Cos(angle) + newarc.CenterY * Math.Sin(angle);
                            double tempy = newarc.CenterY * Math.Cos(angle) - newarc.CenterX * Math.Sin(angle);
                            newarc.CenterX = (float)tempx;
                            newarc.CenterY = (float)tempy;
                            
                            
                        }
                        if(xNegation)
                        {
                            newarc.CenterX = 0 - newarc.CenterX;
                        }
                        if(yNegation)
                        { newarc.CenterY = 0 - newarc.CenterY; }
                        
                        CircleList.Add(newarc);
                        return;
                        break;
                }
            }
        }
        private void ReadLine(string[] list)
        {
            LINE temp = new LINE();
            temp.StartX = float.Parse(list[2]);
            temp.StartY = float.Parse(list[3]);
            temp.EndX = float.Parse(list[4]);
            temp.EndY = float.Parse(list[5]);
            temp.Lwidth = float.Parse(list[6]);
            LineList.Add(temp);
        }
        private void ReadLine()
        {
            LINE newarc = new LINE();
            while (str[1] != "ENDSEC")
            {
                ReadPair(); str = result;
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
                        if(rotation!=0)
                        {
                            double angle = rotation * Math.PI / 180;
                            double tempx = newarc.StartX * Math.Cos(angle) + newarc.StartY * Math.Sin(angle);
                            double tempy = newarc.StartY * Math.Cos(angle) - newarc.StartX * Math.Sin(angle);
                            newarc.StartX = (float)tempx;
                            newarc.StartY = (float)tempy;

                            tempx = newarc.EndX * Math.Cos(angle) + newarc.EndY * Math.Sin(angle);
                            tempy = newarc.EndY * Math.Cos(angle) - newarc.EndX * Math.Sin(angle);
                            newarc.EndX = (float)tempx;
                            newarc.EndY = (float)tempy;
                            //float temp = newarc.StartX;
                            //newarc.StartX = newarc.StartY;
                            //newarc.StartY = temp;
                            //temp = newarc.EndX;
                            //newarc.EndX = newarc.EndY;
                            //newarc.EndY = temp;
                        }
                        if(xNegation)
                        {
                            newarc.StartX = 0 - newarc.StartX;
                            newarc.EndX = 0 - newarc.EndX;
                        }
                        if(yNegation)
                        { newarc.StartY = 0 - newarc.StartY;
                        newarc.EndY = 0 - newarc.EndY;
                        }
                        LineList.Add(newarc);
                        return;
                        break;
                }
            }
        }
        public string transport(string InPath, ref List<string> OutData)
        {
            ArcList.Clear();
            LineList.Clear();
            LayerList.Clear();
            EllipseList.Clear();
            CircleList.Clear();
            OutData.Clear();
            if (File.Exists(InPath))
            {
                fs = new FileStream(InPath, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs);
                read();
                string swline;
                for (int i = 0; i < ArcList.Count; i++)
                {
                    ARC arc = (ARC)ArcList[i];
                    swline = "CA," + arc.LName + "," + arc.CenterX.ToString("F2") + "," + arc.CenterY.ToString("F2") + "," + arc.Radiu.ToString("F2") + "," + arc.SAngle.ToString("F2") + "," + arc.EAngle.ToString("F2");
                    OutData.Add(swline);
                }
                for (int i = 0; i < CircleList.Count; i++)
                {
                    ARC arc = (ARC)CircleList[i];
                    swline = "CC," + arc.LName + "," + arc.CenterX.ToString("F2") + "," + arc.CenterY.ToString("F2") + "," + arc.Radiu.ToString("F2") + "," + arc.SAngle.ToString("F2") + "," + arc.EAngle.ToString("F2");
                    OutData.Add(swline);
                }
                for (int i = 0; i < LineList.Count; i++)
                {
                    LINE line = (LINE)LineList[i];
                    swline = "CL," + line.LName + "," + line.StartX.ToString("F2") + "," + line.StartY.ToString("F2") + "," + line.EndX.ToString("F2") + "," + line.EndY.ToString("F2") + "," + line.Lwidth;
                    OutData.Add(swline);
                }            
           
                return "0";
            }
            else
            {
                return "源文件不存在！";
            }
        }
        private void GetArea(ref OutLine[] outlineData, ref PointDraw[] dataList)
        {
            Max_x = -10000; Min_x = 10000; Max_y = -10000; Min_y = 10000;
            if (outlineData != null)
            {
                for (int i = 0; i < outlineData.Length; i++)
                {
                    outlineData[i].CenterX = 0 - outlineData[i].CenterX;
                    outlineData[i].StartX = 0 - outlineData[i].StartX;
                    outlineData[i].EndX = 0 - outlineData[i].EndX;
                    float x1 = Max_x;
                    float x2 = Max_x;
                    float y1 = Max_x;
                    float y2 = Max_x;
                    if(outlineData[i].Type=="CC"|outlineData[i].Type=="CA")
                    {
                        x1 = outlineData[i].CenterX + outlineData[i].R;
                        x2 = outlineData[i].CenterX - outlineData[i].R;
                        y1 = outlineData[i].CenterY + outlineData[i].R;
                        y2 = outlineData[i].CenterY - outlineData[i].R;
                    }else if(outlineData[i].Type=="CL")
                    {
                        x1 = outlineData[i].StartX;
                        x2 = outlineData[i].EndX;
                        y1 = outlineData[i].StartY;
                        y2 = outlineData[i].EndY;
                    }
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
            if(dataList!=null)
            {
                for(int i=0;i<dataList.Length;i++)
                {
                    dataList[i].x = 0 - dataList[i].x;
                    float x1 = dataList[i].x + dataList[i].r;
                    float x2 = dataList[i].x - dataList[i].r;
                    float y1 = dataList[i].y + dataList[i].r;
                    float y2 = dataList[i].y - dataList[i].r;
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
        private void GetElement(List<string> element)
        {
            for(int i=0;i<element.Count;i++)
            {
                string[] temp = element[i].Split(',');
                if(temp[0]=="CA")
                {
                    ReadArc(temp);
                }
                if(temp[0]=="CC")
                {
                    ReadCircle(temp);
                }
                if(temp[0]=="CL")
                {
                    ReadLine(temp);
                }
            }
        }
     

        public Bitmap bmp;
        public Bitmap OutlinePic( List<OutLine> outlineData,List<PointDraw>dataList)  
        {
            GC.Collect();
            pictureBox1.Size = panel1.Size;
            OutLine[] data = outlineData.ToArray();
            PointDraw[] dataPoint = dataList.ToArray();
            GetArea(ref data, ref dataPoint);
            float Width = Max_x - Min_x;
            float Height = Max_y - Min_y;
            double k;
            if (panel1.Size.Width / Width > panel1.Size.Height / Height)
            {
                k = panel1.Size.Height / Height - 0.1;
            }else
            {
                k = panel1.Size.Width / Width - 0.1;
            }
            if (ratio < k)
                ratio =(float) k;
            Max_x = Max_x * ratio;
            Max_y = Max_y * ratio;
            Min_x = Min_x * ratio;
            Min_y = Min_y * ratio;
            int border = 20;
            //pictureBox1.Dispose();
            //pictureBox1.Refresh();
            //if (bmp != null)
            //{
            //    DeleteObject(bmp.GetHbitmap());  
            //}
            ////pictureBox1.Dispose();
            pictureBox1.Refresh();
            bmp = new Bitmap((int)((Max_x - Min_x) + border), (int)((Max_y - Min_y) + border)); //建立对应的bmp图
            Graphics g = Graphics.FromImage(bmp); //建立工作区
            //Pen pen = new Pen(ColorList[1],ratio);//画笔颜色      
            Pen pen = new Pen(Color.White, ratio);//画笔颜色   
            g.FillRectangle(Brushes.Black, 0, 0, bmp.Width, bmp.Height);//填充背景
            //using (SolidBrush brush = new SolidBrush(ColorList[0]))
            //{
            //    g.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);//填充背景
            //}
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Type == "CA")
                {
                    float x = data[i].CenterX * ratio;
                    float y = data[i].CenterY * ratio;
                    float r = data[i].R * ratio;
                    float Sangle =180-data[i].EndAngle;
                    float Eangle =180-data[i].StartAngle;
                    if(Sangle<0)
                    {
                        Sangle += 360;
                    }
                    if(Eangle<0)
                    {
                        Eangle += 360;
                    }
                    x = x - r + border/2;
                    y = y - r + border/2;
                    if (Sangle < Eangle)
                    {
                        g.DrawArc(pen, x - Min_x, y - Min_y, r * 2, r * 2, Sangle, Eangle - Sangle);
                    }
                    else
                    {
                        g.DrawArc(pen, x - Min_x, y - Min_y, r * 2, r * 2, Sangle, Eangle - Sangle + 360);
                    }
                }
                if (data[i].Type == "CC")
                {
                    float x = data[i].CenterX * ratio;
                    float y = data[i].CenterY * ratio;
                    float r = data[i].R * ratio;
                    x = x - r + border / 2;
                    y = y - r + border / 2;
                    g.DrawArc(pen, x - Min_x, y - Min_y, r * 2, r * 2, 0, 360);            
                }
                if(data[i].Type == "CL")
                {
                    float x1 = data[i].StartX * ratio + border / 2;
                    float y1 = data[i].StartY * ratio + border / 2;
                    float x2 = data[i].EndX * ratio + border / 2;
                    float y2 = data[i].EndY * ratio + border / 2;
                    g.DrawLine(pen, x1 - Min_x , y1 - Min_y , x2 - Min_x , y2 - Min_y );            
                }
            }
            //for (int i = 0; i < CircleList.Count; i++)
            //{

            //    float x = ((ARC)ArcList[i]).CenterX * ratio;
            //    float y = ((ARC)ArcList[i]).CenterY * ratio;
            //    float r = ((ARC)ArcList[i]).Radiu * ratio;
            //    x = x - r + 5;
            //    y = y - r + 5;
            //        g.DrawArc(pen, x - Min_x, y - Min_y, r * 2, r * 2, 0, 360);              
            //}
            //for (int i = 0; i < LineList.Count; i++)
            //{
            //    float x1 = ((LINE)LineList[i]).StartX * ratio;
            //    float y1 = ((LINE)LineList[i]).StartY * ratio;
            //    float x2 = ((LINE)LineList[i]).EndX * ratio;
            //    float y2 = ((LINE)LineList[i]).EndY * ratio;
            //    g.DrawLine(pen, x1 - Min_x+5, y1 - Min_y+5, x2 - Min_x+5, y2 - Min_y+5);             
            //}
            for (int i = 0; i < dataPoint.Length; i++)
            {
                float x = (dataPoint[i].x) * ratio;
                float y = (dataPoint[i].y) * ratio;
                float r = dataPoint[i].r * ratio;
                    x = x - r + border / 2;
                    y = y - r + border / 2;
                    if (dataPoint[i].tag != "LOST")
                    {
                        SolidBrush sldbrush = new SolidBrush(dataPoint[i].color);
                        g.FillEllipse(sldbrush, x - Min_x, y - Min_y, r * 2, r * 2);
                        //g.DrawArc(pen, x - Min_x, y - Min_y, r * 2, r * 2, 0, 360);
                        Font font = new Font("宋体", (float)(r/0.8));
                        PointF posi = new PointF(x - r - Min_x, y - Min_y - r);
                        g.DrawString(dataPoint[i].name, font, Brushes.White, posi);
                    }
                    if (dataPoint[i].tag == "LOST" )
                    {
                        Font font = new Font("宋体", (float)(r / 0.8));
                        PointF posi = new PointF(x - r - Min_x, y - Min_y - r);
                        g.DrawString(dataPoint[i].name, font, Brushes.White, posi);
                        pen = new Pen(Color.Red, ratio);
                        g.DrawArc(pen, x - Min_x-2, y - Min_y-2, (r + 2) * 2, (r + 2) * 2, 0, 360);
                    }
                    if (dataPoint[i].tag == "ERROR")
                    {
                        SolidBrush sldbrush = new SolidBrush(dataPoint[i].color);
                        g.FillEllipse(sldbrush, x - Min_x, y - Min_y, r * 2, r * 2);
                        //g.DrawArc(pen, x - Min_x, y - Min_y, r * 2, r * 2, 0, 360);
                        Font font = new Font("宋体", (float)(r / 0.8));
                        PointF posi = new PointF(x - r - Min_x, y - Min_y - r);
                        g.DrawString(dataPoint[i].name, font, Brushes.White, posi);
                        pen = new Pen(Color.Red, ratio);
                        g.DrawArc(pen, x - Min_x-2, y - Min_y-2, (r + 2) * 2, (r + 2) * 2, 0, 360);
                    }
            }
            g.Dispose();
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
#endregion

        public DxfShow()
        {
            InitializeComponent();
            //pictureBox1.Size = panel1.Size;
        }
       
        public void ShowPic(int index=0,RotateFlipType rotate=RotateFlipType.RotateNoneFlipNone)
        {           
            Image temp = bmp;           
            temp.RotateFlip(rotate); 
             if(index==0)
            pictureBox1.Image = temp;    
            if(index==1)
            {
                pictureBox2.Image = temp;
            }
        }
        #region//放大缩小移动旋转
        public void SaveColor()
        {

        }
        public void Mirror()
        {
            Image temp = pictureBox1.Image;
            temp.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pictureBox1.Image = temp;          
        }
        public void Rotate90()
        {
            Image temp = pictureBox1.Image;
            temp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox1.Refresh();
            pictureBox1.Image = temp;
        }
        public void Rotate180()
        {
            Image temp = pictureBox1.Image;
            temp.RotateFlip(RotateFlipType.Rotate180FlipNone);
            pictureBox1.Refresh();
            pictureBox1.Image = temp;
        }
        public void Rotate270()
        {
            Image temp = pictureBox1.Image;
            temp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            pictureBox1.Refresh();
            pictureBox1.Image = temp;
        }
        public void ZoomUp()
        {
            int index = tabControl1.SelectedIndex;
            ratio =(float) (ratio * 1.2);
            if (ratio > 15)
            {
                ratio = 15;
            }
            if (index == 0)
            {
                OutlinePic(outline, ElementList);
                pictureBox1.Size = bmp.Size;
            }
            if (index == 1)
            {
                OutlinePic(outline2, ElementList2);
                pictureBox2.Size = bmp.Size;
            }
        }
        public void ZoomDown()
        {
            int index = tabControl1.SelectedIndex;
            ratio = (float)(ratio / 1.2);

            if (index == 0)
            {
                OutlinePic(outline, ElementList);
                pictureBox1.Size = bmp.Size;
            }
            if (index == 1)
            {
                OutlinePic(outline2, ElementList2);
                pictureBox2.Size = bmp.Size;
            }
        }
        bool picMove;
        int PicDxfMouseX ;
		int PicDxfMouseY;
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!picMove)
            {
                return;
            }
            pictureBox1.Left = pictureBox1.Left + (e.X - PicDxfMouseX);
            pictureBox1.Top = pictureBox1.Top + (e.Y - PicDxfMouseY);
            picMove = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            PicDxfMouseX = e.X;
            PicDxfMouseY = e.Y;
            picMove = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!picMove)
            {
                return;
            }
            pictureBox1.Left = pictureBox1.Left + (e.X - PicDxfMouseX);
            pictureBox1.Top = pictureBox1.Top + (e.Y - PicDxfMouseY);
        }
        bool picMove2;
        int PicDxfMouseX2;
        int PicDxfMouseY2;
        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (!picMove2)
            {
                return;
            }
            pictureBox2.Left = pictureBox2.Left + (e.X - PicDxfMouseX2);
            pictureBox2.Top = pictureBox2.Top + (e.Y - PicDxfMouseY2);
            picMove2 = false;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            PicDxfMouseX2 = e.X;
            PicDxfMouseY2 = e.Y;
            picMove2 = true;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (!picMove2)
            {
                return;
            }
            pictureBox2.Left = pictureBox2.Left + (e.X - PicDxfMouseX2);
            pictureBox2.Top = pictureBox2.Top + (e.Y - PicDxfMouseY2);
        }
        #endregion
        public void SavePic(string path,bool writeString=false,int index=0)
        {
            if(index==0)
            pictureBox1.Image.Save(path);
            if(index==1)
            pictureBox2.Image.Save(path);
            if(writeString)
            {
                int n = path.Length;
                string pathtxt = path.Remove(n - 3)+"txt";
                if(File.Exists(pathtxt))
                {
                    File.Delete(pathtxt);
                }
                StreamWriter sw=new StreamWriter(pathtxt);
                sw.WriteLine("异常记录:");
                for(int i=0;i<ElementList.Count;i++)
                {
                    if(ElementList[i].tag=="LOST")
                    {
                        sw.WriteLine("{0}号位置缺少{1}",ElementList[i].name , ElementList[i].txt);
                    }
                    if (ElementList[i].tag == "ERROR")
                    {
                        sw.WriteLine("{0}号位置应装{1}", ElementList[i].name, ElementList[i].txt);
                    }
                }
                sw.WriteLine("End");
                sw.Dispose();
                sw.Close();
            }
        }
        
    }
}
