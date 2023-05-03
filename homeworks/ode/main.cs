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

			/*
			double tol = (acc+eps*yh.norm()) * Sqrt(h/(b-a));
			double err = erv.norm();
			if(err<=tol){ // accept step
				x+=h; y=yh;
				if (xlist != null) xlist.add(x);
				if (ylist != null) ylist.add(y);
			}
			h *= Min( Pow(tol/err,0.25)*0.95 , 2); // reajust stepsize
			*/

			double[] tol = new double[y.size + 1];
			double err = erv.norm();

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


  public static void Main() {
		genlist<double> a = new genlist<double>();
		genlist<vector> b = new genlist<vector>();

		vector ys = driver(pendulum_ode, 0, new vector(10.0, 5.0), 15, xlist: a, ylist: b);

		for (int i = 0; i < a.size; i++) {
			Console.WriteLine($"{a[i]},{b[i][0]},{b[i][1]}");
		} 
  }
}
