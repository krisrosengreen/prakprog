using System;
using static ode;
using static System.Math;

public class main {
  public static void Main(string[] args) {
		genlist<double> a = new genlist<double>();
		genlist<vector> b = new genlist<vector>();

		double factor = 1000.0;
		double t0 = 0.0;
		double tf = 10.0;

		vector r1 = new vector(0.746156*factor, 0);
		vector r2 = new vector(-0.373078*factor, 0.238313*factor);
		vector r3 = new vector(-0.373078*factor, -0.238313*factor);

		vector v1 = new vector(0, 0.324677*factor);
		vector v2 = new vector(0.764226*factor, -0.162339*factor);
		vector v3 = new vector(-0.764226*factor, -0.162339*factor);

		double[] input = {r1[0], r1[1], r2[0], r2[1], r3[0], r3[1], v1[0], v1[1], v2[0], v2[1], v3[0], v3[1]};

		vector ys = driver(threebodyproblem_ode, t0, new vector(input), tf, xlist: a, ylist: b);

		for (int i = 0; i < a.size; i++) {
			Console.WriteLine($"{a[i]},{b[i][0]},{b[i][1]},{b[i][2]},{b[i][3]},{b[i][4]},{b[i][5]}");
		}
  }
}
