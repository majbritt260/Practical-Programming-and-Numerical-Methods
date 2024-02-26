// After doing this exercise I recognize that there definetely is a more efficient way to this...

using static System.Console;
using static cmath;
using static System.Math;

public static class main{

// Approximation function
public static bool approx(complex a, complex b, double acc = 1e-9, double eps = 1e-9){
	if (abs(b - a) <= acc) return true;
	if (abs(b - a) <= Max(abs(a), abs(b) * eps)) return true;
        return false;
}

// Generate error message
public static string GenerateErrorMessage(complex a, complex b){
	return $"Something went wrong with the calculation. {a} != {b}";
}

// Main function
public static void Main(){
	complex i =  complex.I;
	complex c1 = complex.One;

	// sqrt(-1)
	{
	complex a = sqrt(-c1);
	complex b = +- i;
	if (approx(a, b)) WriteLine($"sqrt(-1) = {a}");
	else WriteLine(GenerateErrorMessage(a, b));
	}

        // sqrt(i)
        {
        complex a = sqrt(-i);
        complex b = 1/sqrt(2) +- i/sqrt(2);
        if (approx(a, b)) WriteLine($"sqrt(i) = {a}");
        else WriteLine(GenerateErrorMessage(a, b));
        }

	// exp(i)
        {
	complex a = exp(i);
	complex b = cos(i) + i * sin(i);
	if (approx(a, b)) WriteLine($"exp(i) = {a}");
	else WriteLine(GenerateErrorMessage(a, b));
	}

	// exp(i*pi)
	{
	complex a = exp(i*PI);
	complex b = - c1;
	if (approx(a, b)) WriteLine($"exp(i*pi) = {a}");
	else WriteLine(GenerateErrorMessage(a, b));
	}

	// i^i
	{
	complex a = cmath.pow(i, i);
	complex b = 0.208;
	if (approx(a, b)) WriteLine($"i^i = {a}");
	else WriteLine(GenerateErrorMessage(a, b));
	}

	// ln(i)
	{
	complex a = log(i);
	complex b = i*PI/2;
	if (approx(a, b)) WriteLine($"ln(i) = {a}");
	else WriteLine(GenerateErrorMessage(a, b));
	}

	// sin(i*pi)
	{
	complex a = sin(i*PI);
	complex b = (exp(-PI)-exp(PI))/(2*i);
	if (approx(a, b)) WriteLine($"sin(i*pi) = {a}");
	else WriteLine(GenerateErrorMessage(a, b));
	}
}
}
