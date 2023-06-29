set datafile separator comma
set key autotitle columnheader
set terminal png size 1024,768
set term png
set output "output.png"
set key
set yrange [-100:100]

plot "OutB.txt" using 1:2 index 0 w points, "Out.txt" using 1:2 index 1 w points, "Out.txt" using 1:2 index 2 w points, "Out.txt" using 1:2 index 3 w points, "Out.txt" using 1:2 index 4 w points

