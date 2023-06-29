set datafile separator comma
set key autotitle columnheader
set terminal png size 1024,768
set term png
set output "plotsA.png"
set key

set multiplot layout 2,1

plot "OutA.txt" using 1:2 index 0 w l

plot "OutA.txt" using 1:2 index 1 w l
