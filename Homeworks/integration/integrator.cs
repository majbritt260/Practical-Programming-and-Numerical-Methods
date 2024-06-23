using System;
using static System.Math;

public static class integrator{

	//////////  Recursive open-quadrature adaptive integrator /////////
	public static (double , int) integrate(Func<double,double> f, double a, double b,
	double δ = 0.000001, double ε = 0.000001, double f2 = double.NaN, double f3 = double.NaN){
	double h = b - a; // interval
	int ncalls = 0;
	if (double.IsNaN(f2)){ // first call
		f2 = f(a + 2 * h / 6);
		f3 = f(a + 4 * h / 6);
		ncalls += 2;
		}

	ncalls += 2;
	double f1 = f(a + h / 6);
	double f4 = f(a + 5 * h / 6);
	double Q = (2 * f1 + f2 + f3 + 2 * f4) / 6 * (b - a); // higher order rule
	double q = (f1 + f2 + f3 + f4) / 4 * (b - a); // lower order rule
	double err = Abs(Q - q); // local error

	if (err <= δ + ε * Abs(Q)) return (Q , ncalls);

	(double left , int ncallsLeft) = integrate(f, a, (a + b) / 2, δ / Sqrt(2), ε, f1, f2);
	(double right , int ncallsRight) = integrate(f, (a + b) / 2, b, δ / Sqrt(2), ε, f3, f4);

	return (left + right , ncallsLeft + ncallsRight + ncalls);
        }

	////////// Error-function //////////
	// Single precision error function (Abramowitz and Stegun, from Wikipedia)
	public static double erf(double x){
	if (x < 0) return -erf(-x);
	double[] a = {0.254829592, -0.284496736, 1.421413741, -1.453152027, 1.061405429};
	double t = 1 / (1 + 0.3275911 * x);
	double sum = t * (a[0] + t * (a[1] + t * (a[2] + t * (a[3] + t * a[4]))));
	return 1 - sum * Exp(-x * x);
	}

	// Integral representation of error function
	public static double int_erf(double x){
		if (x < 0) return -erf(-x);
		if (x > 1) return 1 - 2 / Sqrt(PI) * integrator.integrate(t => Exp( - Pow(x + (1 - t) / t, 2)) / t  /t, 0, 1).Item1;
		else return 2 / Sqrt(PI) * integrator.integrate(z => Exp(- z * z), 0, x).Item1;
	}

	////////// Clenshaw Curtis variable transformation //////////
	public static (double , int) ClenshawCurtis(Func<double,double> f, double a, double b, double delta = 0.001, double eps = 0.001, double f2 = double.NaN, double f3 = double.NaN){
		Func<double,double> ftransformed = x => f((a + b) / 2 + (b - a) / 2 * Cos(x)) * Sin(x) * (b - a) / 2;
		return integrate(ftransformed, 0, PI, delta, eps, f2, f3);
	}

} // integrator
