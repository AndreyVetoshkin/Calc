using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary
{
    public interface IOperation
    {
        //Название
        string Name { get; }
      
        //Минимальное количество аргументов
        int MinArgsCount { get; }

        //Выполнить операцию
        // args - входные данные
        double Execute(double[] args);
    }
}
