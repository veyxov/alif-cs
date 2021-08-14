using System;

class Program {

    static int[] createArray(int[] arr, int count, int index) {
        // Create array
        int[] rez = new int[count];
        int sz = 0; // This is current position to be filled
        for (int i = index; i < arr.Length; ++i)
            rez[sz++] = arr[i];

        while (sz < count) // If there is empty space fill it with 1
            rez[sz++] = 1;
        // Return the answer
        return rez;
    }


    static void Main() {
        Console.Write("Input array size: ");
        int n = int.Parse(Console.ReadLine());

        Console.WriteLine("Input the array:");
        int[] arr = new int[n];
        for (int i = 0; i < n; ++i)
            arr[i] = int.Parse(Console.ReadLine());

        Console.WriteLine("\nThe array:");
        foreach (int i in arr)
            Console.Write($"{i} ");

        Console.Write("\nInput the count: ");
        int count = int.Parse(Console.ReadLine());

        Console.Write("Input the index: ");
        int index = int.Parse(Console.ReadLine());

        int[] createdArray = createArray(arr, count, index);
        foreach (int i in createdArray)
            Console.Write($"{i} ");
    }
}
