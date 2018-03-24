using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChislMeth2
{
    class Program
    {
        const double eps = 0.00001;
        static void Main(string[] args)
        {
            double a1, a2, a3, _a;
            double[,] mas = new double[3, 3];
            double[] x = new double[3], temp = new double[3];
            mas[0, 0] = 40.21 / 42.18;
            mas[0, 1] = -4.32 / 42.18;
            mas[0, 2] = -3.85 / 42.18;
            mas[1, 0] = 10.24 / 41.32;
            mas[1, 1] = -5.31 / 41.32;
            mas[1, 2] = -1.52 / 41.32;
            mas[2, 0] = 12.82 / 34.32;
            mas[2, 1] = -3.49 / 34.32;
            mas[2, 2] = -4.85 / 34.32;
            a1 = Math.Abs(mas[0, 1] + mas[0, 2]);
            a2 = Math.Abs(mas[1, 1] + mas[1, 2]);
            a3 = Math.Abs(mas[2, 1] + mas[2, 2]);
            _a = compare(a1, a2, a3);

            for (int i = 0; i < 3; i++)
            {
                x[i] = mas[i, 0];
            }

            do
            {
                temp = (double[])x.Clone();
                x[0] = mas[0, 0] + mas[0, 1] * temp[1] + mas[0, 2] * temp[2];
                x[1] = mas[1, 0] + mas[1, 1] * temp[0] + mas[1, 2] * temp[2];
                x[2] = mas[2, 0] + mas[2, 1] * temp[0] + mas[2, 2] * temp[1];

            } while ((_a / (1 - _a)) * distance(temp, x) >= eps);
            Console.WriteLine("Решение системы : ({0:f5};{1:f5};{2:f5})\nТочность Е={3:f5}", x[0], x[1], x[2], eps);
            Console.Read();

        }
        static double compare(double a1, double a2, double a3)
        {
            double max = a1;
            if (max < a2) max = a2;
            else if (max < a3) max = a3;
            return max;
        }
        static double distance(double[] x0, double[] x1)
        {
            double a1, a2, a3;
            a1 = Math.Abs(x1[0] - x0[0]);
            a2 = Math.Abs(x1[1] - x0[1]);
            a3 = Math.Abs(x1[2] - x0[2]);
            double max = a1;
            if (max < a2) max = a2;
            else if (max < a3) max = a3;
            return max;
        }
    }
}
