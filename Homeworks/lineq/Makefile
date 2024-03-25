LIBS = $(addprefix -reference:, $(filter %.dll, $^))
CODE = $(filter %.cs, $^)

out.txt : main.exe
	mono $< > $@

main.exe : main.cs matrix.dll QRGS.dll Makefile
	mcs $(CODE) $(LIBS) -target:exe -out:$@

QRGS.dll : QRGS.cs matrix.dll
	mcs $(CODE) $(LIBS)  -target:library -out:$@

matrix.dll : vector.cs matrix.cs
	mcs $(CODE) -target:library -out:$@


clean :
	rm -f out.txt main.exe matrix.dll QRGS.dll
