using System;

class Program {
    static void solve(int[] arr) {
        int max = arr[0], min = arr[0], sum = 0;
        // There is arr.Length odd numbers
        // In the worst case that all are odd
        int odds = 0;
        int[] odd = new int[arr.Length];

        for (int i = 0; i < arr.Length; ++i) {
            if (arr[i] > max) max = arr[i]; // Find max;
            if (arr[i] < min) min = arr[i]; // Find min;
            sum += arr[i];                  // Find sum;

            // Save odd numbers
            if (arr[i] % 2 != 0) odd[odds++] = arr[i];
        }
        double mean = (double)sum / arr.Length;
        Console.WriteLine($"Maximum: {max}");
        Console.WriteLine($"Minimum: {min}");
        Console.WriteLine($"Summ   : {sum}");
        Console.WriteLine($"Mean   : {mean}");
        Console.Write("Odd numbers: ");
        for (int i = 0; i < odds; ++i)
            Console.Write($"{odd[i]} ");
    }
    static void Main() {
        Console.Write("Input array size: ");
        int.TryParse(Console.ReadLine(), out int n); // Get input
        Console.WriteLine("Input the array:");
        int[] arr = new int[n];
        for (int i = 0; i < n; ++i)
            arr[i] = int.Parse(Console.ReadLine());

        solve(arr);
    }
}
