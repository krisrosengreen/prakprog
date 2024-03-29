using System;
using static System.Math;
using System.Diagnostics;


public class montecarlo {
  public static (double,double) plainmc(Func<vector,double> f,vector a,vector b,int N){
    int dim=a.size;
    double V=1;

    for(int i=0;i<dim;i++)  // Calculate total volume enclosed in integration
      V*=b[i]-a[i];
    
    double sum=0,sum2=0;
    var x=new vector(dim);
    var rnd=new Random();

    for(int i=0;i<N;i++){
      for(int k=0;k<dim;k++)
        x[k]=a[k]+rnd.NextDouble()*(b[k]-a[k]);  // Random value between a[k] and b[k]

      double fx=f(x);
      sum+=fx;
      sum2+=fx*fx;
    }
    double mean=sum/N, sigma=Sqrt(sum2/N-mean*mean);
    var result=(mean*V,sigma*V/Sqrt(N));
    return result;
  }

  static double corput(int n, int b) {
    double q=0, bk=(double) 1.0/b;

    while (n > 0) {
      q += (n % b)*bk;
      n /= b;
      bk /= b;
    }

    return q;
  }

  // Implement a quasi-random monte-carlo integrator using the halton function
  public static (double, double) quasimc(Func<vector, double> f, vector a, vector b, int N) {
    int dim=a.size;
    double V=1;

    for(int i=0;i<dim;i++)  // Calculate total volume enclosed in integration
      V*=b[i]-a[i];
    
    double sum=0;
    double sum2=0;
    var x=new vector(dim);
    var y=new vector(dim);

    for(int i=0;i<N;i++){
      halton(x, i, dim);
      halton(y, i + 7, dim);

      for(int k=0;k<dim;k++)
      {
        x[k]=a[k]+x[k]*(b[k]-a[k]);
        y[k]=a[k]+y[k]*(b[k]-a[k]);
      }

      double fx=f(x);
      double fy=f(y);
      sum+=fx;
      sum2+=fy;
    }

    var result=(sum*V/N, Abs(sum*V/N-sum2*V/N));
    return result;
  }

  public static void halton(vector x, int n,int d){
    int[] baseValues = new int[]{2,3,5,7,11,13,17,19,23,29,31,37,41,43,47,53,59,61};
    int maxd = baseValues.Length;

    Debug.Assert(d <= maxd);
    for (int i = 0; i < d; i++) {
      x[i] = corput(n, baseValues[i]);
    }
  }
}

