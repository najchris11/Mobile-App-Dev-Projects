using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cs {
	public class Quizzes {
        public static Random rng = new Random();
        public static int RandomMilliseconds() {
            return rng.Next(1000);
        }
        public static void A() {
            Console.WriteLine("A Start");
            Thread.Sleep(RandomMilliseconds());
            Console.WriteLine("A Ending");
        }
        public static async Task B() {
            Console.WriteLine("B Start");
            await Task.Delay(0);
            Console.WriteLine("B End");
        }
        public static async Task C() {
            Console.WriteLine("C Start");
            await Task.Delay(RandomMilliseconds());
            Console.WriteLine("C End");
        }
        public static void First() {
            Task t1 = B(); 
            Task t2 = C(); 
            t2.Wait(); 
            t1.Wait();
        }
        public static void Second() {
            Task t1 = B();
            A(); 
            t1.Wait();
            Task t2 = C();
            t2.Wait();
        }
        public static void QuizzesMain(String [] args) {
            Second();
		}
	}
}
