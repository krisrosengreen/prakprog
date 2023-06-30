set datafile separator comma
set terminal png size 1024,768
set term png
set output "timesolve.png"

set xlabel "Matrix size"
set ylabel "Time [s]"

plot "Time.txt" using 1:2 w l

