using System;
using System.Linq;
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
    static public T GetInput<T>(string what = "") {
        IO.Print(what, ConsoleColor.Yellow, false);
        var input = (Console.ReadLine());
        /* Change the input type to the T type */
        return (T)Convert.ChangeType(input, typeof(T));
    }
}

class Program {
    public static class ArrayHelper {
        public static void Print<T>(T[] arr) {
            foreach (T i in arr) {
                IO.Print(i.ToString(), color: ConsoleColor.Cyan, newLine: false);
                IO.Print(" ", newLine: false);
            }
            IO.Print("");
        }
        public static T Pop<T>(ref T[] arr) {
            IO.Debug("Popping element ...");
            var newArr = new T[arr.Length - 1];
            var lastElement = arr[arr.Length - 1];

            for (int i = 0; i < arr.Length - 1; ++i) newArr[i] = arr[i];
            arr = newArr;

            IO.Debug("Element poped !");
            return lastElement;
        }
        public static int Push<T>(ref T[] arr, T newElement) {
            IO.Debug("Pushing element ...");
            var newArr = new T[arr.Length + 1];
            for (int i = 0; i < arr.Length ; ++i)   newArr[i] = arr[i]; 
            newArr[newArr.Length - 1] = newElement;

            arr = newArr;

            IO.Debug("Element Pushed !");
            return arr.Length;
        }
        public static T Shift<T>(ref T[] arr) {
            IO.Debug("Shifting element ...");
            var deletedElement = arr[0];
            var newArr = new T[arr.Length - 1];
            for (int i = 1; i < arr.Length; ++i)    newArr[i - 1] = arr[i];

            arr = newArr;
            IO.Debug("Element Shifted !");
            return deletedElement;
        }
        public static int UnShift<T>(ref T[] arr, T newElement) {
            IO.Debug("Unshifting ...");
            var newArr = new T[arr.Length + 1];
            newArr[0] = newElement;

            for (int i = 0; i < arr.Length; ++i)     newArr[i + 1] = arr[i];
            arr = newArr;
            IO.Debug("UnShifted !");
            return arr.Length;
        }

        /* Helper methods */

        /* If the argument is the array return the whole array */
        public static T[] Slice<T>(T[] arr, int begin = 0, int end = 0) {
            int count = 0;
            if (end < 0) count = arr.Length - begin - Math.Abs(end);
            else         count = end - begin;
            IO.Debug($"Count: {count}");
            if (count <= 0) { IO.Print("Empty !", ConsoleColor.DarkGreen); return new T[0]; }
            var newArr = new T[count];
            var sz = 0;
            try {
                if (end >= 0) {
                    for (int i = begin; i < end; ++i)                           newArr[sz++] = arr[i];
                } else {
                    for (int i = begin; i < arr.Length - Math.Abs(end); ++i)    newArr[sz++] = arr[i];
                }
            } catch (Exception ex) {
                IO.Print("An error occured when slicing, maybe invalid user input.", ConsoleColor.Magenta);
                IO.Print(ex.Message, ConsoleColor.Red);
            }
            return newArr;
        }

        public static T[] Slice<T>(T[] arr) { return arr; }

        /* If only one argument is given */
        public static T[] Slice<T>(T[] arr, int begin) {
            if (begin < 0) return Slice(arr, arr.Length - Math.Abs(begin), arr.Length);
            else           return Slice(arr, begin, arr.Length);
        }
    }
    static void Main() {
        try {
            IO.Print("Input the size: ", newLine: false);
            var n = int.Parse(Console.ReadLine());
        } catch (Exception ex) {
            IO.Print("Invalid array size !", ConsoleColor.Red);
            IO.Print(ex.Message);
            return;
        }

        IO.Print("Input the array (in one line): ", newLine: false);
        /*                                         change type here ! */
        //var arr = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        var arr = Console.ReadLine().Split(" ");

        bool running = true;
        while (running) {
            IO.GetInput<string>("Press enter to continue ...");
            Console.Clear();
            ArrayHelper.Print(arr);
            try {
                var cmd = IO.GetInput<string>("1.Push\t2.Pop\t3.Shift\t4.UnShift\t5.Slice\t6.Go to slice testing\n->: ");

                switch (cmd) {
                    case "1":
                        ArrayHelper.Push(ref arr, IO.GetInput<string>("Push what ?: "));
                        break;
                    case "2":
                        ArrayHelper.Pop(ref arr);
                        break;
                    case "3":
                        ArrayHelper.Shift(ref arr);
                        break;
                    case "4":
                        ArrayHelper.UnShift(ref arr, IO.GetInput<string>("UnShift what ?: "));
                        break;
                    case "5":
                        ArrayHelper.Print(ArrayHelper.Slice(arr,
                                    IO.GetInput<int>("Begin: "),
                                    IO.GetInput<int>("End: ")));
                        break;
                    default:
                        running = false;
                        break;
                }
            } catch (Exception ex) {
                IO.Print("Invalid user input !", ConsoleColor.Magenta);
                IO.Print(ex.Message, ConsoleColor.Red);
                running = false;
            }
        }
        /* Slice testing */
        var animals = new string[] { "ant", "bison", "camel", "duck", "elephant" };

        IO.Print("ArrayHelper.Print(ArrayHelper.Slice(arr));", ConsoleColor.Blue);
        ArrayHelper.Print(ArrayHelper.Slice(animals));

        IO.Print("ArrayHelper.Print(ArrayHelper.Slice(animals, 2));", ConsoleColor.Blue);
        ArrayHelper.Print(ArrayHelper.Slice(animals, 2));

        IO.Print("ArrayHelper.Print(ArrayHelper.Slice(animals, 2, 4));", ConsoleColor.Blue);
        ArrayHelper.Print(ArrayHelper.Slice(animals, 2, 4));

        IO.Print("ArrayHelper.Print(ArrayHelper.Slice(animals, 1, 5));", ConsoleColor.Blue);
        ArrayHelper.Print(ArrayHelper.Slice(animals, 1, 5));

        IO.Print("ArrayHelper.Print(ArrayHelper.Slice(animals, -2));", ConsoleColor.Blue);
        ArrayHelper.Print(ArrayHelper.Slice(animals, -2));

        IO.Print("ArrayHelper.Print(ArrayHelper.Slice(animals, 2, -1));", ConsoleColor.Blue);
        ArrayHelper.Print(ArrayHelper.Slice(animals, 2, -1));

        IO.Print("ArrayHelper.Print(ArrayHelper.Slice(animals, 1, -1));", ConsoleColor.Blue);
        ArrayHelper.Print(ArrayHelper.Slice(animals, 1, -1));

        IO.Print("ArrayHelper.Print(ArrayHelper.Slice(animals, 3, -1));", ConsoleColor.Blue);
        ArrayHelper.Print(ArrayHelper.Slice(animals, 3, -1));

        IO.Print("ArrayHelper.Print(ArrayHelper.Slice(animals, 3));", ConsoleColor.Blue);
        ArrayHelper.Print(ArrayHelper.Slice(animals, 3));

        IO.Print("ArrayHelper.Print(ArrayHelper.Slice(animals, -5));", ConsoleColor.Blue);
        ArrayHelper.Print(ArrayHelper.Slice(animals, -5));

        IO.Print("ArrayHelper.Print(ArrayHelper.Slice(animals, 1, 1));", ConsoleColor.Blue);
        ArrayHelper.Print(ArrayHelper.Slice(animals, 1, 1));

        IO.Print("ArrayHelper.Print(ArrayHelper.Slice(animals, 123));", ConsoleColor.Blue);
        ArrayHelper.Print(ArrayHelper.Slice(animals, 123));
    }
}
