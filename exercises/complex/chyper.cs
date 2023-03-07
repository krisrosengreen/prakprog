using System;
using static System.Math;

public static partial class cmath {

  public static double sinh(double i) {
    return (exp(i) - exp(-i))/2;
  }

  public static double cosh(double i) {
    return (exp(i) + exp(-i))/2;
  }
}
