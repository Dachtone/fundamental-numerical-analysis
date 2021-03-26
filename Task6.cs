using System;

namespace NumericalAnalysis
{
    class Task6
    {
        static double Function(double x)
        {
            return Math.Pow(Math.Log(5 * x), 2);
        }

        static double RiemannSum(double a, double b, int n)
        {
            double sum = 0.0;
            double h = (b - a) / n;
            for (int i = 0; i < n; i++)
            {
                sum += Function(a + i * h) * h;
            }

            return sum;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа №6");
            Console.WriteLine();

            double a = 1.0;
            double b = 100.0;
            const double exact = 2817.8423474439187;
            int n = GetUserIntWithMessage("n");

            Console.WriteLine();

            Console.WriteLine("Метод прямоугольников.");
            double approximation = RiemannSum(a, b, n);
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
