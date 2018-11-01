using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BaseRate
    {
        #region Fields
        private string currencyCode;
        private string currencyName;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public BaseRate() { }

        /// <summary>
        /// Constructor, that creates a new Base Rate with data
        /// </summary>
        /// <param name="currencyCode">string - abbreviation of currency - 3 character string</param>
        /// <param name="currencyName">string</param>
        public BaseRate(string currencyCode, string currencyName)
        {
            this.currencyCode = currencyCode;
            this.currencyName = currencyName;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that returns main content as a string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return currencyName + " (" + currencyCode + ")";
        }

        #endregion

        #region Properties
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

        #endregion

    }
}
