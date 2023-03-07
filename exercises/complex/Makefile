DIR = ~/prakprog/matlib/complex
Out.txt: main.exe
	mono main.exe > Out.txt
	cat Out.txt

main.exe: main.cs cmath.dll
	mcs -reference:cmath.dll main.cs

cmath.dll: $(DIR)/cmath.cs $(DIR)/complex.cs chyper.cs
	mcs -target:library -out:./cmath.dll $^

clean:
	rm Out.txt cmath.dll main.exe
