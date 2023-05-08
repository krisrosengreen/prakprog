using System;
using static System.Math;


class functions {
  public static vector rosenbrockvalley(vector v) {
    double x = v[0];
    double y = v[1];

    double fpx = -2.0*(1.0-x) + 100.0*2.0*(y-x*x)*(-2*x);
    double fpy = 100.0*2*(y-x*x);
    return new vector(fpx, fpy, 0.0);
  }
}


public class main {

  public static void Main() {
    vector x0 = new vector(30.0, 30.0);

    vector root = rootfinder.newton(functions.rosenbrockvalley, x0, 0.00001);

    Console.WriteLine("Result");
    root.print();
    functions.rosenbrockvalley(root).print();
  }

}
