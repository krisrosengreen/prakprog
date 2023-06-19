set datafile separator comma
set key autotitle columnheader
set terminal png size 1024,768
set term png
set output "output.png"
set key

set multiplot layout 1,2

plot "Out.txt" using 1:2 index 0 w l

plot "Out.txt" using 1:2 index 1 w l
