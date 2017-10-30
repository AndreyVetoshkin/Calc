using CalcLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Calculator");

            var calc = new Calculator();

            if (args.Count() == 3)
            {
                var operation = args[0];
                var x = args[1];
                var y = args[2];
                var result = "";

                if (operation == "sum")
                {
                    result = calc.Sum(x, y);
                }

                if (operation == "dsum")
                {
                    result = calc.dSum(x, y).ToString();
                }

                if (operation == "sub")
                {
                    result = calc.Sub(x, y).ToString();
                }

                if (operation == "mul")
                {
                    result = calc.Mul(x, y).ToString();
                }

                if (operation == "div")
                {
                    result = calc.Div(x, y).ToString();
                }
                Console.WriteLine(result);
                Console.ReadKey();
            }

            if (args.Count() == 2)
            {
                var operation = args[0];
                var x = args[1];
                var result = "";
                if (operation == "sqr")
                {
                    result = calc.Sqr(x).ToString();
                }

                if (operation == "sqrt")
                {
                    result = calc.Sqrt(x).ToString();
                }
                Console.WriteLine(result);
                Console.ReadKey();
            }
            if (args.Count() == 0)
            {
                bool alive = true;
                double dResult = 0.0;
                while (alive)
                {
                    // выводим список команд
                    Console.WriteLine("1. Сложение");
                    Console.WriteLine("2. Вычитание");
                    Console.WriteLine("3. Умножение");
                    Console.WriteLine("4. Деление");
                    Console.WriteLine("5. Возведение в квадрат");
                    Console.WriteLine("6. Извлечение квадратного корня");
                    Console.WriteLine("7. Завершить работу\n");
                    Console.WriteLine("Выберите операцию:");
                    try
                    {
                        int command = Convert.ToInt32(Console.ReadLine());
                        switch (command)
                        {
                            case 1:
                                Console.WriteLine("Введите 1-й аргумент: ");
                                string x = Console.ReadLine();
                                Console.WriteLine("Введите 2-й аргумент: ");
                                string y = Console.ReadLine();
                                dResult = calc.dSum(x, y);
                                break;
                            case 2:
                                Console.WriteLine("Введите 1-й аргумент: ");
                                x = Console.ReadLine();
                                Console.WriteLine("Введите 2-й аргумент: ");
                                y = Console.ReadLine();
                                dResult = calc.Sub(x, y);
                                break;
                            case 3:
                                Console.WriteLine("Введите 1-й аргумент: ");
                                x = Console.ReadLine();
                                Console.WriteLine("Введите 2-й аргумент: ");
                                y = Console.ReadLine();
                                dResult = calc.Mul(x, y);
                                break;
                            case 4:
                                Console.WriteLine("Введите 1-й аргумент: ");
                                x = Console.ReadLine();
                                Console.WriteLine("Введите 2-й аргумент: ");
                                y = Console.ReadLine();
                                dResult = calc.Div(x, y);
                                break;
                            case 5:
                                Console.WriteLine("Введите аргумент: ");
                                x = Console.ReadLine();
                                dResult = calc.Sqr(x);
                                break;
                            case 6:
                                Console.WriteLine("Введите аргумент: ");
                                x = Console.ReadLine();
                                dResult = calc.Sqrt(x);
                                break;
                            case 7:
                                alive = false;
                                continue;
                        }
                        Console.WriteLine("\nРезультат операции: " + dResult + "\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n" + ex.Message + "\n");
                    }
                }
            }
        }
    }
}
