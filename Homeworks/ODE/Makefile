LIBS = $(addprefix -reference:, $(filter %.dll, $^))
CODE = $(filter %.cs, $^)

all : out.txt odesolver_test.svg damped_oscillator.svg ode_interpolant_test.svg planetary_motion.svg

odesolver_test.svg : odesolver_test.gpi main.exe
	gnuplot $<

damped_oscillator.svg : damped_oscillator.gpi main.exe
	gnuplot $<

ode_interpolant_test.svg : ode_interpolant_test.gpi main.exe
	gnuplot $<

planetary_motion.svg : planetary_motion.gpi main.exe
	gnuplot $<

out.txt : main.exe Makefile
	mono $< > $@

main.exe : main.cs ODE.dll matrix.dll
	mcs $(CODE) $(LIBS) -target:exe -out:$@


ODE.dll : ODE.cs matrix.dll
	mcs $(CODE) $(LIBS) -target:library -out:$@

matrix.dll : vector.cs matrix.cs
	mcs $(CODE) -target:library -out:$@

clean :
	$(RM) main.exe ODE.dll matrix.dll out.txt \
	odesolver_test.txt odesolver_test.svg \
	damped_oscillator.txt damped_oscillator.svg \
	ode_interpolant_test.txt ode_interpolant_test.svg \
	planetary_motion.txt planetary_motion.svg
