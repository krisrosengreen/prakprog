using System;
using static System.Math;


public class hydrogen {
  public static vector hydrogen_difeq(double t, vector y, double E) {
    double f = y[0];
    double fp = y[1];
    double fpp = -2.0*E*f - 2.0/t * f;

    return new vector(fp, fpp);
  }


  public static vector M(vector x, double rmin, double rmax) {
    double E = x[0];

    genlist<double> t = new genlist<double>();
    genlist<vector> a = new genlist<vector>();

    Func<double, vector, vector> E_H_difeq = (tt,y) => hydrogen_difeq(tt, y, E);
    ode.driver(E_H_difeq, rmin, new vector(rmin - rmin*rmin, 1-2*rmin), rmax, xlist: t, ylist: a);
    
    // Get the last element in the list, which corresponds to f(r) which should give 0 to solve for E
    double val = a[a.size - 1][0];

    return new vector(val);
  }


  public static double find_hydrogen_root() {
    double rmin = 0.00001;
    double rmax = 8.0;

    Func<vector, vector> M_r0f = x => M(x, rmin, rmax);

    vector x0 = rootfinder.newton(M_r0f, new vector(1));

    genlist<double> t = new genlist<double>();
    genlist<vector> a = new genlist<vector>();

    Func<double, vector, vector> E_H_difeq = (tt,y) => hydrogen_difeq(tt, y, x0[0]);

    ode.driver(E_H_difeq, rmin, new vector(rmin - rmin*rmin, 1-2*rmin), rmax, xlist: t, ylist: a);

    Console.WriteLine("'Solution'");
    for (int i = 0; i < t.size; i++) {
      Console.WriteLine($"{t[i]} {a[i][0]} {a[i][1]}");
    }

    return x0[0];
  }
}
