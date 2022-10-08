using System;

class Program {
    // Draw a rectangle of size n * m
    static void Rectangle(int n, int m) {
        for (int i = 0; i < n; ++i) {
            // Draw m lines n time
            for (int j = 0; j < m; ++j) {
                Console.Write("*");
            }
            Console.Write("\n");
        }
    }

    // Draw a right angled triangle of size N
    static void RightTriangle(int n) {
        // i-th layer has the length of i
        for (int i = 1; i <= n; ++i) {
            for (int j = 1; j <= i; ++j) {
                Console.Write("*");
            }
            Console.Write("\n");
        }
    }
    // Draw a triangle with equal sides
    static void EqualiteralTriangle(int n) {
        int space = n;
        int star  = 1;
        for (int i = 0; i < n; ++i) {
            // Draw left side spaces
            for (int j = 0; j < space; ++j)
                Console.Write(" ");
            // Draw stars
            for (int j = 0; j < star; ++j)
                Console.Write("*");
            // Draw right side spaces
            for (int j = 0; j < space; ++j)
                Console.Write(" ");
            // Space decreases by one, starts adds 2 every cicle
            --space;
            star += 2;

            // And go to new line
            Console.Write("\n");
        }
    }
    static void EqualiteralTriangleReverse(int n) {
        // We subtract one from n and start with 2 spaces
        // Because we dont need an intersection
        // Between Upper half and lower half
        --n;
        int space = 2;
        int star  = 2 * n - 1;
        for (int i = 0; i < n; ++i) {
            // Draw left side spaces
            for (int j = 0; j < space; ++j)
                Console.Write(" ");
            // Draw stars
            for (int j = 0; j < star; ++j)
                Console.Write("*");
            // Draw right side spaces
            for (int j = 0; j < space; ++j)
                Console.Write(" ");
            // Space decreases by one, starts adds 2 every cicle
            ++space;
            star -= 2;
            // And go to new line
            Console.Write("\n");
        }
    }
    // Draw a romb of size N
    static void Romb(int n) {
        // For drawing a romb we only need
        // To draw 2 Rqualiteral Triangles
        // In mirrored (reversed) order
        //    *
        //   ***
        //  *****     FIRST  HALF
        //___________
        //  *****     SECOND HALF
        //   ***
        //    *


        EqualiteralTriangle(n);
        EqualiteralTriangleReverse(n);
    }
    static void Main() {
        Console.Write("Input size of the shapes: ");
        int.TryParse(Console.ReadLine(), out int n);
        Console.WriteLine("Rectangle:\n");
        Rectangle(n, n * n - 2 * n);
        Console.WriteLine("Right angled triangle:\n");
        RightTriangle(n);
        Console.WriteLine("Equaliteral triangle:\n");
        EqualiteralTriangle(n);
        Console.WriteLine("Rombus:\n");
        Romb(n);
    }
} // Wow this code is 100 lines long :)
