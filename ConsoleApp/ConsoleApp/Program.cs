using CalcLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var opers = GetLibraries();
            var calc = new Calculator();

            foreach (var oper in opers)
            {
                calc.Operations.Add(oper);
            }
            var count = 0;
            foreach (var oper in calc.Operations)
            {
                Console.WriteLine($"{++count}. {oper.Name}");
            }
            Console.WriteLine("Select operation");
            var operKey = Console.ReadLine();
            var operId = Convert.ToInt32(operKey);
            var operation = calc.Operations.ElementAt(operId - 1);

            Console.WriteLine("Введите аргументы через пробел, если операция требует более одного аргумента: ");
            string x = Console.ReadLine();

            Console.WriteLine(operation.Execute(StringConverter(x)));
            Console.ReadKey();
        }

        static IEnumerable<IOperation> GetLibraries()
        {
            var result = new List<IOperation>();
            //найти текущую директорию
            var dir = Environment.CurrentDirectory + "\\Exts";
            if (!Directory.Exists(dir))
                return result;
            //загрузить все файлы *.dll
            var files = Directory.GetFiles(dir, "*.dll");
            foreach (var item in files)
            {
                //загрузить их
                var dll = Assembly.LoadFrom(item);
                //разобрать их по классам
                var classes = dll.GetTypes();
                //найти нужные классы
                foreach (var cl in classes)
                {
                    var interfaces = cl.GetInterfaces();

                    if (interfaces.Any(i => i == typeof(IOperation)))
                    {
                        var instance = Activator.CreateInstance(cl) as IOperation;
                        if (instance != null)
                        {
                            result.Add(instance);
                        }
                    }

                }
            }
            return result;
        }

        static double[] StringConverter(string args)
        {
            if (args == "")
                return null;
            string[] sArgs = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            double[] dArgs = new double[sArgs.Count()];
            for (int i = 0; i < dArgs.Count(); i++)
            {
                try
                {
                    dArgs[i] = Convert.ToDouble(sArgs[i]);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            return dArgs;
        }
    }
}
