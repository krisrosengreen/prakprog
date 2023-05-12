using System;
using static System.Math;


public class minimum {
  public static double finite_difference(Func<vector, double> f, vector x, int i) {
    double finite_step = 1e-8;

    vector dx = new vector(x.size);
    dx[i] = finite_step;
    return (f(x + dx) - f(x))/finite_step;
  }

  public static double multivar_finite_diff(Func<vector, double> f, vector x, int i, int j) {
    // Using: https://en.wikipedia.org/wiki/Finite_difference
    double h = 1e-8;
    double k = 1e-8;

    int size = x.size;
    vector pp = new vector(new double[size]);
    pp[i] = h; pp[j] = k;
    vector pm = new vector(new double[size]);
    pm[i] = h; pm[j] = -k;
    vector mp = new vector(new double[size]);
    mp[i] = -h; mp[j] = k;
    vector mm = new vector(new double[size]);
    mm[i] = -h; mm[j] = -k;

    return (f(x+pp) - f(x+pm) - f(x+mp) + f(x+mm))/(4*h*k);
  }

  public static matrix numerical_hessian(Func<vector, double> f, vector point) {
    matrix H = new matrix(point.size, point.size);

    for (int i = 0; i < point.size; i++) {
      for (int j = 0; j < point.size; j++) {
        H[i,j] = multivar_finite_diff(f, point, i, j);
      }
    }

    return H;
  }

  public static vector gradient(Func<vector, double> f, vector point) {
    vector grad = new vector(point.size);

    for (int i = 0; i < point.size; i++) {
      grad[i] = finite_difference(f, point, i);
    }

    return grad;
  }

  public static vector qnewton(
    Func<vector, double> f,
    vector start,
    double acc
  ) {
    matrix B  = new matrix(start.size, start.size);
    B.set_identity();

    vector x = start.copy();

    while (gradient(f, x).norm() > acc) {
      vector dx = -B*gradient(f, x);
      
      double lambda = 1;
      while (true) {//Line search
        if (f(x + lambda*dx) < f(x)) {
          x += lambda*dx;
          // UPDATE HESSIAN. Calculate delta B
          vector s = lambda*dx;
          vector y = gradient(f, x+s) - gradient(f, x);
          vector u = s - B*y;
          matrix dB = matrix.outer(u / (s.dot(y)), s);
          B += dB;

          break;
        }
        lambda /= 2.0;

        if (lambda < 1.0/1024.0){
          x += lambda*dx;
          B.set_identity();
          break;
        }
      }
    }

    return x;
  }
}
