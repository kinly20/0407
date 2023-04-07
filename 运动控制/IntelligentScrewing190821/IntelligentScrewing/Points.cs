using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IntelligentScrewing
{
    class Points
    {
        //public static Point[] points = new Point[1000];
        public static List<Point> points = new List<Point>();
        public static List<Point> points2 = new List<Point>(); 
        public struct Point
        {
            public  float X;
            public  float Y;
            public  float Z;
            public  int Axis;
            public  int ScrewType;
            public  int WorkMode;
            public int Status;
            public float floatCheck;
        }
      
        //public static int ChangeNumber = 1;
        //public static Point SelectPoint = new Point();
    }
}
