using System;

class Human {
    public string name;
    public int age;
    public float weight;
    public string work;

    private bool hasJob;
    private bool isAlive;
    private bool isMale;
    private int happiness;

    public Human() {

    }

    public Human(string name, int age) {
        this.name = name;
        this.age = age;
    }

    public GetInfo() {
        Console.WriteLine($"name: {name} age: {age}");
    }

    public void Eat(string food) {
        Console.WriteLine($"{name} ate {food}");
        happiness++; // :)
    }
    public void ChangeName(string newName) {
        this.name = newName;
    }
}

class Program {
    static void Main() {
        Human me = new Human("Ismoil", 17);
        me.GetInfo();
    }
}
