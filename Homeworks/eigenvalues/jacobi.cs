using System;
using static System.Math;

public static class jacobi{
    static void timesJ(matrix A, int p, int q, double theta){
	double c = Cos(theta),s = Sin(theta);
	for (int i = 0 ; i < A.size1 ; i++){
		double aip = A[i,p];
		double aiq = A[i,q];
		A[i,p] = c * aip - s * aiq;
		A[i,q] = s * aip + c * aiq;
		}
    }

    static void Jtimes(matrix A, int p, int q, double theta){
	double c = Cos(theta),s = Sin(theta);
	for(int j = 0 ; j < A.size1 ; j++){
		double apj = A[p,j];
		double aqj = A[q,j];
		A[p,j] = c * apj + s * aqj;
		A[q,j] = -s * apj + c * aqj;
		}
    }

	public static void cyclic(matrix A, matrix V){
		int n = A.size1;
		bool changed;
		do{
		changed = false;
		for(int p = 0;p<n-1;p++){
			for(int q = p+1;q<n;q++){
				double apq = A[p,q];
				double app = A[p,p];
				double aqq = A[q,q];
				double theta = 0.5 * Atan2(2 * apq, aqq - app);
				double c = Cos(theta);
				double s = Sin(theta);
				double new_app = c * c * app - 2 * s * c * apq + s * s * aqq;
				double new_aqq = s * s * app + 2 * s * c * apq + c * c * aqq;
				if(new_app != app || new_aqq != aqq){
					changed=true;
					timesJ(A,p,q, theta);
					Jtimes(A,p,q,-theta);
					timesJ(V,p,q, theta);
				}
			}
		}
		}while(changed);
	}
} // jacobi
