using System;
using static System.Math;

//////////////// A ////////////////
// LINEAR SPLINE

public class linspline{

// Linear spline
public static double lspline(double[] x, double[] y, double z){
	int i = binsearch(x, z);
	double dx = x[i + 1] - x[i]; if (dx <= 0) throw new Exception ("lspline: dx must be larger than 0");
	double dy = y[i + 1] - y[i];
	return y[i] + dy / dx * (z - x[i]);
}

// Binary search (find index of list x where x[i] < z < x[i + 1])
public static int binsearch(double[] x, double z){
	if (z < x[0] || z > x[x.Length - 1]) throw new Exception ("binsearch: z not in interval of x");
	int i = 0, j = x.Length - 1;
	while (j - i > 1){
		int mid = (i + j) / 2;
		if (z > x[mid]) i = mid; else j = mid;
		}
	return i;
}

// Integral of lspline
public static double integral(double[] x, double[] y, double z){
	double sum = 0;
	int i = binsearch(x, z);

	for (int j = 0 ; j <= i ; j++){
		double dx = x[j + 1] - x[j]; if (dx <= 0) throw new Exception ("lspline integral: dx must be larger than 0");
		double dy = y[j + 1] - y[j];
		double p = dy / dx;
		if (j != i) {sum += y[j] * dx + p * Pow(dx , 2) / 2;}
		else {sum += y[i] * (z - x[i]) + p * Pow((z - x[i]) , 2) / 2;}
	}
	return sum;
}
}
