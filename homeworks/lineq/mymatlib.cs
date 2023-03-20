using System;
using static System.Console;

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

  public double norm() {
    double summed = 0;
    for (int i = 0; i < this.size; i++) {
      summed += Math.Pow(this[i], 2.0);
    }

    return Math.Sqrt(summed);
  }

  public static vector operator /(vector b, double a) {
    vector new_vec = new vector(b.size);

    for (int i = 0; i < b.size; i++) {
      new_vec[i] = b[i]/a;
    }

    return new_vec;
  }

  public double dot(vector a) {
    if (this.size != a.size) {
      throw new Exception("Vectors are not of equal size!");
    }

    double dotted = 0;

    for (int i = 0; i < this.size; i++) {
      dotted += this[i]*a[i];
    }

    return dotted;
  }

  public static vector operator *(vector a, double b) {
    vector new_vec = new vector(a.size);

    for (int i = 0; i < a.size; i++) {
      new_vec[i] = a[i]*b;
    }

    return new_vec;
  }

  public static vector operator -(vector a, vector b) {
    if (a.size != b.size) throw new Exception("Vectors are not of equal size!");

    vector new_vec = new vector(a.size);

    for (int i = 0; i < a.size; i++) {
      new_vec[i] = a[i] - b[i];
    }

    return new_vec;
  }

  public void print() {
    Write("Vector: ");
    for (int i = 0; i < this.size; i++) {
      Write($"{this[i]} ");
    }
    Write("\n");
  }
}

public class matrix{
	public readonly int size1,size2;
	
  private double[] data;  // to keep matrix elements
	
  public matrix(int n,int m){      // constructor
		size1=n; size2=m;
		data = new double[size1*size2];
	}

	public double this[int i,int j] {     // indexer
    get {
      if (i >= size1 || j >= size2) throw new Exception("The matrix indeces are greater than matrix size!");

      return data[i+j*this.size1];
    } 
    set {
      if (i >= size1 || j >= size2) throw new Exception("The matrix indeces are greater than matrix size!");

      data[i+j*size1]=value;
    }
	}

  public static matrix operator *(matrix a, matrix b) {
    if (a.size2 != b.size1) throw new Exception("Matrices cannot be multiplied. Incompatible dimensions!");

    matrix nm = new matrix(a.size1, b.size2);
    for (int i = 0; i < a.size1; i++) {
      for (int j = 0; j < b.size2; j++) {
        double summed = 0;
        for (int k = 0; k < a.size2; k++) {
          summed += a[i, k] * b[k, j];
        }
        nm[i, j] = summed;
      } 
    }

    return nm;
  }

  public vector this[int i] {
    get {
      if (i >= size1) throw new Exception("The matrix index is greater than matrix size!");

      vector to_ret = new vector(size2);
      for (int j = 0; j < size2; j++) {
        to_ret[j] = this[i, j];
      }

      return to_ret;
    }

    set {
      if (i >= size1) throw new Exception("The matrix index is greater than matrix size!");
      if (value.size != size1) throw new Exception("Could not set matrix row. Sizes are not equal!");

      for (int j = 0; j < this.size2; j++) {
        this[i, j] = value[j];
      }
    }
  }

  public matrix copy() {
    matrix copy_matrix = new matrix(size1, size2);
    for (int i = 0; i < size1; i++) {
      for (int j = 0; j < size2; j++) {
        copy_matrix[i,j]=this[i,j];
      }
    }

    return copy_matrix;
  }

  public void print() {
    Write("Matrix:");
    for (int i = 0; i < this.size1; i++) {
      Write("\n");
      for (int j = 0; j < this.size2; j++) {
        Write($"{this[i, j]} ");
      }
    }
    Write("\n");
  }

  public matrix T() {
    matrix transt = new matrix(this.size1, this.size2);

    for (int i = 0; i < this.size1; i++) {
      for (int j = 0; j < this.size2; j++) {
        transt[i,j]=this[j,i];
      }
    }

    return transt;
  }
}
