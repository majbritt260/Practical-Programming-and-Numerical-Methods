using static System.Math;
using static System.Console;
using System;

public static class sfuns{

// Single precision error function (Abramowitz and Stegun, from Wikipedia)
public static double erf(double x){
if (x < 0) return -erf(-x);

double[] a = {0.254829592, -0.284496736, 1.421413741, -1.453152027, 1.061405429};
double t = 1 / (1 + 0.3275911 * x);
double sum = t * (a[0] + t * (a[1] + t * (a[2] + t * (a[3] + t * a[4]))));
return 1 - sum * Exp(-x * x);
}


// Calculate factorials
public static int fact(int x){

if (x < 0) throw new ArgumentException("Factorials not defined for negative numbers.");

int result = 1;
for (int i = 2 ; i <= x ; i++) result *= i;
return result;
}

// Single precistion gamma function (Gergo Nemes, from Wikipedia)
public static double gamma(double x){
if (x < 0) return PI/Sin(PI*x)/gamma(1 - x);
if (x < 9) return gamma(x + 1) / x;

double lngamma = x * Log (x + 1 / (12 * x - 1 / x / 10)) - x + Log(2 * PI / x) / 2;
return Exp(lngamma);
}



// Logarithm of gamma function
public static double lngamma(double x){

if (x <= 0) throw new ArgumentException("lngamma: x <= 0.");

if(x < 9) return lngamma(x + 1) - Log(x);
return x * Log(x + 1 / (12 * x - 1/ x / 10)) - x + Log(2 * PI / x) / 2;
}


}
