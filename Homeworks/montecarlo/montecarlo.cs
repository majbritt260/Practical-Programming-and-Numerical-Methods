using System;
using static System.Math;

public class montecarlo{

/////////// plain Monte Carlo /////////
public static (double , double) plainMC (Func<vector,double> f, vector a, vector b, int N){
	int dim = a.size;
	double V = 1;

	for (int i = 0 ; i < dim ; i++) V *= b[i] - a[i];

	double sum1 = 0;
	double  sum2 = 0;
	var x = new vector(dim);
	var rnd = new Random();

	for(int i = 0 ; i < N ; i++){
		for(int j = 0 ; j < dim ; j++) x[j] = a[j] + rnd.NextDouble() * (b[j] - a[j]);
		double fx = f(x);
		sum1 += fx;
		sum2 += fx * fx;
	}

	double mean1 = sum1 / N;
	double mean2 = sum2 / N;
	double sigma = Sqrt(mean2 - mean1 * mean1);
	var result = (mean1 * V, sigma * V / Sqrt(N));

	return result;
} // plainMC

////////// quasi Monte Carlo //////////
public static (double , double) quasiMC (Func<vector,double> f, vector a, vector b, int N, double[] xs = null, double[] ys = null, int offset = 0){
	int dim = a.size;
	double V = 1;

	for(int i = 0 ; i < dim ; i++) V *= b[i] - a[i];

	double sum1 = 0;
	double sum2 = 0;

	vector x1 = new vector(dim);
	vector x2 = new vector(dim);

	for (int i = 0 ; i < N ; i++){
		for (int j = 0 ; j < dim ; j++){
			x1[j] = a[j] + corput(i , prime(j + offset)) * (b[j] - a[j]);
			x2[j] = a[j] + corput(i , prime(j + offset + 7)) *(b[j] - a[j]);
		}
		if (xs != null) {xs[i] = x1[0]; ys[i] = x1[1];}
		sum1 += f(x1);
		sum2 += f(x2);
	}

	double mean1 = sum1 / N;
	double mean2 = sum2 / N;
	double sigma = Abs(mean1 - mean2);
	return (mean1 * V, sigma * V);
} // quasiMC

static double corput(int n, int b){
	double q = 0, bk = (double)1/b;
	while (n > 0) {q += (n % b) * bk; n /= b; bk /= b;}
	return q;
	} // corput

static void halton(int n, int d, vector x){
	int[] base_ = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61};
	int maxd = base_.Length / sizeof(int);
	if(d > maxd) for (int i = 0 ; i < d ; i++) x[i] = corput(n , base_[i]);
	} // halton

static int prime(int i){
	int[] primes = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61};
	if(i >= primes.Length) return prime(i - primes.Length);
	else return primes[i];
	} // prime

} // monte carlo
