set datafile separator comma
set terminal png size 1024,768
set term png
set output "output.png"
set key

plot "Out.txt" using 0:1 w l title "f", "Out.txt" using 0:2 w l title "f'", "Out.txt" using 0:3 w l title "f''", "Out.txt" using 0:4 w l title "F", "Out.txt" using 0:5 w l title "f actual"
