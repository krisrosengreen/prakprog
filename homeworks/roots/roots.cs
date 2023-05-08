using System;
using static System.Math;


public class rootfinder {
  static vector finite_differences(Func<vector, vector> f, vector x, int k, double epsilon) {
    double dx = epsilon;
    vector xdx = x.copy();
    xdx[k] += dx;
    vector gradient = 1.0/dx * (f(xdx) - f(x));

    return gradient;
  }

  static matrix jacobi(Func<vector, vector> f, vector x, double epsilon) {
    vector fv = f(x);

    matrix jacobi = new matrix(fv.size, x.size);

    for (int i = 0; i < fv.size; i++) {
      for (int j = 0; j < x.size; j++) {
        jacobi[i, j] = finite_differences(f, x, j, epsilon)[i];
      } 
    }

    return jacobi;
  }

  static vector newton_stepdx(Func<vector, vector> f, vector x, double epsilon) {
    matrix jacobian = jacobi(f, x, epsilon);

    QRGS qrgs = new QRGS(jacobian);
    vector fv = f(x);

    vector dx = qrgs.solve(-fv);

    return dx;
  }

  public static vector newton(Func<vector, vector> f, vector x, double epsilon=1e-2) {
    while (f(x).norm() > epsilon) {
      vector dx = newton_stepdx(f, x, epsilon);

      double lambda = 1.0;

      while (f(x+lambda*dx).norm() > (1.0-lambda/2.0) * f(x).norm() && lambda > 1.0/1024.0) {
        lambda /= 2.0;
      }

      x += lambda*dx;
    }

    return x;
  }
}
