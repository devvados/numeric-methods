#include <cmath>
#include <iostream>
#define eps 0.001
using namespace std;

double f1(double x) { return   tan(0.58*x+0.1)-x*x; }
double f1deriv(double x) { return -2*x + (0.58)/(pow(cos(0.1+0.58*x),2)); }
double f1deriv2(double x) { return -2 + (0.6728 * tan(0.1+0.58*x))/(pow(cos(0.1+0.58*x),2)); }

void Newton(double a, double b) {
	int n = 0;
	double c = 0.0;
	if (f1(a)*f1deriv2(a) > 0) c = a;
	else c = b;
	cout << endl;
	cout << "Interval [" << a << "," << b << "]" << endl;
	do {
		c = c - f1(c) / f1deriv(c);
		n += 1;
	} while (fabs(f1(c)) >= eps);
	cout << "x=" << c << endl;
	cout << "n = " << n << " iterations" << endl;
}

double f2(double x) { return x+log(x)-0.5; } // - функция для метода половинного деления и итерации

void Divided(double a, double b)
{
	int n = 0;
	double c;
	do {
		c = (a + b) / 2;
		if (f2(c)*f2(a) > 0) a = c;
		else b = c;
		n += 1;
	} while (fabs(a - b) >= eps);
	
	cout << "x=" << c << endl;
	cout << "n = " << n << " iterations" << endl;
}

void Iterative(double x)
{
	double rez; int iter = 0;
	
	cout << "x0= " << x << endl;
	do {
		rez = x;
		//x = (-1) * ((x*x*x+1)/3); - первое уравнение
		x = exp(0.5-x); // - второе уравнение
		iter++;
	} while (fabs(rez - x) > eps && iter<20000);
	cout << "x = " << x << endl;
	cout << "n = " << iter << " iterations" << endl;
}

int main()
{
	cout << "Newton Method" << endl;
	Newton(-1.0, 0.0);
	Newton(0.0, 1.0);
	Newton(2.0, 5.0);
	cout << endl;
	cout << "Iterative Method" << endl;
	Iterative(3); //- указывается начальное приближение
	cout << endl;
	cout << "Divided Method" << endl;
	Divided(1,2.74); //- указывается интервал
	cout << endl;
	system("pause");
	return 0;
}