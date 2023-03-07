Out.txt: main.exe sfuns.dll
	mono $^ > Out.txt
	cat Out.txt

sfuns.dll: sfuns.cs
	mcs -t:library $^

main.exe: main.cs
	mcs -r:sfuns.dll $^
