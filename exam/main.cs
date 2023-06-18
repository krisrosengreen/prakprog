using System;

public class main {
  public static void uprint(string msg) {
    Console.WriteLine(msg);
    Console.WriteLine(new String('-', msg.Length));
  }

  public static void Main() {
    matrix A = CC.construct_matrix(4);

    vector b = new vector(new double[]{1.0, 1.0, 1.0, 1.0});

    vector x = CC.solve(A, b);
    Console.WriteLine("\n");

    uprint("Solver");

    x.print();

    vector test = A*x;
    test.print();

    uprint("Test inverse matrix");

    matrix inverse = CC.inverse(A);

    matrix Afroinv = inverse*A;

    Afroinv.print();

    uprint("Determinant");

    Console.WriteLine($"{CC.determinant(A)}");
  }
}
