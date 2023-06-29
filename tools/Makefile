DLLS = $(addprefix -r:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
CSFILES = $(filter %.cs,$(shell ls))
MAKEDLL = mcs -t:library -out:$@ $(CODE)

all: Out.txt pipe.dll

pipe.dll: pipe.cs
	mcs -t:library $@ 

Out.txt: main.exe
	mono $< < sin.txt > $@
	cat $@ 

main.exe: $(CSFILES)
	mcs -out:main.exe $(CODE) $(DLLS)

clean:
	rm -r *.dll *.txt *.exe
