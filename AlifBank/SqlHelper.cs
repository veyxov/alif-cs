using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
static class SQL
{
    static private string cnnStr = "Data Source=localhost;Initial Catalog=AlifBank;User ID=sa;Password=qwerty112!";
    //static private string cnnStr = "Server=localhost;Database=AlifBank;Trusted_Connection=True";

    static public void DepositToAccount(string login, decimal amount)
    {
        if (amount <= 0) throw new Exception("Deposit amount needs to be positive");
        // No limit needed when depositing
        makeTransaction(login, amount, "Deposit", 0);
    }

    static public void CreditToAccount(string login, decimal amount, int limit)
    {
        makeTransaction(login, amount, "Credit", limit);
    }
    static private void makeTransaction(string login, decimal amount, string type, int limit)
    {
        if (!ExistAccount(login)) throw new Exception($"Account {login} does not exist");
        if (amount <= 0) throw new Exception("Amount should be positive");
        if (IsAdmin(login)) throw new Exception("Admin cannot interact with money");

        try
        {
            var insertQuery = "INSERT INTO Transactions ([Account_Id], [Amount], [Type], [Limit], [Created_At]) VALUES(@accountID, @amount, @type, @limit, @createdAt)";
            using (var cnn = new SqlConnection(cnnStr)) {
                using (var cmd = cnn.CreateCommand()) {
                    /* Open the connection */
                    try {
                        cnn.Open();
                    } catch (Exception ex) {
                        throw new Exception(ex.Message);
                    }
                    /* Create the command */
                    cmd.CommandText = insertQuery;

                    cmd.Parameters.AddWithValue("@AccountId", GetIdByLogin(login));
                    if (type == "Credit") amount *= -1;
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    cmd.Parameters.AddWithValue("@createdAt", DateTime.Now);

                    /* Try to run the command */
                    int result = 0;
                    try {
                        result = cmd.ExecuteNonQuery();
                        if (result <= 0) throw new Exception("Cannot make transaction");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        } catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }
    static public bool IsAdmin(string login) { return SQL.GetAccountData(login).IsAdmin; }
    static public int GetIdByLogin(string login) { return GetAccountData(login).Id; }

    static public Account GetAccountData(string login)
    {
        var getDataQuery =
            "select * from [dbo].[Accounts] WHERE Login = @login";

        try {
            using (var cnn = new SqlConnection(cnnStr)) {
                using (var cmd = cnn.CreateCommand()) {
                    /* Open the connection */
                    try {
                        cnn.Open();
                    } catch (Exception ex) {
                        IO.Print(ex.Message, ConsoleColor.Red);
                        IO.Print("Cannot open connection !", ConsoleColor.Red);
                    }
                    IO.Debug("Connection opened !");

                    /* Create the command */
                    cmd.CommandText = getDataQuery;

                    cmd.Parameters.AddWithValue("@login", login);

                    /* Try to run the command */
                    SqlDataReader result = null;
                    try
                    {
                        result = cmd.ExecuteReader();
                        if (!result.HasRows) throw new Exception("No data !");
                    }
                    catch
                    {

                    }
                    result.Read();
                    // Create new Account instance using the data
                    return new Account()
                    {
                        Id = result.GetInt32(0),
                        Login = result.GetString(1),
                        Password = result.GetString(2),
                        FistName = result.GetString(3),
                        LastName = result.GetString(4),
                        Age = result.GetInt32(5),
                        Gender = result.GetInt32(6),
                        IsAdmin = result.GetInt32(7) == 0 ? false : true
                    };
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    static public DataTable GetAccountTransactions(string login)
    {

        var getDataQuery = 
            "select * from [dbo].[Transactions] WHERE Account_Id  = @AccountID";
        var resTable = new DataTable();

        using (var cnn = new SqlConnection(cnnStr)) {
            using (var cmd = cnn.CreateCommand()) {
                try {
                    cnn.Open();
                } catch (Exception ex) {
                    IO.Print(ex.Message, ConsoleColor.Red);
                    IO.Print("Cannot open connection !", ConsoleColor.Red);
                }
                IO.Debug("Connection opened !");

                /* Create the command */
                cmd.CommandText = getDataQuery;
                cmd.Parameters.AddWithValue("@AccountID", SQL.GetIdByLogin(login));

                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(resTable);
                cmd.Parameters.Clear();
            }
        }
        return resTable;
    }

    static public DataTable GetAccountDataAllTableSpecific(string login)
    {
        if (!ExistAccount(login)) throw new Exception($"Account {login} not found");
        return getAccountDataAllTable(login, "select * from [dbo].[Accounts] WHERE Login = @login");
    }
    static public DataTable GetAccountDataAllTableAll() { return getAccountDataAllTable("", "select * from [dbo].[Accounts]"); }

    static private DataTable getAccountDataAllTable(string login, string getDataQuery)
    {
        var resTable = new DataTable();

        using (var cnn = new SqlConnection(cnnStr)) {
            using (var cmd = cnn.CreateCommand()) {
                try {
                    cnn.Open();
                } catch (Exception ex) {
                    IO.Print(ex.Message, ConsoleColor.Red);
                    IO.Print("Cannot open connection !", ConsoleColor.Red);
                }
                IO.Debug("Connection opened !");

                /* Create the command */
                cmd.CommandText = getDataQuery;
                cmd.Parameters.AddWithValue("@login", login);

                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(resTable);
                cmd.Parameters.Clear();
            }
        }
        return resTable;
    }
    static public List<Account> GetAccountDataAll(string login)
    {
        var resAccounts = new List<Account>();
        var getDataQuery =
            "select * from [dbo].[Accounts] WHERE Login = @login";

        try {
            using (var cnn = new SqlConnection(cnnStr)) {
                using (var cmd = cnn.CreateCommand()) {
                    /* Open the connection */
                    try {
                        cnn.Open();
                    } catch (Exception ex) {
                        IO.Print(ex.Message, ConsoleColor.Red);
                        IO.Print("Cannot open connection !", ConsoleColor.Red);
                    }
                    IO.Debug("Connection opened !");

                    /* Create the command */
                    cmd.CommandText = getDataQuery;

                    cmd.Parameters.AddWithValue("@login", login);

                    /* Try to run the command */
                    SqlDataReader result = null;
                    try {
                        result = cmd.ExecuteReader();
                        if (!result.HasRows) throw new Exception("No data !");
                    } catch (Exception ex) {
                        throw new Exception(ex.Message);
                    }
                    while (result.Read())
                    {
                        resAccounts.Add(new Account() {
                            Id = result.GetInt32(0),
                            Login = result.GetString(1),
                            Password = result.GetString(2),
                            FistName = result.GetString(3),
                            LastName = result.GetString(4),
                            Age = result.GetInt32(5),
                            Gender = result.GetInt32(6),
                            IsAdmin = result.GetInt32(7) == 0 ? false : true
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return resAccounts;
    }

    static public decimal CalculateAccountBalance(string login)
    {
        var getSumQuery =
            "select SUM([Amount]) AS Balance FROM [dbo].[Transactions] WHERE [Account_Id] = @AccountID";

        decimal balance = 0;
        using (var cnn = new SqlConnection(cnnStr))
        {
            using (var cmd = cnn.CreateCommand())
            {
                /* Open the connection */
                cnn.Open();

                /* Create the command */
                cmd.CommandText = getSumQuery;
                /* Add parameters */
                cmd.Parameters.AddWithValue("@AccountID", SQL.GetIdByLogin(login));

                /* Try to run the command */

                var reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader["Balance"] == DBNull.Value) {
                            // TODO: what to do when null
                        } else {
                            balance = decimal.Parse(reader["Balance"].ToString());
                        }
                    }
                } else {
                    throw new Exception("Sum returned NULL");
                }
            }
        }
        return balance;
    }

    static public bool CreateAccount(Account acc)
    {
        if (ExistAccount(acc.Login))
        {
            throw new Exception($"Account with login {acc.Login} already exist.");
        }
        var insertQuery =
            "insert into [dbo].[Accounts]" +
            "([Login], [Password], [FirstName], [LastName], [Age], [Gender], [Is_Admin])" +
            "VALUES (@login, @password, @firstName, @lastname, @age, @gender, @isAdmin)";

        try
        {
            using (var cnn = new SqlConnection(cnnStr))
            {
                using (var cmd = cnn.CreateCommand())
                {
                    /* Open the connection */
                    try
                    {
                        cnn.Open();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    IO.Debug("Connection opened !");

                    /* Create the command */
                    cmd.CommandText = insertQuery;
                    /* Add parameters */
                    cmd.Parameters.AddWithValue("@login", acc.Login);
                    cmd.Parameters.AddWithValue("@password", acc.Password);
                    cmd.Parameters.AddWithValue("@firstName", acc.FistName);
                    cmd.Parameters.AddWithValue("@lastName", acc.LastName);
                    cmd.Parameters.AddWithValue("@age", acc.Age);
                    cmd.Parameters.AddWithValue("@gender", acc.Gender);
                    cmd.Parameters.AddWithValue("@isAdmin", acc.IsAdmin);

                    /* Try to run the command */

                    int result = 0;
                    try {
                        result = cmd.ExecuteNonQuery();
                    } catch (Exception ex) {
                        throw new Exception(ex.Message);
                    }

                    return result > 0 ? true : false;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    static public bool Auth(string login, string pass)
    {
        string authQuery = "select [Password] from [Accounts] WHERE [Login] = @login";

        using (var cnn = new SqlConnection(cnnStr))
        {
            using (var cmd = cnn.CreateCommand())
            {
                /* Open the connection */
                try
                {
                    cnn.Open();
                }
                catch (Exception ex)
                {
                    IO.Print(ex.Message, ConsoleColor.Red);
                    IO.Print("Cannot open connection !", ConsoleColor.Red);
                }
                IO.Debug("Connection opened !");

                /* Create the command */
                cmd.CommandText = authQuery;
                /* Add parameters */
                cmd.Parameters.AddWithValue("@login", login);

                /* Try to run the command */
                SqlDataReader result = null;
                try
                {
                    result = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    IO.Print(ex.Message, ConsoleColor.Red);
                    IO.Print("Cannot execute query !", ConsoleColor.Red);
                }
                string originalPassword = null;
                while (result.Read()) originalPassword = result.GetString(0);
                if (originalPassword == pass)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
    static public bool ExistAccount(string login)
    {
        string checkQuery =
            "SELECT Login FROM [Accounts] WHERE [Login] = @login";
        using (var cnn = new SqlConnection(cnnStr))
        {
            using (var cmd = cnn.CreateCommand())
            {
                /* Open the connection */
                try
                {
                    cnn.Open();
                }
                catch (Exception ex)
                {
                    IO.Print(ex.Message, ConsoleColor.Red);
                    IO.Print("Cannot open connection !", ConsoleColor.Red);
                }
                IO.Debug("Connection opened !");

                /* Create the command */
                cmd.CommandText = checkQuery;
                /* Add parameters */
                cmd.Parameters.AddWithValue("@login", login);

                /* Try to run the command */
                SqlDataReader result = null;
                try
                {
                    result = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    IO.Print(ex.Message, ConsoleColor.Red);
                    IO.Print("Cannot execute query !", ConsoleColor.Red);
                }

                if (result.HasRows)
                {
                    IO.Print($"Account {login} found successfully !", ConsoleColor.Green);
                    return true;
                }
                else
                {
                    IO.Print($"Account not found!", ConsoleColor.Red);
                }
            }
        }
        return false;
    }

    // FIX: always returns first limit
    public static Transaction GetAccountTransactionData(string login)
    {
        var getDataQuery =
            "select * from [dbo].[Transactions] WHERE Account_Id = @accountID ORDER BY Created_At desc";

        using (var cnn = new SqlConnection(cnnStr))
        {
            using (var cmd = cnn.CreateCommand())
            {
                cnn.Open();
                /* Create the command */
                cmd.CommandText = getDataQuery;
                cmd.Parameters.AddWithValue("@AccountID", SQL.GetIdByLogin(login));

                /* Try to run the command */
                SqlDataReader result = null;
                result = cmd.ExecuteReader();
                if (!result.HasRows) throw new Exception("No data !");
                result.Read();
                // Create new Account instance using the data
                return new Transaction()
                {
                    Id = result.GetInt32(0),
                    AccountId = result.GetInt32(1),
                    Amount = result.GetDecimal(2),
                    Type = result.GetString(3),
                    Limit = result.GetInt32(4),
                    Created_At = result.GetDateTime(5)
                };
            }
        }
    }
}

public class Account
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string FistName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public int Gender { get; set; }
    public bool IsAdmin { get; set; }
}

public class Transaction
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; }
    public int Limit { get; set; }
    public DateTime Created_At { get; set; }
}
