using System;
using static matrix;
using static System.Math; 
using static System.Random;

class ann{
  int n; /* number of hidden neurons */
  double step_size;
  Func<double, double> f = x => x*Exp(-x*x); /* activation function */
  Func<double, double> fp = x => Exp(-x*x) - 2*x*x*Exp(-x*x);
  public matrix p; /* network parameters (ai, bi, wi) */

  public ann(int n){
    step_size = 0.01;

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

  public vector response(vector x) {
    double[] resp = new double[x.size];

    for (int i = 0; i < x.size; i++) {
      resp[i] = response(x[i]);
    }

    return new vector(resp);
  }

  public double response_p(double x) {
    double resp = 0;
    for (int i = 0; i < n; i ++) {
      resp += fp((x - p[i, 0])/p[i, 1])*p[i, 2]; 
    }
    
    return resp;

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

    for (int epoch = 0; epoch < 500; epoch++) {
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

class main {
  public static double g(double x) {
    return Cos(5*x - 1)*Exp(-x*x);
  }

  public static (vector, vector) get_training_data(int num_points, Func<double, double> f) {
    Random rand = new Random();

    double[] xarr = new double[num_points];
    double[] yarr = new double[num_points];

    for (int i = 0; i < num_points; i++) {
      double rx = rand.NextDouble()*2 - 1;
      yarr[i] = rx;
      xarr[i] = f(rx);
    }

    return (new vector(xarr), new vector(yarr));
  }

  public static void Main() {
    ann a = new ann(30);

    (vector x, vector y) = get_training_data(20, g);

    Console.WriteLine($"Cost before training: {a.cost(x, y)}");

    a.train(x, y);

    Console.WriteLine($"Cost after train (Same data): {a.cost(x, y)}");
    (x, y) = get_training_data(40, g);
    Console.WriteLine($"Cost after train (New data): {a.cost(x, y)}");
  }
}
