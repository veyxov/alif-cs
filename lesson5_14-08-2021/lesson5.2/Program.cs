using System;

class Program {
    // This function returns a reversed 
    // Array from the original array
    static int[] reverse(int[] arr) {
        int[] rev = new int[arr.Length];
        for (int i = 0; i < arr.Length; ++i)
            rev[i] = arr[arr.Length - i - 1];
        return rev;
    }
    static void Main() {
        Console.Write("Input array size: ");
        int n = int.Parse(Console.ReadLine());
        int[] arr = new int[n];
        for (int i = 0; i < n; ++i)
            arr[i] = int.Parse(Console.ReadLine());

        int[] reversed = reverse(arr);
        Console.WriteLine("Reversed array: ");
        foreach(int i in reversed)
            Console.Write($"{i} ");
    }
}
