MATLIB = ../../matlib
MATRIXLIB = $(MATLIB)/matrix
MATRIXCODE = $(MATRIXLIB)/vector.cs $(MATRIXLIB)/matrix.cs
DLLS = $(addprefix -r:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
CSFILES = $(filter %.cs,$(shell ls))
MAKEDLL = mcs -t:library -out:$@ $(CODE)

all: plots.png Out.txt

plots.png: Out.txt
	gnuplot plot.gnu

Out.txt: main.exe
	mono $< < sin.txt > $@
	cat $@ 

main.exe: matrix.dll pipe.dll $(CSFILES)
	mcs -out:main.exe $(CODE) $(DLLS)

matrix.dll: $(MATRIXCODE)
	$(MAKEDLL)

pipe.dll: ../../tools/pipe.cs
	mcs -t:library $< -out:$@ 

clean:
	rm -r *.dll *.exe
