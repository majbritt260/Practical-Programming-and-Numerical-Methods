// This file contains a class representing eucledean 3D vectors

using static System.Console;
using static System.Math;

public class vec{
	public double x, y, z;

	// Constructors
	public vec(){x = y = z = 0;}
	public vec(double x, double y, double z){
		this.x = x;
		this.y = y;
		this.z = z;
		}

	// Operators
	public static vec operator*(vec v, double c){return new vec(c * v.x , c * v.y , c * v.z);}
	public static vec operator*(double c, vec v){return v * c;}
	public static vec operator+(vec u, vec v){return new vec(u.x + v.x , u.y + v.y , u.z + v.z);}
	public static vec operator-(vec u){return new vec(-u.x , -u.y , -u.z);}
	public static vec operator-(vec u, vec v){return new vec(u.x - v.x , u.y - v.y , u.z - v.z);}

	// Print methods
	public void print(string s){Write(s) ; WriteLine($"{x} {y} {z}");}
	public void print(){this.print("");}

	// Dot product method, called as vec.dot(v, w)
	public static double dot(vec v , vec w)
	{return v.x * w.x + v.y * w.y + v.z * w.z;}

	// Norm of a vector, called as vec.norm(v)
	public static double norm(vec v)
	{return Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);}

	// Cross product of a vector, called as vec.cross(v, w)
	public static vec cross(vec v , vec w)
	{return new vec(v.y * w.z - v.z * w.y , v.z * w.x - v.x * w.z , v.x * w.y - v.y * w.x);}


	// Approximation method
        static bool approx(double a, double b, double acc = 1e-9, double eps = 1e-9){

                if (Abs(a - b) < acc) return true;
                if (Abs(a - b) < (Abs(a) + Abs(b)) * eps) return true;
                return false;
        }

        public bool approx(vec other, double acc = 1e-9, double eps = 1e-9){

                if (!approx(this.x, other.x, acc, eps)) return false;
                if (!approx(this.y, other.y, acc, eps)) return false;
                if (!approx(this.z, other.z, acc, eps)) return false;
                return true;
        }

        public static bool approx(vec u, vec v, double acc = 1e-9, double eps = 1e-9) => u.approx(v, acc, eps); // syntax sugar


	// Override ToString method
	public override string ToString(){return $"{x} {y} {z}";}
}

