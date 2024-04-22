LIBS = $(addprefix -reference:, $(filter %.dll, $^))
CODE = $(filter %.cs, $^)

all : out.txt lspline.svg qspline.svg

lspline.svg : lspline.gpi
	gnuplot $<

qspline.svg : qspline.gpi
	gnuplot $<

out.txt : main.exe
	mono $< > $@

main.exe : main.cs splines.dll matrix.dll
	mcs $(CODE) $(LIBS) -target:exe -out:$@

splines.dll : lspline.cs qspline.cs matrix.dll
	mcs $(CODE) $(LIBS) -target:library -out:$@


matrix.dll : vector.cs matrix.cs
	mcs $(CODE) -target:library -out:$@

clean :
	rm -f main.exe splines.dll matrix.dll out.txt lspline.svg qspline.svg lspline.txt qspline.txt
