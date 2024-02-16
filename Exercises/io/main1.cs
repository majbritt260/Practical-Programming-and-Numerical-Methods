using System;
using static System.Console;
using static System.Math;

static class main{
public static void Main(string[] args){

	// TASK 1
	WriteLine("\nTASK 1");
	foreach(var arg in args){
		var words = arg.Split(':'); // words er splittet af ':' i argument givet
		if (words[0] == "-numbers"){ // hvis vores første word er '--numbers'
			var numbers = words[1].Split(','); // numbers er splittet af ',' i andet ord
			foreach (var number in numbers){
				double x = double.Parse(number); // konverter string til double
				WriteLine($"{x} {Sin(x)} {Cos(x)}");
			}
		}
	}

}
}
