using System;
using static System.Math;


class integrals {
  public static double sqrt(double x) {
    return Sqrt(x);
  }

  public static double invsqrt(double x) {
    return 1.0/Sqrt(x);
  }

  public static double int3(double x) {
    return 4*Sqrt(1 - x*x);
  }

  public static double int4(double x) {
    return Log(x)/Sqrt(x);
  }
}


public class main {

  public static void calculate_print(Func<double, double> f, double a, double b) {
    double val = integration.integrate(f, a, b);
    double err = integration.erf(val);
    Console.WriteLine($"Value: {val} Error {err}\n");
  }

  static void partA() {
    Func<double, double>[] ints = {integrals.sqrt, integrals.invsqrt, integrals.int3, integrals.int4};

    foreach (Func<double, double> func in ints) {
      calculate_print(func, 0, 1);   
    }
  }

  public static void Main() {
    partA();
  }
}
