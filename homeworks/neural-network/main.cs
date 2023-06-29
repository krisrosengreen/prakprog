using System;
using static System.Math; 
using static System.Random;


class data {
  public static double g(double x) {
    return Cos(5*x - 1)*Exp(-x*x);
  }


  public static double gp(double x) {
    return Exp(-x*x)*(-5.0*Sin(5.0*x-1.0) - 2.0*x*Cos(5.0*x - 1.0));
  }


  public static double gpp(double x) {
    return Exp(-x*x)*((4.0*x*x-27.0)*Cos(5.0*x-1.0)+20.0*x*Sin(5.0*x-1.0));
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
}

class exercises {
  public static void evaluate_train(ann model, Func<double, double> func_train) {
    (vector x, vector y) = data.get_training_data(20, func_train);
    Console.WriteLine($"# Cost before training: {model.cost(x, y)}");
    model.train(x, y);

    Console.WriteLine($"# Cost after train (Same data): {model.cost(x, y)}");
    (x, y) = data.get_training_data(40, func_train);
    Console.WriteLine($"# Cost after train (New data): {model.cost(x, y)}");
  }

  public static void partA() {
    ann a = new ann(30);
    evaluate_train(a, data.g);
  }

  public static void partB() {
    ann a = new ann(50);
    (vector x, vector y) = data.get_training_data(200, data.g);
    a.train(x, y);

    double min = -1;
    double max = 1;
    int points = 100;
    double step = 2.0/ (double) points;
    int count = (int) Floor((max-min)/step);

    Console.WriteLine("Actual");
    for (int i = 0; i < count; i++) {
      double val = min + step*i;
      double fval = data.g(val);
      Console.WriteLine($"{val},{fval}");
    }

    Console.WriteLine("\n\nResponse");
    for (int i = 0; i < count; i++) {
      double val = min + step*i;
      double fval = a.response(val);
      Console.WriteLine($"{val},{fval}");
    }

    Console.WriteLine("\n\nResponsep");
    for (int i = 0; i < count; i++) {
      double val = min + step*i;
      double fval = a.response_p(val);
      Console.WriteLine($"{val},{fval}");
    }

    Console.WriteLine("\n\nResponsepp");
    for (int i = 0; i < count; i++) {
      double val = min + step*i;
      double fval = a.response_pp(val);
      Console.WriteLine($"{val},{fval}");
    }

    Console.WriteLine("\n\nIntegral");
    for (int i = 0; i < count; i++) {
      double val = min + step*i;
      double fval = a.response_int(val);
      Console.WriteLine($"{val},{fval}");
    }
  }
}

class main {

  public static void Main(string[] args) {
    if (args.Length > 0) {
      if (args[0] == "A") {
        exercises.partA();
      } else if (args[0] == "B") {
        exercises.partB();
      }
    }
    // tester.test();
  }
}
