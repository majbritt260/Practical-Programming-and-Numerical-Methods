using System;
using System.IO;
using static System.Console;
using static cmath;
using static System.Math;
using System.Globalization;

public static class main{
public static void Main(string[] argn){

	int N = 256; // desired amount of data points
	complex[] ts, xs, nxs; // time, signal, noisy signal

	// For timing purposes
	if (argn.Length > 0){
		N = int.Parse(argn[0]);
		xs = new complex[N];
		for (int i=0 ; i < N ; i++) xs[i] = I;
		fourierlib.FFT(xs);
		return;
	}

	double noise = 3; // size of noise
	double afilter = 0.3; // amplitude filter
	double ffilter = 0.02; // frequency filter

	ts  = new complex[N];
	xs  = new complex[N];
	nxs = new complex[N];


	var rnd = new System.Random(2);

	// Generate a sinusoidal signal with added noise
	for (int i = 0 ; i < N ; i++){
		ts[i] = (double)i / N;
		double omega = 2 * PI;
		xs[i] = cos(3 * omega * ts[i] - 4) + 3 * sin(omega * ts[i]);
		nxs[i] = xs[i] + noise * (rnd.NextDouble() - 0.5);

        }


	// Perform FFT to analyze frequency components
	var cs = fourierlib.FFT(nxs);

	// Amplitude filtering
	double cmax = 0;
	for (int i = 0; i < N; i++) cmax = Max(cmax, abs(cs[i]));
	var cas = new complex[N];
	for (int i = 0; i < N; i++){
		if (abs(cs[i]) < cmax * afilter) cas[i] = 0;
		else cas[i] = cs[i];
	}

	// Frequency filtering
	var cfs = new complex[N];
	for (int i = 0; i < N; i++) {
		if (i > N * ffilter && i < N - N * ffilter) cfs[i] = 0;
		else cfs[i] = cs[i];
}

	// Perform inverse FFT to obtain filtered signal
	var ays = fourierlib.IFFT(cas);
	var fys = fourierlib.IFFT(cfs);

	// Save data points in txt file for plotting
	using (StreamWriter output = new StreamWriter("NoiseFiltering.txt")){
	var IC = CultureInfo.InvariantCulture;
	for (int i = 0 ; i < N ; i++) output.WriteLine($"{ts[i].Re.ToString(IC)} {xs[i].Re.ToString(IC)} {nxs[i].Re.ToString(IC)} "+
							$"{ays[i].Re.ToString(IC)} {fys[i].Re.ToString(IC)}");
	} // streamwriter


} // Main
} // main
