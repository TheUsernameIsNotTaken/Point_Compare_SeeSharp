using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point
{
    class Point : IComparable<Point>
    {
        //Itt propretikkel oldjuk meg a privát változókat. Ez a PropFull
        private int _x, _y;

        public int X
        {
            get { return _x;  }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y;  }
            set { _y = value; }
        }

        //Síknegyed = quadrant
        public int Quadrant
        {
            get
            {
                if (_x >= 0)
                {
                    if (_y >= 0)
                        return 1;
                    else
                        return 4;
                }
                else
                {
                    if (_y >= 0)
                        return 2;
                    else
                        return 3;
                }
            }
            set
            {
                if(value > 0 && value < 5)
                {
                    _x = Math.Abs(_x);
                    _y = Math.Abs(_y);
                    switch (value)
                    {
                        case 1: break;
                        case 2: _x *= -1; break;
                        case 3: _x *= -1; _y *= -1; break;
                        case 4: _y *= -1; break;
                    }
                }
            }
        }

        //Tömörített prop, privát változó nélkül. Property nevek nagybetűvel kezdődnek.
        //public int Y { get; set; }

        //Ctrl+. -> Generate Constructor
        //Az üres paraméterlistás konstruktor alapértelmezett, de üres.
        //Ez nekünk legyen az origó értéke.
        public Point()
        {
            this._x = 0;
            this._y = 0;
        }

        //Lehet más construktort is generálni.
        public Point(int x, int y)
        {
            X = x;
            Y = y;
            //Console.WriteLine("Új pont: " + this.ToString());
        }

        //Lehetnek metódusai egy osztálynak
        public double Distance()
        {
            double dis = Math.Sqrt(Math.Pow(_x, 2) + Math.Pow(_y, 2));
            //Console.WriteLine(ToString() + " távolsága az origótól: " + dis);
            return dis;
        }

        public double Distance(Point b)
        {
            double dis = Math.Sqrt(Math.Pow(this._x - b._x, 2) + Math.Pow(this._y - b._y, 2));
            //Console.WriteLine(ToString() + " távolsága a " + b.ToString() + " ponttól: " + dis);
            return dis;
        }

        //És lehet osztályszintű metódus is.
        public static double Distance(Point a, Point b)
        {
            double dis = Math.Sqrt(Math.Pow(a._x - b._x, 2) + Math.Pow(a._y - b._y, 2));
            //Console.WriteLine(  "A " + a.ToString() +
            //                    " és " + b.ToString() +
            //                    " pontok közötti távolság: " + dis);
            return dis;
        }

        //toString - az elem kiírása
        //Ctr+. -> Generate Overrides
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(x:").Append(_x);
            sb.Append(", y:").Append(_y);
            sb.Append(", Q:");
            switch (Quadrant)
            {
                case 1: sb.Append("I.");    break;
                case 2: sb.Append("II.");   break;
                case 3: sb.Append("III.");  break;
                case 4: sb.Append("IV.");   break;
            }
            sb.Append(')');
            return sb.ToString();
        }

        //2 elem mikor egyenlő.
        //Egyes halmazok a HashCode után ezt használják az egyenlőség meghatározására.
        //Ez lehet pl. halmazoknál. Valami csak hashcode, de valami ezt is megnézi már.
        public override bool Equals(object obj)
        {
            /*
            return obj is Point point &&
                   x == point._x &&
                   y == point._y;
            */
            return base.Equals(obj);
        }

        //Ha már más bemeneti argumentumlista van, akkor egyedi metódus.
        //Most a név is más, hogy könnyen hivatkozhassam a test-ben. Így nem override!
        public bool equals(Point point)
        {
            return point._x == this._x &&
                   point._y == this._y;
        }

        //ComparetTo van a rendezésnél használva
        public int CompareTo(Point other)
        {
            return X.CompareTo(other.X);
        }

        public static int SortByY(Point x, Point y)
        {
            return x.Y.CompareTo(y.Y);
        }
    }
}
