using System;
using static System.Math;

public static class min{
	public static vector gradient(Func<vector, double> φ, vector x){
		vector gradφ = new vector(x.size);
		double φx = φ(x);
		for (int i = 0; i < x.size; i++){
			double dx = Max(Abs(x[i]), 1) * Pow(2, -26);
			x[i] += dx;
			gradφ[i] = (φ(x) - φx) / dx;
			x[i] -= dx;
		}
		return gradφ;
	}


	public static matrix hessian(Func<vector, double> φ, vector x){
		matrix H = new matrix(x.size);
		vector gradφx = gradient(φ, x);
		for (int j = 0; j < x.size; j++){
			double dx = Max(Abs(x[j]), 1) * Pow(2, -13);
			x[j] += dx;
			vector dgradφ = gradient(φ, x) - gradφx;
			for (int i = 0; i < x.size; i++) H[i, j] = dgradφ[i] / dx;
			x[j] -= dx;
		}
		return (H + H.T) / 2;
	}


	public static (vector result, int steps) newton(Func<vector, double> φ, vector x, double acc = 1e-3){
		int maxSteps = 1000;
		int steps = 0;
		for (int step = 0; step < maxSteps; step++){
			steps++;
			var gradφ = gradient(φ, x);
			if (gradφ.norm() < acc) break;
			var H = hessian(φ, x);
			(matrix Q, matrix R) = QRGS.decomp(H);
			var dx = QRGS.solve(Q, R, -gradφ);

			double λ = 1, φx = φ(x);
			while (true){
				if (φ(x + λ * dx) < φx) break;
				if (λ < 1e-4) break;
				λ /= 2;
			}
			x += λ * dx;
		}
		return (x , steps);
	}


} // minimization
