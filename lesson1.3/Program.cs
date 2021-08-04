using System;

class Program {
    static double distance(double x1, double y1, double x2, double y2) {
        double dx = x1 - x2;
        double dy = y1 - y2;
        return Math.Sqrt(dx * dx + dy * dy);
    }
    static void Main() {
        Console.WriteLine(Math.Round(distance(-6.20, 5.2, 2.10, 9.8), 2));
    }
}
