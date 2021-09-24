using System;
using System.Data.SqlClient;
static class SQL
{
    static private string cnnStr = "Data Source=localhost;Initial Catalog=AlifBank;User ID=sa;Password=qwerty112!";

    static private void CreateAccount(string login, string pass, string firstName, string lastName, int age, int gender)
    {
        var insertQuery =
            "insert into [dbo].[Accounts]" + 
            "([Login], [Password], [FirstName], [LastName], [Age], [Gender])" +
            "VALUES (@login, @password, @firstName, @lastname, @age, @gender)";

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
                cmd.CommandText = insertQuery;
                /* Add parameters */
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", pass);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@gender", gender);

                /* Try to run the command */

                int result = 0;
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (Exception ex) {
                    IO.Print(ex.Message, ConsoleColor.Red);
                    IO.Print("Cannot execute query !", ConsoleColor.Red);
                }

                if (result > 0) {
                    IO.Print($"Account {login} created successfully !", ConsoleColor.Green);
                } else {
                    IO.Print($"Cannot create account !", ConsoleColor.Red);
                }
            }
        }
    }
    static public void Register() {
        var phoneNumber = IO.Get<string>("Input your number: ");
    }

    static public bool ExistAccount(string login) {
        string checkQuery = 
            "SELECT Login FROM [Accounts] WHERE [Login] = @login";
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
                cmd.CommandText = checkQuery;
                /* Add parameters */
                cmd.Parameters.AddWithValue("@login", login);

                /* Try to run the command */
                SqlDataReader result = null;
                try {
                    result = cmd.ExecuteReader();
                } catch (Exception ex) {
                    IO.Print(ex.Message, ConsoleColor.Red);
                    IO.Print("Cannot execute query !", ConsoleColor.Red);
                }

                if (result.HasRows) {
                    IO.Print($"Account {login} found successfully !", ConsoleColor.Green);
                    return true;
                } else {
                    IO.Print($"Account not found!", ConsoleColor.Red);
                }
            }
        }
        return false;
    }
}

public class Account {
    int Id          { get; set; }
    string Login    { get; set; }
    string Password { get; set; }
    string FistName { get; set; }
    string LastName { get; set; }
    int age         { get; set; }
    int gender      { get; set; }
}

public class Transaction {
    int Id          { get; set; }
    int AccountId   { get; set; }
    decimal Amount  { get; set; }
}
