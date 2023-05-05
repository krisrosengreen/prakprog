set datafile separator comma
set term png
set output "plot.png"
plot "Out.txt" using 1:2 w l
