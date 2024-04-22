using System;
using System.Globalization;
using System.IO;
using static System.Console;
using static System.Math;

public static class main{
public static void Main(){
	// Borders for neat output file
	string border = new string('-', 75);

	///////////////////// PART A ////////////////////
	WriteLine(border);
	WriteLine("PART A: Linear splines");
	WriteLine(border);
	WriteLine("An indicative plot of linear splines and their integrals can be seen in lspline.svg");
	WriteLine("The desired number of splines between each data point can be modified in lspline.cs\n");


	// Generation of testdata for the linear spline methods (to be used for plot)
	int len = 10; // desired length of generated data
	double  n = 5; // desired number of splines between each data point
	File.Delete("lspline.txt"); // delete file holding generated data for reruns

	double[] xs = new double[len];
	double[] ys = new double[len];

	// Data to be interpolated
	for (int i = 0 ; i < len ; i++){
		double x = i;
		double y = Cos(x); // test-function

		xs[i] = x;
		ys[i] = y;

		File.AppendAllText("lspline.txt" , $"{x} {y.ToString(CultureInfo.InvariantCulture)}\n");
	}

	// Seperation in txt (indexing for gnuoplot)
	File.AppendAllText("lspline.txt" , "\n\n");

	// Interpolated data and its integral using linear splines
	for (int i = 0 ; i < len - 1; i++){
		for (double j = xs[i] + 1/(n+1) ; j < xs[i + 1] - 1/(n+2) ; j += 1/(n+1)){ // z = x_i not allowed (=> no overlap in plot)
//		for (double j = xs[i] ; j < xs[i + 1]  ; j += 1/n){ // z = x_i allowed
			double z = j; // subtract any number between 0 and 1
			double s = linspline.lspline(xs, ys, z);
			double si = linspline.integral(xs, ys, z);

			File.AppendAllText("lspline.txt" , $"{z.ToString(CultureInfo.InvariantCulture)} {s.ToString(CultureInfo.InvariantCulture)} {si.ToString(CultureInfo.InvariantCulture)}\n");
		}
	}


	///////////////////// PART B ////////////////////
        WriteLine(border);
        WriteLine("PART B: Quadratic splines");
        WriteLine(border);
        WriteLine("An indicative plot of quadratic splines their integrals and their derivatives");
	WriteLine("can be seen in qspline.svg");

	vector vxs = new vector(len); // x values
	vector vys = new vector(len); // y values
	vector vss = new vector(len - 1); // splines
	vector vds = new vector(len - 1); // derivatives
	vector vis = new vector(len - 1); // integrals

	File.Delete("qspline.txt");

	for (int i = 0 ; i < len ; i++){
		vxs[i] = i;
		vys[i] = Sin(vxs[i]);

		File.AppendAllText("qspline.txt" , $"{vxs[i].ToString(CultureInfo.InvariantCulture)} {vys[i].ToString(CultureInfo.InvariantCulture)}\n");
	}

	File.AppendAllText("qspline.txt" , "\n\n");

	qspline spline = new qspline(vxs , vys);

	// evaluated splines, integrals and derivatives
	for (int i = 0 ; i < len - 1 ; i++){
		for (double j = vxs[i] ; j < vxs[i + 1] ; j += 1/n){
			double z = j;
			vss[i] = spline.evaluate(z);
			vds[i] = spline.derivative(z);
			vis[i] = spline.integral(z);

			File.AppendAllText("qspline.txt" , $"{z.ToString(CultureInfo.InvariantCulture)} {vss[i].ToString(CultureInfo.InvariantCulture)} {vds[i].ToString(CultureInfo.InvariantCulture)} {vis[i].ToString(CultureInfo.InvariantCulture)}\n");
		}
	}

	WriteLine("For debugging purposes I considered the 3 different tables given in the");
	WriteLine("homework description.");
	WriteLine("The manually calculated b and c values are as follows:\n");
        WriteLine($"for y = 1:        b_i = 0      c_i = 0");
        WriteLine($"for y = x:        b_i = 1      c_i = 0");
        WriteLine($"for y = x^2:      b_i = 2*x_i  c_i = 1\n");
	WriteLine("Using the same tables in my quadratic-spline routine yields the");
	WriteLine("values seen below, which agrees with the manually calculated values.");

	string[] ystrings = new string[]{"for y = 1" , "for y = x" , "for y = x^2"};

	for (int i = 0 ; i < ystrings.Length ; i++){

		WriteLine(border);
		WriteLine($"{ystrings[i]}");

		vector xsdebug = new vector(5);
		vector ysdebug = new vector(5);

		for (int j = 0 ; j < 5 ; j++){
			xsdebug[j] = j;
			ysdebug[j] = Pow(j , i);
		}

		qspline splinedebug = new qspline(xsdebug , ysdebug);
		Write("b_i:");
		splinedebug.b.print();

		Write("c_i:");
		splinedebug.c.print();
	}
}
}

