MATLIB = ../../matlib
MATRIXLIB = $(MATLIB)/matrix
DLLS = $(addprefix -r:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
CSFILES = $(filter %.cs,$(shell ls))
MAKEDLL = mcs -t:library -out:$@ $(CODE)

Out.txt: main.exe
	mono $< > $@
	cat $@ 

main.exe: matrix.dll qrgs.dll $(CSFILES)
	mcs $(CODE) $(DLLS)

matrix.dll: $(MATRIXLIB)/matrix.cs $(MATRIXLIB)/vector.cs
	$(MAKEDLL)

qrgs.dll: ../lineq/QRGS.cs matrix.dll
	$(MAKEDLL) $(DLLS)

clean:
	rm -r *.dll *.txt *.exe
