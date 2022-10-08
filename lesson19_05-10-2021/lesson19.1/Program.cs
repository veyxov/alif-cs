using System;

namespace delig
{
    class Program
    {
        /* This method prompts WHAT and gets the input from Console*/
        static public T GetInput<T>(string what = "") {
            Console.Write(what);
            var input = (Console.ReadLine());
            /* Change the input type to the T type */
            //Console.Write("\n");
            return (T)Convert.ChangeType(input, typeof(T));
        }

        static T Add<T>(dynamic a, dynamic b) { return a + b; }
        static T Sub<T>(dynamic a, dynamic b) { return a - b; }
        static T Div<T>(dynamic a, dynamic b) { return a / b; }
        static T Mul<T>(dynamic a, dynamic b) { return a * b; }

        delegate T deligateTemplate<T>(dynamic x, dynamic y);

        public static void Show(dynamic a, dynamic b) {
            Console.Clear();
            Console.WriteLine($"A: {a} B: {b}");
            Console.WriteLine("1.Add\n2.Sub\n3.Div\n4.Mul");
        }
        public static void Main() {
            deligateTemplate<int> deligateFunc;
            int cmd; // Choosen command

            var a = GetInput<int>("A: ");
            var b = GetInput<int>("B: ");

            while (true) {
                /* Get user info */
                Show(a, b);
                cmd = GetInput<int>("Input your command: ");

                switch (cmd) {
                    case 1:
                        deligateFunc = Add<int>;
                        break;
                    case 2:
                        deligateFunc = Sub<int>;
                        break;
                    case 3:
                        deligateFunc = Div<int>;
                        break;
                    case 4:
                        deligateFunc = Mul<int>;
                        break;
                    default:
                        return;
                }
                Console.WriteLine($"Result {deligateFunc?.Invoke(a, b)}");
                // Pause
                GetInput<string>("Press enter to continue ...");
            }
        }
    }
}
/* 
 * NOTES: 
 * Could not find a solution for problem: Cannot add two T(Generics)s
 * So I used dynamic, as StackOverflow.com suggested :) 
 * */
