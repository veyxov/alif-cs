using System;

class Program {
    static void Main() {
        double n = double.Parse(Console.WriteLine());
        double rez = 0.0;

        if (n > 1000) {
            rez = (n * 5.0) / 100.0;
        } else if (n > 500.0) {
            rez = (n * 3.0) / 100.0;
        }
        Console.WriteLine(rez);
    }
}
