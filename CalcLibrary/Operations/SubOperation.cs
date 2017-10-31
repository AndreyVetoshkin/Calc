using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary.Operations
{
    public class SubOperation : BaseOperation
    {
        public override string Name => "sub";

        public override int MinArgsCount => 2;

        public override double Execute(double[] args)
        {
            return args[0] - args[1];
        }
    }
}
