using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Rate
    {
        #region Fields
        private string currencyCode;
        private double value;
        private static List<BaseRate> BaseRateList = new List<BaseRate>() { new BaseRate("DKK", "Danske Kroner"), new BaseRate("EUR", "Euro"), new BaseRate("GBP", "Britiske Pund"), new BaseRate("USD", "Amerikanske Dollars") };

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Rate() { }

        /// <summary>
        /// Constructor,that accepts an existing Rate
        /// </summary>
        /// <param name="rate">Rate</param>
        public Rate(Rate rate)
        {
            this.currencyCode = rate.CurrencyCode;
            this.value = rate.Value;
        }

        /// <summary>
        /// Constructor used, when receiving data as string from ServerSide
        /// </summary>
        /// <param name="currencyCode">string - abbreviation of currency - 3 character string</param>
        /// <param name="value">string</param>
        public Rate(string currencyCode, string value)
        {
            this.currencyCode = currencyCode;
            this.value = Convert.ToDouble(value);
        }

        /// <summary>
        /// Constructor to create a new rate with data
        /// </summary>
        /// <param name="currencyCode">string - abbreviation of currency - 3 character string</param>
        /// <param name="value">double</param>
        public Rate(string currencyCode, double value)
        {
            this.currencyCode = currencyCode;
            this.value = value;
        }

        #endregion

        #region Methods
        public override string ToString()
        {
            return currencyCode + ": " + Math.Round(value, 2).ToString("0.##");
        }

        #endregion

        #region Properties
        public string CurrencyCode
        {
            get { return this.currencyCode; }
            set { this.currencyCode = value; }
        }

        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        #endregion

    }
}
