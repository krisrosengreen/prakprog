using System;
using System.Threading;
using static System.Console;

public static class main {
  public class data { public int a,b; public double sum;}

  public static void harmonic(object obj){
    var local = (data)obj;
    local.sum=0;
    for(int i=local.a;i<local.b;i++) local.sum+=1.0/i;
  }

  public static void Main(string[] args) {
    int nthreads = 1, nterms = (int)1e8; /* default values */

    foreach(var arg in args) {
     var words = arg.Split(':');
     if(words[0]=="-threads") nthreads=int.Parse(words[1]);
     if(words[0]=="-terms"  ) nterms  =(int)float.Parse(words[1]);
    }

    data[] x = new data[nthreads];
    for(int i=0;i<nthreads;i++) {
       x[i]= new data();
       x[i].a = 1 + nterms/nthreads*i;
       x[i].b = 1 + nterms/nthreads*(i+1);
    }
    x[x.Length-1].b=nterms+1; /* the enpoint might need adjustment */

    var threads = new Thread[nthreads];
    for(int i=0;i<nthreads;i++) {
      threads[i] = new Thread(harm); /* create a thread */
      threads[i].Start(x[i]);        /* run it with x[i] as argument to "harmonic" */
    }

    for(int i=0;i<nthreads;i++) threads[i].Join();

    for(int i=0;i<nthreads;i++) sum+=x[i].sum;

    double sum=0; Parallel.For( 1, N+1, delegate(int i){sum+=1.0/i;} );
  }
}
