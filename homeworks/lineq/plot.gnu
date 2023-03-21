set terminal svg enhanced background rgb 'white'
set term svg
set output 'img.png'

b=-100
a=0.01
c=0

set fit nolog results

f(x) = a*(x+b)**3 + c
fit f(x) 'out.times.data' via a, b, c
plot 'out.times.data' with lines, f(x)
