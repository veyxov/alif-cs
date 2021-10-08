using System;
using System.Collections.Generic;

class Client
{
    public int Id { get; set; }
    public string Name { get; set; }

    private decimal Balance { get; set; }
    private object locker = new object();

    /* Changes current balance to new */
    public void UpdateBalance(decimal newBalance) {
        IO.Debug($"Changing balance: {Balance} -> {newBalance}");
        Balance = newBalance;
    }

    /* Constructors */
    public Client() {} //default constructor
}

class Clients
{
    // The pseudo-database
    static private List<Client> clients = new List<Client>();

    /* Finds client by ID; returns null if not found */
    private Client GetClientById(int id) {
        foreach (var i in clients)
            if (i.Id == id) return i;

        return null;
    }
    /* Adds newClient to the clients list */
    public void Insert(Client newClient) {
        clients.Add(newClient);
        IO.Debug($"Created new client {newClient.Id}");
    }
    /* Delete client using ID */
    public void Update(int clientId, decimal newBalance) {
        var curClient = GetClientById(clientId);
        /* Check for errors */
        if (curClient == null) {
            IO.Print("Account not found !", ConsoleColor.Red);
            return;
        }

        /* Update the balance, if account was found */
        curClient.UpdateBalance(newBalance);
    }
    public void Delete() {

    }
    public void Select() {

    }
}

class Program
{
    static public List<Client> clients = new List<Client>();
    static void Main()
    {

    }
}
