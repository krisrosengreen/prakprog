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

  public static double intB1(double x) {
    return 1.0/Sqrt(x);
  }

  public static double intB2(double x) {
    return Log(x)/Sqrt(x);
  }
}

class exercises {
  static void calculate_print(Func<double, double> f, double a, double b) {
    double val = integration.integrate(f, a, b);
    double err = integration.erf(val);
    Console.WriteLine($"# Value: {val} Error {err}");
  }


  public static void partA() {
    Func<double, double>[] ints = {integrals.sqrt, integrals.invsqrt, integrals.int3, integrals.int4};

    Console.WriteLine("# Funcs in order sqrt, invsqrt, int3 int4");
    foreach (Func<double, double> func in ints) {
      calculate_print(func, 0, 1);   
    }

    Console.WriteLine("Erf");
    double min = -5;
    double max = 5;
    int count = 40;
    double step = (max - min)/(double) count;

    for (int i = 0; i < count; i++) {
      double x = min + i*step;
      Console.WriteLine($"{x},{integration.erf(x)}");
    }

    (var xerr, var yerr, var _) = pipe.threecols();

    Console.WriteLine("\n\nTable");
    for (int i = 0; i < xerr.size; i++) {
      Console.WriteLine($"{xerr[i]},{yerr[i]}");
    }

    Console.WriteLine("\n\nDifference");
    for (int i = 0; i < xerr.size; i++) {
      Console.WriteLine($"{xerr[i]},{yerr[i] - integration.erf(xerr[i])}");
    }
  }


  public static void partB() {
    Func<double,double>[] integs = {integrals.intB1, integrals.intB2};

    foreach(Func<double, double> func in integs) {
      double val = integration.clenshawcurtis(func, 0, 1);
      double reg = integration.integrate(func, 0, 1);

      Console.WriteLine($"{val} {reg}");
    }
  }
}


public class main {
  public static void Main(string[] args) {
    if (args.Length > 0) {
      if (args[0] == "A") exercises.partA();
      else if (args[0] == "B") exercises.partB();
    }
  }
}
