using System;

class Program {
    static double solve(double operand1, double operand2, char sign) {
        if (sign == '/' && operand2 == 0.0) {
            throw new DivideByZeroException("Cannot divide by zero");
        }
        // No need for else here
        return sign switch {
            '+' => operand1 + operand2,
            '-' => operand1 - operand2,
            '*' => operand1 * operand2,
            '/' => operand1 / operand2,
            _   => throw new ArgumentException("Please choose from + - * /"),
        };
    }
    static void Main() {
        Console.WriteLine(solve(1, 0, '='));
    }
}
