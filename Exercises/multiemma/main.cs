using System;
using System.Threading;
using static System.Console;

public class data{
        public int a,b;
        public double sum;
}

public class harm{
public static void harmonic(object obj){
                var local = (data) obj;
                local.sum = 0;
                for (int i = local.a ; i < local.b ; i++)
                local.sum += 1.0 / i;
}
}

public static class main{
	public static void Main(string[] args){
		int nthreads = 1;
		int nterms = (int) 1e8;

		foreach (var arg in args){
			var words = arg.Split(":");
			if (words[0] == "-threads") nthreads = int.Parse(words[1]);
			if (words[0] == "-terms") nterms = (int) float.Parse(words[1]);
			}

		data[] intervals = new data[nthreads];
		for (int i = 0; i < nthreads; i++){
			intervals[i] = new data();
			intervals[i].a = 1 + nterms / nthreads * i;
			intervals[i].b = 1 + nterms / nthreads * (i + 1);
			}

		intervals[intervals.Length - 1].b = nterms + 1;

		var threads = new Thread[nthreads];
		for (int i = 0 ; i < nthreads ; i++){
			threads[i] = new Thread(harm.harmonic);
			threads[i].Start(intervals[i]);
			}

		for (int i = 0 ; i < nthreads ; i++) threads[i].Join();

		double sum = 0;
		for (int i = 0 ; i < nthreads; i++) sum += intervals[i].sum;

		WriteLine($"\nNumber of terms : {nterms}");
		WriteLine($"Number of threads  : {nthreads}");
		WriteLine($"Harmonic sum : {sum}");
}
}


