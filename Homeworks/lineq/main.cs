using System;
using static System.Console;


public static class main{
public static void Main(){

	// Borders for neat output file
	string border = new string('-', 75);

	// Random numbers for matrix generation
	var rdm = new Random();

	// Decide size of matrix here (n >=  m)
	int n = rdm.Next(6 , 10);
	int m = rdm.Next(4 , n - 1);

	// Generate tall matrix A
	matrix A = new matrix(n , m);
	for (int i = 0 ; i < n ; i++){
		for (int j = 0 ; j < m ; j++){
			A[i,j] += rdm.Next(0 , 10);
		}
	}

	// Generate square matrix B and vector b
	matrix B = new matrix(n , n);
	vector b = new vector(n);
	for (int i = 0 ; i < n ; i++){
		b[i] += rdm.Next(0 , 10);
		for (int j = 0 ; j < n ; j++){
			B[i,j] += rdm.Next(0 , 10);
		}
	}


	// Testing decomp function
	WriteLine(border);
	WriteLine($"Test of decomp function with matrix of size ({n},{m})");
	WriteLine(border);

	WriteLine("The matrix A is:");
	A.print();
	WriteLine(border);

	(matrix Q, matrix R) = QRGS.decomp(A);
	WriteLine("After QR decomposition the Q matrix is:");
	Q.print();
	WriteLine(border);

	WriteLine("After QR decomposition the R matrix is upper triangular:");
	R.print();
	WriteLine(border);

	WriteLine("Checking that Q^TQ yields the identity matrix:");
	matrix QTQ = Q.transpose() * Q;
	if (QTQ.approx(matrix.id(m))) {QTQ.print();}
	WriteLine(border);

	WriteLine("Checking that QR = A:");
	matrix QR = Q * R;
	if (QR.approx(A)) {QR.print();}


	// Testing solve function
	WriteLine(); WriteLine();
	WriteLine(border);
	WriteLine($"Test of solve function with matrix of size ({n},{n})");
	WriteLine(border);

	WriteLine("The square matrix B is:");
	B.print();
	WriteLine(border);

	WriteLine("The vector b is:");
	b.print();
	WriteLine(border);

	(matrix QB, matrix RB) = QRGS.decomp(B);
	WriteLine("B is factorized into Q and R:");
	QB.print();
	RB.print();
	WriteLine(border);

	vector x = QRGS.solve(QB, RB, b);
	WriteLine("The solution x to the equation Bx = b is:");
	x.print();
	WriteLine(border);

	WriteLine("Checking that Bx = b");
	vector Bx = B * x;
	if (Bx.approx(b)) {Bx.print();}



	// Testing determinant function
	WriteLine(border);
	WriteLine($"The determinant of the upper triangular matrix R (found via QR-decomposition\n of the square matrix B) is det(R) = {QRGS.det(RB)}");

	// Testing inverse function
	WriteLine(); WriteLine();
	WriteLine(border);
	WriteLine($"Test of inverse function with matrix of size ({n},{n})");
	WriteLine(border);

	matrix B_inv = QRGS.inverse(QB, RB);
	WriteLine("The inverse of matrix B is");
	B_inv.print();
	WriteLine(border);

	matrix BB_inv = B * B_inv;
	WriteLine("Checking that B^(-1)B is the identity matrix");
	if ((BB_inv).approx(matrix.id(n))) {BB_inv.print();}
}
}
