using System;
using static System.Math;
using static cmath;

public static class fourierlib{

// Discrete Fourier Transform (DFT) implementation
public static void DFTS(int sign, int N, complex[] x, int ix, int step, complex[] c, int ic){
	if (N <= 0 || x == null || c == null || ix < 0 || ix >= x.Length || ic < 0 || ic >= c.Length){
		throw new ArgumentException("DFST: Invalid parameters");
}
	for (int i = 0 ; i < N ; i++){
		c[ic + i] = 0;
		for (int j = 0 ; j < N ; j++)
			c[ic + i] += x[ix + j * step] * exp(sign * 2 * PI * I * j * i / N);
	}
} // DFTS

// Fast Fourier Transform (FFT) implementation using Cooley-Tukey algorithm
public static void FFTS(int sign, int N, complex[] x, int ix, int step, complex[] c, int ic){
	if (N <= 0 || x == null || c == null || ix < 0 || ix >= x.Length || ic < 0 || ic >= c.Length){
		throw new ArgumentException("FFTS: Invalid parameters");
}
	if (N == 1) c[ic] = x[ix];
	else if (N % 2 == 0) {
		FFTS(sign, N / 2, x, ix, 2 * step, c, ic);
		FFTS(sign, N / 2, x, ix + step, 2 * step, c, ic + N / 2);
		for (int i = 0; i < N / 2; i++) {
			complex even = c[ic + i];
			complex odd = exp(sign * 2 * PI * I * i / N) * c[ic + i + N / 2];
			c[ic + i] = even + odd;
			c[ic + i + N / 2] = even - odd;
		}
	}
	else DFTS(sign, N, x, ix, step, c, ic);
} // FFTS


// Wrapper: FFT on input array x
public static complex[] FFT(complex[] x){
	if (x == null || x.Length == 0){
		throw new ArgumentException("FFT: Input array x must not be null or empty.");
	}
	int N = x.Length;
	var c = new complex[N];
	FFTS(-1, N, x, 0, 1, c, 0);
	return c;
} // FFT

// Wrapper: Inverse FFT on input array c
public static complex[] IFFT(complex[] c){
        if (c == null || c.Length == 0){
		throw new ArgumentException("IFFT: Input array c must not be null or empty.");
	}
	int N = c.Length;
	var x = new complex[N];
	FFTS(+1, N, c, 0, 1, x, 0);
	for (int i = 0; i < N; i++) x[i] /= N; // Normalize
	return x;
} // IFFT

} // fourierlib


