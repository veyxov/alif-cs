using System;

// This is a simple game using classes in C#

class Player {
    public string name;
    public int level;
    public bool isAlive;
    
    // This variable can
    // only be changed
    // inside the class
    private int health;
    private int force;

    public void Attack(Player opponent) {
        int PAUSE = 1000;
        // You demage the opponent with your force
        opponent.health -= this.force;
        Console.ForegroundColor = ConsoleColor.Red; 
        Console.WriteLine($"{this.name} is attacking {opponent.name} !");
        System.Threading.Thread.Sleep(PAUSE + 500); // Pause
        Console.ResetColor();
        Console.WriteLine($"{opponent.name} recieved {this.force} demage, health = {opponent.health}");
        opponent.Check(opponent);
        System.Threading.Thread.Sleep(PAUSE + 500); // Pause
    }

    private void Check(Player player) {
        if (player.isAlive && player.health < 0) {
            Console.WriteLine($"{player.name} is dead !");
            player.isAlive = false;
        }
    }
    public void GetInfo() {
        // Set the color according to the Player health
        if (health < 50) { Console.ForegroundColor = ConsoleColor.Red; }
        else             { Console.ForegroundColor = ConsoleColor.Green; }
        if (this.isAlive == true)
            Console.WriteLine($"Player: {name}\tLevel: {level}\tHealth: {health}\tForce {force}");
        else
            Console.WriteLine($"Player {name} is dead x(");
        Console.ResetColor();
    }
    // Default constructor
    public Player() {
        name = "Player";
        level = 0;
        health = 100;
        force = 0;
        isAlive = true;
    }
    // Full constructor for a Player
    public Player(string name, int level, int health, int force) {
        this.name = name;
        this.level = level;
        this.health = health + level; // You get level + initialHealth, HP
        this.force = force * level; // You get level * initialFoce, strength
        this.isAlive = true;
    }
}

class Program {
    static void Main() {
        int PAUSE = 1000;
        // Amount of time in ms for pausing moments

        string[] enemyNames = {"C", "Python", "Java", "C++", "GoLang", "JavaScript", "Perl", "PHP", "F#"};
        // Random digit generator
        Random rand = new Random();
        // Initializing the player
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Welcome to the game !");
        Console.Write("What is your name: ");  string name = Console.ReadLine();
        Console.Write("What is your level: "); int.TryParse(Console.ReadLine(), out int level);
        Console.Write("What is your sila: ");  int.TryParse(Console.ReadLine(), out int force);
        Console.ResetColor();

        Player me = new Player(name, level, 100, force);
        me.GetInfo();

        Console.Write("\nHow many enemies do you want to fight with ?: ");
        int.TryParse(Console.ReadLine(), out int enemyCount);

        Player[] enemies = new Player[enemyCount]; // Creating an array for enemies
        Console.WriteLine("Your enemies :");
        for (int i = 0; i < enemyCount; ++i) {
            // Fill enemies with random data

            // This picks a random name from array of names
            // FIX: 1 name can be picked several
            string rName =  enemyNames[rand.Next(enemyNames.Length)];
            int rLevel = rand.Next(1, 10);
            int rHealth = rand.Next(10, 100);
            int rForce = rand.Next(1, 40);

            enemies[i] = new Player(rName, rLevel, rHealth, rForce);
            enemies[i].GetInfo();
        }

        int turn = 0;
        // Main game loop
        while (true) {
            // Chech if I am alive or there are some emenies left
            if (me.isAlive == false) {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("GAME OVER !");
                break;
            }
            bool areThereAlive = false;
            for (int i = 0; i < enemyCount; ++i)
                if (enemies[i].isAlive == true) {
                    areThereAlive = true;
                    break;
                }
            if (!areThereAlive) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("CONGRATULATIONS !\nYou win !");
                break;
            }

            // Is your turn or enemies turn ?
            if (turn % 2 == 0) {
                Console.Write("Your turn !");
                // Rechoose if choosed incorrectly
choosing:
                Console.WriteLine("Who to attack ?");
                for (int i = 0; i < enemyCount; ++i) {
                    Console.Write($"[{i}] ");
                    enemies[i].GetInfo();
                }
                Console.Write("Enter enemy index: ");
                int.TryParse(Console.ReadLine(), out int indx);

                if (indx < 0 || indx >= enemies.Length) goto choosing;

                me.Attack(enemies[indx]);
            } else {
                // Enemies turn :(
                Console.WriteLine("\n\nEnemies TURN !\n\n");
                System.Threading.Thread.Sleep(PAUSE + 500); // Pause
                // Randomly choose how many will attack you 
                int attackers = rand.Next(enemies.Length);
                Console.WriteLine($"{attackers} enemies will attack you !");
                System.Threading.Thread.Sleep(PAUSE + 500); // Pause

                while (attackers-- > 0) {
                    // Randomly choose who will attack you
                    enemies[rand.Next(enemies.Length)].Attack(me);
                    System.Threading.Thread.Sleep(PAUSE); // Pause

                    if (me.isAlive == false) break;
                }
            }
            ++turn;
        }
        Console.WriteLine("Thank you for playing !");
        return;
    }
}
