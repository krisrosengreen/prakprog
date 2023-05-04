set datafile separator comma
set terminal png size 1024,768
set term png
set output "output.png"

plot "Out.txt" using 0:1 w l, "Out.txt" using 2:3 w l, "Out.txt" using 4:5 w l
