using System;

namespace cs {
    public class Params {
        public static void swap<T>(ref T a, ref T b) {
            T tmp = a;
            a = b;
            b = tmp;
        }
        public static void swapI(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }
        public static void f(int a, out int b, ref int c) {
            int x = 1;
            a = 1;      // value param
                        // x = b;	// illegal, b must be set first
            b = 2;
            c = 0;
            x = c;
        }
        public static void GetMin(int[] array, out int min) {
            min = Int32.MaxValue;
            foreach (int v in array)
                min = Math.Min(min, v);
        }
        public static void ParamsMain(string [] args) {
            int[] a1 = { 1, 5, -3, 234 };
            int min;
            GetMin(a1, out min);
            Console.WriteLine(min);
            //return;

            int a = 1, b, c = 3;
            f(a, out b, ref c);

            int x = 0, y = 1;
            swap(ref x, ref y);
            Console.WriteLine(x + " " + y);

            string m = "hello", n = "world";
            swap(ref m, ref n);
            Console.WriteLine(m + " " + n);
        }
    }
}
