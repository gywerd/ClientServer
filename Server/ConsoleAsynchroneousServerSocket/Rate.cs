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
        public Rate() { }

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
