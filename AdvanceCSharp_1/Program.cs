using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;

namespace AdvanceCSharp_1
{
    public interface IStringContainer
    {
        string this[int index]
        { get; set; }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Point ptOne = new Point(100, 100);
            Point ptTwo = new Point(40, 40);
            Console.WriteLine("ptOne = {0}", ptOne);
            Console.WriteLine("ptTwo = {0}", ptTwo);
            Console.WriteLine();
            Console.WriteLine("ptOne + ptTwo: {0} ", ptOne + ptTwo);
            Console.WriteLine("ptOne - ptTwo: {0} ", ptOne - ptTwo);
            Console.WriteLine("ptOne + ptTwo: {0} ", ptOne + ptTwo);

            // Subtract the points to make a smaller point?
            Console.WriteLine("ptOne - ptTwo: {0} ", ptOne - ptTwo);
            Console.WriteLine();

            // Prints [110, 110]
            Point biggerPoint = ptOne + 10;
            Console.WriteLine("ptOne + 10 = {0}", biggerPoint);

            // Prints [120, 120]
            Console.WriteLine("10 + biggerPoint = {0}", 10 + biggerPoint);
            Console.WriteLine();

            // Freebie +=
            Point ptThree = new Point(90, 5);
            Console.WriteLine("ptThree = {0}", ptThree);
            Console.WriteLine("ptThree += ptTwo: {0}", ptThree += ptTwo);
            Console.WriteLine();

            // Freebie -=
            Point ptFour = new Point(0, 500);
            Console.WriteLine("ptFour = {0}", ptFour);
            Console.WriteLine("ptFour -= ptThree: {0}", ptFour -= ptThree);
            Console.WriteLine();

            // Applying the ++ and -- unary operators to a Point.
            Point ptFive = new Point(1, 1);
            Console.WriteLine("++ptFive = {0}", ++ptFive);  // [2, 2]
            Console.WriteLine("--ptFive = {0}", --ptFive);  // [1, 1]
            Console.WriteLine();

            // Apply same operators as postincrement/decrement.
            Point ptSix = new Point(20, 20);
            Console.WriteLine("ptSix++ = {0}", ptSix++);  // [20, 20]
            Console.WriteLine("ptSix-- = {0}", ptSix--);  // [21, 21]
            Console.WriteLine();

            Console.WriteLine("ptOne == ptTwo : {0}", ptOne == ptTwo);
            Console.WriteLine("ptOne != ptTwo : {0}", ptOne != ptTwo);
            Console.WriteLine();

            Console.WriteLine("ptOne < ptTwo : {0}", ptOne < ptTwo);
            Console.WriteLine("ptOne > ptTwo : {0}", ptOne > ptTwo);

            Console.WriteLine("---------------------------------------------------");

            Rectangle r = new Rectangle(15, 4);
            Console.WriteLine(r.ToString());
            r.Draw();

            Console.WriteLine();

            Square s = (Square)r;
            Console.WriteLine(s.ToString());
            s.Draw();

            Console.WriteLine("---------------------------------------------------");

            Rectangle rect = new Rectangle(10, 5);
            DrawSquare((Square)rect);

            Console.WriteLine("---------------------------------------------------");

            int myInt = 12345678;
            myInt.DisplayDefiningAssembly();

            DataSet d = new DataSet();
            d.DisplayDefiningAssembly();

            System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
            sp.DisplayDefiningAssembly();

            Console.WriteLine("Value of myInt: {0}", myInt);
            Console.WriteLine("Reversed digits of myInt: {0}", myInt.ReverseDigits());

            Console.WriteLine("---------------------------------------------------");

            var myCar = new { Color = "Bright Pink", Make = "Saab", CurrentSpeed = 55 };
            Console.WriteLine("My car is a {0} {1}.", myCar.Color, myCar.Make);
            BuildAnonType("BMW", "Black", 90);

            Console.WriteLine("---------------------------------------------------");

            ReflectOverAnonymousType(myCar);

            Console.ReadLine();
        }

        static void EqualityTest()
        {
            var firstCar = new { Color = "Bright Pink", Make = "Saab", CurrentSpeed = 55 };
            var secondCar = new { Color = "Bright Pink", Make = "Saab", CurrentSpeed = 55 };

            if(firstCar.Equals(secondCar))
                Console.WriteLine("Same anonymous object!");
            else
                Console.WriteLine("Not the same anonymous object!");

            if (firstCar == secondCar)
                Console.WriteLine("Same anonymous object!");
            else
                Console.WriteLine("Not the same anonymous object!");

            if (firstCar.GetType().Name == secondCar.GetType().Name)
                Console.WriteLine("We are both the same type!");
            else
                Console.WriteLine("We are different types!");

            Console.WriteLine();
            ReflectOverAnonymousType(firstCar);
            ReflectOverAnonymousType(secondCar);
        }

        static void ReflectOverAnonymousType(object obj)
        {
            Console.WriteLine("obj is an instance of: {0}", obj.GetType().Name);
            Console.WriteLine("Base class of {0} is {1}", obj.GetType().Name, obj.GetType().BaseType);
            Console.WriteLine("obj.ToString() == {0}", obj.ToString());
            Console.WriteLine("obj.GetHashCode() == {0}", obj.GetHashCode());
            Console.WriteLine();
        }

        static void BuildAnonType(string make, string color, int currSp)
        {
            var car = new { Make = make, Color = color, Speed = currSp };

            // Note you can now use this type to get the property data!
            Console.WriteLine("You have a {0} {1} going {2} MPH", car.Color, car.Make, car.Speed);
            Console.WriteLine("ToString() == {0}", car.ToString());
        }

        static void DrawSquare(Square s)
        {
            Console.WriteLine(s.ToString());
            s.Draw();
        }
    }

    class SomeClass : IStringContainer
    {
        private List<string> myStrings = new List<string>();
        public string this[int index]
        {
            get
            {
                return myStrings[index];
            }
            set
            {
                myStrings.Insert(index, value);
            }
        }
    }

    public class Point : IComparable<Point>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int xPos, int yPos)
        {
            X = xPos;
            Y = yPos;
        }

        #region Object overrides
        public override string ToString()
        {
            return string.Format("[{0}, {1}]", this.X, this.Y);
        }
        public override bool Equals(object o)
        {
            return o.ToString() == this.ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        #endregion

        #region Overloaded ops
        // overloaded operator +
        public static Point operator +(Point p1, Point p2)
        { return new Point(p1.X + p2.X, p1.Y + p2.Y); }

        // overloaded operator -
        public static Point operator -(Point p1, Point p2)
        { return new Point(p1.X - p2.X, p1.Y - p2.Y); }

        public static Point operator +(Point p1, int change)
        {
            return new Point(p1.X + change, p1.Y + change);
        }

        public static Point operator +(int change, Point p1)
        {
            return new Point(p1.X + change, p1.Y + change);
        }

        // Add 1 to the X/Y values incoming Point.
        public static Point operator ++(Point p1)
        {
            return new Point(p1.X + 1, p1.Y + 1);
        }

        // Subtract 1 from the X/Y values incoming Point.
        public static Point operator --(Point p1)
        {
            return new Point(p1.X - 1, p1.Y - 1);
        }

        // Now let's overload the == and != operators.
        public static bool operator ==(Point p1, Point p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return !p1.Equals(p2);
        }

        public static bool operator <(Point p1, Point p2)
        { return (p1.CompareTo(p2) < 0); }

        public static bool operator >(Point p1, Point p2)
        { return (p1.CompareTo(p2) > 0); }

        public static bool operator <=(Point p1, Point p2)
        { return (p1.CompareTo(p2) <= 0); }

        public static bool operator >=(Point p1, Point p2)
        { return (p1.CompareTo(p2) >= 0); }

        #endregion

        public int CompareTo(Point other)
        {
            if (this.X > other.X && this.Y > other.Y)
                return 1;
            if (this.X < other.X && this.Y < other.Y)
                return -1;
            else
                return 0;
        }
    }

    public struct Rectangle
    {
        public int Width { get; set; }
        public int Hight { get; set; }

        public Rectangle(int w, int h): this()
        {
            Width = w;
            Hight = h;
        }

        public void Draw()
        {
            for (int i = 0; i < Hight; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        public override string ToString()
        {
            return string.Format("[Width = {0}; Height = {1}]", Width, Hight);
        }
    }

    public struct Square
    {
        public int Length { get; set; }

        public Square(int l)
            : this()
        {
            Length = l;
        }

        public void Draw()
        {

            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        public override string ToString()
        { 
            return string.Format("[Length = {0}]", Length); 
        }

        // Rectangles can be explicitly converted
        // into Squares.
        public static explicit operator Square(Rectangle r)
        {
            Square s = new Square();
            s.Length = r.Hight;
            return s;
        }
    }

    static class MyExtensions
    {
        public static void DisplayDefiningAssembly(this object obj)
        {
            Console.WriteLine("{0} lives here: => {1}\n", obj.GetType().Name, Assembly.GetAssembly(obj.GetType()).GetName().Name);
        }

        public static int ReverseDigits(this int i)
        {
            char[] digits = i.ToString().ToCharArray();
            Array.Reverse(digits);
            string newDigits = new string(digits);

            return int.Parse(newDigits);
        }
    }
}
