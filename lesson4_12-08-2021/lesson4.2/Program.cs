using System;

class Program {
    // Draw a rectangle of size n * m
    static void rectangle(int n, int m) {
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
        // Division by 2 using bit manipulation 
        // This is done for preformance and is equal to n / 2
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
    // Draw a romb of size N
    static void romb(int n) {
    }
    static void Main() {
        romb(5);
    }
}
