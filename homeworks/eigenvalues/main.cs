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
  }
}

public class main {
  public static void Main() {
    // exercises.partA();
    exercises.partB();
  }
}
