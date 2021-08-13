using System;

class Program {
    static void Main() {
        double init = 1000.0;
        double p = double.Parse(Console.ReadLine());
        int k = 0;
        while (init < 1100.0) {
            ++k;
            init += (init * p) / 100.0;
        }
        Console.WriteLine($"Sum = {init} in {k} days");
    }
}
