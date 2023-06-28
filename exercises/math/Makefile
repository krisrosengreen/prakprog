Out.txt: main.exe sfuns.dll
	mono main.exe > Out.txt
	cat Out.txt

sfuns.dll: sfuns.cs
	mcs -t:library $<

main.exe: main.cs sfuns.dll
	mcs -r:sfuns.dll $<
