# if time permitted, I would have implemented the -terms command for all methods

N = 1e8
TIME = /usr/bin/time -p -a -o $@

out.txt : main.exe parallelfor.exe threadlocal_parallelfor.exe Makefile
	>$@
	$(TIME) mono $< -terms:$N -threads:1 >>$@
	$(TIME) mono $< -terms:$N -threads:2 >>$@
	$(TIME) mono $< -terms:$N -threads:3 >>$@
	$(TIME) mono $< -terms:$N -threads:4 >>$@
	$(TIME) mono parallelfor.exe >>$@
	$(TIME) mono threadlocal_parallelfor.exe >>$@

main.exe : main.cs
	mcs $<

parallelfor.exe : parallelfor.cs
	mcs $<
threadlocal_parallelfor.exe : threadlocal_parallelfor.cs
	mcs $<
clean:
	rm -f *.exe out.txt
