using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cs {

    public class Asynch {
        public static ulong LoadData(string filename) {
            StreamReader reader = new StreamReader(filename);
            ulong cnt = 0;
            int iter = 0;
            while (!reader.EndOfStream) {
                string line = reader.ReadLine();
                cnt += (ulong)line.Length;
                if (++iter % 1000000 == 0)
                    Console.WriteLine(iter);
            }
            reader.Close();
            Console.WriteLine("Done reading data: " + cnt);
            return cnt;
        }
        public static async Task<ulong> LoadDataAsync(string filename) {
            StreamReader reader = new StreamReader(filename);
            ulong cnt = 0;
            int iter = 0;
            while (!reader.EndOfStream) {
                string line = await reader.ReadLineAsync();
                cnt += (ulong)line.Length;
                if (++iter % 1000000 == 0)
                    Console.WriteLine(iter);
            }
            reader.Close();
            Console.WriteLine("Done reading data asynchronously");
            return cnt;
        }
        public static void SmallTask() {
            Task.Delay(100);
            Console.WriteLine("Done with small task");
        }
        public static void DemoSyncLoadData() {
            ulong charCount = LoadData("big.txt");
            SmallTask();
            Console.WriteLine("Done with demo " + charCount);
        }
        public static void DemoAsyncLoadData() {
            Task<ulong> dataTask = LoadDataAsync("big.txt");
            SmallTask(); 
            dataTask.Wait();
            ulong charCount = dataTask.Result;
            Console.WriteLine("Done with demo " + charCount);
        }
        public static void AsynchMain(string [] args) {
            //DemoSyncLoadData();
            DemoAsyncLoadData();
        }
    }
}
