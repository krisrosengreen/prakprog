MATLIB = ~/prakprog/matlib
MATRIXDIR = $(MATLIB)/matrix
CMATHDIR = $(MATLIB)/complex
DLLS = $(addprefix -r:,$(filter %.dll,$^))
MAKEDLL = mcs -t:library -out:$@ $(filter %.cs,$^)

Out.txt: main.exe
	mono main.exe < inputfile > Out.txt
	cat Out.txt

main.exe: main.cs matrix.dll cmath.dll
	mcs main.cs $(DLLS)

matrix.dll: $(MATRIXDIR)/matrix.cs $(MATRIXDIR)/vector.cs
	$(MAKEDLL) 

cmath.dll: $(CMATHDIR)/cmath.cs $(CMATHDIR)/complex.cs
	$(MAKEDLL)

clean:
	rm -r *.dll *.txt *.exe
