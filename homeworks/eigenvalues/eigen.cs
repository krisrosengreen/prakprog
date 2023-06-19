using System;
using static System.Math;

public class EVD{
  public static void timesJ(matrix A, int p, int q, double theta){
	  double c=Cos(theta),s=Sin(theta);
	  for(int i=0;i<A.size1;i++){
		  double aip=A[i,p],aiq=A[i,q];
		  A[i,p]=c*aip-s*aiq;
		  A[i,q]=s*aip+c*aiq;
		}
  }

  public static void Jtimes(matrix A, int p, int q, double theta){
	  double c=Cos(theta),s=Sin(theta);
	  for(int j=0;j<A.size1;j++){
		  double apj=A[p,j],aqj=A[q,j];
		  A[p,j]= c*apj+s*aqj;
		  A[q,j]=-s*apj+c*aqj;
		}
  }

  public static void cyclic(matrix A, matrix V){
    int n=A.size1;
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
}

public class hydrogen {
  public static matrix Hamiltonian(double rmax, double dr) {
    int npoints = (int) (rmax/dr) - 1;
    vector r = new vector(npoints);
    for (int i=0; i < npoints; i++) {
      r[i]=dr*(i+1);
    }

    matrix H = new matrix(npoints, npoints);
    for(int i=0; i < npoints-1; i++) {
      H[i, i] = -2;
      H[i, i+1] = 1;
      H[i+1, i] = 1;
    }
    H[npoints-1, npoints-1] =- 2;

    matrix.scale(H, -0.5/dr/dr);

    for (int i=0; i < npoints; i++) {
      H[i, i] += -1/r[i];
    }

    return H;
  }

  public static double lowest_val(vector v) {
    double lowest = v[0];
    for (int i = 1; i < v.size; i++) {
      if (v[i] < lowest) lowest = v[i];
    }

    return lowest;
  }

  // Returns diag H matrix, eig vectors and eigen values
  public static (matrix, matrix, vector) eigen(matrix H) {
    matrix I = H.copy(); I.setid();
    EVD.cyclic(H, I);
    vector eigvals = new vector(I.size1);

    for (int i = 0; i < I.size1; i++) eigvals[i] = H[i, i];  // Eigenvalues are diagonal in H

    return (H, I, eigvals);
  }

  public static double lowest_energy(double rmax, double dr) {
    matrix H = Hamiltonian(rmax, dr);
    (matrix e, matrix eigenvecs, vector eigenvals) = eigen(H);

    double E_lowest = lowest_val(eigenvals);

    return E_lowest;
  }
}
