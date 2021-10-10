using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static Random rnd = new Random();

    /* Constants used in the program */
    const string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
    const int minLen = 5;
    const int maxLen = 25;

    static int height = Console.WindowHeight;
    static int width = Console.WindowWidth;

    const int flowSpeed = 40;
    const int genSpeed = 100;

    /* Get a random int from chars array and convert it to char */
    static char GetRandomChar() { 
        int indx = rnd.Next(chars.Length); 
        return Convert.ToChar(chars[indx]);
    }

    static void ChColor(ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
    }

    static void DrawLine(int x, int y, int len)
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

            Thread.Sleep(flowSpeed);
        }
    }
    static void CleanLine(int x, int y, int len) 
    {
        Console.SetCursorPosition(x, 0);
        for (int i = 0; i <= height; ++i) {
            Console.SetCursorPosition(x, y + i);
            Console.Write(' ');
            Thread.Sleep(flowSpeed - 10);
        }
    }
    static void Draw()
    {
        int x = rnd.Next(5, width);
        int y = rnd.Next(5, height);
        int len = rnd.Next(minLen, maxLen);
        DrawLine(x, y + 1, len);
        CleanLine(x, y, len);
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
            Task.Run(() => Draw());
            Thread.Sleep(genSpeed);
        }
    }

    static void Refresh(object obj) { Console.Clear(); }
}
