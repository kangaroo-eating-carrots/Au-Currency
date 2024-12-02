using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProject.Services
{
    public class APILatestResponse
    {
        [JsonProperty("_base")]
        public string Base { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("rates")]
        public Rates Rates { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("Timestamp")]
        public int Timestamp { get; set; }
    }
}
