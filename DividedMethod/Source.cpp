#include <conio.h>
#include <cmath>
#include <iostream>
#define pi 3.14
#define eps 0.001

using namespace std;
double f(double x) {

	return x*x*x - 6.0*x - 8.0;
}
double f1(double x)
{
	return tan(0.58*x + 0.1) - x*x;
}
int main() {
	int n = 0;
	double a, b, c;
	cout << "a="; cin >> a;
	cout << "b="; cin >> b;
	do {
		c = (a + b) / 2;
		if (f(c)*f(a) > 0) a = c;
		else b = c;

		n += 1;
		cout << a << "\t" << b << "\t" << c << "\t" << f(c) << endl;
	} while (fabs(a - b) >= eps);
	cout << "c=" << c << "\n";
	cout << "n=" << n << "\n";
	system("pause");
	return 0;
}
