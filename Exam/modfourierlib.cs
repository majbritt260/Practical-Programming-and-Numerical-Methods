using System;
using static System.Math;
using static cmath;

public static class modfourierlib{
	public static void DFTS(int sign, int N, complex[] x, int ix, int step){
		if (N <= 0 || x == null || ix < 0 || ix >= x.Length){
			throw new ArgumentException("DFST: Invalid parameters");
		}

		complex[] temp = new complex[N];

		for (int i = 0; i < N; i++){
			temp[i] = 0;
			for (int j = 0; j < N; j++){
				temp[i] += x[ix + j * step] * exp(sign * 2 * PI * I * j * i / N);
			}
		}

		for (int i = 0; i < N; i++){
			x[ix + i * step] = temp[i];
		}
	}

	public static void FFTS(int sign, int N, complex[] x, int ix, int step){
		if (N <= 0 || x == null || ix < 0 || ix >= x.Length){
			throw new ArgumentException("FFTS: Invalid parameters");
		}

		if (N == 1) return;
		else if (N % 2 == 0){
			FFTS(sign, N / 2, x, ix, 2 * step);
			FFTS(sign, N / 2, x, ix + step, 2 * step);
			for (int i = 0; i < N / 2; i++){
				complex even = x[ix + i * step];
				complex odd = exp(sign * 2 * PI * I * i / N) * x[ix + (i + N / 2) * step];
				x[ix + i * step] = even + odd;
				x[ix + (i + N / 2) * step] = even - odd;
			}
		}
		else DFTS(sign, N, x, ix, step);
	}


	public static complex[] FFT(complex[] x){
		if (x == null || x.Length == 0){
			throw new ArgumentException("FFT: Input array x must not be null or empty.");
		}

		int N = x.Length;
		FFTS(+1, N, x, 0, 1);
		return x;
	}

	public static complex[] IFFT(complex[] x){
		if (x == null || x.Length == 0){
			throw new ArgumentException("IFFT: Input array x must not be null or empty.");
		}

		int N = x.Length;
		FFTS(-1, N, x, 0, 1);
		for (int i = 0; i < N; i++) x[i] /= N;
		return x;
	}


} // fourierlib
