MATLIB = ../../matlib
MATRIXLIB = $(MATLIB)/matrix
DLLS = $(addprefix -r:,$(filter %.dll,$^))
CSFILES = $(filter %.cs,$^)
MAKEDLL = mcs -t:library -out:$@ $(CSFILES)

all: OutA.txt OutB.txt higgs.png

higgs.png: higgs.gnu OutB.txt
	gnuplot higgs.gnu

OutA.txt: main.exe
	mono $< A < data.txt > $@
	cat $@ 

OutB.txt: main.exe
	mono $< B < data.txt > $@
	cat $@ 

main.exe: main.cs matrix.dll minimum.cs
	mcs $(CSFILES) $(DLLS)

matrix.dll: $(MATRIXLIB)/matrix.cs $(MATRIXLIB)/vector.cs
	$(MAKEDLL)

clean:
	rm -r *.dll *.txt *.exe
