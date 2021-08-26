using System;

class Employee {
    private string Surname { get; set; }
    private string Name    { get; set; }

    public string Position { get; set; }
    public double Earning  { get; set; }

    private double GetTax() {
        // 13% tax = 1% Pension found
        // Earnign = 100%
        // x     = 14%
        // x = Earning * 14 / 100
        return Earning * (13.0 + 1.0) / 100.0;
    }

    public void GetInfo() {
        Console.WriteLine($"Surname: {Surname}\nName: {Name}\nPosition: {Position}\nEarnings: {Earning}\nTax: {GetTax()}");
    }

    // Default constructor
    public Employee() {

    }

    // Custom constructor
    public Employee(string surname, string name) {
        Surname = surname;
        Name = name;
    }
}
class Program {
    static void Main() {
        Console.Write("What is your surname ?: "); string surname = Console.ReadLine();
        Console.Write("What is your name ?: "); string name = Console.ReadLine();

        Console.Write("What is your position: "); string pos = Console.ReadLine();
        Console.Write("How much do your earn in 1 year ?: "); double earn = double.Parse(Console.ReadLine());

        Employee me = new Employee(surname, name);

        me.Position = pos;
        me.Earning = earn;

        me.GetInfo();
    }

}
