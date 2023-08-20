using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZodiakNameSpace
{
    public class Zodiak
    {
        public string sign;
        public string forecast;
        public Zodiak(string sign, string forecast)
        {
            this.forecast = forecast;
            this.sign = sign;
        }

    }
}
