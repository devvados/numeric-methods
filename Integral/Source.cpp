#include <iostream>
#include <cmath>
using namespace std;

double f(const double & x)
{
	return (x + 1.5)*sin(x*x);
}

double f1(const double & x)
{
	return 1/(sqrt(x*x + 4));
}

double deriv(const double & x)
{
	return (2 * (pow(x, 2) - 2)) / pow((pow(x, 2) + 4), 5.0 / 2.0);
}

double fault()
{
	int n = 10;
	double a = 1.6, b = 2.4;
	return (pow((b - a), 3) / (12 * pow(n, 2))) * deriv(2.4);
}

void trapeze(const double & a, const double & b, int n, const double & eps)

{
	double h, x, s = 0, s_old;

	do {
		h = (b - a) / n;
		s_old = s;
		s = 0;

		for (x = a; x < b; x += h)
			s += (f1(x + h) + f1(x)) / 2 * h;

	} while ((abs(s_old - s) > eps));

	cout << "Trapeze method: " << s << endl;
}

double ff(int i, const double & a, const double & h)
{
	return f(a + h*i / 2);
}

double simpson(const double & a, const double & b, int n, const double & eps)
{
	double h, s = 0.0, s_old, s1, s2;
	int i;

	do {
		h = (b - a) / n;
		s_old = s;

		s1 = 0;
		for (i = 2; i < 2 * n - 1; i += 2)
			s1 += ff(i, a, h);

		s2 = 0;

		for (i = 1; i < 2 * n; i += 2)
			s2 += ff(i, a, h);

		s = h / 6 * (ff(0, a, h) + 2 * s1 + 4 * s2 + ff(2 *n, a, h));

	} while ((abs(s_old - s) > eps*h*n / (b - a)));
	return s;
}

int main()
{
	double a, a1, b, b1, eps;
	int n, n1;
	cout.precision(11);
	trapeze(1.6, 2.4, 10, 0.001);
	cout << "Fault = " << fault() << endl;
	double s1 = simpson(0.4, 1.2, 8, 0.001);
	double s2 = simpson(0.4, 1.2, 16, 0.001);
	double fault = fabs(s1 - s2) / 15;
	cout << "Simpson n = 8: " << s1 << endl;
	cout << "Simpson n = 16: " << s2 << endl;
	system("pause");
	return 0;
}

