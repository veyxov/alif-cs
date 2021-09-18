using System;
/* Input output helper */
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
    static public string GetString(string what = "") {
        IO.Print(what, ConsoleColor.Yellow, false);
        var input = Console.ReadLine();
        return input;
    }

    static public decimal GetDecimal(string what = "") {
        IO.Print(what, ConsoleColor.Yellow, false);
        var input = decimal.Parse(Console.ReadLine());
        return input;
    }
}

class Program {
    static class ArrayHelper {
        static T Pop<T>(ref T[] arr) {
            var newArr = new T[arr.Length - 1];
            
            for (int i = 0; i < arr.Length - 1; ++i)
                newArr[i] = arr[i];

            return arr[arr.Length - 1];
        }
    }
    static void Main() {
        IO.Print("Input the array (in one line): ", newLine: false);

    }
}
