Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs sfuns.dll
	mcs main.cs -out:main.exe -target:exe -reference:sfuns.dll

sfuns.dll : sfuns.cs
	mcs sfuns.cs -out.sfuns.dll -target:library

clean :
	rm -f sfuns.dll main.exe Out.txt
