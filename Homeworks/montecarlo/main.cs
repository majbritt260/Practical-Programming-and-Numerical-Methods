using System;
using System.IO;
using static System.Console;
using static System.Math;
using System.Globalization;

public static class main{
public static void Main(){

	// Borders for neat output file
	string border = new string('-', 75);

	// Generate data
	gendata();

	///////////////////// PART A ////////////////////
	WriteLine(border);
        WriteLine("PART A: Plain Monte Carlo integration");
        WriteLine(border);

	WriteLine("A plot of the estimated error and the actual error as functions of the number\n" +
		"of samplings can be seen in UnitCircle.svg, as well as a plot comparing the\n" +
		"integral outputs for different numbers of samplings.\n");

	// Difficul singular integral
	vector a = new vector(0, 0, 0);
        vector b = new vector(PI, PI, PI);
        int N = (int)1E7;
        var (result, error) = montecarlo.plainMC(x => 1 / (PI * PI * PI) * 1 / (1 - Cos(x[0]) * Cos(x[1]) * Cos(x[2])), a, b, N);
	WriteLine("Calculating the difficult singular integral from the homework description yields\n" +
		$"Result:    {result:F5} +- {error:F5}\n" +
		"Should be: 1,3932039\n");

	///////////////////// PART B ////////////////////
	WriteLine(border);
	WriteLine("PART B: Quasi-random sequences");
	WriteLine(border);

	WriteLine("A plot comparing the estimated errors of the plainMC to that of the quasiMC\n" +
		"can be found in UnitCircle.svg.\n");


} // Main

// Unit Circle function
public static Func<vector, double> UnitCircle = v => v.norm() < 1 ? 1 : 0;

// Data generation for plots
public static void gendata(){

	// Generating data for plots
	vector a = new vector(-1 , -1);
	vector b = new vector(1 , 1);
	int N = (int)1E5;

	using (StreamWriter output = new StreamWriter("UnitCircle.txt")){
	for (int i = 10 ; i < N ; i += i/10){
		var plainMCdata = montecarlo.plainMC(UnitCircle, a, b, i);
		var quasiMCdata = montecarlo.quasiMC(UnitCircle, a, b, i);
		var IC = CultureInfo.InvariantCulture;
		// #iterations - area - estimated error - real error
		output.WriteLine($"{i} {plainMCdata.Item1.ToString(IC)} {plainMCdata.Item2.ToString(IC)} {Abs(plainMCdata.Item1 - PI).ToString(IC)} " +
					$"{quasiMCdata.Item1.ToString(IC)} {quasiMCdata.Item2.ToString(IC)} {Abs(quasiMCdata.Item1 - PI).ToString(IC)}");
	}
	} // streamwriter
}

} // main
