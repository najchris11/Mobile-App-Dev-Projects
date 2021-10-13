using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
/*
 * Answers:
 * 
 * Time to run:
 *      List: 16.8 seconds for 1000 iterations of storing words.txt
 *      SortedSet: 5 minutes and 15.65 seconds for 1000 iterations of storing words.txt
 *      HashSet: 21.31 seconds for 1000 iterations of storing words.txt
 *      The SortedSet was much slower at storing data than the other two. The List was the fastest.
 *      
 *  Time to search:
 *      List: 1 minute and 24.38 seconds for 1000 iterations of checking if words were legal
 *      SortedSet: 27 minutes and 21.24 seconds for 1000 iterations of checking if words were legal
 *      HashSet: 1 minute and 46 seconds for 1000 iterations of checking if words were legal
 *      There was a big difference between the SortedSet and the other two options. The List was the fastest.
 *      
 *  Three letter words: 
 *      There are 972 three letter words
 */

namespace cs {
    public class IO {
        public static Stopwatch sw = new Stopwatch();
        public static void IOMain(string[] args) {
            /* using (StreamWriter output = new StreamWriter("sorted.txt"))
            using (StreamReader input = new StreamReader("items.csv")) {
                while (!input.EndOfStream) {
                    string line = input.ReadLine();
                    string[] toks = line.Split(',');
                    int[] values = new int[toks.Length];
                    for (int i = 0; i < toks.Length; i++)
                        values[i] = Int32.Parse(toks[i]);
                    Array.Sort(values);
                    foreach (int value in values) {
                        output.Write(value + " ");
                    }
                    output.WriteLine();
                }
            } */

            //Loop to ensure that methods are working
            /* foreach (string s in ReadWordsHashSet("words.txt"))
            {
                Console.WriteLine(s);
            } */

            //Loop to measure how long was spent on each process
            startSW();
            string[] words = { "COMPUTERS", "ZYMOTIC", "AARDVARK", "WORD", "DATABASIC" };
            int count;
            //There are four legal words in the array words
            for (int i = 0; i < 10; i++)
            {
                count = 0;
                foreach(string s in words)
                {
                    if (ReadWordsList("words.txt").Contains(s))
                    {
                        count++;
                    }
                }
                //Console.WriteLine(count);
            }
            stopSW();

            //Loop to determine 3 letter words
            /* int count = 0;
            foreach (string s in ReadWordsList("words.txt"))
            {
                if (s.Length == 3) count++;
            }
            Console.WriteLine(count); */
        }

        //This method is complete and correct
        public static List<string> ReadWordsList(string fileName)
        {
            List<string> result = new List<string>();
            //startSW();
            using (StreamReader input = new StreamReader(fileName))
            {
                while (!input.EndOfStream)
                {
                    string line = input.ReadLine();
                    result.Add(line);
                }
            }
            //stopSW();
            return result;
        }

        //This method is complete and correct
        public static SortedSet<string> ReadWordsSortedSet(string fileName)
        {
            SortedSet<string> result = new SortedSet<string>();
            //startSW();
            using (StreamReader input = new StreamReader(fileName))
            {
                while (!input.EndOfStream)
                {
                    string line = input.ReadLine();
                    result.Add(line);
                }
            }
            //stopSW();
            return result;
        }

        //This method is complete and correct
        public static HashSet<string> ReadWordsHashSet(string fileName)
        {
            HashSet<string> result = new HashSet<string>();
            //startSW();
            using (StreamReader input = new StreamReader(fileName))
            {
                while (!input.EndOfStream)
                {
                    string line = input.ReadLine();
                    result.Add(line);
                }
            }
            //stopSW();
            return result;
        }


        //Methods to control timing
        public static void startSW()
        {
            sw.Restart();
            sw.Start();
        }
        
        public static void stopSW()
        {
            sw.Stop();

            TimeSpan ts = sw.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}
