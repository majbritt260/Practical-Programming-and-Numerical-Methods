using System;
using static System.Math;
using static System.Console;
using System.IO;
using System.Globalization;

public static class main{
public static void Main(){
	// Borders for neat output file
	string border = new string('-', 75);

	///////////////////// PART A /////////////////////
	WriteLine(border);
	WriteLine("PART A: Recursive adaptive integrator");
	WriteLine(border);

	integrator_test();

        ///////////////////// PART B /////////////////////
        WriteLine(border);
        WriteLine("PART B: Open quadrature with Clenshaw–Curtis variable transformation");
        WriteLine(border);

	clenshawcurtis_test();


	} // Main




public static void integrator_test(){

	double acc = 0.000001; // accuracy goal
	double test1 = integrator.integrate(f1,0,1).Item1;
	double test2 = integrator.integrate(f2,0,1).Item1;
	double test3 = integrator.integrate(f3,0,1).Item1;
	double test4 = integrator.integrate(f4,0,1).Item1;

	// Integrator test
	WriteLine($"The integrator is tested using a selection of integrals with an accuracy goal of {acc}:\n");

	WriteLine($"Integral of Sqrt(x)       from x = 0 to x = 1.\n" +
			$"Expectation: {2f/3:F6}\n" +
			$"Result:      {test1:F6}\n" +
			$"{IsWithinAccuracyGoal(2f/3, test1, acc)}\n");

	WriteLine($"Integral of 1/Sqrt(x)     from x = 0 to x = 1.\n" +
			$"Expectation: {2}\n" +
			$"Result:      {test2:F6}\n" +
			$"{IsWithinAccuracyGoal(2, test2, acc)}\n");

	WriteLine($"Integral of 4*Sqrt(1-x^2) from x = 0 to x = 1.\n" +
			$"Expectation: {PI:F6}\n" +
			$"Result:      {test3:F6}\n" +
			$"{IsWithinAccuracyGoal(PI, test3, acc)}\n");


	WriteLine($"Integral of ln(x)/Sqrt(x) from x = 0 to x = 1.\n" +
			$"Expectation: {-4}\n" +
			$"Result:      {test4:F6}\n" +
			$"{IsWithinAccuracyGoal(-4, test4, acc)}\n");

	// Data generation for error-function plots
	using (var output = new StreamWriter("gendata.erf.txt")){
		for (double x = -3 ; x <= 3 ; x += 1.0/32){
			// Write the data to the output file
			output.WriteLine($"{x.ToString(CultureInfo.InvariantCulture)}" +
					$" {integrator.int_erf(x).ToString(CultureInfo.InvariantCulture)}");}}

	int erfcount = 0 , interfcount = 0;
	using (var output = new StreamWriter("comparison.erf.txt")){
		using (var tabdata = new StreamReader("tabdata.erf.txt")){

		string line;
		while ((line = tabdata.ReadLine()) != null){

			// Split each line into columns based on space
			string[] columns = line.Split("\t");

			if (columns.Length >= 2){

			// Parse the values from the columns
			double x = double.Parse(columns[0], CultureInfo.InvariantCulture);
			double tabval = double.Parse(columns[1], CultureInfo.InvariantCulture);

			// Calculate the errors
			double erf_error = Abs(integrator.erf(x) - tabval);
			double interf_error = Abs(integrator.int_erf(x) - tabval);

			if (erf_error > interf_error){interfcount++;} else erfcount++;
			// Write the results to the output file
			output.WriteLine($"{x.ToString(CultureInfo.InvariantCulture)}" +
					$" {erf_error.ToString(CultureInfo.InvariantCulture)}" +
					$" {interf_error.ToString(CultureInfo.InvariantCulture)}");}}
		}
	}

	WriteLine("A comparison between the error function via its integral representation and the exact values is illustrated in erfcomparison_tabdata.svg.");
	WriteLine("Furthermore a comparison of the accuracy between the erf from the plots exercise and the integral representation was made.\n"+
		$"For del = eps = 1E-06, the erf from the plot excercise had a better approximation {erfcount} times, while the integral\n"+
		$"representation had a better approximation {interfcount} times. This is illustrated in erfcomparison_plotsexercise.svg.\n");

} // integrator_test


public static void clenshawcurtis_test(){
	var test1 = integrator.integrate(f1,0,1);
	var test2 = integrator.integrate(f2,0,1);
	var test3 = integrator.ClenshawCurtis(f1,0,1);
	var test4 = integrator.ClenshawCurtis(f2,0,1);

	WriteLine($"Integral of Sqrt(x)       from x = 0 to x = 1.\n" +
                        $"Expectation:      {2f/3:F6}\n" +
                        $"Ordinary result:  {test1.Item1:F6}\n" +
			$"CC result:        {test3.Item1:F6}\n" +
			$"CC ncalls: {test3.Item2}\nPython ncalls: 231\n");

        WriteLine($"Integral of 1/Sqrt(x)     from x = 0 to x = 1.\n" +
                        $"Expectation:      {2}\n" +
                        $"Ordinary result:  {test2.Item1:F6}\n" +
                        $"CC result:        {test4.Item1:F6}\n" +
                        $"CC ncalls: {test4.Item2}\nPython ncalls: 315\n");
} // clenshawcurtis_test


///////////////// TEST FUNCTIONS //////////////////
public static double f1(double x) {return Sqrt(x);}
public static double f2(double x) {return 1 / Sqrt(x);}
public static double f3(double x) {return 4 * Sqrt(1 - Pow(x, 2));}
public static double f4(double x) {return Log(x) / Sqrt(x);}
public static double f5(double x) {return Exp(- Pow(x, 2));}
public static double f6(double x) {return 1 / Pow(x, 2);}

///////////////// ACCURACY TESTER //////////////////
public static string IsWithinAccuracyGoal(double expectation, double result, double acc){
	return Abs(expectation - result) < acc ? "Within accuracy goal: yes" : "Within accuracy goal: no";}

} // main

