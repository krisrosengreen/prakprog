using System;

public class qspline{
	vector x,y,b,c;
	public qspline(vector xs, vector ys){
	  /* copy xs,ys into x,y and build b,c */
	  x = xs.copy();
	  y = ys.copy();
	}
	public double evaluate(double z){
	  /* evaluate the spline using x,y,b,c */

	  return z;
	}

  public static int binsearch(vector x, double z)
  {/* locates the interval for z by bisection */ 
    if(!(x[0]<=z && z<=x[x.size-1])) throw new Exception("binsearch: bad z");

    int i=0, j=x.size-1;
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

  public double linterpInteg(double z) {
    int end = binsearch(x, z);

    double sum = 0;
    for (int i = 0; i < end; i++) {
      double dx = x[i + 1] - x[i];
      double dy = y[i + 1] - y[i];

      sum += y[i] * dx + 0.5 * dy * dx;
    }

    double dx2 = z - x[end];
    double dy2 = linterp(z) - y[end];

    sum += y[end] * dx2 + 0.5 * dy2 * dx2;

    return sum;
  }
}

public class quadraticspline {
	vector x,y,b,c;
	public quadraticspline(vector xs, vector ys){
	  x = xs.copy();
	  y = ys.copy();

	  int n = x.size;

	  b = new vector(n - 1);
	  c = new vector(n - 1);

    vector p = new vector(n - 1);
    vector h = new vector(n - 1);

    for (int i = 0; i < n - 1; i++) {
      h[i] = x[i+1] - x[i];
      p[i] = (y[i + 1] - y[i]) / h[i];
    }

    for (int i = 0; i < n - 2; i++) {
      c[i + 1] = (p[i + 1] - p[i] - c[i] * h[i]) / h[i + 1];
    }

    c[n - 2] /= 2;

    for (int i = n - 3; i >= 0; i--) {
      c[i] = (p[i + 1] - p[i] - c[i+1]*h[i+1])/h[i];
    }

    for (int i = 0; i < n - 1; i++) {
      b[i] = p[i] - c[i]*h[i];
    }
	}
	public double evaluate(double z){
	  int n = x.size;
	  if (z < x[0] || z > x[n - 1]) {
	    throw new ArgumentException("z is outside the range of x.");
	  }

	  int i = 0, j = n - 1;

	  while (j - i > 1) {
	    int m = (i+j)/2;
	    if (z > x[m])
	      i = m;
	    else
	      j = m;
	  }

	  double h = z-x[i];
	  return y[i] + h*(b[i] + h*c[i]);
	}
}
