using System;

class Program {
    static void Main() {
        int a = int.Parse(Console.ReadLine());
        int b = int.Parse(Console.ReadLine());

        if (a != b) {
            /*
             * We can use the Math.Max() method:
             *
             * a = Math.Max(a, b);
             * b = Math.Max(a, b);
             * 
             * */

            a = a > b ? a : b;
            b = a > b ? a : b;
        } else {
            // a = 0
            // b = 0
            a = b = 0;
        }
        Console.WriteLine($"{a} {b}");
    }
}
