using static System.Math;

public static class sfuns{

	// Approximation function
	public static bool approx(double a, double b, double acc = 1e-9, double eps = 1e-9){
	if(Abs(b-a) <= acc) return true;
	if(Abs(b-a) <= Max(Abs(a), Abs(b)*eps)) return true;
	return false;
                 }
}
