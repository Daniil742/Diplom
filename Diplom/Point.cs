using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Point()
        {
            X = 0;
            Y = 0;
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int AreaSign(Point a, Point b, Point c)
        {
            double area2;

            area2 = (b.X - a.X) * (double)(c.Y - a.Y) - (c.X - a.X) * (double)(b.Y - a.Y);


            /* The area should be an integer. */
            if (area2 > 0.5)
                return 1;
            else if (area2 < -0.5)
                return -1;
            else
                return 0;
        }

        public bool Collinear(Point a, Point b, Point c)
        {
            return AreaSign(a, b, c) == 0.0;
        }

        //public bool Between(Point a, Point b, Point c)
        //{
        //    //Point ba, ca;

        //    if (!Collinear(a, b, c))
        //        return false;

        //    /* If ab not vertical, check betweenness on x; else on y. */
        //    if (a.X != b.X)
        //        return ((a.X <= c.X) && (c.X <= b.X)) || ((a.X >= c.X) && (c.X >= b.X));
        //    else
        //        return ((a.Y <= c.Y) && (c.Y <= b.Y)) || ((a.Y >= c.Y) && (c.Y >= b.Y));
        //}

        public char SegSegInt(Point a, Point b, Point c, Point d, Point p, Point q)
        {
            char code = '?';
            //double denom = a.X * (double)(d.Y - c.Y) + b.X * (double)(c.Y - d.Y) + d.X * (double)(b.Y - a.Y) + c.X * (double)(a.Y - b.Y);
            double denom = (double)(b.X - a.X) * (c.Y - d.Y) - (c.X - d.X) * (b.Y - a.Y);

            if (denom == 0.0)
                return ParallelInt(a, b, c, d, p, q);

            //double num = a.X * (double)(d.Y - c.Y) + c.X * (double)(a.Y - d.Y) + d.X * (double)(c.Y - a.Y);
            double num = (c.X - a.X) * (double)(c.Y - d.Y) - (c.X - d.X) * (double)(c.Y - a.Y);
            if ((num == 0.0) || (num == denom))
                code = 'v';
            double s = num / denom; //v
            //Console.WriteLine($"SegSegInt: num = {num}, denom = {denom}, s = {s}");

            num = -(a.X * (double)(c.Y - b.Y) + b.X * (double)(a.Y - c.Y) + c.X * (double)(b.Y - a.Y));
            if ((num == 0.0) || (num == denom))
                code = 'v';
            double t = num / denom; //u
            //Console.WriteLine($"SegSegInt: num = {num}, denom = {denom}, s = {t}");

            if ((0.0 < s) && (s < 1.0) && (0.0 < t) && (t < 1.0))
                code = '1';
            else if ((0.0 > s) || (s > 1.0) || (0.0 > t) || (t > 1.0))
                code = '0';

            p.X = a.X + s * (b.X - a.X);
            p.Y = a.Y + s * (b.Y - a.Y);

            //Console.WriteLine();
            //Console.WriteLine(code);
            return code;
        }

        public char ParallelInt(Point a, Point b, Point c, Point d, Point p, Point q)
        {
            if (!a.Collinear(a, b, c))
                return '0';

            if (Between(a, b, c) && Between(a, b, d))
            {
                Assigndi(p, c);
                Assigndi(q, d);
                return 'e';
            }
            if (Between(c, d, a) && Between(c, d, b))
            {
                Assigndi(p, a);
                Assigndi(q, b);
                return 'e';
            }
            if (Between(a, b, c) && Between(c, d, b))
            {
                Assigndi(p, c);
                Assigndi(q, b);
                return 'e';
            }
            if (Between(a, b, c) && Between(c, d, a))
            {
                Assigndi(p, c);
                Assigndi(q, a);
                return 'e';
            }
            if (Between(a, b, d) && Between(c, d, b))
            {
                Assigndi(p, d);
                Assigndi(q, b);
                return 'e';
            }
            if (Between(a, b, d) && Between(c, d, a))
            {
                Assigndi(p, d);
                Assigndi(q, a);
                return 'e';
            }
            return '0';
        }

        public void Assigndi(Point p, Point a)
        {
            p.X = a.X;
            p.Y = a.Y;
        }

        public bool Between(Point a, Point b, Point c)
        {
            /* If ab not vertical, check betweenness on x; else on y. */
            if (a.X != b.X)
                return ((a.X <= c.X) && (c.X <= b.X)) || ((a.X >= c.X) && (c.X >= b.X));
            else
                return ((a.Y <= c.Y) && (c.Y <= b.Y)) || ((a.Y >= c.Y) && (c.Y >= b.Y));
        }

        public void ShowPoint()
        {
            Console.WriteLine($"({X}, {Y})");
        }
    }
}
