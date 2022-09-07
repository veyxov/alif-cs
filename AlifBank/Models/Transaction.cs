using System;

public class Transaction
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; }
    public int Limit { get; set; }
    public DateTime Created_At { get; set; }
}

