# Exam in practical programming and numerical methods

My student number is 202009792, therefore I have to do exercise 14 (92 % 26 = 14). This exercise is "Cholesky decomposition of a real symmetric positive-definite matrix".

# Implementation

I implemented the Cholesky algorithm to find the upper triangular matrix L of a real symmetric positive-definite matrix. From this, I implemented a linear equation solver. This was done by solving the equation Ly=b and then L^Tx=y. This then gives the vector x which solves the equation Ax=b.

Once the linear equation solver was implemented, I wrote a function that calculates the inverse matrix. The columns of the inverse matrix are determined by solving the matrix A to each column of the inverse matrix.
