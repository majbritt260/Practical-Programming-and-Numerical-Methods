public static class QRGS{

//////////////// A ////////////////
// DECOMPOSITION
public static (matrix , matrix) decomp(matrix A){

	int m = A.size2;

	matrix Q = A.copy();
	matrix R = new matrix (m , m);

	for (int i = 0 ; i < m ; i++){
		R[i , i] = Q[i].norm();
		Q[i] /= R[i, i];
		for (int j = i + 1 ; j < m ; j++){
			R[i , j] = Q[i].dot(Q[j]);
			Q[j] -= Q[i]*R[i , j];
		}
	}
	return (Q, R);
}


// SOLVE
public static vector solve(matrix Q, matrix R, vector b){
	b = Q.transpose() * b;
	for (int i = R.size1 - 1 ; i >= 0 ; i--){
		double sum = 0;
		for (int j = i + 1 ; j < R.size2 ; j++){
			sum += R[i , j] * b[j];
		}
	b[i] = (b[i] - sum) / R[i , i];
	}
	return b;
}


// DETERMINANT OF UPPER TRIANGULAR MATRIX (OF SQUARE MATRIX)
public static double det(matrix R){
	double det = R[0,0];
	for (int i = 1 ; i < R.size1 ; i++){
		det *= R[i,i];
	}
	return det;
}

//////////////// B ////////////////
// INVERSION
public static matrix inverse(matrix Q, matrix R){
	matrix inverted = new matrix (Q.size2, Q.size1);
	for (int i = 0 ; i < Q.size2 ; i++){
		vector unit = new vector(Q.size1);
		unit[i] = 1;
		inverted[i] = solve(Q , R , unit);
	}
	return inverted;
}



} // QRGS
