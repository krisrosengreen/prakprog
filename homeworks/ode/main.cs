using System;
using static matrix;
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

	public static void print(genlist<vector> a) {
		for (int i = 0; i < a.size; i++) {
			a[i].print();
		}
	}
}

public class ODE {
	public static (vector,vector) rkstep12(
		Func<double,vector,vector> f,
		double x,
		vector y,
		double h
		)
	{
		vector k0 = f(x,y);              /* embedded lower order formula (Euler) */
		vector k1 = f(x+h/2,y+k0*(h/2)); /* higher order formula (midpoint) */
		vector yh = y+k1*h;              /* y(x+h) estimate */
		vector er = (k1-k0)*h;           /* error estimate */
		return (yh,er);
	}

	public static vector driver(
		Func<double,vector,vector> f, /* the f from dy/dx=f(x,y) */
		double a,                    /* the start-point a */
		vector ya,                   /* y(a) */
		double b,                    /* the end-point of the integration */
		double h=0.01,               /* initial step-size */
		double acc=0.01,             /* absolute accuracy goal */
		double eps=0.01,              /* relative accuracy goal */
		genlist<double> xlist=null,
		genlist<vector> ylist=null
	){
		if(a>b) throw new ArgumentException("driver: a>b");
		double x=a; vector y=ya.copy();

		if (xlist != null) xlist.add(x);
		if (ylist != null) ylist.add(y);

		do {
			if(x>=b) {
				return y;
			}

			if(x+h>b) h=b-x;               /* last step should end at b */
			var (yh,erv) = rkstep12(f,x,y,h);
			double[] tol = new double[y.size];
			// double err = erv.norm();

			for(int i=0;i<y.size;i++)
				tol[i]=(acc+eps*Abs(yh[i]))*Sqrt(h/(b-a));
			bool ok=true;
			for(int i=0;i<y.size;i++)
				if(erv[i] >= tol[i]) ok=false;
			if(ok){
				x+=h; y=yh;
				if (xlist != null) xlist.add(x);
				if (ylist != null) ylist.add(y);
			}
			double factor = tol[0]/Abs(erv[0]);
			for(int i=1;i<y.size;i++) factor=Min(factor,tol[i]/Abs(erv[i]));
			h *= Min( Pow(factor,0.25)*0.95 ,2);

		}while(true);
	}//driver

	// u'=x
	// x'=-u
	public static vector f_test_ode(double a, vector b) {
		double xv = b[0];
		double uv = b[1];
		return new vector(-uv, xv);
	}


	public static vector pendulum_ode(double t, vector y) {
		double b = 0.25;
		double c = 5.0;

		double theta = y[0];
		double omega = y[1];

		vector dydt = new vector(omega, -b*omega - c * Sin(theta));

		return dydt;
	}


	public static vector lotkavolterra_ode(double t, vector z) {
		double a = 1.5;
		double b = 1;
		double c = 3;
		double d = 1;

		double x = z[0];
		double y = z[1];

		return new vector(a*x - b*x*y, -c*y + d*x*y);
	}


	public static vector acceleration(vector r1, vector r2, double m1=1, double m2=1, double G=100000) {
		return G*m1/Math.Pow((r2-r1).norm(), 3) * (r2 - r1);
	}


	public static (vector, vector, vector) pos_to_vec(vector y, int offset) {
		vector r1 = new vector(y[0 + offset], y[1 + offset]);
		vector r2 = new vector(y[2 + offset], y[3 + offset]);
		vector r3 = new vector(y[4 + offset], y[5 + offset]);

		return (r1, r2, r3);
	}


	public static vector threebodyproblem_ode(double t, vector y) {
		(vector r1, vector r2, vector r3) = pos_to_vec(y, 0);
		(vector g1, vector g2, vector g3) = pos_to_vec(y, 6);

		vector v1 = acceleration(r1, r2) + acceleration(r1, r3);
		vector v2 = acceleration(r2, r1) + acceleration(r2, r3);
		vector v3 = acceleration(r3, r1) + acceleration(r3, r2);

		double[] ret = {g1[0], g1[1], g2[0], g2[1], g3[0], g3[1], v1[0], v1[1], v2[0], v2[1], v3[0], v3[1]};
		return new vector(ret);
	}


  public static void Main(string[] args) {
		genlist<double> a = new genlist<double>();
		genlist<vector> b = new genlist<vector>();

		double factor = 100.0;
		double t0 = 0.0;
		double tf = 10000.0;

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
