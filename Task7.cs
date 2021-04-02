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
            for (int i = 0; i < n; i++)
            {
                edgeRight = Function(a + (i + 1) * h);
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
            const double exact = 2817.8423474439187;

            Console.WriteLine("Метод трапеций.");
            for (int n = 10; n <= 100000; n *= 10)
            {
                Console.WriteLine();

                Console.WriteLine($"n = { n }");
                double approximation = TrapezoidalSum(a, b, n);
                Console.WriteLine($"Приближённое значение интеграла: { approximation }");
                Console.WriteLine($"Точное значение интеграла: { exact }");
                Console.WriteLine($"Разница приближённого и точного значений интеграла: { Math.Abs(approximation - exact) }");
            }
            
            Console.ReadKey();
        }
    }
}
