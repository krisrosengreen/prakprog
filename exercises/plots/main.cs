using System;
using static System.Math;

public class MainClass {

  public static void Main() {
    // erf
    (var a, var b, var c) = pipe.threecols();
    
    Console.WriteLine("Error func tab");
    for(int i = 0; i < a.size; i++)
    {
      double x = a[i];
      double tabx = b[i];

      Console.WriteLine($"{x},{tabx}");
    }

    Console.WriteLine("\n\nError function");
    foreach (var x in a.data)
    {
      Console.WriteLine($"{x},{erf(x)}");
    }

    // Gamma
    double min = -4;
    double max = 5;
    int count = 100;
    double step = (max - min)/(double) count;

    Console.WriteLine("\n\nGamma function");
    for (int i = 0; i < count; i++) {
      double x = min + i*step;
      Console.WriteLine($"{x},{gamma(x)}");
    }

    Console.WriteLine("\n\nGamma tab");
    for (int i = 1; i < 5; i++) {
      Console.WriteLine($"{i},{factorial(i-1)}");
    }

    //lngamma
    min = 0.1;
    step = (max - min)/(double) count;

    Console.WriteLine("\n\nLogGamma function");
    for (int i = 0; i < count; i++) {
      double x = min + i*step;
      Console.WriteLine($"{x},{lngamma(x)}");
    }

  }

  static double factorial(int x) {
    double mult = 1.0;

    if (x == 0) {
      return 1;
    }
    if (x == 1) {
      return mult;
    } else {
      mult *= x * factorial(x - 1);
    }

    return mult;
  }

  static double erf(double x){
    /// single precision error function (Abramowitz and Stegun, from Wikipedia)
    if(x<0) return -erf(-x);
    double[] a={0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429};
    double t=1/(1+0.3275911*x);
    double sum=t*(a[0]+t*(a[1]+t*(a[2]+t*(a[3]+t*a[4]))));/* the right thing */
    return 1-sum*Exp(-x*x);
  }

  static double gamma(double x){
    ///single precision gamma function (Gergo Nemes, from Wikipedia)
    if(x<0)return PI/Sin(PI*x)/gamma(1-x);
    if(x<9)return gamma(x+1)/x;
    double lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
    return Exp(lngamma);
  }

  static double lngamma(double x){
    if(x<=0) throw new ArgumentException("lngamma: x<=0");
    if(x<9) return lngamma(x+1)-Log(x);
    return x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
  }
}
