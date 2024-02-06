/* This file contains the code used to answer the questions in the math exercise,
   using the library file "sfuns.cs", which contains the Stirling approximation
   for the gamma-function.
*/

using static System.Math;
using static System.Console;

static class Program{
	static void Main(){
		// Calculate square root of 2
		double sqrt2 = Sqrt(2);
		Write($"The square root of 2 is {sqrt2}\n");

		// Calculate 2^(1/5)
		double pwr = Pow(2,1.0/5);
		Write($"2 raised to the power of 1/5 is {pwr}\n");

		// Calculate e^(Pi)
		double epi = Exp(PI);
		Write($"e raised to the power of Pi is {epi}\n");

		// Calculate Pi^e
		double pie = Pow(PI,E);
		Write($"Pi raised to the power of e is {pie}\n");
	}
}
