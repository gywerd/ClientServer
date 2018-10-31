using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleAsynchroneousServerSocket
{
    public class ClassWebService
    {
        string url = "https://openexchangerates.org/api/";
        private const string APP_ID = "5cf47e89535d41d9a7eb81d950673c02"; //Daniel Api
        string responseStream = "";
        //private HttpClient client;

        public ClassWebService()
        {
            //client = new HttpClient();
            //client.BaseAddress = new Uri(url);
            //GetDataFromApiAsync();
            GetDataFromApi();
        }

        public DollarRates GetCurrencyFromOpenExchangeRate()
        {
            //while (responseStream == "")
            //{
            //    Thread.Sleep(50);
            //}

            return JsonConvert.DeserializeObject<DollarRates>(responseStream);
        }

        //public async void GetDataFromApiAsync()
        //{
        //    while (true)
        //    {
        //        try
        //        {
        //            string query = $"latest.json?app_id=" + APP_ID;
        //            HttpResponseMessage responseMessageExchange = await client.GetAsync(query);

        //            responseStream = await responseMessageExchange.Content.ReadAsStringAsync();

        //            await Task.Delay(11000);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //    }
        //}

        private void GetDataFromApi()
        {
            Uri uri = new Uri(url + $"latest.json?app_id=" + APP_ID);
            using (WebClient w = new WebClient())
            {
                w.Headers["User-Agent"] = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                // attempt to download JSON data as a string
                try
                {
                    this.responseStream = w.DownloadString(uri);
                }
                catch (Exception) { }
            }
        }
        public string ResponseStream { get => this.responseStream; set => this.responseStream = value; }

    }
}
