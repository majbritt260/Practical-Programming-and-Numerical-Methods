using static System.Math;
using static System.Console;

class main{
public static void Main(){

	// ERF
	for (double a = -3 ; a <= 3 ; a += 1.0/8){
	WriteLine($"{a} {sfuns.erf(a)}");
	}

WriteLine(); WriteLine();

	// GAMMA + LNGAMMA CALCULATED VALUES
	for (double b = 0 ; b <= 10 ; b += 1.0/8){
	WriteLine($"{b} {sfuns.gamma(b + 1)} {sfuns.lngamma(b + 1)}");
	}

WriteLine(); WriteLine();

	// GAMMA + LNGAMMA FACTORIALS
	for (int c = 0 ; c <= 10 ; c++){
	WriteLine($"{c} {sfuns.fact(c)} {System.Math.Log(sfuns.fact(c))}");
	}
}
}
