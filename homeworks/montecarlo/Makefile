MATLIB = ../../matlib
MATRIXLIB = $(MATLIB)/matrix
DLLS = $(addprefix -r:,$(filter %.dll,$^))
MAKEDLL = mcs -t:library -out:$@ $(filter %.cs,$^)
CSFILES = $(filter %.cs,$^)

Out.txt: main.exe
	mono $< > $@
	cat $@ 

main.exe: main.cs matrix.dll montecarlo.cs
	mcs $(CSFILES) $(DLLS)

matrix.dll: $(MATRIXLIB)/matrix.cs $(MATRIXLIB)/vector.cs
	$(MAKEDLL)

clean:
	rm -r *.dll *.txt *.exe
