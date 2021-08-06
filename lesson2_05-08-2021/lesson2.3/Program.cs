using System;
class Program {

    static string solve(int x) {

        string res = x switch {
            >= 0  and <= 14  =>  "[0  - 14]",
            >= 15 and <= 35  =>  "[15 - 35]",
            >= 36 and <= 50  =>  "[36 - 50]",
            >= 50 and <= 100 =>  "[50 - 100]",
            _                =>  "Error",
        };
        return res;
    }

    static void Main() {
        int x = 2;
        Console.WriteLine(solve(x));
    }
}
