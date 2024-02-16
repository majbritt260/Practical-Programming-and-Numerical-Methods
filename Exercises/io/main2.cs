using System;
using static System.Console;
using static System.Math;

static class main{
public static void Main(string[] args){

	// TASK 2
	WriteLine("\nTASK 2");
	char[] split_delimiters = {' ', '\t', '\n'}; // danner en liste bestående af char-type characters
	var split_options = StringSplitOptions.RemoveEmptyEntries; // tomme felter fjernes når string splittes
	for (string line = ReadLine() ; line != null ; line = ReadLine()){ // efter hver iteration læses næste linje som bliver ny "line"
		var numbers = line.Split(split_delimiters, split_options);
		foreach (var number in numbers){
			double x = double.Parse(number);
			Error.WriteLine($"{x} {Sin(x)} {Cos(x)}");
		}
	}
}
}
