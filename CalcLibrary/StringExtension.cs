using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsCalculator
{
    public static class StringExtension
    {
        public static double ToDouble(this string input)
        {
            double result;
            if (!double.TryParse(input, out result))
                return double.NaN;

            return result;
        }
    }
}
