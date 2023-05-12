using System;
using static System.Math;


class testfs {
  public static double func1(vector x) {
    return x.dot(x);
  }

  public static double func2(vector x) {
    vector offset = new vector(5.0, 5.0);
    return (x+offset).dot(x+offset);
  }
}

class test {
  static double epsilon = 0.001;
  static bool test_finite_differences() {
    bool test1 = minimum.finite_difference(testfs.func1, new vector(2), 1) < epsilon;
    bool test2 = minimum.finite_difference(testfs.func2, new vector(-5.0, -5.0), 1) < epsilon;
    return test1 && test2;
  }

  static bool test_multivar_finite_diff() {

  }

  public static void test_minimum_methods() {
    Console.WriteLine($"Finite differences {test_finite_differences()}");
    Console.WriteLine($"Finite differences {test_finite_differences()}");
  }
}
