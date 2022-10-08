using System;

static class ArrayHelper {
    // Int
    public static int Pop(ref int[] arr) {
        if (arr.Length == 1) {
            int temp = arr[0];
            arr = new int[] {};
            return temp;
        } else if (arr.Length < 1) throw new Exception("Array already empty !");
        var newArr = new int[arr.Length - 1];
        for (int i = 0; i < arr.Length - 1; ++i)
            newArr[i] = arr[i];
        arr = newArr;
        return arr[arr.Length - 1];
    }
    public static int Push(ref int[] arr, int newElement) {
        var newArr = new int[arr.Length + 1];
        for (int i = 0; i < arr.Length; ++i)
            newArr[i] = arr[i];
        newArr[arr.Length] = newElement;
        arr = newArr;
        return newArr.Length;
    }
    public static int Shift(ref int[] arr) {
        if (arr.Length == 1) {
            int temp = arr[0];
            arr = new int[] {};
            return temp;
        } else if (arr.Length < 1) throw new Exception("Array already empty !");
        var newArr = new int[arr.Length - 1];
        for (int i = 1; i < arr.Length; ++i)
            newArr[i - 1] = arr[i];
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
    public static void PrintArray(int[] arr) {
        for (int i = 0; i < arr.Length; ++i)
            Console.Write($"{arr[i]} ");
        Console.WriteLine();
    }
    // string
    public static string Pop(ref string[] arr) {
        if (arr.Length == 1) {
            string temp = arr[0];
            arr = new string[] {};
            return temp;
        } else if (arr.Length < 1) throw new Exception("Array already empty !");
        var newArr = new string[arr.Length - 1];
        for (int i = 0; i < arr.Length - 1; ++i)
            newArr[i] = arr[i];
        arr = newArr;
        return arr[arr.Length - 1];
    }
    public static int Push(ref string[] arr, string newElement) {
        var newArr = new string[arr.Length + 1];
        for (int i = 0; i < arr.Length; ++i)
            newArr[i] = arr[i];
        newArr[arr.Length] = newElement;
        arr = newArr;
        return newArr.Length;
    }
    public static string Shift(ref string[] arr) {
        if (arr.Length == 1) {
            string temp = arr[0];
            arr = new string[] {};
            return temp;
        } else if (arr.Length < 1) throw new Exception("Array already empty !");
        var newArr = new string[arr.Length - 1];
        for (int i = 1; i < arr.Length; ++i)
            newArr[i - 1] = arr[i];
        arr = newArr;
        return arr[0];
    }
    public static int UnShift(ref string[] arr, string newElement) {
        var newArr = new string[arr.Length + 1];
        newArr[0] = newElement;
        for (int i = 0; i < arr.Length; ++i)
            newArr[i + 1] = arr[i];
        arr = newArr;
        return newArr.Length;
    }
    public static void PrintArray(string[] arr) {
        for (int i = 0; i < arr.Length; ++i)
            Console.Write($"{arr[i]} ");
        Console.WriteLine();
    }
    // Double
    public static double Pop(ref double[] arr) {
        if (arr.Length == 1) {
            double temp = arr[0];
            arr = new double[] {};
            return temp;
        } else if (arr.Length < 1) throw new Exception("Array already empty !");
        var newArr = new double[arr.Length - 1];
        for (int i = 0; i < arr.Length - 1; ++i)
            newArr[i] = arr[i];
        arr = newArr;
        return arr[arr.Length - 1];
    }
    public static double Push(ref double[] arr, double newElement) {
        var newArr = new double[arr.Length + 1];
        for (int i = 0; i < arr.Length; ++i)
            newArr[i] = arr[i];
        newArr[arr.Length] = newElement;
        arr = newArr;
        return newArr.Length;
    }
    public static double Shift(ref double[] arr) {
        if (arr.Length == 1) {
            double temp = arr[0];
            arr = new double[] {};
            return temp;
        } else if (arr.Length < 1) throw new Exception("Array already empty !");
        var newArr = new double[arr.Length - 1];
        for (int i = 1; i < arr.Length; ++i)
            newArr[i - 1] = arr[i];
        arr = newArr;
        return arr[0];
    }
    public static double UnShift(ref double[] arr, double newElement) {
        var newArr = new double[arr.Length + 1];
        newArr[0] = newElement;
        for (int i = 0; i < arr.Length; ++i)
            newArr[i + 1] = arr[i];
        arr = newArr;
        return newArr.Length;
    }
    public static void PrintArray(double[] arr) {
        for (int i = 0; i < arr.Length; ++i)
            Console.Write($"{arr[i]} ");
        Console.WriteLine();
    }
    // Float
    public static float Pop(ref float[] arr) {
        if (arr.Length == 1) {
            float temp = arr[0];
            arr = new float[] {};
            return temp;
        } else if (arr.Length < 1) throw new Exception("Array already empty !");
        var newArr = new float[arr.Length - 1];
        for (int i = 0; i < arr.Length - 1; ++i)
            newArr[i] = arr[i];
        arr = newArr;
        return arr[arr.Length - 1];
    }
    public static float Push(ref float[] arr, float newElement) {
        var newArr = new float[arr.Length + 1];
        for (int i = 0; i < arr.Length; ++i)
            newArr[i] = arr[i];
        newArr[arr.Length] = newElement;
        arr = newArr;
        return newArr.Length;
    }
    public static float Shift(ref float[] arr) {
        if (arr.Length == 1) {
            float temp = arr[0];
            arr = new float[] {};
            return temp;
        } else if (arr.Length < 1) throw new Exception("Array already empty !");
        var newArr = new float[arr.Length - 1];
        for (int i = 1; i < arr.Length; ++i)
            newArr[i - 1] = arr[i];
        arr = newArr;
        return arr[0];
    }
    public static float UnShift(ref float[] arr, float newElement) {
        var newArr = new float[arr.Length + 1];
        newArr[0] = newElement;
        for (int i = 0; i < arr.Length; ++i)
            newArr[i + 1] = arr[i];
        arr = newArr;
        return newArr.Length;
    }
    public static void PrintArray(float[] arr) {
        for (int i = 0; i < arr.Length; ++i)
            Console.Write($"{arr[i]} ");
        Console.WriteLine();
    }

    // Decimal
    public static decimal Pop(ref decimal[] arr) {
        if (arr.Length == 1) {
            decimal temp = arr[0];
            arr = new decimal[] {};
            return temp;
        } else if (arr.Length < 1) throw new Exception("Array already empty !");
        var newArr = new decimal[arr.Length - 1];
        for (int i = 0; i < arr.Length - 1; ++i)
            newArr[i] = arr[i];
        arr = newArr;
        return arr[arr.Length - 1];
    }
    public static decimal Push(ref decimal[] arr, decimal newElement) {
        var newArr = new decimal[arr.Length + 1];
        for (int i = 0; i < arr.Length; ++i)
            newArr[i] = arr[i];
        newArr[arr.Length] = newElement;
        arr = newArr;
        return newArr.Length;
    }
    public static decimal Shift(ref decimal[] arr) {
        if (arr.Length == 1) {
            decimal temp = arr[0];
            arr = new decimal[] {};
            return temp;
        } else if (arr.Length < 1) throw new Exception("Array already empty !");
        var newArr = new decimal[arr.Length - 1];
        for (int i = 1; i < arr.Length; ++i)
            newArr[i - 1] = arr[i];
        arr = newArr;
        return arr[0];
    }
    public static decimal UnShift(ref decimal[] arr, decimal newElement) {
        var newArr = new decimal[arr.Length + 1];
        newArr[0] = newElement;
        for (int i = 0; i < arr.Length; ++i)
            newArr[i + 1] = arr[i];
        arr = newArr;
        return newArr.Length;
    }
    public static void PrintArray(decimal[] arr) {
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
        for (int i = 0; i < n; ++i) arr[i] = int.Parse(vals[i]);
        Console.WriteLine("The array: ");
        ArrayHelper.PrintArray(arr);
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
            } else break;
            Console.Clear();
            ArrayHelper.PrintArray(arr);
        }
        Console.Clear();
        Console.WriteLine("The result: ");
        ArrayHelper.PrintArray(arr);
    }
}
