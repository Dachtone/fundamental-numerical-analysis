using System;
using System.Text;

namespace NumericalAnalysis
{
    class Task5
    {
        static void PrintTridiagonalMatrixAlgorithm()
        {
            int n = 100;
            double k = 6.0;

            double a = 1.0;
            double b = 2.0;
            double c = 1.0;
            double d = 10.0 * k;

            double[] u = new double[n];
            double[] v = new double[n];

            u[0] = -c / b;
            v[0] = d / b;

            for (int i = 1; i < n; i++)
            {
                if (i == n - 1)
                    u[i] = 0;
                else
                    u[i] = -c / (a * u[i - 1] + b);
                v[i] = (d - a * v[i - 1]) / (a * u[i - 1] + b);
            }

            double[] x = new double[n];
            x[n - 1] = (d - a * v[n - 2]) / (a * u[n - 2] + b);

            for (int i = n - 2; i >= 0; i--)
            {
                x[i] = u[i] * x[i + 1] + v[i];
            }

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"i = { i + 1 }, x = { x[i] }");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа №5");
            Console.WriteLine();
            
            Console.WriteLine("Метод прогонки:");
            PrintTridiagonalMatrixAlgorithm();
            Console.WriteLine();
            
            Console.ReadKey();
        }
    }
}
