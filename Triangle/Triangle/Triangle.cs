using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Triangle
{
    public class Triangle
    {
        public static string resultString;

        public string[] m_array = new string[3];
        private static object m_args;

        public string this[int index]
        {
            get { return m_array[index]; }
            set { m_array[index] = value; }
    }

        static double[] ParseArgs(string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException();
            }

            foreach (string arg in args)
            {
                Console.WriteLine("arg: " + arg);
            }

            for (int i = 0; i < args.Length; i++)
            {
                args[i] = args[i].Trim(new Char[] { ',', ';', '\n' });
            }

            double[] result = new double [3];
            for (int i = 0; i < args.Length; i ++)
            {
                result[i] = double.Parse(args[i].Replace(',', '.'), CultureInfo.InvariantCulture);
            }

            if (double.IsInfinity(result[0]) || double.IsInfinity(result[1]) || double.IsInfinity(result[2]))
            {
                throw new OverflowException();
            }

            Triangle.m_args = args;

            return result;
        }

        static bool IsTriangle(double[] edges)
        {
            return edges[0] + edges[1] > edges[2] && edges[0] + edges[2] > edges[1] && edges[1] + edges[2] > edges[0];
        }

        static bool IsEquilateralTriangle(double[] edges)
        {
            return edges[0] == edges[1] && edges[0] == edges[2] && edges[1] == edges[2];
        }

        static bool IsIsoscelesTriangle(double[] edges)
        {
            return edges[0] == edges[1] || edges[0] == edges[2] || edges[1] == edges[2];
        }

        public static void Main(string[] args)
        {
            double[] edges;
            try
            {
                edges = ParseArgs(args);
            }
            catch
            {
                resultString = "Неизвестная ошибка";
                Console.WriteLine(resultString);
                return;
            }

            if (IsTriangle(edges))
            {
                if (IsEquilateralTriangle(edges))
                {
                    resultString = "Равносторонний";
                }
                else if (IsIsoscelesTriangle(edges))
                {
                    resultString = "Равнобедренный";
                }
                else
                {
                    resultString = "Обычный";
                }
            }
            else
            {
                resultString = "Не треугольник";
            }
            Console.WriteLine(resultString);
        }

        public static void WriteTestsResultsInFile(string expectedResult, string programResult, int testNumber, StreamWriter sw)
        {
            if (expectedResult == programResult)
            {
                sw.WriteLine(testNumber + " sucсess;");
            }
            else
            {
                sw.WriteLine(testNumber + " error;");
            }
        }
    }
}
