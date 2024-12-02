using MobileProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Currency = MobileProject.Models.Currency;
using Newtonsoft.Json;
// using Android.Content.OM;


namespace MobileProject.Services
{
    public class ServiceController
    {
        // When making ServiceController object, top 15 currency properties will be added with detailed information
        private List<CurrencySymbol> allSymbols = Enum.GetValues(typeof(CurrencySymbol)).Cast<CurrencySymbol>().ToList();
        public  string AllSymbolStr { get; set; }

        //
        public Currency CurrencyUSD { get; set; }
        public Currency CurrencyAUD { get; set; }
        public Currency CurrencyEUR { get; set; }
        public Currency CurrencyJPY { get; set; }
        public Currency CurrencyGBP { get; set; }
        public Currency CurrencyCNY { get; set; }
        public Currency CurrencyCAD { get; set; }
        public Currency CurrencyCHF { get; set; }
        public Currency CurrencyHKD { get; set; }
        public Currency CurrencySGD { get; set; }
        public Currency CurrencySEK { get; set; }
        public Currency CurrencyKRW { get; set; }
        public Currency CurrencyNOK { get; set; }
        public Currency CurrencyNZD { get; set; }
        public Currency CurrencyINR { get; set; }

        public List<Currency> AllCurrencies { get; set; }


        // To deal with API service
        public APIService APIServer { get; set; }

        // To deal with SQL service
        private SQLService databaseService = default!;
        public SQLService DatabaseService { get; set; }

        // Properties set in Setting page
        public Currency MyCurrency { get; set; }
        public Currency InterestingOne { get; set; }
        public Currency InterestingTwo{ get; set; }

        public CurrencySymbol MyCurrencySymbol { get; set; }
        public CurrencySymbol InterestingOneSymbol { get; set; }
        public CurrencySymbol InterestingTwoSymbol { get; set; }




        


        private DateTime baseDate = new DateTime(1900, 1, 1);

        // Historical data is updated next day at 00:05AM as London Time (Perth Time + 7 hours)
        private int dailyUpdatingTime = 455; // 12:05AM as London Time (7:05Am as Perth Time) + 30 min for opearating safety
        private int updatingSpanToMinutes = 3305; // Span from 00:00AM on the latest updated date: 48 hours + 7 hours + 5 minutes
        public DateTime UpdatedTimeForLatest { get; set; }
        public DateTime UpdatedTimeForHistorical { get; set; }

        
        // Date and rate lists for making trending graph
        public List<DateTime> DatesForTenDays { get; set; }
        public List<DateTime> DatesForOneYear { get; set; }
        public List<DateTime> DatesForThreeYears { get; set; } 

        // Primary Key list from DateTime List
        public List<int> PrimaryKeysForTenDays { get; set; }
        public List<int> PrimaryKeysForOneYear { get; set; }
        public List<int> PrimaryKeysForThreeYears { get; set; }


        public List<float> RatesForTenDaysGraphFirst { get; set; }
        public List<float> RatesForTenDaysGraphSecond { get; set; }

        public List<float> RatesForOneYearGraph { get; set; }
        public List<float> RatesForThreeYearGraph { get; set; }

        //ToBeDeleted
        //To check whether events use API request
        public int ApiRequestNumber;
        public string ApiUseMessage;

        public ServiceController(CurrencySymbol myCurrency, CurrencySymbol interestingOne, CurrencySymbol interestingTwo)
        {
            APIServer = new APIService();

            MyCurrencySymbol = myCurrency;
            InterestingOneSymbol = interestingOne;
            InterestingTwoSymbol = interestingTwo;

            UpdatedTimeForLatest = baseDate;
            UpdatedTimeForHistorical = baseDate;

            DatesForTenDays = new List<DateTime>();
            DatesForOneYear = new List<DateTime>();
            DatesForThreeYears = new List<DateTime>();

            CurrencyUSD = new Currency("United States Dollar", CurrencySymbol.USD, "United States of America", "Images/usd.jpg");
            CurrencyAUD = new Currency("Australia Dollar", CurrencySymbol.AUD, "Commonwealth of Australia", "Images/aud.jpeg");
            CurrencyEUR = new Currency("European Union", CurrencySymbol.EUR, "Euro", "Images/eur.jpg");
            CurrencyJPY = new Currency("Japanese Yen", CurrencySymbol.JPY, "Japan", "Images/jpy.png");
            CurrencyGBP = new Currency("Great British Pound", CurrencySymbol.GBP, "United Kingdom", "Images/gbp.jpg");
            CurrencyCNY = new Currency("Chinese Yuan Renminbi", CurrencySymbol.CNY, "People's Repub. of China", "Images/cny.jpg");
            CurrencyCAD = new Currency("Canadian Dollar", CurrencySymbol.CAD, "Dominion of Canada", "Images/cad.png");
            CurrencyCHF = new Currency("Swiss Franc", CurrencySymbol.CHF, "Swiss Confederation", "Images/chf.jpg");
            CurrencyHKD = new Currency("Hong Kong Dollar", CurrencySymbol.HKD, "Hong Kong", "Images/hkd.jpg");
            CurrencySGD = new Currency("Singapore Dollar", CurrencySymbol.SGD, "Republic of Singapore", "Images/sgd.jpg");
            CurrencySEK = new Currency("Swedish Krona", CurrencySymbol.SEK, "Kingdom of Sweden", "Images/sek.jpeg");
            CurrencyKRW = new Currency("Korean Won", CurrencySymbol.KRW, "Republic of Korea", "Images/krw.jpg");
            CurrencyNOK = new Currency("Norwegian Krone", CurrencySymbol.NOK, "Kingdom of Norway", "Images/nok.jpg");
            CurrencyNZD = new Currency("New Zealand Dollar", CurrencySymbol.NZD, "New Zealand", "Images/nzd.jpg");
            CurrencyINR = new Currency("Indian Rupee", CurrencySymbol.INR, "Republic of India", "Images/inr.jpg");
            
            AllCurrencies = [CurrencyUSD, CurrencyAUD, CurrencyEUR, CurrencyJPY, CurrencyGBP, 
                CurrencyCNY, CurrencyCAD, CurrencyCHF, CurrencyHKD, CurrencySGD, 
                CurrencySEK, CurrencyKRW, CurrencyNOK, CurrencyNZD, CurrencyINR];

            UpdateTargetCurrencies();

            if (databaseService == null)
            {
                databaseService = new SQLService(
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "currencies.db")
                    );

                DatabaseService = databaseService;
            }
            else
            {
                DatabaseService = databaseService;
            }

            AllSymbolStr = string.Empty;
            foreach (CurrencySymbol symbol in allSymbols)
            {
                if (String.IsNullOrEmpty(AllSymbolStr))
                {
                    AllSymbolStr += symbol.ToString();
                    continue;
                }

                AllSymbolStr += "," + symbol.ToString();
            }

            //ToBeDeleted
            ApiRequestNumber = 0;
            ApiUseMessage = "The App used API";


        }

        public bool DatabaseExists()
        {
            string test_path = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "currencies.db");

            return File.Exists(test_path);
        }

        // ToBeDeleted
        public int GetApiRequestNumber()
        {
            int requestNumber = ApiRequestNumber;
            ApiRequestNumber = 0;
            return requestNumber;
        }

        public void UpdateTargetCurrencies()
        {
            foreach (Currency currency in AllCurrencies)
            {
                if (currency.Symbol == MyCurrencySymbol)
                {
                    MyCurrency = currency;
                }

                if (currency.Symbol == InterestingOneSymbol)
                {
                    InterestingOne = currency;
                }

                if (currency.Symbol == InterestingTwoSymbol)
                {
                    InterestingTwo = currency;
                }
            }
        }

        // Check whether 30 minutes passed from the last updated time
        public bool CheckLatestUpdate()
        {
            DateTime baseTime = new DateTime(1900, 1, 1);
            TimeSpan timeSpan;
            Double spanToMinutes;

            if (UpdatedTimeForLatest == baseTime)
            {
                UpdatedTimeForLatest = DateTime.Now;
                return true;
            }
            else
            {
                timeSpan = DateTime.Now - UpdatedTimeForLatest;
                spanToMinutes = timeSpan.TotalMinutes;
            }

            if (spanToMinutes > 30)
            {
                UpdatedTimeForLatest = DateTime.Now;
                return true;
            }

            return false;
        }

        public async Task CurrentRateUpdate()
        {
            APILatestResponse dataResponse = await APIServer.GetLatest(AllSymbolStr); ;
            if (dataResponse.Success)
            {
                CurrencyAUD.UpdateLatest(dataResponse.Rates.AUD);
                CurrencyUSD.UpdateLatest(dataResponse.Rates.USD);
                CurrencyEUR.UpdateLatest(dataResponse.Rates.EUR);
                CurrencyJPY.UpdateLatest(dataResponse.Rates.JPY);
                CurrencyGBP.UpdateLatest(dataResponse.Rates.GBP);
                CurrencyCNY.UpdateLatest(dataResponse.Rates.CNY);
                CurrencyCAD.UpdateLatest(dataResponse.Rates.CAD);
                CurrencyCHF.UpdateLatest(dataResponse.Rates.CHF);
                CurrencyHKD.UpdateLatest(dataResponse.Rates.HKD);
                CurrencySGD.UpdateLatest(dataResponse.Rates.SGD);
                CurrencySEK.UpdateLatest(dataResponse.Rates.SEK);
                CurrencyKRW.UpdateLatest(dataResponse.Rates.KRW);
                CurrencyNOK.UpdateLatest(dataResponse.Rates.NOK);
                CurrencyNZD.UpdateLatest(dataResponse.Rates.NZD);
                CurrencyINR.UpdateLatest(dataResponse.Rates.INR);

                // ToBeDeleted
                ApiRequestNumber += 1;
            }

        }


        // Main Page 1: CheckLatestUpdate & if (true) => API(전체 화폐 Rate 변경) 가 뭔저 와야함.
        public float CurrentRateConverter(Currency numeratorCur, Currency denominatorCur)
        {
            float currentRate = numeratorCur.LatestRatePerUSD / denominatorCur.LatestRatePerUSD;
            return currentRate;
        }


        // Main Page 3: CheckLatestUpdate & if (true) => API(전체 화폐 Rate 변경) 가 뭔저 와야함.
        public float CurrentRateSumConverter(Currency targetCurrency, Currency curOne, float curOneAmount, Currency curTwo, float curTwoAmount, Currency curThree, float curThreeAmount, Currency curFour, float curFourAmount, Currency curFive, float curFiveAmount, Currency curSix, float curSixAmount, Currency curSeven, float curSevenAmount)
        {
            float sumUSD = (curOneAmount / curOne.LatestRatePerUSD) + (curTwoAmount / curTwo.LatestRatePerUSD) + (curThreeAmount / curThree.LatestRatePerUSD) + 
                (curFourAmount / curFour.LatestRatePerUSD) + (curFiveAmount / curFive.LatestRatePerUSD) + (curSixAmount / curSix.LatestRatePerUSD) + (curSevenAmount / curSeven.LatestRatePerUSD);

            float sumTargetCurrency = sumUSD * targetCurrency.LatestRatePerUSD;
            return sumTargetCurrency;
        }


        // Check whether the latest day has been changed for the historical data.
        // Historical data is updated next day at 00:05AM as London Time (Perth Time + 7 hours)
        public bool CheckHistoricalUpdate()
        {
            TimeSpan spanFromLatestDate = DateTime.Now - UpdatedTimeForHistorical;

            if (spanFromLatestDate.TotalMinutes >= updatingSpanToMinutes)
            {
                int currentYear = DateTime.Now.Year;
                int currentMonth = DateTime.Now.Month;
                int currentDay = DateTime.Now.Day;
                int currentHour = DateTime.Now.Hour;
                int currentMinute = DateTime.Now.Minute;

                if ((currentHour * 60 + currentMinute) >= dailyUpdatingTime)
                {
                    UpdatedTimeForHistorical = new DateTime(currentYear, currentMonth, currentDay - 1);
                }
                else
                {
                    UpdatedTimeForHistorical = new DateTime(currentYear, currentMonth, currentDay - 2);
                }

                return true;
            }

            return false;
            
        }


        // To update Ten Days from the lastestly updated historical date
        // Day difference between the date and baseDate will be used as priamry key in DB
        public void UpdateTenDays()
        {
            DatesForTenDays = [];
            PrimaryKeysForTenDays = [];

            for (int i = 10; i > 0; i--)
            {
                DatesForTenDays.Add(UpdatedTimeForHistorical.AddDays(-i+1));
            }

            foreach (DateTime dateTime in DatesForTenDays)
            {
                int primaryKey = (dateTime - baseDate).Days;
                PrimaryKeysForTenDays.Add(primaryKey);
            }
        }
        

        // To update end dates for 12 months from the lastestly updated historical date
        public void UpdateMonthsForOneYear()
        {
            DatesForOneYear = [];
            PrimaryKeysForOneYear = [];
            int endDay = DateTime.DaysInMonth(UpdatedTimeForHistorical.Year, UpdatedTimeForHistorical.Month);
            DateTime yearEndDate = new DateTime(UpdatedTimeForHistorical.Year, 12, 31); 

            //
            int monthDiff;

            if (UpdatedTimeForHistorical.Day == endDay)
            {
                monthDiff = 12 - UpdatedTimeForHistorical.Month;
            }
            else
            {
                monthDiff = 12 - UpdatedTimeForHistorical.Month + 1;
            }

            for (int i = 12; i > 0; i--)
            {
                // To get the end date of each target month
                DateTime endOfMonth = yearEndDate.AddMonths(-i - monthDiff + 1);
                DatesForOneYear.Add(endOfMonth);
            }

            foreach (DateTime dateTime in DatesForOneYear)
            {
                int primaryKey = (dateTime - baseDate).Days;
                PrimaryKeysForOneYear.Add(primaryKey);
            }
        }
    

        // To update end dates of quarters for 3 years from the lastestly updated historical date
        public void UpdateQuartersForThreeYears()
        {
            DatesForThreeYears = [];
            PrimaryKeysForThreeYears = [];
            
            int endDay = DateTime.DaysInMonth(UpdatedTimeForHistorical.Year, UpdatedTimeForHistorical.Month);
            DateTime yearEndDate = new DateTime(UpdatedTimeForHistorical.Year, 12, 31);

            int monthDiff;
            int quarterDiff;

            if (UpdatedTimeForHistorical.Day == endDay)
            {
                monthDiff = 12 - UpdatedTimeForHistorical.Month;
            }
            else
            {
                monthDiff = 12 - UpdatedTimeForHistorical.Month + 1;
            }

            // The end of date list is the end of 4Q
            if (monthDiff == 0)
            {
                quarterDiff = 0;
            }
            else if (monthDiff >= 1 && monthDiff <= 3)
            {
                quarterDiff = 1;
            }
            else if (monthDiff >= 4 && monthDiff <= 6)
            {
                quarterDiff = 2;
            }
            else if (monthDiff >= 7 && monthDiff <= 9)
            {
                quarterDiff = 3;
            }
            else // monthDiff >= 10 && monthDiff <= 12
            {
                quarterDiff = 4;
            }


            for (int i = 12; i > 0; i--)
            {
                DateTime endOfQuarter = yearEndDate.AddMonths(-3 * (i + quarterDiff - 1));
                DatesForThreeYears.Add(endOfQuarter);
            }

            foreach (DateTime dateTime in DatesForThreeYears)
            {
                int primaryKey = (dateTime - baseDate).Days;
                PrimaryKeysForThreeYears.Add(primaryKey);
            }
        }

        // Step1: update rates in database from API if there is no primary key
        // Step2: update lists of rates of each currency, which are used to create graphs in the app (Main Page 1, Main Page 2)
        public async Task UpdateHistoricalData()
        {
            UpdateTenDays();
            UpdateMonthsForOneYear();
            UpdateQuartersForThreeYears();

            // Check whether primarKeys in each period exists in database 
            // If no primaryKey, bring data from API server and save the data in database
            foreach (int primaryKey in PrimaryKeysForTenDays)
            {
                if (await databaseService.CheckPrimaryKey(primaryKey) == false)
                {
                    // Bring Data from API Server
                    DateTime targetDate = baseDate.AddDays(primaryKey);
                    APIDateResponse apiResponse = await APIServer.GetHistorical(targetDate, AllSymbolStr);

                    // Save API Data in Database
                    await databaseService.InsertData(primaryKey, apiResponse);
                        
                    // ToBeDeleted
                    ApiRequestNumber += 1;
                }
            }

            foreach (int primaryKey in PrimaryKeysForOneYear)
            {
                if (await databaseService.CheckPrimaryKey(primaryKey) == false)
                {
                    // Bring Data from API Server
                    DateTime targetDate = baseDate.AddDays(primaryKey);
                    APIDateResponse apiResponse = await APIServer.GetHistorical(targetDate, AllSymbolStr);

                    // Save API Data in Database
                    await databaseService.InsertData(primaryKey, apiResponse);

                    // ToBeDeleted
                    ApiRequestNumber += 1;
                }
            }

            foreach (int primaryKey in PrimaryKeysForThreeYears)
            {
                if (await databaseService.CheckPrimaryKey(primaryKey) == false)
                {
                    // Bring Data from API Server
                    DateTime targetDate = baseDate.AddDays(primaryKey);
                    APIDateResponse apiResponse = await APIServer.GetHistorical(targetDate, AllSymbolStr);

                    // Save API Data in Database
                    await databaseService.InsertData(primaryKey, apiResponse);

                    // ToBeDeleted
                    ApiRequestNumber += 1;
                }
            }
            

            //Update each currency's rates for making graphs 
            foreach (Currency currency in AllCurrencies)
            {
                await databaseService.UpdateRatesForTenDays(PrimaryKeysForTenDays, currency);
                await databaseService.UpdateRatesForOneYear(PrimaryKeysForOneYear, currency);
                await databaseService.UpdateRatesForThreeYears(PrimaryKeysForThreeYears, currency);
            }
        }

        // Make a list to make a ten day graph
        // Before condcut this method, make sure that rates in currency are up to date.
        public async Task<List<float>> MakeTenDayGraphData(Currency numeratorCurrency, Currency denominatorCurrency)
        {
            List<float> graphRates = new List<float>();
            if (numeratorCurrency.RatesTenDays.Count != denominatorCurrency.RatesTenDays.Count)
            {
                return graphRates;
            }

            int listLength = numeratorCurrency.RatesTenDays.Count;

            for (int i = 0; i <  listLength; i++)
            {
                float rate = numeratorCurrency.RatesTenDays[i] / denominatorCurrency.RatesTenDays[i];
                graphRates.Add(rate);
            }

            return graphRates;
        }


        // Make a list to make a one year graph
        // Before condcut this method, make sure that rates in currency are up to date.
        public async Task<List<float>> MakeOneYearGraphData(Currency numeratorCurrency, Currency denominatorCurrency)
        {
            List<float> graphRates = new List<float>();
            if (numeratorCurrency.RatesOneYear.Count != denominatorCurrency.RatesOneYear.Count)
            {
                return graphRates;
            }

            int listLength = numeratorCurrency.RatesOneYear.Count;

            for (int i = 0; i < listLength; i++)
            {
                float rate = numeratorCurrency.RatesOneYear[i] / denominatorCurrency.RatesOneYear[i];
                graphRates.Add(rate);
            }

            return graphRates;
        }



        // Make a list to make a three year graph
        // Before condcut this method, make sure that rates in currency are up to date.
        public async Task<List<float>> MakeThreeYearGraphData(Currency numeratorCurrency, Currency denominatorCurrency)
        {
            List<float> graphRates = new List<float>();
            if (numeratorCurrency.RatesThreeYears.Count != denominatorCurrency.RatesThreeYears.Count)
            {
                return graphRates;
            }

            int listLength = numeratorCurrency.RatesThreeYears.Count;

            for (int i = 0; i < listLength; i++)
            {
                float rate = numeratorCurrency.RatesThreeYears[i] / denominatorCurrency.RatesThreeYears[i];
                graphRates.Add(rate);
            }

            return graphRates;
        }


        /*To DO List
         * 각 메써드 체크 / 필요없는 부분 삭제
         * 필요시 Comment 업데이트
         * 기냥 아이디어; 합계계산에서 숫자 아니어도 에러 / 화폐 지정 안되어 있어도 에러
         * 근데 0. 104. 이건 어찌함?? 이게 있으면 뒤에 자동으로 0을 추가 0.0, 104.0 
         */
    }
}
