MATLIB = ../matlib
MATRIXLIB = $(MATLIB)/matrix
MATRIXCODE = $(MATRIXLIB)/vector.cs $(MATRIXLIB)/matrix.cs
DLLS = $(addprefix -r:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
CSFILES = $(filter %.cs,$(shell ls))
MAKEDLL = mcs -t:library -out:$@ $(CODE)

all: Out.txt Test.txt Time.txt timesolve.png

timesolve.png: Time.txt plot.gnu
	gnuplot plot.gnu

Out.txt: main.exe
	mono $< > $@
	cat $@ 

Test.txt: main.exe
	mono $< test > $@
	cat $@ 

Time.txt: main.exe
	>$@
	for N in $$(seq 100 20 900); do \
		time --format "$$N,%e" --output Time.txt --append \
		mono main.exe time $$N 1>out 2>err ;\
	done

main.exe: matrix.dll $(CSFILES)
	mcs -out:main.exe $(CODE) $(DLLS)

matrix.dll: $(MATRIXCODE)
	$(MAKEDLL)

clean:
	rm -r *.dll *.txt *.exe
