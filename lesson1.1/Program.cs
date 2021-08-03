using System;

class Program {
    static double geometricMean(double a, double b) {
        return Math.Sqrt(a * b);
    }
    static void Main() {
        Console.WriteLine(geometricMean(16.8, 12.40));
    }
}
