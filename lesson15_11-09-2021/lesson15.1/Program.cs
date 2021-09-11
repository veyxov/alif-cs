using System;
using System.Data.SqlClient;

class SqlHelper {
    // We will initilize this later
    SqlConnection cnn = null;

    // This is for storing the global connection string
    public string cnnString;
    public string dbName = "Person";

    public void Print(string what, ConsoleColor color = ConsoleColor.Black) {
        Console.ForegroundColor = color; // Set the color
        // Console.ForegroundColor = ConsoleColor.White; //UNCOMMENT THIS FOR LESS COLORFUL OUTPUT !
        Console.WriteLine(what); // Print the content
        Console.ResetColor(); // Reset color
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

    public void Insert(string time, string lastN, string firstN, string middleN = "") {
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

    // Deletes a Person with Id, id
    public void Delete(int id) {
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
            var middleName = reader.GetValue(3); // Ignored
            var birth = reader.GetValue(4);
            Print($"Id: {id}\tSurname: {lastN}\tName: {firstN}\tBirthday: {birth}", ConsoleColor.Cyan);
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
    public void SelectById(string ID) {
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
        SqlHelper SQL = new SqlHelper(@"Data Source=localhost;Initial Catalog=DZ; User=sa; Password=qwerty112!");

        SQL.StartConnection();

        SQL.SelectAll();
        SQL.SelectById("2");

        SQL.TerminateConnection();
        //TODO: Interactive tests
        //TODO: Write update method
    }
}
