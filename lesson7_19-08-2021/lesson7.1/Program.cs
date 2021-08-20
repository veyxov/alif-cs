using System;

class Rectangle {
    // Sides a and b of rectangle
    private double side1, side2;

    public double AreaCalculator() {
        // Area of rectangle = a * b
        return side1 * side2;
    }

    public double PerimeterCalculator() {
        // Perimeter of rectangle = 2 * (a + b)
        return 2 * (side1 + side2);
    }

    // This propriety returns the Area of the Rectangle
    public double Area {
        get {
            return AreaCalculator();
        }
    }

    // This propriety returns the Perimeter of the Rectangle
    public double Perimeter {
        get {
            return PerimeterCalculator();
        }
    }

    // Defauld constructor
    public Rectangle() {

    }
    // Custom constructor
    public Rectangle(double side1, double side2) {
        this.side1 = side1;
        this.side2 = side2;
    }
}
class Program {
    static void Main() {
        Console.Write("Input the first side(a): ");
        double.TryParse(Console.ReadLine(), out double a);

        Console.Write("Input the second side(b): ");
        double.TryParse(Console.ReadLine(), out double b);

        // Instance of Rectangle Class
        Rectangle rect = new Rectangle(a, b);

        Console.WriteLine($"Perimeter: {rect.Perimeter}\nArea: {rect.Area}");
    }
}
