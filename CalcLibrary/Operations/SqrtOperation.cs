using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary.Operations
{
    class SqrtOperation : BaseOperation
    {
        public override string Name => "sqrt";

        public override int MinArgsCount => 1;

        public override double Execute(double[] args)
        {
            if (args == null)
            {
                Console.WriteLine($"Ошибка: аргументы не определены");
                return 0;
            }
            if (args.Count() > 1)
            {
                Console.WriteLine($"Ошибка: максимальное количество аргументов {MinArgsCount}");
                return 0;
            }
            if (args[0] < 0)
            {
                Console.WriteLine($"Ошибка: аргументом может быть только неотрицательное число");
                return 0;
            }
            return Math.Sqrt(args[0]);
        }
    }
}
