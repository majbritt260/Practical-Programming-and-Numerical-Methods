LIBS = $(addprefix -reference:, $(filter %.dll, $^))
CODE = $(filter %.cs, $^)

all : out.txt plots

plots : plots.gpi
	gnuplot $<

out.txt : main.exe Makefile
	mono $< < higgsdata.txt 1> $@ 2> log

main.exe : main.cs min.dll matrix.dll genlist.dll
	mcs $(CODE) $(LIBS) -target:exe -out:$@

min.dll : min.cs QRGS.dll matrix.dll
	mcs $(CODE) $(LIBS) -target:library -out:$@

genlist.dll : genlist.cs
	mcs $(CODE) -target:library -out:$@

QRGS.dll : QRGS.cs matrix.dll
	mcs $(CODE) $(LIBS)  -target:library -out:$@

matrix.dll : vector.cs matrix.cs
	mcs $(CODE) -target:library -out:$@

clean :
	$(RM) out.txt [Ll]og* Fitting.txt *.exe *.dll *.svg
