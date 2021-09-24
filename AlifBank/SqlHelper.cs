using System;
using System.Data.SqlClient;
static class SQL
{
    static private string cnnStr = "Data Source=localhost;Initial Catalog=AlifBank;User ID=sa;Password=qwerty112!";

    static public void CreateAccount(string login, string pass, string firstName, string lastName, int age, int gender)
    {
        var insertQuery =
            "insert into [dbo].[Accounts]" + 
            "([Login], [Password], [FirstName], [LastName], [Age], [Gender])" +
            "VALUES (@login, @password, @firstName, @lastname, @age, @gender)";

        using (var cnn = new SqlConnection(cnnStr)) {
            using (var cmd = cnn.CreateCommand()) {
                /* Open the connection */
                cnn.Open();
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

                var result = cmd.ExecuteNonQuery();
                IO.Debug($"{result}");

                if (result > 0) {
                    IO.Print($"Account {login} created successfully !", ConsoleColor.Green);
                } else {
                    IO.Print($"Cannot create account !", ConsoleColor.Red);
                }
            }
        }
    }
}
