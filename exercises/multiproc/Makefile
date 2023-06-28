N=1e8
TIME = time --portability --append --output $@

Out.txt: main.exe
	$(TIME) mono $< A -terms:$N -threads:1 >>$@
	$(TIME) mono $< A -terms:$N -threads:2 >>$@
	$(TIME) mono $< A -terms:$N -threads:3 >>$@
	$(TIME) mono $< A -terms:$N -threads:4 >>$@
	$(TIME) mono $< B -terms:$N >>$@
	cat Out.txt

main.exe: main.cs
	mcs main.cs
