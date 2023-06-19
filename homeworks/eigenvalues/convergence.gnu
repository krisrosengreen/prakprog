set datafile separator comma
set key autotitle columnheader
set terminal png size 1024,768
set term png
set output "convergence.png"
set key

set multiplot layout 1,2

set ylabel "ε_0"
set xlabel "Δr"

plot "Out.txt" using 1:2 index 0 w l

set xlabel "r_{max}"

plot "Out.txt" using 1:2 index 1 w l
