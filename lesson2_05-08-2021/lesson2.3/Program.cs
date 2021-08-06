using System;
class Program {

    static string solve(int x) {
        // [0 - 14], [15 - 35], [36 - 50], [50 - 100]
        //    1          2          3          4
        
        int res = x switch {
            >= 0  and <= 14  =>  1,
            >= 15 and <= 35  =>  2,
            >= 36 and <= 50  =>  3,
            >= 50 and <= 100 =>  4,
            _                => -1,
        };
        return res == -1 ? "Incorrect range" : Convert.ToString(res);
    }

    static void Main() {
        int x = 100;
        Console.WriteLine(solve(x));
    }
}
