#include <iostream>
#include <math.h>
#include <iomanip>

#define eps 0.00001
using namespace std;
const int n = 3;

float A[n][n] = {
	{ 35.41, 2.31, 1.44 },
	{ 2.42, 28.49, 4.85 },
	{ 3.21, 1.52, 34.12 },
};

float  B[n] = { 10.04, 20.16, 12.25 };

float  X[30][n] = { 0 };
float  TEST[n];
float  summ;
int    k = 0;

bool ExitFunc()
{
	if (k == 0) return true;
	for (int i = 0; i<n; i++) 
	{
		if (fabs(X[k][i] - X[k - 1][i]) > eps)
			return true;
	}
	return false;
}

void main(void)
{
	do
	{
		for (int i = 0; i<n; i++)
		{
			summ = 0;
			for (int j = 0; j<n; j++)
				if (i != j)
					summ += A[i][j] * X[k][j];
			X[k + 1][i] = (1 / A[i][i]) * (B[i] - summ);
		}
		k++;
	} while (ExitFunc());

	//Проверка
	
	cout << "\nResult: ";
	for (int j = 0; j < n; j++)
		cout << setprecision(5) << X[k][j] << " ";

	cout << "\nTest: ";
	for (int i = 0; i<n; i++)
	{
		for (int j = 0; j<n; j++)
			TEST[i] += (A[i][j] * X[k][j]);
		cout << setprecision(5) << TEST[i] << " ";
	}
	cout << endl;
	cout << k << " Iterations" << endl;

	system("pause");
}