using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestConsole
{
    class TimeZoneData
    {
        [JsonProperty("countryCode")]
        public string countryCode { get; set; }
        [JsonProperty("countryName")]
        public string country { get; set; }
        [JsonProperty("regionName")]
        public string region { get; set; }
        [JsonProperty("cityName")]
        public string city { get; set; }
        [JsonProperty("zoneName")]
        public string zone { get; set; }
        [JsonProperty("abbreviation")]
        public string zoneAbbrev { get; set; }
        [JsonProperty("gmtOffset")]
        public int gmtOffset { get; set; }
        //[JsonProperty("dst")]
        //public string dst { get; set; }
        [JsonProperty("zoneStart")]
        public string zoneStart { get; set; }
        [JsonProperty("zoneEnd")]
        public string zoneEnd { get; set; }
        [JsonProperty("nextAbbreviation")]
        public string nextZone { get; set; }
        [JsonProperty("timestamp")]
        public string timeDT { get; set; }
        [JsonProperty("formatted")]
        public string time { get; set; }
    }
}
