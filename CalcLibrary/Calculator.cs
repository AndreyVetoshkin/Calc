using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary
{
    public class Calculator
    {
        public string Sum(string x, string y)
        {
            double xd;
            if (!Double.TryParse(x, out xd))
                return "error";

            double yd;
            if (!Double.TryParse(y, out yd))
                return "error";
            var result = xd + yd;

            return $"{result}";
        }

        string Sub(string x, string y)
        {
            return "";
        }
    }
}
