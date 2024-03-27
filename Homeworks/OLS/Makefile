LIBS = $(addprefix -reference:, $(filter %.dll, $^))
CODE = $(filter %.cs, $^)

all: fit.svg fitdata.txt out.txt

fit.svg : fit.gpi fitdata.txt
	gnuplot $<

fitdata.txt : main.exe
	mono $< > $@ -input:data.txt -output:fitdata.txt

out.txt : main.exe
	mono $< > $@ -input:data.txt -output:fitdata.txt

main.exe : main.cs matrix.dll OLSF.dll Makefile
	mcs $(CODE) $(LIBS) -target:exe -out:$@

OLSF.dll : OLSF.cs QRGS.dll matrix.dll
	mcs $(CODE) $(LIBS) -target:library -out:$@

QRGS.dll : QRGS.cs matrix.dll
	mcs $(CODE) $(LIBS)  -target:library -out:$@

matrix.dll : vector.cs matrix.cs
	mcs $(CODE) -target:library -out:$@


clean :
	rm -f out.txt fitdata.txt fit.svg main.exe matrix.dll QRGS.dll OLSF.dll

