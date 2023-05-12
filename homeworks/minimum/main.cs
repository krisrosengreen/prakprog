using System;
using static System.Math;


class funcs {
  public static double simple(vector v) {
    return v.dot(v);
  }

  public static double simple2(vector v) {
    vector offset = new vector(5.0, 5.0);
    return (v+offset).dot(v+offset);
  }

  public static double rosenbrock(vector v) {
    double x = v[0];
    double y = v[1];

    return Pow(1-x, 2) + 100*Pow(y-x*x, 2);
  }

  public static double himmelblau(vector v) {
    double x = v[0];
    double y = v[1];

    return Pow(x*x + y - 11, 2) + Pow(x+y*y-7, 2);
  }
}

public class main {
  public static void Main() {
    vector xmin = minimum.qnewton(funcs.rosenbrock, new vector(-3.0, -3.0), 0.001);
    xmin.print();
    Console.WriteLine($"{funcs.rosenbrock(xmin)}");

    xmin= minimum.qnewton(funcs.himmelblau, new vector(2), 0.001);
    xmin.print();
    Console.WriteLine($"{funcs.himmelblau(xmin)}");
  }
}
