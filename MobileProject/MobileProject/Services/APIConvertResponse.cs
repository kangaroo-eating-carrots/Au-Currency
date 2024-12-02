using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MobileProject.Services
{
    public class APIConvertResponse
    {

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("historical")]
        public string Historical { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("query")]
        public Query Query { get; set; }

        [JsonProperty("result")]
        public float Result { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }

    public class Info
    {
        [JsonProperty("rate")]
        public float Rate { get; set; }

        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }
    }

    public class Query
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }

}