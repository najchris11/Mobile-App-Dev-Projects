using System;

namespace Distance
{
    class Distance
    {
        static void Main(string[] args)
        {
            USLocations.USLocations db = new USLocations.USLocations("zipcodes.tsv");
            Console.Write("distance> ");
            string input = Console.ReadLine();
            while (!(input.Equals("exit"))) {
                int zip1 = Int32.Parse(input.Substring(0, 5));
                int zip2 = Int32.Parse(input.Substring(6));

                double km = db.Distance(zip1, zip2),
                    mi = km / 1.609;

                Console.WriteLine("The distance between {0} and {1} is {2}mi ({3} km)", zip1, zip2, Math.Round(mi, 2), Math.Round(km, 2));
                Console.Write("distance> ");
                input = Console.ReadLine();
            }
            Console.WriteLine("Goodbye!");
        }
    }
}
