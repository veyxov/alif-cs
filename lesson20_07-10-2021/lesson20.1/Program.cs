using System;
using System.Threading;
using System.Collections.Generic;

public static class Globals
{
    public static bool otherNotRunning = true;
}

class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal LastChange { get; set; }

    public decimal Balance { get; set; }
    private object locker = new object();

    public void GetInfo() {
        IO.Print($"Id: {Id} Balance: {Balance}");
    }
    /* Changes current balance to new */
    public void UpdateBalance(decimal newBalance) {
        IO.Debug($"Changing balance: {Balance} -> {Balance + newBalance}");
        Balance += newBalance;

        /* Save the last changed value */
        LastChange = newBalance;
    }

    /* Constructors */
    public Client() { Balance = 0; } //default constructor

    public void GetBalanceInfo() {
        // Variables for constructing result string
        ConsoleColor curColor;
        string sign;
        if (LastChange < 0) {
            curColor = ConsoleColor.Red;
            sign = "-";
        }
        else {
            curColor = ConsoleColor.Green;
            sign = "+";
        }
        IO.Print($"Account: {Name}" + sign + $" {Balance} -> {Balance + LastChange} ({LastChange})", curColor);
    }
}

class Program
{
    /* Finds client by ID; returns null if not found */
    /* Adds newClient to the clients list */
    static private void Insert(Client newClient) {
        clnts.Add(newClient);
        IO.Debug($"Created new client {newClient.Id}");
    }
    /* Delete client using ID */
    static public void Update() {
        var clientId = IO.GetInput<int>("Input the client id: ");
        var newBalance = IO.GetInput<decimal>("Input new balance: ");
        var curClient = GetClientById(clientId);

        if (curClient != null) {  
            /* Update the balance, if account was found */
            curClient.UpdateBalance(newBalance);
        } else {
            IO.Print("Account not found !", ConsoleColor.Red);
        }
        Globals.otherNotRunning = true;
    }
    static public void Delete() {
        int clientId = IO.GetInput<int>("Input the client id: ");
        var curClient = GetClientById(clientId);
        if (clnts.Remove(curClient))
            IO.Debug("User deleted !", ConsoleColor.Green);
        Globals.otherNotRunning = true;
    }
    static public void Select() {
        for (int i = 0; i < clnts.Count; ++i) {
            IO.Print($"{i} - {clnts[i].Balance}", ConsoleColor.Green);
        }
        int indx = IO.GetInput<int>("Choose the index: ");

        if (indx < 0 || indx > clnts.Count) {
            IO.Print("Invalid indx", ConsoleColor.Red);
        } else {
            clnts[indx].GetInfo();
        }
        Globals.otherNotRunning = true;
    }
    static public void Create() {
        int id = IO.GetInput<int>("Input the id: ");
        if (GetClientById(id) != null) {
            IO.Print("This account alrady exist !", ConsoleColor.Red);
        }
        Insert(new Client() { Id = id, Name = "Test" });
        Globals.otherNotRunning = true;
    }

    static private Client GetClientById(int id) {
        foreach (var i in clnts)
            if (i.Id == id) return i;

        // Not found
        IO.Print("Account not found !", ConsoleColor.Red);
        return null;
    }
    private const int Period = 1000 * 25;

    // The pseudo-database
    static public List<Client> clnts = new List<Client>();

    static void Main()
    {
        var timer = new Timer(Refresh, null, 0, Period);

        IO.Print("1.Create\t2.Delete\t3.Update\t4.Select");
        bool running = true;

        var mainThread = Thread.CurrentThread;

        while (running) {
            int cmd = 0;
            if (Globals.otherNotRunning) cmd = IO.GetInput<int>("Enter the command ->: ");
            switch (cmd) {
                case 1:
                    Globals.otherNotRunning = false;
                    Thread CreateThread = new Thread(Create);
                    CreateThread.Start();
                    break;
                case 2:
                    Globals.otherNotRunning = false;
                    Thread DeleteThread = new Thread(Delete);
                    DeleteThread.Start();
                    break;
                case 3:
                    Globals.otherNotRunning = false;
                    Thread UpdateThread = new Thread(Update);
                    UpdateThread.Start();
                    break;
                case 4:
                    Globals.otherNotRunning = false;
                    Thread SelectThread = new Thread(Select);
                    SelectThread.Start();
                    break;
                default:
                    Globals.otherNotRunning = true;
                    Console.Clear();
                    IO.Print("1.Create\t2.Delete\t3.Update\t4.Select");
                    break;
            }
        }
        Console.ReadLine(); // There was no mention in the lesson, that this is neccessary :(
    }
    static public void Refresh(object obj) {
        foreach (var i in clnts) {
            if (i.LastChange != 0) {
                i.GetBalanceInfo();
                i.LastChange = 0;
            }
        }
    }
}
