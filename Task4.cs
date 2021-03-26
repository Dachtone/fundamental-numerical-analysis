using System;
using System.Text;

namespace NumericalAnalysis
{
    public struct AnalysisResult
    {
        public double ApproximationX;
        public double ApproximationY;

        public double Accuracy;
        public int Iterations;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Приближённое значение корня: x = { ApproximationX }, y = { ApproximationY }");
            builder.AppendLine($"Точность: { Accuracy }");
            builder.AppendLine($"Количество итераций: { Iterations }");

            return builder.ToString();
        }
    }

    class Task4
    {
        static double FunctionOne(double x, double y)
        {
            return Math.Sin(y + 0.5) - x - 1.0;
        }

        static double FunctionTwo(double x, double y)
        {
            return Math.Cos(x - 2.0) + y;
        }

        static double DerivativeOneX(double x, double y)
        {
            return -1.0;
        }

        static double DerivativeTwoX(double x, double y)
        {
            return -Math.Sin(x - 2.5);
        }

        static double DerivativeOneY(double x, double y)
        {
            return Math.Cos(y + 0.5);
        }

        static double DerivativeTwoY(double x, double y)
        {
            return 1.0;
        }

        static double PhiX(double x, double y)
        {
            return Math.Sin(y + 0.5) - 1.0;
        }

        static double PhiY(double x, double y)
        {
            return -Math.Cos(x - 2.0);
        }

        static double[,] GetInverseJacobian(double x, double y)
        {
            double[,] matrix = new double[2, 2];

            // Jacobain:
            /*
                df1/dx  df2/dx
                df1/dy  df2/dy
            */
            matrix[0, 0] = DerivativeOneX(x, y);
            matrix[0, 1] = DerivativeTwoX(x, y);
            matrix[1, 0] = DerivativeOneY(x, y);
            matrix[1, 1] = DerivativeTwoY(x, y);

            double determinant = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            // Inverse:
            /*
                df2/dy / det    -df2/dx / det
                -df1/dy / det   df1/dx / det
            */
            double temp = matrix[0, 0];
            matrix[0, 0] = matrix[1, 1] / determinant;
            matrix[0, 1] = -matrix[0, 1] / determinant;
            matrix[1, 0] = -matrix[1, 0] / determinant;
            matrix[1, 1] = temp / determinant;

            return matrix;
        }

        static AnalysisResult NewtonsApproximation(double x0, double y0, double epsilon)
        {
            int iterations = 0;

            double lastX;
            double x = x0;

            double lastY;
            double y = y0;
            do
            {
                iterations++;
                lastX = x;
                lastY = y;

                double[,] matrix = GetInverseJacobian(lastX, lastY);

                x = lastX - matrix[0, 0] * FunctionOne(lastX, lastY) - matrix[0, 1] * FunctionTwo(lastX, lastY);
                y = lastY - matrix[1, 0] * FunctionOne(lastX, lastY) - matrix[1, 1] * FunctionTwo(lastX, lastY);
            }
            while (Math.Abs(x - lastX) >= epsilon || Math.Abs(y - lastY) >= epsilon);

            return new AnalysisResult()
            {
                ApproximationX = x,
                ApproximationY = y,

                Accuracy = epsilon,
                Iterations = iterations
            };
        }

        static AnalysisResult FixedPointIterationApproximation(double x0, double y0, double epsilon)
        {
            int iterations = 0;

            double lastX;
            double x = x0;

            double lastY;
            double y = y0;
            do
            {
                iterations++;
                lastX = x;
                lastY = y;

                x = PhiX(lastX, lastY);
                y = PhiY(lastX, lastY);
            }
            while (Math.Abs(x - lastX) >= epsilon);

            return new AnalysisResult()
            {
                ApproximationX = x,
                ApproximationY = y,

                Accuracy = epsilon,
                Iterations = iterations
            };
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа №4");
            Console.WriteLine();
            
            double x0 = GetUserDoubleWithMessage("x0");
            double y0 = GetUserDoubleWithMessage("y0");
            double epsilon = GetUserDoubleWithMessage("epsilon");

            Console.WriteLine();

            Console.WriteLine("Метод Ньютона.");
            AnalysisResult newtons = NewtonsApproximation(x0, y0, epsilon);
            Console.WriteLine(newtons);

            Console.WriteLine("Метод простых итераций.");
            AnalysisResult fixedPointIteration = FixedPointIterationApproximation(x0, y0, epsilon);
            Console.WriteLine(fixedPointIteration);

            string comparisonSummary = (newtons.Iterations - fixedPointIteration.Iterations) switch
            {
                0 =>
                    "Методы сходятся одинаково быстро.",
                < 0 =>
                    $"Метод Ньютона сходится быстрее метода простых итераций на кол-во итераций: " +
                    $"{ fixedPointIteration.Iterations - newtons.Iterations }",
                > 0 =>
                    $"Метод простых итераций сходится быстрее метода Ньютона на кол-во итераций: " +
                    $"{ newtons.Iterations - fixedPointIteration.Iterations }"
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
