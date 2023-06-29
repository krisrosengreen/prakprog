MATLIB = ../../matlib
MATRIXLIB = $(MATLIB)/matrix
MATRIXCODE = $(MATRIXLIB)/vector.cs $(MATRIXLIB)/matrix.cs
DLLS = $(addprefix -r:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
CSFILES = $(filter %.cs,$(shell ls))
MAKEDLL = mcs -t:library -out:$@ $(CODE)

all: OutA.txt OutB.txt plotsA.png plotsB.png

plotsA.png: OutA.txt plotA.gnu
	gnuplot plotA.gnu

plotsB.png: OutB.txt plotB.gnu
	gnuplot plotB.gnu

OutA.txt: main.exe
	mono $< A > $@
	cat $@ 

OutB.txt: main.exe
	mono $< B > $@
	cat $@ 

OutC.txt: main.exe
	mono $< C > $@
	cat $@ 

main.exe: matrix.dll $(CSFILES)
	mcs -out:main.exe $(CODE) $(DLLS)

matrix.dll: $(MATRIXCODE)
	$(MAKEDLL)

clean:
	rm -r *.dll *.txt *.exe
