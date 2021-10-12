using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    // Console dimensions
    static int width = Console.WindowWidth;
    static int height = Console.WindowHeight - 2;

    // Colors
    const ConsoleColor BaseColor = ConsoleColor.DarkGreen;
    const ConsoleColor TailColor = ConsoleColor.Green;
    const ConsoleColor HeadColor = ConsoleColor.White;
    // Speeds
    static int flowSpeed = 40;
    const int Margin = 3;

    // Random number generator
    static Random rnd = new Random();
    private static object locker = new object();

    //static char GetRandomChar() => Convert.ToChar(rnd.Next(255));
    static char GetRandomChar() => Convert.ToChar(rnd.Next(65, 97));

    /* Draws a char to console with specific coordinates and color */
    static async Task DrawChar(int x, int y, bool is_clear = false, ConsoleColor color = BaseColor)
    {
        // Lock the cursor position (not doing so introduces bugs)
        lock (locker)
        {
            // Set the position and print random char
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);

            char res = ' ';
            if (!is_clear) res = GetRandomChar();
            Console.Write(res);
        }
    }
    static async Task DrawLine(int x, int y, int len)
    {
        for (int i = 0; i < len; ++i)
        {
            await Task.Delay(flowSpeed);
            ConsoleColor curColor = BaseColor;

            // Index from the end
            int j = len - i - 1;
            if (j == 0) curColor = HeadColor;
            else if (j == 1) curColor = TailColor;

            await DrawChar(x, y + i, false, curColor);
            //await DrawChar(x, y + i - 1, false, TailColor);
            //await DrawChar(x, y + i - 2, false, BaseColor);
        }
        for (int i = -2; i < len; ++i)
        {
            await DrawChar(x, y + i, true);
            await Task.Delay(flowSpeed);
        }
    }

    static async Task Draw()
    {
        int x = rnd.Next(Margin, width - Margin);
        int y = rnd.Next(2, height - Margin);
        int len = rnd.Next(3, 20);

        await DrawLine(x, y, len);
    }

    static async Task DrawMatrix(int amount)
    {
        var tasks = new Task[amount];
        for (int i = 0; i < amount; ++i)
        {
            tasks[i] = Task.Factory.StartNew(
                    () => Draw()
                    );
        }
        await Task.WhenAll(tasks);
    }
    /* Initialize the screen */
    static void Init()
    {
        Console.Clear();
        Console.CursorVisible = false;
    }
    static async Task Main()
    {
        Init();
        while (true) {
            await DrawMatrix(5);
            Thread.Sleep(500);
        }
    }
}
