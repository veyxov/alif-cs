using System;

static class ArrayHelper {
    // This method removes the last element of arr and returns it.
    public static int Pop(ref int[] arr) {
        if (arr.Length == 1) {
            int temp = arr[0];
            arr = new int[] {};
            return temp;
        } else if (arr.Length < 1) {
            throw new Exception("Array already empty !");
        }

        var newArr = new int[arr.Length - 1];

        // We go to (arr.Lenght - 1), becase we don't need the last element
        for (int i = 0; i < arr.Length - 1; ++i)
            newArr[i] = arr[i];

        arr = newArr;
        // Return the last element.
        return arr[arr.Length - 1];
    }
    // This method pushes newElement into arr and returns the new size.
    public static int Push(ref int[] arr, int newElement) {
        // We need +1 space for the pushed element.
        var newArr = new int[arr.Length + 1];

        // First copy all.
        for (int i = 0; i < arr.Length; ++i)
            newArr[i] = arr[i];

        // Add newElement to the end.
        newArr[arr.Length] = newElement;

        // Change the pointer to new array.
        arr = newArr;
        return newArr.Length;
    }

    // This method deletes the first element of array and returns it
    public static int Shift(ref int[] arr) {
        if (arr.Length == 1) {
            int temp = arr[0];
            arr = new int[] {};
            return temp;
        } else if (arr.Length < 1) {
            throw new Exception("Array already empty !");
        }

        var newArr = new int[arr.Length - 1];
        for (int i = 1; i < arr.Length; ++i)
            newArr[i - 1] = arr[i]; // The n'th element in arr is n - 1'th in newArr
        arr = newArr;
        return arr[0];
    }

    public static int UnShift(ref int[] arr, int newElement) {
        var newArr = new int[arr.Length + 1];
        newArr[0] = newElement;
        for (int i = 0; i < arr.Length; ++i)
            newArr[i + 1] = arr[i];
        arr = newArr;
        return newArr.Length;
    }

    // This method prints an array to console
    public static void PrintArray(int[] arr) {
        for (int i = 0; i < arr.Length; ++i)
            Console.Write($"{arr[i]} ");
        Console.WriteLine();
    }
}

class Program {
    static void Main() {
        Console.Write("Input the size: "); int.TryParse(Console.ReadLine(), out int n);
        var arr = new int[n];

        Console.Write("Input the array in one line (separated by spaces): ");
        var vals = Console.ReadLine().Split(' ');
        for (int i = 0; i < n; ++i)
            arr[i] = int.Parse(vals[i]);

        Console.WriteLine("The array: ");
        ArrayHelper.PrintArray(arr);

        // Ask the user what to do
        while (true) {
            Console.WriteLine("Choose one option 1-pop 2-push 3-shift 4-unshift 5-end");
            Console.Write("Action: ");
            int cmd = int.Parse(Console.ReadLine());

            if (cmd == 1) {
                Console.WriteLine("Poping last element");
                ArrayHelper.Pop(ref arr);
            } else if (cmd == 2) {
                Console.Write("New element to be pushed: ");
                int newEl = int.Parse(Console.ReadLine());
                ArrayHelper.Push(ref arr, newEl);
            } else if (cmd == 3) {
                Console.WriteLine("Shifting");
                ArrayHelper.Shift(ref arr);
            } else if (cmd == 4) {
                Console.Write("Input new element: ");
                int newEl = int.Parse(Console.ReadLine());
                ArrayHelper.UnShift(ref arr, newEl);
            } else {
                break;
            }
            Console.Clear();
            ArrayHelper.PrintArray(arr);
        }
        Console.Clear();
        Console.WriteLine("The result: ");
        ArrayHelper.PrintArray(arr);
    }
}
