MATLIB = ~/prakprog/matlib
MATRIXDIR = $(MATLIB)/matrix
DLLS = $(addprefix -r:,$(filter %.dll,$^))

Out.txt: main.exe
	mono main.exe < inputfile > Out.txt
	cat Out.txt

main.exe: main.cs matrix.dll
	mcs main.cs $(DLLS)

mymatlib.dll: mymatlib.cs
	mcs -t:library mymatlib.cs

matrix.dll: $(MATRIXDIR)/matrix.cs $(MATRIXDIR)/vector.cs
	mcs -t:library -out:$@ $^

clear:
	rm main.exe
	rm Out.txt
