using System;
using static System.Math;

//////////////// B ////////////////
// QUADRATIC  SPLINE

public class qspline{
public vector x , y , b , c , dx , dy , p;

public qspline(vector xs,vector ys){
	this.x = xs.copy();
	this.y = ys.copy();

	if (x.size != y.size) throw new ArgumentException("x- and y-data not of same length");
	if (x.size < 2) throw new ArgumentException("Not enough data for interpolation");

	int len = x.size - 1;

	this.b = new vector(len);
	this.c = new vector(len);

	// c parameter will be calculated according to the book => average over forwards and backwards recursion
	c[0] = 0;

	// step 1: calculation of dx, dy and p
	this.dx = new vector(len);
	this.dy = new vector(len);
	this.p  = new vector(len);

	for (int i = 0 ; i < len ; i++){
		dx[i] = x[i + 1] - x[i];
		dy[i] = y[i + 1] - y[i];
		p[i]  = dy[i] / dx[i];
	}

	// step 2: forwards recursion
	for (int i = 0 ; i < len - 1 ; i++){
		c[i + 1] = (p[i + 1] - p[i] - c[i] * dx[i]) / dx[i + 1];
	}

	c[len - 1] /= 2; // to get average

	// step 3: backwards recursion
	for (int i = len - 2 ; i >= 0 ; i--){
		c[i] = (p[i + 1] - p[i] - c[i + 1] * dx[i + 1]) / dx[i];
	}

	// b parameter
	for (int i = 0 ; i < len ; i++){
		b[i] = p[i] - c[i] * dx[i];
	}
}

public double evaluate(double z){
	int i = binsearch(x , z);
	return y[i] + b[i] * (z - x[i]) + c[i] * Pow(z - x[i] , 2);
}

public double integral(double z){
	int i = binsearch(x , z);
	double sum = 0;

	for (int j = 0 ; j <= i ; j++){
		if (j != i) {sum += y[j] * dx[j] + b[j] * dx[j] * dx[j] / 2 + c[j] * Pow(dx[j] , 3) / 3;}
		else {sum += y[i] * (z - x[i]) + b[i] * (z - x[i]) * (z - x[i]) / 2 + c[i] * Pow((z - x[i]) , 3) / 3;}
        }
        return sum;
}

public double derivative(double z){
	int i = binsearch(x , z);
	return b[i] + 2 * c[i] * (z - x[i]);
}

// Binary search
public static int binsearch(vector x, double z){
	if (z < x[0] || z > x[x.size - 1]) throw new Exception ("binsearch: z not in interval of x");
	int i = 0, j = x.size - 1;
	while (j - i > 1){
		int mid = (i + j) / 2;
		if (z > x[mid]) i = mid; else j = mid;
		}
	return i;
}

}
