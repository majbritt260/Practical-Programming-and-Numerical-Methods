using System;
using static System.Math;

public static class roots{

	// Newton's method with simple backtracking line-search algorithm
	public static vector newton(
		Func<vector,vector> f // the function to find the root of
		,vector start         // the start point
		,double acc = 1e-2    // accuracy goal: on exit ‖f(x)‖ should be <acc
		,vector δx = null     // optional δx-vector for calculation of jacobian
		){
	vector x = start.copy();
	vector fx = f(x), z, fz;
	do { // Newton's iterations
		if( fx.norm() < acc) break; // job done
		matrix J = jacobian(f, x, fx, δx);
		(matrix Q, matrix R) = QRGS.decomp(J);
		vector Dx = QRGS.solve(Q, R, -fx); // Newton's step
		double λ = 1;
		do{ // linesearch
			double λmin = 1/32;
			z = x + λ * Dx;
			fz = f(z);
			if( fz.norm() < (1 - λ / 2) * fx.norm() ) break;
			if( λ < λmin ) break;
			λ /= 2;
			} while(true);
		x = z; fx = fz;
		} while(true);
	return x;
	}

	// Numerical estimation of Jacobian
	public static matrix jacobian(
		Func<vector,vector> f,vector x, vector fx = null, vector dx = null){
		if(dx == null) dx = x.map(xi => Abs(xi) * Pow(2,-26));
		if(fx == null) fx = f(x);
		matrix J = new matrix(x.size);
		for(int j = 0 ; j < x.size ; j++){
			x[j] += dx[j];
			vector df = f(x) - fx;
			for(int i= 0 ; i < x.size;i++) J[i,j] = df[i] / dx[j];
			x[j] -= dx[j];
			}
		return J;
}
} // roots
