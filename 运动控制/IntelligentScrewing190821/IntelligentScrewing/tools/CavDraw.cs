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
using System.Windows.Forms;

namespace IntelligentScrewing.tools
{
    public class CavDraw
    {
        #region///变量声明
        private List<string[]> listStr = new List<string[]>();//数组集合，存贮csv文档中的数据                
        private ArrayList LineList = new ArrayList();//直线数据
        private ArrayList ArcList = new ArrayList();//弧线数据      
        private ArrayList CircleList = new ArrayList();//圆数据
        private ArrayList Mark1List = new ArrayList();//MARK点1数据
        private ArrayList Mark2List = new ArrayList();//MARK点2数据      
        public float Max_x = -10000, Max_y = -10000, Min_x = 10000, Min_y = 10000;
        //创建背景图尺寸最大值取负最小值取正,后面通过确定bmp尺寸的方法比较出所需要的最大尺寸后来确定尺寸        
        private string[] str = new string[2];
        private int count;
        string[] result;
        private FileStream fs;//文件流
        private StreamReader sr;
        #endregion
        private void ReadPair()
        // 读取的数据放入数组result中,读两行,第一行是为标识(键)第二行为对应的值
        {
            result = null;//result清零
            string code = sr.ReadLine().Trim();  // 从字节流中读取字符并去掉空字符赋值给code    
            string codedata = sr.ReadLine().Trim();
            count += 2;//计数+2   
            result = new string[2] { code, codedata };    //读取的字符赋值给result
        }
        private void read() //读DXF文件
       
        {
            while (sr.Peek() != -1)  // 未读到最后一个字符  就循环
            {
                ReadPair(); //调用方法,读2行
                str = result;   //读2行的的值赋给 数组str
                if (str[1] == "SECTION")//确认读完前2行
                {
                    ReadPair(); str = result; //再读2行,赋值给result
                    switch (str[1])//直到读到实体段才结束循环
                    {
                        //case "HEADER": Readheader();
                        //    break;
                        //case "TABLES": ReadTable();
                        //    break;
                        case "ENTITIES": ReadEntities();//实体
                            break;
                    }
                }
            }
            sr.Close();//释放资源,,,可以考虑用using扩起来
            fs.Close();//释放资源,,,可以考虑用using扩起来
            count = 0;//计数归零
        }
        private void ReadEntities()
        //读DXF中实体部分
        {
            while (str[1] != "ENDSEC")
            {
                switch (str[1])
                {
                    case "LINE": ReadLine();  //遇到字段中的直线则调用读直线方法
                        break;
                    case "ARC": ReadArc();//遇到字段中的弧线调用读弧线方法
                        break;
                    case "CIRCLE": ReadCircle();//遇到字段中的圆调用读圆方法
                        break;
                    case "ELLIPSE": ReadArc();//遇到字段中的调用读椭圆方法
                        break;
                    case "LWPOLYLINE": ReadLine();
                        break;
                    case "SPLINE": ReadLine();
                        break;
                    default: ReadPair(); str = result;//其他情况则调用读两行方法一直往下读
                        break;
                }
            }
        }
        private void ReadArc()
        //读弧线,包含单独提取图层99(腔体图层)弧线数据
        {
            ARC newarc = new ARC();//创建弧线实例对象            
            while (str[1] != "ENDSEC")//创建循环读取直线的所有数据,直到弧线字段结尾即ENDSEC字符
            {
                ReadPair(); str = result;//读两行并赋值到str
                switch (str[0])//遍历ARC中的数据找出坐标相关的数据
                {
                    case "8": newarc.LName = str[1];//图层名,赋值到LName                                                
                        break;
                    case "10": newarc.CenterX = float.Parse(str[1]);//圆心X坐标                       
                        break;
                    case "20": newarc.CenterY = float.Parse(str[1]);//圆心Y坐标                       
                        break;
                    case "40": newarc.Radiu = float.Parse(str[1]);//圆半径                        
                        break;
                    case "50": newarc.SAngle = float.Parse(str[1]);//圆开始角度                        
                        break;
                    case "51": newarc.EAngle = float.Parse(str[1]);//圆结束角度                        
                        break;
                    case "370": newarc.Lwidth = float.Parse(str[1]);//线宽,可不用
                        break;
                    case "0": ArcList.Add(newarc);//读到下一个数据开头时则将已经读取的数据添加到newarc集合中                        
                        return;
                }
            }
        }
        private void ReadCircle()  //读圆,包含单独提取图层99(腔体图层)圆数据
      
        {
            ARC newarc = new ARC();//创建一个ARC类的实例对象,记录下这条线                              
            while (str[1] != "ENDSEC")//创建循环读取直线的所有数据,直到圆字段结尾即ENDSEC字符
            {
                ReadPair(); str = result;//读两行并赋值到result
                switch (str[0])//遍历CIRCLE中的数据找出坐标相关的数据
                {
                    case "8": newarc.LName = str[1];//图层名,赋值到LName                                                
                        break;
                    case "10": newarc.CenterX = float.Parse(str[1]);//圆心X坐标                                                                    
                        break;
                    case "20": newarc.CenterY = float.Parse(str[1]);//圆心Y坐标                                               
                        break;
                    case "40": newarc.Radiu = float.Parse(str[1]);//圆半径                                              
                        break;
                    case "50": newarc.SAngle = float.Parse(str[1]);//圆起始角度                       
                        break;
                    case "51": newarc.EAngle = float.Parse(str[1]);//圆终止角度                        
                        break;
                    case "370": newarc.Lwidth = float.Parse(str[1]);//线宽,可不用                        
                        break;
                    case "0": CircleList.Add(newarc);
                        //读到下一个数据开头时则将已经读取的数据添加到newarc集合中
                        return;
                }
            }
        }
        private void ReadLine()
        //读直线,包含单独提取图层99(腔体图层)直线数据
        {
            LINE newarc = new LINE();//创建一个LINE类的实例对象,记录下这条线           
            while (str[1] != "ENDSEC")//创建循环读取直线的所有数据,直到直线字段结尾即ENDSEC字符
            {
                ReadPair(); str = result;//读两行并赋值到result
                switch (str[0])//遍历LINE中的数据找出坐标相关的数据
                {
                    case "8": newarc.LName = str[1];//图层名,赋值到LName                       
                        break;
                    case "10": newarc.StartX = float.Parse(str[1]);//键10对应的值表示起点的X轴坐标                       
                        break;
                    case "20": newarc.StartY = float.Parse(str[1]);//键20对应的值表示起点的Y轴坐标                       
                        break;
                    case "11": newarc.EndX = float.Parse(str[1]);//终点的X轴坐标                        
                        break;
                    case "21": newarc.EndY = float.Parse(str[1]);//终点的Y轴坐标                        
                        break;
                    case "62": newarc.Colornum = float.Parse(str[1]);//颜色
                        break;
                    case "370": newarc.Lwidth = float.Parse(str[1]);//线宽,没有用
                        break;
                    case "0": LineList.Add(newarc);//读到下一数据的开头部分 则将已经赋值的newarc数据添加到LINELIST集合中                       
                        return;
                }
            }
        }
        public void transport(string InPath, string OutPath)
        //DXF转化为数据并把数据写到EXCEL.csv文件中
        {
            ArcList.Clear();//打开文件前初始化数据
            LineList.Clear();
            CircleList.Clear();
            fs = new FileStream(InPath, FileMode.Open, FileAccess.Read);//Inpath(打开的文件路径),打开,只读
            sr = new StreamReader(fs);//将fs中的数据读取并赋值给sr
            read();//调用read()方法读sr
            if (File.Exists(OutPath))//如果保存的路径有同名文件
            {
                File.Delete(OutPath);//删除同名文件
            }
            File.Create(OutPath).Close();//释放资源
            StreamWriter sw = new StreamWriter(OutPath);//创建一个写入的对象sw,路径为Outpath
            string swline;
            for (int i = 0; i < ArcList.Count; i++)//遍历弧线数据集合
            {
                ARC arc = (ARC)ArcList[i];//字符串转为ARC
                swline = "CA," + arc.LName + "," + arc.CenterX + "," + arc.CenterY + "," + arc.Radiu + "," + arc.SAngle + "," + arc.EAngle;
                sw.WriteLine(swline);//将相关数据写入到sw中
            }
            for (int i = 0; i < CircleList.Count; i++)//遍历弧线数据集合
            {
                ARC arc = (ARC)CircleList[i];
                swline = "CC," + arc.LName + "," + arc.CenterX + "," + arc.CenterY + "," + arc.Radiu + "," + arc.SAngle + "," + arc.EAngle;
                sw.WriteLine(swline);
            }
            for (int i = 0; i < LineList.Count; i++)
            {
                LINE line = (LINE)LineList[i];
                swline = "CL," + line.LName + "," + line.StartX + "," + line.StartY + "," + line.EndX + "," + line.EndY + "," + line.Lwidth;
                sw.WriteLine(swline);
            }
            sw.Close();//释放空间
            sw.Dispose();//释放由sw使用的所有资源                                                                                                  
        }
        public bool FoundCavity(string path1, string path2, string path3, string path4,string layer)
        //遍历csv文件,调用SaveCavity方法存储数据
        //可读取最多4份csv数据合为一份,单机使用只需要给一个path参数路径,其他参数给null即可
        {
            LineList.Clear();//所有集合初始化
            ArcList.Clear();
            CircleList.Clear();
            Mark1List.Clear();
            Mark2List.Clear();
            listStr.Clear();
            if (path1 != null)
            {
                StreamReader reader = new StreamReader(path1);//读取csv文件放入集合中
                string line = "";//按行读取,声明变量            
                line = reader.ReadLine();//读取一行数据
                while (line != null)//数据不为空时
                {
                    listStr.Add(line.Split(','));//去掉逗号将一行数据分割为字符串数组添加到集合中
                    line = reader.ReadLine();//再读一行
                }
                for (int i = 0; i < listStr.Count; i++)//遍历读取的csv文件数据
                {
                    {
                        SaveCavity(i,layer);//调用存数据方法
                    }
                }
                reader.Close();//释放资源
                reader.Dispose();
            }
            if (path2 != null)
            {
                StreamReader reader = new StreamReader(path2);//读取csv文件放入集合中
                string line = "";//按行读取,声明变量            
                line = reader.ReadLine();//读取一行数据
                while (line != null)//数据不为空时
                {
                    listStr.Add(line.Split(','));//去掉逗号将一行数据分割为字符串数组添加到集合中
                    line = reader.ReadLine();//再读一行
                }
                for (int i = 0; i < listStr.Count; i++)//遍历读取的csv文件数据
                {
                    {
                        SaveCavity(i, layer);//调用存数据方法
                    }
                }
                reader.Close();//释放资源
                reader.Dispose();
            }
            if (path3 != null)
            {
                StreamReader reader = new StreamReader(path3);//读取csv文件放入集合中
                string line = "";//按行读取,声明变量            
                line = reader.ReadLine();//读取一行数据
                while (line != null)//数据不为空时
                {
                    listStr.Add(line.Split(','));//去掉逗号将一行数据分割为字符串数组添加到集合中
                    line = reader.ReadLine();//再读一行
                }
                for (int i = 0; i < listStr.Count; i++)//遍历读取的csv文件数据
                {
                    {
                        SaveCavity(i, layer);//调用存数据方法
                    }
                }
                reader.Close();//释放资源
                reader.Dispose();
            }
            if (path4 != null)
            {
                StreamReader reader = new StreamReader(path4);//读取csv文件放入集合中
                string line = "";//按行读取,声明变量            
                line = reader.ReadLine();//读取一行数据
                while (line != null)//数据不为空时
                {
                    listStr.Add(line.Split(','));//去掉逗号将一行数据分割为字符串数组添加到集合中
                    line = reader.ReadLine();//再读一行
                }
                for (int i = 0; i < listStr.Count; i++)//遍历读取的csv文件数据
                {
                    {
                        SaveCavity(i, layer);//调用存数据方法
                    }
                }
                reader.Close();//释放资源
                reader.Dispose();
            }
            if ((Mark1List.Count == 0) || (Mark2List.Count == 0))
            {
                return false;
            }
            return true;
        }
        private void SaveCavity(int i,string layer)
        //遍历时找出腔体和MARK点数据分类存贮到集合中,按类型放到对象中存贮(圆,弧线,直线,MARK点)
        {
            CARC Carc = new CARC();//弧线
            CCIRCLE Ccircle = new CCIRCLE();//圆
            CLINE Cline = new CLINE();//直线
            CMARK Cmark = new CMARK();//MARK点
            string[] listCase = listStr[i];//把字符串集合中的字符串数据赋值到listCase
            float mark1 = 1.9f, mark2 = 1.91f;//声明mark点半径的浮点数对象用来比较
            if ((listCase[1] == layer) || (listCase[1] == "NEW"))//读到图层99(轮廓图层)的数据时开始赋值
            {
                switch (listCase[0])//根据字符串第一个元素的标识来判断是什么类型的数据
                {
                    case "CC"://圆型数据
                        if (float.Parse(listCase[4]) == mark1)
                        //是MARK点1则将数据额外赋值到MARK点1对象中
                        {
                            Cmark.MarkCenterX = float.Parse(listCase[2]);
                            Cmark.MarkCenterY = float.Parse(listCase[3]);
                            Cmark.MarkRadiu = float.Parse(listCase[4]);
                            Mark1List.Add(Cmark);//所有对象的数据添加到集合中
                        }
                        else if (float.Parse(listCase[4]) == mark2)
                        //是MARK点2则将数据额外赋值到MARK点2对象中
                        {
                            Cmark.MarkCenterX = float.Parse(listCase[2]);
                            Cmark.MarkCenterY = float.Parse(listCase[3]);
                            Cmark.MarkRadiu = float.Parse(listCase[4]);
                            Mark2List.Add(Cmark);
                        }
                        else
                        {
                            //不是MARK点则赋值到圆对象中
                            Ccircle.CenterX = float.Parse(listCase[2]);
                            Ccircle.CenterY = float.Parse(listCase[3]);
                            Ccircle.Radiu = float.Parse(listCase[4]);
                            Ccircle.SAngle = float.Parse(listCase[5]);
                            Ccircle.EAngle = float.Parse(listCase[6]);
                            CircleList.Add(Ccircle);
                        }
                        return;//返回数据
                    case "CA"://弧线,赋值到弧线对象中
                        Carc.CenterX = float.Parse(listCase[2]);
                        Carc.CenterY = float.Parse(listCase[3]);
                        Carc.Radiu = float.Parse(listCase[4]);
                        Carc.SAngle = float.Parse(listCase[5]);
                        Carc.EAngle = float.Parse(listCase[6]);
                        ArcList.Add(Carc);
                        return;
                    case "CL"://直线,赋值到直线对象中
                        Cline.StartX = float.Parse(listCase[2]);
                        Cline.StartY = float.Parse(listCase[3]);
                        Cline.EndX = float.Parse(listCase[4]);
                        Cline.EndY = float.Parse(listCase[5]);
                        Cline.Lwidth = float.Parse(listCase[6]);
                        LineList.Add(Cline);
                        return;
                }
            }
        }
        public float[] quitepoint(float Max_x, float Max_y, float Min_x, float Min_y, float x1, float x2, float y1, float y2)
        //比较所有数据的坐标大小找出最大坐标的方法
        {
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
            float[] quite = { Max_x, Max_y, Min_x, Min_y };
            return quite;
        }
        public Bitmap bmpSize()
        //遍历腔体数据确定图片尺寸,返回一个尺寸图
        //画图前确定图片最大尺寸,返回画布,用于下一步画图方法(OutCavitybmp)的矩形尺寸  
        {
            float[] quite = new float[4];
            if (CircleList != null)//如果有圆数据
            {
                for (int i = 0; i < CircleList.Count; i++)//遍历圆数据,确定画圆和弧线需要的最大尺寸
                {
                    float x1 = ((CCIRCLE)CircleList[i]).CenterX + ((CCIRCLE)CircleList[i]).Radiu;//圆心X坐标+半径  找出圆最宽的部分右边点坐标
                    float x2 = ((CCIRCLE)CircleList[i]).CenterX - ((CCIRCLE)CircleList[i]).Radiu;//圆心X坐标-半径  找出圆最宽的部分左边点坐标
                    float y1 = ((CCIRCLE)CircleList[i]).CenterY + ((CCIRCLE)CircleList[i]).Radiu;//圆心Y坐标+半径  找出圆最高点的坐标
                    float y2 = ((CCIRCLE)CircleList[i]).CenterY - ((CCIRCLE)CircleList[i]).Radiu;//圆心Y坐标-半径  找出圆最低点的坐标
                    quite = quitepoint(Max_x, Max_y, Min_x, Min_y, x1, x2, y1, y2);//调用方法确定最大值
                }
            }
            if (Mark1List != null)//如果有圆数据
            {
                for (int i = 0; i < Mark1List.Count; i++)//遍历圆数据,确定画圆和弧线需要的最大尺寸
                {
                    float x1 = ((CMARK)Mark1List[i]).MarkCenterX + ((CMARK)Mark1List[i]).MarkRadiu;//圆心X坐标+半径  找出圆最宽的部分右边点坐标
                    float x2 = ((CMARK)Mark1List[i]).MarkCenterX - ((CMARK)Mark1List[i]).MarkRadiu;//圆心X坐标-半径  找出圆最宽的部分左边点坐标
                    float y1 = ((CMARK)Mark1List[i]).MarkCenterY + ((CMARK)Mark1List[i]).MarkRadiu;//圆心Y坐标+半径  找出圆最高点的坐标
                    float y2 = ((CMARK)Mark1List[i]).MarkCenterY - ((CMARK)Mark1List[i]).MarkRadiu;//圆心Y坐标-半径  找出圆最低点的坐标
                    quite = quitepoint(quite[0], quite[1], quite[2], quite[3], x1, x2, y1, y2);//调用方法确定最大值
                }
            }
            if (Mark2List != null)//如果有圆数据
            {
                for (int i = 0; i < Mark2List.Count; i++)//遍历圆数据,确定画圆和弧线需要的最大尺寸
                {
                    float x1 = ((CMARK)Mark2List[i]).MarkCenterX + ((CMARK)Mark2List[i]).MarkRadiu;//圆心X坐标+半径  找出圆最宽的部分右边点坐标
                    float x2 = ((CMARK)Mark2List[i]).MarkCenterX - ((CMARK)Mark2List[i]).MarkRadiu;//圆心X坐标-半径  找出圆最宽的部分左边点坐标
                    float y1 = ((CMARK)Mark2List[i]).MarkCenterY + ((CMARK)Mark2List[i]).MarkRadiu;//圆心Y坐标+半径  找出圆最高点的坐标
                    float y2 = ((CMARK)Mark2List[i]).MarkCenterY - ((CMARK)Mark2List[i]).MarkRadiu;//圆心Y坐标-半径  找出圆最低点的坐标
                    quite = quitepoint(quite[0], quite[1], quite[2], quite[3], x1, x2, y1, y2);//调用方法确定最大值
                }
            }
            if (ArcList != null)//如果有弧线数据
            {
                for (int i = 0; i < ArcList.Count; i++)//遍历弧线数据,确定弧线需要的最大尺寸
                {
                    float x1 = ((CARC)ArcList[i]).CenterX + ((CARC)ArcList[i]).Radiu;//圆心X坐标+半径  找出圆最宽的部分右边点坐标
                    float x2 = ((CARC)ArcList[i]).CenterX - ((CARC)ArcList[i]).Radiu;//圆心X坐标-半径  找出圆最宽的部分左边点坐标
                    float y1 = ((CARC)ArcList[i]).CenterY + ((CARC)ArcList[i]).Radiu;//圆心Y坐标+半径  找出圆最高点的坐标
                    float y2 = ((CARC)ArcList[i]).CenterY - ((CARC)ArcList[i]).Radiu;//圆心Y坐标-半径  找出圆最低点的坐标
                    quite = quitepoint(quite[0], quite[1], quite[2], quite[3], x1, x2, y1, y2);//调用方法确定最大值
                }
            }
            if (LineList != null)//如果有直线的数据,再确认画直线需要的最大尺寸
            {
                for (int i = 0; i < LineList.Count; i++)//遍历此数据
                {
                    float x1 = ((CLINE)LineList[i]).StartX;//直线起点的X坐标
                    float x2 = ((CLINE)LineList[i]).EndX;//直线终点的X坐标
                    float y1 = ((CLINE)LineList[i]).StartY;//直线起点的Y坐标
                    float y2 = ((CLINE)LineList[i]).EndY;//直线终点的Y坐标    
                    float temp;
                    if (x1 < x2)
                    {
                        temp = x1; x1 = x2; x2 = temp;
                    }
                    if (y1 < y2)
                    {
                        temp = y1; y1 = y2; y2 = temp;
                    }
                    quite = quitepoint(quite[0], quite[1], quite[2], quite[3], x1, x2, y1, y2);//调用方法确定最大值
                    Max_x = quite[0]; Max_y = quite[1]; Min_x = quite[2]; Min_y = quite[3];
                }
            }
            Bitmap bmp = new Bitmap((int)(quite[0] - quite[2]), (int)(quite[1] - quite[3])); //建立对应的bmp图的尺寸 
            return bmp;//将矩形图尺寸数据返回
        }
        public Bitmap OutCavitybmp(Bitmap bmp)
        //画腔体图片不包括MARK点(图层99)
        {
            try
            {
                Graphics g = Graphics.FromImage(bmp); //建立工作区    在bmp对象上画图    
                Pen pen = new Pen(Color.LightPink);//画笔颜色      
                g.FillRectangle(Brushes.Black, 0, 0, bmp.Width, bmp.Height);
                //填充背景色,参数说明分别是填充颜色,矩形开始X坐标,开始Y坐标,矩形宽度,矩形高度                     
                for (int i = 0; i < ArcList.Count; i++)//遍历弧线
                {
                    if (((CARC)ArcList[i]).SAngle < ((CARC)ArcList[i]).EAngle)//如果弧线开始角度小于结束角度
                    {
                        float x = ((CARC)ArcList[i]).CenterX - ((CARC)ArcList[i]).Radiu;//开始点X坐标确定
                        float y = ((CARC)ArcList[i]).CenterY - ((CARC)ArcList[i]).Radiu;//开始点Y坐标确定
                        g.DrawArc(pen, x - Min_x, y - Min_y, ((CARC)ArcList[i]).Radiu * 2, ((CARC)ArcList[i]).Radiu * 2, ((CARC)ArcList[i]).SAngle, ((CARC)ArcList[i]).EAngle - ((CARC)ArcList[i]).SAngle);
                    }
                    //参数定义,画笔,起点X坐标,起点Y坐标,弧线高度,弧线宽度,起点从顺时针方向的角度,终点从顺时针方向的角度
                    else
                    {
                        float x = ((CARC)ArcList[i]).CenterX - ((CARC)ArcList[i]).Radiu;
                        float y = ((CARC)ArcList[i]).CenterY - ((CARC)ArcList[i]).Radiu;
                        g.DrawArc(pen, x - Min_x, y - Min_y, ((CARC)ArcList[i]).Radiu * 2, ((CARC)ArcList[i]).Radiu * 2, ((CARC)ArcList[i]).SAngle, ((CARC)ArcList[i]).EAngle - ((CARC)ArcList[i]).SAngle + 360);
                    }
                }
                for (int i = 0; i < CircleList.Count; i++)//遍历圆,画圆
                {
                    float x = ((CCIRCLE)CircleList[i]).CenterX - ((CCIRCLE)CircleList[i]).Radiu;
                    float y = ((CCIRCLE)CircleList[i]).CenterY - ((CCIRCLE)CircleList[i]).Radiu;
                    g.DrawArc(pen, x - Min_x, y - Min_y, ((CCIRCLE)CircleList[i]).Radiu * 2, ((CCIRCLE)CircleList[i]).Radiu * 2, 0, 360);
                }
                for (int i = 0; i < LineList.Count; i++)  //遍历直线,画直线
                {
                    g.DrawLine(pen, ((CLINE)LineList[i]).StartX - Min_x, ((CLINE)LineList[i]).StartY - Min_y, ((CLINE)LineList[i]).EndX - Min_x, ((CLINE)LineList[i]).EndY - Min_y);
                }
                return bmp;//画完后返回图像     
            }
            catch
            {
                return null;
            }
        }
        public Bitmap OutMark1bmp(Bitmap bmp)
        //画MARK点1(图层99)
        {
            try
            {
                Graphics g = Graphics.FromImage(bmp); //建立工作区    在bmp对象上画图    
                Pen pen = new Pen(Color.RoyalBlue);//画笔颜色      
                //g.FillRectangle(Brushes.Black, 0, 0, bmp.Width, bmp.Height);
                //填充背景色,参数说明分别是填充颜色,矩形开始X坐标,开始Y坐标,矩形宽度,矩形高度                     

                for (int i = 0; i < Mark1List.Count; i++)//遍历圆,画圆
                {
                    float x = ((CMARK)Mark1List[i]).MarkCenterX - ((CMARK)Mark1List[i]).MarkRadiu;
                    float y = ((CMARK)Mark1List[i]).MarkCenterY - ((CMARK)Mark1List[i]).MarkRadiu;
                    g.DrawArc(pen, x - Min_x, y - Min_y, ((CMARK)Mark1List[i]).MarkRadiu * 2, ((CMARK)Mark1List[i]).MarkRadiu * 2, 0, 360);
                }
                return bmp;//画完后返回图像                     
            }
            catch
            {
                return null;
            }
        }
        public Bitmap OutMark2bmp(Bitmap bmp)
        //画MARK点2(图层99)
        {
            try
            {
                Graphics g = Graphics.FromImage(bmp); //建立工作区    在bmp对象上画图    
                Pen pen = new Pen(Color.Red);//画笔颜色      
                //g.FillRectangle(Brushes.Black, 0, 0, bmp.Width, bmp.Height);
                //填充背景色,参数说明分别是填充颜色,矩形开始X坐标,开始Y坐标,矩形宽度,矩形高度                     

                for (int i = 0; i < Mark2List.Count; i++)//遍历圆,画圆
                {
                    float x = ((CMARK)Mark2List[i]).MarkCenterX - ((CMARK)Mark2List[i]).MarkRadiu;
                    float y = ((CMARK)Mark2List[i]).MarkCenterY - ((CMARK)Mark2List[i]).MarkRadiu;
                    g.DrawArc(pen, x - Min_x, y - Min_y, ((CMARK)Mark2List[i]).MarkRadiu * 2, ((CMARK)Mark2List[i]).MarkRadiu * 2, 0, 360);
                }
                return bmp;//画完后返回图像     
            }
            catch
            {
                return null;
            }
        }
        public Bitmap OutPointBmp(Bitmap bmp, float x, float y, float r)
        //根据输入的参数画点位圆的方法
        {
            try
            {
                Graphics g = Graphics.FromImage(bmp); //以参数bmp为基准建立工作区  在bmp上画点位圆
                Pen pen = new Pen(Color.White);//画笔颜色                                                          
                g.DrawEllipse(pen, x - r, y - r, r * 2, r * 2);//画圆
                return bmp;//画完后返回图像               
            }
            catch
            {
                return null;
            }
        }
        public float[] PlanOffset(float a, float b)
        //比较MARK点1坐标差的方法,参数a,b为txetbox填入的MARK点1坐标
        {
            float offsetmarkx, offsetmarky;//声明变量MARK点1坐标偏移量
            offsetmarkx = a - ((CMARK)Mark1List[0]).MarkCenterX;//调用MARK点1的数据计算偏移量
            offsetmarky = b - ((CMARK)Mark1List[0]).MarkCenterY;
            float[] offset = { offsetmarkx, offsetmarky };//把偏移量存到数组中返回
            return offset;
        }
        public float[] OutTranslationCsv(float x, float y, string OutPath)
        //根据对比MARK点1的坐标差,平移所有坐标并保存为NewDxfGoMark1.csv文件
        //参数x,y为MARK点1圆心坐标的X轴和Y轴偏移量,由方法PlanOffset返回数值,此方法返回值为平移后MARK点2坐标,用于下一步骤
        {
            if (File.Exists(OutPath))//如果保存的路径有同名文件
            {
                File.Delete(OutPath);//删除同名文件
            }
            File.Create(OutPath).Close();//释放资源  
            StreamWriter sw = new StreamWriter(OutPath);//创建一个写入的对象sw,路径为Outpath
            string swline;
            for (int i = 0; i < ArcList.Count; i++)//遍历弧线数据集合
            {
                CARC arc = (CARC)ArcList[i];//字符串转为ARC
                swline = "CA," + "NEW" + "," + (arc.CenterX + x) + "," + (arc.CenterY + y) + "," + arc.Radiu + "," + arc.SAngle + "," + arc.EAngle;
                sw.WriteLine(swline);//将相关数据写入到sw中
            }
            for (int i = 0; i < CircleList.Count; i++)//遍历弧线数据集合
            {
                CCIRCLE arc = (CCIRCLE)CircleList[i];
                swline = "CC," + "NEW" + "," + (arc.CenterX + x) + "," + (arc.CenterY + y) + "," + arc.Radiu + "," + arc.SAngle + "," + arc.EAngle;
                sw.WriteLine(swline);
            }
            for (int i = 0; i < Mark1List.Count; i++)//遍历弧线数据集合
            {
                CMARK mark1 = (CMARK)Mark1List[i];
                swline = "CC," + "NEW" + "," + (mark1.MarkCenterX + x) + "," + (mark1.MarkCenterY + y) + "," + mark1.MarkRadiu + "," + "0" + "," + "MARK1";
                sw.WriteLine(swline);
            }
            for (int i = 0; i < Mark2List.Count; i++)//遍历弧线数据集合
            {
                CMARK mark2 = (CMARK)Mark2List[i];
                swline = "CC," + "NEW" + "," + (mark2.MarkCenterX + x) + "," + (mark2.MarkCenterY + y) + "," + mark2.MarkRadiu + "," + "0" + "," + "MARK2";
                sw.WriteLine(swline);
            }
            for (int i = 0; i < LineList.Count; i++)
            {
                CLINE line = (CLINE)LineList[i];
                swline = "CL," + "NEW" + "," + (line.StartX + x) + "," + (line.StartY + y) + "," + (line.EndX + x) + "," + (line.EndY + y) + "," + line.Lwidth;
                sw.WriteLine(swline);
            }
            float[] newmark2 = { (((CMARK)Mark2List[0]).MarkCenterX + x), (((CMARK)Mark2List[0]).MarkCenterY + y) };
            sw.Close();//释放空间
            sw.Dispose();//释放由sw使用的所有资源 
            return newmark2;
        }
        public int GetTwistSlant(float newmark2x, float newmark2y, float oldmark2x, float oldmark2y, float p, float q)
        //以MARK点1为圆心,根据MARK点2与平移后的MARK点2坐标对比判断图片旋转的角度
        //参数说明 newmark2x,newmark2y为平移后MARK点2的坐标,oldmark2x,oldmark2y为MARK点2的实际坐标,q,p为MARK点1的坐标(以MARK点1为圆心旋转)
        {
            if ((newmark2x == (oldmark2y - q + p)) && (newmark2y == (p - oldmark2x + q)))//顺时针转90度,还需要逆时针旋转90度
            {
                return 1;//返回逆时针旋转次数1
            }
            if ((newmark2x == (2 * p - oldmark2x)) && (newmark2y == (q - oldmark2y)))//顺时针转180度,还需要逆时针旋转180度
            {
                return 2;//返回逆时针旋转次数2
            }
            if ((newmark2x == (p - oldmark2y)) && (newmark2y == (oldmark2x - p + q)))//顺时针转270度,还需要逆时针转270度
            {
                return 3;//返回逆时针旋转次数3
            }
            if ((newmark2x == oldmark2x) && (newmark2y == oldmark2y))
            {
                return 0;//坐标一致说明位置正确不用旋转
            }
            return 5;//以上4种情况都不是说明位置正确不用旋转
        }
        public Bitmap TwistBmp(Bitmap bmp)
        //顺时针旋转图片90度,仅旋转图片,csv数据不作改变
        {
            try
            {
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                return bmp;
            }
            catch
            {
                return null;
            }
        }
        public void OutConvertCsv(float x, float y, int time, string OutPath,string layer)
        //所有坐标数据逆时针旋转90度,保存到NewDxfGoMark2.csv文档中
        //参数x,y为MARK点1圆心坐标的X轴和Y轴,以MARK点1圆心为基准点逆时针旋转90度,time为需要循环的次数,该次数由方法GetTwistSlant的返回值给出
        {
            for (int j = 0; j < time; j++)
            {
                if (File.Exists(OutPath))//如果保存的路径有同名文件
                {
                    File.Delete(OutPath);//删除同名文件
                }
                File.Create(OutPath).Close();//释放资源
                StreamWriter sw = new StreamWriter(OutPath);//创建一个写入的对象sw,路径为Outpath
                string swline;
                for (int i = 0; i < ArcList.Count; i++)//遍历弧线数据集合
                {
                    CARC arc = (CARC)ArcList[i];//字符串转为ARC
                    swline = "CA," + "NEW" + "," + (arc.CenterY + x - y) + "," + (x - arc.CenterX + y) + "," + arc.Radiu + "," + (arc.SAngle + 270f) + "," + (arc.EAngle + 270f);
                    sw.WriteLine(swline);//将相关数据写入到sw中
                }
                for (int i = 0; i < CircleList.Count; i++)//遍历弧线数据集合
                {
                    CCIRCLE cir = (CCIRCLE)CircleList[i];
                    swline = "CC," + "NEW" + "," + (cir.CenterY + x - y) + "," + (x - cir.CenterX + y) + "," + cir.Radiu + "," + cir.SAngle + "," + cir.EAngle;
                    sw.WriteLine(swline);
                }
                for (int i = 0; i < Mark2List.Count; i++)//遍历弧线数据集合
                {
                    CMARK mark2 = (CMARK)Mark2List[i];
                    swline = "CC," + "NEW" + "," + (mark2.MarkCenterY + x - y) + "," + (x - mark2.MarkCenterX + y) + "," + mark2.MarkRadiu + "," + "0" + "," + "MARK2";
                    sw.WriteLine(swline);
                }
                for (int i = 0; i < LineList.Count; i++)
                {
                    CLINE line = (CLINE)LineList[i];
                    swline = "CL," + "NEW" + "," + (line.StartY + x - y) + "," + (x - line.StartX + y) + "," + (line.EndY + x - y) + "," + (x - line.EndX + y) + "," + line.Lwidth;
                    sw.WriteLine(swline);
                }
                sw.Close();//释放空间
                sw.Dispose();//释放由sw使用的所有资源     
                FoundCavity(OutPath, null, null, null,layer);
            }
        }
        public Bitmap btnOpenDxfFile(string outpath, float mark1x, float mark1y, float mark2x, float mark2y,string layer)
        //打开DXF文件根据填入的MARK点坐标自动修正DXF坐标并输出图片,同时将修正后的坐标生成DXF.CSV文件保存到D盘目录下
        //需要给出5个参数,csv文件的输出路径(默认为D盘),MARK点1和MARK点2的X,Y轴坐标
        {

            string inpath;
            try
            {
                OpenFileDialog openfile = new OpenFileDialog();//创建打开文件类的实例
                inpath = null;//DXF文件路径初始化
                openfile.Filter = @"dxf文件（*.dxf)|*.dxf";//打开文件类型限定DXF
                openfile.InitialDirectory = @"D:\";//默认打开文件路径
                if (openfile.ShowDialog() == DialogResult.OK)//判断选取文件是否确定
                {
                    inpath = openfile.FileName;//选取文件的路径赋值给inpath
                    transport(inpath, outpath);//把选取文件的数据生成CSV文档保存到outpath路径            
                }
            }
            catch
            {
                MessageBox.Show("选取的DXF文件无数据或者不存在,请选取正确的DXF文件");
                return null;
            }
            try
            {
                bool flagmark = FoundCavity(outpath, null, null, null,layer);
                //遍历数据并分类存贮数据,返回值是BOOL类型,如果没有MARK点会返回FALSE,有MARK点会返回TRUE
                if (flagmark == false)//如果没有MARK点
                {
                    MessageBox.Show("你载入的DXF文件中缺少MARK点,请载入正确的DXF文件!");
                    return null;
                }
            }
            catch//异常判断,理论上这一步不会出现异常,保险起见
            {
                MessageBox.Show("文件路径错误,请重新给定CSV文件路径!");
                return null;
            }
            float[] mark1change = PlanOffset(mark1x, mark1y);
            //调用方法比较MARK点1坐标偏差,返回偏差值数组mark1change
            float[] newmark2 = OutTranslationCsv(mark1change[0], mark1change[1], outpath);
            //调用方法,参数给入MARK1点的偏移量,根据坐标偏移量平移整个csv坐标数据,将平移后的数据保存到Dxf.csv文件,覆盖原DXF文件                                                               
            int time = GetTwistSlant(newmark2[0], newmark2[1], mark2x, mark2y, mark1x, mark1y);
            //对比新生成的DXF中MARK点2坐标差,根据此方法判断需要旋转的角度,返回int值time为旋转次数
            //参数依次为OutTranslationCsv()方法返回的新生成CSV文件中的MARK点2,X轴和Y轴坐标,正确的MAERK点2,X轴和Y轴坐标,MARK点1的X轴和Y轴坐标(以MARK点1为圆心旋转)
            if (time == 5)//如果返回值是5表示坐标对比异常
            {
                MessageBox.Show("坐标对比结果不正确,请核对MARK点坐标是否正确后再加载DXF文件!");
                return null;
            }
            FoundCavity(outpath, null, null, null,layer);
            //重读新的CSV文件
            OutConvertCsv(mark1x, mark1y, time, outpath,layer);
            //参数给入MARK1点圆心坐标,逆时针旋转次数,由GetTwistSlant()方法的返回值提供,调用方法后生成改正坐标后的DXF.csv文件,覆盖原CSV文件            
            FoundCavity(outpath, null, null, null,layer);
            //再读完全修正所有坐标后的csv文件
            Bitmap bmp = bmpSize();
            //确定图片背景矩形尺寸    
            Bitmap picboxbmp = (OutCavitybmp(bmp));
            return picboxbmp;
            //画腔体图,调用bmpSize方法返回的矩形尺寸,将画出的图片显示在picturebox中   
        }
        public Bitmap OutAllCsvPic(string path1, string path2, string path3, string path4,string layer)
        {
            FoundCavity(path1, path2, path3, path4,layer);
            //再读完全修正所有坐标后的csv文件
            Bitmap bmp = bmpSize();
            //确定图片背景矩形尺寸    
            Bitmap picboxbmp = (OutCavitybmp(bmp));
            return picboxbmp;
        }
    }
    class CCIRCLE
    //腔体圆数据类
    {
        public float CenterX;//圆中心X坐标
        public float CenterY;//圆中心Y坐标
        public float Radiu;//圆半径
        public float SAngle;//圆开始角度
        public float EAngle;//圆终结角度       
    }
    class CARC
    //腔体弧线数据类
    {
        public float CenterX;//弧线中心X坐标
        public float CenterY;//弧线中心Y坐标
        public float Radiu;//弧线半径
        public float SAngle;//弧线开始角度
        public float EAngle;//弧线终结角度
    }
    class CLINE
    //腔体直线数据类
    {
        public float StartX;//直线起点X坐标
        public float StartY;//直线起点Y坐标
        public float EndX;//直线终点X坐标
        public float EndY;//直线终点Y坐标
        public float Lwidth;//线宽
    }
    class CMARK
    //腔体MARK点数据类
    {
        public float MarkCenterX;//mark点圆心X坐标
        public float MarkCenterY;//mark点圆心Y坐标
        public float MarkRadiu;//mark点半径  
    }
    class ARC
    //DXF文件弧线数据类
    {
        public string LName;
        public float CenterX;
        public float CenterY;
        public float Radiu;
        public float SAngle;
        public float EAngle;
        public float Lwidth;
    }
    class LINE
    //DXF文件直线数据类
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
