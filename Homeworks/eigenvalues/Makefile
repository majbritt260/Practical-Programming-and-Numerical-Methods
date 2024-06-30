LIBS = $(addprefix -reference:, $(filter %.dll, $^))
CODE = $(filter %.cs, $^)

all : out.txt plots

plots : plots.gpi
	gnuplot $<

out.txt : main.exe
	mono $< > $@

main.exe : main.cs jacobi.dll matrix.dll
	mcs $(CODE) $(LIBS) -target:exe -out:$@

jacobi.dll: jacobi.cs matrix.dll
	mcs $(CODE) $(LIBS) -target:library -out:$@

matrix.dll : vector.cs matrix.cs
	mcs $(CODE) -target:library -out:$@

clean :
	$(RM) *.exe *.txt *.dll *.svg
