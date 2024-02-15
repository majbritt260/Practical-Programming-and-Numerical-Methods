// File for testing my 3D eucledean vector library

using static System.Console;

static class main{
static void Main(){

	// Testing contructors
	vec v = new vec(1, 2, 3);
	vec w = new vec(4, 5, 6);

	// Testing print method
	v.print("v = ");
	w.print("w = ");

	// Testing operators
	(v + w).print("v + w = ");
	(- w).print("- w = ");
	(v - w).print("v - w = ");
	(2 * v).print("2 * v = ");

	// Testing dot product, cross product, and norm methods
	WriteLine($"v · w = {vec.dot(v, w)}");
	WriteLine($"v x w = {vec.cross(v, w)}");
	WriteLine($"|v| = {vec.norm(v)}");

	// Testing the approximation function
	WriteLine($"Are v and w the same vector? {vec.approx(v, w, 1)}");
	WriteLine($"Are v and v the same vector? {vec.approx(v, v, 1)}");

}
}
