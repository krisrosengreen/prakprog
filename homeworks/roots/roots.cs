using System;
using static System.Math;


public class rootfinder {
  public static vector newton(Func<vector, vector> f, vector x, double epsilon=1e-2, double max_iterations=10000) {

    while (f(x).norm() > epsilon && max_iterations > 0) {
      vector dx = newton_stepdx(f, x);

      double lambda = 1.0;

      while (f(x+lambda*dx).norm() > (1.0-lambda/2.0) * f(x).norm() && lambda > 1.0/1024.0) {
        lambda /= 2.0;
      }

      x += lambda*dx;

      max_iterations -= 1;
    }

    // Throw exception if max_iteration = 0;
    if (max_iterations == 0) {
      throw new System.ArgumentException("Maximum number of iterations reached");
    }

    return x;
  }


  static vector finite_differences(Func<vector, vector> f, vector x, int k) {
    double dx = 1e-15;
    vector xdx = x.copy();
    xdx[k] += dx;
    vector gradient = (f(xdx) - f(x)) / dx;

    return gradient;
  }


  static matrix jacobi(Func<vector, vector> f, vector x) {
    vector fv = f(x);

    matrix jacobi = new matrix(fv.size, x.size);

    for (int i = 0; i < fv.size; i++) {
      jacobi[i] = finite_differences(f, x, i);
    }

    return jacobi;
  }


  static vector newton_stepdx(Func<vector, vector> f, vector x) {
    matrix jacobian = jacobi(f, x);

    QRGS qrgs = new QRGS(jacobian);
    vector fv = f(x);

    vector dx = qrgs.solve(-fv);

    return dx;
  }
}
