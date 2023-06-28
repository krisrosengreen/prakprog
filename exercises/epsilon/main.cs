using System;
using static System.Math;
using static System.Console;

class MainClass
{
    static void Main() {

        int ii = 1;
        while (ii+1 > ii) {
            ii++;
        }

        Console.WriteLine($"Biggest number is = {ii}");

        int jj = -1;
        while (jj-1 < jj) {
            jj--;
        }

        Console.WriteLine($"Smallest number is = {jj}");

        double x=1;
        while (1+x!=1) {
            x/=2;
        }
        x*=2;

        Console.WriteLine($"Smallest double {x}");

        float y=1F;
        while((float)(1F+y) != 1F) {
            y/=2F;
        }

        y*=2F;
        Console.WriteLine($"Smallest float {y}");

        int n=(int)1e6;
        double epsilon=Pow(2,-52);
        double tiny=epsilon/2;
        double sumA=0,sumB=0;

        sumA+=1; for(int i=0;i<n;i++){sumA+=tiny;}
        for(int i=0;i<n;i++){sumB+=tiny;} sumB+=1;

        WriteLine($"sumA-1 = {sumA-1:e} should be {n*tiny:e}");
        WriteLine($"sumB-1 = {sumB-1:e} should be {n*tiny:e}");

        double d1 = 0.1+0.1+0.1+0.1+0.1+0.1+0.1+0.1;
        double d2 = 8*0.1;

        WriteLine($"d1={d1:e15}");
        WriteLine($"d2={d2:e15}");
        WriteLine($"d1==d2 ? => {d1==d2}");

        WriteLine($"{approx(d1, d2)}");
    }

    static bool approx(double a, double b, double acc=1e-9, double eps=1e-9) {
        return Abs(a-b) < acc || Abs(a-b)/Max(Abs(a), Abs(b)) < eps;
    }
}
