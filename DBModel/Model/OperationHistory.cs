﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Model
{
    public class OperationHistory
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Args { get; set; }
        public double? Result { get; set; }
        public long ExecTime { get; set; }
    }
}
