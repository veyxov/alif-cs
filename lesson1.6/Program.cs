using System;
class Program {

    static int day(int n) {
        // 1 2 3 4 5 6 7 8 9 10 ...
        // 1 2 3 4 5 6 0 1 2  3 ...
        return n % 7;
    }

    static void Main() {
        Console.WriteLine(day(202));
    }
}
