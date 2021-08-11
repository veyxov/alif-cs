using System;

class Program {
    static void Main() {
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
}
