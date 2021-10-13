using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Threading.Tasks;

namespace USLocations {

	public class USLocations
	{
		Dictionary<string, ArrayList> states = new Dictionary<string, ArrayList>();
		Dictionary<string, string[]> locations = new Dictionary<string, string[]>();
		int zip = 1,
			city = 3,
			state = 4,
			lat = 6,
			lon = 7;
		Task loadingTask;
        // This constructor will initiate the loading of the TSV file.
        // The constructor must return very quickly, perhaps before all 
        // of the zip code information has been completely loaded. Tasks
        // will be needed to do this.
        public USLocations(string fileName)
		{
			loadingTask = loadData(fileName);


			//using (StreamReader input = new StreamReader(fileName))
			//{
			//	input.ReadLine();
			//	while (!input.EndOfStream)
			//	{
			//		string line = input.ReadLine();
			//		string[] tabLine = line.Split("\t");
			//		locations[tabLine[zip]] = tabLine;
			//		if (states.ContainsKey(tabLine[state]))
   //                 {
			//			states.GetValueOrDefault(tabLine[state]).Add(tabLine);
   //                 }else
   //                 {
			//			states.Add(tabLine[state], new ArrayList() { tabLine });

			//		}
			//	}
			//}
		}

		public async Task loadData(string fileName)
        {
			using (StreamReader input = new StreamReader(fileName))
			{
				input.ReadLine();
				while (!input.EndOfStream)
				{
					string line = await input.ReadLineAsync();
					string[] tabLine = line.Split("\t");
					locations[tabLine[zip]] = tabLine;
					if (states.ContainsKey(tabLine[state]))
					{
						states.GetValueOrDefault(tabLine[state]).Add(tabLine);
					}
					else
					{
						states.Add(tabLine[state], new ArrayList() { tabLine });

					}
				}
			}
		}
		/**
		 * Returns the city names that appear in both of the given states.
			 * "OH" and "MI" would yield {OXFORD, FRANKLIN, ... }
		 */
		public ISet<string> GetCommonCityNames(string state1, string state2)
		{
			if (!(loadingTask.IsCompleted))
            {
				loadingTask.Wait();
            }
			ISet<string> commonCities = new HashSet<string>();
			foreach (string[] s1 in states.GetValueOrDefault(state1))
			{
				foreach (string[] s2 in states.GetValueOrDefault(state2))
				{
					if (s1[city].Equals(s2[city]))
					{
						commonCities.Add(s1[city]);
					}
				}

			}
			return commonCities;
		}

		/**
		 * Returns all zipcodes that are within a specified distance from a
		 * particular zipcode.
		 */
		// Do this by researching the "Haversine" formula to do this one.
		// The formulat for converting from degrees to radians is:
		//     radians = degrees * Math.PI / 180.0;
		public double toRadians(double angle)
		{
			return Math.PI * angle / 180.0;
		}
		public double Distance(int zip1, int zip2)
		{
			if (!(loadingTask.IsCompleted))
			{
				loadingTask.Wait();
			}
			double lat1 = Double.Parse(locations.GetValueOrDefault("" + zip1)[lat]),
				lon1 = Double.Parse(locations.GetValueOrDefault("" + zip1)[lon]),
				lat2 = Double.Parse(locations.GetValueOrDefault("" + zip2)[lat]),
				lon2 = Double.Parse(locations.GetValueOrDefault("" + zip2)[lon]);

			return calculate
				(lat1, lon1, lat2, lon2);

		}
		public double calculate(double lat1, double lon1, double lat2, double lon2)
		{
			var R = 6372.8; // In kilometers
			var dLat = toRadians(lat2 - lat1);
			var dLon = toRadians(lon2 - lon1);
			lat1 = toRadians(lat1);
			lat2 = toRadians(lat2);

			var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
			var c = 2 * Math.Asin(Math.Sqrt(a));
			return R * c;
		}

	}
}
	