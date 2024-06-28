LIBS = $(addprefix -reference:, $(filter %.dll, $^))
CODE = $(filter %.cs, $^)

plots : plots.gpi NoiseFiltering.txt ModNoiseFiltering.txt ExecutionTime.txt Makefile
	gnuplot $<

NoiseFiltering.txt : main.exe Makefile
	mono $< > $@

ModNoiseFiltering.txt : modmain.exe Makefile
	mono $< > $@

N = $(shell echo '2^13; 2^14; 2^15; 2^16; 2^17; 2^18; 2^19'|bc -l)
ExecutionTime.txt : main.exe modmain.exe Makefile
	> $@
	for n in $(N); do \
	main_time=$$( { time mono ./main.exe $$n; } 2>&1 | grep user | awk '{ split($$2, a, "[ms]"); print a[1] * 60 + a[2] }' ); \
	modmain_time=$$( { time mono ./modmain.exe $$n; } 2>&1 | grep user | awk '{ split($$2, a, "[ms]"); print a[1] * 60 + a[2] }' ); \
	echo $$n $$main_time $$modmain_time >> $@; \
	done

modmain.exe : modmain.cs cmath.dll modfourierlib.dll
	mcs $(CODE) $(LIBS) -target:exe -out:$@

main.exe : main.cs cmath.dll fourierlib.dll
	mcs $(CODE) $(LIBS) -target:exe -out:$@


modfourierlib.dll : modfourierlib.cs cmath.dll
	mcs $(CODE) $(LIBS) -target:library -out:$@

fourierlib.dll : fourierlib.cs cmath.dll
	mcs $(CODE) $(LIBS) -target:library -out:$@

cmath.dll : cmath.cs complex.cs
	mcs $(CODE) -target:library -out:$@

clean :
	 $(RM) *.exe *.log *.dll *.svg \
		NoiseFiltering.txt ModNoiseFiltering.txt ExecutionTime.txt \

