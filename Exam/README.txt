Implementation
--------------
An implementation of the Cooley Tukey algorithm for FFT can be found in fourierlib.cs. 
The  modified version not using array-copying was attempted and can be found in modfourierlib.cs.
However it is not working as intended, as illustrated in NoiseFiltering.svg.
The naive implementation is inspired from the example in The Book.

Demonstration 
-------------
To demonstrate the implementations I made a few plots, all based on noise filtering. 
The data generation for this can be found in main.cs (and modmain.cs for the failed attempt on non-array-copying implementation)

2 plots were made:

* NoiseFiltering.svg
Demonstration of the naive and modified FFT on the noisy data.
 
* ExecutionTime.svg
Demonstration of the scaling of the naive and modified FFT, to check if it is indeed O(Nlog(N)). 
For the Cooley-Tukey algorithm, this scaling is achieved for N = 2^n, hence values of this type are used for the scaling analysis. 

Self evaluation
---------------
Using the same system for point distribution as in the homeworks:
Part A: successful implementation (6 points)
Part B: successful implementation (3 points)
Part C: unsuccessful implementation (0 points)

Total: 9/10 points

All the good intentions
------------------------
If I had succeeded with part C, I would have definitely worked on something cool, such as having only one main file for both implementations.




