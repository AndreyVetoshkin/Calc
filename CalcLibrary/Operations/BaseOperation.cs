using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary.Operations
{
    public abstract class BaseOperation : IOperation
    {
        public abstract string Name { get; }

        public virtual int MinArgsCount => 1;

        public abstract double Execute(double[] args);
        
    }
}
