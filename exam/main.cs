using System;
using static System.Math;

class testing {
  public static void performTests() {
    matrix A = CC.construct_matrix(4);
    vector b = new vector(new double[]{1.0, 1.0, 1.0, 1.0});

    vector x = CC.solve(A, b);

    print_test(b.approx(A*x), "Check A*x=b for solved x");

    matrix L = CC.decomp(A);

    print_test(A.approx(L*L.T), "Check L^T*L=A");

    matrix inv = CC.inverse(A);

    matrix Id = inv.copy(); Id.setid();

    print_test(Id.approx(inv*A), "A^-1*A=I");

    A = CC.construct_matrix(2);

    double determinant = A[0,0]*A[1,1] - A[1,0]*A[0,1];

    print_test(Abs(CC.determinant(A) - determinant) < 1e-4, "Check det(A) is calculated correctly");
  }

  public static void timeProcedure(int size) {
    matrix A = CC.construct_matrix(size);
    vector b = new vector(size);
    for (int i = 0; i < size; i++) b[i] = 1;

    CC.solve(A, b);
  }

  static void print_test(bool cond, string msg) {
    if (cond) Console.WriteLine($" - (SUCCESS) {msg}");
    else Console.WriteLine($" - (FAIL) {msg}");
  }
}

public class main {
  public static void uprint(string msg) {
    Console.WriteLine(msg);
    Console.WriteLine(new String('-', msg.Length));
  }

  public static void Main(string[] args) {
    if (args.Length > 0) {
      if (args[0] == "test") testing.performTests();
      else if (args[0] == "time" && args.Length == 2) testing.timeProcedure(Int32.Parse(args[1]));
      
      
      return;
    }

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
