

Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs vec.dll
	mcs main.cs -out:main.exe -target:exe -reference:vec.dll

vec.dll : vec.cs
	mcs vec.cs -out:vec.dll -target:library

clean:
	rm -f vec.dll main.exe Out.txt
