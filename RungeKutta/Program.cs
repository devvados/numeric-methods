using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RungeKutta
{
    class Program
    {
        static double f(double x)
        {
            return -0.02 * Math.Pow(Math.E, -0.6 * x);
        }
        static double f2(double x)
        {
            return 0.012 * Math.Pow(Math.E, -0.6 * x);
        }
        static double rungekutt(double[] x, double h)
        {
            double k1, k2, k3, k4, y = 0;
            x[0] = 0;
            for (int i = 1; i < x.Length; i++)
            {
                x[i] = x[i - 1] + 0.1;
            }
            Console.WriteLine();
            Console.WriteLine("\tY1:");
            for (int i = 0; i < x.Length; i++)
            {
                k1 = f(x[i]);
                k2 = f(x[i] + (h / 2));
                k3 = f(x[i] + (h / 2));
                k4 = f(x[i] + h);
                y = y + (h / 6) * (k1 + 2 * k2 + 2 * k3 + k4);
                Console.WriteLine("\tШаг №{0}, y = {1};", i, y);
            }
            Console.WriteLine();
            return y;
        }
        static double rungekutt2(double[] x, double h)
        {
            double k1, k2, k3, k4, y = 1.0 / 3.0;
            x[0] = 0;
            for (int i = 1; i < x.Length; i++)
            {
                x[i] = x[i - 1] + 0.1;
            }
            Console.WriteLine("\tY2:");
            for (int i = 0; i < x.Length; i++)
            {
                k1 = f2(x[i]);
                k2 = f2(x[i] + (h / 2));
                k3 = f2(x[i] + (h / 2));
                k4 = f2(x[i] + h);
                y = y + (h / 6) * (k1 + 2 * k2 + 2 * k3 + k4);
                Console.WriteLine("\tШаг №{0}, y = {1};", i, y);
            }
            Console.WriteLine();
            return y;
        }
        static void Main(string[] args)
        {
            double h = 0.1;
            double[] array = new double[31];
            Console.WriteLine("\n\tРешение системы: \n\ty1 = {0};\n\ty2 = {1}; \n", rungekutt(array, h), rungekutt2(array, h));
        }
    }
}
