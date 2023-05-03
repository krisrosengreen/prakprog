MATLIB = ../../matlib
MATRIXDIR = $(MATLIB)/matrix
DLLS = $(addprefix -r:,$(filter %.dll,$^))
MAKEDLL = mcs -t:library -out:$@ $(filter %.cs,$^)

Out.txt: main.exe
	mono $< > $@
	cat $@ 

main.exe: main.cs matrix.dll
	mcs $< $(DLLS)

matrix.dll: $(MATRIXDIR)/matrix.cs $(MATRIXDIR)/vector.cs
	$(MAKEDLL)

clean:
	rm -r *.dll *.txt *.exe
