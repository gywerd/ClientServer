using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Rate
    {
        private string currencyCode;
        private double value;
        private static List<BaseRate> BaseRateList = new List<BaseRate>() { new BaseRate("DKK", "Danske Kroner"), new BaseRate("EUR", "Euro"), new BaseRate("GBP", "Britiske Pund"), new BaseRate("USD", "Amerikanske Dollars") };

        public Rate() { }

        public Rate(Rate rate)
        {
            this.currencyCode = rate.CurrencyCode;
            this.value = rate.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currencyCode">string - abbreviation of currency</param>
        /// <param name="value">string</param>
        public Rate(string currencyCode, string value)
        {
            this.currencyCode = currencyCode;
            this.value = Convert.ToDouble(value);
        }

        public Rate(string currencyCode, double value)
        {
            this.currencyCode = currencyCode;
            this.value = value;
        }

        public override string ToString()
        {
            return currencyCode + ": " + Math.Round(value, 2).ToString("0.##");
        }

        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public string CurrencyCode
        {
            get { return this.currencyCode; }
            set { this.currencyCode = value; }
        }

    }
}
