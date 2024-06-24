LIBS = $(addprefix -reference:, $(filter %.dll, $^))
CODE = $(filter %.cs, $^)

all : out.txt plots

plots : UnitCircle.gpi
	gnuplot $<

out.txt : main.exe Makefile
	mono $< > $@

main.exe : main.cs montecarlo.dll matrix.dll
	mcs $(CODE) $(LIBS) -target:exe -out:$@

montecarlo.dll : montecarlo.cs matrix.dll
	mcs $(CODE) $(LIBS) -target:library -out:$@

matrix.dll : matrix.cs vector.cs
	mcs $(CODE) -target:library -out:$@

clean :
	$(RM) *.dll *.exe *.txt *.svg
