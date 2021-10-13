using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace ZipcodesFromWeb
{
    public partial class MainPage : ContentPage
    {
        public string ZippopotomusEndpoint = "https://api.zippopotam.us";
        public HttpClient client = new HttpClient();
        public string CreateZipQuery(string countryName, string stateName, string cityName)
        {
            string requestURL = ZippopotomusEndpoint;
            requestURL += $"/{countryName}";
            requestURL += $"/{stateName}";
            requestURL += $"/{cityName}";
            return requestURL;
        }

        public async Task<string> GetZippopotamusQueryResult()
        {
            string countryCode = Countryinfo.Text.Trim();
            string stateCode = Stateinfo.Text.Trim();
            string cityName = Cityinfo.Text.Trim();
            string query = CreateZipQuery(countryCode, stateCode, cityName);
            string result = null;

            try
            {
                var response = await client.GetAsync(query).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    requestStatus.Text = "OK";
                }
            }
            catch (Exception e)
            {
                requestStatus.Text = "ERROR: " + e.Message;
                //return "attempt failed";
                //Environment.Exit(0);
            }
            return result;
        }
        public void ProcessZippopotomusQuery()
        {
            Task<String> task = GetZippopotamusQueryResult();
            if (!task.IsCompleted)
            {
                task.Wait();
            }
            string response = task.Result;
            ZipCode zipData = JsonConvert.DeserializeObject<ZipCode>(response);
            ReturnedDataList.ItemsSource = zipData.places;
        }
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ProcessZippopotomusQuery();
        }
    }
}
