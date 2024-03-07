using static System.Console;

public static class main{
public static void Main(){
	double sum = 0;
	int nterms = (int) 1e8;

	System.Threading.Tasks.Parallel.For(1 , nterms + 1 , delegate(int i) {sum += 1.0 / i;});

	WriteLine($"\nHarmonic sum using Parallel.For : {sum}");
}
}
