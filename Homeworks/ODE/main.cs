using System;
using static System.Console;
using System.IO;
using System.Globalization;
using static System.Math;

public static class main{
public static void Main(){
	// Borders for neat output file
	string border = new string('-', 75);

	///////////////////// PART A ////////////////////
	WriteLine(border);
        WriteLine("PART A: Embedded rule Runge-Kutta ODE integrator");
        WriteLine(border);

	WriteLine("The driver and stepper routines are located in ODE.cs, where the embedded\n" +
                        "stepper method is of order 12\n");

	WriteLine("For debugging purposes the routines were tested solving the second\n" +
			"order differential equation u''=-u which should return sin(x). This\n" +
			"is vizualised in odesolver_test.svg\n");

	odesolver_test();

	WriteLine("With the driver and stepper routines the oscillator with friction example is\n" +
			" reproduced, and vizualized in damped_oscillator.svg\n");

	damped_oscillator();

        ///////////////////// PART B ////////////////////
        WriteLine(border);
        WriteLine("PART B: Alternative interface");
        WriteLine(border);

	WriteLine("The ODE interpolator is tested using the same ODE as in part A. This \n" +
			"is vizualised in ode_interpolant_test.svg\n");

	ode_interpolant_test();

	WriteLine("Integrating the ODE for equatorial motion with the values for ε and initial \n" +
			"conditions as stated in the homework description yields newtonian circular \n" +
			"motion, newtonial elliptical motion, and the relativistic precession of a \n" +
			"planetary orbit, respectively. These results are vizualised and can be seen \n" +
			"in planetary_motion.svg\n");

	planetary_motion();

        ///////////////////// PART C ////////////////////
/*        WriteLine(border);
//        WriteLine("PART C: Newtonian gravitational three-body problem");
//        WriteLine(border);

	threebody_problem();
*/

} // Main()


public static void odesolver_test(){
	// Testing routine by solving u" = -u (solution should be sin(x)))
	var interval = (0 , 2 * Math.PI);
	vector ystart = new vector(0 , 1); // initial conditions  y1' = 0 and y2'' = 1
	var points = odesolver.driver(test_func , interval , ystart);
	var xs = points.Item1;
	var ys = points.Item2;

	File.Delete("odesolver_test.txt"); // delete file for reruns
	for (int i = 0 ; i < xs.size ; i++){
		File.AppendAllText("odesolver_test.txt" , $"{xs[i].ToString(CultureInfo.InvariantCulture)} " +
						$"{ys[i][0].ToString(CultureInfo.InvariantCulture)} " +
						$"{ys[i][1].ToString(CultureInfo.InvariantCulture)}\n");
	}

} // test()

public static void damped_oscillator(){
	// Reproduce oscillator with friction example
	var interval = (0 , 10);
	vector ystart = new vector(PI - 0.1 , 0.0); // initial conditions
	var points = odesolver.driver(damped_oscillator_func , interval , ystart);
	var xs = points.Item1;
	var ys = points.Item2;

	File.Delete("damped_oscillator.txt");
	for (int i = 0 ; i < xs.size ; i++){
	File.AppendAllText("damped_oscillator.txt" , $"{xs[i].ToString(CultureInfo.InvariantCulture)} " +
						$"{ys[i][0].ToString(CultureInfo.InvariantCulture)} " +
						$"{ys[i][1].ToString(CultureInfo.InvariantCulture)}\n");
	}

} // dampened_oscillator()

public static void ode_interpolant_test(){
        // Testing routine by solving u" = -u (solution should be sin(x)))
        var interval = (0 , 2 * Math.PI);
        vector ystart = new vector(0 , 1); // initial conditions  y1' = 0 and y2'' = 1

	var (xs , ys, interpolant) = odesolver.make_ode_ivp_interpolant(test_func, interval, ystart);

	File.Delete("ode_interpolant_test.txt"); // delete file for reruns
	for (int i = 0 ; i < xs.size - 1 ; i++){
		double z = (xs[i] + xs[i + 1]) / 2;
                File.AppendAllText("ode_interpolant_test.txt" , $"{xs[i].ToString(CultureInfo.InvariantCulture)} " +
                                                $"{ys[i][0].ToString(CultureInfo.InvariantCulture)} " +
                                                $"{z.ToString(CultureInfo.InvariantCulture)} " +
						$"{interpolant(z)[0].ToString(CultureInfo.InvariantCulture)} \n");
        }

} // ode_interpolant_test

public static void planetary_motion(){
	// Integrate u''(φ) + u(φ) = 1 + εu(φ)^2 to obtain different planetary orbits around a star
	var interval = (0 , 2 * Math.PI);
        File.Delete("planetary_motion.txt");

	// 1) newtonian circular motion
	vector ystart1 = new vector (1.0 - 1.0/128, 0.0);
	var points1 = odesolver.driver(newtonian_motion_func , interval , ystart1, 0.0001,0.0001,0.0001);
	var xs1 = points1.Item1;
	var ys1 = points1.Item2;

	for (int i = 0 ; i < xs1.size ; i++){
	File.AppendAllText("planetary_motion.txt" , $"{xs1[i].ToString(CultureInfo.InvariantCulture)} " +
						$"{ys1[i][0].ToString(CultureInfo.InvariantCulture)} " +
						$"{ys1[i][1].ToString(CultureInfo.InvariantCulture)}\n");
	}

	File.AppendAllText("planetary_motion.txt" , "\n\n"); // indexing data for gnuplot

	// 2) newtonian elliptical motion
        vector ystart2 = new vector (1.0 - 1.0/128, -0.5);
        var points2 = odesolver.driver(newtonian_motion_func , interval , ystart2, 0.0001,0.0001,0.0001);
        var xs2 = points2.Item1;
        var ys2 = points2.Item2;

        for (int i = 0 ; i < xs2.size ; i++){
        File.AppendAllText("planetary_motion.txt" , $"{xs2[i].ToString(CultureInfo.InvariantCulture)} " +
                                                $"{ys2[i][0].ToString(CultureInfo.InvariantCulture)} " +
                                                $"{ys2[i][1].ToString(CultureInfo.InvariantCulture)}\n");
        }

	File.AppendAllText("planetary_motion.txt" , "\n\n"); // indexing data for gnuplot

	// 3) relativistic precession
	var interval3 = (0 , 8 * Math.PI);
        vector ystart3 = new vector (1.0 - 1.0/128, -0.5);
        var points3 = odesolver.driver(relativistic_precession_func , interval3 , ystart3, 0.0001,0.0001,0.0001);
        var xs3 = points3.Item1;
        var ys3 = points3.Item2;

        for (int i = 0 ; i < xs3.size ; i++){
        File.AppendAllText("planetary_motion.txt" , $"{xs3[i].ToString(CultureInfo.InvariantCulture)} " +
                                                $"{ys3[i][0].ToString(CultureInfo.InvariantCulture)} " +
                                                $"{ys3[i][1].ToString(CultureInfo.InvariantCulture)}\n");
        }
}

/*public static void threebody_problem(){
// Reproduce solution from wikipedia
        var interval = (0 , 6.3259);             // period
        vector ystart = new vector(0.4662036850, // initial conditions
				0.4323657300,
				-0.93240737,
				-0.86473146,
				0.4662036850,
				0.4323657300,
				-0.97000436,
				0.24308753,
				0,
				0,
				0.97000436,
				-0.24308753);
        var points = odesolver.driver(threebody_func , interval , ystart);
        var xs = points.Item1;
        var ys = points.Item2;

	File.Delete("threebody_problem.txt"); // delete file for reruns
	for (int i = 0 ; i < xs.size ; i++){
		File.AppendAllText("" , $"{}");
	}
}
*/



//////////////////// FIRST ORDER EQUATIONS ///////////////////////////

// Test function for debugging
// Retwrite second order differential equation u" = -u into two first-order equations following procedure from  book
// y1 = u , y2 = u' => y1' = y2 , y2' = -y1
static vector test_func(double x , vector ys){return new vector (ys[1] , - ys[0]);}

// Function for dampened oscillator example
// Rewrite second order differential equation u" + b*u' + c*sin(u) = 0 into two first-order equations
// y1 = u , y2 = u' => y1' = y2 , y2' = -b*y2 - c*sin(y1)
static vector damped_oscillator_func(double x , vector ys){return new vector (ys[1] , -0.25 * ys[1] - 5.0*Sin(ys[0]));}

// Function for equatorial motion of a planet around a star
// Rewrite second order differential equation u" + u = 1 + ε*u*u
// y1 = u , y2 = u' => y1' = y2 , y2´ = 1 - y1 + ε*y1*y1, ε is the relativistic correction
static vector newtonian_motion_func(double x , vector ys){return new vector (ys[1] , 1 - ys[0]);}
static vector relativistic_precession_func(double x , vector ys){return new vector (ys[1] , 1 - ys[0] + 0.01 * ys[0] * ys[0]);}

// Function for threebody problem
//static vector threebody_func(double x, vector ys){
/*	double vx1 = ys[0];
	double vy1 = ys[1];
	double vx2 = ys[2];
	double vy2 = ys[3];
	double vx3 = ys[4];
	double vy3 = ys[5];
	double xx1 = ys[6];
	double xy1 = ys[7];
	double xx2 = ys[8];
	double xy2 = ys[9];
	double xx3 = ys[10];
	double xy3 = ys[11];
	double d12 = Sqrt((xx1 - xx2) * (xx1 - xx2) + (xy1 - xy2) * (xy1 - xy2));
	double d13 = Sqrt((xx1 - xx3) * (xx1 - xx3) + (xy1 - xy3) * (xy1 - xy3));
	double d23 = Sqrt((xx3 - xx2) * (xx3 - xx2) + (xy3 - xy2) * (xy3 - xy2));
	return new vector(new double[] {
			(xx2 - xx1) /Pow(d12 , 3) + (xx3 - xx1) / Pow(d13 , 3),
			(xy2 - xy1) /Pow(d12 , 3) + (xy3 - xy1) / Pow(d13 , 3),
			(xx1 - xx2) /Pow(d12 , 3) + (xx3 - xx2) / Pow(d23 , 3),
			(xy1 - xy2) /Pow(d12 , 3) + (xy3 - xy2) / Pow(d23 , 3),
			(xx1 - xx3) /Pow(d13 , 3) + (xx2 - xx3) / Pow(d23 , 3),
			(xy1 - xy3) /Pow(d13 , 3) + (xy2 - xy3) / Pow(d23 , 3),
			vx1,
			vy1,
			vx2,
			vy2,
			vx3,
			vy3});}
*/
} // main
