using System;
using static System.Console;
using static matrix;
using static System.Random;
using System.Linq;
using System.Collections.Generic;

public class main {

  static matrix RandomMatrix(int size) {
    Random rnd = new Random();

    matrix mat = new matrix(size, size);

    for (int i = 0; i < size; i++) {
      for (int j = 0; j < size; j++) {
        mat[i, j] = rnd.Next(1,1003);
      }
    }

    return mat;
  }

  public static void Main(String[] args) {
    if (args.Length == 0) {
      char[] split_delimiters = {' ', '\t', '\n'};
      var split_options = StringSplitOptions.RemoveEmptyEntries;

      List<string> rows = new List<string>();

      for (string line = ReadLine(); line != null; line = ReadLine()) {
        rows.Add(line);
      }

      int rows_count = rows.Count;
      int columns_count = rows[0].Split(split_delimiters, split_options).Length;

      matrix A = new matrix(rows_count, columns_count);
      
      for (int i = 0; i < rows.Count; i++) {
        var split = rows[i].Split(split_delimiters, split_options);
        for (int j = 0; j < columns_count; j++) {
          A[i, j] = double.Parse(split[j]);
        }
      }  

      // Matrix stuff

      A.print();

      QRGS qr = new QRGS(A);

      vector nvec = new vector(4);
      nvec[0]=-1;
      nvec[1]=7;
      nvec[2]=2;

      qr.Q.print();
      qr.R.print();

      vector x = qr.solve(nvec);
      Write("The x-vector:\n");
      x.print();

      vector b_through_A = A*x;
      b_through_A.print();

      Write("Determinant stuff\n");

      Write($"Determinant: {qr.det()}\n");

      Write("Calculate the inverse!\n");
      qr.inverse();
    } else {
      if (args[0] == "test") {
        tester.testA();
        tester.testB();
        return;
      }

      foreach(string arg in args) {
        if (arg.Contains("-size:")) {
          int size = int.Parse(arg.Split(":")[1]);
          Write($"Size: {size}\n");

          matrix m = RandomMatrix(size);
          QRGS qr = new QRGS(m);

          Write($"Done!\n");
        }
      }
    }
  }

  public static void setup_test_matrix(matrix nm) {
    nm[0, 0] = 1;
    nm[0, 1] = 2;
    nm[0, 2] = 3;
    nm[1, 0] = 9;
    nm[1, 1] = 4;
    nm[1, 2] = 7;
    nm[2, 0] = 2;
    nm[2, 1] = 0;
    nm[2, 2] = 2;
  }

  public static void setup_test_vector(vector testvec) {
    testvec[0]=9;
    testvec[1]=5;
    testvec[2]=4;
  }

  public static void test() {
    matrix nm = new matrix(3, 3);
    nm[0, 0] = 1;
    nm[0, 1] = 2;
    nm[0, 2] = 3;
    nm[1, 0] = 9;
    nm[1, 1] = 4;
    nm[1, 2] = 7;
    nm[2, 0] = 2;
    nm[2, 1] = 0;
    nm[2, 2] = 2;

    vector testvec = new vector(3);
    testvec[0]=9;
    testvec[1]=5;
    testvec[2]=4;

    QRGS qr = new QRGS(nm);

    vector x = qr.solve(testvec.copy());
    x.print();

    vector b = nm*x;

    nm.print();

    testvec.print();
    b.print();
  }
}

public class tester {
  public static void testA() {
    Console.WriteLine("Test A");
    int n = 12;
    int m = 7;

    Random random = new Random();
  
    matrix tall = new matrix(n, m);

    for (int i = 0; i < n; i++) {
      for (int j = 0; j < m; j++) {
        tall[i, j] = random.NextDouble();
      }
    }

    QRGS qr = new QRGS(tall);

    // Check that R is upper triangular
    bool uppertriang = true;
    for (int i = 1; i < qr.R.size1; i++) {
      for (int j = 0; j < i; j++) {
        if (qr.R[i, j] != 0) {
          uppertriang = false;
        }
      }
    }


    print_test(uppertriang, "Test if R is upper triangular");
    matrix QTQ = qr.Q.T*qr.Q;
    QTQ.print();
    matrix QR = qr.Q*qr.R;

    matrix Id = new matrix(QTQ.size1, QTQ.size1);
    Id.setid();

    print_test(Id.approx(QTQ, 1e-4, 1e-4), "Q.T*Q = I");
    print_test(tall.approx(qr.Q*qr.R), "QR=A");
  }

  public static void testB() {
    Console.WriteLine("Test B"); 

    int size = 30;

    matrix sym = new matrix(size, size);

    Random rand = new Random();

    for (int i = 0; i < size; i++) {
      for (int j = i; j < size; j++) {
        double a = rand.NextDouble();
        sym[i, j] = a;
        sym[j, i] = a;
      }
    }

    QRGS qr = new QRGS(sym);

    matrix inv = qr.inverse();

    matrix AB = inv*sym;

    matrix Id = AB.copy();

    print_test(Id.approx(AB), "AB=I");
  }

  static void print_test(bool cond, string message) {
    if (cond) {
      Console.WriteLine($" - (SUCCESS) {message}");
    } else {
      Console.WriteLine($" - (FAIL) {message}");
    }
  }
}
