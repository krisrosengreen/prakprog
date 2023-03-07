Out.txt: main.exe
	mono main.exe > Out.txt
	cat Out.txt

main.exe: main.cs
	mcs $(addprefix -r:,$(shell ls | grep ".dll")) main.cs

clear:
	rm main.exe
	rm Out.txt
