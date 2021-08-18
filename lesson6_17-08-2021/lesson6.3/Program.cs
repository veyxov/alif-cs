using System;
// This is an example of inheritance

// Class for a *angle

class Shape {
    public double a;
    public double b;
}

// The triangle class adds a new side
class Triangle : Shape {
    public double c;

    public double GetPerimeter() {
        return a + b + c;
    }
    // This class implements its 
    // own Area-calculation method
    public double GetArea() {
        double s = GetPerimeter() / 2.0;
        return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
    }

    public Triangle() {

    }
    public Triangle(double a, double b, double c) {
        this.a = a;
        this.b = b;
        this.c = c;
    }
}

class Rectangle : Shape {
    public double GetPerimeter() {
        return 2 * (a + b);
    }
    public double GetArea() {
        return a * b;
    }
    public Rectangle() {

    }
    public Rectangle(double a, double b) {
        this.a = a;
        this.b = b;
    }
}

class Program {
    static void Main() {
        double a = 5.0 ;
        double b = 5.0 ;
        double c = 10.0;
        Triangle tri = new Triangle(a, b, c);
        Console.WriteLine(tri.GetArea());

        Rectangle rec = new Rectangle(a, c);

        Console.WriteLine(rec.GetArea());
    }
}
