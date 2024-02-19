


out.txt : main.exe input.txt
	mono $< < input.txt > $@

main.exe : main.cs genlist.dll
	mcs $< -target:exe -out:$@ -reference:genlist.dll

genlist.dll : genlist.cs
	mcs $< -target:library -out:$@

clean:
	$(RM) main.exe genlist.dll out.txt


