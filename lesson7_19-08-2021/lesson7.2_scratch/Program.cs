using System;

class Book {
    class Title {
        private string data;
        void Show() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Title: {data}");
            Console.ResetColor();
        }
        // We can use getters and setters but, the DZ says to do this way :(
        public void SetTitle(string title) {
            data = title;
        }
    }
    class Author {
        private string data;
        void Show() {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Title: {data}");
            Console.ResetColor();
        }
        public void SetAuthor(string author) {
            data = author;
        }
    }
    class Content {
        private string data;
        void Show() {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"Title: {data}");
            Console.ResetColor();
        }
        public void SetContent(string content) {
            data = content;
        }
    }
    // Default constructor
    public Book() {

    }
    // Custom constructor
    public Book(string title, string author, string content) {
        Book.Title Title = new Book.Title();
        Book.Author Author = new Book.Author();
        Book.Content Content = new Book.Content();

        Title.SetTitle(title);
        Author.SetAuthor(author);
        Content.SetContent(content);
    }
}
class Program {
    static void Main() {
        Console.Write("Input book name: ");
        string name = Console.ReadLine();

        Console.Write("Input book author: ");
        string author = Console.ReadLine();

        Console.Write("Input book content: ");
        string content = Console.ReadLine();

        // Instance
        Book book = new Book(title, author, content);

    }
}
