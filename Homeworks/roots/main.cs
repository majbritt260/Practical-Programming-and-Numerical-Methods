using System;
using static System.Console;
using static System.Math;
using System.IO;
using static odesolver;
using System.Globalization;

public static class main{
public static void Main(){

	// Borders for neat output file
	string border = new string('-', 85);

	// PART A
	WriteLine(border);
	WriteLine($"PART A: Newton's method with numerical Jacobian and back-tracking linesearch");
	WriteLine(border);

	partA();

        // PART B
        WriteLine(border);
        WriteLine($"PART B) Bound states of hydrogen atom with shooting method for boundary value problems");
        WriteLine(border);

	partB();

} // Main


public static void partA(){
	// Debugging function
	Func<vector, vector> f = ys => {
		vector result = new vector(ys.size);
		for (int i = 0 ; i < ys.size ; i++) result[i] = ys[i] * ys[i] - 4;
		return result;
	};
	vector x0 = new vector("1.0,-1.0"); // initial guess
	vector debugroots = roots.newton(f, x0, 1e-3);

        WriteLine($"\n* Debugging function\nf(x) = x² - 4 \n"+
		$"\nCalculated roots\n({debugroots[0]:F4},{debugroots[1]:F4})\n"+
		$"Should be\n(2,-2)");

	// Rosenbrock
	Func<vector, vector> rosenbrock = x => {
            vector df = new vector(x.size);
            double dfdx = -2 * (1 - x[0]) - 200 * (x[1] - Pow(x[0], 2)) * 200;
            double dfdy = 200 * (x[1] - Pow(x[0], 2));
            df[0] = dfdx; df[1] = dfdy;
            return df;
        };
        x0 = new vector("1,1");
        vector rosroots = roots.newton(rosenbrock, x0, 1e-3);
        WriteLine($"\n* Rosenbrock's valley function\nf(x,y) = (1 - x)² + 100(y - x²)²\n"+
		$"\nDerivatives:\n∂f/∂x = -2(1-x)-200x(y-x²)\n∂f/∂y = 200(y-x²)\n"+
		$"\nRoot\n({rosroots[0]:F4},{rosroots[1]:F4})");


	// Himmelblau
        Func<vector, vector> himmelblau = x =>
        {
            vector df = new vector(x.size);
            double dfdx = 4 * (x[0] * x[0] + x[1] - 11) * x[0] + 2 * (x[0] + x[1] * x[1] - 7);
            double dfdy = 2 * (x[0] * x[0] + x[1] - 11) + 4 * (x[0] + x[1] * x[1] - 7) * x[1];
            df[0] = dfdx; df[1] = dfdy;
            return df;
        };

	string[] x0s = { "3,2", "-3,3", "-4,-3", "4,-2" };
	WriteLine($"\n* Himmelblau's function\nf(x, y) = (x² + y - 11)² + (x + y² - 7)²");
	WriteLine($"\nDerivatives\n∂f/∂x = 4x(x² + y - 11) - 2(x + y² - 7)\n∂f/∂y = 2(x² + y - 11) +4y(x + y² - 7)");
	WriteLine("\nRoots");
	for (int i = 0 ; i < 4 ; i++){
		x0 = new vector(x0s[i]);
		vector himmelroots = roots.newton(himmelblau, x0, 1e-3);
		WriteLine($"{i+1}: ({himmelroots[0]:F4},{himmelroots[1]:F4})");
	}
} // partA

public static void partB(){
	// Wavefunction
	WriteLine("A plot of the resulting wavefunction and comparison with the exact result is vizualised in Hydrogen.svg.\n");

	var rs = new odesolver.genlist<double>();
	var fs = new odesolver.genlist<vector>();

	double rmin = 0.05, rmax = 8;
	var e0 = new vector(-0.7);
	var fstart = new vector(rmin - rmin * rmin, 1 - 2 * rmin);

	var eroots = roots.newton(M(rmin, rmax), e0, acc: 1e-3);

	(rs, fs) = odesolver.driver(radialschrodinger(eroots[0]), (rmin, rmax), fstart, 1e-2, 1e-3, 1e-3);
	var IC = CultureInfo.InvariantCulture;
	using (var output = new StreamWriter("Hydrogen.txt")){
		for (int i = 0; i < rs.size; i++)
		output.WriteLine($"{rs[i].ToString(IC)} {fs[i][0].ToString(IC)}");}

	// Convergence
	WriteLine("An investigation of the convergence of the solution towards the exact result with respect to rmax, rmin\n"+
		"and with respect to the parameters acc and eps of my ODE integrators is vizualised in Convergence.svg");

	int n = 100;
	var rmins = new genlist<double>();
	var rmaxs = new genlist<double>();
	var accs = new genlist<double>();
	var epss = new genlist<double>();

	for (int i = 0; i < n; i++){
		rmins.add(0.001 + i * (0.4 - 0.001) / (n - 1));
		rmaxs.add(1 + i * (rmax - 1) / (n - 1));
		accs.add(Log10(1e-6) + (Log10(1e-1) - Log10(1e-6)) / (n - 1) * i);
		epss.add(Log10(1e-9) + (Log10(1e-1) - Log10(1e-9)) / (n - 1) * i);
	}

	using (var output = new StreamWriter("Convergence.txt")){
		for (int i = 0; i < n; i++){
			double rminData = roots.newton(M(rmins[i], rmax, 1e-4, 1e-4), e0)[0];
			double rmaxData = roots.newton(M(rmin, rmaxs[i], 1e-4, 1e-4), e0)[0];
			double accData = roots.newton(M(rmin, rmax, Pow(10, accs[i]), 1e-4), e0)[0];
			double epsData = roots.newton(M(rmin, rmax, 1e-4, Pow(10, epss[i])), e0)[0];
		output.WriteLine($"{rmins[i].ToString(IC)} {rminData.ToString(IC)} {rmaxs[i].ToString(IC)} {rmaxData.ToString(IC)} {accs[i].ToString(IC)} {accData.ToString(IC)} {epss[i].ToString(IC)} {epsData.ToString(IC)}");
		}
	}

} // partB

public static Func<vector, vector> M(double rmin, double rmax, double acc = 1e-4, double eps = 1e-4)
{
    var fstart = new vector(rmin - rmin * rmin, 1 - 2 * rmin);
    Func<vector, vector> FE = e =>
    {
        var ys = odesolver.driver(radialschrodinger(e[0]), (rmin, rmax), fstart, 1e-2, acc, eps).Item2;
        return new vector(ys[ys.size - 1][0]);
    };
    return FE;
}

public static Func<double, vector, vector> radialschrodinger(double E)
{
    Func<double, vector, vector> f = (r, u) =>
    {
        var f1 = u[0];
        var f2 = u[1];
        var f3 = -2 * (E + 1 / r) * u[0];
        return new vector(f2, f3);
    };
    return f;
}

} // main
