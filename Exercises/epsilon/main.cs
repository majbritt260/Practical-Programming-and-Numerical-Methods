// This file contails the code used to answer the questions in the epsilon exercise. 

using static System.Console;
using static System.Math;

static class Program{
	static void Main(){


	// TASK 1
	WriteLine("\nTASK 1");

	int i =1;
	while(i + 1 > i){
		i++;
		}
	WriteLine($"My maximum representable integer is {i} which should be the same as the int.MaxValue, which yields a value of {int.MaxValue}");

	int j = 1;
	while(j - 1 < i){
		j--;
		}
	WriteLine($"My minimum representable integer is {j} which should be the same as the int.MinValue, which yields a value of {int.MinValue}");


	// TASK 2
	WriteLine("\nTASK 2");

	double x = 1;
	while(1 + x != 1){
		x /= 2;
		}
	x *= 2;
	double systemx = Pow(2,-52);
	WriteLine($"The machine epsilon for the double type is {x}, which should equal {systemx}");

	float y = 1;
	while(1F + y != 1F){
		y /= 2F;
		}
	y *= 2F;
	double systemy = Pow(2,-23);
	WriteLine($"The machine epsilon for the float type is {y}, which should equal {systemy}");


	// TASK 3
	WriteLine("\nTASK 3");

	double epsilon = systemx;
	double tiny = epsilon/2;
	double a = 1 + tiny + tiny;
	double b = tiny + tiny + 1;

	WriteLine($"a == b? {a == b}");
	WriteLine($"a == 1? {a == 1}");
	WriteLine($"b > 1? {b > 1}");
	WriteLine($"I think the machine regocnizes 'tiny' but can't comprehent tiny by itself, hence rounding it down to 0 when adding it to a number it can comprehent. When adding tiny to tiny the machine suddenly can comprehent the number, at thus it is able to add it.");


	// TASK 4
	WriteLine("\nTASK 4");

	double d1 = 0.1+0.1+0.1+0.1+0.1+0.1+0.1+0.1;
	double d2 = 8*0.1;

	WriteLine($"Using the approximation function to compare d1 and d2 yields {sfuns.approx(d1,d2)}");
	}
}

