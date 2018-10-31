using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BaseRate
    {
        private string currencyCode;
        private string currencyName;

        public BaseRate() { }

        public BaseRate(string country, string value)
        {
            this.currencyCode = country;
            this.currencyName = value;
        }

        public override string ToString()
        {
            return currencyName + " (" + currencyCode + ")";
        }

        public string CurrencyCode
        {
            get { return this.currencyCode; }
            set { this.currencyCode = value; }
        }

        public string CurrencyName
        {
            get { return this.currencyName; }
            set { this.currencyName = value; }
        }

    }
}
