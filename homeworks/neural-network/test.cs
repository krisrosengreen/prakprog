using System;
using static System.Math;

class tester {
  public static double g(double x) {
    return Cos(5.0*x - 1.0)*Exp(-x*x);
  }

  public static (vector, vector) get_training_data(int num_points, Func<double, double> f) {
    Random rand = new Random();

    double[] xarr = new double[num_points];
    double[] yarr = new double[num_points];

    for (int i = 0; i < num_points; i++) {
      double rx = rand.NextDouble()*2 - 1;
      xarr[i] = rx;
      yarr[i] = f(rx);
    }

    return (new vector(xarr), new vector(yarr));
  }

  static (vector, vector) test_func(int size) {
    vector xs = new vector(size);
    vector ys = new vector(size);

    for (int i = 0; i < size; i++) {
      xs[i] = (double) i / 2.0; 
      ys[i] = Cos(xs[i]); 
    }

    return (xs, ys);
  }

  public static void test() {
    ann nn = new ann(100);

    (vector xs, vector ys) = get_training_data(300, g);

    Console.WriteLine($"# Cost before train: {nn.cost(xs, ys)}");

    nn.train(xs, ys);
    
    Console.WriteLine($"# Cost after train: {nn.cost(xs, ys)}");

    Console.WriteLine("Actual");
    for (int i = 0; i < xs.size; i++) {
      Console.WriteLine($"{xs[i]},{ys[i]}");
    }

    Console.WriteLine("\n\nNeural net");
    for (int i = 0; i < xs.size; i++) {
      Console.WriteLine($"{xs[i]},{nn.response(xs[i])}");
    }
  } 
}
