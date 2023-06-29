set datafile separator comma
set key autotitle columnheader
set terminal png size 1024,768
set term png
set output "wavefuncs.png"
set key

set ylabel "r"
set xlabel "R"

plot "Out.txt" using 1:2 index 2 w l, "Out.txt" using 1:2 index 3 w l, "Out.txt" using 1:2 index 4 w l, "Out.txt" using 1:2 index 5 w l
