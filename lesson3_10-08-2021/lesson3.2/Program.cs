using System;

class Program {
    static void Main0() {
        int[] a = new int[4];
        a[0] = int.Parse(Console.ReadLine());
        a[1] = int.Parse(Console.ReadLine());
        a[2] = int.Parse(Console.ReadLine());
        a[3] = int.Parse(Console.ReadLine());

        for (int i = 0; i + 1 < 4; ++i) {
            if (!(a[i] < a[i + 1])) {
                // Output the min
                Console.WriteLine(Math.Min(a[0], Math.Min(a[1], Math.Min(a[2], a[3]))));
                return;
            }
        }
        bool allTheSame = true;
        for (int i = 0; i + 1 < 4; ++i) {
            if (a[i] != a[i + 1]) {
                allTheSame = false;
                break;
            }
        }
        if (allTheSame) {
            Console.WriteLine(a[0] * a[1] * a[2] * a[3]);
        } else {
            Console.WriteLine("Numbers are increasing");
        }
    }
    
    // Other solution
    static void Main() {
        int a, b, c, d;
        a = int.Parse(Console.ReadLine());
        b = int.Parse(Console.ReadLine());
        c = int.Parse(Console.ReadLine());
        d = int.Parse(Console.ReadLine());
        if (a == b && b == c && c == d) {
            int mul = 4 * a; // All of them are equal: a * a * a ... n = n * a
            Console.WriteLine(mul);
        }
        else if (!(a < b && b < c && c < d)) {
            int min = Math.Min(a, Math.Min(b, Math.Min(c, d)));
            Console.WriteLine(min);
        } else {
            Console.WriteLine("Числа расположены по возрастанию");
        }
    }
}
