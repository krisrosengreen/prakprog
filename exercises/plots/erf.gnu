set datafile separator comma
set key autotitle columnheader
set terminal png size 1024,768
set term png
set output "erf.png"
set key

plot "Out.txt" using 1:2 index 0 w points, "Out.txt" using 1:2 index 1 w l
