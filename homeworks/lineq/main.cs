using System;
using static System.Console;

public class QRGS{
  public matrix Q,R;
  public QRGS(matrix A){ /* the above "decomp" is the constructor here */
    Q=A.copy();
    R=new matrix(A.size1,A.size2);
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
  /*
  public vector solve(vector b){

  }

  public double det(){

  }

  public matrix inverse(){

  }
  */
}

public class MainClass {

  public static void Main() {
    test();
  }

  public static void pre_test() {
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

    nm.print();

    matrix nmcopy = nm.copy();

    matrix multed = nm * nmcopy;

    multed.print();
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

    QRGS qr = new QRGS(nm);

    qr.Q.print();

    matrix I = qr.Q.T()*qr.Q;

    I.print();

    qr.R.print();
  }

}
