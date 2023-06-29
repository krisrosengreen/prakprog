MATLIB = ../../matlib
MATRIXLIB = $(MATLIB)/matrix
DLLS = $(addprefix -r:,$(filter %.dll,$^))
CSFILES = $(filter %.cs,$^)
MAKEDLL = mcs -t:library -out:$@ $(CSFILES)

all: OutA.txt OutB.txt erf.png

erf.png: OutA.txt erf.gnu
	gnuplot erf.gnu

OutA.txt: main.exe
	mono $< A < erf > $@
	cat $@ 

OutB.txt: main.exe
	mono $< B > $@
	cat $@ 

pipe.dll: ../../tools/pipe.cs
	mcs $< -t:library -out:$@

main.exe: main.cs matrix.dll pipe.dll integration.cs
	mcs $(CSFILES) $(DLLS)

matrix.dll: $(MATRIXLIB)/matrix.cs $(MATRIXLIB)/vector.cs
	$(MAKEDLL)

clean:
	rm -r *.dll *.txt *.exe
