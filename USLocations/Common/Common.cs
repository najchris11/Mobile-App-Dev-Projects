using System;
using System.Collections.Generic;

namespace Common
{
    class Common
    {
        static void Main(string[] args)
        {
            USLocations.USLocations db = new USLocations.USLocations("zipcodes.tsv");
            Console.Write("commons> ");
            string input = Console.ReadLine();
            while (!(input.Equals("exit")))
            {
                string s1 = input.Substring(0, 2),
                    s2 = input.Substring(3);

                ISet<string> list = db.GetCommonCityNames(s1, s2);

                foreach (string s in list)
                {
                    Console.WriteLine(s);
                }

                Console.Write("commons> ");
                input = Console.ReadLine();
            }
            Console.WriteLine("Goodbye!");
        }
    }
}
