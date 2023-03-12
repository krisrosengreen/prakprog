Out.txt: main.exe inputfile
	mono $(addprefix -r:,${shell ls | grep ".dll"}) main.exe < inputfile > Out.txt
	cat Out.txt

main.exe: main.cs
	mcs main.cs

clear:
	rm main.exe
	rm Out.txt
