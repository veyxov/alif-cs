using System;

class Program {
    // What if n / 60 > 24 * 60 ? :)

    static int fullMinutes(int n) {
        return n / 60;
    }

    static void Main() {
        Console.WriteLine(fullMinutes(10985));
    }
}
