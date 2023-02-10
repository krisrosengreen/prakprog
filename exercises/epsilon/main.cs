using System;

class MainClass
{
    static void Main() {

        int i = 1;
        while (i+1 > i) {
            i++;
        }

        Console.WriteLine($"Biggest number is = {i}");

        int j = -1;
        while (j-1 < j) {
            j--;
        }

        Console.WriteLine($"Smallest number is = {j}");

    }
}
