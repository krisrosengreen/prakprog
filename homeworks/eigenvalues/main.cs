using System;
// Use random package to randomize matrix elements
using static System.Random;

public class exercises{
  public static void partA() {
    int size = 5;
    matrix original = new matrix(size, size);
    matrix Id = new matrix(size, size); 
    Id.setid();

    // Randomize matrix A
    Random rand = new Random();
    for(int i=0;i<size;i++)
    {
      for(int j=0;j<i+1;j++) {
        double a = rand.NextDouble();
        original[i,j] = a;
        original[j,i] = a;
      }
    }

    matrix A = original.copy();

    EVD.cyclic(A, Id);
    original.print();
    A.print();
  }

  public static void partB() {
    // NUMERICAL CALCULATION
    double rmax = 10;
    double dr = 0.3;

    matrix H = hydrogen.Hamiltonian(rmax, dr);
  
    (matrix Hdiag, matrix eigvecs, vector eigvals) = hydrogen.eigen(H);

    double E_lowest = hydrogen.lowest_val(eigvals);

    Console.WriteLine($"# Lowest E value {E_lowest}");


    // CONVERGENCE
    // rmax fixed, variable dr

    double fixrmax = 10.0;
    Console.WriteLine("\trmax_fix:");
    for (int i = 1; i < 10; i++) {
      double step = (float) i / 10.0;
      double E = hydrogen.lowest_energy(fixrmax, step);

      Console.WriteLine($"{step},{E}");
    }

    // dr fixed, variable rmax
    double fixdr = 0.3;
    Console.WriteLine("\n\n\tdr_fix:");
    for (int i = 1; i < 10; i++) {
      double var_rmax = (float) i ;
      double E = hydrogen.lowest_energy(var_rmax, fixdr);

      Console.WriteLine($"{var_rmax},{E}");
    }

    // WAVEFUNCTIONS

    // values to be looked at
    double[] rmaxs = {10.0, 10.0, 10.0, 10.0, 10.0};
    double[] drs = {0.1, 0.2, 0.3, 0.4, 0.5};

    for (int i = 0; i < rmaxs.Length; i++) {
      rmax = rmaxs[i];
      dr = drs[i];

      vector eigenfunc = hydrogen.lowest_wavefunction(rmax, dr);
      vector rs = hydrogen.rvalues(rmax, dr);

      Console.WriteLine($"\n\nrmax{rmax}dr{dr}");
      for (int j = 0; j < eigenfunc.size; j++) {
        Console.WriteLine($"{rs[j]},{eigenfunc[j]/rs[j]}");
      }
    }
  }
}

public class test {
  public static void testA() {
    Console.WriteLine("Testing part A");
    //Show that implementation works as intended
    int size = 4;

    matrix sym = symmatrix(4);
    matrix D = sym.copy();

    matrix Id = new matrix(size, size); 
    Id.setid();

    matrix V = Id.copy();

    EVD.cyclic(D, V);

    matrix fromcomps = V*D*V.T;

    print_testres(fromcomps.approx(sym), "A from EVD components");
    print_testres(Id.approx(V.T*V), "VT*V = I");
    print_testres(Id.approx(V*V.T), "VT*V = I");
    print_testres(D.approx(V.T*sym*V), "V.T * A * V = D");
  }

  static void print_testres(bool cond, string message) {
    if (cond) {
      Console.WriteLine($"- (SUCCESS) {message}");
    } else {
      Console.WriteLine($"- (FAIL) {message}");
    }
  }

  static matrix symmatrix(int size) {
    matrix testmat = new matrix(size, size);

    Random random = new Random();

    for (int i = 0; i < size; i++) {
      for (int j = i; j < size; j++) {
        double randval = random.NextDouble();
        testmat[i,j] = randval;
        testmat[j,i] = randval;
      }
    }

    return testmat;
  }
}

public class main {
  public static void Main(string[] args) {
    if (args.Length > 0) {
      if (args[0] == "test") {
        test.testA();
        return;
      } else if (args[0] == "A") exercises.partA();
      else if (args[0] == "B") exercises.partB();
    }
  }
}
