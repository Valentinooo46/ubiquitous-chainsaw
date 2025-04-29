using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace NovaPoshta
{
    public class NPRequest
    {
        [JsonProperty("methodProperties")] public methodProperties methodProperties { get; init; }

        [JsonProperty("modelName")] public string modelName { get; init; }

        [JsonProperty("apiKey")] public string apiKey{get;init; }

        [JsonProperty("calledMethod")] public string calledMethod { get; init; }
    }
    public class methodProperties
    {
        [JsonProperty("CityName")] public string? CityName { get; set; }
        [JsonProperty("Limit")] public string? Limit { get; set; }
        [JsonProperty("Page")] public string? Page { get; set; }
        public methodProperties( string? limit = null, string? page = null, string? cityName = null)
        {
            CityName = cityName;
            Limit = limit;
            Page = page;
        }

    }
}
