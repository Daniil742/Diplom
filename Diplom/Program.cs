using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Point> P = new List<Point>();
            P.Add(new Point(2, 2));
            P.Add(new Point(-4, 2));
            P.Add(new Point(-7, -2));
            P.Add(new Point(-1, -3));

            List<Point> Q = new List<Point>();
            Q.Add(new Point(3, 3));
            Q.Add(new Point(4, 0));
            Q.Add(new Point(5, 1));

            ListOfPointd list = new ListOfPointd();
            var inters2 = list.Intersection;
            var inters = list.ConvexIntersect(P, Q, P.Count, Q.Count);

            foreach (var item in inters)
            {
                item.ShowPoint();
            }

            Console.WriteLine(inters.Count);
            Console.WriteLine(inters2.Count);

            foreach (var item in inters2)
            {
                item.ShowPoint();
            }

            Console.ReadKey();
        }
    }
}
