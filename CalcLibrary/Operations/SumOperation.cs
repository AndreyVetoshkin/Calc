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
            return args.Sum();
        }
    }
}
