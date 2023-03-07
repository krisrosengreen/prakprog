Out.txt: main.exe
	mono main.exe > Out.txt
	cat Out.txt

main.exe: main.cs
	mcs main.cs

clean:
	rm main.exe
