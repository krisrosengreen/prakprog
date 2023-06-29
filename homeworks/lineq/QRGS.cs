using static matrix;
using System;


public class QRGS{
  public matrix Q,R;
  public QRGS(matrix A){ /* the above "decomp" is the constructor here */
    Q=A.copy();
    R=new matrix(A.size2,A.size2);
    /* orthogonalize Q and fill-in R */
    for (int i=0; i < A.size2; i++){
      R[i, i] = Q[i].norm(); 
      Q[i] /= R[i, i];
      for (int j=i+1; j < A.size2; j++){
        R[i, j] = Q[i].dot(Q[j]);
        Q[j] -= Q[i]*R[i, j];
      }
    }
  }


  public vector solve(vector b){
    vector x = QRGS.backsub(R, Q.T*b);

    return x;
  }


  public static vector backsub (matrix U, vector b) {
    vector c = b.copy();

    for (int i=c.size-1; i >=0; i--){
      double sum=0;
      for (int k=i+1; k<c.size; k++) sum += U[i, k]*c[k];
      c[i]=(c[i]-sum)/U[i, i];
    }
    return c;
  }


  public double det() {
    double prodded = 1;
    for (int i = 0; i < this.R.size1; i++) {
      prodded *= this.R[i,i];
    }
    return prodded;
  }


  public matrix inverse() {
    // Ax=QRx=b
    // Rx=Q^T b
    // R^-1 Rx = R^-1 Q^T b
    // x = R^-1 Q^T b
    // => A^-1 = R^-1 Q^T

    //Step 1: Find R-inverse
    //Step 2: Calculate R^-1 Q^T
    //Step 3: Return what was calculated in step 2
    
    //Step 1
    
    matrix id = matrix.id(this.R.size2);
    matrix Rinv = new matrix(this.R.size1, this.R.size2);

    for (int i = 0; i < this.R.size1; i++) {
      Rinv[i] = QRGS.backsub(this.R, id[i]);
    }
    /*
    Rinv[0] = QRGS.backsub(this.R, id[0]);
    Rinv[1] = QRGS.backsub(this.R, id[1]);
    Rinv[2] = QRGS.backsub(this.R, id[2]);
    */
    //Step 2

    matrix Ainv = Rinv*this.Q.T;

    //Step 3

    return Ainv;
  }
}
