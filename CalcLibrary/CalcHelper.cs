using CalcLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsCalculator
{
    public static class CalcHelper
    {
        public static IEnumerable<IOperation> GetOperations()
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

        public static double[] StringConverter(string args)
        {
            var result = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                             .Select(s => s.ToDouble())
                             .ToArray();
            return result;
        }
    }
}
