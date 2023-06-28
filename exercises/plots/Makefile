all: erf.png gamma.png lngamma.png

erf.png: erf.gnu Out.txt
	gnuplot erf.gnu

gamma.png: gamma.gnu Out.txt
	gnuplot gamma.gnu

lngamma.png: lngamma.gnu Out.txt
	gnuplot lngamma.gnu

Out.txt: main.exe erf.txt
	mono main.exe < erf.txt > Out.txt
	cat Out.txt

main.exe: main.cs pipe.dll
	mcs -r:pipe.dll -out:$@ $<

pipe.dll: ../../tools/pipe.cs
	mcs -t:library -out:$@ $< 

clear:
	rm main.exe
	rm Out.txt
