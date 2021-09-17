using System;
using System.Data.SqlClient;
//using System.Threading;

/* Input output helper */
static class IO {
    static private bool DEBUG    = true;
    static private bool COLORFUL = true;

    static public void Print(string what, ConsoleColor color = ConsoleColor.White, bool newLine = true) {
        Console.ForegroundColor = COLORFUL ? color : ConsoleColor.White;
        Console.Write(what);

        if (newLine) Console.Write("\n");

        Console.ResetColor();
    }

    /* This function will not output to the terminal if DEBUG is not set */
    static public void Debug(string what, ConsoleColor color = ConsoleColor.DarkGray, bool newLine = true) {
        if (!DEBUG) return;

        Print(what, color, newLine);
    }

    /* This method prompts WHAT and gets the input from Console*/
    static public string GetString(string what = "") {
        IO.Print(what, ConsoleColor.Yellow, false);
        var input = Console.ReadLine();
        return input;
    }
}

class Account {
    public int       Id          { get; set; }
    public string    Acc         { get; set; }
    public int       Is_Active   { get; set; }
    public DateTime  Created_At  { get; set; }
    public DateTime? Updated_At  { get; set; } // This can be null

    public void GetInfo() { IO.Print($"{Id}\t{Acc}\t{Is_Active}\t{Created_At}\t{Updated_At}", ConsoleColor.Blue); }

    // Default constructor
    public Account() { }

    public Account(string acc, int is_active, DateTime created_at) {
        Acc = acc;
        Is_Active = is_active;
        Created_At = created_at;
        IO.Debug("Created new account instance.", ConsoleColor.Green);
    }

    class Transaction { 
        public int      Id           { get; set; }
        public int      Account_Id   { get; set; }
        public decimal  Amount       { get; set; }
        public DateTime Created_At   { get; set; }
    }

    class Program {
        static decimal GetAccountBalance(string account, SqlConnection cnn) {
            decimal balance = 0;

            cnn.Open(); IO.Debug("Connection opened !");
            var command = cnn.CreateCommand();

            // I think parameters are owerhead in this situation (only 1 parameter)
            command.CommandText = $"select sum([Amount]) from [dbo].[Transactions] LEFT JOIN [dbo].[Account] ON [Account].[Id] = [Transactions].[Account_Id] WHERE '{account}'";

            var data = command.ExecuteReader();

            while (data.Read()) balance = !string.IsNullOrEmpty(data.GetValue(0)?.ToString()) ? data.GetDecimal(0) : 0;

            return balance;
        }
        /* NOTE: Why ref ? */
        static void CreateAccount(ref SqlConnection cnn) {
            var curAcc = new Account(IO.GetString("Input Account number in xxxxx format: "), 1, DateTime.Now);

            try {
                if (String.IsNullOrEmpty(curAcc.Acc)) throw new Exception("Account can not be empty !");
                /* If OK then, open the connection */
                cnn.Open(); IO.Debug("Connection opened !");
                try {
                    var command = cnn.CreateCommand();
                    /* The command to execute */
                    command.CommandText = "INSERT INTO [dbo].[Account]([Account], [Is_Active], [Created_At]) VALUES (@Acc, @IsActive, @Created_At)";
                    command.Parameters.AddWithValue("@Acc", curAcc.Acc);
                    command.Parameters.AddWithValue("@IsActive", curAcc.Is_Active);
                    command.Parameters.AddWithValue("@Created_At", curAcc.Created_At);
                    var res = command.ExecuteNonQuery();
                    if (res <= 0) throw new Exception("The account was not created !");
                    /* If OK print this ...*/
                    IO.Print($"Account {curAcc.Acc} created successfully !", ConsoleColor.Green);

                    command.Parameters.Clear(); IO.Debug("Parameters cleared !");
                } catch (Exception ex) {
                    IO.Print(ex.Message); IO.Print("Account was not created !", ConsoleColor.Red);
                } finally {
                    /* Close the connection in every case */
                    IO.Debug("Connection closed !"); cnn.Close();
                }
            } catch (Exception ex) {
                IO.Print(ex.Message, ConsoleColor.Red);
            }
        }
        static void ShowAccounts(ref SqlConnection cnn) {
            try {
                cnn.Open(); IO.Debug("Connection opened !");

                var command = cnn.CreateCommand();

                command.CommandText = "SELECT * FROM Account";
                var data = command.ExecuteReader();

                /* Output the data */
                IO.Print("Id\tAccount\tActive\tCreation Date\t\tUpdate Date");
                while (data.Read()) {
                    var tempAcc        = new Account();
                    tempAcc.Id         = int.Parse(data["Id"].ToString());
                    tempAcc.Acc        = data["Account"].ToString();
                    tempAcc.Is_Active  = int.Parse(data["Is_Active"].ToString());
                    tempAcc.Created_At = DateTime.Parse(data["Created_At"].ToString());

                    // Temporary store UpdatedAt
                    var tUpdatedAt = data["Updated_At"].ToString();
                    tempAcc.Updated_At = !String.IsNullOrEmpty(tUpdatedAt) ? DateTime.Parse(tUpdatedAt) : null;

                    /* Print to console (terminal)*/
                    tempAcc.GetInfo();
                }
                data.Close(); IO.Debug("Reader closed !");
            } catch (Exception ex) {
                // TODO: Complete this block
                IO.Print(ex.Message); IO.Print("An error occured !", ConsoleColor.Red);
            } finally {
                cnn.Close();
                IO.Debug("Connection closed !");
            }
        }
        static void Transfer(string from, string to, decimal amount, ref SqlConnection cnn) {
            try {
                cnn.Open(); IO.Debug("Connection opened !");

                if (String.IsNullOrEmpty(from) || String.IsNullOrEmpty(to)) {
                    IO.Print("Invalid accounts !", ConsoleColor.Red);
                }

                IO.Debug("Transfer ended.");
            } catch (Exception ex) {
                // TODO: Complete this block
                IO.Print(ex.Message);
                IO.Print("An error occured !", ConsoleColor.Red);
            } finally {
                cnn.Close(); IO.Debug("Connection closed !");
            }
        }

        static void Main() {
            /* Initializing SQL Connection */
            IO.Print("Starting SQL connection ...", ConsoleColor.Yellow);
            var builder = new SqlConnectionStringBuilder();

            /* Change these to your needs */
            builder.DataSource = "localhost";
            builder.UserID = "sa";
            builder.Password = "qwerty112!";
            builder.InitialCatalog = "DZ";

            var cnn = new SqlConnection(builder.ToString());

            /* Testing system */
            bool running = true;
            while (running) {
                IO.Print("Pess any key ...", ConsoleColor.Cyan, false);
                Console.ReadLine();
                Console.Clear();

                /* Get user choice */
                IO.Print("1. Create Account\t 2. Show Accounts\t 3. Transfer");
                var cmd = IO.GetString("->: ");

                switch (cmd) {
                    case "1":
                        CreateAccount(ref cnn);
                        break;
                    case "2":
                        ShowAccounts(ref cnn);
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    default:
                        IO.Print("Invalid command !", ConsoleColor.Red);
                        running = false;
                        break;
                }
            }
        }
    }
}
