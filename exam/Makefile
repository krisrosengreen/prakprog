MATLIB = ../matlib
MATRIXLIB = $(MATLIB)/matrix
MATRIXCODE = $(MATRIXLIB)/vector.cs $(MATRIXLIB)/matrix.cs
DLLS = $(addprefix -r:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
CSFILES = $(filter %.cs,$(shell ls))
MAKEDLL = mcs -t:library -out:$@ $(CODE)

Out.txt: main.exe
	mono $< > $@
	cat $@ 

main.exe: matrix.dll $(CSFILES)
	mcs -out:main.exe $(CODE) $(DLLS)

matrix.dll: $(MATRIXCODE)
	$(MAKEDLL)

clean:
	rm -r *.dll *.txt *.exe
