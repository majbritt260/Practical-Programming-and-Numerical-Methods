LIBS = $(addprefix -reference:, $(filter %.dll, $^))
CODE = $(filter %.cs, $^)

all : out.txt plots

plots : plots.gpi
	gnuplot $<

out.txt: main.exe Makefile
	mono $< > $@

main.exe: main.cs matlib.dll
	mcs $(CODE) $(LIBS) -target:exe -out:$@

matlib.dll: vector.cs matrix.cs min.cs ann.cs genlist.cs QRGS.cs integrator.cs
	mcs $(CODE) -target:library -out:$@

clean:
	$(RM) *.exe *.dll [Ll]og* *.svg *.txt
