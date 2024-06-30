using System;
using System.IO;
using static System.Console;
using static System.Math;
using System.Globalization;

static class main{
static void Main(){
	WriteLine("The implementation of A and B is vizualised in plot.svg.");

	Func<double,double> wavelet = x => Cos(5 * x) * Exp(- x * x);

	int points = 50; // training points
	int n = 6; // no. neurons
	int resolution = 100;
	var IC = CultureInfo.InvariantCulture;

	vector xs = new vector(points);
	vector ys = new vector(points);

	// Training data
	using(var output = new StreamWriter($"Training.txt")){
		for(int i = 0 ; i < points ; i++){
			double x = 2.0 * i / points - 1;
			xs[i] = x;
			ys[i] = wavelet(x);
			output.WriteLine($"{xs[i].ToString(IC)} {ys[i].ToString(IC)}");
		}
	}

	// Neural network data
	ann data = new ann(n);
	data.train(xs , ys);

	using(var output = new StreamWriter($"Interpolation.txt")){
		for(int i = 0; i < resolution; i++){
			double x = 2.0 * i / resolution - 1;
			output.WriteLine($"{x.ToString(IC)} {data.response(x).ToString(IC)} " +
                              $"{data.firstDerivative(x).ToString(IC)} {data.secondDerivative(x).ToString(IC)} " +
                              $"{data.antiDerivative(x).ToString(IC)}");
			}
		}
} // Main
} // main
