Out.txt: main.exe
	mono main.exe > Out.txt
	cat Out.txt

main.exe: main.cs vec.dll
	mcs -target:exe -out:main.exe -reference:vec.dll main.cs

vec.dll: vec.cs
	mcs -t:library vec.cs

clean:
	rm main.exe Out.txt vec.dll
