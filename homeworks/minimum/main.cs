using System;
using static System.Math;


public class genlist<T>{
	public T[] data;
	public int size => data.Length; // property
	public T this[int i] => data[i]; // indexer
	public genlist(){ data = new T[0]; }
	public void add(T item){ /* add item to the list */
		T[] newdata = new T[size+1];
		System.Array.Copy(data,newdata,size);
    newdata[size]=item;
		data=newdata;
	}
}

class funcs {
  public static double simple(vector v) {
    return v.dot(v);
  }

  public static double simple2(vector v) {
    vector offset = new vector(5.0, 5.0);
    return (v+offset).dot(v+offset);
  }

  public static double rosenbrock(vector v) {
    double x = v[0];
    double y = v[1];

    return Pow(1-x, 2) + 100*Pow(y-x*x, 2);
  }

  public static double himmelblau(vector v) {
    double x = v[0];
    double y = v[1];

    return Pow(x*x + y - 11, 2) + Pow(x+y*y-7, 2);
  }

  public static double breitwigner(vector x, double E) {
    double A = x[0];
    double GAMMA = x[1];
    double m = x[2];

    return A/(Pow(E-m, 2) + GAMMA*GAMMA / 4);
  }

  public static double breitwignier_deviation(genlist<double> energy, genlist<double> signal, genlist<double> error, vector x) {
    double D = 0;
    for (int i = 0; i < energy.size; i++) {
      D += Pow((breitwigner(x, energy[i]) - signal[i]) / error[i], 2);
    }

    return D;
  }
}

public class main {
  static void partA() {
    vector xmin = minimum.qnewton(funcs.rosenbrock, new vector(-3.0, -3.0), 0.001);
    xmin.print();
    Console.WriteLine($"{funcs.rosenbrock(xmin)}");

    xmin= minimum.qnewton(funcs.himmelblau, new vector(2), 0.001);
    xmin.print();
    Console.WriteLine($"{funcs.himmelblau(xmin)}");
  }

  static void partB() {
    var energy = new genlist<double>();
    var signal = new genlist<double>();
    var error = new genlist<double>();
    var seperators = new char[] {' ', '\t'};
    var options = StringSplitOptions.RemoveEmptyEntries;
    do {
      string line = Console.In.ReadLine();
      if(line == null) break;
      string[] words = line.Split(seperators, options);
      energy.add(double.Parse(words[0]));
      signal.add(double.Parse(words[1]));
      error.add(double.Parse(words[2]));
    } while (true);

    Func<vector, double> minimizer = x => funcs.breitwignier_deviation(energy, signal, error, x);

    vector xmin = minimum.qnewton(minimizer, new vector(100.0,100.0,100.0), 0.001);
    xmin.print();
  }

  public static void Main() {
    partB();
  }
}
