using System;

namespace NumericalAnalysis
{
    class Task7
    {
        static double Function(double x)
        {
            return Math.Pow(Math.Log(5 * x), 2);
        }

        static double TrapezoidalSum(double a, double b, int n)
        {
            double sum = 0.0;
            double h = (b - a) / n;
            double edgeLeft = Function(a);
            double edgeRight;
            for (int i = 1; i < n; i++)
            {
                edgeRight = Function(a + i * h);
                sum += (edgeLeft + edgeRight) / 2.0 * h;
                edgeLeft = edgeRight;
            }

            return sum;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа №7");
            Console.WriteLine();

            double a = 1.0;
            double b = 100.0;
            const double exact = 2817.8;
            int n = GetUserIntWithMessage("n");

            Console.WriteLine();

            Console.WriteLine("Метод трапеций.");
            double approximation = TrapezoidalSum(a, b, n);
            Console.WriteLine($"Приближённое значение интеграла: { approximation }");
            Console.WriteLine($"Точное значение интеграла: { exact }");
            Console.WriteLine($"Разница приближённого и точного значений интеграла: { Math.Abs(approximation - exact) }");
            
            Console.ReadKey();
        }

        private static int GetUserIntWithMessage(string valueName)
        {
            int value;
            Console.Write($"Введите { valueName }: ");
            if (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Ошибка ввода!");
                Console.ReadKey();
                Environment.Exit(13); // "The data is invalid" code
            }

            return value;
        }
    }
}
