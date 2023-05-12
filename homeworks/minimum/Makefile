MATLIB = ../../matlib
MATRIXLIB = $(MATLIB)/matrix
DLLS = $(addprefix -r:,$(filter %.dll,$^))
CSFILES = $(filter %.cs,$^)
MAKEDLL = mcs -t:library -out:$@ $(CSFILES)

Out.txt: main.exe
	mono $< < data.txt > $@
	cat $@ 

main.exe: main.cs matrix.dll minimum.cs
	mcs $(CSFILES) $(DLLS)

matrix.dll: $(MATRIXLIB)/matrix.cs $(MATRIXLIB)/vector.cs
	$(MAKEDLL)

clean:
	rm -r *.dll *.txt *.exe
