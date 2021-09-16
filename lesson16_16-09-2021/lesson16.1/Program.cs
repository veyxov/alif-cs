using System;
using System.SqlClient;

class Account {
    public int      Id          { get; set; }
    public string   Acc         { get; set; }
    public int      Is_Active   { get; set; }
    public DateTime Created_At  { get; set; }
    public DateTime Updated_At  { get; set; }
}

class Transaction { 
    public int      Id           { get; set; }
    public int      Account_Id   { get; set; }
    public decimal  Amount       { get; set; }
    public DateTime Created_At   { get; set; }
}

class Program {
    static void CreateAccount() {

    }
    static void ShowAccounts() {

    }
    static void Transfer(string from, string to, decimal amount) {

    }
    static void Main() {

    }
}
