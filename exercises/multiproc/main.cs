using System;
using System.Threading;
using static System.Console;

public static class main {
  static double[] sum;

  static void harm_sum(int fro, int to, int thread_id) {

    double inner_sum = 0;
    for (int i = fro; i < to; i++){
      inner_sum += 1.0/i;
    }

    sum[thread_id] = inner_sum;
    Write($"Thread done! sum {inner_sum} from {fro} to {to} id {thread_id}\n");
  }

  static void Main() {
    harm_sum_threading();
  }

  static void harm_sum_threading() {
    int num_threads = 10;
    int range_thread = 1000;
    sum = new double[num_threads+1];

    Thread[] ts = new Thread[num_threads];

    for (int i = 0; i < num_threads; i++) {
      ts[i] = new Thread(() => harm_sum(i*range_thread+1, i*range_thread + range_thread, i));
      ts[i].Start();
    }

    for (int i = 0; i < num_threads; i++) {
      ts[i].Join();
    }

    /*
    Thread t1 = new Thread(() => harm_sum(1,100));
    t1.Start();
    t1.Join();
    */

    foreach(double d in sum) {
      Write($"val = {d}\n");
    }
  }
}
