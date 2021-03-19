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

    class Task2
    {
        static double Function(double x)
        {
            return Math.Sin(0.5  + x) - 2.0 * x + 0.5;
        }

        static double Phi(double x)
        {
            return (Math.Sin(0.5 + x) + 0.5) / 2.0;
        }

        static AnalysisResult DichotomyApproximation(double a, double b, double epsilon)
        {
            int iterations = 1;
            double x = (a + b) / 2;
            do
            {
                iterations++;
                
                if (Function(x) < 0)
                {
                    b = x;
                }
                else
                {
                    a = x;
                }

                x = (a + b) / 2;
            }
            while (Math.Abs(b - a) >= epsilon);

            return new AnalysisResult()
            {
                Approximation = x,
                Accuracy = epsilon,
                Iterations = iterations
            };
        }

        static AnalysisResult FixedPointIterationApproximation(double a, double b, double epsilon)
        {
            int iterations = 0;
            double lastX = 1;
            double x = lastX;
            do
            {
                iterations++;

                lastX = x;
                x = Phi(lastX);
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
            Console.WriteLine("Лабораторная работа №2");
            Console.WriteLine();

            double a = GetUserDoubleWithMessage("a");
            double b = GetUserDoubleWithMessage("b");
            double epsilon = GetUserDoubleWithMessage("epsilon");

            Console.WriteLine();

            Console.WriteLine("Метод дихотомии.");
            AnalysisResult dichotomy = DichotomyApproximation(a, b, epsilon);
            Console.WriteLine(dichotomy);

            Console.WriteLine("Метод простых итераций.");
            AnalysisResult fixedPointIteration = FixedPointIterationApproximation(a, b, epsilon);
            Console.WriteLine(fixedPointIteration);

            string comparisonSummary = (dichotomy.Iterations - fixedPointIteration.Iterations) switch
            {
                0 =>
                    "Методы сходятся одинаково быстро.",
                < 0 =>
                    $"Метод дихотомии сходится быстрее метода простых итераций на кол-во итераций: " +
                    $"{ fixedPointIteration.Iterations - dichotomy.Iterations }",
                > 0 =>
                    $"Метод простых итераций сходится быстрее метода дихотомии на кол-во итераций: " +
                    $"{ dichotomy.Iterations - fixedPointIteration.Iterations }"
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
