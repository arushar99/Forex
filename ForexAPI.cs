using Forex.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Forex
{
    public class ForexAPI
    {

        private static readonly string Base_URL = "https://fcsapi.com/api/forex/";
        private static readonly string APIKey = "priXDSH4PdLgSTXFikPsUqliKdDBX9Z9TmoDG9pIjC6JcDOEwx";


        private static string API_Call(string URL)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    //HTTP GET
                    var responseTask = client.GetAsync(URL);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        string str = readTask.Result;
                        return str;


                    }
                }
            }
            catch
            {

            }
            return "NULL";

        }
        private static string Request_The_Moving_Avgerages(string CurrencyPair)
        {
            string URL = Base_URL + $"ma_avg?symbol={CurrencyPair}&period=1h&access_key={APIKey}";
            string result = API_Call(URL);
            return result;
        }
        private static string Request_The_History_Of_Currency_Pairs(string CurrencyPair)
        {
            string URL = Base_URL + $"history?symbol={CurrencyPair}/&period=1h&access_key={APIKey}";
            string result = API_Call(URL);
            return result;
        }

        private static CurrenciesForList Deserialize_The_String(string str)
        {
            CurrenciesForList ListOfCurrenciesForBase = new CurrenciesForList();
            string[] str_splitted = str.Split(new string[] { "{"}, StringSplitOptions.RemoveEmptyEntries);
            Currency currency = new Currency();
            List<Currency> currencies = new List<Currency>();
            for(int i = 0; i < str_splitted.Length; i++)
            {
                string temp1 = str_splitted[i];
                if (temp1.Contains("o") && !temp1.Contains("response") && !temp1.Contains("id"))
                {
                    temp1 = temp1.Replace("\\", "");
                    string[] splitted = temp1.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for(int j = 0; j < splitted.Length; j++)
                    {
                        string temp2 = splitted[j];
                        temp2 = temp2.Replace("\"", "");
                        int colon_index; 
                        if (temp2.Contains("o") && !temp2.Contains("info"))
                        {
                            colon_index = temp2.IndexOf(":");
                            currency.Open = double.Parse(temp2.Substring(colon_index + 1));
                        }
                        else if (temp2.Contains("h"))
                        {
                            colon_index = temp2.IndexOf(":");
                            currency.High = double.Parse(temp2.Substring(colon_index + 1));
                        }
                        else if (temp2.Contains("l"))
                        {
                            colon_index = temp2.IndexOf(":");
                            currency.Low = double.Parse(temp2.Substring(colon_index + 1));
                        }
                        else if (temp2.Contains("c"))
                        {
                            colon_index = temp2.IndexOf(":");
                            currency.Close = double.Parse(temp2.Substring(colon_index + 1));
                        }
                        else if (temp2.Contains("tm"))
                        {
                            temp2 = temp2.Replace("}", "");
                            temp2 = temp2.Replace("]", "");
                            colon_index = temp2.IndexOf(":");
                            currency.Time = temp2.Substring(colon_index + 1);
                            currencies.Add(currency);
                            currency = new Currency();
                        }

                    }

                }
            }
            ListOfCurrenciesForBase.Currencies = currencies;


            return ListOfCurrenciesForBase;
        }
        private static string Remove_Unneccessary(string str)
        {
            while (str.Contains("/") || str.Contains("\\") || str.Contains("{") || str.Contains("}") || str.Contains("[") || str.Contains("]") || str.Contains(";") || str.Contains("\"") || str.Contains("rn"))
            {
                str = str.Replace("rn", "");
                str = str.Replace("\\n", "");
                str = str.Replace("\\r", "");
                str = str.Replace("\\t", "");
                str = str.Replace("/", "");
                str = str.Replace("\\", "");
                str = str.Replace("{", "");
                str = str.Replace("}", "");
                str = str.Replace("]", "");
                str = str.Replace("[", "");

              //  str = str.Replace(":", "");
                str = str.Replace("\"", "");
                str = str.Replace(";", "");
            }
            return str;
        }
        private static TechnicalAnalysisTools Get_The_Moving_Averages(string str)
        {
            string[] str_splitted = str.Split(new string[] { "},\"" }, StringSplitOptions.RemoveEmptyEntries);
            TechnicalAnalysisTools MovingAverages = new TechnicalAnalysisTools();
            bool found_simple_ma = false;
            bool found_ema_ma = false;

            for (int i = 0; i < str_splitted.Length; i++)
            {
                string temp1 = str_splitted[i];
                //  temp1 = Remove_Unneccessary(temp1);
                if (found_simple_ma || temp1.Contains("SMA"))
                {
                    if (temp1.Contains("SMA"))
                    {
                        found_simple_ma = true;
                    }
                    if (temp1.Contains("MA5") && !temp1.Contains("MA50"))
                    {
                        if (temp1.Contains("Sell"))
                        {
                            MovingAverages.Moving_AVG5 = "Sell";
                        }
                        else if (temp1.Contains("Buy"))
                        {
                            MovingAverages.Moving_AVG5 = "Buy";
                        }
                        else if (temp1.Contains("Neutral"))
                        {
                            MovingAverages.Moving_AVG5 = "Neutral";
                        }

                    }
                    else if (temp1.Contains("MA10") && !temp1.Contains("MA100"))
                    {
                        if (temp1.Contains("Sell"))
                        {
                            MovingAverages.Moving_AVG10 = "Sell";
                        }
                        else if (temp1.Contains("Buy"))
                        {
                            MovingAverages.Moving_AVG10 = "Buy";
                        }
                        else if (temp1.Contains("Neutral"))
                        {
                            MovingAverages.Moving_AVG10 = "Neutral";
                        }

                    }
                    else if (temp1.Contains("MA20") && !temp1.Contains("MA200"))
                    {
                        if (temp1.Contains("Sell"))
                        {
                            MovingAverages.Moving_AVG20 = "Sell";
                        }
                        else if (temp1.Contains("Buy"))
                        {
                            MovingAverages.Moving_AVG20 = "Buy";
                        }
                        else if (temp1.Contains("Neutral"))
                        {
                            MovingAverages.Moving_AVG20 = "Neutral";
                        }

                    }
                    else if (temp1.Contains("MA50"))
                    {
                        if (temp1.Contains("Sell"))
                        {
                            MovingAverages.Moving_AVG50 = "Sell";
                        }
                        else if (temp1.Contains("Buy"))
                        {
                            MovingAverages.Moving_AVG50 = "Buy";
                        }
                        else if (temp1.Contains("Neutral"))
                        {
                            MovingAverages.Moving_AVG50 = "Neutral";
                        }

                    }
                    else if (temp1.Contains("MA100"))
                    {
                        if (temp1.Contains("Sell"))
                        {
                            MovingAverages.Moving_AVG100 = "Sell";
                        }
                        else if (temp1.Contains("Buy"))
                        {
                            MovingAverages.Moving_AVG100 = "Buy";
                        }
                        else if (temp1.Contains("Neutral"))
                        {
                            MovingAverages.Moving_AVG100 = "Neutral";
                        }

                    }

                    else if (temp1.Contains("MA200"))
                    {
                        if (temp1.Contains("Sell"))
                        {
                            MovingAverages.Moving_AVG200 = "Sell";
                        }
                        else if (temp1.Contains("Buy"))
                        {
                            MovingAverages.Moving_AVG200 = "Buy";
                        }
                        else if (temp1.Contains("Neutral"))
                        {
                            MovingAverages.Moving_AVG200 = "Neutral";
                        }
                        found_simple_ma = false;
                    }



                }
                else if (found_ema_ma || temp1.Contains("EMA"))
                {

                    if (temp1.Contains("EMA"))
                    {
                        found_ema_ma = true;
                    }
                    if (temp1.Contains("MA5") && !temp1.Contains("MA50"))
                    {
                        if (temp1.Contains("Sell"))
                        {
                            MovingAverages.Exp_Moving_AVG5 = "Sell";
                        }
                        else if (temp1.Contains("Buy"))
                        {
                            MovingAverages.Exp_Moving_AVG5 = "Buy";
                        }
                        else if (temp1.Contains("Neutral"))
                        {
                            MovingAverages.Exp_Moving_AVG5 = "Neutral";
                        }

                    }
                    else if (temp1.Contains("MA10") && !temp1.Contains("MA100"))
                    {
                        if (temp1.Contains("Sell"))
                        {
                            MovingAverages.Exp_Moving_AVG10 = "Sell";
                        }
                        else if (temp1.Contains("Buy"))
                        {
                            MovingAverages.Exp_Moving_AVG10 = "Buy";
                        }
                        else if (temp1.Contains("Neutral"))
                        {
                            MovingAverages.Exp_Moving_AVG10 = "Neutral";
                        }

                    }
                    else if (temp1.Contains("MA20") && !temp1.Contains("MA200"))
                    {
                        if (temp1.Contains("Sell"))
                        {
                            MovingAverages.Exp_Moving_AVG20 = "Sell";
                        }
                        else if (temp1.Contains("Buy"))
                        {
                            MovingAverages.Exp_Moving_AVG20 = "Buy";
                        }
                        else if (temp1.Contains("Neutral"))
                        {
                            MovingAverages.Exp_Moving_AVG20 = "Neutral";
                        }

                    }
                    else if (temp1.Contains("MA50"))
                    {
                        if (temp1.Contains("Sell"))
                        {
                            MovingAverages.Exp_Moving_AVG50 = "Sell";
                        }
                        else if (temp1.Contains("Buy"))
                        {
                            MovingAverages.Exp_Moving_AVG50 = "Buy";
                        }
                        else if (temp1.Contains("Neutral"))
                        {
                            MovingAverages.Exp_Moving_AVG50 = "Neutral";
                        }

                    }
                    else if (temp1.Contains("MA100"))
                    {
                        if (temp1.Contains("Sell"))
                        {
                            MovingAverages.Exp_Moving_AVG100 = "Sell";
                        }
                        else if (temp1.Contains("Buy"))
                        {
                            MovingAverages.Exp_Moving_AVG100 = "Buy";
                        }
                        else if (temp1.Contains("Neutral"))
                        {
                            MovingAverages.Exp_Moving_AVG100 = "Neutral";
                        }

                    }

                    else if (temp1.Contains("MA200"))
                    {
                        if (temp1.Contains("Sell"))
                        {
                            MovingAverages.Exp_Moving_AVG200 = "Sell";
                        }
                        else if (temp1.Contains("Buy"))
                        {
                            MovingAverages.Exp_Moving_AVG200 = "Buy";
                        }
                        else if (temp1.Contains("Neutral"))
                        {
                            MovingAverages.Exp_Moving_AVG200 = "Neutral";
                        }
                        found_ema_ma = false;

                    }
                }
                else if (temp1.Contains("summary"))
                {
                    temp1 = Remove_Unneccessary(temp1);
                    int colon_index = temp1.IndexOf(":");
                    MovingAverages.Summary = temp1.Substring(colon_index + 1);

                }

                
            }
            return MovingAverages;

        }
        public static CurrenciesForList Currency_History_Caller(string str = "GBP/USD")
        {

            string api_request_result = Request_The_History_Of_Currency_Pairs(str);
            CurrenciesForList CurrenciesForBase = new CurrenciesForList();
            CurrenciesForBase = Deserialize_The_String(api_request_result);
            string MovingAvgStr = Request_The_Moving_Avgerages(str);
            CurrenciesForBase.MovingAverages = Get_The_Moving_Averages(MovingAvgStr);
            CurrenciesForBase.Symbol = str;
            CurrenciesForBase.Period = "1hr";
            return CurrenciesForBase;
        }


    }
}