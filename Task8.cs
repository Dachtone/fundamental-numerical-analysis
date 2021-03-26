using System;

namespace NumericalAnalysis
{
    class Task8
    {
        static double Function(double x)
        {
            return Math.Pow(Math.Log(5 * x), 2);
        }

        static double SimpsonsSum(double a, double b, int n)
        {
            double sum = 0.0;
            double h = (b - a) / n;
            double edgeLeft = Function(a);
            double edgeRight;
            for (int i = 0; i < n; i++)
            {
                edgeRight = Function(a + (i + 1) * h);
                sum += h / 6.0 * (edgeLeft + 4.0 * Function((2.0 * a + h * (2.0 * i + 1.0)) / 2.0) + edgeRight);
                edgeLeft = edgeRight;
            }

            return sum;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа №8");
            Console.WriteLine();

            double a = 1.0;
            double b = 100.0;
            const double exact = 2817.8423474439187;
            int n = GetUserIntWithMessage("n");

            Console.WriteLine();

            Console.WriteLine("Метод Симпсона.");
            double approximation = SimpsonsSum(a, b, n);
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
