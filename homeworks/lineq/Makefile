MATLIB = ~/prakprog/matlib
MATRIXDIR = $(MATLIB)/matrix
DLLS = $(addprefix -r:,$(filter %.dll,$^))
MAKEDLL = mcs -t:library -out:$@ $(filter %.cs,$^)

Out.txt: main.exe
	mono main.exe < inputfile > Out.txt
	cat Out.txt

out.times.data: main.exe
	>$@
	for N in $$(seq 100 20 200); do \
		time --format "$$N %e" --output $@ --append \
		mono $< -size:$$N 1>out 2>err ;\
	done

main.exe: main.cs matrix.dll QRGS.dll
	mcs main.cs $(DLLS)

QRGS.dll: QRGS.cs matrix.dll
	$(MAKEDLL) $(DLLS)

matrix.dll: $(MATRIXDIR)/matrix.cs $(MATRIXDIR)/vector.cs
	$(MAKEDLL) 

clean:
	rm -r *.dll *.txt *.exe
