using System;

public static class OLSF{
public static (vector , matrix) lsfit(Func<double , double>[] fs , vector x , vector y , vector dy){
	// construct matrix A and vector b as given by eq. 3.14 in the book
	int n = x.size;
	int m = fs.Length;

	matrix A = new matrix(n , m);
	vector b = new vector(n);

	for (int i = 0 ; i < n ; i++){
		b[i] = y[i] / dy[i];
		for (int k = 0 ; k < m ; k++){
			A[i , k] = fs[k](x[i]) / dy[i];
		}
	}

	// deconstruct A into Q and R
	(matrix Q , matrix R) = QRGS.decomp(A);

	// find fitting coefficients
	vector c = QRGS.solve(Q , R , b);

	// find covariance matrix
	matrix AC = A.transpose() * A;
	(matrix QC , matrix RC) = QRGS.decomp(AC);
	var S = QRGS.inverse(QC , RC);

	return (c , S);
}
}
