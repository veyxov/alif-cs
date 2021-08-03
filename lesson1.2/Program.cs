using System;

class Program {
    static void findLenOfLines(double a, double b, double c) {
        // Math.Abs(A)
        // |+A| = +A
        // |-A| = +A

        double ac = Math.Abs(a - c);
        double bc = Math.Abs(b - c);
        double sum = ac + bc;

        Console.WriteLine($"ac  = {ac}");
        Console.WriteLine($"bc  = {bc}");
        Console.WriteLine($"sum = {sum}");
    }
    static void Main() {
        findLenOfLines(1.4, -5.5, 0.6);
    }
}
