Out1.txt Out2.txt Out3.txt: main.exe
	mono main.exe -numbers:1,2,3,4,5 > Out1.txt
	echo 1 2 3 4 5 6 7 8 | mono main.exe > Out2.txt
	mono main.exe -input:inputfile -output:Out3.txt

main.exe: main.cs
	mcs main.cs
