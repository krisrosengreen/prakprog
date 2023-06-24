using System;
using static System.Math;


class integrals {
  public static double box(vector x) {
    return 5.0;
  }

  public static double partA3integral(vector v) {
    double x = v[0];
    double y = v[1];
    double z = v[2];

    return Pow(1.0-Cos(x)*Cos(y)*Cos(z), -1)/(PI*PI*PI);
  }

  public static double partA2integral(vector v) {  // Unit circle
    return 2*PI*v.norm(); 
  }
}


class exercises {
  public static void partA() {
    (double area, double error) = montecarlo.plainmc(integrals.partA3integral, new vector(0.0, 0.0, 0.0), new vector(PI, PI, PI), 10000000);

    Console.WriteLine($"PartA3: Area {area} error {error}");

    (area, error) = montecarlo.plainmc(integrals.partA2integral, new vector(0.0), new vector(1.0), 100000);

    Console.WriteLine($"PartA2: Area {area} error {error}");
  }

  public static void partB() {

  }
}


class main {
  public static void Main() {
    // exercises.partA();
    for (int i = 0; i < 10; i++) {
      vector resp = montecarlo.halton(i,10);
      resp.print();
    }
  }
}
