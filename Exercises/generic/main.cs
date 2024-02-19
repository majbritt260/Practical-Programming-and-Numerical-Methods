// Class containing Main function

using System;
using static System.Console;

public static class main{
public static void Main(){
	var list = new genlist<double[]>();
	char[] delimiters = {' ', '\t'}; //no \n since we read lines later on?
	var options = StringSplitOptions.RemoveEmptyEntries;

	// read input, turn to doubles, put into list
	for (string line = ReadLine() ; line!= null ; line = ReadLine()){
		var words = line.Split(delimiters, options);
		int n = words.Length;
		var numbers = new double[n]; // creates an array of double values with length n, and assigns a reference to that array to the variable numbers
		for (int i = 0 ; i < n ; i++){numbers[i] = double.Parse(words[i]);} // turn strings into doubles
		list.add(numbers); // adds the numbers to the list
		}

	//
	for (int i = 0 ; i < list.size ; i++){
		var numbers = list[i]; // creates list of lengtht i
		foreach (var number in numbers){Write($"{number : 0.00e+00 ; -0.00e+00}");} // two decimal points, scientific notation for both pos and neg numbers
		WriteLine();
		}
}
}
