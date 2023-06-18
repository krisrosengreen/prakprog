using System;
using static System.Random;
using static System.Math;


// Cholinsky implementation
public class CC {
  // Contructs random real symmetric positive-definite matrix
  public static matrix construct_matrix(int size) {
    matrix Q = new matrix(size, size);
    matrix D = new matrix(size, size);

    // A = Q^T D Q
    Random rand = new Random();
    for (int i = 0; i < size; i++) {
      D[i, i] = rand.NextDouble();
      for (int j = 0; j <= i; j++) {
        Q[i, j] = rand.NextDouble();
        Q[j, i] = rand.NextDouble();
      }
    }

    matrix A = Q.T*D*Q;

    return A;
  }

  public static matrix decomp(matrix A) {
    matrix L = new matrix(A.size1, A.size1);

    for (int i = 0; i < A.size1; i++) {
      for (int j = 0; j <= i; j++) {
        double sum = 0;
        for (int k = 0; k < j; k++)
          sum += L[i, k] * L[j, k];

        if (i == j)
          L[i, j] = Sqrt(A[i, i] - sum);
        else
          L[i, j] = (1.0 / L[j, j] * (A[i, j] - sum));
      }
    }

    return L;
  }

  public static void underline_print(string msg) {
    Console.WriteLine(msg);
    Console.WriteLine(new String('*', msg.Length));
  }

  public static vector solve(matrix A, vector b) {
    // Solve Ly = b
    matrix L = decomp(A);

    // Forward substitution
    vector y = new vector(b.size);
    for (int i = 0; i < L.size1; i++) {
      double sum = 0;
      for (int j = 0; j < i; j++) {
        sum += L[i, j]*y[j];
      }

      y[i] = (1.0 / L[i, i]) * (b[i] - sum);
    }

    // Back substitution
    vector x = new vector(b.size);
    for (int i = L.size1 - 1; i >= 0; i--) {
      double sum = 0;
      for (int j = i + 1; j < L.size1; j++) {
        sum += L[j, i]*x[j];
      }

      x[i] = (1.0 / L[i, i]) * (y[i] - sum);
    }

    return x;
  }

  public static matrix inverse(matrix A) {
    matrix I = new matrix(A.size1, A.size1);
    I.setid();

    for (int i = 0; i < I.size1; i++) {
      vector col = solve(A, I[i]);
      I[i] = col;
    }

    return I;
  }

  public static double determinant(matrix A) {
    matrix L = decomp(A);

    double det = 1;

    for (int i = 0; i < L.size1; i++) {
      det *= L[i,i]*L[i,i];
    }

    return det;
  }
}
