using System;
using static System.Math;


class data {
  public static (vector, vector) generate_sin() {
    int n = 30;
    vector x = new vector(n);
    vector y = new vector(n);

    for (int i = 0; i < n; i++) {
      double z = (double) i / 5.0;
      x[i] = z;
      y[i] = Sin(z);
    }

    return (x, y);
  }
}

class exercises {
  public static vector genlist2vec(genlist<double> gl) {
    vector a = new vector(gl.size);

    for (int i = 0; i < gl.size; i++) {
      a[i] = gl[i];
    }

    return a;
  }

  public static void partA() {
    (genlist<double> a, genlist<double> b) = pipe.twocols();

    vector va = genlist2vec(a);
    vector vb = genlist2vec(b);

    qspline qs = new qspline(va, vb);

    Console.WriteLine("Linterp");

    for (int i = 0; i < 60; i++) {
      double z = (double) i / 10.0;

      Console.WriteLine($"{z},{qs.linterp(z)}");
    }

    Console.WriteLine("\n\nIntegral");

    // Print -cos(x) by integrating sin(x) using qs
    for (int i = 0; i < 60; i++) {
      double z = (double) i / 10.0;

      double integ = qs.linterpInteg(z);
      Console.WriteLine($"{z},{integ}");
    }
  }

  public static void partB() {
    (vector x, vector y) = data.generate_sin();
    quadraticspline qs = new quadraticspline(x,y);

    Console.WriteLine("\n\nQuadratic Interp");
    for (int i = 0; i < 60; i++) {
      double z = (double) i / 10.4;

      Console.WriteLine($"{z},{qs.evaluate(z)}");
    }
  }
}

public class main {
  public static void Main() {
    exercises.partA();
    exercises.partB();
  }
}
