using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RestConsole.Helpers;

namespace RestConsole {
	/*
	 * 1) Continues to work provided that that parameter isn't being requested.
	 * 2) It skips over the field.
	 * 3) 1, 2, 3
	 */ 
	public class RestExample {
		/*
		 * Documentation on weather API: https://openweathermap.org/current
		 * You will need to get your own keys:
		 * https://home.openweathermap.org/users/sign_up
		 * https://timezonedb.com/register
		 */

		public static string OpenWeatherMapEndpoint = "https://api.openweathermap.org/data/2.5/weather";
		public static string TimeZoneDBEndpoint = "http://api.timezonedb.com/v2.1/get-time-zone";
		// YOU WILL NEED TO REGISTER FROM YOUR OWN KEY AND INSERT THAT KEY INTO secrets.json
		public static string OpenWeatherMapAPIKey = Secrets.OWAPIKEY;
		public static string TimeZondDBAPIKey = Secrets.TZAPIKEY;
		public HttpClient client = new HttpClient();
		public string CreateWeatherQuery(string cityName) {
			string requestUri = OpenWeatherMapEndpoint;
			requestUri += $"?q={cityName}";
			requestUri += "&units=imperial"; // or units=metric
			requestUri += $"&APPID={OpenWeatherMapAPIKey}";
			return requestUri;
		}
		public string CreateTZQuery(string timeZone)
        {
			string requestURL = TimeZoneDBEndpoint;
			requestURL += $"?key={TimeZondDBAPIKey}";
			requestURL += $"&format=json";
			requestURL += $"&by=zone";
			requestURL += $"&zone={timeZone}";
			return requestURL;
        }
		public async Task<string> GetWeatherQueryResult() {
			Console.Write("Please enter a big US city: ");
			string city = Console.ReadLine().Trim();
			string query = CreateWeatherQuery(city);
			string result = null;

			try {
				var response = await client.GetAsync(query);
				if (response.IsSuccessStatusCode) {
					result = await response.Content.ReadAsStringAsync();
				}
			}
			catch (Exception ex) {
				Debug.WriteLine("\t\tERROR {0}", ex.Message);
				Environment.Exit(0);
			}

			return result;
		}
		public async Task<string> GetTZQueryResult()
        {
			Console.Write("Please enter a Time Zone: ");
			string timezone = Console.ReadLine().Trim();
			string query = CreateTZQuery(timezone);
			string result = null;

			try
            {
				var respoonse = await client.GetAsync(query);
				if (respoonse.IsSuccessStatusCode)
                {
					result = await respoonse.Content.ReadAsStringAsync();
                }
            } catch (Exception e)
            {
				Debug.WriteLine("\t\tError {0}", e.Message);
				Environment.Exit(0);
            }
			return result;
        }
		public void ProcessWeatherQuery() {
			string response = GetWeatherQueryResult().Result;
			WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(response);
			Console.WriteLine("Using WeatherData class");
			Console.WriteLine(weatherData.Title);
			Console.WriteLine(weatherData.Visibility);
			Console.WriteLine(weatherData.Main.Temperature);
			Console.WriteLine(weatherData.Wind.Speed);

			var smallDef = new { name = "", visibility = 0 };
			var smallObj = JsonConvert.DeserializeAnonymousType(response, smallDef);
			Console.WriteLine("\nUsing small anonymous class");
			Console.WriteLine(smallObj.name + " " + smallObj.visibility);
		}
		public void ProcessTZQuery()
        {
			string response = GetTZQueryResult().Result;
			TimeZoneData tz = JsonConvert.DeserializeObject<TimeZoneData>(response);

			Console.WriteLine("Using TimeZoneData class");
			Console.WriteLine(tz.time);
			Console.WriteLine(tz.zoneAbbrev + ", " + tz.countryCode);
			Console.WriteLine(tz.gmtOffset);
        }
		public static void Main(string[] args) {
			RestExample rest = new RestExample();
			rest.ProcessTZQuery();
		}
	}
}
