using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static Random rnd = new Random();

    /* Constants used in the program */
    const string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";

    static int height = Console.WindowHeight;
    static int width = Console.WindowWidth;

    static int minLen = 5;
    static int maxLen = height;

    const int flowSpeed = 50;
    const int genSpeed = 100;

    /* Get a random int from chars array and convert it to char */
    static char GetRandomChar() { return Convert.ToChar(chars[rnd.Next(chars.Length)]); }

    static void ChColor(ConsoleColor color = ConsoleColor.White) => Console.ForegroundColor = color;

    static async Task DrawLine(int x, int y, int len)
    {
        Console.SetCursorPosition(x, y);

        for (int i = 0; i < len; ++i) {
            char curChar = GetRandomChar();
            Console.SetCursorPosition(x, y + i);

            // Write the head with white
            ChColor(ConsoleColor.White);
            Console.Write(curChar);

            // Go one back and redraw with green
            Console.SetCursorPosition(x, y + i - 1);
            ChColor(ConsoleColor.Green);
            Console.Write(curChar);
            //
            // Go two back and redraw with green
            Console.SetCursorPosition(x, y + i - 2);
            ChColor(ConsoleColor.DarkGreen);
            Console.Write(curChar);

            Thread.Sleep(flowSpeed);
        }
    }
    static async Task CleanLine(int x) 
    {
        for (int i = 0; i <= height; ++i) {
            Console.SetCursorPosition(x, i);
            Console.Write(' ');
            Thread.Sleep(flowSpeed - 10);
        }
    }
    static async Task Draw()
    {
        int x = rnd.Next(2, width);
        int y = 2;
        int len = rnd.Next(minLen, Math.Min(maxLen, height - y + 1));
        await Task.Run(() => DrawLine(x, y, len));
        await Task.Run(() => CleanLine(x));
    }

    static void Init()
    {
        Console.Clear(); // First clear the screen
        Console.CursorVisible = false; // Hide the cursor
    }
    static void Main()
    {
        Init();
        // Console size
        IO.Debug($"{width} x {height}");

        Timer timer = new Timer(new TimerCallback(Refresh), null, 10000, 10000);

        while (true) {
            var curTask = Task.Run(() => Draw());
            Thread.Sleep(genSpeed);
        }
    }

    static void Refresh(object obj) { Console.Clear(); }
}
