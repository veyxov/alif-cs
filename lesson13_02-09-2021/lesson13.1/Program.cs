using System;
class Worker {
    public string name;
    public int age;
    public decimal salary;
    
    public void GetInfo() {
        Console.WriteLine($"Name: {name}\nAge: {age}\n salary: {salary}");
    }
    
    public Worker () {}
    public Worker (string name, int age, int salary) {
        this.name = name;
        this.age = age;
        this.salary = salary;
    }
}
class Program {
  public static void Main() {
      Woker emp = new Woker("Worker", 20, 2500);
      emp.GetInfo();
  }
}
