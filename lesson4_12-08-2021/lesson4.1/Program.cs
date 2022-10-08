using System;

class Program {
    // This function returns sum of integers in range (a ... b) exclusive
    static int sumBetween(int a, int b) {
        int sum = 0;
        for (int i = a + 1; i < b; ++i)
            sum += i;
        return sum;
    }

    static void Main() {
        // Read input
        int a = int.Parse(Console.ReadLine());
        int b = int.Parse(Console.ReadLine());
        // Write result
        Console.WriteLine(sumBetween(a, b));
    }
}
