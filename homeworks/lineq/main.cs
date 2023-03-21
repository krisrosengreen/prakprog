using System;
using static System.Console;
using System.Linq;
using System.Collections.Generic;
using static matrix;

public class QRGS{
  public matrix Q,R;
  public QRGS(matrix A){ /* the above "decomp" is the constructor here */
    Q=A.copy();
    R=new matrix(A.size2,A.size2);
    /* orthogonalize Q and fill-in R */
    for (int i=0; i < A.size2; i++){
      R[i,i] = Q[i].norm(); 
      Q[i] /= R[i,i];
      for (int j=i+1; j < A.size2; j++){
        R[i,j] = Q[i].dot(Q[j]);
        Q[j] -= Q[i]*R[i,j];
      }
    }
  }

  public vector solve(vector b){
    vector x = QRGS.backsub(this.R, this.Q.T*b);
    return x;
  }

  public static vector backsub (matrix U, vector b) {
    vector c = b.copy();

    for (int i=c.size-1; i >=0; i--){
      double sum=0;
      for (int k=i+1; k<c.size; k++) sum+=U[i,k]*c[k];
      c[i]=(c[i]-sum)/U[i,i];
    }
    return c;
  }
}

public class MainClass {
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

  public static void pre_test() {
    matrix nm = new matrix(3, 3);
    setup_test_matrix(nm);

    vector testvec = new vector(3);
    setup_test_vector(testvec);

    QRGS qr = new QRGS(nm);
    vector x = qr.solve(testvec);

    qr.Q.print();
    qr.Q.T.print();
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
