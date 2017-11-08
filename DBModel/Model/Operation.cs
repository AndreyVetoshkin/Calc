using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Model
{
    public class Operation
    {
        public Operation()
        {
        }

        public Operation(string name)
        {
            Name = name;
        }

        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
    }
}
