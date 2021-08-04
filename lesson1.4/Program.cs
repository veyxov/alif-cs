using System;
using System.Data;
using System.Diagnostics;

class Program {
    // Implementation using strings
    // For using with Console.ReadLine():
    //     Console.WriteLine(reverse(Console.ReadLine());
    static void reverse(string s) {
        for (int i = s.Length - 1; i >= 0; --i) 
            Console.Write(s[i]);
    }


    // A extended version of reverse
    // that supports *any number length
    //
    // *The length is limited to the type (int)
    static void reverseAnyLen(int n) {
        while (n > 0) {
            Console.Write(n % 10);
            n /= 10;
        }
        Console.Write('\n');
    }
    
    static void reverse1(int n) {
        // The number should by 2 digits long
        Debug.Assert(n >= 10 && n <= 99); 
        Console.WriteLine($"{n % 10}{n / 10}");
    }

    // Main solution

    static int reverse(int n) {
        return ((n % 10) * 10 + n / 10);
    }

    static void Main() {
        reverse(23);
        reverse1(41);
        reverseAnyLen(1234);
        reverse("123456789");
    }
}
