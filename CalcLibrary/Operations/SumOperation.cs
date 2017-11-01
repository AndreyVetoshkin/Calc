using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary.Operations
{
    public class SumOperation : BaseOperation
    {
        public override string Name
        {
            get
            {
                return "sum";
            }
        }

        public override int MinArgsCount => 1;

        public override double Execute(double[] args)
        {
            if (args == null)
            {
                Console.WriteLine($"Ошибка: аргументы не определены");
                return 0;
            }
            return args.Sum();
        }
    }
}
