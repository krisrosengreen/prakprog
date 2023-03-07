using static System.Console;
using static cmath;
using static System.Math;

public static class main {

  public static void Main() {
    //Calculate
    //sqrt(-1), sqrt(i), e^i, e^(i*pi), i^i, ln(i), sin(i*pi)

    Write($"sqrt(i) = {sqrt(I)}\n");
    Write($"e^i = {exp(I)}\n");
    Write($"e^(i*pi) = {exp(I*PI)}\n");
    Write($"i^i = {cmath.pow(I, I)}\n");
    Write($"log(i) = {log(I)}\n");
    Write($"sin(i*PI) = {sin(I*PI)}\n");

    Write($"sinh(10) = {sinh(-2)}\n");
    Write($"cosh(10) = {cosh(-2)}\n");
  }

}
