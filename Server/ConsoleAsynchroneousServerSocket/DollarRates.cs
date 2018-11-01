using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAsynchroneousServerSocket
{
    public class DollarRates
    {
        #region Fields
        private List<Rate> rateList;
        private string _disclaimer;
        private string _licence;
        private string _timestamp;
        private string _base;
        private Dictionary<string, decimal> _rates;

        #endregion

        #region Constructors
        public DollarRates()
        {
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that fills Dollar Rate List with data
        /// </summary>
        public void FillListWithDollarRates()
        {
            List<Rate> ld = new List<Rate>();
            foreach (var item in _rates)
            {
                Rate cd = new Rate();
                cd.CurrencyCode = item.Key;
                cd.Value = item.Value.ToString();
                ld.Add(cd);
            }

            rateList = ld;
        }

        /// <summary>
        /// Method, that browses the Rate List to find a specific value
        /// </summary>
        /// <param name="currencyCode">string</param>
        /// <returns>string</returns>
        public string GetRateFromCountryCode(string currencyCode)
        {
            foreach (Rate rate in rateList)
            {
                if (rate.CurrencyCode == currencyCode)
                {
                    return rate.ToString();
                }
            }
            return "The requested currency, " + currencyCode + " is unknown.";
        }

        #endregion

        #region Properties
        public List<Rate> RateList
        {
            get { return this.rateList; }
            set { this.rateList = value; }
        }

        public string disclaimer
        {
            get { return this._disclaimer; }
            set { this._disclaimer = value; }
        }

        public string licence
        {
            get { return this._licence; }
            set { this._licence = value; }
        }


        public string timestamp
        {
            get { return this._timestamp; }
            set { this._timestamp = value; }
        }

        public string @base
        {
            get { return this._base; }
            set { this._base = value; }
        }

        public Dictionary<string, decimal> rates
        {
            get { return this._rates; }
            set
            {
                if (value != _rates)
                {
                    this._rates = value;
                    FillListWithDollarRates();
                }
            }
        }

        #endregion
    }
}
