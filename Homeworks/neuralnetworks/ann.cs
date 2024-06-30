using System;
using static System.Console;
using static System.Math;

public class ann{
	int n; // number of hidden neurons

	public Func<double,double> f = x => x * Exp(-x * x);
	public Func<double,double> ffd = x => (1 - 2 * x * x) * Exp(- x * x);
	public Func<double,double> fsd = x => Exp(-x * x) * (4 * x * x * x - 6 * x);
	public Func<double,double> F  = x => -0.5 * Exp(-x * x);

	// Parameters
	vector param;
	public double a(int i) {return param[i];}
	public double b(int i) {return param[n + i];}
	public double w(int i) {return param[2 * n + i];}

	// Setters
	public void seta(int i, double x) {param[i] = x;}
	public void setb(int i, double x) {param[n + i] = x;}
	public void setw(int i, double x) {param[2 * n + i] = x;}

	public ann(int n) {
		this.n = n;
		param = new vector(3 * n);
		var rand = new Random();
		for (int i = 0; i < param.size; i++) {
			param[i] = rand.NextDouble() - 0.5;
		}
	}


	// Responese, derivatives, training
	public double response(double x){
		double sum = 0;
		for(int i = 0 ; i < n ; i++) sum += w(i) * f((x - a(i)) / b(i));
		return sum;
	}


	public double firstDerivative(double x){
		double sum = 0;
		for(int i = 0 ; i < n ; i++) sum += w(i) * ffd((x - a(i)) / b(i)) / b(i);
		return sum;
	}

	public double secondDerivative(double x){
		double sum = 0;
		for(int i = 0 ; i < n ; i++) sum += w(i) * fsd((x - a(i)) / b(i)) / b(i) / b(i);
		return sum;
	}

	public double antiDerivative(double x){
		double sum = 0;
		for(int i = 0 ; i < n ; i++) sum += w(i) * F((x - a(i)) / b(i)) * b(i);
		return sum;
	}

	public void train(vector x, vector y) {
		for (int i = 0; i < n; i++) {
			setw(i, 1);
			setb(i, 1);
			seta(i, x[0] + (x[x.size - 1] - x[0]) * i / (n - 1));
		}

		Func<vector, double> cost = v => {
			this.param = v;
			double costp = 0;
			for (int k = 0; k < x.size; k++) costp += Pow(this.response(x[k]) - y[k], 2);
			return costp / x.size;
		};
		param = min.newton(cost, param, 1e-4).Item1;
	} // train

} // ann
