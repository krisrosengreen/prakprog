using System;
using static System.Math; 
using static System.Random;


class data {
  public static double g(double x) {
    return Cos(5*x - 1)*Exp(-x*x);
  }

  public static (vector, vector) get_training_data(int num_points, Func<double, double> f) {
    Random rand = new Random();

    double[] xarr = new double[num_points];
    double[] yarr = new double[num_points];

    for (int i = 0; i < num_points; i++) {
      double rx = rand.NextDouble()*2 - 1;
      yarr[i] = rx;
      xarr[i] = f(rx);
    }

    return (new vector(xarr), new vector(yarr));
  }
}

class main {
  public static void evaluate_train(ann model, Func<double, double> func_train) {
    (vector x, vector y) = data.get_training_data(20, func_train);
    Console.WriteLine($"Cost before training: {model.cost(x, y)}");
    model.train(x, y);

    Console.WriteLine($"Cost after train (Same data): {model.cost(x, y)}");
    (x, y) = data.get_training_data(40, func_train);
    Console.WriteLine($"Cost after train (New data): {model.cost(x, y)}");
  }

  static void partA() {
    ann a = new ann(30);
    evaluate_train(a, data.g);
  }

  static void partB() {
    ann a = new ann(30);
    (vector x, vector y) = data.get_training_data(20, data.g);
    a.train(x, y);

    /*
    double val_eval = 0.5;

    Console.WriteLine("\n\nDERIVATIVE AND ANTI-DERIVATIVE");
    Console.WriteLine($"Func {a.response(val_eval)}");
    Console.WriteLine($"Derivative {a.response_p(val_eval)}");
    Console.WriteLine($"Double Derivative {a.response_pp(val_eval)}");
    Console.WriteLine($"Integral {a.response_int(val_eval)}");
    */

    double min = -1;
    double max = 1;
    double step = 0.1;
    int count = (int) Floor((max-min)/step);

    for (int i = 0; i < count; i++) {
      double val = min + step*i;
      double vf = a.response(val);
      double vfp = a.response_p(val);
      double vfpp = a.response_pp(val);
      double vfint = a.response_int(val);
      double vf_actual = data.g(val);
      Console.WriteLine($"{val},{vf},{vfp},{vfpp},{vfint},{vf_actual}");
    }
  }

  public static void Main() {
    partB();
  }
}
