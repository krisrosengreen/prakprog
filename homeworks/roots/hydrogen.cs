using System;
using static System.Math;

public class hydrogen {
  public static double rwf(double r, double a0) {
    return 2*Pow(a0, -3.0/2.0)*Exp(-r/a0);
  }
}
