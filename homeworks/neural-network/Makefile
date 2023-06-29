MATLIB = ../../matlib
MATRIXLIB = $(MATLIB)/matrix
MATRIXCODE = $(MATRIXLIB)/vector.cs $(MATRIXLIB)/matrix.cs
DLLS = $(addprefix -r:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
CSFILES = $(filter %.cs,$(shell ls))
MAKEDLL = mcs -t:library -out:$@ $(CODE)

all: OutA.txt OutB.txt output.png

output.png: Out.txt
	gnuplot plot.gnu

OutA.txt: main.exe
	mono $< A > $@
	cat $@ 

OutB.txt: main.exe
	mono $< B > $@
	cat $@ 

main.exe: matrix.dll integrate.dll $(CSFILES)
	mcs -out:main.exe $(CODE) $(DLLS)

matrix.dll: $(MATRIXCODE)
	$(MAKEDLL)

integrate.dll: ../quadratures/integration.cs
	$(MAKEDLL)

clean:
	rm -r *.dll *.txt *.exe
