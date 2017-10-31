using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary.Operations
{
    class DivOperation : BaseOperation
    {
        public override string Name => "div";

        public override int MinArgsCount => 2;

        public override double Execute(double[] args)
        {
            if (args == null)
            {
                Console.WriteLine($"Ошибка: аргументы не определены");
                return 0;
            }
            if (args.Count() < 2)
            {
                Console.WriteLine($"Ошибка: минимальное количество аргументов {MinArgsCount}");
                return 0;
            }
            if (args[0] == 0)
                return 0;
            if (args.Contains(0) && args[0] != 0)
            {
                Console.WriteLine("Ошибка: только первый аргумент может быть равен \"0\"");
                return 0;
            }
            double result = args[0];
            for (int i = 1; i < args.Count(); i++)
            {
                result /= args[i];
            }
            return result;
        }
    }
}
