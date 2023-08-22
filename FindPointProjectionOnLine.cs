using System.Drawing;
using System.Globalization;
using Community.CsharpSqlite;
using DataBases;

namespace SampleProject
{
    class Point2D
    {
        public double X { get; set; }
        
        public double Y { get; set; }

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public class Program
    {
        public static double GetDeterminant(double[,] matrix)
        {
            return matrix[0,0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }

        public static void Main()
        {
            var m = new Point2D(0, 0);
            var n = new Point2D(1, 1);
            var p = new Point2D(1, 1);

            var a = m.X - n.X;
            var b = m.Y - n.Y;
            var c = -(p.X * a + p.Y * b);
            var d = b * m.X - a * m.Y;

            var d1 = GetDeterminant(new[,] { { -c, b }, { -d, a } });
            var d2 = GetDeterminant(new[,] { { a, -c }, { -b, -d } });
            var dx = GetDeterminant(new[,] { { a, b }, { -b, a } });
            Console.WriteLine(d1 / dx);
            Console.WriteLine(d2 / dx);
        }
    }
}
