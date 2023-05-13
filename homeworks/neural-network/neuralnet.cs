using System;
using static System.Math; 
using static System.Random;


public class ann{
  int n; /* number of hidden neurons */
  int epochs;
  double step_size;
  Func<double, double> fint = x => -0.5*Exp(-x*x); 
  Func<double, double> f = x => x*Exp(-x*x); /* activation function */
  Func<double, double> fp = x => Exp(-x*x) - 2*x*x*Exp(-x*x);
  Func<double, double> fpp = x => -2.0*x*Exp(-x*x) - 4*x*Exp(-x*x) - 2*x*x*Exp(-x*x)*(-2*x);
  matrix p; /* network parameters (ai, bi, wi) */


  public ann(int n, double step_size=0.01, int epochs=500){
    this.step_size=step_size;
    this.epochs=epochs;
    p = new matrix(n, 3);

    // Initialize random values in matrix p
    Random rnd = new Random();

    for (int i = 0; i < n; i++) {
      for (int j = 0; j < 3; j++) {
        p[i, j] = rnd.NextDouble();
      }
    }

    this.n = n;
  }


  public double response(double x){
    /* return the response of the network to the input signal x */
    double resp = 0;
    for (int i = 0; i < n; i ++) {
      resp += f((x - p[i, 0])/p[i, 1])*p[i, 2]; 
    }
    
    return resp;
  }


  public double response_p(double x) {
    double resp = 0;
    for (int i = 0; i < n; i ++) {
      resp += fp((x - p[i, 0])/p[i, 1])*p[i, 2]; 
    }
    
    return resp;
  }


  public double response_pp(double x) {
    double resp = 0;
    for (int i = 0; i < n; i ++) {
      resp += fpp((x - p[i, 0])/p[i, 1])*p[i, 2]; 
    }
    
    return resp;
  }


  public double response_int(double x) {
    double resp = 0;
    for (int i = 0; i < n; i ++) {
      resp += fint((x - p[i, 0])/p[i, 1])*p[i, 2]; 
    }
    
    return resp;
  }


  public vector response(vector x) {
    double[] resp = new double[x.size];

    for (int i = 0; i < x.size; i++) {
      resp[i] = response(x[i]);
    }

    return new vector(resp);
  }


  public double cost(vector x, vector y) {
    double cost = 0;
    
    for (int i = 0; i < x.size; i++) {
      cost += 1.0/x.size * Pow(response(x[i]) - y[i], 2);
    }

    return cost;
  }


  public void train(vector x, vector y){
    /* train the network to interpolate the given table {x,y} */
    matrix ptrain = p.copy();

    for (int epoch = 0; epoch < epochs; epoch++) {
      for (int it = 0; it < x.size; it++) {

        double xt = x[it];
        double yt = y[it];

        for (int i = 0; i < n; i++) {
          double a = p[i, 0];
          double b = p[i, 1];
          double w = p[i, 2];

          double da = (2.0/n) * (response(xt) - yt) * fp((xt-a)/b)  * w * (-1.0/b)      * w * step_size;
          double db = (2.0/n) * (response(xt) - yt) * fp((xt-a)/b)  * w * (a/Pow(b, 2)) * w * step_size;
          double dw = (2.0/n) * (response(xt) - yt) * f((xt-a)/b)                           * step_size;

          ptrain[i, 0] -= da;
          ptrain[i, 1] -= db;
          ptrain[i, 2] -= dw;
        }
      }
      p = ptrain.copy();
      // Console.WriteLine($"{epoch},{cost(x, y)}");
    }
  }
}

