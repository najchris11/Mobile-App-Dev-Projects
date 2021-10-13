using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cs {
	public class AsyncCPU {
        public static uint[] ary;
        public static Random rng = new Random();
        public static int RandomMilliseconds() {
            return rng.Next(1000);
        }
        public static void SmallTask() {
            Task.Delay(100);
            Console.WriteLine("Done with small task");
        }
        public static uint SlowFib(int N) {
            if (N <= 1)
                return 1;
            else
                return SlowFib(N - 1) + SlowFib(N - 2);
        }
        public static void FillArray(int N) {
            ary = new uint[N];
            for (int i = 0; i < N; i++) {
                ary[i] = SlowFib(i);
                Console.WriteLine(i + " " + ary[i]);
            }
            Console.WriteLine("Done filling array");
        }
        public static void DemoAsyncComputation() {
            Action action = () => { FillArray(40); };
            Task task = new Task(action);
            task.Start();
            SmallTask();
            task.Wait();
            Console.WriteLine("Done with compute intensive task");
        }
        public static void AsynchMain(string[] args) {
            DemoAsyncComputation();
        }
    }
}
