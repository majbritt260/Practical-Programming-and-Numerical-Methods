LIBS = $(addprefix -reference:, $(filter %.dll, $^))
CODE = $(filter %.cs, $^)

all : out.txt plots

plots : erf.gpi
	gnuplot $<

out.txt : main.exe Makefile
	mono $< > $@

main.exe : main.cs integrator.dll
	mcs $(CODE) $(LIBS) -target:exe -out:$@

integrator.dll : integrator.cs
	mcs $(CODE) -target:library -out:$@

clean:
	$(RM) main.exe integrator.dll out.txt \
	gendata.erf.txt comparison.erf.txt \
	erfcomparison_plotsexercise.svg erfcomparison_tabdata.svg
