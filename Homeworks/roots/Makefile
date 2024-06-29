LIBS = $(addprefix -reference:, $(filter %.dll, $^))
CODE = $(filter %.cs, $^)

all : out.txt plots

plots : plots.gpi
	gnuplot $<

out.txt : main.exe
	mono $< > $@

main.exe : main.cs roots.dll matrix.dll ODE.dll
	mcs $(CODE) $(LIBS) -target:exe -out:$@

roots.dll : roots.cs QRGS.dll matrix.dll
	mcs $(CODE) $(LIBS) -target:library -out:$@

QRGS.dll : QRGS.cs matrix.dll
	mcs $(CODE) $(LIBS)  -target:library -out:$@

matrix.dll : vector.cs matrix.cs
	mcs $(CODE) -target:library -out:$@

ODE.dll : ODE.cs matrix.dll
	mcs $(CODE) $(LIBS) -target:library -out:$@

clean :
	$(RM) *.exe *.txt *.dll *.svg
