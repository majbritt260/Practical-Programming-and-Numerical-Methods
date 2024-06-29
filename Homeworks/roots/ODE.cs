using System;
using static System.Math;
using static System.Console;

public static class odesolver{

// Generic list
public class genlist<T>{
	public T[] data;
	public int size => data.Length;
	public T this[int i] => data[i];     // Indexer
	public genlist(){ data = new T[0]; } // Constructer
	public void add(T item){
		T[] newdata = new T[size + 1];
		System.Array.Copy(data, newdata, size);
		newdata[size] = item;
		data = newdata;
	}
}


// Steppers
public static (vector , vector) rkstep12(            // embedded method of order 1 and 2 (midpoint euler method)
	Func<double , vector , vector> f ,           // the f from dy/dx=f(x,y)
	double x ,                                   // the current value of the variable
	vector y ,                                   // the current value y(x) of the sought function
	double h                                     // the step to be taken
	)
	{
	vector k0 = f(x , y);                        // embedded lower order formula (Euler)
	vector k1 = f(x + h / 2 , y + k0 * (h / 2)); // higher order formula (midpoint)
	vector yh = y + k1 * h;                      // y(x + h) estimate
	vector dy = (k1 - k0) * h;                   // error estimate
	return (yh , dy);
	}

// Driver
public static (genlist<double> , genlist<vector>) driver(
	Func<double , vector , vector> F, // the f from dy / dx = f(x,y)
	(double , double) interval,        // (start-point,end-point)
	vector ystart,                     // y(start-point) */
	double h = 0.01,                  // initial step-size */
	double acc = 0.01,                 // absolute accuracy goal */
	double eps = 0.01                  // relative accuracy goal */
	)
	{
	var (a , b) = interval;
	double x = a;
	vector y = ystart.copy();

	var xlist = new genlist<double>(); xlist.add(x);
	var ylist = new genlist<vector>(); ylist.add(y);

	do{
		if(x >= b) return (xlist , ylist);       // job done
		if(x + h > b) h = b - x;                 // last step should end at b
		var (yh , dy) = rkstep12(F , x , y , h); //choose stepper here
		double tol = (acc + eps * yh.norm()) * Sqrt(h / (b - a));
		double err = dy.norm();
		if(err <= tol){ // accept step
			x += h ; y = yh;
			xlist.add(x);
			ylist.add(y);
		}
		h *= Min(Pow(tol / err , 0.25) * 0.95 , 2);    // readjust stepsize
		} while(true);
	}

// Interpolaton of integrator
public static Func<double , vector> make_linear_interpolant(genlist<double> x , genlist<vector> y){
	Func<double,vector> interpolant = delegate(double z){
		int i = binsearch(x , z);
		double dx = x[i + 1] - x[i];
		vector dy = y[i + 1]-y[i];
		return y[i] + dy / dx * (z - x[i]);
	};
	return interpolant;
}
public static (genlist<double> , genlist<vector> , Func<double , vector>) make_ode_ivp_interpolant
	(Func<double , vector , vector> f,
	(double,double) interval,
	vector y,
	double acc = 0.01,
	double eps = 0.01,
	double h = 0.01
	)
	{
	var (xlist , ylist) = driver(f, interval, y, acc, eps, h);
	return (xlist, ylist, make_linear_interpolant(xlist , ylist));
	}

// Binary search
public static int binsearch(genlist<double> x, double z){
	if (z < x[0] || z > x[x.size - 1]) throw new Exception ("binsearch: z not in interval of x");
	int i = 0, j = x.size - 1;
	while (j - i > 1){
		int mid = (i + j) / 2;
		if (z > x[mid]) i = mid; else j = mid;
		}
	return i;
}

}
