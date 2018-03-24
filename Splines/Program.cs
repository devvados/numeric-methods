using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splines
{
    class Program
    {
        public struct SplineTuple
        {
            public double a, b, c, d, x;
        }
        class CubicSpline
        {
            public SplineTuple[] splines; // Сплайн
            public void BuildSpline(double[] x, double[] y, int n)
            {
                splines = new SplineTuple[n];
                for (int i = 0; i < n; ++i)
                {
                    splines[i].x = x[i];
                    splines[i].a = y[i];
                }
                splines[0].c = splines[n - 1].c = 0.0;

                double[] alpha = new double[n - 1];
                double[] beta = new double[n - 1];
                alpha[0] = beta[0] = 0.0;
                for (int i = 1; i < n - 1; ++i)
                {
                    double hi = x[i] - x[i - 1];
                    double hi1 = x[i + 1] - x[i];
                    double A = hi;
                    double C = 2.0 * (hi + hi1);
                    double B = hi1;
                    double F = 6.0 * ((y[i + 1] - y[i]) / hi1 - (y[i] - y[i - 1]) / hi);
                    double z = (A * alpha[i - 1] + C);
                    alpha[i] = -B / z;
                    beta[i] = (F - A * beta[i - 1]) / z;
                }
                // Нахождение решения - обратный ход метода прогонки
                for (int i = n - 2; i > 0; --i)
                {
                    splines[i].c = alpha[i] * splines[i + 1].c + beta[i];
                }
                // По известным коэффициентам c[i] находим значения b[i] и d[i]
                for (int i = n - 1; i > 0; --i)
                {
                    double hi = x[i] - x[i - 1];
                    splines[i].d = (splines[i].c - splines[i - 1].c) / hi;
                    splines[i].b = hi * (2.0 * splines[i].c + splines[i - 1].c) / 6.0 + (y[i] - y[i - 1]) / hi;
                }
            }
        }

        static void Main(string[] args)
        {
            CubicSpline CS = new CubicSpline();
            double[] x = new double[21];
            double[] y = { -1.24, -1.17, -1.08, -0.96, -0.84, -0.79, -0.8, -0.9, -1.1, -1.21,
                -1.02, -1.28, -1.32, -1.34, -1.36, -1.37, -1.37, -1.36, -1.35, -1.33, -1.30 };
            x[0] = (-2 * Math.PI) / 21;
            Console.WriteLine();
            Console.WriteLine("\tx[0] = {0}", x[0]);
            for (int k = 1; k < y.Length; k++)
            {
                x[k] = (2 * Math.PI * (k - 1)) / 21;
                Console.WriteLine("\tx[{0}] = {1}", k, x[k]);
            }
            CS.BuildSpline(x, y, 21);
            Console.WriteLine();
            Console.Write("\t\tx");
            Console.Write("\t\t    a");
            Console.Write("\t    b");
            Console.Write("\t    c");
            Console.Write("\t    d\n");
            int i = 0;
            foreach (SplineTuple v in CS.splines)
            {
                if (i > 0 && i < 20)
                {
                    Console.Write("\t{0:f3} < x <{1,6:f3}\t", (x[i] - (x[i + 1] - x[i]) / 2.0), (x[i] + (x[i + 1] - x[i]) / 2.0));
                    //Console.WriteLine();
                    //double x1 = (x[i] - (x[i + 1] - x[i]) / 2.0);
                    //double x2 = (x[i] + (x[i + 1] - x[i]) / 2.0);
                    //double h = ((x2) - (x1))/10.0;
                    //for (int m = 0; m < 10; m++)
                    //{
                    //    x1 += Math.Abs(h);
                    //    Console.WriteLine("{0}", x1);
                    //}
                }
                if (i == 0)
                {
                    Console.Write("\t{0:f3} < x < {1,6:f3}\t", x[i], (x[i] + (x[i + 1] - x[i]) / 2.0));
                    //Console.WriteLine();
                    //double x1 = x[i];
                    //double x2 = (x[i] + (x[i + 1] - x[i]) / 2.0);
                    //double h = (x2 - x1) / 10.0;
                    //for (int j = 0; j < 10; j++)
                    //{
                    //    x1 += Math.Abs(h);
                    //    Console.WriteLine("{0}", x1);
                    //}
                }
                if (i == 20)
                {
                    Console.Write("\t{0:f3} < t < {1,6:f3}\t", (x[i - 1] + (x[i] - x[i - 1]) / 2.0), x[i]);
                }
                Console.Write("{0,7:f3}\t", v.a);
                Console.Write("{0,7:f3}\t", v.b);
                Console.Write("{0,7:f3}\t", v.c);
                Console.Write("{0,7:f3}\t\n", v.d);
                Console.WriteLine();
                i++;
            }
        }
    }
}
