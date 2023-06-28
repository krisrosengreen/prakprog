using System;
using static System.Math;
using static System.Console;
using static sfuns;

class MyClass {
    static void Main() {
        double sqrt2 = Math.Sqrt(2.0);
        double epi = Math.Pow(Math.E, Math.PI);
        double pie = Math.Pow(Math.PI, Math.E);

        Write($"sqrt(2) = {sqrt2}\n");
        Write($"e^pi = {epi}\n");
        Write($"pi^e = {pie}\n");

        Write($"Gamma(1) = {gamma(1)}\n");
        Write($"Gamma(2) = {gamma(2)}\n");
        Write($"Gamma(3) = {gamma(3)}\n");
        Write($"Gamma(10) = {gamma(10)}\n");

        WriteLine($"Lngamma(10) = {logGamma(10)}");
    }
}
