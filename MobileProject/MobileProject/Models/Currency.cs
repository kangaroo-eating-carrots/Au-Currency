using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProject.Models
{
    public class Currency
    {
        public string FullName { get; set; }
        public CurrencySymbol Symbol { get; set; }
        public string Country { get; set; }
        public string ImagePath { get; set; }
        public float LatestRatePerUSD { get; set; }
        
        // Daily rates per USD for 10 days
        public List<float> RatesTenDays { get; set; }

        // Monthly rates per USD for 1 year
        public List<float> RatesOneYear { get; set; }

        // Quarterly rates per USD for 3 years
        public List<float> RatesThreeYears { get; set; }



        public Currency(string fullName, CurrencySymbol symbol, string country, string imagePath)
        {
            FullName = fullName;
            Symbol = symbol;
            Country = country;
            ImagePath = imagePath;
            LatestRatePerUSD = 0;

            RatesTenDays = new List<float>();
            RatesOneYear = new List<float>();
            RatesThreeYears = new List<float>();
        }

        public void UpdateLatest(float latestRate)
        {
            // Stop if the latest rate value is unreasonable
            if (latestRate < 0)
            {
                return;
            }

            LatestRatePerUSD = latestRate;
        }

        public void UpdateTrendTenDays(List<float> updatedTrend)
        {
            RatesTenDays = [];

            foreach (float rate in updatedTrend)
            {
                RatesTenDays.Add(rate);
            }
        }

        public void UpdateTrendOneYear(List<float> updatedTrend)
        {
            RatesOneYear = [];

            foreach (float rate in updatedTrend)
            {
                RatesOneYear.Add(rate);
            }
        }

        public void UpdateTrendThreeYears(List<float> updatedTrend)
        {
            RatesThreeYears = [];

            foreach (float rate in updatedTrend)
            {
                RatesThreeYears.Add(rate);
            }
        }

        public override string ToString()
        {
            string currencyExplanation;
            currencyExplanation = $"{FullName} ({Symbol})";
            return currencyExplanation;
        }

    }

    public enum CurrencySymbol
    {
        USD,
        AUD,
        EUR,
        JPY,
        GBP,
        CNY,
        CAD,
        CHF,
        HKD,
        SGD,
        SEK,
        KRW,
        NOK,
        NZD,
        INR
    }
}
