// using Intents; errors?
using Microsoft.Maui.Handlers;
using MobileProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MobileProject.Services
{
    public class APIService
    {
        const string apiKey = "Please Update Your API!";
        const string baseURL = "https://api.apilayer.com/exchangerates_data/";

        HttpClient httpClient;

        public APIService()
        {
            httpClient = new HttpClient();
        }


        public async Task<APIConvertResponse> GetConversion(float amount, string currencyTo, string currencyFrom)
        {
            string fullURL = $"{baseURL}convert?amount={amount}&from{currencyFrom}&to{currencyTo}";

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, fullURL);

            message.Headers.Add("apikey", apiKey);

            HttpResponseMessage response = await httpClient.SendAsync(message);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Failed to bring the requested information from the server.");
            }

            string responseString = await response.Content.ReadAsStringAsync();

            APIConvertResponse? currencyResponse = JsonConvert.DeserializeObject<APIConvertResponse>(responseString);

            if (currencyResponse == null)
            {
                throw new JsonException("Failed to interpret data from the server.");
            }

            return currencyResponse;
        }

        public async Task<APILatestResponse> GetLatest(string strSymbols, CurrencySymbol baseCurrency = CurrencySymbol.USD)
        {
            // AllSymbols = "USD, AUD,EUR,JPY,GBP,CNY,CAD,CHF,HKD,SGD,SEK,KRW,NOK,NZD,INR";

            string strBase = baseCurrency.ToString();

            string fullURL = $"{baseURL}latest?symbols={strSymbols}&base={strBase}";

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, fullURL);

            message.Headers.Add("apikey", apiKey);

            HttpResponseMessage response = await httpClient.SendAsync(message);

            if (!response.IsSuccessStatusCode)
            {
                //string limitMonth = response.Headers.GetValues("x-ratelimit-limit-month").FirstOrDefault();
                //string remainingMonth = response.Headers.GetValues("x-ratelimit-remaining-month").FirstOrDefault();
                //string limitDay = response.Headers.GetValues("x-ratelimit-limit-day").FirstOrDefault();
                //string remainingDay = response.Headers.GetValues("x-ratelimit-remaining-day").FirstOrDefault();
                //throw new HttpRequestException($"[Daily: {remainingDay} / {limitDay}]\n[Monthly: {remainingMonth}/{limitMonth}]\nFailed to bring the historical information from the server");
                throw new HttpRequestException("Failed to bring the latest information from the server");
            }
            else
            {
                Console.WriteLine("----------------------   API Request Number   ---------------------- ");
                string limitMonth = response.Headers.GetValues("x-ratelimit-limit-month").FirstOrDefault();
                Console.WriteLine("x-ratelimit-limit-month: " + limitMonth);
                string remainingMonth = response.Headers.GetValues("x-ratelimit-remaining-month").FirstOrDefault();
                Console.WriteLine("x-ratelimit-remaining-month: " + remainingMonth);
                string limitDay = response.Headers.GetValues("x-ratelimit-limit-day").FirstOrDefault();
                Console.WriteLine("x-ratelimit-limit-day: " + limitDay);
                string remainingDay = response.Headers.GetValues("x-ratelimit-remaining-day").FirstOrDefault();
                Console.WriteLine("x-ratelimit-remaining-day: " + remainingDay);
                Console.WriteLine("-------------------------------------------------------------------- ");
            }

            //Console.WriteLine("----------------------   API Request Number   ---------------------- ");
            //string limitMonth = response.Headers.GetValues("x-ratelimit-limit-month").FirstOrDefault();
            //Console.WriteLine("x-ratelimit-limit-month: " + limitMonth);
            //string remainingMonth = response.Headers.GetValues("x-ratelimit-remaining-month").FirstOrDefault();
            //Console.WriteLine("x-ratelimit-remaining-month: " + remainingMonth);
            //string limitDay = response.Headers.GetValues("x-ratelimit-limit-day").FirstOrDefault();
            //Console.WriteLine("x-ratelimit-limit-day: " + limitDay);
            //string remainingDay = response.Headers.GetValues("x-ratelimit-remaining-day").FirstOrDefault();
            //Console.WriteLine("x-ratelimit-remaining-day: " + remainingDay);
            //Console.WriteLine("-------------------------------------------------------------------- ");

            string responseString = await response.Content.ReadAsStringAsync();

            APILatestResponse? dateResponse = JsonConvert.DeserializeObject<APILatestResponse>(responseString);

            if (dateResponse == null)
            {
                throw new JsonException("Failed to interpret the latest data from the server.");
            }

            return dateResponse;
        }

        public async Task<APIDateResponse> GetHistorical(DateTime historicalDate, string strSymbols)
        {
            string strDate = $"{historicalDate.Year}-{historicalDate.Month:00}-{historicalDate.Day:00}";

            string strBase = "USD";

            string fullURL = $"{baseURL}{strDate}?symbols={strSymbols}&base={strBase}";

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, fullURL);

            message.Headers.Add("apikey", apiKey);

            HttpResponseMessage response = await httpClient.SendAsync(message);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Failed to bring the historical information from the server.");
            }

            //Console.WriteLine("----------------------   API Request Number   ---------------------- ");
            //string limitMonth = response.Headers.GetValues("x-ratelimit-limit-month").FirstOrDefault();
            //Console.WriteLine("x-ratelimit-limit-month: " + limitMonth);
            //string remainingMonth = response.Headers.GetValues("x-ratelimit-remaining-month").FirstOrDefault();
            //Console.WriteLine("x-ratelimit-remaining-month: " + remainingMonth);
            //string limitDay = response.Headers.GetValues("x-ratelimit-limit-day").FirstOrDefault();
            //Console.WriteLine("x-ratelimit-limit-day: " + limitDay);
            //string remainingDay = response.Headers.GetValues("x-ratelimit-remaining-day").FirstOrDefault();
            //Console.WriteLine("x-ratelimit-remaining-day: " + remainingDay);
            //Console.WriteLine("-------------------------------------------------------------------- ");

            string responseString = await response.Content.ReadAsStringAsync();

            APIDateResponse? dateResponse = JsonConvert.DeserializeObject<APIDateResponse>(responseString);

            if (dateResponse == null)
            {
                throw new JsonException("Failed to interpret the historical data from the server.");
            }

            return dateResponse;

        }
    }
}
