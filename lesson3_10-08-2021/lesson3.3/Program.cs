using System;

class Program {
    static void Main() {
        int a = int.Parse(Console.ReadLine());
        int b = int.Parse(Console.ReadLine());
        int c = int.Parse(Console.ReadLine());

        int max = Math.Max(a, Math.Max(b, c));
        int min = Math.Min(a, Math.Min(b, c));
        int mid = (a + b + c) - (max + min);

        a = max;
        b = mid;
        c = min;

        Console.WriteLine($"{a} {b} {c}");
    }
}
