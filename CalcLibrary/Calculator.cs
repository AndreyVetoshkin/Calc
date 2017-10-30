using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary
{
    public class Calculator
    {
        private bool ParseToDouble(string x, string y, out double dvalue1, out double dvalue2)
        {
            double xd, yd;
            if (Double.TryParse(x, out xd) && Double.TryParse(y, out yd))
            {
                dvalue1 = xd;
                dvalue2 = yd;
                return true;
            }
            else
            {
                Console.WriteLine("Ошибка: операнды должны принимать числовые значения");
                dvalue1 = dvalue2 = 0;
                return false;
            }
        }
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

        public double dSum(string x, string y)
        {
            double xd, yd, result;
            if (ParseToDouble(x, y, out xd, out yd))
            {
                result = result = xd + yd;
            }
            else result = 0;
            return result;
        }

        public double Sub(string x, string y)
        {
            double xd, yd, result;
            if (ParseToDouble(x, y, out xd, out yd))
            {
                result = xd - yd;
            }
            else result = 0;
            return result;
        }

        public double Mul(string x, string y)
        {
            double xd, yd, result;
            if (ParseToDouble(x, y, out xd, out yd))
            {
                result = xd * yd;
            }
            else result = 0;
            return result;
        }

        public double Div(string x, string y)
        {
            double xd, yd, result;
            if (ParseToDouble(x, y, out xd, out yd) && yd != 0)
            {
                result = xd / yd;
            }
            else result = 0;
            if (yd == 0)
            {
                Console.WriteLine("Ошибка: делитель не может быть равен \"0\"");
            }
            return result;
        }

        public double Sqr(string x)
        {
            double xd, result;
            if (Double.TryParse(x, out xd))
            {
                result = xd * xd;
            }
            else
            {
                Console.WriteLine("Ошибка: операнды должны принимать числовые значения");
                result = 0;
            }
            return result;
        }

        public double Sqrt(string x)
        {
            double xd, result;
            if (Double.TryParse(x, out xd))
            {
                if (xd >= 0)
                    result = Math.Sqrt(xd);
                else
                {
                    Console.WriteLine("Ошибка: аргумент должен быть положительным числом");
                    result = 0;
                }
            }
            else
            {
                Console.WriteLine("Ошибка: операнды должны принимать числовые значения");
                result = 0;
            }
            return result;
        }
    }
}
