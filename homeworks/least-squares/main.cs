using System;
using static System.Math;


class data {
  public static (Func<double, double>[], vector, vector, vector) radioactive_decay() {
    vector t = new vector(new double[]{1.0, 2.0, 3.0, 4.0, 6.0, 9.0, 10.0, 13.0, 15.0});
    vector y = new vector(new double[]{117.0, 100.0, 88.0, 72.0, 53.0, 29.5, 25.2, 15.2, 11.1});
    vector dy = new vector(new double[]{5,5,5,4,4,3,3,2,2});

    for (int i = 0; i < y.size; i++) {
      dy[i] = dy[i]/y[i];
      y[i] = Log(y[i]);
    }

    //  Two funcs below:
    //    1) Corresponds to -lambda*t (Where lambda is determined by c)
    //    2) Corresponds to constant ln(a) this will be determined by c
    Func<double, double>[] funcs = {x => -x, _ => 1.0};

    return (funcs,t,y,dy);
  }
}

public class main {
  static void partA() {
    (Func<double,double>[]funcs,vector x, vector y, vector dy) = data.radioactive_decay();

    (matrix cov, vector fit) = leastsquares.lsfit(funcs,x,y,dy);
    fit.print();

    // Table value of half-life of Ra224 is 3.631 days
    Console.WriteLine($"Half-life: {Log(2.0)/fit[0]}");
  }

  static void partB() {
    (Func<double,double>[]funcs,vector x, vector y, vector dy) = data.radioactive_decay();

    (matrix cov, vector fit) = leastsquares.lsfit(funcs,x,y,dy);
    fit.print();

    // Table value of half-life of Ra224 is 3.631 days
    Console.WriteLine($"Half-life: {Log(2.0)/fit[0]}");
    vector errors = leastsquares.fit_errors(cov);
    Console.WriteLine("Errors:");
    errors.print();

    // The table value lies within the uncertainty of the fitted values
    Console.WriteLine($"Half-life fit upper: {Log(2.0)/(fit[0] + errors[0])}");
    Console.WriteLine($"Half-life fit lower: {Log(2.0)/(fit[0] - errors[0])}");
  }

  public static void Main() {
    partB();
  }
}
