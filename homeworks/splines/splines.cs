using System;
using static System.Math;


class qspline {
  vector x,y,b,c;

  public qspline(vector xs, vector ys) {
    x = xs;
    y = ys;
  }

  public double polinterp(int n, double z) {
    double p,s=0;
    for (int i = 0; i < n; i++) {
      p=1;
      for (int k = 0; k < n; k++) {
        if (k!=i) {
          p*=(z-x[k])/(x[i]-x[k]);
        }
      }
      s+=y[i]*p;
    }
    return s;
  }

  public int binsearch(double[] x, double z)
	{/* locates the interval for z by bisection */ 
    if(!(x[0]<=z && z<=x[x.Length-1])) throw new Exception("binsearch: bad z");
    int i=0, j=x.Length-1;
    while(j-i>1){
      int mid=(i+j)/2;
      if(z>x[mid]) i=mid; else j=mid;
    }
    return i;
	}

  public double linterp(double z){
    int i=binsearch(x,z);
    double dx=x[i+1]-x[i]; if(!(dx>0)) throw new Exception("uups...");
    double dy=y[i+1]-y[i];
    return y[i]+dy/dx*(z-x[i]);
  }
}
