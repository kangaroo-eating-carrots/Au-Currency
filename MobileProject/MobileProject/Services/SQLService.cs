
using MobileProject.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MobileProject.Services
{
    public class SQLService
    {
        SQLiteAsyncConnection database;
        private string tableName = "rates";

        public SQLService(string databasePath)
        {
            Console.WriteLine("Database Path: " + databasePath);

            if (File.Exists(databasePath))
            {
                Console.WriteLine("________________________________________");
                Console.WriteLine("Database file exists.");
                Console.WriteLine("________________________________________");
            }
            else
            {
                Console.WriteLine("________________________________________");
                Console.WriteLine("Database file does not exists.");
                Console.WriteLine("________________________________________");
            }

            database = new SQLiteAsyncConnection(databasePath);
            database.CreateTableAsync<HistoricalRates>().Wait();
        }

        public async Task DisplayPrimiaryKeys()
        {
            List<HistoricalRates> currentTable = await GetRatesAsync();
            if (currentTable.Count == 0 || currentTable == null)
            {
                Console.WriteLine("________________________________________");
                Console.WriteLine("No Table Item is found!");
                Console.WriteLine("________________________________________");
            }
            else
            {
                Console.WriteLine("________________________________________");
                foreach (HistoricalRates row in currentTable)
                {
                    DateTime baseOne = new DateTime(1900, 1, 1);
                    DateTime priOne = baseOne.AddDays(row.Id);
                    Console.WriteLine($"[{row.Id}] {priOne.Year}/{priOne.Month}/{priOne.Day}");
                }

                Console.WriteLine("________________________________________");
            }

        }
        public async Task<List<HistoricalRates>> GetRatesAsync()
        {
            return await database.Table<HistoricalRates>().ToListAsync();
        }

        public async Task<bool> CheckPrimaryKey(int primaryKey)
        {
            List<HistoricalRates> currentTable = await GetRatesAsync();

            // If the primary key already existed, return true.
            foreach (HistoricalRates row in currentTable)
            {
                if (row.Id == primaryKey)
                {
                    return true;
                }
            }
            
            // If the primary key was not found in the table, return false. 
            return false;

        }


        public async Task UpdateRatesForTenDays(List<int> primaryKeys, Currency currency)
        {
            List<HistoricalRates> currentTable = await GetRatesAsync();
            List<float> tempRates = [];

            foreach (int primaryKey in primaryKeys)
            {
                foreach (HistoricalRates row in currentTable)
                {
                    if (row.Id == primaryKey)
                    {
                        if (currency.Symbol == CurrencySymbol.USD)
                        {
                            tempRates.Add(row.USD);
                        }
                        else if (currency.Symbol == CurrencySymbol.AUD)
                        {
                            tempRates.Add(row.AUD);
                        }
                        else if (currency.Symbol == CurrencySymbol.EUR)
                        {
                            tempRates.Add(row.EUR);
                        }
                        else if (currency.Symbol == CurrencySymbol.JPY)
                        {
                            tempRates.Add(row.JPY);
                        }
                        else if (currency.Symbol == CurrencySymbol.GBP)
                        {
                            tempRates.Add(row.GBP);
                        }
                        else if (currency.Symbol == CurrencySymbol.CNY)
                        {
                            tempRates.Add(row.CNY);
                        }
                        else if (currency.Symbol == CurrencySymbol.CAD)
                        {
                            tempRates.Add(row.CAD);
                        }
                        else if (currency.Symbol == CurrencySymbol.CHF)
                        {
                            tempRates.Add(row.CHF);
                        }
                        else if (currency.Symbol == CurrencySymbol.HKD)
                        {
                            tempRates.Add(row.HKD);
                        }
                        else if (currency.Symbol == CurrencySymbol.SGD)
                        {
                            tempRates.Add(row.SGD);
                        }
                        else if (currency.Symbol == CurrencySymbol.SEK)
                        {
                            tempRates.Add(row.SEK);
                        }
                        else if (currency.Symbol == CurrencySymbol.KRW)
                        {
                            tempRates.Add(row.KRW);
                        }
                        else if (currency.Symbol == CurrencySymbol.NOK)
                        {
                            tempRates.Add(row.NOK);
                        }
                        else if (currency.Symbol == CurrencySymbol.NZD)
                        {
                            tempRates.Add(row.NZD);
                        }
                        else if (currency.Symbol == CurrencySymbol.INR)
                        {
                            tempRates.Add(row.INR);
                        }
                    }
                }
            }

            currency.UpdateTrendTenDays(tempRates);

        }

        public async Task UpdateRatesForOneYear(List<int> primaryKeys, Currency currency)
        {
            List<HistoricalRates> currentTable = await GetRatesAsync();
            List<float> tempRates = [];

            foreach (int primaryKey in primaryKeys)
            {
                foreach (HistoricalRates row in currentTable)
                {
                    if (row.Id == primaryKey)
                    {
                        if (currency.Symbol == CurrencySymbol.USD)
                        {
                            tempRates.Add(row.USD);
                        }
                        else if (currency.Symbol == CurrencySymbol.AUD)
                        {
                            tempRates.Add(row.AUD);
                        }
                        else if (currency.Symbol == CurrencySymbol.EUR)
                        {
                            tempRates.Add(row.EUR);
                        }
                        else if (currency.Symbol == CurrencySymbol.JPY)
                        {
                            tempRates.Add(row.JPY);
                        }
                        else if (currency.Symbol == CurrencySymbol.GBP)
                        {
                            tempRates.Add(row.GBP);
                        }
                        else if (currency.Symbol == CurrencySymbol.CNY)
                        {
                            tempRates.Add(row.CNY);
                        }
                        else if (currency.Symbol == CurrencySymbol.CAD)
                        {
                            tempRates.Add(row.CAD);
                        }
                        else if (currency.Symbol == CurrencySymbol.CHF)
                        {
                            tempRates.Add(row.CHF);
                        }
                        else if (currency.Symbol == CurrencySymbol.HKD)
                        {
                            tempRates.Add(row.HKD);
                        }
                        else if (currency.Symbol == CurrencySymbol.SGD)
                        {
                            tempRates.Add(row.SGD);
                        }
                        else if (currency.Symbol == CurrencySymbol.SEK)
                        {
                            tempRates.Add(row.SEK);
                        }
                        else if (currency.Symbol == CurrencySymbol.KRW)
                        {
                            tempRates.Add(row.KRW);
                        }
                        else if (currency.Symbol == CurrencySymbol.NOK)
                        {
                            tempRates.Add(row.NOK);
                        }
                        else if (currency.Symbol == CurrencySymbol.NZD)
                        {
                            tempRates.Add(row.NZD);
                        }
                        else if (currency.Symbol == CurrencySymbol.INR)
                        {
                            tempRates.Add(row.INR);
                        }
                    }
                }
            }

            currency.UpdateTrendOneYear(tempRates);

        }

        public async Task UpdateRatesForThreeYears(List<int> primaryKeys, Currency currency)
        {
            List<HistoricalRates> currentTable = await GetRatesAsync();
            List<float> tempRates = [];

            foreach (int primaryKey in primaryKeys)
            {
                foreach (HistoricalRates row in currentTable)
                {
                    if (row.Id == primaryKey)
                    {
                        if (currency.Symbol == CurrencySymbol.USD)
                        {
                            tempRates.Add(row.USD);
                        }
                        else if (currency.Symbol == CurrencySymbol.AUD)
                        {
                            tempRates.Add(row.AUD);
                        }
                        else if (currency.Symbol == CurrencySymbol.EUR)
                        {
                            tempRates.Add(row.EUR);
                        }
                        else if (currency.Symbol == CurrencySymbol.JPY)
                        {
                            tempRates.Add(row.JPY);
                        }
                        else if (currency.Symbol == CurrencySymbol.GBP)
                        {
                            tempRates.Add(row.GBP);
                        }
                        else if (currency.Symbol == CurrencySymbol.CNY)
                        {
                            tempRates.Add(row.CNY);
                        }
                        else if (currency.Symbol == CurrencySymbol.CAD)
                        {
                            tempRates.Add(row.CAD);
                        }
                        else if (currency.Symbol == CurrencySymbol.CHF)
                        {
                            tempRates.Add(row.CHF);
                        }
                        else if (currency.Symbol == CurrencySymbol.HKD)
                        {
                            tempRates.Add(row.HKD);
                        }
                        else if (currency.Symbol == CurrencySymbol.SGD)
                        {
                            tempRates.Add(row.SGD);
                        }
                        else if (currency.Symbol == CurrencySymbol.SEK)
                        {
                            tempRates.Add(row.SEK);
                        }
                        else if (currency.Symbol == CurrencySymbol.KRW)
                        {
                            tempRates.Add(row.KRW);
                        }
                        else if (currency.Symbol == CurrencySymbol.NOK)
                        {
                            tempRates.Add(row.NOK);
                        }
                        else if (currency.Symbol == CurrencySymbol.NZD)
                        {
                            tempRates.Add(row.NZD);
                        }
                        else if (currency.Symbol == CurrencySymbol.INR)
                        {
                            tempRates.Add(row.INR);
                        }
                    }
                }
            }

            currency.UpdateTrendThreeYears(tempRates);
        }



        public async Task InsertData(int primaryKey, APIDateResponse apiResponse)
        {
            HistoricalRates newRates = new HistoricalRates();
            newRates.Id = primaryKey;
            newRates.USD = apiResponse.Rates.USD;
            newRates.AUD = apiResponse.Rates.AUD;
            newRates.EUR = apiResponse.Rates.EUR;
            newRates.JPY = apiResponse.Rates.JPY;
            newRates.GBP = apiResponse.Rates.GBP;
            newRates.CNY = apiResponse.Rates.CNY;
            newRates.CAD = apiResponse.Rates.CAD;
            newRates.CHF = apiResponse.Rates.CHF;
            newRates.HKD = apiResponse.Rates.HKD;
            newRates.SGD = apiResponse.Rates.SGD;
            newRates.SEK = apiResponse.Rates.SEK;
            newRates.KRW = apiResponse.Rates.KRW;
            newRates.NOK = apiResponse.Rates.NOK;
            newRates.NZD = apiResponse.Rates.NZD;
            newRates.INR = apiResponse.Rates.INR;

            await database.InsertAsync(newRates);
        }

    }
}
