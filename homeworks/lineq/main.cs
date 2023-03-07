using System;

public class vector {
  public double[] data;
  public int size => data.Length;
  public double this[int i]{   // indexer
    get => data[i];        // getter
    set => data[i]=value;  // setter
  }
  public vector(int n){        // constructor
      data=new double[n];
  }
}

public class matrix{
	public readonly int size1,size2;
	private double[] data;  // to keep matrix elements
	public matrix(int n,int m){      // constructor
		size1=n; size2=m;
		data = new double[size1*size2];
		}
	public double this[int i,int j]{     // indexer
		get => data[i+j*size1];
		set => data[i+j*size1]=value;
		}
}

public static class QRGS {
  public static void decomp(matrix a, matrix r) {

  }

  public static vector solve(matrix Q, matrix R, vector b) {

  }

  public static double det(matrix R) {

  }
}

public class MainClass {

  public static void Main() {
    Console.Write("Hello!");
  }

}
