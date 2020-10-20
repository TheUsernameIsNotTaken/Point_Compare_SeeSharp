using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point
{
    class SortByDisXY : IComparer<Point>
    {
        public int Compare(Point p1, Point p2)
        {
            int dif = p1.Distance().CompareTo(p2.Distance());
            if (dif != 0)   return dif;
            dif = p1.X.CompareTo(p2.X);
            if (dif != 0) return dif;
            return p1.Y.CompareTo(p2.Y);
        }
    }

    class Compare
    {
        static void Main(string[] args)
        {
			//Tömbök
			int[] t;
			int []n;
			t = new int[4]; // Nulla az alapértelmezett kezdőértek.
			n = new int[] { 2, 5, 3, 4, 5 };
			int[] s = { 1, 2, 3, 4 };
			//Rendezés
			Array.Sort(n);
            Console.WriteLine("Rendezett számok: " + string.Join(",", n));

            Console.WriteLine(" - - - - - ");

			//Saját osztályú tömbök
			Point[] pt = {
				new Point(1,2),
				new Point(1,1),
				new Point(2,2),
				new Point(2,1),
				new Point(0,2),
				new Point(2,0),
				new Point()
			};
            //Rendezés - Osztály által megmondott - Természetes rendezés
            Console.Write("1-es tömb rendezetlenül: " + string.Join(",", (object[])pt));
            Array.Sort(pt);        //Hiányzik a Comparable alapból!
            Console.WriteLine("X: " + string.Join(",", (object[])pt));

            // helyettesíthetőség példája:
            // a SortByDisXY implementálja az IComparer-t, így helyettesítheti őt.
            // (ugye az Array.Sort ebben az esetben egy IComparer-t vár második paraméterként)
            IComparer<Point> comparer = new SortByDisXY();
            Array.Sort(pt, comparer);
            Console.WriteLine("Táv, X és Y: " + string.Join(", ", (object[])pt));

            // ebben az esetben a második paraméter egy Comparison típusú referencia:
            // maga a comparison egy delegált típus,
            // ami referencia típus, a tevékenységet a delegáltra bízom.
            Comparison<Point> cp = Point.SortByY;
            Array.Sort(pt, cp);
            Console.WriteLine("Y: " + string.Join(", ", (object[])pt));

            // Helyben létrehozhatok egy delegáltat. (ez comparison típusú lesz)
            Array.Sort(pt, delegate (Point a, Point b)
            {
                return b.X.CompareTo(a.X);
            });
            Console.WriteLine("Fordított X: " + string.Join(", ", (object[])pt));

            // anonim metódus
            Comparison<Point> cpp = delegate (Point a, Point b)
            {
                return b.Y.CompareTo(a.Y);
            };
            Array.Sort(pt, cpp);
            Console.WriteLine("Fordított Y: " + string.Join(", ", (object[])pt));

            // lambda kifejezés
            Array.Sort(pt, (Point a, Point b) =>
            {
                int dif = a.Distance(new Point(1, 1)).CompareTo(b.Distance(new Point(1, 1)));
                if (dif != 0) return dif;
                return a.X.CompareTo(b.X);
            });
            Console.WriteLine("Dis(1,1) és X: " + string.Join(", ", (object[])pt));

            // lambda utasítás rövid alakja, ha csak 1 utasítás áll a lambda kifejezésben
            Array.Sort(pt, (a, b) => a.Distance(new Point(1, 1)).CompareTo(b.Distance(new Point(1, 1))));
            Console.WriteLine("Dis(1,1): " + string.Join(", ", (object[])pt));

            Console.WriteLine(" - - - - - ");
            //Új tömb és rendezése
            Point[] pt2 = {
                    new Point(3,4),
                    new Point(-3,-4),
                    new Point(-3,4),
                    new Point(3, -4)
            };

            // LINQ
            Func<Point, double> f = delegate (Point a) { return a.Distance(new Point(3,4)); };
            var res = pt2.OrderBy(f);
            Console.WriteLine("Dis(3,4): " + string.Join(", ", res));

            // a delegáltat lambdára cseréltük
            pt2 = pt2.OrderBy(a => a.Distance(new Point(3, 4))).ToArray();
            Console.WriteLine("Dis(3,4): " + string.Join(", ", (object[])pt2));

            var tmp = from p in pt2 orderby p.Quadrant select p;
            Console.WriteLine("Quadrant: " + string.Join(", ", tmp));

            var tmpPart = (from p in pt orderby p.Y select new { p.X, p.Y }).ToArray();
            Console.WriteLine("X részlet: " + string.Join(", ", (object[])tmpPart));
        }
    }
}
