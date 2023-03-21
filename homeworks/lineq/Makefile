MATLIB = ~/prakprog/matlib
MATRIXDIR = $(MATLIB)/matrix
DLLS = $(addprefix -r:,$(filter %.dll,$^))
MAKEDLL = mcs -t:library -out:$@ $(filter %.cs,$^)

Out.txt: main.exe
	mono main.exe < inputfile > Out.txt
	cat Out.txt

main.exe: main.cs matrix.dll QRGS.dll
	mcs main.cs $(DLLS)

QRGS.dll: QRGS.cs matrix.dll
	$(MAKEDLL) $(DLLS)

matrix.dll: $(MATRIXDIR)/matrix.cs $(MATRIXDIR)/vector.cs
	$(MAKEDLL) 

clean:
	rm -r *.dll *.txt *.exe
