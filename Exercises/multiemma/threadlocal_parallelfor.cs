using static System.Console;
using System.Linq;

public static class main{
public static void Main(){
	int nterms = (int) 1e8;

        var sum = new System.Threading.ThreadLocal<double>( () => 0 , trackAllValues:true);
        System.Threading.Tasks.Parallel.For(1 , nterms + 1 , delegate(int i) {sum.Value += 1.0 / i;});
        double totalsum = sum.Values.Sum();
        WriteLine($"\nHarmonic sum using ThreadLocal : {totalsum}");}
}
