using System;
using System.Data.SqlClient;

class SqlHelper {
    // Enable or disable VERBOSE logging
    private bool DEBUG = true;
    //
    // We will initilize this later
    SqlConnection cnn = null;

    // This is for storing the global connection string
    public string cnnString;
    public string dbName = "Person";

    public void Print(string what, ConsoleColor color = ConsoleColor.Black) {
        if (!DEBUG) return;
        Console.ForegroundColor = color; // Set the color
        // Console.ForegroundColor = ConsoleColor.White; //UNCOMMENT THIS FOR LESS COLORFUL OUTPUT !
        Console.WriteLine(what); // Print the content
        Console.ResetColor(); // Reset color
    }

    // Get user input with custom output
    public void GetUserInfo(string output, out string rez) {
        Console.Write(output);
        rez = Console.ReadLine();
    }

    // Initializs a SQL connection
    public void StartConnection() {
        Print("Starting SQL Connection ...", ConsoleColor.Red);
        // Declare the connection
        cnn = new SqlConnection(cnnString);
        // Start SQL connection
        cnn.Open();
        Print("Connected !", ConsoleColor.Green);
    }

    public void TerminateConnection() {
        Print("Closing the SQL connection ...", ConsoleColor.Red);
        cnn.Close(); // Close !
        Print("Closed.", ConsoleColor.Green);
    }

    public void Insert() {
        GetUserInfo("Input time: ", out string time);
        GetUserInfo("Input LastName: ", out string lastN);
        GetUserInfo("Input FirstName: ", out string firstN);
        GetUserInfo("Input MiddleName: ", out string middleN);

        Print($"Inserting {lastN} ...", ConsoleColor.DarkMagenta);
        var Query = $"INSERT INTO {dbName}(BirthDate, LastName, FirstName, MiddleName) VALUES (" +
            $"'{time}'," +
            $"'{lastN}',"  +
            $"'{firstN}'," +
            $"'{middleN}')";

        SqlCommand command = new SqlCommand(Query, cnn);
        // Exe the command

        int changes = command.ExecuteNonQuery();
        if (changes > 0) Print($"Inserted successfully !", ConsoleColor.Green);
        else             Print($"Inserting failed !", ConsoleColor.Red);
    }

    public void Update() {
        // First output all
        SelectAll();
        // Get user input
        GetUserInfo("Choose Id for editing: ", out string ID);

        // TODO: Handle nonExisting person reference

        GetUserInfo("Enter birth date in YYYYMMDD: ", out string BD);
        GetUserInfo("Enter first name: ", out string FirstName);
        GetUserInfo("Enter last name: ", out string LastName);
        GetUserInfo("Enter middle name: ", out string MiddleName);

        string Query = String.Format("UPDATE Person SET BirthDate='{0}', LastName='{1}', FirstName='{2}', MiddleName='{3}' WHERE Id={4}", BD, LastName, FirstName, MiddleName, ID);

        SqlCommand command = new SqlCommand(Query, cnn);
        int changes = command.ExecuteNonQuery();

        if (changes > 0) Print("Changed !", ConsoleColor.Green);
        else             Print("Changing failed!", ConsoleColor.Red);
    }

    // Deletes a Person with Id, id
    public void Delete() {
        // First output the table
        SelectAll();
        GetUserInfo("Input ID: ", out string str_id);
        int id = int.Parse(str_id);
        var Query = $"DELETE FROM {dbName} WHERE Id = {id}";
        SqlCommand command = new SqlCommand(Query, cnn);
        
        int changes = command.ExecuteNonQuery();
        if (changes > 0) Print($"Deleted successfully !", ConsoleColor.Green);
        else             Print($"Deletion failed !", ConsoleColor.Red);
    }

    // Internal method used by wrappers
    private void select(string ID) {
        var Query = $"SELECT * FROM {dbName} WHERE Id = {ID}";
        SqlCommand cmd = new SqlCommand(Query, cnn);

        var reader = cmd.ExecuteReader();

        // Output
        while (reader.Read()) {
            // Get values
            var id = reader.GetValue(0).ToString();
            var lastN = reader.GetValue(1);
            var firstN = reader.GetValue(2);
            var middleName = reader.GetValue(3);
            var birth = reader.GetValue(4);
            Console.WriteLine($"Id: {id}\tSurname: {lastN}\tMiddleName: {middleName}\tName: {firstN}\tBirthday: {birth}");
        }

        // NOTE: Close the data reader
        reader.Close();
    }

    // Wrapper method, using Select
    public void SelectAll() {
        Print("The table: ", ConsoleColor.Magenta);
        select("Id"); // Id = Id is always TRUE, it is like 1 = 1
    }

    // Wrapper method, using Select
    public void SelectById() {
        GetUserInfo("Input ID: ", out string ID);
        Print($"Person with Id = {ID}: ", ConsoleColor.Magenta);
        select(ID);
    }

    // Defalut constructor
    public SqlHelper() {  }

    public SqlHelper(string cnnString) {
        this.cnnString = cnnString;
    }
}

class Program {
    static void Main() {
        Console.WriteLine("Start...");
        // SQL server data
        
        // Please user your data and RENAME DATABASE TO YOURS (ON LINE 13) !
        SqlHelper SQL = new SqlHelper(@"Data Source=localhost;Initial Catalog=DZ; User=sa; Password=qwerty112!");

        SQL.StartConnection();

        // Interactive testing system
        bool running = true;
        while (running) {
            Console.WriteLine("Choose 1-insert 2-delete 3-update 4-selectID 5-selectALL");
            var cmd = Console.ReadLine();

            // Run commands according to user input
            switch (cmd) {
                case "1":
                    SQL.Insert();
                    break;
                case "2":
                    SQL.Delete();
                    break;
                case "3":
                    SQL.Update();
                    break;
                case "4":
                    SQL.SelectById();
                    break;
                case "5":
                    SQL.SelectAll();
                    break;
                default:
                    running = false;
                    break;
            }
        }
END:
        SQL.TerminateConnection();
    }
}
