using System;
using static System.Console;
using static System.Math;
using System.Globalization;
using System.IO;

public static class main{
public static void Main(string[] args){

	// Borders for neat output file
	string border = new string('-', 85);

	////////// PART A //////////
	WriteLine(border);
	WriteLine($"PART A) Jacobi diagonalization with cyclic sweeps");
	WriteLine(border);

	// Generate random symmetric matrix A
	var rnd = new Random();
	int n = 6;
	matrix A = new matrix(n , n);
        matrix V = new matrix(n , n);
        for(int i = 0; i < n; i++){
		V[i,i] = 1;
		for (int j = i ; j < n ; j++){
			int val = rnd.Next(0,10);
			A[i,j] = val;
			A[j,i] = val;
		}
	}

	matrix A2 = A.copy();

	A.print("Random symmetric matrix A");
	WriteLine(border);

	jacobi.cyclic(A , V);
	V.print("Orthogonal matrix V of eigenvectors");
	WriteLine(border);

	A.print("Diagonal matrix D with the corresponding eigenvalues");
	WriteLine(border);

	WriteLine("VDV^T is equal to the initial matrix A");
	matrix VT = V.transpose();
	matrix VDVT = V * A * VT;
	VDVT.print();
	WriteLine(border);

	WriteLine("V^(T)AV is equal to the matrix D");
	matrix VTAV = VT * A2 * V;
	VTAV.print();
	WriteLine(border);

	WriteLine("V^(T)V is the identity matrix");
	matrix VTV = VT * V;
	VTV.print();
	WriteLine(border);


	WriteLine("VV^(T) is the identity matrix");
	matrix VVT = V * VT;
	VVT.print();
	WriteLine("\n");


        ////////// PART B //////////
        WriteLine(border);
        WriteLine($"PART B) Hydrogen atom, s-wave radial Schrödinger equation on a grid");
        WriteLine(border);

	WriteLine("A plot of the three lowest eigenfunctions and a comparison with the analytical results can be found in eigenfunctions.svg.");
	WriteLine("An investigation of the convergence of the energies with respect to dr and rmax is vizualised in convergence.svg.");

	// Default values
	double rmax = 30;
	double dr = 0.1;

	// Get rmax and dr from the command line
	foreach(string arg in args){
		var words = arg.Split(':');
		if(words[0] == "-rmax") rmax = double.Parse(words[1]);
		if(words[0] == "-dr")     dr = double.Parse(words[1]);
	}

	// Generate data for convergence
	GenerateConvergenceData(rmax, dr);

	// Generate data for eigenfunctions
	GenerateEigenfunctionData(rmax, dr);

} // Main

private static void GenerateConvergenceData(double rmax, double dr){
	var IC = CultureInfo.InvariantCulture;
	int data_length = 20;

	using (var output = new StreamWriter("convergence_dr.txt")){
		double[] dr_values = new double[data_length];
		for (int j = 0 ; j < data_length ; j++) dr_values[j] = (double)(j + 1) / data_length;
		foreach (double dr_value in dr_values){
			int npoints = (int)(rmax / dr_value) - 1;
			vector r = new vector(npoints);
			for (int i = 0; i < npoints; i++) r[i] = dr_value * (i + 1);
			matrix H = BuildHamiltonianMatrix(r, dr_value);
			matrix V = new matrix(npoints, npoints);
			jacobi.cyclic(H, V);
			double lowestEigenvalue = H[0, 0];
			output.WriteLine($"{dr_value.ToString(IC)} {lowestEigenvalue.ToString(IC)}");
		}
	}

	using (var output = new StreamWriter("convergence_rmax.txt")){
//		double[] rmax_values = { 10, 15, 20, 25, 30 };

		double[] rmax_values = new double[data_length];
		for (int j = 0 ; j < data_length ; j++) rmax_values[j] = (double)(j + 2);


		foreach (double rmax_value in rmax_values){
			int npoints = (int)(rmax_value / dr) - 1;
			vector r = new vector(npoints);
			for (int i = 0; i < npoints; i++) r[i] = dr * (i + 1);
			matrix H = BuildHamiltonianMatrix(r, dr);
			matrix V = new matrix(npoints, npoints);
			jacobi.cyclic(H, V);
			double lowestEigenvalue = H[0, 0];
			output.WriteLine($"{rmax_value.ToString(IC)} {lowestEigenvalue.ToString(IC)}");
		}
	}
} // GenerateConvergenceData

private static void GenerateEigenfunctionData(double rmax, double dr){
	var IC = CultureInfo.InvariantCulture;
	int npoints = (int)(rmax / dr) - 1;
	vector r = new vector(npoints);
	for (int i = 0; i < npoints; i++) r[i] = dr * (i + 1);

	matrix H = BuildHamiltonianMatrix(r, dr);
	matrix V = new matrix(npoints, npoints);
	for(int i = 0; i < npoints; i++) V[i,i] = 1;
	jacobi.cyclic(H, V);

	// Save the lowest few eigenfunctions
	using (var output = new StreamWriter("eigenfunctions.txt")){
		int numEigenfunctions = 3;
		for (int i = 0; i < npoints; i++){
			output.Write($"{r[i].ToString(IC)} ");
			for (int k = 0; k < numEigenfunctions; k++){
				double normalizationConstant = 1.0 / Math.Sqrt(dr);
				double f_k = normalizationConstant * V[i, k];
				output.Write($"{f_k.ToString(IC)} ");
			}
			output.WriteLine();
		}
	}

	using (var output = new StreamWriter("analytical_eigenfunctions.txt")){
		for (int i = 0; i < npoints ; i++){
			double r_i = r[i];
			double f1 = r_i * dr * (i + 1) * 2 * Pow(r_i , -3/2) * Exp(-dr * (i + 1)); // Ground state
			double f2 = r_i * dr * (i + 1) * Pow(r_i , -3/2) * (1 - dr * (i + 1) / 2) * Exp(-dr * (i + 1) / 2) / Sqrt(2); // First excited state
			double f3 = r_i * dr * (i + 1) * 2 *Pow(r_i , -3/2) * (1 - 2* dr * (i + 1) / 3 + 2 * Pow(dr * (i + 1) , 2) / 27) * Exp(-dr * (i + 1) / 3) / Sqrt(3) / 3; // Second excited state
			output.WriteLine($"{r_i.ToString(IC)} {f1.ToString(IC)} {f2.ToString(IC)} {f3.ToString(IC)}");
		}
	}
} // GenerateEigenfunctionData

private static matrix BuildHamiltonianMatrix(vector r, double dr){
	int npoints = r.size;
	matrix H = new matrix(npoints, npoints);
	double invDr2 = 1 / (dr * dr);
	double coeff = -0.5 * invDr2;

	for (int i = 0; i < npoints - 1; i++){
		H[i, i] = -2 * coeff;
		H[i, i + 1] = coeff;
		H[i + 1, i] = coeff;
	}

	H[npoints - 1, npoints - 1] = -2 * coeff;
	for (int i = 0; i < npoints; i++) H[i, i] += -1 / r[i];

	return H;

} // BuildHamiltonianMatrix

} // main
