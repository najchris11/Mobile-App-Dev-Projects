using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ZipcodesFromWeb
{
    class ZipCode
    {
        [JsonProperty("post code")]
        public string zipCode { get; set; }

        [JsonProperty("places")]
        public List<Place> places { get; set; }
        
    }
    class Place
    {
        [JsonProperty("place name")]
        public string placeName { get; set; }

        [JsonProperty("post code")]
        public string postCode { get; set; }

        [JsonProperty("latitude")]
        public string lat { get; set; }

        [JsonProperty("longitude")]
        public string lon { get; set; }

        public override string ToString()
        {
            return String.Format("{0} ({1}), ({2}, {3})", placeName, postCode, lat, lon);
            
        }
    }
}
