using static System.Console;

public class main {

  public static void Main() {
    Write("Hello world!\n");

    test();
  }

  public static void test() {
    vec v1 = new vec(1, 2, 3);
    vec v2 = new vec(4, 5, 6);
    Write("Operations using vec1 (x=1, y=2, z=3) and vec2 (x=4, y=5, z=6)\n");
    (2*v1).print("scalar = ");
    (v1+v2).print("plus = ");
    (v1-v2).print("minus = ");
    Write($"dot = {v1.dot(v2)}\n");
    Write("vec1 to string = " + v1.ToString());
  }
}
