using System;
using System.Threading;
using System.Collections.Generic;

public static class Globals
{
    public static object Locker = new object();
}

class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal LastChange { get; set; }

    public decimal Balance { get; set; }

    public void GetInfo()
    {
        IO.Print($"Id: {Id} Balance: {Balance}");
    }
    /* Changes current balance to new */
    public void UpdateBalance(decimal newBalance)
    {
        IO.Debug($"Changing balance: {Balance} -> {Balance + newBalance}");
        Balance += newBalance;

        /* Save the last changed value */
        LastChange = newBalance;
    }

    /* Constructors */
    public Client() { Balance = 0; } //default constructor

    public void GetBalanceInfo()
    {
        // Variables for constructing result string
        ConsoleColor curColor;
        string sign;
        if (LastChange < 0)
        {
            curColor = ConsoleColor.Red;
            sign = "-";
        }
        else
        {
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
    static private void Insert(Client newClient)
    {
        clnts.Add(newClient);
        IO.Debug($"Created new client {newClient.Id}");
    }
    /* Delete client using ID */
    static public void Update(int clientId, decimal newBalance)
    {
        lock (Globals.Locker)
        {
            var curClient = GetClientById(clientId);
            if (curClient != null)
            {
                /* Update the balance, if account was found */
                curClient.UpdateBalance(newBalance);
            }
            else
            {
                IO.Debug("Account not found !");
            }
        }
    }
    static public void Delete(int clientId)
    {
        var curClient = GetClientById(clientId);
        if (clnts.Remove(curClient))
            IO.Debug("User deleted !", ConsoleColor.Green);
    }
    static public void Select(int indx)
    {
        lock (Globals.Locker)
        {
            for (int i = 0; i < clnts.Count; ++i)
            {
                IO.Print($"{i} - {clnts[i].Balance}", ConsoleColor.Green);
            }

            if (indx < 0 || indx > clnts.Count)
            {
                IO.Print("Invalid indx", ConsoleColor.Red);
            }
            else
            {
                clnts[indx].GetInfo();
            }
        }
    }
    static public void Create(int id, string name)
    {
        if (GetClientById(id) != null)
        {
            IO.Print("This account alrady exist !", ConsoleColor.Red);
        }
        Insert(new Client() { Id = id, Name = name });
    }

    static private Client GetClientById(int id)
    {
        foreach (var i in clnts)
            if (i.Id == id) return i;

        // Not found
        IO.Debug("Account not found !");
        return null;
    }
    private const int Period = 1000 * 1;

    // The pseudo-database
    static public List<Client> clnts = new List<Client>();

    static void Main()
    {
        var timer = new Timer(Refresh, null, 0, Period);

        var mainThread = Thread.CurrentThread;

        bool running = true;
        while (running) {
            IO.Print("1.Create\t2.Update\t3.Select\t4.Delete");

            var cmd = IO.GetInput<int>("Input your command: ");

            switch (cmd) {
                case 1:
                    var id = IO.GetInput<int>("Input id: ");
                    var name = IO.GetInput<string>("Input name: ");
                    var CreateThread = new Thread(() => Create(id, name));
                    CreateThread.Start();
                    break;
                case 2:
                    var clientId = IO.GetInput<int>("Input id: ");
                    var upd = IO.GetInput<decimal>("Input amount: ");
                    var UpdateThread = new Thread(() => Update(clientId, upd));
                    UpdateThread.Start();
                    break;
                case 3:
                    var indx = IO.GetInput<int>("Input the id: ");
                    var SelectThread = new Thread(() => Select(indx));
                    SelectThread.Start();
                    break;
                case 4:
                    var idtodel = IO.GetInput<int>("Input the id: ");
                    var DeleteThread = new Thread(() => Delete(idtodel));
                    DeleteThread.Start();
                    break;
                default:
                    running = false;
                    break;
            }
        }
        IO.GetInput<string>();
    }
    static public void Refresh(object obj)
    {
        foreach (var i in clnts)
        {
            if (i.LastChange != 0)
            {
                Console.Clear();
                i.GetBalanceInfo();
                i.LastChange = 0;
                IO.Print("1.Create\t2.Update\t3.Select");
            }
        }
    }
}
