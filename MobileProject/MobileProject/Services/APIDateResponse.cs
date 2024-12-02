using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProject.Services
{
    public class APIDateResponse
    {
        [JsonProperty("_base")]
        public string Base { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("historical")]
        public bool Historical { get; set; }

        [JsonProperty("rates")]
        public Rates Rates { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }
    }

    public class Rates
    {
        [JsonProperty("USD")]
        public float USD { get; set; }

        [JsonProperty("AUD")]
        public float AUD { get; set; }

        [JsonProperty("EUR")]
        public float EUR { get; set; }

        [JsonProperty("JPY")]
        public float JPY { get; set; }

        [JsonProperty("GBP")]
        public float GBP { get; set; }

        [JsonProperty("CNY")]
        public float CNY { get; set; }

        [JsonProperty("CAD")]
        public float CAD { get; set; }

        [JsonProperty("CHF")]
        public float CHF { get; set; }

        [JsonProperty("HKD")]
        public float HKD { get; set; }

        [JsonProperty("SGD")]
        public float SGD { get; set; }

        [JsonProperty("SEK")]
        public float SEK { get; set; }

        [JsonProperty("KRW")]
        public float KRW { get; set; }

        [JsonProperty("NOK")]
        public float NOK { get; set; }

        [JsonProperty("NZD")]
        public float NZD { get; set; }

        [JsonProperty("INR")]
        public float INR { get; set; }
    }

}
