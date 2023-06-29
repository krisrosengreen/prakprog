using System;
using static System.Math;


class functions {
  public static vector rosenbrockvalley(vector v) {
    double x = v[0];
    double y = v[1];

    double fpx = -2.0*(1.0-x) + 100.0*2.0*(y-x*x)*(-2*x);
    double fpy = 100.0*2*(y-x*x);
    return new vector(fpx, fpy);
  }
}

class exercises {
  public static void partA() {
    vector x0 = new vector(20.0, 20.0);

    vector root = rootfinder.newton(functions.rosenbrockvalley, x0, 0.00001);

    Console.WriteLine("Result");
    root.print();
    Console.WriteLine("function value at xmin");
    functions.rosenbrockvalley(root).print();
  }


  public static void partB() {
    double E = hydrogen.find_hydrogen_root();  
    Console.WriteLine($"# Found energy {E}");
    Console.WriteLine("\n\nExact");
    
    double min = 0.1;
    double max = 8.0;
    double count = 40;
    double step = (max - min)/(double) count;

    for (int i = 0; i < count; i++) {
      double r = min + i*step;
      Console.WriteLine($"{r},{r*Exp(-r)}");
    }
  }
}

public class main {
  public static void Main(string[] args) {
    if (args.Length > 0) {
      if (args[0] == "A") exercises.partA();
      else if (args[0] == "B") exercises.partB();
    }
  }
}
