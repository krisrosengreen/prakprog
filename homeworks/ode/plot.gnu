set datafile separator comma
set terminal png size 1024,768
set term png
set output "output.png"

plot "Out.txt" using 1:2 w l, "Out.txt" using 1:3 w l
