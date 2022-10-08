using System;

class Program {
    static void findLenOfLines(double a, double b, double c) {
        double ac = Math.Abs(a - c);
        double bc = Math.Abs(b - c);
        double sum = ac + bc;

        Console.WriteLine($"ac  = {Math.Round(ac, 2)}");
        Console.WriteLine($"bc  = {Math.Round(bc, 2)}");
        Console.WriteLine($"sum = {Math.Round(sum, 2)}");
    }
    static void Main() {
        findLenOfLines(1.4, -5.5, 0.6);
    }
}
