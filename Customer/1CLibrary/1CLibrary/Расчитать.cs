using CalcLibrary;
using CalcLibrary.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1CLibrary
{
    public class Расчитать : BaseOperation
    {
        public override string Name => "расчёт";


        public override double Execute(double[] args)
        {
            Thread.Sleep(new Random().Next(1, 10) * 1000);
            return args.Average();
        }
    }
}
