using LiveCharts;
using LiveCharts.Wpf;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DifferentialEq
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        List<double> valuesY1 = new List<double>();
        List<double> valuesY2 = new List<double>();
        List<double> valuesY3 = new List<double>();
        
        public SeriesCollection SeriesCollectionRKM { get; set; }
        public SeriesCollection SeriesCollectionAM { get; set; } 
        public SeriesCollection SeriesCollectionSM { get; set; }

        double[,] decision = new double[2, 2001];
        double[,] AddamsDis = new double[2, 2001];

        /// <summary>
        /// Y'=Z
        /// </summary>
        /// <param name="x">Значение x</param>
        /// <param name="y">Значение y</param>
        /// <param name="z">Значение z</param>
        /// <returns></returns>
        public double Y(double x, double y, double z) 
        {
            return z;
        }

        /// <summary>
        /// Z'=(Y/X)-Z+((X+1)/X)
        /// </summary>
        /// <param name="x">Значение x</param>
        /// <param name="y">Значение y</param>
        /// <param name="z">Значение z</param>
        /// <returns></returns>
        public double Z(double x, double y, double z) 
        {
            return (y / x) - z + ((x + 1) / x);
        }

        public double Border(double x1, double y, double z, double a, double b)
        {
            return 0;
        }

        public MainWindow()
        {
            InitializeComponent();

            //ВЫВОД ФОРМУЛ
            TBFormula.Text = @"y''+y'-\frac{y}{x} = \frac{x+1}{x},";
            TBLeftBorder.Text = @"y(0,5)=-\frac{1}{2}\ln2,";
            TBRightBorder.Text = @"y'(1)=0";
        }

        #region Обработчики кнопок

        private void BRungeKuttaStart_Click(object sender, RoutedEventArgs e)
        {
            int n = 8;
            double
                eps = 0.00001,
                yt,
                error = -100.00,
                x0 = 0.5, //
                x1 = 1.0, //границы
                y0 = -4.0,
                z0 = 1.5,
                yk = y0,
                yl = y0,
                yr = 5.0;
            double[] w1l, w1k, y = new double[2];
            double[] yh = new double[n + 1];
            double[] yh2 = new double[2 * n + 1];

            do
            {
                do
                {
                    yt = yk;
                    yk = (yl + yr) / 2.0;
                    w1l = FourthOrder(x0, yl, z0, x0, x1, n);
                    w1k = FourthOrder(x0, yk, z0, x0, x1, n);
                    for (int j = 0; j <= n; j++)
                    { yh[j] = decision[1, j]; }


                    if (Border(x1, w1l[0], w1l[1], x0, x1) * Border(x1, w1k[0], w1k[1], x0, x1) < 0)
                    { yr = yk; }
                    else
                    { yl = yk; }

                }
                while (Math.Abs(yt - yk) > eps);

                y = FourthOrder(x0, yk, z0, x0, x1, 2 * n);

                for (int j = 0; j <= 2 * n; j++)
                { yh2[j] = decision[1, j]; }

                for (int j = 1; j < n; j++)
                {
                    if (error < Math.Abs(yh[j] - yh2[2 * j]))
                    {
                        error = Math.Abs(yh[j] - yh2[2 * j]);
                    }
                }
            }
            while (error > 15.000 * eps);

            TBError1.Text += "Максимальная ошибка =  " + error.ToString() + "\n";
            TBError1.Text += "Количество разбиений: n = " + n.ToString();

            FourthOrder(x0, yk, z0, x0, x1, n);
            double h = (x1 - x0) / (double)n;
            
            for (int j = 0; j <= n; j++)
            {
                valuesY1.Add(decision[1, j]);
            }       
        }

        private void BAddamsStart_Click(object sender, RoutedEventArgs e)
        {
            int n = 30;
            double 
                eps = 0.00001, 
                yt, 
                error = -100.00, 
                x0 = 0.5, 
                x1 = 1.0, 
                y0 = -4.0, 
                z0 = 1.5, 
                yk = y0, 
                yl = y0, 
                yr = 5.0, 
                y1, y2; // y(0.5)=p
            double[] w1l, w1k, y = new double[2];
            double[] yh = new double[n + 1];
            double[] yh2 = new double[2 * n + 1];

            do
            {
                do
                {
                    yt = yk;
                    yk = (yl + yr) / 2.0;

                    w1l = AdamsMethod(x0, yl, z0, x0, x1, n);
                    w1k = AdamsMethod(x0, yk, z0, x0, x1, n);
                    for (int j = 0; j <= n; j++)
                    {
                        yh[j] = AddamsDis[1, j];
                    }

                    if (Border(x1, w1l[0], w1l[1], x0, x1) * Border(x1, w1k[0], w1k[1], x0, x1) < 0)
                    { yr = yk; }
                    else
                    { yl = yk; }

                }
                while (Math.Abs(yt - yk) > eps);

                y = AdamsMethod(x0, yk, z0, x0, x1, 2 * n);
                for (int j = 0; j <= 2 * n; j++)
                { yh2[j] = AddamsDis[1, j]; }

                for (int j = 1; j <= n; j++)
                {
                    if (error < Math.Abs(yh[j] - yh2[2 * j]))
                    {
                        error = Math.Abs(yh[j] - yh2[2 * j]);
                    }
                }

            }
            while (error > 7.000 * eps);


            TBError2.Text += "Максимальная ошибка =  " + error.ToString() + "\n";
            TBError2.Text += "Количество разбиений: n = " + n.ToString();

            AdamsMethod(x0, yk, z0, x0, x1, n);
            double h = (x1 - x0) / (double)n;

            for (int j = 0; j <= n; j++)
            {
                valuesY2.Add(decision[1, j]);
            }            
        }

        private void BStartSweep_Click(object sender, RoutedEventArgs e)
        {
            int n = 128;
            double x0 = 0.5, x1 = 1, eps = 0.00001, error = -100;
            double[] x = new double[n + 1];
            double[] y = new double[n + 1];
            double[] y2 = new double[2 * n + 1];

            double h = (x1 - x0) / (double)n;
            for (int i = 0; i < n + 1; i++)
            {
                x[i] = x0 + (double)i * h;
            }
            y = SweepMethod(n);
            y2 = SweepMethod(2 * n);

            for (int j = 1; j <= n; j++)
            {
                if (error < Math.Abs(y[j] - y2[2 * j]))
                {
                    error = Math.Abs(y[j] - y2[2 * j]);
                }
            }
            TBError3.Text += "Максимальная ошибка =  " + error.ToString() + "\n";

            for (int i = 0; i < n + 1; i++)
            {
                valuesY3.Add(y[i]);
            }
            TBError3.Text += "Количество разбиений: n = " + n.ToString();
        }

        #endregion

        #region Методы решения дифура

        /// <summary>
        /// Метод Рунге-Кутты 4-го порядка
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public double[] FourthOrder(double x, double y, double z, double a, double b, int n)
        {
            double k1, k2, k3, k4, l1, l2, l3, l4;
            double[] fres = new double[2];                      //fres(y,z)
            double h = (b - a) / (double)n;
            decision[1, 0] = y;
            decision[0, 0] = z;

            for (int i = 0; i < n; i++)
            {
                x = a + i * h;
                k1 = Y(x, y, z);
                l1 = Z(x, y, z);

                k2 = Y(x + h / 2.0, y + h * k1 / 2.0, z + h * l1 / 2.0);
                l2 = Z(x + h / 2.0, y + h * k1 / 2.0, z + h * l1 / 2.0);

                k3 = Y(x + h / 2.0, y + h * k2 / 2.0, z + h * l2 / 2.0);
                l3 = Z(x + h / 2.0, y + h * k2 / 2.0, z + h * l2 / 2.0);

                k4 = Y(x + h, y + h * k3, z + h * l3);
                l4 = Z(x + h, y + h * k3, z + h * l3);

                y += (k1 + 2.0 * k2 + 2.0 * k3 + k4) * h / 6.0;
                z += (l1 + 2.0 * l2 + 2.0 * l3 + l4) * h / 6.0;

                fres[0] = y;
                fres[1] = z;
                decision[1, i + 1] = y;
                decision[0, i + 1] = z;
            }
            return fres;
        }

        /// <summary>
        /// Метод Адамса
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public double[] AdamsMethod(double x, double y, double z, double a, double b, int n)
        {
            double[] fres = new double[2];                  //fres(y,z)
            double h = (b - a) / (double)n;
            AddamsDis[1, 0] = y;
            AddamsDis[0, 0] = z;
            FourthOrder(x, y, z, a, b, n);
            AddamsDis[1, 1] = decision[1, 1];
            AddamsDis[0, 1] = decision[0, 1];

            AddamsDis[1, 2] = decision[1, 2];
            AddamsDis[0, 2] = decision[0, 2];

            for (int i = 3; i <= n; i++)
            {

                AddamsDis[1, i] =
                    AddamsDis[1, i - 1] + h * (23.000 * Y((a + (double)(i - 1) * h),
                    AddamsDis[1, i - 1],
                    AddamsDis[0, i - 1]) / 12.000 - 4.000 * Y((a + (double)(i - 2) * h),
                    AddamsDis[1, i - 2],
                    AddamsDis[0, i - 2]) / 3.000 + 5.000 * Y((a + (double)(i - 3) * h),
                    AddamsDis[1, i - 3], AddamsDis[0, i - 3]) / 12.000);
                AddamsDis[0, i] =
                    AddamsDis[0, i - 1] + h * (23.000 * Z((a + (double)(i - 1) * h),
                    AddamsDis[1, i - 1],
                    AddamsDis[0, i - 1]) / 12.000 - 4.000 * Z((a + (double)(i - 2) * h),
                    AddamsDis[1, i - 2], AddamsDis[0, i - 2]) / 3.000 + 5.000 * Z((a + (double)(i - 3) * h),
                    AddamsDis[1, i - 3],
                    AddamsDis[0, i - 3]) / 12.000);

            }
            fres[0] = AddamsDis[1, n];
            fres[1] = AddamsDis[0, n];
            return fres;
        }

        /// <summary>
        /// Метод прогонки
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public double[] SweepMethod(int n)
        {
            double x0 = 0.5, x1 = 1.0, f, m0, F0, m1, Fm;
            double[] x = new double[n + 1];
            double[] y = new double[n + 1];
            double[] r = new double[n + 1];
            double[] s = new double[n + 1];
            double a;
            double[] b = new double[n];
            double c;

            double h = (x1 - x0) / (double)n;
            f = Math.Pow(h, 2.00);


            for (int i = 0; i < n + 1; i++)
            {
                x[i] = x0 + (double)i * h;
            }
            a = 1.000 - h;
            c = 1.000 + h;

            for (int i = 1; i < n; i++)
            {
                b[i] = -2.000 - 4.000 * Math.Pow(h, 2) / x[i];
            }

            m0 = (a + c) / b[1];
            F0 = (f + 3 * a * h) / b[1];

            r[1] = -m0;
            s[1] = F0;

            for (int i = 1; i < n; i++)
            {
                r[i + 1] = -c / (a * r[i] + b[i]);
                s[i + 1] = (f - a * s[i]) / (a * r[i] + b[i]);
            }

            m1 = (b[n - 1] - 2.00 * h * c) / (a + c);
            Fm = (f - 8.00 * h * c) / (a + c);

            y[n] = (Fm - s[n]) / (m1 + r[n]);

            for (int i = n; i > 0; i--)
            {
                y[i - 1] = r[i] * y[i] + s[i];
            }
            return y;
        }

        #endregion

        /// <summary>
        /// Показать все графики
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowCharts_Click(object sender, RoutedEventArgs e)
        {
            SeriesCollectionRKM = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Рунге-Кутта",
                    Values = new ChartValues<double>(valuesY1.AsEnumerable()),
                    Fill = new SolidColorBrush
                    {
                        Color = Colors.Red,
                        Opacity = .4
                    }
                }
            };
            SeriesCollectionAM = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Адамса",
                    Values = new ChartValues<double>(valuesY2.AsEnumerable())
                }
            };
            SeriesCollectionSM = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Прогонки",
                    Values = new ChartValues<double>(valuesY3.AsEnumerable()),
                    Fill = new SolidColorBrush
                    {
                        Color = Colors.Green,
                        Opacity = .4
                    }
                }
            };
            DataContext = this;
        }
    }
}
