using System;
using static System.Math;
using static matrix;
using static cmath;


public class EVD{
  public vector w;
  public matrix V;

  public EVD(matrix M){
    matrix A=M.copy();
    V=matrix.id(M.size1);
    w=new vector(M.size1);
    /* run Jacobi rotations on A and update V */
    /* copy diagonal elements into w */
  }

  static void timesJ(matrix A, int p, int q, double theta){
    double c=cos(theta),s=sin(theta);
    for(int i=0;i<A.size1;i++){
      double aip=A[i,p],aiq=A[i,q];
      A[i,p]=c*aip-s*aiq;
      A[i,q]=s*aip+c*aiq;
    }
  }

  static void Jtimes(matrix A, int p, int q, double theta){
    double c=cos(theta),s=sin(theta);
    for(int j=0;j<A.size1;j++){
      double apj=A[p,j],aqj=A[q,j];
      A[p,j]= c*apj+s*aqj;
      A[q,j]=-s*apj+c*aqj;
      }
  }

  public void cyclic(matrix A, int n) {
    bool changed;
    do{
      changed=false;
      for(int p=0;p<n-1;p++)
      for(int q=p+1;q<n;q++){
        double apq=A[p,q], app=A[p,p], aqq=A[q,q];
        double theta=0.5*Atan2(2*apq,aqq-app);
        double c=Cos(theta),s=Sin(theta);
        double new_app=c*c*app-2*s*c*apq+s*s*aqq;
        double new_aqq=s*s*app+2*s*c*apq+c*c*aqq;
        if(new_app!=app || new_aqq!=aqq) // do rotation
          {
          changed=true;
          timesJ(A,p,q, theta); // A←A*J 
          Jtimes(A,p,q,-theta); // A←JT*A 
          timesJ(V,p,q, theta); // V←V*J
          }
      }
    }while(changed);
  }

  public static void Main() {

  }
}
