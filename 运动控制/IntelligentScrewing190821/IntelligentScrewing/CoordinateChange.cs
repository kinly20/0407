using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MathNet.Numerics.LinearAlgebra.Double; //真平度算法

namespace IntelligentScrewing
{
    class CoordinateChange
    {
        private static PointF ProductOffset = new PointF();
        public static double ProductAngle = 0;
        private static PointF ProductOrg = new PointF();
        private static PointF CADOrg = new PointF();
        public static double SetProductCoordinate(PointF org1, PointF org2, PointF ProductPoint1, PointF ProductPoint2)
        {
            CADOrg = org1;
            ProductOrg = ProductPoint1;
            double lengthOrg = Math.Sqrt((org2.X - org1.X) * (org2.X - org1.X) + (org2.Y - org1.Y) * (org2.Y - org1.Y));
            double lengthProd = Math.Sqrt((ProductPoint2.X - ProductPoint1.X) * (ProductPoint2.X - ProductPoint1.X) + (ProductPoint2.Y - ProductPoint1.Y) * (ProductPoint2.Y - ProductPoint1.Y));
            double off = Math.Abs(lengthOrg - lengthProd);
            if(off>1.5)
            {
                return off;
            }
            
            double a = Math.Asin((org2.Y - org1.Y) / lengthOrg);
            double b = Math.Asin((ProductPoint2.Y - ProductPoint1.Y) / lengthProd);
            if (Parameter.Para[106] == "正向")
            { ProductAngle = a - b; }
            else if (Parameter.Para[106] == "反向")
            { ProductAngle = b - a; }
            else
            { ProductAngle = 0; }
            ProductOffset.X = ProductPoint1.X - org1.X;
            ProductOffset.Y = ProductPoint1.Y - org1.Y;
            return off;
        }
        public static void GetProductCoordinate(PointF inPoint, out PointF OutPoint)
        {
            OutPoint = new PointF();
            double x = inPoint.X - CADOrg.X;
            double y = inPoint.Y - CADOrg.Y;
            
            double x1 = x * Math.Cos(ProductAngle)- y * Math.Sin(ProductAngle);
            double y1 = y * Math.Cos(ProductAngle) + x * Math.Sin(ProductAngle);
            OutPoint.X = (float)(CADOrg.X + x1) + ProductOffset.X;
            OutPoint.Y = (float)(CADOrg.Y + y1) + ProductOffset.Y;
            if(Math.Abs(inPoint.X-OutPoint.X)<5&Math.Abs(inPoint.Y-OutPoint.Y)<5)
            {            
                
            }else
            {
                OutPoint = inPoint;
            }
            
        }
        public static double[] Plane(double[] x, double[] y, double[] z)
        {
            DenseMatrix A = new DenseMatrix(3, 3);
            DenseVector B = new DenseVector(3);

            A[0, 0] = x.Select(v => v * v).Sum();
            A[1, 0] = x.Zip(y, (a, b) => a * b).Sum();
            A[2, 0] = x.Sum();
            A[0, 1] = A[1, 0];
            A[1, 1] = y.Select(v => v * v).Sum();
            A[2, 1] = y.Sum();
            A[0, 2] = A[2, 0];
            A[1, 2] = A[2, 1];
            A[2, 2] = x.Count();

            B[0] = x.Zip(z, (a, b) => a * b).Sum();
            B[1] = y.Zip(z, (a, b) => a * b).Sum();
            B[2] = z.Sum();
            var X = A.Inverse() * B;
            return X.ToArray();
        }
        public static bool[] floatCheck(List<float[]> data, float offset)
        {
            int n = data.Count;
            double[] x = new double[n];
            double[] y = new double[n];
            double[] z = new double[n];
            bool[] result=new bool[n];
            double[] _z = new double[n];
            if (n >= 3&offset!=0)
            {
                for(int i=0;i<n;i++)
                {
                    x[i] = data[i][0];
                    y[i] = data[i][1];
                    z[i] = data[i][2];
                }
                double[] abc = Plane(x, y, z);
                if (double.IsNaN(abc[0]) | double.IsNaN(abc[1]) | double.IsNaN(abc[2]))
                {
                    return result;
                }
                for (int i = 0; i < n;i++ )
                {
                    _z[i] = abc[0] * x[i] + abc[1] * y[i] + z[i];
                    result[i] = Math.Abs(z[i]-_z[i]) < offset ? false : true;
                }
            }
            return result;


        }

    }
}
