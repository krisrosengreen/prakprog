using System;


public class main {
  public static void Main() {
    (genlist<double> a, genlist<double> b) = pipe.twocols();

    Console.WriteLine("We get");

    for (int i = 0; i < a.size; i++) {
      Console.WriteLine($"{a[i]} {b[i]}");
    }
  }
}

