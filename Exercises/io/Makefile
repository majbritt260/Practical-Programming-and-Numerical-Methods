all : out1.txt out2.txt out3.txt


out1.txt : main1.exe Makefile
	mono main1.exe -numbers:1,2,3,4,5 > out1.txt
	cat out1.txt

out2.txt : main2.exe Makefile
	echo 1 2 3 4 5 > input2.txt
	mono main2.exe < input2.txt 2> out2.txt
	cat out2.txt

out3.txt : main3.exe Makefile
	mono main3.exe -input:input3.txt -output:out3.txt
	cat out3.txt



main1.exe : main1.cs
	mcs $^ -target:exe -out:$@

main2.exe : main2.cs
	mcs $^ -target:exe -out:$@

main3.exe : main3.cs
	mcs $^ -target:exe -out:$@



clean :
	rm -f main1.exe main2.exe main3.exe out1.txt out2.txt out3.txt input2.txt
