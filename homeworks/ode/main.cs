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

	public static (genlist<double>,genlist<vector>) driver(
		Func<double,vector,vector> f, /* the f from dy/dx=f(x,y) */
		double a,                    /* the start-point a */
		vector ya,                   /* y(a) */
		double b,                    /* the end-point of the integration */
		double h=0.01,               /* initial step-size */
		double acc=0.01,             /* absolute accuracy goal */
		double eps=0.01              /* relative accuracy goal */
	){
	if(a>b) throw new ArgumentException("driver: a>b");
	double x=a; vector y=ya.copy();
	var xlist=new genlist<double>(); xlist.add(x);
	var ylist=new genlist<vector>(); ylist.add(y);
	do      {
					if(x>=b) return (xlist,ylist); /* job done */
					if(x+h>b) h=b-x;               /* last step should end at b */
					var (yh,erv) = rkstep12(f,x,y,h);
					double tol = (acc+eps*yh.norm()) * Sqrt(h/(b-a));
					double err = erv.norm();
					if(err<=tol){ // accept step
			x+=h; y=yh;
			xlist.add(x);
			ylist.add(y);
			}
		h *= Min( Pow(tol/err,0.25)*0.95 , 2); // reajust stepsize
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


  public static void Main() {
		(genlist<double> a, genlist<vector> b) = driver(pendulum_ode, 0, new vector(PI - 0.1, 0.0), 101);

		for (int i = 0; i < a.size; i++) {
			Console.WriteLine($"{a[i]},{b[i][0]},{b[i][1]}");
		} 
  }
}
