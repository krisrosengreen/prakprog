using System;
using static System.Math;

class integration {
  public static double erf(double z) {
    if (z < 0) {
      return -erf(-z);
    } else if (0 <= z && z <= 1) {
      return 2.0/Sqrt(PI) * integrate(x => Exp(-x*x), 0.0, z);
    } else {
      return 1.0-2.0/Sqrt(PI) * integrate(t => Exp(-Pow(z+(1-t)/t, 2)/t /t), 0.0, 1.0);
    }
  }

  public static double integrate(
      Func<double, double> f,
      double a,
      double b,
      double delta=0.001,
      double epsilon=0.001,
      double f2=double.NaN,
      double f3=double.NaN) {
    double h=b-a;
    if (Double.IsNaN(f2)) {
      f2=f(a+2*h/6);
      f3=f(a+4*h/6);
    }
    double f1=f(a+h/6);
    double f4=f(a+5*h/6);
    double Q = (2*f1+f2+f3+2*f4)/6*(b-a);
    double q = (f1+f2+f3+f4)/4*(b-a);
    double err = Abs(Q-q);
    if (err <= delta + epsilon*Abs(Q)) return Q;
    else {
      double val = integrate(f,a,(a+b)/2,delta/Sqrt(2),epsilon,f1,f2) + integrate(f,(a+b)/2,b,delta/Sqrt(2),epsilon,f3,f4);

      return val;
    }
  }


  public static double clenshawcurtis(
      Func<double, double> f,
      double a,
      double b,
      double delta=0.001,
      double epsilon=0.001,
      double f2=double.NaN,
      double f3=double.NaN
      ) {

    Func<double, double> var_transform = x => f((a+b)/2.0 + (b-a)/2.0 *Cos(x)) * Sin(x)*(b-a)/2;

    return integrate(var_transform, 0, PI, delta, epsilon, f2, f3);
  }
}
