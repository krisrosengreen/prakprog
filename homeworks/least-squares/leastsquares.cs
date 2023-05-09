using System;
using static System.Math;


public class leastsquares {
  // This returns the covariance matrix and best-fit c vector.
  public static (matrix, vector) lsfit(Func<double, double>[] fs, vector x, vector y, vector dy) {
    
    int size1 = x.size;
    int size2 = fs.Length;
    matrix A = new matrix(size1, size2);
    vector b = new vector(size1);

    for (int i = 0; i < size1; i++) {
      b[i] = y[i]/dy[i];
      for (int k = 0; k < size2; k++) {
        A[i, k] = fs[k](x[i]) / dy[i];
      }
    }

    QRGS qrgs = new QRGS(A);
    vector c = qrgs.solve(b);

    matrix ATA = (A.T * A);
    ATA.print();
    matrix cov = (new QRGS(A)).inverse();

    return (cov,c);
  }

  public static vector fit_errors(matrix covariance) {
    vector errs = new vector(covariance.size1);

    for (int i = 0; i < covariance.size1; i++) {
      errs[i] = Sqrt(covariance[i, i]);
    }

    return errs;
  }
}
