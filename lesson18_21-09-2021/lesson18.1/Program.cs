using System;

static class IO {
    static private bool DEBUG    = true;
    static private bool COLORFUL = true;

    static public void Print(string what, ConsoleColor color = ConsoleColor.White, bool newLine = true) {
        Console.ForegroundColor = COLORFUL ? color : ConsoleColor.White;
        Console.Write(what);

        if (newLine) Console.Write("\n");

        Console.ResetColor();
    }

    /* This function will not output to the terminal if DEBUG is not set */
    static public void Debug(string what, ConsoleColor color = ConsoleColor.DarkGray, bool newLine = true) {
        if (!DEBUG) return;

        Print(what, color, newLine);
    }

    /* This method prompts WHAT and gets the input from Console*/
    static public T GetInput<T>(string what = "") {
        IO.Print(what, ConsoleColor.Yellow, false);
        var input = (Console.ReadLine());
        /* Change the input type to the T type */
        return (T)Convert.ChangeType(input, typeof(T));
    }
}

public class MyList<T> {
    private T[] data;
    public void Add(T elem) {
        // Get the current size
        int sz = data.Length;
        // Resize the array (+1)
        Array.Resize(ref data, sz + 1);
        // Push the element
        data[sz] = elem;
        IO.Debug("Resized !");
    }

    // Get length
    public int Size {
        private set {}
        get {
            return data.Length;
        }
    }

    public T this[int index] {
        set {
            IO.Debug($"The length = {data.Length}");
            if (index < 0)                   throw new Exception("Index can not be negative.");
            if (index > data.Length)         throw new Exception("Index can be only +1 offset from the last el...");
            if (index < data.Length)         data[index] = value; // If in bounds assign to existing ...
            else if (index == data.Length)   Add(value);          // Resize and assign
            else                             throw new Exception("Something went wrong !");
        }
        get {
            // Check bounds
            if (index >= data.Length || index < 0) throw new Exception("Index out of bounds.");
            return data[index];
        }
    }

    public void Print() {
        foreach (var i in data)
            Console.Write($"{i} ");
        Console.Write("\n");
    }

    // Default constructor ...
    public MyList() {
        data = new T[0];
    }
    
    // Initialize using an array
    public MyList(T[] arr) {
        Array.Resize(ref data, arr.Length);
        for (int i = 0; i < arr.Length; ++i)
            data[i] = arr[i];
    }
}

class Program {
    static void Main() {
        //int n = IO.GetInput<int>("Input array size: ");
        //IO.Print("Input the array (separated by spaces): ", ConsoleColor.Yellow, false);

        //[> Get input and create an instance <]
        //var ints = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        //var lst = new MyList<int>(ints);

        //IO.Print("The array: ", ConsoleColor.Green);
        //lst.Print();

        /* Adding */
        var lst = new MyList<int>(new int[]{1, 2, 3});
        lst[3] = 4;
        lst[4] = 5;
        lst[5] = 6;
        lst[5] = -6; // Reassignment
        IO.Print($"{lst.Size}");
        lst.Print();
    }
}
