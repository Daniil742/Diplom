using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    class ListOfPointd
    {
        public List<Point> Intersection = new List<Point>();

        int aa = 0;
        int ba = 0;
        int i = 3;
        int j = 1;
        Point Origin = new Point();  /* (0,0) */
        public List<Point> ConvexIntersect(List<Point> P, List<Point> Q, int n, int m)
        {
            //List<Point> Intersection = new List<Point>();
            Point a = P[i];
            Point b = Q[j];
            var inflag = new InFlag();
            bool FirstPoint = true;
            Point a1, b1;
            var A = new Point();
            var B = new Point();
            var p = new Point();
            var q = new Point();
            var p0 = new Point();

            do
            {
                Console.Write("a = ");
                a.ShowPoint();
                Console.Write("b = ");
                b.ShowPoint();
                Console.WriteLine($"aa = {aa}");
                Console.WriteLine($"ba = {ba}");
                Console.WriteLine($"FirstPoint = {FirstPoint}");
                Console.WriteLine($"inflag = {inflag.f}");

                a1 = P[(i + n - 1) % n];
                b1 = Q[(j + m - 1) % m];

                SubVec(a, a1, A);
                SubVec(b, b1, B);

                Console.Write("A = ");
                A.ShowPoint();
                Console.Write("B = ");
                B.ShowPoint();

                var cross = Origin.AreaSign(Origin, A, B);
                var aHB = b1.AreaSign(b1, b, a);
                var bHA = a1.AreaSign(a1, a, b);
                Console.WriteLine("cross = " + cross + ", aHB = " + aHB + ", bHA = " + bHA);

                /* If A & B intersect, update inflag. */
                char code = a1.SegSegInt(a1, a, b1, b, p, q);
                Console.WriteLine("code = " + code);

                if (code == '1' || code == 'v')
                {
                    if ((inflag.f == inflag.Unknown) && FirstPoint)
                    {
                        aa = ba = 0;
                        FirstPoint = false;
                        p0.X = p.X;
                        p0.Y = p.Y;
                        //Intersection.Add(p0);
                    }
                    inflag = InOut(p, inflag, aHB, bHA);
                    //Console.WriteLine("InOut sets inflag=" + inflag.f);
                }

                /*-----Advance rules-----*/

                /* Special case: A & B overlap and oppositely oriented. */
                if ((code == 'e') && (Dot(A, B) < 0))
                {
                    InsertSharedSeg(p, q);
                }

                /* Special case: A & B parallel and separated. */
                if ((cross == 0) && (aHB < 0) && (bHA < 0))
                {
                    Console.WriteLine("P and Q are disjoint.");
                }


                /* Special case: A & B collinear. */
                else if ((cross == 0) && (aHB == 0) && (bHA == 0))
                {
                    /* Advance but do not output point. */
                    if (inflag.f == inflag.Pin) 
                    {
                        b = P[Advance(b, "ba", inflag.f == inflag.Qin, b, Q.Count)];
                    }
                    else
                    {
                        a = P[Advance(a, "aa", inflag.f == inflag.Pin, a, P.Count)];
                    }
                }

                /* Generic cases. */
                else if (cross >= 0)
                {
                    if (bHA > 0)
                    {
                        a = P[Advance(a, "aa", inflag.f == inflag.Pin, a, P.Count)];
                    }
                    else
                    {
                        b = Q[Advance(b, "ba", inflag.f == inflag.Qin, b, Q.Count)];
                    }
                }
                else /* if ( cross < 0 ) */
                {
                    if (aHB > 0)
                    {
                        b = Q[Advance(b, "ba", inflag.f == inflag.Qin, b, Q.Count)];
                    }
                    else
                    {
                        i = Advance(a, "aa", inflag.f == inflag.Pin, a, P.Count);
                        a = P[i];
                    }
                }
                Console.WriteLine();
            } while (((aa < n) || (ba < m)) && (aa < 2 * n) && (ba < 2 * m));

            //if (!FirstPoint) /* If at least one point output, close up. */
            //    Intersection.Add(p0);

            ///* Deal with special cases: not implemented. */
            if (inflag.f == inflag.Unknown)
            {
                Console.WriteLine("The boundaries of P and Q do not cross.");
            }

            return Intersection;
        }

        InFlag InOut(Point p, InFlag inflag, int aHB, int bHA)
        {
            Intersection.Add(p);

            if (aHB > 0)
            {
                inflag.f = inflag.Pin;
                return inflag;
            }
            else if (bHA > 0)
            {
                inflag.f = inflag.Qin;
                return inflag;
            }
            else
                return inflag;
        }

        private int Advance(Point a, String counter, bool inside, Point v, int tmp)
        {
            int index = 0;
            if (inside)
            {
                Intersection.Add(v);
            }
            if (counter.Equals("aa"))
            {
                aa += 1;
                i += 1;
                if (i == tmp)
                    i = 0; // return 0;
                index = i;
            }
            else if (counter.Equals("ba"))
            {
                ba += 1;
                j += 1;
                if (j == tmp)
                    j = 0; // return 0;
                index = j;
            }
            return index;
        }

        private void SubVec(Point a, Point b, Point c)
        {
            c.X = a.X - b.X;
            c.Y = a.Y - b.Y;
        }
        private double Dot(Point a, Point b)
        {
            double sum = a.X * b.X + a.Y * b.Y;
            return sum;
        }

        public void InsertSharedSeg(Point p, Point q)
        {
            Intersection.Add(p);
            Intersection.Add(q);
        }
    }
}
