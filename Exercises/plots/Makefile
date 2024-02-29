all: erf.png gamma.png lngamma.png

CODE = $(filter %.cs , $^) #find all cs files in the prerequisites
LIBS = $(addprefix -reference:, $(filter %.dll , $^)) #find all dll files in the prerequisites, give prefix
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

erf.png : out.txt tabdata.erf.txt Makefile
	echo '\
		set terminal png background "white" ;\
		set out "$@" ;\
		set key left ;\
		set xlabel "x" ;\
		set ylabel "erf(x)" ;\
		set tics out ;\
		set grid ;\
		set title "Error function" ;\
		plot [-3 : 3] \
			 "$<" index 0 using 1:2 with lines lc rgb "#2039ba" title "Single precision error function" \
			,"tabdata.erf.txt" using 1:2 with points pointtype 6 lc rgb "#57d762"  title "Tabulated values" \
			,"tabdata.erf.txt" using (-($$1)):(-($$2)) with points pointtype 6 lc rgb "#57d762" notitle ;\
	' | tee log.gpi | gnuplot


gamma.png : out.txt Makefile
	echo '\
		set terminal png background "white" ;\
		set out "$@" ;\
		set key left spacing 2 box lt -1 lw 1 ;\
		set xlabel "x" ;\
		set grid ;\
		set title "Gamma function" ;\
		plot [-0.2 : 3.2] [0 : 6.2] \
			 "$<" index 1 using 1:2 with lines linewidth 2 lc rgb "#2039ba" title "Γ(x + 1)" \
			,"$<" index 2 using 1:2 with points pointtype 7 pointsize 1.3 lc rgb "#2039ba"  title "x!" ;\
		' | tee log.gpi | gnuplot


lngamma.png : out.txt Makefile
	echo '\
		set terminal png background "white" ;\
		set out "$@" ;\
		set key left spacing 2 box lt -1 lw 1 ;\
		set xlabel "x" ;\
		set grid ;\
		set title "Logarithm of the gamma function" ;\
		plot [-0.2 : 8] [-2 : 10] \
			 "$<" index 1 using 1:3 with lines linewidth 2 lc rgb "#2039ba" title "ln(Γ(x + 1))" \
			,"$<" index 2 using 1:3 with points pointtype 7 pointsize 1.3 lc rgb "#2039ba"  title "ln(x!)" ;\
		' | tee log.gpi | gnuplot



out.txt : main.exe Makefile
	mono $< | sed 's/,/./g' > $@

main.exe : main.cs sfuns.dll ; $(MKEXE)

sfuns.dll : sfuns.cs ; $(MKLIB)

clean:
	$(RM) main.exe out.txt sfuns.dll log.gpi *.svg
