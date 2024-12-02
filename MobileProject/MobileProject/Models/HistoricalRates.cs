using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MobileProject.Models
{
    public class HistoricalRates
    {
        [PrimaryKey]
        public int Id { get; set; }
        public float USD { get; set; }
        public float AUD { get; set; }
        public float EUR { get; set; }
        public float JPY { get; set; }
        public float GBP { get; set; }
        public float CNY { get; set; }
        public float CAD { get; set; }
        public float CHF { get; set; }
        public float HKD { get; set; }
        public float SGD { get; set; }
        public float SEK { get; set; }
        public float KRW { get; set; }
        public float NOK { get; set; }
        public float NZD { get; set; }
        public float INR { get; set; }
    }
}
