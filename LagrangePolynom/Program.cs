using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagrangePolynom
{
    class Program
    {
        static double[] xmas = new double[] { 3.0, 3.5, 4.0, 4.5 };
        static double[] ymas = new double[] { 20.086, 33.115, 54.598, 90.017 };

        static double[] xmas1 = new double[] { 0.00, 0.05, 0.10, 0.15, 0.20, 0.25, 0.30, 0.35, 0.40, 0.45, 0.50 };
        static double[] ymas1 = new double[] { 0.2808, 0.3127, 0.3454, 0.3790, 0.4131, 0.4477, 0.4825, 0.5174, 0.5522, 0.5868, 0.6209 };

        static double SolvePolynom(double x)
        {

            double solve = 0.0, sum = 0.0, mult = 1.0;
            for (int i = 0; i < ymas.Length; i++)
            {
                for(int j = 0; j < xmas.Length; j++)
                {
                    if(j!=i)
                    {
                        mult *= (x - xmas[j]) / (xmas[i] - xmas[j]);
                    }
                }
                sum = sum + ymas[i] * mult;
                mult = 1.0;
            }
            return sum;
        }
        static double NewtonFirst(double t, int n, double[] x, double[] y)
        {
            double res = y[0], F, den;
            int i, j, k;
            for (i = 1; i <= n; i++)
            {
                F = 0;
                for (j = 0; j <= i; j++)
                {
                    den = 1;
                    for (k = 0; k <= i; k++)
                    {
                        if (k != j) den *= (x[j] - x[k]);
                    }
                    F += y[j] / den;
                }
                for (k = 0; k < i; k++) F *= (t - x[k]);
                res += F;
            }
            return res;
        }

        static double NewtonSecond(double t, int n, double[] x, double[] y)
        {
            double res = y[n], F, den;
            int i, j, k;
            for (i = n; i >= 1; i--)
            {
                F = 0;
                for (j = i; j >= 0; j--)
                {
                    den = 1;
                    for (k = i; k >=0; k--)
                    {
                        if (k != j) den *= (x[j] - x[k]);
                    }
                    F += y[j] / den;
                }
                for (k = i; k >0; k--) F *= (t - x[k]);
                res += F;
            }
            return res;
        }

        static void Main(string[] args)
        {
            double x = 3.2, x1 = 0.03, x2 = 0.32;
            Console.WriteLine("Solve L({0}) = {1}", x, SolvePolynom(x));
            double d = NewtonFirst(x1, 3, xmas1, ymas1);
            Console.WriteLine("Solve N1({0}) = {1}", x1, d);
            double d1 = NewtonSecond(x2, 3, xmas1, ymas1);
            Console.WriteLine("Solve N2({0}) = {1}", x2, d1);
        }
    }
}
