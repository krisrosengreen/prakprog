MATLIB = ../../matlib
MATRIXLIB = $(MATLIB)/matrix
MATRIXCODE = $(MATRIXLIB)/vector.cs $(MATRIXLIB)/matrix.cs
DLLS = $(addprefix -r:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
CSFILES = $(filter %.cs,$(shell ls))
MAKEDLL = mcs -t:library -out:$@ $(CODE)

all: Test.txt wavefuncs.png convergence.png Out.txt OutA.txt

# Images

convergence.png: Out.txt convergence.gnu
	gnuplot convergence.gnu

wavefuncs.png: Out.txt wavefuncs.gnu
	gnuplot wavefuncs.gnu

# Text output

Test.txt: main.exe
	mono $< test > $@
	cat Test.txt

Out.txt: main.exe
	mono $< B > $@
	cat $@ 

OutA.txt: main.exe
	mono $< A > $@
	cat $@

main.exe: matrix.dll $(CSFILES)
	mcs -out:main.exe $(CODE) $(DLLS)

matrix.dll: $(MATRIXCODE)
	$(MAKEDLL)

clean:
	rm -r *.dll *.txt *.exe
