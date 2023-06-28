set datafile separator comma
set key autotitle columnheader
set terminal png size 1024,768
set term png
set output "gamma.png"
set key

set yrange [-5:5]

plot "Out.txt" using 1:2 index 2 w l, "Out.txt" using 1:2 index 3 w points

