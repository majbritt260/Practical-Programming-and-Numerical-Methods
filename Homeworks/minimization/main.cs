using System;
using System.IO;
using static System.Math;
using static System.Console;
using static min;
using System.Globalization;

public class main{
public static void Main(){
	// Borders for neat output file
	string border = new string('-', 85);

	// PART A
	WriteLine(border);
	WriteLine($"PART A: Newton's method with numerical gradient, numerical Hessian matrix and back-tracking linesearch");
	WriteLine(border);

	// Rosenbrock function
	Func<vector, double> rosenbrock = v => Pow(1 - v[0], 2) + 100 * Pow(v[1] - Pow(v[0], 2), 2);

	// Himmelblau function
	Func<vector, double> himmelblau = v => Pow(v[0] * v[0] + v[1] - 11, 2) + Pow(v[0] + v[1] * v[1] - 7, 2);

	// Find minimum of Rosenbrock function
	vector x0 = new vector("2 2");
	var (result, steps) = newton(rosenbrock, x0, 1e-3);
	WriteLine($"Rosenbrock minimum: {result[0]}, {result[1]} in {steps} steps");

	// Find minimum of Himmelblau function
	vector y0 = new vector("-3 -3");
	(result, steps) = newton(himmelblau, y0);
	WriteLine($"Himmelblau minimum: {result[0]}, {result[1]} in {steps} steps");

	// PART B
        WriteLine(border);
        WriteLine($"PART B: Higgs boson discovery");
        WriteLine(border);

	// Read data for Higgs boson discovery
	var energy = new genlist<double>();
	var signal = new genlist<double>();
	var error = new genlist<double>();
	var separators = new char[] { ' ', '\t' };
	var options = StringSplitOptions.RemoveEmptyEntries;
	do{
		string line = Console.In.ReadLine();
		if (line == null) break;
		string[] words = line.Split(separators, options);
		energy.add(double.Parse(words[0]));
		signal.add(double.Parse(words[1]));
		error.add(double.Parse(words[2]));
	} while (true);



	// Define Breit-Wigner function
	Func<double, vector, double> BreitWigner = (double E, vector v) => {
		double m = v[0];
		double Γ = v[1];
		double A = v[2];
		return A / (Pow(E - m, 2) + Γ * Γ / 4);
	};

	// Define Deviation function
	Func<vector, double> Deviation = u => {
		double sum = 0;
		for (int i = 0; i < energy.size; i++) {
			sum += Pow((BreitWigner(energy[i], u) - signal[i]) / error[i], 2);
		}
		return sum;
	};

	// Initial guess for m, Γ, A
	vector initialGuess = new vector("126 2 11");
	vector fitResult = newton(Deviation, initialGuess).Item1;
	WriteLine($"Initial guess: m = {initialGuess[0]}, Γ = {initialGuess[1]}, A = {initialGuess[2]}");
	WriteLine($"Fitted parameters: m = {fitResult[0]}, Γ = {fitResult[1]}, A = {fitResult[2]}");
	WriteLine("The plotted fit together with the experimental data is vizualised in Higgs.svg");
	using(var output = new StreamWriter("Fitting.txt")){
		int iterations = 1000;
		double EE = 0;
		double fit = 0;
		var IC = CultureInfo.InvariantCulture;
		for(int i = 0; i < iterations; i++){
			fit = BreitWigner(EE, fitResult);
			EE = energy[0] + (energy[energy.size - 1] - energy[0]) * i / iterations;
			output.WriteLine($"{EE.ToString(IC)} {fit.ToString(IC)}");

		}

	}

} // Main
} // main
