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

  public double linterp(int n, double z) {
    int i = 0, j=n-1;
    while(j-i>1) {
      int m = (i+j)/2;
      if (z>x[m]) {
        i = m;
      } else {
        j = m;
      }
    }

    double dy=y[i+1]-y[i], dx=x[i+1]-x[i];

    return y[i]+dy/dx*(z-x[i]);
  }
}
