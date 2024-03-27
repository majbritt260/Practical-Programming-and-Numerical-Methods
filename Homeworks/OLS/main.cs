using System.Globalization;
using System;
using static System.Console;
using System.IO;
using static System.Math;

public static class main{
public static void Main(string[] args){

	// Borders for neat output file
	string border = new string('-', 75);

	WriteLine(border);
	WriteLine("PART A, B AND C");
	WriteLine(border);
	//Fitting data to ln(y) = ln(a) - λt, errors δln(y) = δy/y

	// Functions for fitting
	var fs = new Func<double,double>[] { x => 1.0 , x => - x };

	// Make outfile for fitted data
	string infile = null , outfile = null;

	foreach(var arg in args){
		var words = arg.Split(':');
		if (words[0] == "-input") infile = words[1];
		if (words[0] == "-output") outfile=words[1];
		}

	if (infile == null || outfile == null) {
		Error.WriteLine("Wrong filename argument");
		}

	var instream = new System.IO.StreamReader(infile);
	var outstream = new System.IO.StreamWriter(outfile , append : false);

	vector xs  = new vector(infile.Length + 1);
	vector ys  = new vector(infile.Length + 1);
	vector dys = new vector(infile.Length + 1);

	// Generate data to fit
	int s = 0;
	for (string line = instream.ReadLine();
		line != null;
		line = instream.ReadLine()){

		string[] values = line.Split("\t");

		double x  = double.Parse(values[0]);
		double y = double.Parse(values[1]);
		double dy = double.Parse(values[2]);

		xs[s]  = x;
		ys[s]  = Log(y);
		dys[s] = dy/y;

		s += 1;
	}

	instream.Close();

	// Generate fitting constants and covarianse matrix
	(vector popt , matrix pcov) = OLSF.lsfit(fs , xs , ys , dys);

	// Uncertainty on fitting constants
	double da = Sqrt(pcov[0,0]);
	double dlambda = Sqrt(pcov[1,1]);

	WriteLine($"The fitting constants are found to be:");
	WriteLine($"a = {popt[0]} +- {da}");
	WriteLine($"lambda = {popt[1]} +- {dlambda}.\n");
	Write("The covariance matrix is:");
	pcov.print();
	WriteLine(border);

	// Generate and send curve data via outstream
	for (int i = 0 ; i < 1000 ; i++){
		double t = i/1000.0 * xs.max();
		double n = 0;
		double nm = 0;
		double np = 0;

		for (int j = 0 ; j < fs.Length ; j++){
			n += popt[j] * fs[j](t);
		}

		nm += (popt[0] - da) * fs[0](t);
		nm += (popt[1] + dlambda) * fs[1](t);
		np += (popt[0] + da) * fs[0](t);
		np += (popt[1] - dlambda) * fs[1](t);

	outstream.WriteLine($"{t.ToString(CultureInfo.InvariantCulture)} {Exp(n).ToString(CultureInfo.InvariantCulture)} {Exp(nm).ToString(CultureInfo.InvariantCulture)} {Exp(np).ToString(CultureInfo.InvariantCulture)}");
	}

	outstream.Close();

	// Half-time of ThX
	double Th  = Log(2)/popt[1];
	double dTh = Log(2)*dlambda/(popt[1]*popt[1]);
	WriteLine($"The half-time of ThX is found to be {Th} +- {dTh} days.");
	WriteLine("The modern value is 3.6319+-0.0023 days, meaning the value found from "
		+ "the fit differs from the modern value.");

	// Where to find graph
	WriteLine(border);
	WriteLine("Plot of best fit with uncertainties can be found in fit.svg.");
}
}
