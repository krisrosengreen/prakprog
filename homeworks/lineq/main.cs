using System;
using static System.Console;
using static matrix;
using System.Linq;
using System.Collections.Generic;

public class MainClass {

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
