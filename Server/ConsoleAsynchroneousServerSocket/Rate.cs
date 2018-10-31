using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAsynchroneousServerSocket
{
    public class Rate
    {
        private string _country;
        private string _value;

        public Rate() { }

        public override string ToString()
        {
            return @"100 " + _country + @" koster " + _value + @" US$";
        }

        public string country
        {
            get { return this._country; }
            set { this._country = value; }
        }

        public string value
        {
            get { return this._value; }
            set { this._value = value; }
        }

    }
}
