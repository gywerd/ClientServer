using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAsynchroneousServerSocket
{
    public class Rate
    {
        #region Fields
        private string currencyCode;
        private string value;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Rate() { }

        /// <summary>
        /// Constructor used, when converting data from Json
        /// </summary>
        /// <param name="currencyCode">string - abbreviation of currency - 3 character string</param>
        /// <param name="value">string</param>
        public Rate(string currencyCode, string value)
        {
            this.currencyCode = currencyCode;
            this.value = value;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method, that returns main content as a string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return @"100 " + currencyCode + @" koster " + value + @" US$";
        }

        #endregion

        #region Properties
        public string CurrencyCode
        {
            get { return this.currencyCode; }
            set { this.currencyCode = value; }
        }

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        #endregion

    }
}
