using System;
using System.Text;

namespace NumericalAnalysis
{
    public struct AnalysisResult
    {
        public double Approximation;
        public double Accuracy;
        public int Iterations;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Приближённое значение корня: { Approximation }");
            builder.AppendLine($"Точность: { Accuracy }");
            builder.AppendLine($"Количество итераций: { Iterations }");

            return builder.ToString();
        }
    }

    class Task3
    {
        static double Function(double x)
        {
            return Math.Sin(0.5  + x) - 2.0 * x + 0.5;
        }

        static double Derivative(double x)
        {
            return Math.Cos(0.5 + x) - 2.0;
        }

        static AnalysisResult NewtonsApproximation(double a, double b, double epsilon)
        {
            int iterations = 0;
            double lastX;
            double x = b;
            do
            {
                iterations++;

                lastX = x;
                x = lastX - Function(lastX) / Derivative(lastX);
            }
            while (Math.Abs(x - lastX) >= epsilon);

            return new AnalysisResult()
            {
                Approximation = x,
                Accuracy = epsilon,
                Iterations = iterations
            };
        }

        static AnalysisResult SecantApproximation(double a, double b, double epsilon)
        {
            int iterations = 0;
            double functionOfB = Function(b);
            double lastX;
            double x = a;
            do
            {
                iterations++;

                lastX = x;
                double functionOfX = Function(lastX);
                x = lastX - ((b - lastX) / (functionOfB - functionOfX) * functionOfX);
            }
            while (Math.Abs(x - lastX) >= epsilon);

            return new AnalysisResult()
            {
                Approximation = x,
                Accuracy = epsilon,
                Iterations = iterations
            };
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа №3");
            Console.WriteLine();

            double a = GetUserDoubleWithMessage("a");
            double b = GetUserDoubleWithMessage("b");
            double epsilon = GetUserDoubleWithMessage("epsilon");

            Console.WriteLine();

            Console.WriteLine("Метод Ньютона.");
            AnalysisResult newtons = NewtonsApproximation(a, b, epsilon);
            Console.WriteLine(newtons);

            Console.WriteLine("Метод хорд.");
            AnalysisResult secant = SecantApproximation(a, b, epsilon);
            Console.WriteLine(secant);

            string comparisonSummary = (newtons.Iterations - secant.Iterations) switch
            {
                0 =>
                    "Методы сходятся одинаково быстро.",
                < 0 =>
                    $"Метод Ньютона сходится быстрее метода хорд на кол-во итераций: " +
                    $"{ secant.Iterations - newtons.Iterations }",
                > 0 =>
                    $"Метод хорд сходится быстрее метода Ньютона на кол-во итераций: " +
                    $"{ newtons.Iterations - secant.Iterations }"
            };
            Console.WriteLine(comparisonSummary);

            Console.ReadKey();
        }

        private static double GetUserDoubleWithMessage(string valueName)
        {
            double value;
            Console.Write($"Введите { valueName }: ");
            if (!double.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Ошибка ввода!");
                Console.ReadKey();
                Environment.Exit(13); // "The data is invalid" code
            }

            return value;
        }
    }
}
