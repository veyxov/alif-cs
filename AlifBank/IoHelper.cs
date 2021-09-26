using System;
/* Input output helper */
static class IO {
    static private bool DEBUG    = false;
    static private bool COLORFUL = true;

    static public void Print<T>(T what, ConsoleColor color = ConsoleColor.White, bool newLine = true) {
        Console.ForegroundColor = COLORFUL ? color : ConsoleColor.White;
        //Console.Write(what);

        //if (newLine) Console.Write("\n");

        Console.ResetColor();
    }

    /* This function will not output to the terminal if DEBUG is not set */
    static public void Debug(string what, ConsoleColor color = ConsoleColor.DarkGray, bool newLine = true) {
        if (!DEBUG) return;

        Print(what, color, newLine);
    }

    /* This method prompts WHAT and gets the input from Console*/
    static public T Get<T>(string what = "") {
        IO.Print(what, ConsoleColor.Yellow, false);
        var input = (Console.ReadLine());
        /* Change the input type to the T type */
        return (T)Convert.ChangeType(input, typeof(T));
    }
}
