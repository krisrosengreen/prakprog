Out.txt: main.exe
	mono main.exe > Out.txt
	cat Out.txt

main.exe: main.cs mymatlib.dll
	mcs -r:mymatlib.dll main.cs

mymatlib.dll: mymatlib.cs
	mcs -t:library mymatlib.cs

clear:
	rm main.exe
	rm Out.txt
